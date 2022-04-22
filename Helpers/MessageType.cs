namespace MessageType
{
    public class MsgType
    {
        public string Type { get; set; } = string.Empty;
        public bool InManualMode { get; set; } = false;
        public bool SetTemperature { get; set; } = false;
        public bool SetHumidity { get; set; } = false;

        public MsgType(string type, bool inManualMode, bool setTemperature, bool setHumidity)
        {
            Type = type;
            InManualMode = inManualMode;
            SetTemperature = setTemperature;
            SetHumidity = setHumidity;
        }
    }
}