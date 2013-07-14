using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class CopyProfileFromTemplate : System.Web.UI.UserControl
    {
        List<RMC.DataService.ProfileType> objectGenericPreofileType = null;
        RMC.BussinessService.BSProfileType objectBSProfileType = null;

        public string PType
        {
            get
            {
                if (Convert.ToInt16(Request.QueryString["valuetype"]) == 0)
                {
                    return "value added";
                }
                else if (Convert.ToInt16(Request.QueryString["valuetype"]) == 1)
                {
                    return "others";
                }
                else
                {
                    return "location";
                }
            }
        }

        private void BindDropDownListProfileType()
        {
            try
            {
                objectBSProfileType = new RMC.BussinessService.BSProfileType();
                objectGenericPreofileType = objectBSProfileType.GetProfileInformation(PType);
                if (objectGenericPreofileType.Count > 0)
                {
                    DropDownListProfileType.DataSource = objectGenericPreofileType;
                    DropDownListProfileType.DataTextField = "ProfileName";
                    DropDownListProfileType.DataValueField = "ProfileTypeID";
                    DropDownListProfileType.DataBind();
                    DropDownListProfileType.Items.Insert(0, new ListItem("--Select Profile--", "0"));
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Event", "BindDropDownListProfileType");
                ex.Data.Add("Page", "ProfileDetail.ascx");
                LogManager._stringObject = "ProfileDetail.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDropDownListProfileType();
                Session["SelectedProfileIndex"] = 0;
                Session["SelectedProfileValue"] = 0;
            }
        }

        protected void DropDownListProfileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["SelectedProfileIndex"] = DropDownListProfileType.SelectedIndex;
            Session["SelectedProfileValue"] = DropDownListProfileType.SelectedValue;
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Session["SelectedProfileIndex"] = 0;
            Session["SelectedProfileValue"] = 0;
        }

       
    }
}