using Macropse.Domain.External;
using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;
using Macropse.Infrastructure.Module.Message.Args;
using Macropse.Infrastructure.Module.Message.Command;
using Macropse.Infrastructure.Module.Message.Params;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace Macropse.Domain.Logic.Parser
{
    public sealed class CommandBuilder : IBuilder<XElement, IExecutable>
    {
        private string[] AllowedKeywords = { "type", "params", "loop" };

        private OutputPackage<List<dynamic>> ParseRawParams(List<string> rawParams, Specification.ICommandParamsInfo commandParamsInfo)
        {
            if (commandParamsInfo is null)
            {
                if(rawParams != null)
                {
                    return new OutputPackage<List<dynamic>>(item: default, errorMessage: new UnknownArgumentMessage("command", "params"));
                }
                return new OutputPackage<List<dynamic>>(item: default, errorMessage: default);
            }

            if (rawParams is null)
            {
                return new OutputPackage<List<dynamic>>(item: default, errorMessage: new EmptyArgumentMessage("params", "command"));
            }

            if (rawParams.Count < commandParamsInfo.Bounds.MinCount || rawParams.Count > commandParamsInfo.Bounds.MaxCount)
            {
                return new OutputPackage<List<dynamic>>(item: default, errorMessage: new ParamsOutOfBoundsMessage(commandParamsInfo.Bounds, rawParams.Count));
            }

            var parsedParams = new List<dynamic>();

            for (var i = 0; i < rawParams.Count; ++i)
            {
                Specification.ParamsTypeTable.TryGetValue(commandParamsInfo.ValidTypes[i], out var paramType);
                object[] args = { rawParams[i] };
                dynamic paramPac = typeof(ParamParser).GetMethod(nameof(ParamParser.ParseParam)).MakeGenericMethod(paramType).Invoke(null, args);
                if (paramPac.HasError)
                {
                    return new OutputPackage<List<dynamic>>(item: default(List<dynamic>), errorMessage: paramPac.ErrorMessage);
                }
                parsedParams.Add(paramPac.Item);
            }

            return new OutputPackage<List<dynamic>>(item: parsedParams, errorMessage: default);
        }

        public OutputPackage<IExecutable> BuildObject(XElement sourceData)
        {
            if (sourceData.Attribute("type") is null)
            {
                return new OutputPackage<IExecutable>(item: default, errorMessage: new EmptyCommandTypeMessage());
            }

            if (sourceData.HasElements)
            {
                return new OutputPackage<IExecutable>(item: default, errorMessage: new NestedCommandNotAllowedMessage());
            }

            foreach (var curAttr in sourceData.Attributes())
            {
                if (!ParserUtills.TryToValidateKeyword(curAttr.Name.LocalName, AllowedKeywords))
                {
                    return new OutputPackage<IExecutable>(item: default, errorMessage: new UnknownArgumentMessage(sourceData.Name.LocalName, curAttr.Name.LocalName));
                }
            }

            var typeVal = sourceData.Attribute("type")?.Value;

            if (typeVal != null && typeVal.ToEnum<CommandType>(out var comtype))
            {
                var loop = (uint)1;
                if (sourceData.Attribute("loop") != null)
                {
                    var loopPac = ParamParser.ParseParam<uint>(sourceData.Attribute("loop").Value);
                    if (loopPac.HasError)
                    {
                        return new OutputPackage<IExecutable>(item: default, errorMessage: loopPac.ErrorMessage);
                    }
                    loop = loopPac.Item;
                }

                Specification.CommandTable.TryGetValue(comtype, out var creator);
                Specification.ParamsTable.TryGetValue(comtype, out var paramsInfo);
                var paramsPac = ParseRawParams(ParserUtills.ExtractRawParams(
                    new string(sourceData.Attribute("params")?.Value
                    .Where(c => !Char.IsWhiteSpace(c))
                    .ToArray())),
                    paramsInfo);

                if (paramsPac.HasError)
                {
                    return new OutputPackage<IExecutable>(item: default, errorMessage: paramsPac.ErrorMessage);
                }
                return new OutputPackage<IExecutable>(item: creator.Create(paramsPac.Item, loop), errorMessage: default);
            }

            return new OutputPackage<IExecutable>(item: default, errorMessage: new UnknownCommandTypeMessage(typeVal));
        }
    }
}
