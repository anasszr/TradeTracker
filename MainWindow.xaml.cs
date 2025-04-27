using System;
using System.Windows;
using TradeTracker.ViewModels;

namespace TradeTracker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            DataContext = new TradeModelView(); // Set ViewModel
        }

        private void Button_Exit(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        
    }
}