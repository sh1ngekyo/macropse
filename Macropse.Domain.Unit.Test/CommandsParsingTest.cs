using Macropse.Domain.Logic.Macro.Command;
using Macropse.Domain.Logic.Parser;
using Macropse.Infrastructure.Module.Message.Args;
using Macropse.Infrastructure.Module.Message.Command;
using Macropse.Infrastructure.Module.Message.Params;

using NUnit.Framework;

using System.Xml.Linq;

namespace Macropse.Domain.Unit.Test
{
    public class CommandsParsingTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void BuildCommandObject_EmptyTypeArg_ShouldReturnError()
        {
            var input = XElement.Parse(
                "<command type=\"\" params=\"cmd.exe\"/>"
                );

            var output = new CommandBuilder().BuildObject(input);

            Assert.True(output.HasError);
            Assert.AreEqual(null, output.Item);
            Assert.IsInstanceOf(typeof(UnknownCommandTypeMessage), output.ErrorMessage);
        }

        [Test]
        public void BuildCommandObject_WithoutType_ShouldReturnError()
        {
            var input = XElement.Parse(
                "<command params=\"cmd.exe\"/>"
                );

            var output = new CommandBuilder().BuildObject(input);

            Assert.True(output.HasError);
            Assert.AreEqual(null, output.Item);
            Assert.IsInstanceOf(typeof(EmptyCommandTypeMessage), output.ErrorMessage);
        }

        [Test]
        public void BuildCommandObject_InnierCommand_ShouldReturnError()
        {
            var input = XElement.Parse(
                "<command type=\"Run\" params=\"cmd.exe\">" +
                    "<command type=\"Run\" params=\"cmd.exe\"/>" +
                "</command>"
                );

            var output = new CommandBuilder().BuildObject(input);

            Assert.True(output.HasError);
            Assert.AreEqual(null, output.Item);
            Assert.IsInstanceOf(typeof(NestedCommandNotAllowedMessage), output.ErrorMessage);
        }

        [Test]
        public void BuildCommandObject_UnknownArgs_ShouldReturnError()
        {
            var input = XElement.Parse(
                "<command type=\"Run\" test=\"cmd.exe\"/>"
                );

            var output = new CommandBuilder().BuildObject(input);

            Assert.True(output.HasError);
            Assert.AreEqual(null, output.Item);
            Assert.IsInstanceOf(typeof(UnknownArgumentMessage), output.ErrorMessage);
        }

        [Test]
        public void BuildCommandObject_CommandRunOneParam_ShouldReturnSuccess()
        {
            var input = XElement.Parse(
                "<command type = \"Run\" params = \"cmd.exe\"/>"
                );

            var output = new CommandBuilder().BuildObject(input);
            Assert.False(output.HasError);
            Assert.IsInstanceOf(typeof(CommandRun), output.Item);
        }

        [Test]
        public void BuildCommandObject_CommandRunTwoParams_ShouldReturnSuccess()
        {
            var input = XElement.Parse(
                "<command type = \"Run\" params = \"cmd.exe,true\"/>"
                );

            var output = new CommandBuilder().BuildObject(input);
            Assert.False(output.HasError);
            Assert.IsInstanceOf(typeof(CommandRun), output.Item);

            input = XElement.Parse(
                "<command type = \"Run\" params = \"cmd.exe, false\"/>"
                );

            output = new CommandBuilder().BuildObject(input);
            Assert.False(output.HasError);
            Assert.IsInstanceOf(typeof(CommandRun), output.Item);
        }

        [Test]
        public void BuildCommandObject_CommandRunMoreParamsThanAllowed_ShouldReturnError()
        {
            var input = XElement.Parse(
                "<command type = \"Run\" params = \"cmd.exe,true,true,false\"/>"
                );

            var output = new CommandBuilder().BuildObject(input);
            Assert.True(output.HasError);
            Assert.IsInstanceOf(typeof(ParamsOutOfBoundsMessage), output.ErrorMessage);
        }

