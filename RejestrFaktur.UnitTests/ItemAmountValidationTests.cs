using RejestrFaktur.Validations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejestrFaktur.UnitTests
{
    public class ItemAmountValidationTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(1000)]
        [InlineData(2)]
        [InlineData(3873052)]
        public void ItemAmountValidationRule_IsValid_True(int amount)
        {
            var validationResultClass = new ItemAmountValidationRule();
            var validationResult = validationResultClass.Validate(amount, CultureInfo.DefaultThreadCurrentCulture);
            Assert.Equal(true, validationResult.IsValid);
        }
        [Theory]
        [InlineData(-30)]
        [InlineData(-2)]
        [InlineData(-1)]
        [InlineData(0)]
        public void ItemAmountValidationRule_NegativeNumbers_False(int amount)
        {
            var validationResultClass = new ItemAmountValidationRule();
            var validationResult = validationResultClass.Validate(amount, CultureInfo.DefaultThreadCurrentCulture);
            Assert.Equal(false, validationResult.IsValid);
            Assert.Equal("Ilość może zawierać tylko liczby dodatnie!", validationResult.ErrorContent);
        }
    }
}
