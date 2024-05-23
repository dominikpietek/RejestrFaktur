using RejestrFaktur.Validations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejestrFaktur.UnitTests
{
    public class InvoiceNumberValidationTests
    {
        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("15")]
        [InlineData("1/02/2022")]
        [InlineData("1/01/02/2002")]
        public void InvoiceNumberValidationRule_IsValid_True(string invoiceNumber)
        {
            var validationResultClass = new InvoiceNumberValidationRule();
            var validationResult = validationResultClass.Validate(invoiceNumber, CultureInfo.DefaultThreadCurrentCulture);
            Assert.Equal(true, validationResult.IsValid);
        }
        [Theory]
        [InlineData("1-")]
        [InlineData("a2")]
        [InlineData("a5")]
        [InlineData("faktura vat")]
        public void InvoiceNumberValidationRule_ContainsNumber_False(string invoiceNumber)
        {
            var validationResultClass = new InvoiceNumberValidationRule();
            var validationResult = validationResultClass.Validate(invoiceNumber, CultureInfo.DefaultThreadCurrentCulture);
            Assert.Equal(false, validationResult.IsValid);
            Assert.Equal("Numer faktury może zawierać tylko liczby i znaki '/'!", validationResult.ErrorContent);
        }
    }
}
