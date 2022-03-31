using Macropse.Domain.Logic.Parser;
using Macropse.Infrastructure.Module.Driver;
using Macropse.Infrastructure.Module.IO;
using Macropse.Infrastructure.Module.Message.Args;
using Macropse.Infrastructure.Module.Message.Params;
using Macropse.Infrastructure.Module.Message.ScriptBase;

using NUnit.Framework;

namespace Macropse.Domain.Unit.Test
{
    public class RootParsingTest
    {
        public ScriptParser parser;

        [SetUp]
        public void Setup()
        {
            parser = new ScriptParser();
        }

        [Test]
        public void Parse_IncorrectRootTagName_ShouldReturnError()
        {
            var output = parser.Parse(new Script("test", "<test></test>"));
            Assert.True(output.HasError);
            Assert.AreEqual(null, output.Item);
            Assert.IsInstanceOf(typeof(ScriptRootMissingMessage), output.ErrorMessage);
        }

        [Test]
        public void Parse_RootWithoutMacro_ShouldReturnError()
        {
            var output = parser.Parse(new Script("test", "<root></root>"));
            Assert.True(output.HasError);
            Assert.AreEqual(null, output.Item);
            Assert.IsInstanceOf(typeof(EmptyNestedTagMessage), output.ErrorMessage);
        }

        [Test]
        public void Parse_UnknownArgumentInRootTag_ShouldReturnError()
        {
            var output = parser.Parse(new Script("test", "<root test=\"\"><macro></macro></root>"));
            Assert.True(output.HasError);
            Assert.AreEqual(null, output.Item);
            Assert.IsInstanceOf(typeof(UnknownArgumentMessage), output.ErrorMessage);
        }

        [Test]
        public void Parse_EmptyCorrectRoot_ShouldReturnSuccess()
        {
            var expectedDelay = 100;
            var expectedPauseKey = VirtualKey.Pause;

            var output = parser.Parse(new Script("test", 
                "<root>" +
                "<macro keys=\"A\">" +
                "<command type=\"run\" params=\"test\"/>" +
                "</macro>" +
                "</root>"));

            Assert.False(output.HasError);
            Assert.AreEqual(expectedDelay, output.Item.Root.GlobalDelay);
            Assert.AreEqual(expectedPauseKey, output.Item.Root.PauseKey);
        }

        [Test]
        public void Parse_CorrectRootParams_ShouldReturnSuccess()
        {
            var expectedDelay = 123;
            var expectedPauseKey = VirtualKey.A;

            var output = parser.Parse(new Script("test",
                "<root pause=\"A\" delay=\"123\">" +
                "<macro keys=\"A\">" +
                "<command type=\"run\" params=\"test\"/>" +
                "</macro>" +
                "</root>"));

            Assert.False(output.HasError);
            Assert.AreEqual(expectedDelay, output.Item.Root.GlobalDelay);
            Assert.AreEqual(expectedPauseKey, output.Item.Root.PauseKey);
        }

        [Test]
        public void Parse_IncorrectRootDelay_ShouldReturnError()
        {
            var output = parser.Parse(new Script("test",
                "<root delay=\"test\">" +
                "<macro keys=\"A\">" +
                "<command type=\"run\" params=\"test\"/>" +
                "</macro>" +
                "</root>"));

            Assert.True(output.HasError);
            Assert.AreEqual(null, output.Item);
            Assert.IsInstanceOf(typeof(IncorrectParamMessage), output.ErrorMessage);
        }

        [Test]
        public void Parse_IncorrectRootPauseKey_ShouldReturnError()
        {
            var output = parser.Parse(new Script("test",
                "<root pause=\"test\">" +
                "<macro keys=\"A\">" +
                "<command type=\"run\" params=\"test\"/>" +
                "</macro>" +
                "</root>"));

            Assert.True(output.HasError);
            Assert.AreEqual(null, output.Item);
            Assert.IsInstanceOf(typeof(IncorrectParamMessage), output.ErrorMessage);
        }

        [Test]
        public void Parse_MultiplyRootPauseKey_ShouldReturnError()
        {
            var output = parser.Parse(new Script("test",
                "<root pause=\"t,e,s,t\">" +
                "<macro keys=\"A\">" +
                "<command type=\"run\" params=\"test\"/>" +
                "</macro>" +
                "</root>"));
            Assert.True(output.HasError);
            Assert.AreEqual(null, output.Item);
            Assert.IsInstanceOf(typeof(IncorrectParamMessage), output.ErrorMessage);
        }
    }
}
