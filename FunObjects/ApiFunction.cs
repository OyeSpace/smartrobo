using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChampService.FunObjects
{
    public class ApiFunction
    {
        public string Name { get; set; }
        public string ExtName { get; set; }
        public string Description { get; set; }
        public List<Parameter> Parameters { get; set; }
    }
}