using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace kursy_walut.Models
{
    class SettingsPicker
    {
        public int Id { get; set; }
        public string CurrencyOptionName { get; set;}

        public List<SettingsPicker> GetSettingsPickers()
        {
            return new List<SettingsPicker>
            { 
                new SettingsPicker{Id = 0, CurrencyOptionName="CAD"},
                new SettingsPicker{Id = 1, CurrencyOptionName="HKD"},
                new SettingsPicker{Id = 2, CurrencyOptionName="ISK"},
                new SettingsPicker{Id = 3, CurrencyOptionName="PHP"},
                new SettingsPicker{Id = 4, CurrencyOptionName="DKK"},
                new SettingsPicker{Id = 5, CurrencyOptionName="HUF"},
                new SettingsPicker{Id = 6, CurrencyOptionName="CZK"},
                new SettingsPicker{Id = 7, CurrencyOptionName="AUD"},
                new SettingsPicker{Id = 8, CurrencyOptionName="SEK"},
                new SettingsPicker{Id = 9, CurrencyOptionName="INR"},
                new SettingsPicker{Id = 10, CurrencyOptionName="BRL"},
                new SettingsPicker{Id = 11, CurrencyOptionName="RUB"},
                new SettingsPicker{Id = 12, CurrencyOptionName="HRK"},
                new SettingsPicker{Id = 13, CurrencyOptionName="JPY"},
                new SettingsPicker{Id = 14, CurrencyOptionName="THB"},
                new SettingsPicker{Id = 15, CurrencyOptionName="CHF"},
                new SettingsPicker{Id = 16, CurrencyOptionName="BGN"},
                new SettingsPicker{Id = 17, CurrencyOptionName="TRY"},
                new SettingsPicker{Id = 18, CurrencyOptionName="CNY"},
                new SettingsPicker{Id = 19, CurrencyOptionName="NOK"},
                new SettingsPicker{Id = 20, CurrencyOptionName="NZD"},
                new SettingsPicker{Id = 21, CurrencyOptionName="ZAR"},
                new SettingsPicker{Id = 22, CurrencyOptionName="USD"},
                new SettingsPicker{Id = 23, CurrencyOptionName="MXN"},
                new SettingsPicker{Id = 24, CurrencyOptionName="ILS"},
                new SettingsPicker{Id = 25, CurrencyOptionName="GBP"},
                new SettingsPicker{Id = 26, CurrencyOptionName="KRW"},
                new SettingsPicker{Id = 27, CurrencyOptionName="MYR"},
                new SettingsPicker{Id = 28, CurrencyOptionName="EUR"},
                new SettingsPicker{Id = 29, CurrencyOptionName=""},
            };
        }
    }
}
