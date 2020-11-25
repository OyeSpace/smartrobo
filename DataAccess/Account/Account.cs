using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChampService.DataAccess;

namespace ChampService.DataAccess
{
    public class Account
    {
        public int AccountID { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }

    public class LoginOTP
    {
        public int LOID { get; set; }
        public string LOMobileNumber { get; set; }
        public string LOOTP { get; set; }
        public bool LOIsActive { get; set; }
        public DateTime LODCreated { get; set; }
        public string LODVerfied { get; set; }
     
    }
}