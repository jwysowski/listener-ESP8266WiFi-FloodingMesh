using MessageType;

namespace MessageTypes
{
    public class MessageTypesLUT
    {
        private Dictionary<char, MsgType> _types;

        public MessageTypesLUT()
        {
            _types = new Dictionary<char, MsgType>();
            _types.Add('0', new MsgType("Temperature", false, true, false));
            _types.Add('1', new MsgType("Humidity", false, false, true));
            _types.Add('2', new MsgType("Set temperature to auto", false, true, false));
            _types.Add('3', new MsgType("Set temperature rising", true, true, false));
            _types.Add('4', new MsgType("Set temperature falling", true, true, false));
            _types.Add('5', new MsgType("Set humidity to auto", false, false, true));
            _types.Add('6', new MsgType("Set humidity rising", true, false, true));
            _types.Add('7', new MsgType("Set humidity falling", true, false, true));
        }

        public string GetMessageType(char key)
        {
            return _types[key].Type;
        }

        public bool GetMode(char key)
        {
            return _types[key].InManualMode;
        }

        public bool SetTemperature(char key)
        {
            return _types[key].SetTemperature;
        }

        public bool SetHumidity(char key)
        {
            return _types[key].SetHumidity;
        }
    }
}
