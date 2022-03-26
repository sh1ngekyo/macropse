using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Infrastructure.Module.Message.Command
{
    public class EmptyCommandTypeMessage : IMessage
    {
        public ResultCode ResultCode { get; }
        public string Message { get; }
        public EmptyCommandTypeMessage()
        {
            ResultCode = ResultCode.ParseCommandError;
            Message = $"Command without type isn't allowed. Read the documentation to find out the signatures of the avaliable commands.";
        }
    }
}
