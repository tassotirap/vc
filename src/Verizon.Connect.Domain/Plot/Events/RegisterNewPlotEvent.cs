namespace Verizon.Connect.Domain.Plot.Events
{
    using System;
    using Verizon.Connect.Domain.Core.Events;
    using Verizon.Connect.Domain.Plot.Models;

    public class RegisterNewPlotEvent : Event
    {
        public RegisterNewPlotEvent(PlotEntity entity)
        {
            this.Entity = entity;
        }

        public PlotEntity Entity { get; }
    }
}
