using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RejestrFaktur.Validations
{
    public class NetPriceValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string valueString = value.ToString();
            var regex = new Regex(@"^[1234567890]*\.?[1234567890]{1,2}$");
            if (regex.IsMatch(valueString))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                if (valueString.Contains(','))
                {
                    return new ValidationResult(false, "Używaj '.' zamiast ','!");
                }
                return new ValidationResult(false, "Niepoprawna kwota!");
            }
        }
    }
}
