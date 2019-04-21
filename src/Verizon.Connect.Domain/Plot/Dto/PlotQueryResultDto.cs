namespace Verizon.Connect.Domain.Plot.Dto
{
    using System;

    using Verizon.Connect.Domain.Plot.Enums;
    using Verizon.Connect.Domain.Plot.Models;

    public class PlotQueryResultDto
    {
        public PlotQueryResultDto(PlotEntity plotEntity)
        {
            this.EventCode = plotEntity.EventCode.ToString();
            this.Lat = plotEntity.Lat;
            this.Lon = plotEntity.Lon;
            this.VId = plotEntity.VId;
            this.TimeStamp = plotEntity.TimeStamp;
        }

        public string EventCode { get; set; }

        public DateTime? JourneyEnd { get; set; }

        public DateTime? JourneyStart { get; set; }

        public int Lat { get; set; }

        public int Lon { get; set; }

        public DateTime TimeStamp { get; set; }

        public int VId { get; set; }
    }
}