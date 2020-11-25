using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChampService.DataAccess;
using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;
using nimbleStream.DataStream;
using nimbleStreamClient;

namespace ChampService.FunObjects
{
    public class ApiObject
    {
       
        #region const

        public const string FUN_CONNECTASYNC = "ConnectAsync";
        public const string FUN_DISCONNECT = "Disconnect";
        public const string FUN_AUTHENTICATE = "Authenticate";
        public const string FUN_GETEXCHANGES = "GetExchanges";
        public const string FUN_GETHISTORY = "GetHistory";
        public const string FUN_GETINSTRUMENTS = "GetInstruments";
        public const string FUN_GETLASTEXCHANGESMESSAGE = "GetLastExchangesMessage";
        public const string FUN_GETLASTMARKETMESSAGE = "GetLastMarketMessage";
        public const string FUN_GETLASTQUOTE = "GetLastQuote";
        public const string FUN_GETLASTQUOTEARRAY = "GetLastQuoteArray";
        public const string FUN_GETLIMITATION = "GetLimitation";
        public const string FUN_SUBSCRIBEREALTIME = "SubscribeRealtime";
        public const string FUN_SUBSCRIBESNAPSHOT = "SubscribeSnapshot";

        #endregion //const

        #region members
        private List<ApiFunction> _function;
        private readonly ApiObject _functions = new ApiObject();

        #endregion //Members
        private static Client _client;
        public static double Values { get; private set; }

        public List<ApiFunction> GetFunctions()
        {
            if(_function == null)
            
                InitFunctions();
                return _function;
            
        }
        #region privates

        private void InitFunctions()
        {
            
            _function = new List<ApiFunction>();

            _function.Add(FunConnectAsync());
          //  _functions.Add(FunDisconnect());
            _functions.Add(FunAuthenticate());
            //_functions.Add(FunGetexchanges());
            //_functions.Add(FunGethistory());
            //_functions.Add(FunGetinstruments());
            //_functions.Add(FunGetlastexchangesmessage());
            //_functions.Add(FunGetlastmarketmessage());
            _function.Add(FunGetlastquote());
            //_functions.Add(FunGetlastquotearray());
            //_functions.Add(FunGetlimitation());
            //_functions.Add(FunSubscriberealtime());
            //_functions.Add(FunSubscribeSnapshot());
        }

        private void Add(ApiFunction apiFunction)
        {

        }

        private ApiFunction FunGetlastquote()
        {
            return new ApiFunction
            {
                Name = FUN_GETLASTQUOTE,
                ExtName = "GetLastQuote(exchange, instrumentIdentifier)",
                Parameters = new List<Parameter> {new Parameter { Name = "exchange", Default = "NFO", Description = "Name of supported exchange. To get list of supported exchanges you can use 'GetExchanges' function", Type = typeof(string) },
                                                  new Parameter { Name = "instrumentIdentifier", Default = "NIFTY-I", Description = "Value of instrument identifier", Type = typeof(string) } },
                Description = "Get last quote as single request.\r\nResponse in 'LastQuoteResult' event."
            };
        }

        private ApiFunction FunConnectAsync()
        {
            return new ApiFunction
            {
                Name = FUN_CONNECTASYNC,
                ExtName = "ConnectAsync(host, port)",
                Parameters = new List<Parameter> { new Parameter { Name = "host", Default = "localhost", Description = "endpoint name, e.g localhost, 127.0.0.1, etc.", Type = typeof(string) },
                                                   new Parameter { Name = "port", Default = "4525", Description = "endpoint TCP/IP port, e.g 4525, etc.", Type = typeof(int) }},
                Description = "Use this function to establish connection to server. To heave response you should subscribe on 'BeginConnect', 'ConnectFailed', 'Reconnecting', 'ConnectComple' events"
            };
        }

        private ApiFunction FunAuthenticate()
        {
            return new ApiFunction
            {
                Name = "Authenticate",
                ExtName = "Authenticate(password)",
                Parameters = new List<Parameter> { new Parameter { Name = "password", Default = "<API-KEY>", Description = "Access key", Type = typeof(string) } },
                Description = "Use this function after recived answer that connection success in 'ConnectComple' event. To have answer about Authenticate result you should subscribe on AuthenticateResult event"
            };
        }
        public static void IsConnected()
        {
            Socket s = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);

