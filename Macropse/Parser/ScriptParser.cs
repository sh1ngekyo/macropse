using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO.File;
using System.Xml;
using macropse.Commands;

namespace macropse.Parser
{
    class ScriptParser
    {
        private T ParseStringToEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        private ICommand parseCommand(XmlNode command_node)
        {
            var _type = command_node.Attributes.GetNamedItem("type").Value;
            /*if (_type is null)
            {
                throw new ArgumentNullException();
            }
            var _params = command_node.Attributes.GetNamedItem("params").Value;
            var _loop = command_node.Attributes.GetNamedItem("loop").Value;*/
            Specification.CommandTable.TryGetValue(ParseStringToEnum<CommandType>(_type), out var creator);
            return creator.Create(null, 1);
        }

        public List<Macros.Macro> Parse(Script script)
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
            var macros = new List<Macros.Macro>();
            if (root != null)
            {
                foreach (XmlElement macroNode in root)
                {
                    var name = macroNode.Attributes.GetNamedItem("name")?.Value;
                    var commandList = new List<ICommand>();
                    foreach(XmlNode commandNode in macroNode.ChildNodes)
                    {
                        commandList.Add(parseCommand(commandNode));
                    }
                    macros.Add(new Macros.Macro(name, commandList, 1));
                }
            }
            return macros;
        }
    }
}
