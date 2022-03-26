using macropse.MessageSystem.Message;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace macropse.Parser
{
    class ObjectPackage<T>
    {
        public bool HasError { get; }
        public IMessage ErrorMessage { get; }
        public T Item { get; }

        public ObjectPackage(T item, IMessage errorMessage)
        {
            Item = item;
            ErrorMessage = errorMessage;
            HasError = !(ErrorMessage is null);
        }
    }
}