            Console.WriteLine("Establishing Connection to {0}",
                "nimblestream.lisuns.com");
            s.Connect("nimblestream.lisuns.com", 4525);
            Console.WriteLine("Connection established");
        }

        public static void ApplyFunctionAsync(nimbleStreamClient.Client client, string fun)
        {
            switch (fun)
            {
                case "ConnectAsync":
                    _client.ConnectAsync("nimblestream.lisuns.com", 4525);                
                    break;
              
                case "Authenticate":
                    _client.Authenticate("b016c6e2-fffc-47d5-81b5-4835f2b4d49b");
                    break;
              
                case "GetLastQuote":
                    _client.GetLastQuote("NFO", "BANKNIFTY-I");
                   // InitClient();
                    break;
                case "Exchanges":
                    _client.GetExchanges();
                    break;

                        default: break;
                    
            }
          
        }

        #endregion privates


     
       

      
        public static void InitClient() 
        {
            if (_client != null)
            {

                _client.BeginConnect -= client_BeginConnect;
                _client.ConnectFailed -= client_ConnectFailed;
                _client.ConnectComple -= client_ConnectComple;
                _client.AuthenticateResult -= client_AuthenticateResult;
                _client.LastQuoteResult -= client_LastQuoteResult;
                _client.LastExchangeMessagesItemResult -= client_LastExchangeMessagesItemResult;
                _client.ExchangesResult -= client_ExchangesResult;

                _client.RequestErrorResult -= client_RequestErrorResult;
              
               
            }

            _client = new Client(false);
            _client.BeginConnect += client_BeginConnect;
            _client.ConnectFailed += client_ConnectFailed;
            _client.ConnectComple += client_ConnectComple;
            _client.AuthenticateResult += client_AuthenticateResult;
            _client.ExchangesResult += client_ExchangesResult;
            _client.RequestErrorResult += client_RequestErrorResult;
          
            _client.LastQuoteResult += client_LastQuoteResult;
            _client.LastExchangeMessagesItemResult += client_LastExchangeMessagesItemResult;

        }

        public static void client_BeginConnect(object sender, EventArgs e)
        {
            // UpdateLog("Connection Started.");
            Trace.WriteLine("connection Started");
        }

        public static void client_ConnectFailed(object sender, EventArgs e)
        {
            Trace.WriteLine("connection failed. Check Connection IP and Port");
        }

        public static void client_RequestErrorResult(object sender, DS_RequestError value)
        {
            Trace.WriteLine("REquest");
        }

        public static void client_ConnectComple(object sender, EventArgs e)
        {
            Trace.WriteLine("Connection Complete. Ready to accept 'Authenticate request");
        }


        public static void client_AuthenticateResult(object sender, DS_AuthenticateResult value)
        {
            if (value.Complete)
                Trace.WriteLine("Authenticated");
            else
                Trace.WriteLine("Authentication failed with message: " + value.Message);

        }

        public static void client_LastQuoteResult(object sender, DS_LastQuoteResult value)
        {
            //Values = value.LastTradePrice;
            
            Trace.WriteLine("CurrentDayOpen " + value.Open);
            Trace.WriteLine("Last ClosePrice " + value.LastTradePrice);
           
         }
        private static void client_ExchangesResult(object sender, DS_ExchangesResult value)
        {
              foreach (var itm in value.Result)
                Trace.WriteLine("exchange value "+itm.Value);
        }
        public static void client_LastExchangeMessagesItemResult(object sender, DS_LastExchangeMessagesResultItem value)
        {
            //    UpdateLog("received LastExchangeMessagesItemResult");
            //    UpdateLog("\t-----------------------------------------------------------------------------------", false);
            //    UpdateLog("\tImportant : In sample output, Only Limited fields from the response are printed.", false);
            //    UpdateLog("\tIf you want to see all fields, please modify source of the sample suitably.", false);
            //    UpdateLog("\t-----------------------------------------------------------------------------------", false);
            //    UpdateLog("\tIdentifier\tMsgID\tMessage", false);
            //    UpdateLog(string.Format("\t{0}\t{1}\t{2}", value.Identifier, value.MsgID, value.Message), false);
        }

        
        }
    }
