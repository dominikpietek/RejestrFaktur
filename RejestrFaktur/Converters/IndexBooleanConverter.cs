﻿using RejestrFaktur.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RejestrFaktur.Converters
{
    public class IndexBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && parameter != null)
            {
                return (int)value == System.Convert.ToInt32(parameter);
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                int index = (int)System.Convert.ToInt32(parameter);
                return index == 1 ? PaymentMethodsEnum.Gotówka : PaymentMethodsEnum.PrzelewBankowy;
            }
            return PaymentMethodsEnum.PrzelewBankowy;
        }
    }
}
