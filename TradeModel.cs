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

      

        public decimal EntryPrice
        {
            get => _entryPrice;
            set { _entryPrice = value; OnPropertyChanged(); }
        }

        public decimal ClosePrice
        {
            get => _closePrice;
            set { _closePrice = value; OnPropertyChanged(); }
        }

        public string ShortLong
        {
            get => _shortLong;
            set { _shortLong = value; OnPropertyChanged(); }
        }
        public string WinLoss
        {
            get => _winLoss;
            set { _winLoss = value; OnPropertyChanged(); }
        }

        public decimal Value
        {
            get => _value;
            set { _value = value; OnPropertyChanged(); }
        }

        public string Note
        {
            get => _note;
            set { _note = value; OnPropertyChanged(); }
        }
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DateDisplay)); // Add this line
            }
        }

        // Add this property for formatted display
        public string DateDisplay => Date.ToString("yyyy-MM-dd");

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}