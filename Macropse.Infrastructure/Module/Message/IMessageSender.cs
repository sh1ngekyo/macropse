namespace Macropse.Infrastructure.Module.Message
{
    public interface IMessageSender
    {
        void SendMessage(IMessage message);
    }
}
