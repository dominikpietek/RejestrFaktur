using RejestrFaktur.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RejestrFaktur.Commands
{
    public class CreateInvoiceCommand : CommandBase
    {
        private readonly Action _CloseWindow;
        public CreateInvoiceCommand(Action CloseWindow)
        {
            _CloseWindow = CloseWindow;
        }
        public override void Execute(object parameter)
        {
            Window window = new CreateInvoiceView();
            window.Show();
            _CloseWindow.Invoke();
        }
    }
}
