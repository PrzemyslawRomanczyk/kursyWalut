using kursy_walut.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        private ObservableCollection<Waluty> _Currency;
        private String _ApiRespons;
        private ObservableCollection<Waluty> _CurrencyNames;

        async Task getCurrencyChangeRatio(string Currency, ObservableCollection<Waluty> Coll)
        {
            await RequestApi(Currency);
            System.Diagnostics.Debug.WriteLine("Inside"+ _ApiRespons);
            JObject RatesObj = JObject.Parse(_ApiRespons);
            double CurrenyRate = (double)RatesObj.SelectToken("rates.PLN");
            System.Diagnostics.Debug.WriteLine("Rates " + CurrenyRate);
            Coll.Add(new Waluty { NazwaWaluty = Currency, KursWaluty = CurrenyRate } );
        }
        async Task RequestApi(string CurrencyName)
        {
            _ApiRespons = await _client.GetStringAsync(Url + "/latest?symbols=PLN&base=" + CurrencyName);
        }

        protected override async void OnAppearing()
        {
            System.Diagnostics.Debug.WriteLine("_Currency1 " + _Currency);
            base.OnAppearing();
        }

        public HomePage()
        {
            InitializeComponent();
            listView.ItemsSource = _Currency;

        }

        private void DeleteElement(object sender, EventArgs e)
        {
            var Element = (sender as MenuItem).CommandParameter as Waluty;
            _Currency.Remove(Element);

        }

        async private void HandleSelection(object sender, SelectedItemChangedEventArgs e)
        {
            var Kurs = e.SelectedItem as Waluty;
            await Navigation.PushAsync(new Views.DetailPage());
        }

        private  void RefreshList(object sender, EventArgs e)
        {
            //await getCurrencyChangeRatio("EUR");
            listView.ItemsSource = _Currency;
            listView.EndRefresh();
        }
    }
}
