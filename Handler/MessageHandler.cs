using MQTTnet;
using Listener.Models;
using MessageTypes;
using System.Diagnostics;

namespace MessageHandler
{
    public class Handler
    {
        private readonly ListenerContext _context;
        private readonly MessageTypesLUT _typesLUT;
        private bool _isCommand;
        private long _start;
        private long _end;

        public Handler(ListenerContext context)
        {
            _context = context;
            _typesLUT = new MessageTypesLUT();
            _isCommand = false;
            _start = 0;
            _end = 0;
        }
        public async Task Handle(MqttApplicationMessageReceivedEventArgs msg)
        {
            await SaveLog(msg);
        }

        private async Task SaveLog(MqttApplicationMessageReceivedEventArgs msg)
        {
            var log = new Log();

            log.Message = System.Text.Encoding.UTF8.GetString(
                msg?.ApplicationMessage?.Payload ?? Array.Empty<byte>());


            if (log.Message[0] != ':')
                return;

            log.NodeId = log.Message.Substring(7, 10);
            log.Type = _typesLUT.GetMessageType(log.Message[1]);
            if (log.Type != "Temperature" && log.Type != "Humidity")
            {
                _isCommand = true;
                _start = Stopwatch.GetTimestamp();
            }
            else
            {
                _isCommand = false;
                _end = Stopwatch.GetTimestamp();
                var ts = _end - _start;
                double ts_d = Convert.ToDouble(ts);
                ts_d /= Stopwatch.Frequency;
                Console.WriteLine("Elapsed time: {0:N5}", ts_d);
            }

            log.Value = Convert.ToDouble(log.Message.Substring(2, 5));
            log.Timestamp = System.DateTime.Now;

            _context.Log.Add(log);
            await _context.SaveChangesAsync();
            await SaveNode(log);
        }

        private async Task SaveNode(Log log)
        {
            bool notInList = false;
            var node = _context.Node.FirstOrDefault(n => n.NodeId == log.NodeId);
            if (node == null)
            {
                node = new Node();
                notInList = true;
            }

            node.NodeId = log.NodeId;
            if (!_isCommand)
            {
                if (_typesLUT.SetTemperature(log.Message[1]))
                    node.Temperature = log.Value;
                else
                    node.Humidity = log.Value;
            }
            else
            {
                if (!_typesLUT.GetMode(log.Message[1]))
                {
                    node.TargetTemperature = null;
                    node.TargetHumidity = null;
                }
                else if (_typesLUT.SetTemperature(log.Message[1]))
                {
                    node.TargetTemperature = log.Value;
                }
                else
                {
                    node.TargetHumidity = log.Value;
                }
            }

            node.InManualMode = _isCommand ? _typesLUT.GetMode(log.Message[1]) : node.InManualMode;
            node.LastUpdate = log.Timestamp;
            node.Arch = false;

            if (notInList)
                _context.Node.Add(node);

            await _context.SaveChangesAsync();
        }
    }
}