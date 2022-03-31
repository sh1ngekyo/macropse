namespace Macropse.Infrastructure.Module.Message.Command
{
    public class NestedCommandNotAllowedMessage : IMessage
    {
        public ResultCode ResultCode { get; }
        public string Message { get; }
        public NestedCommandNotAllowedMessage()
        {
            ResultCode = ResultCode.ParseCommandError;
            Message = $"Tag <command> can't be nested. Please, read the documentation.";
        }
    }
}
