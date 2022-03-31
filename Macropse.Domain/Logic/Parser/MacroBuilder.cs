using Macropse.Domain.External;
using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Macro;
using Macropse.Infrastructure.Module.Driver;
using Macropse.Infrastructure.Module.Message.Args;
using Macropse.Infrastructure.Module.Message.ScriptBase;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Macropse.Domain.Logic.Parser
{
    public sealed class MacroBuilder : IBuilder<XElement, Macros>
    {
        private string[] AllowedKeywords = { "keys", "name", "loop" };

        public OutputPackage<Macros> BuildObject(XElement sourceData)
        {
            if (sourceData.Name != "macro")
            {
                return new OutputPackage<Macros>(item: default, errorMessage: new WrongTagTypeMessage(sourceData.Name.LocalName, "macro"));
            }

            if (!sourceData.HasElements)
            {
                return new OutputPackage<Macros>(item: default, errorMessage: new EmptyNestedTagMessage(sourceData.Name.LocalName, "command"));
            }

            foreach (var curAttr in sourceData.Attributes())
            {
                if (!ParserUtills.TryToValidateKeyword(curAttr.Name.LocalName, AllowedKeywords))
                {
                    return new OutputPackage<Macros>(item: default, errorMessage: new UnknownArgumentMessage(sourceData.Name.LocalName, curAttr.Name.LocalName));
                }
            }

            var rawKeysValues = ParserUtills.ExtractRawParams(
                    new string(sourceData.Attribute("keys")?.Value
                    .Where(c => !Char.IsWhiteSpace(c))
                    .ToArray()));

            if (rawKeysValues == null)
            {
                return new OutputPackage<Macros>(item: default, errorMessage: new EmptyArgumentMessage("keys", sourceData.Name.LocalName));
            }

            var keys = new List<VirtualKey>();
            var name = sourceData.Attribute("name")?.Value;
            var loop = (uint)1;

            foreach (var rawKeyValue in rawKeysValues)
            {
                var keyPac = ParamParser.ParseParam<VirtualKey>(rawKeyValue);
                if (keyPac.HasError)
                {
                    return new OutputPackage<Macros>(default, errorMessage: keyPac.ErrorMessage);
                }
                keys.Add(keyPac.Item);
            }

            if (sourceData.Attribute("loop") != null)
            {
                var loopPac = ParamParser.ParseParam<uint>(sourceData.Attribute("loop").Value);
                if (loopPac.HasError)
                {
                    return new OutputPackage<Macros>(default, errorMessage: loopPac.ErrorMessage);
                }
                loop = loopPac.Item;
            }

            var commandList = new List<IExecutable>();
            var commandBuilder = new CommandBuilder();

            foreach (var commandNode in sourceData.Elements())
            {
                if (commandNode.Name != "command")
                {
                    return new OutputPackage<Macros>(item: default, errorMessage: new WrongTagTypeMessage(commandNode.Name.LocalName, "command"));
                }
                var commandPac = commandBuilder.BuildObject(commandNode);
                if (commandPac.HasError)
                {
                    return new OutputPackage<Macros>(item: default, errorMessage: commandPac.ErrorMessage);
                }
                commandList.Add(commandPac.Item);
            }

            return new OutputPackage<Macros>(item: new Macros(name, keys, commandList, loop), errorMessage: default);
        }
    }
}
