using RejestrFaktur.Databases;
using RejestrFaktur.Interfaces;
using RejestrFaktur.Models;
using RejestrFaktur.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RejestrFaktur.Commands
{
    public class RemoveInvoiceCommand : CommandBase
    {
        private readonly InvoiceModel _invoiceModel;
        private readonly StackPanel _stackPanel;
        private readonly ObservableCollection<StackPanel> _invoices;

        public RemoveInvoiceCommand(InvoiceModel invoiceModel, StackPanel stackPanel, ObservableCollection<StackPanel> invoices)
        {
            _invoiceModel = invoiceModel;
            _stackPanel = stackPanel;
            _invoices = invoices;
        }

        public async override void Execute(object parameter)
        {
            using (var db = new InvoicesDbContext())
            {
                IInvoicesRepository repository = new InvoicesRepository(db);
                await repository.RemoveInvoiceById(_invoiceModel.Id);
            }
            _invoices.Remove(_stackPanel);
            File.Delete($@"C:\Programs\RejestrFaktur\RejestrFaktur\Invoices\{_invoiceModel.InvoiceNumber}.pdf");
        }
    }
}
