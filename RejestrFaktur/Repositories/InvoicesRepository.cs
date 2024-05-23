using Microsoft.EntityFrameworkCore;
using RejestrFaktur.Databases;
using RejestrFaktur.Interfaces;
using RejestrFaktur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejestrFaktur.Repositories
{
    public class InvoicesRepository : IInvoicesRepository
    {
        private readonly InvoicesDbContext _db;

        public InvoicesRepository(InvoicesDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddInvoice(InvoiceModel model)
        {
            await _db.Invoices.AddAsync(model);
            return await Save();

        }

        public async Task<List<string>> GetAllInvoicesNumbers()
        {
            return await _db.Invoices.Select(i => i.InvoiceNumber).ToListAsync();
        }

        public async Task<InvoiceModel> GetInvoiceByName(string name)
        {
            return await _db.Invoices
                .Include(i => i.Contractor)
                .Include(i => i.Owner)
                .Include(i => i.Items)
                .FirstAsync(i => i.InvoiceNumber == name);
        }

        public async Task<bool> RemoveInvoiceById(int id)
        {
            _db.Invoices.Remove(await _db.Invoices.FirstAsync(i => i.Id == id));
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UpdateInvoice(InvoiceModel model)
        {
            _db.Invoices.Update(model);
            return await Save();
        }
    }
}
