using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RejestrFaktur.Commands;
using RejestrFaktur.Enums;
using RejestrFaktur.Models;
using RejestrFaktur.Repositories;
using RejestrFaktur.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace RejestrFaktur.ViewModels
{
    public class CreateInvoiceViewModel : ViewModelBase
    {
        #region OnPropertyChangeProperties
        private ObservableCollection<ComboBoxItem> _InvoiceTypes = new ObservableCollection<ComboBoxItem>();
        public ObservableCollection<ComboBoxItem> InvoiceTypes 
        { 
            get { return  _InvoiceTypes; }
            set
            {
                _InvoiceTypes = value;
                OnPropertyChanged(nameof(InvoiceTypes));
            }
        }
        private int _InvoiceType;
        public int InvoiceType
        {
            get { return _InvoiceType; }
            set
            {
                _InvoiceType = value;
                OnPropertyChanged(nameof(InvoiceType));
            }
        }
        private string _BankAccountNumber;
        public string BankAccountNumber
        {
            get { return _BankAccountNumber; }
            set
            {
                _BankAccountNumber = value;
                OnPropertyChanged(nameof(BankAccountNumber));
            }
        }
        private string _InvoiceNumber;
        public string InvoiceNumber
        {
            get { return _InvoiceNumber; }
            set
            {
                _InvoiceNumber = value;
                OnPropertyChanged(nameof(InvoiceNumber));
            }
        }
        private DateTime _InvoiceDate;
        public DateTime InvoiceDate
        {
            get { return _InvoiceDate; }
            set
            {
                _InvoiceDate = value;
                OnPropertyChanged(nameof(InvoiceDate));
            }
        }
        private DateTime _InvoiceSellDate;
        public DateTime InvoiceSellDate
        {
            get { return _InvoiceSellDate; }
            set
            {
                _InvoiceSellDate = value;
                OnPropertyChanged(nameof(InvoiceSellDate));
            }
        }
        private DateTime _PaymentDeadLine;
        public DateTime PaymentDeadLine
        {
            get { return _PaymentDeadLine; }
            set
            {
                _PaymentDeadLine = value;
                OnPropertyChanged(nameof(PaymentDeadLine));
            }
        }
        private PaymentMethodsEnum _PaymentMethod;
        public PaymentMethodsEnum PaymentMethod
        {
            get { return _PaymentMethod; }
            set
            {
                _PaymentMethod = value;
                OnPropertyChanged(nameof(PaymentMethod));
            }
        }
        private string _InvoiceDescription;
        public string InvoiceDescription
        {
            get { return _InvoiceDescription; }
            set
            {
                _InvoiceDescription = value;
                OnPropertyChanged(nameof(InvoiceDescription));
            }
        }
        private string _InvoiceContractorNIPNumber;
        public string InvoiceContractorNIPNumber
        {
            get { return _InvoiceContractorNIPNumber; }
            set
            {
                _InvoiceContractorNIPNumber = value;
                if (value.Length == 10)
                {
                    ScrapeContractorData(value);
                }
                OnPropertyChanged(nameof(InvoiceContractorNIPNumber));
            }
        }
        private string _InvoiceContractorName;
        public string InvoiceContractorName
        {
            get { return _InvoiceContractorName; }
            set
            {
                _InvoiceContractorName = value;
                OnPropertyChanged(nameof(InvoiceContractorName));
            }
        }
        private string _InvoiceContractorAddress;
        public string InvoiceContractorAddress
        {
            get { return _InvoiceContractorAddress; }
            set
            {
                _InvoiceContractorAddress = value;
                OnPropertyChanged(nameof(InvoiceContractorAddress));
            }
        }
        private string _InvoiceOwnerNIPNumber;
        public string InvoiceOwnerNIPNumber
        {
            get { return _InvoiceOwnerNIPNumber; }
            set
            {
                _InvoiceOwnerNIPNumber = value;
                if (value.Length == 10)
                {
                    ScrapeOwnerData(value);
                }
                OnPropertyChanged(nameof(InvoiceOwnerNIPNumber));
            }
        }
        private string _InvoiceOwnerName;
        public string InvoiceOwnerName
        {
            get { return _InvoiceOwnerName; }
            set
            {
                _InvoiceOwnerName = value;
                OnPropertyChanged(nameof(InvoiceOwnerName));
            }
        }
        private string _InvoiceOwnerAddress;
        public string InvoiceOwnerAddress
        {
            get { return _InvoiceOwnerAddress; }
            set
            {
                _InvoiceOwnerAddress = value;
                OnPropertyChanged(nameof(InvoiceOwnerAddress));
            }
        }
        #endregion
        #region Commands
        public ICommand SaveInvoiceButton { get; set; }
        #endregion

        public CreateInvoiceViewModel(Action CloseWindow)
        {
            InvoiceTypes = GenerateListFromEnum.Generate<InvoiceTypesEnum>(InvoiceTypes);
            InvoiceType = 0;
            InvoiceDate = DateTime.Now;
            InvoiceSellDate = DateTime.Now;
            PaymentDeadLine = DateTime.Now;
            SaveInvoiceButton = new SaveInvoiceCommand(CreateObjectFromData, CloseWindow);
        }

        private InvoiceModel CreateObjectFromData()
        {
            var invoice = new InvoiceModel()
            {
                InvoiceType = (InvoiceTypesEnum)InvoiceType,
                InvoiceDate = this.InvoiceDate,
                InvoiceNumber = this.InvoiceNumber,
                InvoiceSellDate = this.InvoiceSellDate,
                PaymentDeadLine = this.PaymentDeadLine,
                PaymentMethod = this.PaymentMethod,
                BankAccountNumber = this.BankAccountNumber,
                Description = this.InvoiceDescription,
                Contractor = new CompanyDataContractorModel()
                {
                    NIPNumber = InvoiceContractorNIPNumber,
                    Name = InvoiceContractorName,
                    Address = InvoiceContractorAddress
                },
                Owner = new CompanyDataOwnerModel()
                {
                    NIPNumber = InvoiceOwnerNIPNumber,
                    Name = InvoiceOwnerName,
                    Address = InvoiceOwnerAddress
                }
            };
            return invoice;
        }

        private void ScrapeContractorData(string nip)
        {
            var dataScraper = new CompanyDataScraper(nip);
            CompanyDataOwnerModel data = dataScraper.Search();
            InvoiceContractorName = data.Name;
            InvoiceContractorAddress = data.Address;
        }

        private void ScrapeOwnerData(string nip)
        {
            var dataScraper = new CompanyDataScraper(nip);
            CompanyDataOwnerModel data = dataScraper.Search();
            InvoiceOwnerName = data.Name;
            InvoiceOwnerAddress = data.Address;
        }
    }
}
