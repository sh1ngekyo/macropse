namespace Macropse.Infrastructure.Module.Message.Args
{
    public class EmptyArgumentMessage : IMessage
    {
        public ResultCode ResultCode { get; }

        public string Message { get; }

        private EmptyArgumentMessage()
        {
            ResultCode = ResultCode.ArgumentParseError;
        }

        public EmptyArgumentMessage(string argument) : this()
        {
            Message = $"Argument '{argument}' must not be empty. Addition information not provided. Please, read the documentation.";
        }

        public EmptyArgumentMessage(string argument, string element) : this()
        {
            Message = $"Argument '{argument}' in '<{element}>' must not be empty or missing. Please, read the documentation.";
        }
    }
}
