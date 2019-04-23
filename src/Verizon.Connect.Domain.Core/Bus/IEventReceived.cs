namespace Verizon.Connect.Domain.Core.Bus
{
    using System.Threading.Tasks;
    using Verizon.Connect.Domain.Core.Events;

    public interface IEventReceived<in T> where T : Event
    {
        /// <summary>
        /// Event Received
        /// </summary>
        /// <param name="event">Event object</param>
        /// <returns>Return success</returns>
        Task<bool> EventReceived(T @event);
    }
}
