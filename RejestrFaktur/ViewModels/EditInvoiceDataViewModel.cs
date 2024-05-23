using RejestrFaktur.Commands;
using RejestrFaktur.Enums;
using RejestrFaktur.Models;
using RejestrFaktur.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RejestrFaktur.ViewModels
{
    public class EditInvoiceDataViewModel : ViewModelBase
    {
        #region OnPropertyChangeProperties
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
        private DateTime _PaymentDeadline;
        public DateTime PaymentDeadline
        {
            get { return _PaymentDeadline; }
            set
            {
                _PaymentDeadline = value;
                OnPropertyChanged(nameof(PaymentDeadline));
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

        private InvoiceModel _model;
        public ICommand SaveInvoiceChangesButton { get; set; }

        public EditInvoiceDataViewModel(InvoiceModel model, Action CloseWindow)
        {
            _model = model;
            SetDeafultData();
            SaveInvoiceChangesButton = new SaveInvoiceChangesCommand(CreateObjectFromData, CloseWindow);
        }

        private InvoiceModel CreateObjectFromData()
        {
            _model.InvoiceDate = this.InvoiceDate;
            _model.InvoiceSellDate = this.InvoiceSellDate;
            _model.PaymentDeadLine = this.PaymentDeadline;
            _model.PaymentMethod = this.PaymentMethod;
            _model.BankAccountNumber = this.BankAccountNumber;
            _model.Description = this.InvoiceDescription;
            _model.Contractor.NIPNumber = InvoiceContractorNIPNumber;
            _model.Contractor.Name = InvoiceContractorName;
            _model.Contractor.Address = InvoiceContractorAddress;
            _model.Owner.NIPNumber = InvoiceOwnerNIPNumber;
            _model.Owner.Name = InvoiceOwnerName;
            _model.Owner.Address = InvoiceOwnerAddress;
            return _model;
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

        private void SetDeafultData()
        {
            BankAccountNumber = _model.BankAccountNumber;
            InvoiceDate = _model.InvoiceDate;
            InvoiceSellDate = _model.InvoiceSellDate;
            PaymentDeadline = _model.PaymentDeadLine;
            PaymentMethod = _model.PaymentMethod;
            InvoiceDescription = _model.Description;
            InvoiceContractorNIPNumber = _model.Contractor.NIPNumber;
            InvoiceContractorName = _model.Contractor.Name;
            InvoiceContractorAddress = _model.Contractor.Address;
            InvoiceOwnerNIPNumber = _model.Owner.NIPNumber;
            InvoiceOwnerName = _model.Owner.Name;
            InvoiceOwnerAddress = _model.Owner.Address;
        }
    }
}
