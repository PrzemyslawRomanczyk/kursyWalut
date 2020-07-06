
using kursy_walut.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQLite;
using kursy_walut.ViewModels;

namespace kursy_walut.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Settings : ContentPage
    {
        public Settings(SQLiteAsyncConnection _Connection)
        {
            InitializeComponent();
            BindingContext = new SettingsViewModel(_Connection);
        }

        protected override void OnAppearing()
        {
            //(BindingContext as SettingsViewModel).TrimPicker();
        }

        private void Picker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //(BindingContext as SettingsViewModel).PickerSelect();
        }
    }
}