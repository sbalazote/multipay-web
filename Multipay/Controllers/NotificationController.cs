using System.Web;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using Multipay.DTOs;

namespace Multipay.Controllers
{
    public class NotificationController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void GetAuthenticationToken(string topic, int id) 
        {
            Log.Debug(topic);
            if (topic != null)
            {
            //return the created user
            throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.Created));
            }
            else
            {
            throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.InternalServerError));        
            }
        }

        [HttpPost]
        [Route("api/notifications")]
        public HttpResponseMessage Notifications([FromBody] WebhooksDTO webhooksDto)
        {
            var type = HttpContext.Current.Request.QueryString["type"];
            var dataId = HttpContext.Current.Request.QueryString["data.id"];

            var response = Request.CreateResponse(HttpStatusCode.Created);
            

            if (type.Equals("payment"))
            {
                if (webhooksDto.Action.Equals("payment.created"))
                {

                }
                else if (webhooksDto.Action.Equals("payment.updated"))
                {
                    
                }

            }
            else if (type.Equals("mp-connect"))
            {
                if (webhooksDto.Action.Equals("application.authorized"))
                {

                }
                else if (webhooksDto.Action.Equals("application.deauthorized"))
                {

                }
            }
            return response;
        }
    }
}
