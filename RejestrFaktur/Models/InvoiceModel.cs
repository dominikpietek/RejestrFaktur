using RejestrFaktur.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejestrFaktur.Models
{
    public class InvoiceModel
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = "";
        public InvoiceTypesEnum InvoiceType { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime InvoiceSellDate { get; set; }
        public DateTime PaymentDeadLine { get; set; }
        public PaymentMethodsEnum PaymentMethod { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? Description { get; set; }
        public CompanyDataContractorModel Contractor { get; set; } = new CompanyDataContractorModel();
        public CompanyDataOwnerModel Owner { get; set; } = new CompanyDataOwnerModel();
        public List<ItemModel> Items { get; set; } = new List<ItemModel>();
    }
}
