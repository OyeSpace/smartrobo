using ChampService.DataAccess;
using ChampService.FunObjects;
using ChampService.Models;
using ChampService.Utils;
using KiteConnect;
using nimbleStream.DataStream;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.UI.DataVisualization.Charting;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TrueData.Velocity;
using TrueData.Velocity.External;

namespace Trading.Controllers
{
      [RoutePrefix("trading/api")] 
    public class AccountController : ApiController
    {
        APISuccessResponseMessage<Account> response = new APISuccessResponseMessage<Account>(null);
        private HttpResponseMessage SendMessageResponse(HttpStatusCode statuscode, APIResponseMessage msg)
        {
            return Request.CreateResponse(statuscode, msg);
        }

        [HttpPost]
        [Route("v1/AccountCreate")]
        [Route("AccountCreate")]
        public HttpResponseMessage AccountCreate([FromBody] Account account)
        {


            AccountReader reader = new AccountReader();
            ProcedureResult procedureResult = reader.AccountCreate(account.MobileNumber);


            if (procedureResult.isSuccess == 1)
            {
                ErrorResponse error = new ErrorResponse();
                error.code = 3000;
                error.message = procedureResult.FailureReason;
                return SendMessageResponse(HttpStatusCode.OK, new APIFailureResponseMessage<ErrorResponse>(error));

            }
            else
            {
                ErrorResponse error = new ErrorResponse();
                error.code = 3000;
                error.message = procedureResult.FailureReason;
                return SendMessageResponse(HttpStatusCode.OK, new APISuccessResponseMessage<ErrorResponse>(error));
            }

        }
        public bool Disposing { get; }
        public bool InvokeRequired { get; }


        public nimbleStreamClient.Client client;

        [HttpGet]
        [Route("v1/ConnectionEstablishment/{fun}")]
        [Route("ConnectionEstablishment/{fun}")]
        public void ConnectionEstablishment(string fun)
        {

            if (fun == "ConnectAsync")
            {
                ApiObject.InitClient();
            }

            ApiObject.ApplyFunctionAsync(client, fun);



            // return SendMessageResponse(HttpStatusCode.OK, new APIFailureResponseMessage<ErrorResponse>(null));
        }
        [HttpGet]
        [Route("v1/RequestToken/{RequestToken}")]
        [Route("RequestToken/{RequestToken}")]
        public HttpResponseMessage RequestToken(string RequestToken)
        {
           
            string myAPIKey = "m49ujqztaounoep5";

            Kite kite = new Kite(myAPIKey);

            var url = kite.GetLoginURL();

            string mySecret = "ohc4e350gr4tid9bwl6njk18h685du8s";
            var data  = kite.GenerateSession(RequestToken, mySecret);
            string access_token = data.AccessToken;

            kite.SetAccessToken(access_token);
           // var order_id = kite.PlaceOrder("NSE", "INFY", "BUY", 1,1,"MIS","MARKET");

            Trace.WriteLine("Token " + access_token);       
          
            response.AddDataMember("Token",access_token);
            return SendMessageResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [Route("v1/PlaceOrder/{Token}")]
        [Route("PlaceOrder/{Token}")]
        public void PlaceOrder(string token)
        {

            string myAPIKey = "m49ujqztaounoep5";

            Kite kite = new Kite(myAPIKey);

            //var url = kite.GetLoginURL();

            //  ivr = new ivr { associationID = associationId, visitorLogId = visitorLogId, status = typeOfVisit + " Approved", environment = env };

            //'Intraday orders(MIS) are allowed only till 3.20 PM. Try placing a CNC order

            kite.SetAccessToken(token);
            try
            {
              //  Ticker ticker = new Ticker(myAPIKey, token);

              //  // Add handlers to events
              //  ticker.OnTick += onTick;
              //  ticker.OnOrderUpdate += OnOrderUpdate;
              //  //ticker.OnReconnect += onReconnect;
              //  //ticker.OnNoReconnect += oNoReconnect;
              //  //ticker.OnError += onError;
              //  //ticker.OnClose += onClose;
              //  //ticker.OnConnect += onConnect;

              //  // Engage reconnection mechanism and connect to ticker
              //  ticker.EnableReconnect(Interval: 5, Retries: 50);
              //  ticker.Connect();
               
              //  // Disconnect ticker before closing the application
              ////  ticker.Close();

              //  // Subscribing to NIFTY50 and setting mode to LTP
              //  ticker.Subscribe(Tokens: new UInt32[] { 11667970 });
              //  ticker.SetMode(Tokens: new UInt32[] { 11667970 }, Mode: Constants.MODE_LTP);

               

                var order_id = kite.PlaceOrder("NSE", "ACC", "BUY", 1, 0, "MIS", "MARKET");

               // kite.GetOrders();

            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
            //OrderDetails orders = new OrderDetails();

            //orders = new OrderDetails { Exchange = "NSE", TradingSymbol = "INFY", TransactionType = "BUY", Quantity = 1, Price = 2, Product = "MIS", OrderType = "MARKET" };


            //Trace.WriteLine("OrderID " + orders);


        }

        // Example onTick handler
        static void onTick(Tick TickData)
        {
            Console.WriteLine("LTP: " + TickData.LastPrice);
        }
        static void OnOrderUpdate(Order OrderData)
        {
            Console.WriteLine("OrderUpdate " + Utils.JsonSerialize(OrderData));
        }

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
                    //if (!string.IsNullOrEmpty(txtResults.Text))
                    //    txtResults.AppendText("\r\n");

                    if (showTimeStamp)

                        Trace.WriteLine(string.Format("{0}\t{1}", DateTime.Now, value));
                   // txtResults.AppendText(string.Format("{0}\t{1}", DateTime.Now, value));



                 //   txtResults.SelectionStart = txtResults.TextLength;
                 
                }
            }
        }

        private void Invoke(MethodInvoker methodInvoker)
        {
        
        }
    }

}
