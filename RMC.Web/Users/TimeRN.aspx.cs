using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace RMC.Web.Users
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Report"] == "Dashboard")
                {
                    Title = "Reports :: LEAN Dashboard";
                }
                else if (Request.QueryString["Report"] == "PieCharts")
                {
                    Title = "Reports :: Monthly Data - Pie Charts";
                }
                else if (Request.QueryString["Report"] == "ControlCharts")
                {
                    Title = "Reports :: Control Charts";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
