using RejestrFaktur.Commands;
using RejestrFaktur.Databases;
using RejestrFaktur.Enums;
using RejestrFaktur.Interfaces;
using RejestrFaktur.Models;
using RejestrFaktur.Repositories;
using RejestrFaktur.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Input;

namespace RejestrFaktur.ViewModels
{
    public class AddItemToInvoiceViewModel : ViewModelBase
    {
        #region OnPropertyChangeProperties
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
        private int _ItemNetPrice;
        public int ItemNetPrice
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
        #region Commands
        public ICommand AddAnotherItemButton { get; set; }
        public ICommand AddAndSaveButton { get; set; }
        #endregion

        private InvoiceModel _model;

        public AddItemToInvoiceViewModel(InvoiceModel model, Action CloseWindow)
        {
            _model = model;
            GenerateUnits();
            AddAnotherItemButton = new AddAnotherItemCommand(AddItem);
            AddAndSaveButton = new AddAndSaveCommand(Save, CloseWindow);
        }

        private void AddItem()
        {
            _model.Items.Add(new ItemModel()
            {
                Name = ItemName,
                Unit = ItemUnit,
                Amount = ItemAmount,
                NetPrice = ItemNetPrice,
                TaxRate = (double)(ItemTaxRate / 10)
            });   
        }

        private async Task Save()
        {
            AddItem();
            using (var db = new InvoicesDbContext())
            {
                IInvoicesRepository repository = new InvoicesRepository(db);
                await repository.AddInvoice(_model);
            }
            var invoice = new GenerateInvoice(_model);
            invoice.Generate();
            invoice.Save($"{_model.InvoiceNumber}.pdf");
        }

        private void GenerateUnits()
        {
            var enumNames = Enum.GetNames(typeof(UnitsEnum));
            foreach (string enumName in enumNames)
            {
                ItemUnits.Add(new ComboBoxItem() { Content = enumName });
            }
        }
    }
}
