using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Infrastructure.Module.Message
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
            Message = $"Argument '{argument}' should not be empty. Addition information not provided. Please, read the documentation.";
        }

        public EmptyArgumentMessage(string argument, string element) : this()
        {
            Message = $"Argument '{argument}' in '<{element}>' should not be empty. Addition information not provided. Please, read the documentation.";
        }

        public EmptyArgumentMessage(string argument, Tuple<uint, uint> errorPosition) : this()
        {
            Message = $"Argument '{argument}' at line '{errorPosition.Item1}' and '{errorPosition.Item2}' should not be empty. Please, read the documentation.";
        }

        public EmptyArgumentMessage(string argument, string element, Tuple<uint, uint> errorPosition) : this()
        {
            Message = $"Argument '{argument}' in '<{element}>' at line '{errorPosition.Item1}' and '{errorPosition.Item2}' should not be empty. Please, read the documentation.";
        }
    }
}
