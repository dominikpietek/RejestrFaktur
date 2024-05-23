using System.Globalization;
using RejestrFaktur.Validations;

namespace RejestrFaktur.UnitTests
{
    public class InvoiceElementNumberValidationTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(50)]
        [InlineData(150)]
        [InlineData(800023)]
        public void InvoiceElementNumberValidationRule_IsValid_True(int number)
        {
            var validationResultClass = new InvoiceElementNumberValidationRule();
            var validationResult = validationResultClass.Validate(number, CultureInfo.DefaultThreadCurrentCulture);
            Assert.Equal(true, validationResult.IsValid);
        }
        [Fact]
        public void InvoiceElementNumberValidationRule_IsZero_False()
        {
            var validationResultClass = new InvoiceElementNumberValidationRule();
            var validationResult = validationResultClass.Validate(0, CultureInfo.DefaultThreadCurrentCulture);
            Assert.Equal(false, validationResult.IsValid);
            Assert.Equal("Liczba nie może być zerem!", validationResult.ErrorContent);
        }
    }
}