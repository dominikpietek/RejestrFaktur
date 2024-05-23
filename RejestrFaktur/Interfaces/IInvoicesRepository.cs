using RejestrFaktur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejestrFaktur.Interfaces
{
    public interface IInvoicesRepository
    {
        Task<InvoiceModel> GetInvoiceByName(string name);
        Task<bool> AddInvoice(InvoiceModel model);
        Task<bool> UpdateInvoice(InvoiceModel model);
        Task<bool> RemoveInvoiceById(int id);
        Task<List<string>> GetAllInvoicesNumbers();
        Task<bool> Save();
    }
}
