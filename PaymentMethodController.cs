using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ChampService.DataAccess;
using ChampService.Models;
using ChampService.Utils;

namespace ChampService.Controllers
{
    [RoutePrefix("champ/api")]
    public class PaymentMethodController : ApiController
    {

        const string _CHAMP_API_KEY = "1FDF86AF-94D7-4EA9-8800-5FBCCFF8E5C1";
        const string _CHAMP_API_KEYNAME = "X-Champ-APIKey";
        APISuccessResponseMessage<PaymentMethod> response = null;
        public int Method { get; private set; }


        private bool IsValidAPIKey()
        {
            IEnumerable<string> values;
            bool found = this.Request.Headers.TryGetValues(_CHAMP_API_KEYNAME, out values);
            if (found == false || values.Count() == 0)
                return false;
            if (string.Equals(values.ToArray()[0], _CHAMP_API_KEY))
            {
                return true;
            }
            return false;
        }
        private HttpResponseMessage SendMessageResponse(HttpStatusCode statuscode, APIResponseMessage msg)
        {
            return Request.CreateResponse(statuscode, msg);
        }

        [HttpGet]
        [Route("v1/paymentmethods/getpaymentmethods")]
        [Route("paymentmethods/getpaymentmethods")]

        public HttpResponseMessage GetPaymentMethods()

        {
            if (!IsValidAPIKey())
            {
                ErrorResponse error = new ErrorResponse();
                error.code = 3001;
                error.message = "Invalid API Key";
                return SendMessageResponse(HttpStatusCode.Unauthorized, new APIFailureResponseMessage<ErrorResponse>(error));

            }

            Collection<PaymentMethod> coll = PaymentMethodReader.GetPaymentMethods();

            if (coll.Count == 0)

                return SendMessageResponse(HttpStatusCode.OK, new APISuccessResponseMessage<PaymentCategory>(null));
            APISuccessResponseMessage<List<PaymentCategory>> resp = new APISuccessResponseMessage<List<PaymentCategory>>(null);
            resp.AddDataMember("PaymentMethod", coll.ToList());
            return SendMessageResponse(HttpStatusCode.OK, resp);
        }

        [HttpGet]
        [Route("v1/paymentmethod/getpaymentmethodslist/{methodid}")]
        [Route("paymentmethod/getpaymentmethodslist/{methodid}")]

        public HttpResponseMessage GetPaymentMethodsList(int MethodID)

        {
            if (!IsValidAPIKey())
            {
                ErrorResponse error = new ErrorResponse();
                error.code = 3001;
                error.message = "Invalid API Key";
                return SendMessageResponse(HttpStatusCode.Unauthorized, new APIFailureResponseMessage<ErrorResponse>(error));

            }

            Collection<PaymentMethod> coll = PaymentMethodReader.GetPaymentMethodList(MethodID);

            if (coll.Count == 0)
            {
                ErrorResponse error = new ErrorResponse();
                error.code = 3002;
                error.message = "Invalid ID";
                return SendMessageResponse(HttpStatusCode.Unauthorized, new APIFailureResponseMessage<ErrorResponse>(error));
            }

            APISuccessResponseMessage<PaymentMethod> resp = new APISuccessResponseMessage<PaymentMethod>(coll[0]);

            return SendMessageResponse(HttpStatusCode.OK, resp);
        }

        [HttpPost]
        [Route("v1/paymentmethod/add")]
        [Route("paymentmethod/add")]
        public HttpResponseMessage PaymentMethodAdd([FromBody]PaymentMethod pymtmethod)
        {
            if (!IsValidAPIKey())
            {
                ErrorResponse error = new ErrorResponse();
                error.code = 3002;
                error.message = "Invalid API Key";
                return SendMessageResponse(HttpStatusCode.Unauthorized, new APIFailureResponseMessage<ErrorResponse>(error));

            }

            if (pymtmethod == null || pymtmethod.MethodID  == 0)
            {
                // send error
                ErrorResponse error = new ErrorResponse();
                error.code = 3003;
                error.message = "Invalid PaymentMethod Information";
                return SendMessageResponse(HttpStatusCode.Unauthorized, new APIFailureResponseMessage<ErrorResponse>(error));
            }

            if (pymtmethod == null || pymtmethod.Method == null)
            {
                PaymentMethodReader.GetPaymentMethodByMethod(pymtmethod.Method);
            }
            else
            {
                ErrorResponse error = new ErrorResponse();
                error.code = 3009;
                error.message = "PaymentMethodDetails already exist";
                return SendMessageResponse(HttpStatusCode.Unauthorized, new APIFailureResponseMessage<ErrorResponse>(error));
            }
            response = new APISuccessResponseMessage<PaymentMethod>(Method);
            return SendMessageResponse(HttpStatusCode.OK, response);
        }


    }
}