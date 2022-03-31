using Macropse.Domain.Logic.Parser;
using Macropse.Infrastructure.Module.Driver;
using Macropse.Infrastructure.Module.Message.Args;
using Macropse.Infrastructure.Module.Message.ScriptBase;

using NUnit.Framework;

using System.Linq;
using System.Xml.Linq;

namespace Macropse.Domain.Unit.Test
{
    public class MacrosParsingTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void BuildMacroObject_UnknownTag_ShouldReturnError()
        {
            var input = XElement.Parse(
                "<test>" +
                "<command type=\"Run\" params=\"cmd.exe\"/>" +
                "</test>"
                );

            var output = new MacroBuilder().BuildObject(input);

            Assert.True(output.HasError);
            Assert.AreEqual(null, output.Item);
            Assert.IsInstanceOf(typeof(WrongTagTypeMessage), output.ErrorMessage);
        }

        [Test]
        public void BuildMacroObject_MacroWithoutCommands_ShouldReturnError()
        {
            var input = XElement.Parse(
                "<macro keys=\"A\">" +
                "</macro>"
                );

            var output = new MacroBuilder().BuildObject(input);

            Assert.True(output.HasError);
            Assert.AreEqual(null, output.Item);
            Assert.IsInstanceOf(typeof(EmptyNestedTagMessage), output.ErrorMessage);
        }

        [Test]
        public void BuildMacroObject_MacroWithoutBinds_ShouldReturnError()
        {
            var input = XElement.Parse(
                "<macro>" +
                "<command type=\"Run\" params=\"cmd.exe\"/>" +
                "</macro>"
                );

            var output = new MacroBuilder().BuildObject(input);

            Assert.True(output.HasError);
            Assert.AreEqual(null, output.Item);
            Assert.IsInstanceOf(typeof(EmptyArgumentMessage), output.ErrorMessage);
        }

        [Test]
        public void BuildMacroObject_MacroWithUnknownArgs_ShouldReturnError()
        {
            var input = XElement.Parse(
                "<macro type=\"Run\">" +
                "<command type=\"Run\" params=\"cmd.exe\"/>" +
                "</macro>"
                );

            var output = new MacroBuilder().BuildObject(input);

            Assert.True(output.HasError);
            Assert.AreEqual(null, output.Item);
            Assert.IsInstanceOf(typeof(UnknownArgumentMessage), output.ErrorMessage);
        }

        [Test]
        public void BuildMacroObject_MacroWithEmptyBinds_ShouldReturnError()
        {
            var input = XElement.Parse(
                "<macro keys=\"\">" +
                "<command type=\"Run\" params=\"cmd.exe\"/>" +
                "</macro>"
                );

            var output = new MacroBuilder().BuildObject(input);

            Assert.True(output.HasError);
            Assert.AreEqual(null, output.Item);
            Assert.IsInstanceOf(typeof(EmptyArgumentMessage), output.ErrorMessage);
        }

        [Test]
        public void BuildMacroObject_MacroWrongInnierTag_ShouldReturnError()
        {
            var input = XElement.Parse(
                "<macro keys=\"A\">" +
                "<test type=\"Run\" params=\"cmd.exe\"/>" +
                "</macro>"
                );

            var output = new MacroBuilder().BuildObject(input);

            Assert.True(output.HasError);
            Assert.AreEqual(null, output.Item);
            Assert.IsInstanceOf(typeof(WrongTagTypeMessage), output.ErrorMessage);
        }

        [Test]
        public void BuildMacroObject_MacroDefaultValues_ShouldReturnSuccess()
        {
            string expectedName = null;
            var expectedKey = VirtualKey.A;
            var expectedRepeats = 1;

            var input = XElement.Parse(
                "<macro keys=\"A\">" +
                "<command type=\"Run\" params=\"cmd.exe\"/>" +
                "</macro>"
                );

            var output = new MacroBuilder().BuildObject(input);

            Assert.False(output.HasError);
            Assert.AreEqual(expectedName, output.Item.Name);
            Assert.AreEqual(1, output.Item.Keys.Count());
            Assert.AreEqual(expectedKey, output.Item.Keys[0]);
            Assert.AreEqual(expectedRepeats, output.Item.Repeats);
            Assert.AreEqual(1, output.Item.Commands.Count());
        }

        [Test]
        public void BuildMacroObject_MacroMultiplyKeys_ShouldReturnSuccess()
        {
            VirtualKey[] expectedKeys = { VirtualKey.A, VirtualKey.S, VirtualKey.D };

            var input = XElement.Parse(
                $"<macro keys={"\"A, S,D\""}>" +
                    $"<command type={"\"Run\""} params={"\"cmd.exe\""}/>" +
                $"</macro>"
                );

            var output = new MacroBuilder().BuildObject(input);
            Assert.False(output.HasError);
            Assert.AreEqual(3, output.Item.Keys.Count());
            for (var i = 0; i < output.Item.Keys.Count(); ++i)
            {
                Assert.AreEqual(expectedKeys[i], output.Item.Keys[i]);
            }
        }

        [Test]
        public void BuildMacroObject_MacroLoop_ShouldReturnSuccessWithRepeatsValue100()
        {
            var input = XElement.Parse(
                $"<macro keys={"\"A\""} loop={"\"100\""}>" +
                    $"<command type={"\"Run\""} params={"\"cmd.exe\""}/>" +
                $"</macro>"
                );

            var output = new MacroBuilder().BuildObject(input);
            Assert.False(output.HasError);
            Assert.AreEqual(100, output.Item.Repeats);
        }

        [Test]
        public void BuildMacroObject_MacroName_ShouldReturnSuccessWithNameSample()
        {
            var expectedName = "Sample";

            var input = XElement.Parse(
                $"<macro keys={"\"A\""} name=\"{expectedName}\">" +
                    $"<command type={"\"Run\""} params={"\"cmd.exe\""}/>" +
                $"</macro>"
                );

            var output = new MacroBuilder().BuildObject(input);
            Assert.False(output.HasError);
            Assert.AreEqual(expectedName, output.Item.Name);
        }
    }
}
