using RejestrFaktur.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejestrFaktur.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public InvoiceModel Invoice { get; set; }
        public string Name { get; set; } = "";
        public UnitsEnum Unit { get; set; }
        public int Amount { get; set; }
        public int NetPrice { get; set; }
        public double TaxRate { get; set; }
    }
}
