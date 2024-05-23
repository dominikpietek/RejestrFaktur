using Microsoft.EntityFrameworkCore.Metadata;
using RejestrFaktur.Commands;
using RejestrFaktur.Databases;
using RejestrFaktur.Interfaces;
using RejestrFaktur.Models;
using RejestrFaktur.Repositories;
using RejestrFaktur.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace RejestrFaktur.ViewModels
{
    public class BrowseInvoicesViewModel : ViewModelBase
    {
        private ObservableCollection<StackPanel> _Invoices = new ObservableCollection<StackPanel>();
        public ObservableCollection<StackPanel> Invoices 
        { 
            get { return _Invoices; }
            set
            {
                _Invoices = value;
                OnPropertyChanged(nameof(Invoices));
            }
        }

        private int _InvoiceIndex = 0;

        public BrowseInvoicesViewModel()
        {
            GetInvoices();
        }

        private async Task GetInvoices()
        {
            foreach (string invoiceName in SearchInvoicesLocally.GetInvoicesName())
            {
                using (var db = new InvoicesDbContext())
                {
                    IInvoicesRepository repository = new InvoicesRepository(db);
                    invoiceName.Replace(".pdf", "");
                    var model = await repository.GetInvoiceByName(invoiceName);
                    var generateInvoice = new GenerateInvoice(model);
                    generateInvoice.Generate();
                    Invoices.Add(GenerateStackPanel(model, generateInvoice));
                }
            }
        }

        private StackPanel GenerateStackPanel(InvoiceModel model, GenerateInvoice generateInvoice)
        {

            StackPanel stackPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new System.Windows.Thickness(0, 0, 0, 10)
            };
            stackPanel.Children.Add(new Button()
            {
                Content = model.InvoiceNumber,
                Command = new SelectedCommand(generateInvoice),
                Width = 100,
                Height = 50,
                Cursor = Cursors.Hand
            });
            stackPanel.Children.Add(new Button()
            {
                Content = "edytuj dane faktury",
                Command = new EditInvoiceDataCommand(model),
                Width = 200,
                Height = 50,
                Cursor = Cursors.Hand
            });
            stackPanel.Children.Add(new Button()
            {
                Content = "edytuj elementy faktury",
                Command = new EditInvoiceItemCommand(model),
                Width = 200,
                Height = 50,
                Cursor = Cursors.Hand
            });
            stackPanel.Children.Add(new Button()
            {
                Content = "dodaj element",
                Command = new AddElementToExistingInvoiceCommand(model),
                Width = 150,
                Height = 50,
                Cursor = Cursors.Hand
            });
            stackPanel.Children.Add(new Button()
            {
                Content = "usuń fakture",
                Command = new RemoveInvoiceCommand(model, stackPanel, Invoices),
                Width = 140,
                Height = 50,
                Cursor = Cursors.Hand
            });
            return stackPanel;
        }
    }
}
