using RejestrFaktur.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RejestrFaktur.Views
{
    /// <summary>
    /// Interaction logic for CreateInvoiceView.xaml
    /// </summary>
    public partial class CreateInvoiceView : Window
    {
        public CreateInvoiceView()
        {
            InitializeComponent();
            this.DataContext = new CreateInvoiceViewModel(CloseWindow);
        }

        public void CloseWindow()
        {
            this.Close();
        }
    }
}
