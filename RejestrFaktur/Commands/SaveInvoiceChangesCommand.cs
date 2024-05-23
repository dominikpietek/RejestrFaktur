using RejestrFaktur.Databases;
using RejestrFaktur.Interfaces;
using RejestrFaktur.Models;
using RejestrFaktur.Repositories;
using RejestrFaktur.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejestrFaktur.Commands
{
    public class SaveInvoiceChangesCommand : CommandBase
    {
        private readonly Func<InvoiceModel> _GenerateObjectFromData;
        private readonly Action _CloseWindow;

        public SaveInvoiceChangesCommand(Func<InvoiceModel> GenerateObjectFromData, Action CloseWindow)
        {
            _GenerateObjectFromData = GenerateObjectFromData;
            _CloseWindow = CloseWindow;
        }

        public async override void Execute(object parameter)
        {
            var model = _GenerateObjectFromData.Invoke();
            using (var db = new InvoicesDbContext())
            {
                IInvoicesRepository repository = new InvoicesRepository(db);
                await repository.UpdateInvoice(model);
            }
            var generateInvoice = new GenerateInvoice(model);
            generateInvoice.Generate();
            generateInvoice.Show();
            _CloseWindow.Invoke();
        }
    }
}
