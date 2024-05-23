using Microsoft.Extensions.Options;
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
    public class InvoiceElementNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int valueInt;
            if (int.TryParse(value.ToString(), out valueInt))
            {
                if (valueInt != 0)
                {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false, "Liczba nie może być zerem!");
                }
            }
            return new ValidationResult(false, "Liczba nie może być zerem!");
        }
    }
}
