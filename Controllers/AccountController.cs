using ChampService.DataAccess;
using ChampService.FunObjects;
using ChampService.Models;
using ChampService.TrueData;
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
using System.Threading.Tasks;
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
        [Route("v1/SetToken/{RequestToken}")]
        [Route("SetToken/{RequestToken}")]
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
        public async Task<bool> PlaceOrder(string token)
        {

            ApiObject.InitClient();

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(1);

            var timer = new System.Threading.Timer((e) =>
            {
                GetLastTradePrice();
            }, null, startTimeSpan, periodTimeSpan);

            //// TrueObjects.InitializeComponent();
            //string myAPIKey = "m49ujqztaounoep5";
            //Kite kite = new Kite(myAPIKey);
            //kite.SetAccessToken(token);

            //var startTimeSpan = TimeSpan.Zero;
            //var periodTimeSpan = TimeSpan.FromMilliseconds(1);

            //var timer = new System.Threading.Timer((e) =>
            //{
            //    GetLastTradePrice();
            //}, null, startTimeSpan, periodTimeSpan);

            //try
            //{

            //    //   var order_id = kite.PlaceOrder("NSE", "ACC", "BUY", 1, 0, "MIS", "MARKET");

            //    //   Request.CreateResponse(order_id);


            //}
            //catch (Exception e)
            //{
            //    Trace.WriteLine(e.Message);
            //}

            return true;

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




        public  async Task<bool> GetLastTradePrice()
        {
            ApiObject.ApplyFunctionAsync(client, "GetLastQuote");
            ApiObject.InitClient();
            return true;
       }







        //var startTimeSpan = TimeSpan.Zero;
        //var periodTimeSpan = TimeSpan.FromSeconds(1);

        //var timer = new System.Threading.Timer((e) =>
        //{
        //    MyMethod();
        //}, null, startTimeSpan, periodTimeSpan);




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

        //   working - var order_id = kite.PlaceOrder("NSE", "ACC", "BUY", 1, 0, "MIS", "MARKET");
        // symbol(INFY)year(19)month(OCT)strike(650)type(CE / PE).
    }

}
