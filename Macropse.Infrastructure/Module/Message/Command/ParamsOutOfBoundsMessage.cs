using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Infrastructure.Module.Message.Command
{
    public class ParamsOutOfBoundsMessage : IMessage
    {
        public ResultCode ResultCode { get; }
        public string Message { get; }
        public ParamsOutOfBoundsMessage()
        {
            ResultCode = ResultCode.ParseCommandError;
        }
        public ParamsOutOfBoundsMessage((uint, uint) bounds, int count) : this()
        {
            Message = $"Params count '{count}' out of bounds '[{bounds.Item1},{bounds.Item2}]'. Read the documentation to find out the signatures of the avaliable commands.";
        }
    }
}
