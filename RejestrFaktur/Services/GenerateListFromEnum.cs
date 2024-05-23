using RejestrFaktur.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RejestrFaktur.Services
{
    public static class GenerateListFromEnum
    {
        public static ObservableCollection<ComboBoxItem> Generate<T>(ObservableCollection<ComboBoxItem> list) where T : Enum
        {
            var enumNames = Enum.GetNames(typeof(T));
            foreach (string enumName in enumNames)
            {
                list.Add(new ComboBoxItem() { Content = enumName });
            }
            return list;
        }
    }
}
