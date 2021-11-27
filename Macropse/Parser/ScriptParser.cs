using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO.File;
using System.Xml;
using macropse.Macros;
using macropse.Macros.Commands;

namespace macropse.Parser
{
    class ScriptParser
    {
        private T ParseStringToEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        private uint ParseRepeats(XmlNode node)
        {
            if (uint.TryParse(node.Attributes.GetNamedItem("loop")?.Value, out uint repeats))
            {
                return repeats;
            }
            throw new ArgumentException($"Can't convert {node.Attributes.GetNamedItem("loop").Value} to unsigned integer.", "loop");
        }

        public List<string> GetRawParamsRepresentation(string xmlAttributeValue)
        {
            return string.IsNullOrEmpty(xmlAttributeValue) ? null : xmlAttributeValue.ToLower().Split(new char[] { ',' }).ToList();
        }

        public bool TryParseParams(Specification.ICommandParamsInfo paramsInfo, List<string> rawParams, out List<object> result)
        {
            result = null;
            if (rawParams.Count < paramsInfo.Bounds.Item1)
            {
                //not enough
                return false;
            }
            else if (rawParams.Count > paramsInfo.Bounds.Item2)
            {
                //too many
                return false;
            }
            result = new List<object>();
            for (var i = 0; i < rawParams.Count; ++i)
            {
                object[] args = { rawParams[i], null };
                Specification.ParamsTypeTable.TryGetValue(paramsInfo.ValidTypes[i], out var type);
                if (!(bool)typeof(AttributeParser).GetMethod(nameof(AttributeParser.TryParseParam)).MakeGenericMethod(type).Invoke(null, args))
                {
                    return false;
                }
                result.Add(args[1]);
            }
            return true;
        }

        private bool TryParseCommand(XmlNode command_node, out ICommand result)
        {
            result = null;
            var type = command_node.Attributes.GetNamedItem("type")?.Value;
            if (type is null)
            {
                return false;
            }
            if (Specification.CommandTable.TryGetValue(ParseStringToEnum<CommandType>(type), out var creator))
            {
                var rawParams = GetRawParamsRepresentation(new string(command_node.Attributes.GetNamedItem("params")?.Value.Where(c => !Char.IsWhiteSpace(c)).ToArray()));
                Specification.ParamsTable.TryGetValue(ParseStringToEnum<CommandType>(type), out var paramsInfo);
                if (TryParseParams(paramsInfo, rawParams, out var curParams))
                {
                    result = creator.Create(curParams, 1);
                    return true;
                }
            }
            return false;
        }

        public List<Macro> Parse(Script script)
        {
            var doc = new XmlDocument();
            try
            {
                doc.LoadXml(script.Content);
            }
            catch (Exception e)
            {
                throw e;
            }
            var root = doc?.DocumentElement;
            var macros = new List<Macro>();
            if (root != null)
            {
                foreach (XmlElement macroNode in root)
                {
                    var name = macroNode.Attributes.GetNamedItem("name")?.Value;
                    var commandList = new List<ICommand>();
                    foreach (XmlNode commandNode in macroNode.ChildNodes)
                    {
                        if (TryParseCommand(commandNode, out var command))
                        {
                            commandList.Add(command);
                        }
                    }
                    macros.Add(new Macro(name, commandList, 1));
                }
            }
            return macros;
        }
    }
}
