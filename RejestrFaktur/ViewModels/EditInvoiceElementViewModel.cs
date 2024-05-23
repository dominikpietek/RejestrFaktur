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
        private UnitsEnum _ItemUnit;
        public UnitsEnum ItemUnit
        {
            get { return _ItemUnit; }
            set
            {
                _ItemUnit = value;
                OnPropertyChanged(nameof(ItemUnit));
            }
        }
        private double _ItemNetPrice;
        public double ItemNetPrice
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
        private InvoiceModel _model;

        public EditInvoiceElementViewModel(InvoiceModel model)
        {
            _model = model;
            SaveUpdatesButton = new SaveUpdatesCommand(AddChangesToModel, RestartProperties);
            ItemUnits = GenerateListFromEnum.Generate<UnitsEnum>(ItemUnits);
        }

        public InvoiceModel AddChangesToModel()
        {
            _model.Items[ItemNumber - 1] = new ItemModel()
            {
                Id = _model.Items[ItemNumber - 1].Id,
                Name = ItemName,
                Unit = ItemUnit,
                Amount = ItemAmount,
                NetPrice = ItemNetPrice,
                TaxRate = (double)(ItemTaxRate / 10)
            };
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
            ItemNetPrice = 0;
            ItemTaxRate = 230;
        }

        private void SetValuesForNumber(int number)
        {
            if (_model.Items.Count() >= number && number > 0)
            {
                ItemModel choosenItem = _model.Items[number - 1];
                ItemName = choosenItem.Name;
                ItemUnit = choosenItem.Unit;
                ItemAmount = choosenItem.Amount;
                ItemNetPrice = choosenItem.NetPrice;
                ItemTaxRate = (int)(choosenItem.TaxRate * 10);
            }
        }
    }
}
