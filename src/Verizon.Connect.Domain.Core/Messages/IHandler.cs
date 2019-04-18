namespace Verizon.Connect.Domain.Core.Messages
{
    public interface IHandler<in T> where T : Message
    {
        void Handle(T message);
    }
}
