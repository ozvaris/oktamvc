using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace okta_aspnet_mvc_example
{
    public class SecuredTokenWebService : System.Web.Services.Protocols.SoapHeader
    {
        public String UserName { get; set; }
        public String Password { get; set; }
        public String AuthenticationToken { get; set; } 

        public SecuredTokenWebService(String UserName, String Password, String AuthenticationToken)
        {
            this.UserName = UserName;
            this.Password = Password;
            this.AuthenticationToken = AuthenticationToken;
        }

       public bool IsUserCredentialsValid(SecuredTokenWebService SoapHeader)
        {
            if (SoapHeader == null)
                return false;

            if (!string.IsNullOrEmpty(SoapHeader.AuthenticationToken))
                return (HttpRuntime.Cache[SoapHeader.AuthenticationToken] != null);

            return false;
        }
    }
}