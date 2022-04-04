using Macropse.Domain.External;
using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;
using Macropse.Infrastructure.Module.Message.Args;
using Macropse.Infrastructure.Module.Message.Command;
using Macropse.Infrastructure.Module.Message.Params;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;


namespace Macropse.Domain.Logic.Parser
{
    public sealed class CommandBuilder : IBuilder<XElement, IExecutable>
    {
        private string[] AllowedKeywords = { "type", "params", "loop" };

        private OutputPackage<List<dynamic>> ParseRawParams(string rawParams, Specification.ICommandParamsInfo commandParamsInfo)
        {
            if (commandParamsInfo is null)
            {
                if (!(rawParams is null))
                {
                    return new OutputPackage<List<dynamic>>(item: default, errorMessage: new UnknownArgumentMessage("command", "params"));
                }
                return new OutputPackage<List<dynamic>>(item: default, errorMessage: default);
            }

            if (rawParams is null)
            {
                return new OutputPackage<List<dynamic>>(item: default, errorMessage: new EmptyArgumentMessage("params", "command"));
            }

            var rawParamsList = new List<string>();
            Regex.Split(rawParams, "('.*')")
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList()
                .ForEach(x =>
                {
                    if (!x.StartsWith("'") || !x.EndsWith("'"))
                    {
                        rawParamsList.AddRange(ParserUtills.ExtractRawParams(new string(x
                                            .Where(c => !Char.IsWhiteSpace(c))
                                            .ToArray())));
                    }
                    else
                    {
                        rawParamsList.Add(x);
                    }
                });

            if (rawParamsList.Count < commandParamsInfo.Bounds.MinCount || rawParamsList.Count > commandParamsInfo.Bounds.MaxCount)
            {
                return new OutputPackage<List<dynamic>>(item: default, errorMessage: new ParamsOutOfBoundsMessage(commandParamsInfo.Bounds, rawParamsList.Count));
            }

            var parsedParams = new List<dynamic>();

            var indexOfList = 0;
            var count = 0;
            foreach (var rawParamItem in rawParamsList)
            {
                Specification.ParamsTypeTable.TryGetValue(commandParamsInfo.ValidTypes[indexOfList].Type, out var validParam);
                object[] args = { rawParamItem };
                dynamic paramPac = typeof(ParamParser).GetMethod(nameof(ParamParser.ParseParam)).MakeGenericMethod(validParam).Invoke(null, args);
                if (paramPac.HasError)
                {
                    return new OutputPackage<List<dynamic>>(item: default(List<dynamic>), errorMessage: paramPac.ErrorMessage);
                }
                parsedParams.Add(paramPac.Item);
                count++;
                if (count >= commandParamsInfo.ValidTypes[indexOfList].Count)
                {
                    count = 0;
                    ++indexOfList;
                }
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

            if (!(typeVal is null) && typeVal.ToEnum<CommandType>(out var comtype))
            {
                var loop = (uint)1;
                if (!(sourceData.Attribute("loop") is null))
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

                var paramsPac = ParseRawParams(sourceData.Attribute("params")?.Value, paramsInfo);

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
