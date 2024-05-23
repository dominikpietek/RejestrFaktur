using RejestrFaktur.Commands;
using RejestrFaktur.Enums;
using RejestrFaktur.Models;
using RejestrFaktur.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RejestrFaktur.ViewModels
{
    public class EditInvoiceElementViewModel : ViewModelBase
    {
        #region OnPropertyChangeProperties
        private int _ItemNumber;
        public int ItemNumber
        {
            get { return _ItemNumber; }
            set
            {
                _ItemNumber = value;
                SetValuesForNumber(value);
                OnPropertyChanged(nameof(ItemNumber));
            }
        }
        private string _ItemName;
        public string ItemName
        {
            get { return _ItemName; }
            set
            {
                _ItemName = value;
                OnPropertyChanged(nameof(ItemName));
            }
        }
        private ObservableCollection<ComboBoxItem> _ItemUnits = new ObservableCollection<ComboBoxItem>();
        public ObservableCollection<ComboBoxItem> ItemUnits
        {
            get { return _ItemUnits; }
            set
            {
                _ItemUnits = value;
                OnPropertyChanged(nameof(ItemUnits));
            }
        }
        private int _ItemUnit;
        public int ItemUnit
        {
            get { return _ItemUnit; }
            set
            {
                _ItemUnit = value;
                OnPropertyChanged(nameof(ItemUnit));
            }
        }
        private string _ItemNetPrice;
        public string ItemNetPrice
        {
            get { return _ItemNetPrice; }
            set
            {
                _ItemNetPrice = value;
                OnPropertyChanged(nameof(ItemNetPrice));
            }
        }
        private int _ItemAmount;
        public int ItemAmount
        {
            get { return _ItemAmount; }
            set
            {
                _ItemAmount = value;
                OnPropertyChanged(nameof(ItemAmount));
            }
        }
        private int _ItemTaxRate;
        public int ItemTaxRate
        {
            get { return _ItemTaxRate; }
            set
            {
                _ItemTaxRate = value;
                OnPropertyChanged(nameof(ItemTaxRate));
            }
        }
        #endregion

        public ICommand SaveUpdatesButton { get; set; }
        public ICommand SliderPlusButton { get; set; }
        public ICommand SliderMinusButton { get; set; }
        private InvoiceModel _model;

        public EditInvoiceElementViewModel(InvoiceModel model)
        {
            _model = model;
            SaveUpdatesButton = new SaveUpdatesCommand(AddChangesToModel, RestartProperties);
            ItemUnits = GenerateListFromEnum.Generate<UnitsEnum>(ItemUnits);
            SliderMinusButton = new SliderCommand(ChangeTaxRateValue, -1);
            SliderPlusButton = new SliderCommand(ChangeTaxRateValue, 1);
        }

        public void ChangeTaxRateValue(int value)
        {
            ItemTaxRate += value;
        }

        public InvoiceModel AddChangesToModel()
        {
            if (_model.Items.Count() >= ItemNumber && ItemNumber > 0)
            {
                _model.Items[ItemNumber - 1] = new ItemModel()
                {
                    Id = _model.Items[ItemNumber - 1].Id,
                    Name = ItemName,
                    Unit = (UnitsEnum)ItemUnit,
                    Amount = ItemAmount,
                    NetPrice = double.Parse(ItemNetPrice),
                    TaxRate = ItemTaxRate
                };
            }
            else
            {
                MessageBox.Show(
                    messageBoxText: "Nie istniejący numer produktu!",
                    caption: "",
                    button: MessageBoxButton.OK,
                    icon: MessageBoxImage.Error);
            }
            return _model;
        }

        public void RestartProperties()
        {
            ItemNumber = 0;
            ItemName = "";
            ItemUnits = new ObservableCollection<ComboBoxItem>();
            ItemUnits = GenerateListFromEnum.Generate<UnitsEnum>(ItemUnits);
            ItemUnit = 0;
            ItemAmount = 0;
            ItemNetPrice = "0";
            ItemTaxRate = 230;
        }

        private void SetValuesForNumber(int number)
        {
            if (_model.Items.Count() >= number && number > 0)
            {
                ItemModel choosenItem = _model.Items[number - 1];
                ItemName = choosenItem.Name;
                ItemUnit = (int)choosenItem.Unit;
                ItemAmount = choosenItem.Amount;
                ItemNetPrice = choosenItem.NetPrice.ToString();
                ItemTaxRate = (int)(choosenItem.TaxRate);
            }
        }
    }
}
