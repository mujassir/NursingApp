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

namespace RMC.Web.Users
{
    public partial class StandardFormat : System.Web.UI.Page
    {

        #region Properties

        public string MessageString
        {
            get
            {
                string message = string.Empty;
                if (Request.QueryString["filename"] != null)
                {
                    message = "Please select the configuration of this non-configuration file " + Convert.ToString(Request.QueryString["filename"]);
                }
                else
                {
                    message = "Please select the configuration for non-configuration file.";
                }

                return message;
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = "Popup";
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            Session["ConfigName"] = DropDownListConfigName.SelectedItem.Text;
            string script = @"<script>window.opener.getProp(); window.close();</script>";

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "close", script);
        }

        #endregion

    }
}
