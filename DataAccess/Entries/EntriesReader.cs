using ChampService.Models;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TrueData.Velocity;
using TrueData.Velocity.External;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Status;

namespace ChampService.DataAccess
{
    public class EntriesReader : ObjectReaderWithConnection<Entries>

    {
        TrueDataExternal api = null;
        public EntriesReader() { }
        public EntriesReader(string cmdText)
        {
            CommandText = cmdText;
        }
        protected override CommandType CommandType 
        {
            get { return System.Data.CommandType.Text; }
        }

        protected override MapperBase<Entries> GetMapper()
        {
            MapperBase<Entries> mapper = new EntriesMapper();
            return mapper;
        }

        protected override Collection<IDataParameter> GetParameters(IDbCommand command)
        {
            Collection<IDataParameter> collection = new Collection<IDataParameter>();
            return collection;
        }



        public ProcedureResult Entries(Entries entries)
        {

            string queryString = "exec PriceUpAndDownRanges '" + 0.003819 + "','"+ 24930.9 +"','"+entries.EntryTime+"','"+entries.LastEntryTime+"','"+entries.SquareOfTime+"','" +entries.IsShorts+ "','" +entries.IsLongs+"','"+entries.LongTakeProfit+"','"+entries.LongStopLoss+"','"+entries.ShortTakeProfit+"','"+entries.ShortStopLoss+"','"+entries.TradeType+"','"+entries.AccountID+"' ";
            SqlExecute ex = new SqlExecute();
            SqlDataReader reader = ex.runCommand(queryString);
            ProcedureResult procedureResult = new ProcedureResult();
            if (reader.Read())
            {
                procedureResult.isSuccess = reader.GetInt32(0);
                procedureResult.FailureReason = reader.IsDBNull(1) ? null : reader.GetString(1);
            }

            return procedureResult;
        }

        public static void EntrieCreate(Entries entries)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();


            sb.Append("exec PriceUpAndDownRanges '" + entries.PriceLineRange + "','" + 2.0 + "','" +
                entries.EntryTime.ToString("HH:mm:ss") + "','" + entries.LastEntryTime.ToString("HH:mm:ss") + "','" +
                entries.SquareOfTime.ToString("HH:mm:ss") + "','" + entries.IsShorts + "','" + entries.IsLongs + "','" +
                entries.LongTakeProfit + "','" + entries.LongStopLoss + "','" + entries.ShortTakeProfit + "','" + 
                entries.ShortStopLoss + "','" +entries.LotSize+"','"+ entries.TradeType + "','" + entries.AccountID + "' ");

            EntriesReader reader = new EntriesReader(sb.ToString());
            reader.ExecuteNonQuery();
        }

    }
}