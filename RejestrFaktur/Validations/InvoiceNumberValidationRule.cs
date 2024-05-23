using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RejestrFaktur.Validations
{
    public class InvoiceNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string valueString = value.ToString();
            Trace.Write(valueString);
            var regex = new Regex(@"^[1234567890][1234567890\/]*$");
            if (regex.IsMatch(valueString))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Numer faktury może zawierać tylko liczby i znaki '/'!");
            }
        }
    }
}
