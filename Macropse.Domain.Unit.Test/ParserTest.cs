using Macropse.Domain.Logic.Parser;
using Macropse.Infrastructure.Module.IO;
using Macropse.Infrastructure.Module.Message;
using Macropse.Infrastructure.Module.Message.Command;
using Macropse.Infrastructure.Module.Message.IO;
using Macropse.Infrastructure.Module.Message.ScriptBase;

using NUnit.Framework;

namespace Macropse.Domain.Unit.Test
{
    public class Tests
    {
        public ScriptParser parser;

        [SetUp]
        public void Setup()
        {
            parser = new ScriptParser();
        }

        [Test]
        public void ConvertEmptyScript()
        {
            var output = parser.Parse(new Script("test", ""));
            Assert.True(output.HasError);
            Assert.AreEqual(null, output.Item);
            Assert.IsInstanceOf(typeof(IncorrectScriptMessage), output.ErrorMessage);
        }

        [Test]
        public void ConvertIncorrectScript()
        {
            var output = parser.Parse(new Script("test", "<root></test>"));
            Assert.True(output.HasError);
            Assert.AreEqual(null, output.Item);
            Assert.IsInstanceOf(typeof(IncorrectScriptMessage), output.ErrorMessage);
        }

        [Test]
        public void ParseRepeatsShouldPass()
        {
            var output = parser.Parse(new Script("test", "<root><macro loop = \"123\"></macro></root>"));
            Assert.False(output.HasError);
            Assert.AreEqual(123, output.Item[0].Repeats);
        }

        [Test]
        public void ParseEmptyRepeatsShouldPass()
        {
            var output = parser.Parse(new Script("test", "<root><macro></macro></root>"));
            Assert.False(output.HasError);
            Assert.AreEqual(1, output.Item[0].Repeats);
        }

        [Test]
        public void ParseRepeatsShouldNotPass()
        {
            var output = parser.Parse(new Script("test", "<root><macro loop = \"1qwe2\"></macro></root>"));
            Assert.True(output.HasError);
            Assert.IsInstanceOf(typeof(IncorrectRepeatValueMessage), output.ErrorMessage);
            Assert.AreEqual(null, output.Item);
        }

        [Test]
        public void ParseMacroName()
        {
            var output = parser.Parse(new Script("test", "<root><macro></macro></root>"));
            Assert.AreEqual(null, output.Item[0].Name);
            output = parser.Parse(new Script("test", "<root><macro name = \"\"></macro></root>"));
            Assert.AreEqual(string.Empty, output.Item[0].Name);
            output = parser.Parse(new Script("test", "<root><macro name = \"test\"></macro></root>"));
            Assert.AreEqual("test", output.Item[0].Name);
        }

        [Test]
        public void ParseEmptyCommandType()
        {
            var output = parser.Parse(new Script("test", "<root><macro><command/></macro></root>"));
            Assert.True(output.HasError);
            Assert.IsInstanceOf(typeof(EmptyCommandTypeMessage), output.ErrorMessage);
        }

        [Test]
        public void ParseIncorrectCommandType()
        {
            var output = parser.Parse(new Script("test", "<root><macro><command type=\"\"/></macro></root>"));
            Assert.True(output.HasError);
            Assert.IsInstanceOf(typeof(UnknownCommandTypeMessage), output.ErrorMessage);
        }

        [Test]
        public void ParseCorrectCommandType()
        {
            var output = parser.Parse(new Script("test", "<root><macro><command type=\"Run\" params = \"test.exe\"/></macro></root>"));
            Assert.False(output.HasError);
        }

        [Test]
        public void ParseCommandRunParamsShouldPass()
        {
            var output = parser.Parse(new Script("test", "<root><macro><command type=\"Run\" params = \"test\"/></macro></root>"));
            Assert.False(output.HasError); 
            output = parser.Parse(new Script("test", "<root><macro><command type=\"Run\" params = \"test, true\"/></macro></root>"));
            Assert.False(output.HasError);
            output = parser.Parse(new Script("test", "<root><macro><command type=\"Run\" params = \"test, false\"/></macro></root>"));
            Assert.False(output.HasError);
        }

        [Test]
        public void ParseCommandRunParamsShouldNotPass()
        {
            var output = parser.Parse(new Script("test", "<root><macro><command type=\"Run\" params = \"\"/></macro></root>"));
            Assert.True(output.HasError);
            Assert.IsInstanceOf(typeof(ParamsOutOfBoundsMessage), output.ErrorMessage);
            output = parser.Parse(new Script("test", "<root><macro><command type=\"Run\" params = \"test, true, 123\"/></macro></root>"));
            Assert.True(output.HasError);
            Assert.IsInstanceOf(typeof(ParamsOutOfBoundsMessage), output.ErrorMessage);
            output = parser.Parse(new Script("test", "<root><macro><command type=\"Run\" params = \"test, asd\"/></macro></root>"));
            Assert.True(output.HasError);
            Assert.IsInstanceOf(typeof(IncorrectParamMessage), output.ErrorMessage);
        }
    }
}