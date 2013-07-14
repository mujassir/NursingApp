using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Xml.Linq;

namespace RMC.Web.Security
{
    public class Security
    {

        #region Protected Methods

        /// <summary>
        /// Implement Form base authentication using Token.
        /// Created By : Davinder Kumar.
        /// Created Date : June 23, 2009
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="role"></param>
        protected static string SecurityValidation(string userName, string role, string AccessRequest)
        {
            string redirectPage, encrypt;
            string[] splitRedirectUrl;
            HttpCookie httpCookie;
            try
            {
                FormsAuthentication.Initialize();
                //Generate the ticket, used for Login authentication.
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now,
                                DateTime.Now.AddMinutes(240), true, role, FormsAuthentication.FormsCookieName);
                encrypt = FormsAuthentication.Encrypt(ticket);
                httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypt);
                if (ticket.IsPersistent)
                {
                    httpCookie.Expires = ticket.Expiration;
                }
                HttpContext.Current.Response.Cookies.Add(httpCookie);

                redirectPage = FormsAuthentication.GetRedirectUrl(userName, false);
                splitRedirectUrl = redirectPage.Split('/');
                // code changes by Raman BBB
                // dated 12/25/2010
                // login functionality and add a hospital functionality
                if (splitRedirectUrl[1] != "Administrator" && role == "SuperAdmin" && (AccessRequest == "Owner" || AccessRequest == null))
                {
                    redirectPage = "~/Administrator/AdminHomePage.aspx";
                }
                else if (splitRedirectUrl[1] != "Users" && (role == "Admin" || role == "PowerUser") && (AccessRequest == "ReadOnly" || AccessRequest == null))
                {
                    redirectPage = "~/Users/UserHomePage.aspx";
                }
                else if (splitRedirectUrl[1] != "Users" && (role == "Admin" || role == "PowerUser") && (AccessRequest == "Owner" || AccessRequest == null))
                {
                    redirectPage = "~/Users/UserHomePage.aspx";
                }

                if (redirectPage.IndexOf('/') == 0)
                {
                    redirectPage = "~" + redirectPage;
                }

                return redirectPage;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SecurityValidation");
                ex.Data.Add("Class", "Security");
                throw ex;
            }
        }
        //End Of SecurityValidation Function.

        #endregion

    }
    //End Of Security.
}
//End Of Namespace.
