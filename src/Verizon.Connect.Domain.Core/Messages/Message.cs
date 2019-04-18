namespace Verizon.Connect.Domain.Core.Messages
{
    using MediatR;

    public abstract class Message : IRequest<bool>
    {
        public string MessageType { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
