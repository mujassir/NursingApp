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
using System.Collections.Generic;

namespace RMC.Web.Administrator
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {

        #region Events

        /// <summary>
        /// Logout Administrator.
        /// Created By : Davinder Kumar
        /// Creation Date : July 06, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonLogout_Click(object sender, EventArgs e)
        {
            try
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Login.aspx", false);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "LinkButtonLogout_Click");
                ex.Data.Add("Page", "Administrator/AdminMaster.Master");
                LogManager._stringObject = "AdminMaster.Master ---- LinkButtonLogout_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
            }
        }

        /// <summary>
        /// Page Load Method use to delete Cache.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 06, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    //to delete cache
                    Response.AddHeader("Pragma", "no-cache");
                    Response.AddHeader("Cache-Control", "no-cache");
                    Response.CacheControl = "no-cache";
                    Response.Expires = -1;
                    Response.ExpiresAbsolute = new DateTime(1900, 1, 1);
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                }

                //List<string> str = RMC.BussinessService.BSSerialization.Deserialize<List<string>>(HttpContext.Current.Session["BackUrl"] as System.IO.MemoryStream) as List<string>;
                //foreach (string s in str)
                //{
                //    ListBox1.Items.Add(new ListItem(s,s));
                //}
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "Administrator/AdminMaster.Master");
                LogManager._stringObject = "AdminMaster.Master ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
            }
        }

        #endregion

        protected void LinkButtonHospitalBanchMark_Click(object sender, EventArgs e)
        {
            string url = "~/Administrator/HospitalBenchmark.aspx";
            string fullUrl = "window.open('" + url + "','_blank','height=500,width=800,status=yes,toolbar=no,menubar=yes,location=no,scrollbars=yes,resizable=yes,titlebar=no');";
            //ButtonGenerateReport.Attributes.Add("OnClick", fullUrl);
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullUrl, true);
        }
     
    }
}
