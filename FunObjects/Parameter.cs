using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChampService.FunObjects
{
    public class Parameter
    {
        public const string DATE_TIME_FORMAT = "MM-dd-yyyy HH:mm:ss";
        public string Name { get; set; }
        public string Default { get; set; }
        public Type Type { get; set; }
        public string Description { get; set; }
        public bool Optional { get; set; }
        public object Current { get; set; }
    }


}