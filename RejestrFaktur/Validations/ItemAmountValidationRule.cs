using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RejestrFaktur.Validations
{
    public class ItemAmountValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int valueInt = int.Parse(value.ToString());
            if (valueInt > 0)
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Ilość może zawierać tylko liczby dodatnie!");
            }
        }
    }
}
