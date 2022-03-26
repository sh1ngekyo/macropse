using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using macropse.MessageSystem.Message;

namespace macropse.MessageSystem
{
    public interface IMessageSender
    {
        void SendMessage(IMessage message);
    }
}
