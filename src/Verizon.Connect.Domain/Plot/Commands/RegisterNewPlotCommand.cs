namespace Verizon.Connect.Domain.Plot.Commands
{
    using Verizon.Connect.Domain.Core.Commands;
    using Verizon.Connect.Domain.Plot.Models;

    public class RegisterNewPlotCommand : Command
    {
        public RegisterNewPlotCommand(PlotEntity entity)
        {
            Entity = entity;
        }

        public PlotEntity Entity { get; }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(Entity.Lat)
                && !string.IsNullOrEmpty(Entity.Lon)
                && !string.IsNullOrEmpty(Entity.TimeStamp)
                && !string.IsNullOrEmpty(Entity.VId);
        }
    }
}
