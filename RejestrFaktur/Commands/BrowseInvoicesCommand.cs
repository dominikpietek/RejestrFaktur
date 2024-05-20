using RejestrFaktur.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RejestrFaktur.Commands
{
    public class BrowseInvoicesCommand : CommandBase
    {
        private readonly Action _CloseWindow;
        public BrowseInvoicesCommand(Action CloseWindow)
        {
            _CloseWindow = CloseWindow;
        }
        public override void Execute(object parameter)
        {
            Window window = new BrowseInvoicesView();
            window.Show();
            _CloseWindow.Invoke();
        }
    }
}
