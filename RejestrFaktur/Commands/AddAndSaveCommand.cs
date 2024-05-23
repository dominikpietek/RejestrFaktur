using RejestrFaktur.Databases;
using RejestrFaktur.Interfaces;
using RejestrFaktur.Models;
using RejestrFaktur.Repositories;
using RejestrFaktur.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejestrFaktur.Commands
{
    public class AddAndSaveCommand : CommandBase
    {
        private readonly Action _Save;
        private readonly Action _CloseWindow;
        private readonly InvoiceModel _model;

        public AddAndSaveCommand(Action Save, Action CloseWindow, InvoiceModel model)
        {
            _model = model;
            _Save = Save;
            _CloseWindow = CloseWindow;
        }

        public async override void Execute(object parameter)
        {
            _Save.Invoke();
            if (_model.Id == null)
            {
                using (var db = new InvoicesDbContext())
                {
                    IInvoicesRepository repository = new InvoicesRepository(db);
                    await repository.AddInvoice(_model);
                }
            }
            else
            {
                using (var db = new InvoicesDbContext())
                {
                    IInvoicesRepository repository = new InvoicesRepository(db);
                    await repository.UpdateInvoice(_model);
                }
            }
            _CloseWindow.Invoke();
        }
    }
}
