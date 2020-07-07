using kursy_walut.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace kursy_walut.ViewModels
{
    class DetailPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<HistoricalRatio> Data { get; set; }
        public ObservableCollection<HistoricalRatio> _data { get; set; }
        private CurrencyC CurrentCurrency;
        private HttpClient _client = new HttpClient();
        private String _ApiRespons;
        private const string Url = "https://api.exchangeratesapi.io";

        public event PropertyChangedEventHandler PropertyChanged;


        public DetailPageViewModel(CurrencyC SelectedCurrencyObj, SQLiteAsyncConnection Connection, HttpClient Client)
        {
            CurrentCurrency = SelectedCurrencyObj;
            _client = Client;
            Data = new ObservableCollection<HistoricalRatio>() { };
            CreateDataForChart(CurrentCurrency.CurrencyName);
            OnPropertyChanged();

        }
        async private void CreateDataForChart(string CurrencyAcro)
        {
            for (int i = 20; i > 0; i--)
            {
                string endDate = DateTime.Now.Subtract(new TimeSpan(i,0,0,0)).ToString("yyyy-MM-dd");
                string startDate = DateTime.Now.Subtract(new TimeSpan(i-1, 0, 0, 0)).ToString("yyyy-MM-dd");
                await RequestApi(CurrencyAcro, startDate, endDate);
                JObject ApiResParsed = JObject.Parse(_ApiRespons);
                JToken Ratio = CheckIfEmpty(ApiResParsed);
                if (Ratio != null)
                {
                    System.Diagnostics.Debug.WriteLine("Date" + endDate + " " + Ratio.ToString());
                    Data.Add(new HistoricalRatio { Date = endDate, Ratio = Math.Round(Convert.ToDouble(Ratio),4) });
                    OnPropertyChanged();
                }
            }

        }

        private JToken CheckIfEmpty(JObject parsedInput)
        {
            try
            {
                IEnumerable<JToken> Output = parsedInput.SelectTokens("rates.*.PLN", errorWhenNoMatch: true);
                System.Diagnostics.Debug.WriteLine("Out123 :" + Output.Count());
                if (Output.Count() == 0)
                    return null;
                else
                    return Output.First();
            }
            catch (JsonException)
            {
                System.Diagnostics.Debug.WriteLine("Desired object not found");
                return null;
            }
        }

        async Task RequestApi(string CurrencyName, String startDate, String endDate)
        { 
            _ApiRespons = await _client.GetStringAsync(Url + "/history?start_at=" + endDate + "&end_at=" + startDate + "&symbols=PLN&base=" + CurrencyName);
            System.Diagnostics.Debug.WriteLine(Url + "/history?start_at=" + startDate + "&end_at=" + endDate + "&symbols=PLN&base=" + CurrencyName);
            System.Diagnostics.Debug.WriteLine("APIRes" + _ApiRespons);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
