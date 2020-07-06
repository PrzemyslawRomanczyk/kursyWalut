using kursy_walut.Models;
using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace kursy_walut.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        private SQLiteAsyncConnection _ConnectionSet;
        private CurrencyC CurrentCurrency;
        private HttpClient _client = new HttpClient();
        private String _ApiRespons;
        private const string Url = "https://api.exchangeratesapi.io";
        public DetailPage(CurrencyC SelectedCurrencyObj, SQLiteAsyncConnection Connection, HttpClient Client)
        {
            _ConnectionSet = Connection;
            CurrentCurrency = SelectedCurrencyObj;
            _client = Client;

            InitializeComponent();
        }

        async protected override void OnAppearing()
        {
            CreateDataForChart(CurrentCurrency.CurrencyName);
            base.OnAppearing();
        }

        async private void CreateDataForChart(string CurrencyAcro)
        {
            await RequestApi(CurrencyAcro);
            JObject ApiResParsed = JObject.Parse(_ApiRespons);
            IEnumerable<JToken> _ApiResParsed = ApiResParsed.SelectTokens("rates.*.PLN");
            foreach(JToken item in _ApiResParsed)
                System.Diagnostics.Debug.WriteLine(item);

        }

       async Task RequestApi(string CurrencyName)
        {
            string startdate = DateTime.Now.ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.Subtract(new TimeSpan(10,0,0,0)).ToString("yyyy-MM-dd");
            _ApiRespons = await _client.GetStringAsync(Url + "/history?start_at=" + endDate + "&end_at="+ startdate +"&symbols=PLN&base=" + CurrencyName);
            System.Diagnostics.Debug.WriteLine(_ApiRespons);
        }
    }
}