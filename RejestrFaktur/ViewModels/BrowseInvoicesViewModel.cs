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
using System.Configuration;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

            using (var db = new InvoicesDbContext())
            {
                IInvoicesRepository repository = new InvoicesRepository(db);
                foreach (string invoiceName in await repository.GetAllInvoicesNumbers())
                {
                    var model = await repository.GetInvoiceByName(invoiceName);
                    var generateInvoice = new GenerateInvoice(model);
                    generateInvoice.Generate();
                    Invoices.Add(GenerateStackPanel(model, generateInvoice));
                }
            }
        }

        private StackPanel GenerateStackPanel(InvoiceModel model, GenerateInvoice generateInvoice)
        {
            var background = Colors.LightGray;
            var fontSize = 14;
            var height = 80;
            StackPanel stackPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new System.Windows.Thickness(0, 0, 0, 10),
                Background = new SolidColorBrush(background),
        };
            stackPanel.Children.Add(new Label()
            {
                Content = $"Faktura nr: {model.InvoiceNumber}",
                FontSize = fontSize,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Padding = new System.Windows.Thickness(10, 0, 10, 0),
                Background = new SolidColorBrush(background),
                FontWeight = FontWeights.Bold
            });
            stackPanel.Children.Add(new Button()
            {
                Content = "Podgląd Faktury",
                Command = new SelectedCommand(generateInvoice),
                Width = 150,
                Height = height,
                Cursor = Cursors.Hand,
                Background = new SolidColorBrush(background),
                BorderThickness = new System.Windows.Thickness(0, 0, 0, 0),
                FontSize = fontSize,
                FontWeight = FontWeights.Bold
            });
            stackPanel.Children.Add(new Button()
            {
                Content = "Edytuj Dane Faktury",
                Command = new EditInvoiceDataCommand(model),
                Width = 200,
                Height = height,
                Cursor = Cursors.Hand,
                Background = new SolidColorBrush(background),
                BorderThickness = new System.Windows.Thickness(0, 0, 0, 0),
                FontSize = fontSize,
                FontWeight = FontWeights.Bold
            });
            stackPanel.Children.Add(new Button()
            {
                Content = "Edytuj Elementy Faktury",
                Command = new EditInvoiceItemCommand(model),
                Width = 200,
                Height = height,
                Cursor = Cursors.Hand,
                Background = new SolidColorBrush(background),
                BorderThickness = new System.Windows.Thickness(0, 0, 0, 0),
                FontSize = fontSize,
                FontWeight = FontWeights.Bold
            });
            stackPanel.Children.Add(new Button()
            {
                Content = "Dodaj Element",
                Command = new AddElementToExistingInvoiceCommand(model),
                Width = 150,
                Height = height,
                Cursor = Cursors.Hand,
                Background = new SolidColorBrush(background),
                BorderThickness = new System.Windows.Thickness(0, 0, 0, 0),
                FontSize = fontSize,
                FontWeight = FontWeights.Bold
            });

            stackPanel.Children.Add(new Button()
            {
                Content = new Image() { Source = GenerateImageSourceFromPath(ConfigurationManager.AppSettings["absolutePath"]) },
                Command = new RemoveInvoiceCommand(model, stackPanel, Invoices),
                Width = 60,
                Height = height,
                Cursor = Cursors.Hand,
                Background = new SolidColorBrush(background),
                BorderThickness = new System.Windows.Thickness(0, 0, 0, 0),
                FontSize = fontSize,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                FontWeight = FontWeights.Bold
            });
        return stackPanel;
        }
        private BitmapImage GenerateImageSourceFromPath(string path)
        {
            var imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.UriSource = new Uri(@$"{path}\Resources\trashIcon.png", UriKind.Absolute);
            imageSource.EndInit();
            return imageSource;
        }
    }
}
