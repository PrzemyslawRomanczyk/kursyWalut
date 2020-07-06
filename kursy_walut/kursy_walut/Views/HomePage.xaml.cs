using kursy_walut.Models;
using kursy_walut.ViewModels;
using System;
 using System.ComponentModel;
using Xamarin.Forms;

namespace kursy_walut.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class HomePage : ContentPage
    {
        protected override void OnAppearing()
        {
            (BindingContext as HomeViewModel).RefreshList();
            (BindingContext as HomeViewModel).ConnectDB();
            base.OnAppearing();
        }

        public HomePage()
        {
            BindingContext = new HomeViewModel(Navigation);
            InitializeComponent();
            
        }

        async private void HandleSelection(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new Views.DetailPage());
        }

        async private  void RefreshList(object sender, EventArgs e)
        {
            (BindingContext as HomeViewModel).RefreshList();
        }
        async private void GoToSettings(object sender, EventArgs e)
        {
            (BindingContext as HomeViewModel).GoToSettings();
        }
    }
}
