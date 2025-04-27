using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using TradeTracker.Models;

namespace TradeTracker.ViewModels
{
    public class TradeModelView : INotifyPropertyChanged
    {
        private ObservableCollection<string> _symbolOptions;
        public ObservableCollection<string> SymbolOptions
        {
            get => _symbolOptions;
            set { _symbolOptions = value; OnPropertyChanged(nameof(SymbolOptions)); }
        }

        private ObservableCollection<string> _shortLongOptions;
        public ObservableCollection<string> ShortLongptions
        {
            get => _shortLongOptions;
            set { _shortLongOptions = value; OnPropertyChanged(nameof(ShortLongptions)); }
        }

        private ObservableCollection<TradeModel> _trades;
        public ObservableCollection<TradeModel> Trades
        {
            get => _trades;
            set { _trades = value; OnPropertyChanged(nameof(Trades)); }
        }

        private TradeModel _newTrade;
        public TradeModel NewTrade
        {
            get => _newTrade;
            set { _newTrade = value; OnPropertyChanged(nameof(NewTrade)); }
        }

        private bool _isEditMode = false;

        public ICommand ClickCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public TradeModelView()
        {
            SymbolOptions = new ObservableCollection<string> { "ES11", "USDGBP", "JPYUSD" };
            ShortLongptions = new ObservableCollection<string> { "-- select --", "Short", "Long" };
            Trades = new ObservableCollection<TradeModel>();

            // Initialize form
            ResetNewTrade();

            // Commands
            ClickCommand = new RelayCommand(_ => ExecuteClick());
            EditCommand = new RelayCommand(obj => OnEdit(obj as TradeModel));
            DeleteCommand = new RelayCommand(obj => OnDelete(obj as TradeModel));
        }

        private void ExecuteClick()
        {
            if (!_isEditMode)
            {
                // Add new trade
                var newEntry = new TradeModel
                {
                    Id = Trades.Count + 1,
                    Symbol = NewTrade.Symbol,
                    Date = NewTrade.Date,
                    EntryPrice = NewTrade.EntryPrice,
                    ClosePrice = NewTrade.ClosePrice,
                    ShortLong = NewTrade.ShortLong,
                    Contracts = NewTrade.Contracts,
                    Note = NewTrade.Note,
                    Media = NewTrade.Media
                };
                Trades.Add(newEntry);
            }
            else
            {
                // In edit mode, changes are already propagated
                _isEditMode = false;
            }

            // Reset form
            ResetNewTrade();
        }

        private void OnEdit(TradeModel trade)
        {
            if (trade == null) return;
            _isEditMode = true;
            // Load selected trade into form for editing
            NewTrade = trade;
        }

        private void OnDelete(TradeModel trade)
        {
            if (trade == null) return;
            var result = MessageBox.Show(
                $"Delete trade #{trade.Id}?",
                "Confirm delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
                Trades.Remove(trade);
        }

        private void ResetNewTrade()
        {
            NewTrade = new TradeModel
            {
                Date = DateTime.Now,
                Contracts = 1m,
                ShortLong = "-- select --"
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
