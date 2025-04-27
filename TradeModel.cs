using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TradeTracker.Models
{
    public class TradeModel : INotifyPropertyChanged
    {
        private int _id;
        private string _symbol;
        private DateTime _date;
        private decimal _entryPrice;
        private decimal _closePrice;
        private string _shortLong;
        private string _winLoss;
        private decimal _value;
        private string _note;
        private decimal _contracts = 1m;   // default to 1
        private string _media;
        public string Media
        {
            get => _media;
            set { _media = value; OnPropertyChanged(); }
        }

        public decimal Contracts
        {
            get => _contracts;
            set
            {
                if (_contracts == value) return;
                _contracts = value;
                OnPropertyChanged();
                Recalculate();
            }
        }

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        public string Symbol
        {
            get => _symbol;
            set { _symbol = value; OnPropertyChanged(); }
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DateDisplay));
            }
        }

        public string DateDisplay => Date.ToString("yyyy-MM-dd");

        public decimal EntryPrice
        {
            get => _entryPrice;
            set
            {
                if (_entryPrice == value) return;
                _entryPrice = value;
                OnPropertyChanged();
                Recalculate();
            }
        }

        public decimal ClosePrice
        {
            get => _closePrice;
            set
            {
                if (_closePrice == value) return;
                _closePrice = value;
                OnPropertyChanged();
                Recalculate();
            }
        }

        public string ShortLong
        {
            get => _shortLong;
            set
            {
                if (_shortLong == value) return;
                _shortLong = value;
                OnPropertyChanged();
                Recalculate();
            }
        }

        // Bind these directly; their setters are now private to prevent external overwrites
        public string WinLoss
        {
            get => _winLoss;
             set
            {
                _winLoss = value;
                OnPropertyChanged();
            }
        }

        public decimal Value
        {
            get => _value;
             set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        public string Note
        {
            get => _note;
            set { _note = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Calculate Win/Loss and P&L in USD (points × $50 multiplier).
        /// </summary>
        private void Recalculate()
        {
           

           
            const decimal multiplier = 50m; // e-mini S&P-500

            if (ShortLong == "Long")
            {
                if (EntryPrice < ClosePrice)
                {
                    WinLoss = "Win";
                    Value = (ClosePrice - EntryPrice) * multiplier *Contracts;
                }
                else
                {
                    WinLoss = "Loss";
                    Value = (EntryPrice - ClosePrice) * multiplier * Contracts;
                }
            }
            else if (ShortLong == "Short")
            {
                if (EntryPrice > ClosePrice)
                {
                    WinLoss = "Win";
                    Value = (EntryPrice - ClosePrice) * multiplier * Contracts;
                }
                else
                {
                    WinLoss = "Loss";
                    Value = (ClosePrice - EntryPrice) * multiplier * Contracts;
                }
            }
            else
            {
                // if none selected yet
                WinLoss = string.Empty;
                Value = 0m;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
