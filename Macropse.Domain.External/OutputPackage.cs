using Macropse.Infrastructure.Module.Message;

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
