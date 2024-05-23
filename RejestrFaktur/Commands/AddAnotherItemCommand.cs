using RejestrFaktur.Models;
using RejestrFaktur.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RejestrFaktur.Commands
{
    public class AddAnotherItemCommand : CommandBase
    {
        private readonly Action _AddItem;
        private readonly Action _CloseWindow;
        private readonly InvoiceModel _model;

        public AddAnotherItemCommand(Action AddItem, Action CloseWindow, InvoiceModel model)
        {
            _AddItem = AddItem;
            _CloseWindow = CloseWindow;
            _model = model;
        }

        public override void Execute(object parameter)
        {
            _AddItem.Invoke();
            Window window = new AddItemToInvoiceView(_model);
            window.Show();
            _CloseWindow.Invoke();
        }
    }
}
