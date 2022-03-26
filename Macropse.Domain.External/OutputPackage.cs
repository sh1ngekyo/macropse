using Macropse.Infrastructure.Module.Message;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Domain.External
{
    public class OutputPackage<T>
    {
        public bool HasError { get; }
        public IMessage ErrorMessage { get; }
        public T Item { get; }

        public OutputPackage(T item, IMessage errorMessage)
        {
            Item = item;
            ErrorMessage = errorMessage;
            HasError = !(ErrorMessage is null);
        }
    }
}
