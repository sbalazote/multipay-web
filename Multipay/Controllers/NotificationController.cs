using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace Multipay.Controllers
{
    public class NotificationController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void GetAuthenticationToken(string topic, int id) 
        {
            Log.Debug(topic);
            if (user != null)
            {
            //return the created user
            throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.Created, user);
            }
            else
            {
            throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.InternalServerError));        
            }
            Log.Debug(topic);
        }
    }
}
