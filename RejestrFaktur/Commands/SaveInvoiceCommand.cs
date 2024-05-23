using RejestrFaktur.Databases;
using RejestrFaktur.Interfaces;
using RejestrFaktur.Models;
using RejestrFaktur.Repositories;
using RejestrFaktur.Services;
using RejestrFaktur.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RejestrFaktur.Commands
{
    public class SaveInvoiceCommand : CommandBase
    {
        private readonly Func<InvoiceModel> _CreateObjectFromData;
        private readonly Action _CloseWindow;

        public SaveInvoiceCommand(Func<InvoiceModel> CreateObjectFromData, Action CloseWindow)
        {
            _CreateObjectFromData = CreateObjectFromData;
            _CloseWindow = CloseWindow;
        }

        public async override void Execute(object parameter)
        {
            InvoiceModel model = _CreateObjectFromData.Invoke();
            List<string> invoicesNumbers;
            using (var db = new InvoicesDbContext())
            {
                IInvoicesRepository repository = new InvoicesRepository(db);
                invoicesNumbers = await repository.GetAllInvoicesNumbers();
            }
            if (!invoicesNumbers.Contains(model.InvoiceNumber))
            {
                var window = new AddItemToInvoiceView(model);
                window.Show();
                _CloseWindow.Invoke();
            }
            else
            {
                MessageBox.Show(
                    messageBoxText: "Istnieje już faktura o tym numerze!",
                    caption: "",
                    button: MessageBoxButton.OK,
                    icon: MessageBoxImage.Error);
            }
        }
    }
}
