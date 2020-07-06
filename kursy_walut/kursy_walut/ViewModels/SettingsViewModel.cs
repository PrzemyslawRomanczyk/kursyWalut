using kursy_walut.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace kursy_walut.ViewModels
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        //Variables
        private SQLiteAsyncConnection _ConnectionSet;
        public List<SettingsPicker> SettingsPickersView
        {
            get { return _settingsPickersView; }
            set
                {
                _settingsPickersView = value;
                OnPropertyChanged();
                }
        }
        private ObservableCollection<CurrencyC> _Currency;
        public int SelectedIndex { get; set; }
        public SettingsPicker SelectedItem { get; set; }
        List<SettingsPicker> _settingsPickersView;

        public event PropertyChangedEventHandler PropertyChanged;

        //Constructor
        public SettingsViewModel(SQLiteAsyncConnection _Connection)
        {
            _ConnectionSet = _Connection;

            //SettingsPicker pickeropt = new SettingsPicker();
            //_settingsPickersView = pickeropt.GetSettingsPickers();
            _settingsPickersView = new List<SettingsPicker>
            {
                new SettingsPicker{Id = 0, CurrencyOptionName="CAD"},
                new SettingsPicker{Id = 1, CurrencyOptionName="HKD"},
                new SettingsPicker{Id = 2, CurrencyOptionName="ISK"},
                new SettingsPicker{Id = 3, CurrencyOptionName="PHP"},
                new SettingsPicker{Id = 4, CurrencyOptionName="DKK"},
                new SettingsPicker{Id = 5, CurrencyOptionName="HUF"},
                new SettingsPicker{Id = 6, CurrencyOptionName="CZK"},
                new SettingsPicker{Id = 7, CurrencyOptionName="AUD"},
                new SettingsPicker{Id = 8, CurrencyOptionName="SEK"},
                new SettingsPicker{Id = 9, CurrencyOptionName="INR"},
                new SettingsPicker{Id = 10, CurrencyOptionName="BRL"},
                new SettingsPicker{Id = 11, CurrencyOptionName="RUB"},
                new SettingsPicker{Id = 12, CurrencyOptionName="HRK"},
                new SettingsPicker{Id = 13, CurrencyOptionName="JPY"},
                new SettingsPicker{Id = 14, CurrencyOptionName="THB"},
                new SettingsPicker{Id = 15, CurrencyOptionName="CHF"},
                new SettingsPicker{Id = 16, CurrencyOptionName="BGN"},
                new SettingsPicker{Id = 17, CurrencyOptionName="TRY"},
                new SettingsPicker{Id = 18, CurrencyOptionName="CNY"},
                new SettingsPicker{Id = 19, CurrencyOptionName="NOK"},
                new SettingsPicker{Id = 20, CurrencyOptionName="NZD"},
                new SettingsPicker{Id = 21, CurrencyOptionName="ZAR"},
                new SettingsPicker{Id = 22, CurrencyOptionName="USD"},
                new SettingsPicker{Id = 23, CurrencyOptionName="MXN"},
                new SettingsPicker{Id = 24, CurrencyOptionName="ILS"},
                new SettingsPicker{Id = 25, CurrencyOptionName="GBP"},
                new SettingsPicker{Id = 26, CurrencyOptionName="KRW"},
                new SettingsPicker{Id = 27, CurrencyOptionName="MYR"},
                new SettingsPicker{Id = 28, CurrencyOptionName="EUR"},
                new SettingsPicker{Id = 29, CurrencyOptionName=""},
            };
        }
        //methods
        async public void TrimPicker()
        {
            await _ConnectionSet.CreateTableAsync<CurrencyC>();

            var CurrentCurrencyDB = await _ConnectionSet.Table<CurrencyC>().ToListAsync();
            _Currency = new ObservableCollection<CurrencyC>(CurrentCurrencyDB);
            foreach (var element in _Currency)
                _settingsPickersView.Remove(_settingsPickersView.Single(x => x.CurrencyOptionName == element.CurrencyName));
            System.Diagnostics.Debug.WriteLine("Setting4");
        }
        public void PickerSelect()
        {
            if (SelectedIndex != -1)
            {
                var name = SettingsPickersView[SelectedIndex];
                _ConnectionSet.InsertAsync(new CurrencyC { CurrencyName = name.CurrencyOptionName.ToString() });
                IAlertHelper();
                SettingsPickersView.Remove(name);
                SelectedIndex = -1;
            }
        }

        async private void IAlertHelper()
        {
            await Application.Current.MainPage.DisplayAlert("Added new currency to watched list", SelectedItem.CurrencyOptionName, "Ok");
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
