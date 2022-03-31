namespace Macropse.Infrastructure.Module.Message.Params
{
    public class IncorrectParamMessage : IMessage
    {
        public ResultCode ResultCode { get; }
        public string Message { get; }
        public IncorrectParamMessage()
        {
            ResultCode = ResultCode.ParseCommandError;
        }
        public IncorrectParamMessage(string curParam, string expected) : this()
        {
            Message = $"Expected '{expected}', got '{curParam}'. Read the documentation to find out the signatures of the avaliable commands.";
        }
    }
}
