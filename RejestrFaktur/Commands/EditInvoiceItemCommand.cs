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
    public class EditInvoiceItemCommand : CommandBase
    {
        private readonly InvoiceModel _model;

        public EditInvoiceItemCommand(InvoiceModel model)
        {
            _model = model;
        }

        public override void Execute(object parameter)
        {
            Window window = new EditInvoiceElementView(_model);
            window.Show();
        }
    }
}
