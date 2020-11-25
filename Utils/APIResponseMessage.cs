using ChampService.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;

namespace ChampService.Utils
{
    public class APIResponseMessage
    {
        public string apiVersion;
        public bool success { get; set; }
       
        public APIResponseMessage()
        {
            apiVersion = "1.0";
              
        }
    }

    public class APISuccessResponseMessage<T> :  APIResponseMessage
    {
        public Dictionary<string, object> data = null;

        public APISuccessResponseMessage(T obj)
        {
            success = true;
            if (obj == null)
                data = null;
            else
            {
                data = new Dictionary<string, object>();
                data.Add(typeof(T).Name, obj);
            }
        }

        public void AddDataMember(string name, object obj)
        {
            if (data != null)
            {
                data.Add(name, obj);
            }
            else
            {
                data = new Dictionary<string, object>();
                data.Add(name, obj);
            }
        }

        public void AddDataMember(string v)
        {
            throw new NotImplementedException();
        }
    }
    
    public class APIFailureResponseMessage<T> : APIResponseMessage
    {
        public T error { get; set; }
        public APIFailureResponseMessage(T obj)
        {
            success = false;
            error = obj;
        }
       
    }

    //public class MyData<T>
    //{
    //    public string kind;
    //    public T resource;

    //    public MyData(T obj)
    //    {
    //        kind = typeof(T).Name;
    //        resource = obj;
    //    }
    //}
}