using RejestrFaktur.Validations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejestrFaktur.UnitTests
{
    public class NetPriceValidationTests
    {
        [Theory]
        [InlineData("1.2")]
        [InlineData("1")]
        [InlineData("200")]
        [InlineData("3873052")]
        [InlineData("200.20")]
        public void NetPriceValidationRule_IsValid_True(string number)
        {
            var validationResultClass = new NetPriceValidationRule();
            var validationResult = validationResultClass.Validate(number, CultureInfo.DefaultThreadCurrentCulture);
            Assert.Equal(true, validationResult.IsValid);
        }
        [Theory]
        [InlineData("1,2")]
        [InlineData("1,34")]
        public void NetPriceValidationRule_WrongCommaSign_False(string number)
        {
            var validationResultClass = new NetPriceValidationRule();
            var validationResult = validationResultClass.Validate(number, CultureInfo.DefaultThreadCurrentCulture);
            Assert.Equal(false, validationResult.IsValid);
            Assert.Equal("Używaj '.' zamiast ','!", validationResult.ErrorContent);
        }
        [Theory]
        [InlineData("abc")]
        [InlineData("1.23432")]
        public void NetPriceValidationRule_WrongPrice_False(string number)
        {
            var validationResultClass = new NetPriceValidationRule();
            var validationResult = validationResultClass.Validate(number, CultureInfo.DefaultThreadCurrentCulture);
            Assert.Equal(false, validationResult.IsValid);
            Assert.Equal("Niepoprawna kwota!", validationResult.ErrorContent);
        }
    }
}
