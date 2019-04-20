namespace Verizon.Connect.Domain.Plot.Models
{
    using Verizon.Connect.Domain.Core.Models;
    using Verizon.Connect.Domain.Plot.Enums;

    public class PlotEntity : Entity
    {
        public string VId { get; set; }

        public string Lat { get; set; }

        public string Lon { get; set; }

        public EventCode EventCode { get; set; }

        public string TimeStamp { get; set; }

        public string JourneyStart { get; set; }

        public string JourneyEnd { get; set; }

        public PlotEntity()
        {
        }

        public PlotEntity(int vId, int timeStamp, EventCode eventCode)
        {
            this.EventCode = eventCode;
            this.Lat = $"la{timeStamp}";
            this.Lon = $"lo{timeStamp}";
            this.TimeStamp = $"t{timeStamp}";
            this.VId = $"VId{vId}";
        }
    }
}
