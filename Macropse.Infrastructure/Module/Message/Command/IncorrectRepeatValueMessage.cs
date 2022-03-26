using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Infrastructure.Module.Message.Command
{
    public class IncorrectRepeatValueMessage : IMessage
    {
        public ResultCode ResultCode { get; }
        public string Message { get; }
        public IncorrectRepeatValueMessage()
        {
            ResultCode = ResultCode.ParseCommandError;
        }
        public IncorrectRepeatValueMessage(string value) : this()
        {
            Message = $"Can't convert {value} to unsigned integer.";
        }
    }
}
