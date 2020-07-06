using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace kursy_walut.Models
{
    [Table("CurrencyCurrentRatio")]
    public class CurrencyC //INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public double CurrencyRatio { get; set; }

        //public string CurrencyName {
        //        get { return CurrencyName; }
        //        set
        //        {
        //        CurrencyName = value;
        //        OnPropertyChanged();
        //        }
        //    }
        //public double CurrencyRatio
        //{
        //    get { return CurrencyRatio; }
        //    set
        //    {
        //        CurrencyRatio = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public event PropertyChangedEventHandler PropertyChanged;
        //private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
