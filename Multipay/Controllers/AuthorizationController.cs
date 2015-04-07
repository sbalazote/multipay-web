using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Multipay.Controllers
{
    public class AuthorizationController : ApiController
    {
        public string GetAuthorizationCode(string code) {
            return code;
        }
    }
}
