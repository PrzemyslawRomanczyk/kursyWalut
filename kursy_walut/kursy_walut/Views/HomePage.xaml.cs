using kursy_walut.Models;
using kursy_walut.Persistence;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace kursy_walut.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class HomePage : ContentPage
    {
        private const string Url = "https://api.exchangeratesapi.io";
        private HttpClient _client = new HttpClient();

        private ObservableCollection<CurrencyC> _Currency;
        private String _ApiRespons;
        private SQLiteAsyncConnection _Connection;

        async Task getCurrencyChangeRatio(string Currency)
        {
            //System.Diagnostics.Debug.WriteLine("inside3");
            await RequestApi(Currency);
            JObject RatesObj = JObject.Parse(_ApiRespons);
            double CurrentRate = Math.Round((double)RatesObj.SelectToken("rates.PLN"), 2);
            if (_Currency.Contains(_Currency.SingleOrDefault(x => x.CurrencyName == Currency)))
            {
                _Currency.SingleOrDefault(x => x.CurrencyName == Currency).CurrencyRatio = CurrentRate;
            }
            else
            {
                _Currency.Add(new CurrencyC { CurrencyName = Currency, CurrencyRatio = CurrentRate });
                await _Connection.InsertAsync(new CurrencyC { CurrencyName = Currency, CurrencyRatio = CurrentRate });
            }
        }
        async Task RequestApi(string CurrencyName)
        {
            _ApiRespons = await _client.GetStringAsync(Url + "/latest?symbols=PLN&base=" + CurrencyName);
        }

        protected override async void OnAppearing()
        {
            await _Connection.CreateTableAsync<CurrencyC>();
            var CurrentCurrencyDB = await _Connection.Table<CurrencyC>().ToListAsync();
            _Currency = new ObservableCollection<CurrencyC>(CurrentCurrencyDB); 

            if (CurrentCurrencyDB.Any())
            {
                foreach (var Name in _Currency)
                    await getCurrencyChangeRatio(Name.CurrencyName);
            }
            listView.ItemsSource = _Currency;
            base.OnAppearing();
        }

        public HomePage()
        {
            InitializeComponent();
            _Connection = DependencyService.Get<ISQLiteDb>().GetConnection();

        }

        private void DeleteElement(object sender, EventArgs e)
        {
            var Element = (sender as MenuItem).CommandParameter as CurrencyC;
            _Currency.Remove(Element);
            _Connection.DeleteAsync(Element);

        }

        async private void HandleSelection(object sender, SelectedItemChangedEventArgs e)
        {
            var SelectedCurrencyObj = e.SelectedItem as CurrencyC;
            await Navigation.PushAsync(new Views.DetailPage(SelectedCurrencyObj, _Connection, _client));
        }

        async private  void RefreshList(object sender, EventArgs e)
        {
            foreach (var Name in _Currency)
                    await getCurrencyChangeRatio(Name.CurrencyName);
            listView.ItemsSource = _Currency;
            listView.EndRefresh();
        }
        async private void GoToSettings(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.Settings(_Connection));
        }
    }
}
