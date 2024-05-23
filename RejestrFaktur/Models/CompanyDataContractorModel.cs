using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejestrFaktur.Models
{
    public class CompanyDataContractorModel
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public InvoiceModel Invoice { get; set; }
        public string? NIPNumber { get; set; } = "";
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
    }
}
