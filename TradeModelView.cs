using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private ObservableCollection<string> _winLossOptions;
        public ObservableCollection<string> WinLossOptions
        {
            get => _winLossOptions;
            set { _winLossOptions = value; OnPropertyChanged(nameof(_winLossOptions)); }
        }

        private ObservableCollection<string> _shortLongptions;
        public ObservableCollection<string> ShortLongptions
        {
            get => _shortLongptions;
            set { _shortLongptions = value; OnPropertyChanged(nameof(_shortLongptions)); }
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

        public RelayCommand ClickCommand { get; }

        public TradeModelView()
        {
           
            SymbolOptions = new ObservableCollection<string> { "ES11", "USDGBP", "JPYUSD" };
            ShortLongptions = new ObservableCollection<string> { "Short", "Long" };
            WinLossOptions = new ObservableCollection<string> { "Win", "Loss" };
            Trades = new ObservableCollection<TradeModel>();
            NewTrade = new TradeModel();
            // Initialize NewTrade with current date
            NewTrade = new TradeModel
            {
                Date = DateTime.Now
            };
            ClickCommand = new RelayCommand(_ => ExecuteClick());
        }

        private void ExecuteClick()
        {
            Trades.Add(new TradeModel
            {
                Id = Trades.Count + 1,
                Symbol = NewTrade.Symbol,
                Date = NewTrade.Date,
                EntryPrice = NewTrade.EntryPrice,
                ClosePrice = NewTrade.ClosePrice,
                ShortLong = NewTrade.ShortLong,
                WinLoss = NewTrade.WinLoss,
                Value = NewTrade.Value,
                Note = NewTrade.Note
            });
            ResetNewTrade();
        }
        private void ResetNewTrade()
        {
            NewTrade = new TradeModel
            {
                // Reset to defaults
                Date = DateTime.Now, // Keep current date
                Symbol = "",
                EntryPrice = 0,
                ClosePrice = 0,
                ShortLong = "",
                WinLoss = "",
                Value = 0,
                Note = ""
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}