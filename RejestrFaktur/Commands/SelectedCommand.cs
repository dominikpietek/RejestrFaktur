using RejestrFaktur.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejestrFaktur.Commands
{
    public class SelectedCommand : CommandBase
    {
        private readonly GenerateInvoice _generateInvoice;

        public SelectedCommand(GenerateInvoice generateInvoice)
        {
            _generateInvoice = generateInvoice;
        }

        public override void Execute(object parameter)
        {
            _generateInvoice.Show();
        }
    }
}
