using Macropse.Domain.External;
using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Output;
using Macropse.Infrastructure.Module.Driver;
using Macropse.Infrastructure.Module.Message.Args;
using Macropse.Infrastructure.Module.Message.ScriptBase;

using System.Collections.Generic;
using System.Xml.Linq;

namespace Macropse.Domain.Logic.Parser
{
    public sealed class ScriptRootBuilder : IBuilder<XElement, Header>
    {
        private Dictionary<string, dynamic> AllowedAttributesDefaultValues = new Dictionary<string, dynamic>()
        {
            {"pause", VirtualKey.Pause },
            {"delay", 100 },
            {"whilePressed", false },
            {"ifWinActive", string.Empty }
        };

        public OutputPackage<Header> BuildObject(XElement sourceData)
        {
            if (sourceData.Name != "root")
            {
                return new OutputPackage<Header>(item: default, errorMessage: new ScriptRootMissingMessage(sourceData.Name.LocalName));
            }

            if(!sourceData.HasElements)
            {
                return new OutputPackage<Header>(item: default, errorMessage: new EmptyNestedTagMessage(sourceData.Name.LocalName, "macro"));
            }

            foreach (var curAttr in sourceData.Attributes())
            {
                if (AllowedAttributesDefaultValues.TryGetValue(curAttr.Name.LocalName, out var result))
                {
                    object[] args = { curAttr.Value };
                    dynamic paramPac = typeof(ParamParser).GetMethod(nameof(ParamParser.ParseParam)).MakeGenericMethod(result.GetType()).Invoke(null, args);
                    if (paramPac.HasError)
                    {
                        return new OutputPackage<Header>(item: default(Header), errorMessage: paramPac.ErrorMessage);
                    }
                    AllowedAttributesDefaultValues[curAttr.Name.LocalName] = paramPac.Item;
                }
                else
                {
                    return new OutputPackage<Header>(item: default, errorMessage: new UnknownArgumentMessage(sourceData.Name.LocalName, curAttr.Name.LocalName));
                }
            }

            AllowedAttributesDefaultValues.TryGetValue("pause", out var pauseAttr);
            AllowedAttributesDefaultValues.TryGetValue("delay", out var delayAttr);
            AllowedAttributesDefaultValues.TryGetValue("whilePressed", out var whilePressedAttr);
            AllowedAttributesDefaultValues.TryGetValue("ifWinActive", out var ifWinActiveAttr);

            return new OutputPackage<Header>(item: new Header(pauseAttr, delayAttr, whilePressedAttr, ifWinActiveAttr), errorMessage: default);
        }
    }
}
