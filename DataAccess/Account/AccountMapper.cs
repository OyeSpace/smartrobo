using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ChampService.DataAccess
{
    class AccountMapper : MapperBase<Account>
    {
        protected override Account Map(IDataRecord record)
        {
            Account acct = new Account();
            try
            {




                Account account = new Account();
                try
                {
                    account.AccountID = (DBNull.Value == record["AccountID"]) ?
                        0 : (int)record["AccountID"];

                    account.MobileNumber = (DBNull.Value == record["MobileNumber"]) ?
                      "" : (string)record["MobileNumber"];

                    account.Password = (DBNull.Value == record["Password"]) ?
                     "" : (string)record["Password"];

                    account.IsActive = (DBNull.Value == record["IsActive"]) ?
                    false : (bool)record["IsActive"];
                }
                catch (Exception e)
                {
                    throw e;
                }
                return account;

            }
            catch (Exception e)
            {
                Trace.TraceError("{0}: Exception reading account information", DateTime.Now.ToString());
                throw e;
                // NOTE: 
                // consider handling exeption here instead of re-throwing
                // if graceful recovery can be accomplished
            }

            return acct;
        }
    }

    class LoginOTPMapper: MapperBase<LoginOTP>
    {
        protected override LoginOTP Map(IDataRecord record)
        {
            LoginOTP logotp = new LoginOTP();
            try
            {

                logotp.LOID = (DBNull.Value == record["LOID"]) ?
                0 : (int)record["LOID"];
                logotp.LOMobileNumber = (DBNull.Value == record["LOMobileNumber"]) ?
                "" : (string)record["LOMobileNumber"];
                logotp.LOOTP = (DBNull.Value == record["LOOTP"]) ?
                "" : (string)record["LOOTP"];
                logotp.LOIsActive = (DBNull.Value == record["LOIsActive"]) ?
                 false : (bool)record["LOIsActive"];
                logotp.LODCreated = (DBNull.Value == record["LODCreated"]) ?
                 new DateTime() : (DateTime)record["LODCreated"];
                logotp.LODVerfied = (DBNull.Value == record["LODVerfied"]) ?
                 "" : (string)record["LODVerfied"];
            }
            catch (Exception e)
            {

            }
            return logotp;
        }
   }
}