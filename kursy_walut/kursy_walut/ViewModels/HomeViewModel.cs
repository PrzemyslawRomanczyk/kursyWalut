using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace kursy_walut.ViewModels
{
    public class HomeViewModel : ContentView
    {
        private readonly INavigation _navigation;

        public HomeViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }
    }
}