using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TrueData.Velocity;
using TrueData.Velocity.External;

namespace ChampService.TrueData
{
    public class TrueObjects
    {
        public static void InitializeComponent()
        {
            //   button_Initialize_Click -= button_Initialize_Click;
            button_Initialize += new System.EventHandler(button_Initialize_Click);
            api.OnRealTimeData += OnRealTimeData;
           
        }


       static TrueDataExternal api = null;

        public static EventHandler button_Initialize { get; private set; }

        public static void button_Initialize_Click(object sender, EventArgs e)
        {
            try
            {
                api = new TrueDataExternal();
                api.OnBarData += new TrueDataExternal.OnBarDataDelegate(OnBarData);
                api.OnRealTimeData += new TrueDataExternal.OnRealTimeDataDelegate(OnRealTimeData);


                api.VelocityInitialize();

                api.VelocityWaitForReadyToProcess();
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

       
            public static void OnBarData(int RequestId, ITrueDataChart chart)
            {
                for (int index = 0; index < chart.GetCount(); index++)
                {
                    SAbBarDataEx bar = (SAbBarDataEx)chart.GetBar(index);

                //  SetText(string.Format("{0};{1};{2};{3};{4};{5}", DateTime.FromOADate(bar.date), bar.open, bar.high, bar.low, bar.close, bar.volume));
                Trace.WriteLine("Open " + bar.open);
                }
            }
        public static void OnRealTimeData(string symbol, double time, ITrueDataUpdate update)
        {
            for (int index = 0; index < update.GetCount(); index++)
            {
                SRealTimeData price = (SRealTimeData)update.GetUpdate(index);

                // SetText(string.Format("{0};{1};{2};{3}", symbol, DateTime.FromOADate(time), price.id, price.data));
                Trace.WriteLine("price " + price.data);
            }
        }


        private void button_Uninitialize_Click(object sender, EventArgs e)
        {
            try
            {
                api.VelocityUninitialize();
                api.OnBarData -= OnBarData;
                api.OnRealTimeData -= OnRealTimeData;
            }
            catch (System.Exception ex)
            {
               Trace.WriteLine(ex.Message);
            }
        }
    }
}