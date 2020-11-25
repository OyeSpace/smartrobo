using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ChampService.Utils
{
    public class ErrorResponse
    {
        public int code { get; set; }
        public string message { get; set; }
    }
}