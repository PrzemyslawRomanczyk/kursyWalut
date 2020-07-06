using kursy_walut.Models;
using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace kursy_walut.ViewModels
{
    class DetailPageViewModel
    {
        public List<HistoricalRatio> Data { get; set; }

        private SQLiteAsyncConnection _ConnectionSet;
        private CurrencyC CurrentCurrency;
        private HttpClient _client = new HttpClient();
        private String _ApiRespons;
        private const string Url = "https://api.exchangeratesapi.io";

        public DetailPageViewModel(CurrencyC SelectedCurrencyObj, SQLiteAsyncConnection Connection, HttpClient Client)
        {
            _ConnectionSet = Connection;
            CurrentCurrency = SelectedCurrencyObj;
            _client = Client;

            Data = new List<HistoricalRatio>()
            {
                new HistoricalRatio{Date = "2020-07-06", Ratio=4.1},
                new HistoricalRatio{Date = "2020-07-05", Ratio=4.2},
                new HistoricalRatio{Date = "2020-07-04", Ratio=4.3},
                new HistoricalRatio{Date = "2020-07-03", Ratio=4.4}
            };
        }
        async private void CreateDataForChart(string CurrencyAcro)
        {
            await RequestApi(CurrencyAcro);
            JObject ApiResParsed = JObject.Parse(_ApiRespons);
            IEnumerable<JToken> _ApiResParsed = ApiResParsed.SelectTokens("rates.*.PLN");
            foreach (JToken item in _ApiResParsed)
                System.Diagnostics.Debug.WriteLine(item);

        }

        async Task RequestApi(string CurrencyName)
        {
            string startdate = DateTime.Now.ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0)).ToString("yyyy-MM-dd");
            _ApiRespons = await _client.GetStringAsync(Url + "/history?start_at=" + endDate + "&end_at=" + startdate + "&symbols=PLN&base=" + CurrencyName);
            System.Diagnostics.Debug.WriteLine(_ApiRespons);
        }
    }
}
