using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChampService.DataAccess
{
    public class Entries
    {
        public int EntriesID { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime LastEntryTime { get; set; }
        public DateTime SquareOfTime { get; set; }
        public decimal PriceLineRange { get; set; }
        public string IsShorts { get; set; }
        public string IsLongs { get; set; }
        public decimal LongTakeProfit { get; set; }
        public decimal LongStopLoss { get; set; }
        public decimal ShortTakeProfit { get; set; }
        public decimal ShortStopLoss { get; set; }
        public string TradeType { get; set; }
        public int LotSize { get; set; }
        public DateTime DateCreated { get; set; }
        public int AccountID { get; set; }

    }
}