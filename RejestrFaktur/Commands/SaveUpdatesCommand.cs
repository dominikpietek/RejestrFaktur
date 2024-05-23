using Accessibility;
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
    public class SaveUpdatesCommand : CommandBase
    {
        private readonly Func<InvoiceModel> _GetModel;
        private readonly Action _RestartProperties;

        public SaveUpdatesCommand(Func<InvoiceModel> GetModel, Action RestartProperties)
        {
            _GetModel = GetModel;
            _RestartProperties = RestartProperties;
        }

        public async override void Execute(object parameter)
        {
            var model = _GetModel.Invoke();
            using (var db = new InvoicesDbContext())
            {
                IInvoicesRepository repository = new InvoicesRepository(db);
                await repository.UpdateInvoice(model);
            }
            var generateInvoice = new GenerateInvoice(model);
            generateInvoice.Generate();
            generateInvoice.Show();
            _RestartProperties.Invoke();
        }
    }
}
