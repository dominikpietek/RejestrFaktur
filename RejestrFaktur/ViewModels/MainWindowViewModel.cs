using RejestrFaktur.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RejestrFaktur.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ICommand CreateInvoiceButton { get; set; }
        public ICommand BrowseInvoicesButton { get; set; }
        public MainWindowViewModel(Action CloseWindow)
        {
            CreateInvoiceButton = new CreateInvoiceCommand(CloseWindow);
            BrowseInvoicesButton = new BrowseInvoicesCommand(CloseWindow);
        }
    }
}
