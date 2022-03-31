using Macropse.Domain.External;
using Macropse.Infrastructure.Module.Message.Params;

namespace Macropse.Domain.Logic.Parser
{
    public static class ParamParser
    {
        public static OutputPackage<T> ParseParam<T>(string rawValue)
        {
            object[] args = { rawValue, null };
            if (!(bool)typeof(ParserUtills).GetMethod(nameof(ParserUtills.TryToParam)).MakeGenericMethod(typeof(T)).Invoke(null, args))
            {
                return new OutputPackage<T>(item: default, errorMessage: new IncorrectParamMessage(rawValue, typeof(T).Name));
            }
            return new OutputPackage<T>(item: (T)args[1], errorMessage: default);
        }
    }
}
