using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace kursy_walut
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjgwNjIyQDMxMzgyZTMxMmUzMExHbUNqYk9EU2FKanZCWFhPblRJMzc2QjhsZVBpSk1LSEJBTnZJUWdHTEU9");
            InitializeComponent();

            MainPage = new NavigationPage(new Views.HomePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
