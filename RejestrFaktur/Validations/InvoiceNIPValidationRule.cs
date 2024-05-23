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
    public class InvoiceNIPValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string valueString = value.ToString();
            var regex = new Regex(@"^[1234567890]*$");
            if (regex.IsMatch(valueString) && valueString.Length == 10)
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                if (valueString.Length > 10)
                {
                    return new ValidationResult(false, "NIP jest za długi!");
                }
                if (valueString.Length < 10)
                {
                    return new ValidationResult(false, "NIP jest za krórki!");
                }
                if (!regex.IsMatch(valueString))
                {
                    return new ValidationResult(false, "NIP może zawierać tylko liczby!");
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
