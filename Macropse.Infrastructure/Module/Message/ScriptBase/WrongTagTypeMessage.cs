namespace Macropse.Infrastructure.Module.Message.ScriptBase
{
    public class WrongTagTypeMessage : IMessage
    {
        public ResultCode ResultCode { get; }
        public string Message { get; }

        private WrongTagTypeMessage()
        {
            ResultCode = ResultCode.GlobalParseError;
        }

        public WrongTagTypeMessage(string nodeName, string expected) : this()
        {
            Message = $"Incorrect tag <{nodeName}>. Expected <{expected}>. Please, read the documentation.";
        }
    }
}
