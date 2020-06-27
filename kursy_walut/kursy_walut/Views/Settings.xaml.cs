
using kursy_walut.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace kursy_walut.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        private IList<SettingsPicker> _settingsPickers;
        public ObservableCollection<string> _ListOfCurrencies;
        public Settings()
        {
            InitializeComponent();
            SettingsPicker PickerOpt = new SettingsPicker();
            _settingsPickers = PickerOpt.GetSettingsPickers();

            if (Application.Current.Properties.ContainsKey("OptionsCurrency"))
                _ListOfCurrencies.Add(Application.Current.Properties["OptionsCurrency"]
                    .ToString());
            else
            {
                _ListOfCurrencies = new ObservableCollection<string> { "EUR", "USD" };
                _settingsPickers.Remove(_settingsPickers.SingleOrDefault(x => x.CurrencyOptionName == "USD"));
                _settingsPickers.Remove(_settingsPickers.SingleOrDefault(x => x.CurrencyOptionName == "EUR"));
                Application.Current.SavePropertiesAsync();
            }

            foreach (var method in _settingsPickers)
                CurrencyPicker.Items.Add(method.CurrencyOptionName);
        }

        private void Picker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (CurrencyPicker.SelectedIndex != -1)
            { 
                var name = CurrencyPicker.Items[CurrencyPicker.SelectedIndex];
                var picker = _settingsPickers.Single(cm => cm.CurrencyOptionName == name);
                _ListOfCurrencies.Add(name);
                Application.Current.Properties["OptionsCurrency"] = _ListOfCurrencies;
                _settingsPickers.Remove(picker);
                CurrencyPicker.Items.Remove(picker.CurrencyOptionName);
                CurrencyPicker.SelectedItem = -1;
                DisplayAlert("Added new currency to watched list", picker.CurrencyOptionName, "Ok");

                Application.Current.SavePropertiesAsync();
            }
        }
    }
}