namespace Verizon.Connect.Domain.Plot.Events
{
    using Verizon.Connect.Domain.Core.Events;
    using Verizon.Connect.Domain.Plot.Models;

    public class RegisterPlotEvent : Event
    {
        public RegisterPlotEvent(PlotEntity entity)
        {
            this.Entity = entity;
        }

        public PlotEntity Entity { get; }
    }
}
