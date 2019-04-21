namespace Verizon.Connect.Domain.Plot.Models
{
    using System;

    using Verizon.Connect.Domain.Core.Models;
    using Verizon.Connect.Domain.Plot.Enums;

    public class PlotEntity : Entity
    {
        public PlotEntity()
        {
        }

        public PlotEntity(int vId, int lat, int lon, DateTime timeStamp, EventCode eventCode)
        {
            this.EventCode = eventCode;
            this.Lat = lat;
            this.Lon = lon;
            this.TimeStamp = timeStamp;
            this.VId = vId;
        }

        public EventCode EventCode { get; set; }

        public int Lat { get; set; }

        public int Lon { get; set; }

        public DateTime TimeStamp { get; set; }

        public int VId { get; set; }
    }
}