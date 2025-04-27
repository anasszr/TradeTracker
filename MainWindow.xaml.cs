using System;
using System.IO;
using Microsoft.Win32;
using System.Windows;
using TradeTracker.ViewModels;
using System.Windows.Media.Imaging;


namespace TradeTracker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            DataContext = new TradeModelView(); // Set ViewModel
        }
        private void OnPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Allow only numeric input (digits, and one decimal point)
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9\.]+$");
        }
        public static BitmapImage LoadImage(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return null;

            byte[] imageData = File.ReadAllBytes(path);

            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = mem;
                image.EndInit();
                image.Freeze(); // Optional: allow cross-thread access
            }

            return image;
        }
        private void Button_Exit(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                Multiselect = false
            };

            if (dlg.ShowDialog() == true)
            {
                var selectedFile = dlg.FileName;

                if (Application.Current.MainWindow.DataContext is TradeModelView vm)
                {
                    var currentTrade = vm.NewTrade;

                    var date = currentTrade.Date;
                    string formattedDate = date.ToString("yyyyMMdd"); // Example: 20250427
                    var symbol = string.IsNullOrWhiteSpace(currentTrade.Symbol) ? "UnknownSymbol" : currentTrade.Symbol;

                    var picturesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                    var tradeTrackerFolder = System.IO.Path.Combine(picturesFolder, "TradeTracker");
                    var tradeFolderName = $"Trade_{formattedDate}_{symbol}";
                    var tradeFolderPath = System.IO.Path.Combine(tradeTrackerFolder, tradeFolderName);

                    // Create the Trade folder if it doesn't exist
                    System.IO.Directory.CreateDirectory(tradeFolderPath);

                    // BEFORE copying new file: Delete old image if exists
                    if (!string.IsNullOrEmpty(currentTrade.Media) && System.IO.File.Exists(currentTrade.Media))
                    {
                        try
                        {
                            System.IO.File.Delete(currentTrade.Media);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Failed to delete old image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                    var originalFileName = System.IO.Path.GetFileName(selectedFile);
                    var newFileName = $"{formattedDate}_{originalFileName}";

                    var destFile = System.IO.Path.Combine(tradeFolderPath, newFileName);

                    System.IO.File.Copy(selectedFile, destFile, overwrite: true);

                    // Update trade media path
                    currentTrade.Media = destFile;

                    MessageBox.Show($"Image copied to:\n{destFile}", "Upload Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }



        }
    }
}
