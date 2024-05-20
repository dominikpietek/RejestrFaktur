using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using RejestrFaktur.ViewModels;
using System.Net;
using System.Windows;

namespace RejestrFaktur.Views
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(CloseWindow);
        }

        public void CloseWindow()
        {
            this.Close();
        }
    }
}
