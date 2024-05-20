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

        public override void Execute(object parameter)
        {
            InvoiceModel model = _CreateObjectFromData.Invoke();
            var window = new AddItemToInvoiceView(model);
            window.Show();
            _CloseWindow.Invoke();
        }
    }
}
