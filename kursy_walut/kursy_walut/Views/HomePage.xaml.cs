using kursy_walut.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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

        private ObservableCollection<Waluty> _Waluty;
        private ObservableCollection<Rates> _Rates;
        private Double DownloadedRates; 

        public class Rates
        {
            public Dictionary<string, double> RatesDict { get; set; }
            public string Currency { get; set; }
            public string Date { get; set; }
        }
        protected override async void OnAppearing()
        {
            var content = await _client.GetStringAsync(Url + "/latest??symbols=PLN");
            var ParsedContent = JsonConvert.DeserializeObject<Rates>(content);
            DownloadedRates = ParsedContent.RatesDict["PLN"];
            _Rates = new ObservableCollection<Rates>(ParsedContent);
            base.OnAppearing();
        }

        public HomePage()
        {
            InitializeComponent();

            _Waluty = GetWaluty();
            listView.ItemsSource = _Waluty;

            //BindingContext = new ViewModels.HomeViewModel(Navigation);
        }

        ObservableCollection<Waluty> GetWaluty()
        {
            _Waluty = new ObservableCollection<Waluty>
            { new Waluty { NazwaWaluty = "Euro", KursWaluty = /*DownloadedRates.RatesDict["PLN"]*/ 1.23 } };

            return _Waluty;
        }

        private void DeleteElement(object sender, EventArgs e)
        {
            var Element = (sender as MenuItem).CommandParameter as Waluty;
            _Waluty.Remove(Element);

        }

        async private void HandleSelection(object sender, SelectedItemChangedEventArgs e)
        {
            var Kurs = e.SelectedItem as Waluty;
            await Navigation.PushAsync(new Views.DetailPage());
        }

        private void RefreshList(object sender, EventArgs e)
        {
            listView.ItemsSource = GetWaluty();
            listView.EndRefresh();
        }
    }
}
