using Macropse.Infrastructure.Module.Message;

using System;

namespace Macropse.Presentation.Evaluator
{
    internal class ConsoleMessageSender : IMessageSender
    {
        public void SendMessage(IMessage message)
        {
            Console.WriteLine(message.Message);
        }
    }
}
