using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;
using System.Security.Principal;
using System.Diagnostics;
using System.Reflection;

namespace RMC.Web
{
    public class Global : System.Web.HttpApplication
    {
        private void RecoverFromWebResourceError()
        {

            Type myType = typeof(System.Web.Handlers.AssemblyResourceLoader);

            FieldInfo handlerExistsField = myType.GetField("_handlerExists", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            FieldInfo handlerExistenceCheckedField = myType.GetField("_handlerExistenceChecked", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            if ((((Boolean)handlerExistsField.GetValue(null)) == false) && (((Boolean)handlerExistenceCheckedField.GetValue(null)) == true))
            {
                handlerExistenceCheckedField.SetValue(null, false);
                //EventLog.WriteEntry("Demo", "Recovered from WebResource.axd not found condition", EventLogEntryType.Warning);
            }

        }

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id = (FormsIdentity)(HttpContext.Current.User.Identity);
                        FormsAuthenticationTicket ticket = id.Ticket;
                        string userData = ticket.UserData;
                        string[] roles = userData.Split(',');
                        HttpContext.Current.User = new GenericPrincipal(id, roles);
                    }
                }
            }           
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            RecoverFromWebResourceError();
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}