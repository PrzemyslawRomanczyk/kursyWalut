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
        private List<Ratio> ListRatio;

        public class Rates
        {
            public  Ratio RatesObj { get; set; }
            public string Currency { get; set; }
            public DateTime Date { get; set; }
        }
        public class Ratio
        {
            public string Base { get; set; }
            public double CurrentRatio { get; set; }
        }
        protected override async void OnAppearing()
        {
            var content = await _client.GetStringAsync(Url + "/latest?symbols=PLN");
            //System.Diagnostics.Debug.WriteLine("Content :" + content);
            var content2 = content.Remove(0,9);
            //System.Diagnostics.Debug.WriteLine("Content :" + content2);
            var content3 = content2.Remove(14);
            //System.Diagnostics.Debug.WriteLine("Content :" + content3);
            //var ParsedContent = JsonConvert.DeserializeObject<Ratio>(content3);
            //System.Diagnostics.Debug.WriteLine("Currency :" + ParsedContent);
            //JObject Jobj = JObject.Parse(content);
            //string Curency = (string)Jobj.SelectToken("base");
            //System.Diagnostics.Debug.WriteLine("Currency :" + Curency);
            //string Rates2 = (string)Jobj.SelectToken("rates");
            JObject RatesObj = JObject.Parse(content3);
            double CurrenyRate = (double)RatesObj.SelectToken("PLN");
            System.Diagnostics.Debug.WriteLine("Currency :" + CurrenyRate);
            base.OnAppearing();
        }

        public HomePage()
        {
            InitializeComponent();

            _Waluty = GetWaluty();
            listView.ItemsSource = _Waluty;

        }

        ObservableCollection<Waluty> GetWaluty()
        {
            if (DownloadedRates == 0)
            {
                _Waluty = new ObservableCollection<Waluty>
                    { new Waluty { NazwaWaluty = "Euro", KursWaluty = /*DownloadedRates.RatesDict["PLN"]*/ 2.0} };
            }
            else
            {
                _Waluty = new ObservableCollection<Waluty> {
                new Waluty {NazwaWaluty = "Euro", KursWaluty=4.55},
                new Waluty {NazwaWaluty = "Dolar", KursWaluty=4.12},
                new Waluty {NazwaWaluty = "Jen", KursWaluty=0.01}};
            }
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
