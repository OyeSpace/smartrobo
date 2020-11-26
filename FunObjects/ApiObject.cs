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
                   
                    var startTimeSpan = TimeSpan.Zero;
                    var periodTimeSpan = TimeSpan.FromSeconds(0.001);

                    var timer = new System.Threading.Timer((e) =>
                    {
                        _client.GetLastQuote("NFO", "BANKNIFTY-I");
                    }, null, startTimeSpan, periodTimeSpan);

                    break;
                //case "SubscribeRealTime":
                //    _client.SubscribeRealtime("NFO", "NIFTY-I");
                //    break;


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
                //_client.Reconnecting -= client_Reconnecting;
                //_client.ConnectComple -= client_ConnectComple;
                _client.AuthenticateResult -= client_AuthenticateResult;
                //_client.RequestErrorResult -= client_RequestErrorResult;
                //_client.RequestAcceptResult -= client_RequestAcceptResult;
                //_client.ExchangesResult -= client_ExchangesResult;
                //_client.HistoryOHLCResult -= client_HistoryOHLCResult;
                //_client.HistoryTickResult -= client_HistoryTickResult;
                //_client.InstrumentsResult -= client_InstrumentsResult;
                //_client.LastExchangeMessagesItemResult -= client_LastExchangeMessagesItemResult;
                //_client.LastMarketMessagesItemResult -= client_LastMarketMessagesItemResult;
                //_client.LastExchangeMessagesResult -= client_LastExchangeMessagesResult;
                //_client.LastMarketMessagesResult -= client_LastMarketMessagesResult;
                _client.LastQuoteResult -= client_LastQuoteResult;
             //   _client.LastQuoteArrayResult -= client_LastQuoteArrayResult;
                _client.RealtimeResult -= client_RealtimeResult;
                //_client.LimitationResult -= client_LimitationResult;
                //_client.SnapshotResult -= client_SnapshotResult;


            }

            _client = new Client(false);
          
            _client.BeginConnect += client_BeginConnect;
            //_client.ConnectFailed += client_ConnectFailed;
            //_client.Reconnecting += client_Reconnecting;
            //_client.ConnectComple += client_ConnectComple;
            _client.AuthenticateResult += client_AuthenticateResult;
            //_client.RequestErrorResult += client_RequestErrorResult;
            //_client.RequestAcceptResult += client_RequestAcceptResult;
            //_client.ExchangesResult += client_ExchangesResult;
            //_client.HistoryOHLCResult += client_HistoryOHLCResult;
            //_client.HistoryTickResult += client_HistoryTickResult;
            //_client.InstrumentsResult += client_InstrumentsResult;
            //_client.LastExchangeMessagesItemResult += client_LastExchangeMessagesItemResult;
            //_client.LastMarketMessagesItemResult += client_LastMarketMessagesItemResult;
            //_client.LastExchangeMessagesResult += client_LastExchangeMessagesResult;
            //_client.LastMarketMessagesResult += client_LastMarketMessagesResult;
            _client.LastQuoteResult += client_LastQuoteResult;
            //  _client.LastQuoteArrayResult += client_LastQuoteArrayResult;
            _client.RealtimeResult += client_RealtimeResult;
            //_client.LimitationResult += client_LimitationResult;
            //_client.SnapshotResult += client_SnapshotResult;
            //  _client.RealtimeSnapshotResult += client_RealtimeSnapshotResult;

        }
        public static void client_SnapshotResult(object sender, DS_SnapshotResult value)
        {
            DS_SnapshotResultItem result = new DS_SnapshotResultItem();
             Trace.WriteLine( result.InstrumentIdentifier);
        }
        //client.GetLimitation(...)
        public static void client_LimitationResult(object sender, DS_LimitationResult value)
        {
            

            //if (value.AllowedExchanges != null)
            //{
            //    UpdateLog("=AllowedExchanges=", false);
            //    foreach (var itm in value.AllowedExchanges)
            //    {
            //        UpdateLog(string.Format("\t{0}: {1}", "ExchangeName", itm.ExchangeName), false);
            //        UpdateLog(string.Format("\t{0}: {1}", "DataDelay", itm.DataDelay), false);
            //        UpdateLog(string.Format("\t{0}: {1}", "AllowedInstruments", itm.AllowedInstruments), false);
            //        UpdateLog("\r\n", false);

            //        if (itm.UsedInstrumentsInThisSession.Count > 0)
            //        {
            //            UpdateLog(string.Format("\t{0}:", "UsedInstrumentsInThisSession"), false);
            //            foreach (var inst in itm.UsedInstrumentsInThisSession)
            //                UpdateLog(string.Format("\t\t{0}", inst.Value), false);
            //        }
            //    }
            //}

            //if (value.AllowedFunctions != null)
            //{
            //    UpdateLog("=AllowedFunctions=", false);
            //    foreach (var itm in value.AllowedFunctions)
            //    {
            //        UpdateLog(string.Format("\t{0}: {1}", "FunctionName", itm.FunctionName), false);
            //        UpdateLog(string.Format("\t{0}: {1}", "IsEnabled", itm.IsEnabled), false);
            //        UpdateLog("\r\n", false);
            //    }
            //}

            //if (value.GeneralParams != null)
            //{
            //    UpdateLog("=GeneralParams=", false);
            //    UpdateLog(string.Format("\t{0}: {1}", "AllowedBandwidthPerHour", value.GeneralParams.AllowedBandwidthPerHour), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "AllowedBandwidthPerMonth", value.GeneralParams.AllowedBandwidthPerMonth), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "AllowedCallsPerHour", value.GeneralParams.AllowedCallsPerHour), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "AllowedCallsPerMonth", value.GeneralParams.AllowedCallsPerMonth), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "Enabled", value.GeneralParams.Enabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}\r\n", "ExpirationDate", value.GeneralParams.ExpirationDate), false);
            //}

            //if (value.HistoryLimitation != null)
            //{
            //    UpdateLog("=HistoryLimitation=", false);
            //    UpdateLog(string.Format("\t{0}: {1}", "DayEnabled", value.HistoryLimitation.DayEnabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "Hour_12Enabled", value.HistoryLimitation.Hour_12Enabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "Hour_1Enabled", value.HistoryLimitation.Hour_1Enabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "Hour_2Enabled", value.HistoryLimitation.Hour_2Enabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "Hour_3Enabled", value.HistoryLimitation.Hour_3Enabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "Hour_4Enabled", value.HistoryLimitation.Hour_4Enabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "Hour_6Enabled", value.HistoryLimitation.Hour_6Enabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "Hour_8Enabled", value.HistoryLimitation.Hour_8Enabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "MaxEOD", value.HistoryLimitation.MaxEOD), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "MaxIntraday", value.HistoryLimitation.MaxIntraday), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "Minute_10Enabled", value.HistoryLimitation.Minute_10Enabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "Minute_12Enabled", value.HistoryLimitation.Minute_12Enabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "Minute_15Enabled", value.HistoryLimitation.Minute_15Enabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "Minute_1Enabled", value.HistoryLimitation.Minute_1Enabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "Minute_20Enabled", value.HistoryLimitation.Minute_20Enabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "Minute_2Enabled", value.HistoryLimitation.Minute_2Enabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "Minute_30Enabled", value.HistoryLimitation.Minute_30Enabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "Minute_3Enabled", value.HistoryLimitation.Minute_3Enabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "Minute_4Enabled", value.HistoryLimitation.Minute_4Enabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "Minute_5Enabled", value.HistoryLimitation.Minute_5Enabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "Minute_6Enabled", value.HistoryLimitation.Minute_6Enabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "MonthEnabled", value.HistoryLimitation.MonthEnabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "TickEnabled", value.HistoryLimitation.TickEnabled), false);
            //    UpdateLog(string.Format("\t{0}: {1}", "WeekEnabled", value.HistoryLimitation.WeekEnabled), false);
            //}
        }

        public static void client_LastMarketMessagesItemResult(object sender, DS_LastMarketMessagesResultItem value)
        {
             }

        //returns data in real-time if exchange allowed for your account
        public static void client_LastExchangeMessagesItemResult(object sender, DS_LastExchangeMessagesResultItem value)
        {
           Trace.WriteLine( value.Identifier);
        }

      
        public static void client_LastExchangeMessagesResult(object sender, DS_LastExchangeMessagesResult value)
        {
            //UpdateLog("received LastExchangeMessagesResult");
            //UpdateLog("\t-----------------------------------------------------------------------------------", false);
            //UpdateLog("\tImportant : In sample output, Only Limited fields from the response are printed.", false);
            //UpdateLog("\tIf you want to see all fields, please modify source of the sample suitably.", false);
            //UpdateLog("\t-----------------------------------------------------------------------------------", false);
            //UpdateLog("\tIdentifier\tMsgID\tMessage", false);
            //var tmp = string.Empty;
            //var cnt = 0;
            //foreach (var itm in value.Result)
            //{
            //    cnt++;
            //    if (cnt > _maxArray)
            //    {
            //        tmp += string.Format("\r\n...Reflected {0} elementd. Total count {1}...", _maxArray, value.Result.Count);
            //        break;
            //    }
            //    tmp += string.Format("\t{0}\t{1}\t{2}\r\n", itm.Identifier, itm.MsgID, itm.Message);
            //}
            //UpdateLog(tmp, false);
        }
     
       
        public static void client_InstrumentsResult(object sender, DS_InstrumentsResult value)
        {
           
            //var tmp = string.Empty;
            //var cnt = 0;
            //foreach (var itm in value.Result)
            //{
            //    cnt++;
            //    if (cnt > _maxArray)
            //    {
            //        tmp += string.Format("\r\n...Reflected {0} elementd. Total count {1}...", _maxArray, value.Result.Count);
            //        break;
            //    }
            //    tmp += string.Format("\t{0}\t{1}\t{2}\t{3}\r\n", itm.Identifier, itm.Name, itm.Product, itm.Expiry);
            //}
            //UpdateLog(tmp, false);
        }
        public static void client_BeginConnect(object sender, EventArgs e)
        {
            // UpdateLog("Connection Started.");
            Trace.WriteLine("connection Started");
        }

        public static void client_Reconnecting(object sender, EventArgs e)
        {
            Trace.WriteLine("Connection was broken. Reconnecting to server");
        }
        public static void client_RequestAcceptResult(object sender, DS_RequestAcceptResult value)
        {
            Trace.WriteLine(string.Format("Request [{0}] accepted", value.PaketID));
        }

        public static void client_ConnectFailed(object sender, EventArgs e)
        {
            Trace.WriteLine("connection failed. Check Connection IP and Port");
        }

        public static void client_RequestErrorResult(object sender, DS_RequestError value)
        {
            Trace.WriteLine("REquest "+value.Message);
          //  UpdateLog(string.Format("Server returned error for last request [{0}]: {1}", value.PaketID, value.Message));

        }

        public static void client_ConnectComple(object sender, EventArgs e)
        {
            Trace.WriteLine("Connection Complete. Ready to accept 'Authenticate request");
        }

        public static void client_HistoryOHLCResult(object sender, DS_HistoryOHLCResult value, string userComment)
        {
            Trace.WriteLine(value.Request.Exchange+","+ value.Request.InstrumentIdentifier);
           
            //var tmp = string.Empty;
            //var cnt = 0;
            //foreach (var itm in value.Result)
            //{
            //    cnt++;
            //    if (cnt > _maxArray)
            //    {
            //        tmp += string.Format("\r\n...Reflected {0} elementd. Total count {1}...", _maxArray, value.Result.Count);
            //        break;
            //    }
            //    tmp += string.Format("\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\r\n", itm.LastTradeTime, itm.Open, itm.High, itm.Low, itm.Close, itm.TradedQty, userComment);
            //}
            //UpdateLog(tmp, false);
        }

        public static void client_HistoryTickResult(object sender, DS_HistoryTickResult value, string userComment)
        {
           Trace.WriteLine( value.Request.Exchange+","+ value.Request.InstrumentIdentifier);
            //Trace.WriteLine("\tTimeStamp\tClose\tVolume\tComment", false);
            //var tmp = string.Empty;
            //var cnt = 0;
            //foreach (var itm in value.Result)
            //{
            //    cnt++;
            //    if (cnt > _maxArray)
            //    {
            //        tmp += string.Format("\r\n...Reflected {0} elementd. Total count {1}...", _maxArray, value.Result.Count);
            //        break;
            //    }
            //    tmp += string.Format("\t{0}\t{1}\t{2}\t{3}\r\n", itm.LastTradeTime, itm.LastTradePrice, itm.TradedQty, userComment);
            //}
            //UpdateLog(tmp, false);
        }
     
        public static void client_AuthenticateResult(object sender, DS_AuthenticateResult value)
        {
            if (value.Complete)
                Trace.WriteLine("Authenticated");
            else
                Trace.WriteLine("Authentication failed with message: " + value.Message);

        }
        public static void client_RealtimeResult(object sender, DS_RealtimeResult value)
        {
          //  UpdateLog(string.Format("\t{0}\t{1}\t{2}\t{3}", value.InstrumentIdentifier, value.LastTradeTime, value.LastTradePrice, value.LastTradeQty), false);

            Trace.WriteLine(value.InstrumentIdentifier+" ,"+ value.LastTradePrice+","+value.LastTradeTime);
        }

        public static void client_LastQuoteResult(object sender, DS_LastQuoteResult value)
        {
            //Values = value.LastTradePrice;
            
            Trace.WriteLine("CurrentDayOpen " + value.Open);

            Trace.WriteLine("Last ClosePrice " + value.LastTradePrice);
            AccountReader.InsertTickData(value.LastTradePrice);
            Trace.WriteLine(value.ServerTime);
           
         }
        private static void client_ExchangesResult(object sender, DS_ExchangesResult value)
        {
              foreach (var itm in value.Result)
                Trace.WriteLine("exchange value "+itm.Value);
        }
      

        
        }
    }
