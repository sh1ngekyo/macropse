using Macropse.Domain.External;
using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Macro;
using Macropse.Domain.Logic.Settings;
using Macropse.Infrastructure.Module.IO;
using Macropse.Infrastructure.Module.Message.Command;
using Macropse.Infrastructure.Module.Message.ScriptBase;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Macropse.Domain.Logic.Parser
{
    public class ScriptParser
    {
        static ScriptParser()
        {

        }

        private OutputPackage<uint> ParseRepeats(XmlNode node)
        {
            if (node.Attributes.GetNamedItem("loop") != null)
            {
                if (uint.TryParse(node.Attributes.GetNamedItem("loop")?.Value, out uint repeats))
                {
                    return new OutputPackage<uint>(item: repeats, default);
                }
                return new OutputPackage<uint>(item: default, errorMessage: new IncorrectRepeatValueMessage(node.Attributes.GetNamedItem("loop").Value));
            }
            return new OutputPackage<uint>(item: 1, default);
        }

        private OutputPackage<List<object>> TryParseParams(Specification.ICommandParamsInfo paramsInfo, List<string> rawParams)
        {
            if (rawParams is null || rawParams.Count < paramsInfo.Bounds.Item1 || rawParams.Count > paramsInfo.Bounds.Item2)
            {
                return new OutputPackage<List<object>>(item: default, errorMessage: new ParamsOutOfBoundsMessage((paramsInfo.Bounds.Item1, paramsInfo.Bounds.Item2), rawParams is null ? 0 : rawParams.Count));
            }
            var result = new List<object>();
            for (var i = 0; i < rawParams.Count; ++i)
            {
                object[] args = { rawParams[i], null };
                Specification.ParamsTypeTable.TryGetValue(paramsInfo.ValidTypes[i], out var type);
                if (!(bool)typeof(ParserUtills).GetMethod(nameof(ParserUtills.TryToParam)).MakeGenericMethod(type).Invoke(null, args))
                {
                    return new OutputPackage<List<object>>(item: default, errorMessage: new IncorrectParamMessage(rawParams[i], type.Name));
                }
                result.Add(args[1]);
            }
            return new OutputPackage<List<object>>(item: result, default);
        }

        private OutputPackage<IExecutable> TryParseCommand(XmlNode command_node)
        {
            var type = command_node.Attributes.GetNamedItem("type")?.Value;
            if (type is null)
            {
                return new OutputPackage<IExecutable>(item: default, errorMessage: new EmptyCommandTypeMessage());
            }
            if(!ParserUtills.ToEnum<CommandType>(type, out var comtype))
            {
                return new OutputPackage<IExecutable>(item: default, errorMessage: new UnknownCommandTypeMessage(type));
            }
            if (Specification.CommandTable.TryGetValue(comtype, out var creator))
            {
                var rawParams = ParserUtills.ExtractRawParams(new string(command_node.Attributes.GetNamedItem("params")?.Value.Where(c => !Char.IsWhiteSpace(c)).ToArray()));
                Specification.ParamsTable.TryGetValue(comtype, out var paramsInfo);
                var result = TryParseParams(paramsInfo, rawParams);
                if (result.HasError)
                {
                    return new OutputPackage<IExecutable>(item: default, errorMessage: result.ErrorMessage);
                }
                var repeats = ParseRepeats(command_node);
                if(repeats.HasError)
                {
                    return new OutputPackage<IExecutable>(item: default, errorMessage: repeats.ErrorMessage);
                }
                return new OutputPackage<IExecutable>(item: creator.Create(result.Item, repeats.Item), errorMessage: default);
            }
            return new OutputPackage<IExecutable>(item: default, errorMessage: new UnknownCommandTypeMessage(type));
        }

        private OutputPackage<List<Macros>> GetMacros(XmlElement root)
        {
            var macros = new List<Macros>();
            foreach (XmlElement macroNode in root)
            {
                var commandList = new List<IExecutable>();
                foreach (XmlNode commandNode in macroNode.ChildNodes)
                {
                    var result = TryParseCommand(commandNode);
                    if (result.HasError)
                    {
                        return new OutputPackage<List<Macros>>(item: default, errorMessage: result.ErrorMessage);
                    }
                    commandList.Add(result.Item);
                }
                var name = macroNode.Attributes.GetNamedItem("name")?.Value;
                var repeats = ParseRepeats(macroNode);
                if(repeats.HasError)
                {
                    return new OutputPackage<List<Macros>>(item: default, errorMessage: repeats.ErrorMessage);
                }
                macros.Add(new Macros(name, commandList, repeats.Item));
            }
            return new OutputPackage<List<Macros>>(item: macros, errorMessage: default);
        }

        private OutputPackage<XmlDocument> LoadDoc(Script script)
        {
            var doc = new XmlDocument();
            try
            {
                doc.LoadXml(script.Content);
                return new OutputPackage<XmlDocument>(item: doc, errorMessage: default);
            }
            catch (Exception e)
            {
                return new OutputPackage<XmlDocument>(item: default,
                                                     errorMessage: new IncorrectScriptMessage(script.Name, e.Message));
            }
        }

        public OutputPackage<List<Macros>> Parse(Script script)
        {
            var doc = LoadDoc(script);
            if (doc.HasError)
            {
                return new OutputPackage<List<Macros>>(item: default, errorMessage: doc.ErrorMessage);
            }
            var macroList = GetMacros(doc.Item.DocumentElement);
            return macroList.HasError
                ? new OutputPackage<List<Macros>>(item: default, errorMessage: macroList.ErrorMessage)
                : new OutputPackage<List<Macros>>(macroList.Item, default);
        }
    }
}
