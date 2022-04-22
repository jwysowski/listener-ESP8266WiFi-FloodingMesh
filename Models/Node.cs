using System.ComponentModel.DataAnnotations;

namespace Listener.Models
{
    public class Node
    {
        public int Id { get; set; }
        public string NodeId { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        [DataType(DataType.Date)]
        public DateTime LastUpdate { get; set; }
        public bool InManualMode { get; set; } = false;
        public double? TargetTemperature { get; set; }
        public double? TargetHumidity { get; set; }
        public bool Arch { get; set; }
    }
}