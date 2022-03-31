using Macropse.Domain.Logic.Parser;
using Macropse.Infrastructure.Module.IO;
using Macropse.Infrastructure.Module.Message.ScriptBase;

using NUnit.Framework;

namespace Macropse.Domain.Unit.Test
{
    public class GlobalParsingTest
    {
        public ScriptParser parser;

        [SetUp]
        public void Setup()
        {
            parser = new ScriptParser();
        }

        [Test]
        public void Parse_EmptyScript_ShouldReturnError()
        {
            var output = parser.Parse(new Script("test", ""));
            Assert.True(output.HasError);
            Assert.AreEqual(null, output.Item);
            Assert.IsInstanceOf(typeof(IncorrectScriptMessage), output.ErrorMessage);
        }

        [Test]
        public void Parse_IncorrectScript_ShouldReturnError()
        {
            var output = parser.Parse(new Script("test", "<root></test>"));
            Assert.True(output.HasError);
            Assert.AreEqual(null, output.Item);
            Assert.IsInstanceOf(typeof(IncorrectScriptMessage), output.ErrorMessage);
        }
    }
}