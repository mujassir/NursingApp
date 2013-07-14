using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RMC.Web.Users
{
    public partial class ReportUnitAssessment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Report"] == "Dashboard")
                {
                    Title = "Monthly Summary Dashboard";
                }
                else if (Request.QueryString["Report"] == "PieCharts")
                {
                    Title = "Monthly Data - Pie Charts";
                }
                else if (Request.QueryString["Report"] == "ControlCharts")
                {
                    Title = "Control Charts";
                }
                else if (Request.QueryString["Report"] == "UnitAssessment")
                {
                    Title = "Unit Assessment";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}