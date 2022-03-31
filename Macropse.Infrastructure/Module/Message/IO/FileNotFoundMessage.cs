namespace Macropse.Infrastructure.Module.Message.IO
{
    public class FileNotFoundMessage : IMessage
    {
        public ResultCode ResultCode { get; }

        public string Message { get; }

        private FileNotFoundMessage()
        {
            ResultCode = ResultCode.IOError;
        }

        public FileNotFoundMessage(string fileName) : this()
        {
            Message = $"File '{fileName}' not found. Please, check your path or file name and try again.";
        }

        public FileNotFoundMessage(string fileName, string path) : this()
        {
            Message = $"File '{fileName}' at path '{path}' not found. Please, check your path or file name and try again.";
        }
    }
}
