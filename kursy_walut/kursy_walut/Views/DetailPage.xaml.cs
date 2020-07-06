using kursy_walut.Models;
using kursy_walut.ViewModels;
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

        public DetailPage(CurrencyC SelectedCurrencyObj, SQLiteAsyncConnection Connection, HttpClient Client)
        {
            this.BindingContext = new DetailPageViewModel(SelectedCurrencyObj, Connection, Client);
            InitializeComponent();
        }

        async protected override void OnAppearing()
        {
            base.OnAppearing();
        }


    }
}