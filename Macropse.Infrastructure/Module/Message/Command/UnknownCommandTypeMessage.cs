namespace Macropse.Infrastructure.Module.Message.Command
{
    public class UnknownCommandTypeMessage : IMessage
    {
        public ResultCode ResultCode { get; }
        public string Message { get; }

        private UnknownCommandTypeMessage()
        {
            ResultCode = ResultCode.ParseCommandError;
        }

        public UnknownCommandTypeMessage(string rawCommandType) : this()
        {
            Message = (rawCommandType is null) ? "Missing command type." : $"Unknown command '{rawCommandType}'.";
            Message += " Read the documentation to find out the signatures of the avaliable commands.";
        }
    }
}
