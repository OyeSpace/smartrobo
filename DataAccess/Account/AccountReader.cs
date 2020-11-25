using ChampService.FunObjects;
using ChampService.Models;
using nimbleStream.DataStream;
using NimbleStreamWinFormExample;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.Net.Sockets;

namespace ChampService.DataAccess
{
   public class AccountReader : ObjectReaderWithConnection<Account>
    {
       public bool Disposing { get; }
       public bool InvokeRequired { get; }
      // public object Invoke(Delegate method);
        public AccountReader() { }
      
        public AccountReader(string cmdText)
        {
            CommandText = cmdText;
        }

        protected override CommandType CommandType
        {
            get { return System.Data.CommandType.Text; }
        }

        protected override MapperBase<Account> GetMapper()
        {
            MapperBase<Account> mapper = new AccountMapper();
            return mapper;
        }

        protected override Collection<IDataParameter> GetParameters(IDbCommand command)
        {
            Collection<IDataParameter> collection = new Collection<IDataParameter>();
            return collection;

     
        }

        public ProcedureResult AccountCreate(string mobileNum)
        {

            string queryString = "exec prAccountCreation" + " '" + mobileNum + "' ";
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


        public static string EstablishConnection(nimbleStreamClient.Client client)
        {
            try
            {
                if (CheckParametersValue())
                {
                    client.ConnectAsync("nimblestream.lisuns.com", 4525);
                   
                 //   ApiObject.InitClient();
                }
            }
            catch(SocketException ex)
            {
                throw ex;
            }

            return "Connection Established";
        }

     
        public static bool CheckParametersValue()
        {
             
            var ret = true;
            //foreach (ctlParameter ctl in pnlParameters.Controls)
            //{
            //    if (!ctl.Check())
            //    {
            //        ret = false;
            //        break;
            //    }
            //}
            return ret;
        }







        #region publics
        public static string AuthenticateConnection(nimbleStreamClient.Client client)
        {
            try
            {
                client.Authenticate("b016c6e2-fffc-47d5-81b5-4835f2b4d49b");
                // client.SubscribeRealtime("NFO", "NIFTY-I",false);
                client.GetLastQuote("NFO", "BANKNIFTY-I");

              //  ApiObject.InitClient();

             
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return "Authenticated";
        }

        #endregion

       
        //public void client_LastQuoteResult(object sender, DS_LastQuoteResult value)
        //{
           
        //    UpdateLog(string.Format("\t{0}\t{1}\t{2}\t{3}", value.InstrumentIdentifier, value.LastTradeTime, value.LastTradePrice, value.LastTradeQty), false);
        //}


        private System.Windows.Forms.TextBox txtResults;
        public void UpdateLog(string value, bool showTimeStamp = true)
        {
            if (!Disposing)
            {
                if (this.InvokeRequired)
                {
                    Invoke((MethodInvoker)(() =>
                    {
                        UpdateLog(value, showTimeStamp);
                    }));
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtResults.Text))
                        txtResults.AppendText("\r\n");

                    if (showTimeStamp)
                    
                        Console.WriteLine(string.Format("{0}\t{1}", DateTime.Now, value));
                    txtResults.AppendText(string.Format("{0}\t{1}", DateTime.Now, value));



                    txtResults.SelectionStart = txtResults.TextLength;
                    txtResults.ScrollToCaret();

                }
            }
        }

        private void Invoke(MethodInvoker methodInvoker)
        {
          
        }
    }

}
