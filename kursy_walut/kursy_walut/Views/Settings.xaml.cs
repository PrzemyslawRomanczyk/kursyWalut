
using kursy_walut.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQLite;

namespace kursy_walut.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Settings : ContentPage
    {
        private SQLiteAsyncConnection _ConnectionSet;
        private IList<SettingsPicker> _settingsPickers;
        private ObservableCollection<CurrencyC> _Currency;

        public Settings(SQLiteAsyncConnection _Connection)
        {
            InitializeComponent();
            SettingsPicker PickerOpt = new SettingsPicker();
            _settingsPickers = PickerOpt.GetSettingsPickers();
            _ConnectionSet = _Connection;

            foreach (var method in _settingsPickers)
                CurrencyPicker.Items.Add(method.CurrencyOptionName);
        }

        protected override async void OnAppearing()
        {
            await _ConnectionSet.CreateTableAsync<CurrencyC>();

            var CurrentCurrencyDB = await _ConnectionSet.Table<CurrencyC>().ToListAsync();
            _Currency = new ObservableCollection<CurrencyC>(CurrentCurrencyDB);
            foreach (var element in _Currency)
                _settingsPickers.Remove(_settingsPickers.SingleOrDefault(x => x.CurrencyOptionName == element.CurrencyName));
        }

        private void Picker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (CurrencyPicker.SelectedIndex != -1)
            { 
                var name = CurrencyPicker.Items[CurrencyPicker.SelectedIndex];
                var picker = _settingsPickers.Single(cm => cm.CurrencyOptionName == name);
                _ConnectionSet.InsertAsync(new CurrencyC { CurrencyName = name, CurrencyRatio = 0.0 });
                _settingsPickers.Remove(picker);
                CurrencyPicker.Items.Remove(picker.CurrencyOptionName);
                CurrencyPicker.SelectedItem = -1;
                DisplayAlert("Added new currency to watched list", picker.CurrencyOptionName, "Ok");
            }
        }
    }
}