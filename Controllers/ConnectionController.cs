using ChampService.DataAccess;
using ChampService.Models;
using ChampService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ChampService.Controllers
{
    [RoutePrefix("trading/api")]
    public class ConnectionController : ApiController
    {

        APISuccessResponseMessage<Entries> response = new APISuccessResponseMessage<Entries>(null);
        private HttpResponseMessage SendMessageResponse(HttpStatusCode statuscode, APIResponseMessage msg)
        {
            return Request.CreateResponse(statuscode, msg);
        }

        [HttpPost]
        [Route("v1/Entries")]
        [Route("Entries")]
        public void Entries([FromBody] Entries entries)
        {


            EntriesReader.EntrieCreate(entries);
            
        }

        public async Task<bool> BuySellSignals()
        {

            

            return true;
        }
    }
}
