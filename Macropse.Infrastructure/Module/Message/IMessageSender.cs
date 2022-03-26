using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Infrastructure.Module.Message
{
    public interface IMessageSender
    {
        void SendMessage(IMessage message);
    }
}
