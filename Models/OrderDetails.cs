using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChampService.Models
{
    public class OrderDetails
    {
        public string Exchange;
        public string TradingSymbol;
        public string TransactionType;
        public int Quantity;
        public decimal Price;
        public string Product;
        public string OrderType;            
    }
}