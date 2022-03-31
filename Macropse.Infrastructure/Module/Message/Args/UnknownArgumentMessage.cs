namespace Macropse.Infrastructure.Module.Message.Args
{
    public class UnknownArgumentMessage : IMessage
    {
        public ResultCode ResultCode { get; }
        public string Message { get; }
        public UnknownArgumentMessage()
        {
            ResultCode = ResultCode.ArgumentParseError;
        }
        public UnknownArgumentMessage(string element, string argument) : this()
        {
            Message = $"Unknown argument '{argument}' in tag <{element}>.";
        }
    }
}
