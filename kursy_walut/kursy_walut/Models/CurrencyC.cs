using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace kursy_walut.Models
{
    [Table("CurrencyCurrentRatio")]
    public class CurrencyC
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public double CurrencyRatio { get; set; }
    }
}
