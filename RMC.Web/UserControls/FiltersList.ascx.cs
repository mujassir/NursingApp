using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class FiltersList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                LogManager._stringObject = "GetUsers.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonSearchFilter_Click(object sender, EventArgs e)
        {
            try
            {
                ListBoxShowFilter.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSearchUser_Click");
                LogManager._stringObject = "GetUsers.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonSearchAddANewUser_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("BenchmarkingFilters.aspx", false);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSearchAddANewUser_Click");
                LogManager._stringObject = "GetUsers.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ListBoxShowFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ListBoxShowFilter.SelectedIndex > -1)
                {
                    Response.Redirect("ViewBenchmarkingFilters.aspx?filterId=" + ListBoxShowFilter.SelectedItem.Value, false);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ListBoxShowUser_SelectedIndexChanged");
                LogManager._stringObject = "GetUsers.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

    }
}