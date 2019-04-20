namespace Verizon.Connect.Domain.Plot.Dto
{
    using Verizon.Connect.Domain.Plot.Enums;
    using Verizon.Connect.Domain.Plot.Models;

    public class PlotQueryResultDto
    {
        public PlotQueryResultDto(PlotEntity plotEntity)
        {
            this.EventCode = plotEntity.EventCode;
            this.Lat = plotEntity.Lat;
            this.Lon = plotEntity.Lon;
            this.VId = plotEntity.VId;
            this.TimeStamp = plotEntity.TimeStamp;
        }

        public EventCode EventCode { get; set; }

        public string JourneyEnd { get; set; }

        public string JourneyStart { get; set; }

        public string Lat { get; set; }

        public string Lon { get; set; }

        public string TimeStamp { get; set; }

        public string VId { get; set; }
    }
}