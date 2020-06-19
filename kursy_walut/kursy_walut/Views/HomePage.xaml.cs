using kursy_walut.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        private ObservableCollection<Waluty> _Waluty;
        public HomePage()
        {
            InitializeComponent();

            _Waluty = GetWaluty();
            listView.ItemsSource = _Waluty;

            //BindingContext = new ViewModels.HomeViewModel(Navigation);
        }

        ObservableCollection<Waluty> GetWaluty()
        {
            _Waluty = new ObservableCollection<Waluty> {
                new Waluty {NazwaWaluty = "Euro", KursWaluty=4.55},
                new Waluty {NazwaWaluty = "Dolar", KursWaluty=4.12},
                new Waluty {NazwaWaluty = "Jen", KursWaluty=0.01}
            };
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
