using RejestrFaktur.Validations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejestrFaktur.UnitTests
{
    public class InvoiceNIPValidationTests
    {
        [Theory]
        [InlineData("0000000000")]
        [InlineData("1020034302")]
        [InlineData("2345678910")]
        public void InvoiceNIPValidationRule_IsValid_True(string nip)
        {
            var validationResultClass = new InvoiceNIPValidationRule();
            var validationResult = validationResultClass.Validate(nip, CultureInfo.DefaultThreadCurrentCulture);
            Assert.Equal(true, validationResult.IsValid);
        }
        [Theory]
        [InlineData("00000000000000")]
        [InlineData("10200343024")]
        [InlineData("2345678910437856795678")]
        public void InvoiceNIPValidationRule_IsTooLong_False(string nip)
        {
            var validationResultClass = new InvoiceNIPValidationRule();
            var validationResult = validationResultClass.Validate(nip, CultureInfo.DefaultThreadCurrentCulture);
            Assert.Equal(false, validationResult.IsValid);
            Assert.Equal("NIP jest za długi!", validationResult.ErrorContent);
        }
        [Theory]
        [InlineData("")]
        [InlineData("10200343")]
        [InlineData("102003430")]
        [InlineData("1")]
        public void InvoiceNIPValidationRule_IsTooShort_False(string nip)
        {
            var validationResultClass = new InvoiceNIPValidationRule();
            var validationResult = validationResultClass.Validate(nip, CultureInfo.DefaultThreadCurrentCulture);
            Assert.Equal(false, validationResult.IsValid);
            Assert.Equal("NIP jest za krórki!", validationResult.ErrorContent);
        }
        [Theory]
        [InlineData("a111111111")]
        [InlineData("dsaf121111")]
        [InlineData("1sadf21111")]
        [InlineData("/:21111111")]
        [InlineData("2/31111111")]
        [InlineData("2-36789691")]
        public void InvoiceNIPValidationRule_CanContainOnlyNumbers_False(string nip)
        {
            var validationResultClass = new InvoiceNIPValidationRule();
            var validationResult = validationResultClass.Validate(nip, CultureInfo.DefaultThreadCurrentCulture);
            Assert.Equal(false, validationResult.IsValid);
            Assert.Equal("NIP może zawierać tylko liczby!", validationResult.ErrorContent);
        }
    }
}
