namespace Macropse.Infrastructure.Module.Message.ScriptBase
{
    public class EmptyNestedTagMessage : IMessage
    {
        public ResultCode ResultCode { get; }
        public string Message { get; }

        private EmptyNestedTagMessage()
        {
            ResultCode = ResultCode.GlobalParseError;
        }

        public EmptyNestedTagMessage(string tagName, string expectedNestedTag) : this()
        {
            Message = $"Unexpected empty tag <{tagName}>. Expected at least one <{expectedNestedTag}>. Please, read the documentation.";
        }
    }
}
