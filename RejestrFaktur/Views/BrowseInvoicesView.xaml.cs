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
    /// Interaction logic for BrowseInvoicesView.xaml
    /// </summary>
    public partial class BrowseInvoicesView : Window
    {
        public BrowseInvoicesView()
        {
            InitializeComponent();
            this.DataContext = new BrowseInvoicesViewModel();
        }
    }
}
