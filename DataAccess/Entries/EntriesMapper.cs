using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ChampService.DataAccess 
{
    public class EntriesMapper : MapperBase<Entries>
    {
        protected override Entries Map(IDataRecord record)
        {
            Entries entries = new Entries();
            try
            {
                entries.EntriesID = (DBNull.Value == record["EntriesID"]) ?
                   0 : (int)record["EntriesID"];

                entries.EntryTime = (DBNull.Value == record["EntryTime"]) ?
                   new DateTime(0) : (DateTime)record["EntryTime"];

                entries.LastEntryTime = (DBNull.Value == record["LastEntryTime"]) ?
                   new DateTime(0) : (DateTime)record["LastEntryTime"];

                entries.SquareOfTime = (DBNull.Value == record["SquareOfTime"]) ?
                  new DateTime(0) : (DateTime)record["SquareOfTime"];

                entries.PriceLineRange = (DBNull.Value == record["PriceLineRange"]) ?
                   0 : Math.Ceiling(Convert.ToDecimal(record["PriceLineRange"]));

                entries.IsShorts = (DBNull.Value == record["IsShorts"]) ?
                    "" : (string)record["IsShorts"];

                entries.IsLongs = (DBNull.Value == record["IsLongs"]) ?
                   "" : (string)record["IsLongs"];

                entries.LongTakeProfit = (DBNull.Value == record["LongTakeProfit"]) ?
                     0 : Math.Ceiling(Convert.ToDecimal(record["LongTakeProfit"]));

                entries.LongStopLoss = (DBNull.Value == record["LongStopLoss"]) ?
                     0 : Math.Ceiling(Convert.ToDecimal(record["LongStopLoss"]));

                entries.ShortTakeProfit = (DBNull.Value == record["ShortTakeProfit"]) ?
                   0 : Math.Ceiling(Convert.ToDecimal(record["ShortTakeProfit"]));

                entries.ShortStopLoss = (DBNull.Value == record["ShortStopLoss"]) ?
                   0 : Math.Ceiling(Convert.ToDecimal(record["ShortStopLoss"]));

                entries.TradeType = (DBNull.Value == record["TradeType"]) ?
                    "" : (string)record["TradeType"];

                entries.LotSize = (DBNull.Value == record["LotSize"]) ?
                 0 : (int)record["LotSize"];

                entries.DateCreated = (DBNull.Value == record["DateCreated"]) ?
                   new DateTime(0) : (DateTime)record["DateCreated"];

                entries.AccountID = (DBNull.Value == record["AccountID"]) ?
                 0 : (int)record["AccountID"];
            }
            catch (Exception e)
            {
                throw e;
            }
            return entries;

        }
    }
}