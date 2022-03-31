using Macropse.Domain.Logic.Parser;
using Macropse.Infrastructure.Module.Driver;
using Macropse.Infrastructure.Module.IO;
using Macropse.Infrastructure.Module.Message.Params;

using NUnit.Framework;

namespace Macropse.Domain.Unit.Test
{
    public class ParamsParsingTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ParseParam_SignedNumber_ShouldReturnError()
        {
            var output = ParamParser.ParseParam<uint>("-123");
            Assert.True(output.HasError);
            Assert.AreEqual(default(uint), output.Item);
            Assert.IsInstanceOf(typeof(IncorrectParamMessage), output.ErrorMessage);
        }

        [Test]
        public void ParseParam_CorrectNumber_ShouldReturnSuccess()
        {
            var expected = 123;
            var output = ParamParser.ParseParam<uint>("123");
            Assert.False(output.HasError);
            Assert.AreEqual(expected, output.Item);
        }

        [Test]
        public void ParseParam_IncorrectNumber_ShouldReturnError()
        {
            var output = ParamParser.ParseParam<uint>("12gh3");
            Assert.True(output.HasError);
            Assert.AreEqual(default(uint), output.Item);
            Assert.IsInstanceOf(typeof(IncorrectParamMessage), output.ErrorMessage);
        }

        [Test]
        public void ParseParam_CorrectBool_ShouldReturnSuccess()
        {
            var expectedFalse = false;
            var expectedTrue = true;

            var output = ParamParser.ParseParam<bool>("false");
            Assert.False(output.HasError);
            Assert.AreEqual(expectedFalse, output.Item);

            output = ParamParser.ParseParam<bool>("true");
            Assert.False(output.HasError);
            Assert.AreEqual(expectedTrue, output.Item);
        }

        [Test]
        public void ParseParam_NumberToBool_ShouldReturnError()
        {
            var output = ParamParser.ParseParam<bool>("0");
            Assert.True(output.HasError);
            Assert.AreEqual(default(bool), output.Item);
            Assert.IsInstanceOf(typeof(IncorrectParamMessage), output.ErrorMessage);
        }

        [Test]
        public void ParseParam_IncorrectKey_ShouldReturnError()
        {
            var output = ParamParser.ParseParam<VirtualKey>("F88");
            Assert.True(output.HasError);
            Assert.AreEqual(default(VirtualKey), output.Item);
            Assert.IsInstanceOf(typeof(IncorrectParamMessage), output.ErrorMessage);
        }

        [Test]
        public void ParseParam_CorrectKey_ShouldReturnSuccess()
        {
            var expectedKey = VirtualKey.F8;
            var output = ParamParser.ParseParam<VirtualKey>("F8");
            Assert.False(output.HasError);
            Assert.AreEqual(expectedKey, output.Item);
        }

        [Test]
        public void ParseParam_MultiplyCorrectKey_ShouldReturnError()
        {
            var output = ParamParser.ParseParam<VirtualKey>("A,S,D");
            Assert.True(output.HasError);
            Assert.AreEqual(default(VirtualKey), output.Item);
            Assert.IsInstanceOf(typeof(IncorrectParamMessage), output.ErrorMessage);
        }
    }
}
