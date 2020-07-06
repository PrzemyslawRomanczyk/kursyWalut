using kursy_walut.Models;
using kursy_walut.Persistence;
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
using System.Windows.Input;
using Xamarin.Forms;

namespace kursy_walut.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {

        private const string Url = "https://api.exchangeratesapi.io";
        private HttpClient _client = new HttpClient();
        public CurrencyC SelectedCurrency { get; set; }

        public ObservableCollection<CurrencyC> CurrencyList { get;set; }

        private String _ApiRespons;
        private SQLiteAsyncConnection _Connection;
        private readonly INavigation _navigation;
        public bool _IsRefreshing = false;
        public bool IsRefreshing
        {
            get { return _IsRefreshing; }
            set {
                _IsRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand DeleteElementCommand { get; private set; }


        //Constructor
        public HomeViewModel(INavigation navigation)
        {
            _navigation = navigation;
            DeleteElementCommand = new Command<CurrencyC>(Cur => DeleteElement(Cur));
        }

        //Bindable methods
        public void DeleteElement(CurrencyC currencylist)
        {
            CurrencyList.Remove(currencylist);
            _Connection.DeleteAsync(currencylist);
            OnPropertyChanged("CurrencyList");
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true;
                    RefreshList();
                    IsRefreshing = false;

                });
            }
        }
        async public void RefreshList()
        {
            if (CurrencyList != null)
            {
                foreach (var Name in CurrencyList)
                    await getCurrencyChangeRatio(Name.CurrencyName);
                System.Diagnostics.Debug.WriteLine("Refresh");
                OnPropertyChanged("CurrencyList");
            }
        }

        async public void GoToSettings()
        {
            await _navigation.PushAsync(new Views.Settings(_Connection));
        }


        //helper methods 
        async Task getCurrencyChangeRatio(string Currency)
        {
            await RequestApi(Currency);
            JObject RatesObj = JObject.Parse(_ApiRespons);
            double CurrentRate = Math.Round((double)RatesObj.SelectToken("rates.PLN"), 2);
            if (CurrencyList.Contains(CurrencyList.SingleOrDefault(x => x.CurrencyName == Currency)))
            {
                CurrencyList.SingleOrDefault(x => x.CurrencyName == Currency).CurrencyRatio = CurrentRate;
                System.Diagnostics.Debug.WriteLine("Update123");
                System.Diagnostics.Debug.WriteLine(Currency + " " + CurrentRate);
                OnPropertyChanged("CurrencyList");
            }
            else
            {
                CurrencyList.Add(new CurrencyC { CurrencyName = Currency, CurrencyRatio = CurrentRate });
                await _Connection.InsertAsync(new CurrencyC { CurrencyName = Currency, CurrencyRatio = CurrentRate });
                System.Diagnostics.Debug.WriteLine("Create123");
                OnPropertyChanged("CurrencyList");
            }
        }
        async Task RequestApi(string CurrencyName)
        {
            _ApiRespons = await _client.GetStringAsync(Url + "/latest?symbols=PLN&base=" + CurrencyName);
        }
        async public void ConnectDB()
        {
            _Connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            await _Connection.CreateTableAsync<CurrencyC>();
            var CurrentCurrencyDB = await _Connection.Table<CurrencyC>().ToListAsync();
            CurrencyList = new ObservableCollection<CurrencyC>(CurrentCurrencyDB);

            if (CurrentCurrencyDB.Any())
            {
                foreach (var Name in CurrencyList)
                    await getCurrencyChangeRatio(Name.CurrencyName);
            }
            OnPropertyChanged("CurrencyList");

        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}