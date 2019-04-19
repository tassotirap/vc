namespace Verizon.Connect.Domain.Plot.Dto
{
    using Verizon.Connect.Domain.Plot.Enums;

    public class PlotQueryDto
    {
        public string VId { get; set; }

        public string Lat { get; set; }

        public string Lon { get; set; }

        public EventCode EventCode { get; set; }

        public string TimeStamp { get; set; }

        public string JourneyStart { get; set; }

        public string JourneyEnd { get; set; }
    }
}
