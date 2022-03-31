using Macropse.Domain.External;

namespace Macropse.Domain.Logic.Interfaces
{
    public interface IBuilder<InType, OutType>
    {
        OutputPackage<OutType> BuildObject(InType sourceData);
    }
}
