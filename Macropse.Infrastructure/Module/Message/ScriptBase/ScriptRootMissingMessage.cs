namespace Macropse.Infrastructure.Module.Message.ScriptBase
{
    public class ScriptRootMissingMessage : IMessage
    {
        public ResultCode ResultCode { get; }
        public string Message { get; }

        private ScriptRootMissingMessage()
        {
            ResultCode = ResultCode.GlobalParseError;
        }

        public ScriptRootMissingMessage(string nodeName) : this()
        {
            Message = $"Incorrect script root '{nodeName}'. Use <root> instead. Please, read the documentation.";
        }
    }
}
