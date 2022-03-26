using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Message = $"Unknown command '{rawCommandType}'. Read the documentation to find out the signatures of the avaliable commands.";
        }
    }
}