        [Test]
        public void BuildCommandObject_CommandDelayWrongParamType_ShouldReturnError()
        {
            var input = XElement.Parse(
                "<command type = \"delay\" params = \"test\"/>"
                );

            var output = new CommandBuilder().BuildObject(input);
            Assert.True(output.HasError);
            Assert.IsInstanceOf(typeof(IncorrectParamMessage), output.ErrorMessage);
        }

        [Test]
        public void BuildCommandObject_CommandDelayCorrectParamType_ShouldReturnSuccess()
        {
            var input = XElement.Parse(
                "<command type = \"delay\" params = \"111\"/>"
                );

            var output = new CommandBuilder().BuildObject(input);
            Assert.False(output.HasError);
            Assert.IsInstanceOf(typeof(CommandDelay), output.Item);
        }

        [Test]
        public void BuildCommandObject_CommandMoveMouseToLessParamsThanAllowed_ShouldReturnError()
        {
            var input = XElement.Parse(
                "<command type = \"movemouseto\" params = \"111\"/>"
                );

            var output = new CommandBuilder().BuildObject(input);
            Assert.True(output.HasError);
            Assert.IsInstanceOf(typeof(ParamsOutOfBoundsMessage), output.ErrorMessage);
        }


        [Test]
        public void BuildCommandObject_CommandMoveMouseToMoreParamsThanAllowed_ShouldReturnError()
        {
            var input = XElement.Parse(
                "<command type = \"movemouseto\" params = \"111,222, 333\"/>"
                );

            var output = new CommandBuilder().BuildObject(input);
            Assert.True(output.HasError);
            Assert.IsInstanceOf(typeof(ParamsOutOfBoundsMessage), output.ErrorMessage);
        }

        [Test]
        public void BuildCommandObject_CommandMoveMouseToIncorrectParams_ShouldReturnError()
        {
            var input = XElement.Parse(
                "<command type = \"movemouseto\" params = \"11.5,   22.2\"/>"
                );

            var output = new CommandBuilder().BuildObject(input);
            Assert.True(output.HasError);
            Assert.IsInstanceOf(typeof(IncorrectParamMessage), output.ErrorMessage);
        }

        [Test]
        public void BuildCommandObject_CommandMoveMouseToCorrectValues_ShouldReturnSuccess()
        {
            var input = XElement.Parse(
                "<command type = \"movemouseto\" params = \"115,   222\"/>"
                );

            var output = new CommandBuilder().BuildObject(input);
            Assert.False(output.HasError);
            Assert.IsInstanceOf(typeof(CommandMoveMouseTo), output.Item);
        }

        [Test]
        public void BuildCommandObject_CommandShowMsgBox_ShouldReturnSuccess()
        {
            var input = XElement.Parse(
                "<command type = \"showmsgbox\" params = \"test test test\"/>"
                );

            var output = new CommandBuilder().BuildObject(input);
            Assert.False(output.HasError);
            Assert.IsInstanceOf(typeof(CommandShowMsgBox), output.Item);
        }

        [Test]
        public void BuildCommandObject_CommandSendKeyMultiplyParams_ShouldReturnError()
        {
            var input = XElement.Parse(
                "<command type = \"sendkey\" params = \"A,S, D\"/>"
                );

            var output = new CommandBuilder().BuildObject(input);
            Assert.True(output.HasError);
            Assert.IsInstanceOf(typeof(ParamsOutOfBoundsMessage), output.ErrorMessage);
        }

        [Test]
        public void BuildCommandObject_CommandSendKeyCorrectValue_ShouldReturnSuccess()
        {
            var input = XElement.Parse(
                "<command type = \"sendkey\" params = \"f9\"/>"
                );

            var output = new CommandBuilder().BuildObject(input);
            Assert.False(output.HasError);
            Assert.IsInstanceOf(typeof(CommandSendKey), output.Item);
        }
    }
}
