namespace Macropse.Infrastructure.Module.Message.ScriptBase
{
    public class IncorrectScriptMessage : IMessage
    {
        public ResultCode ResultCode { get; }
        public string Message { get; }

        private IncorrectScriptMessage()
        {
            ResultCode = ResultCode.GlobalParseError;
        }

        public IncorrectScriptMessage(string scriptName) : this()
        {
            Message = $"Incorrect script '{scriptName}'. Addition information not provided. Please, read the documentation.";
        }

        public IncorrectScriptMessage(string scriptName, string errorMessage) : this()
        {
            Message = $"Incorrect script '{scriptName}'. {errorMessage}. Please, check your script.";
        }
    }
}
