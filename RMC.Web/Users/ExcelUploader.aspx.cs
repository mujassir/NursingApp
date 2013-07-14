using System;
using System.Collections;
using System.Collections.Generic;
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
using LogExceptions;

namespace RMC.Web.Users
{
    public partial class ExcelUploader : System.Web.UI.Page
    {

        #region Variables

        //Generic object of Data Service.
        List<RMC.DataService.UserInfo> _genericUserInfo;

        #endregion
        
        #region Events

        /// <summary>
        /// Use to Clear UnitName DropDownList.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 15, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListHospitalName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {            
                DropDownListUnitName.Items.Clear();               
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "DropDownListHospitalName_SelectedIndexChanged");
                ex.Data.Add("Page", "ExcelUploader.aspx");
                LogManager._stringObject = "ExcelUploader.aspx ---- DropDownListHospitalName_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        /// <summary>
        /// Use Save DemographicID in Session.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 15, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListUnitName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["DemographicID"] = DropDownListUnitName.SelectedValue;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "DropDownListUnitName_SelectedIndexChanged");
                ex.Data.Add("Page", "ExcelUploader.aspx");
                LogManager._stringObject = "ExcelUploader.aspx ---- DropDownListUnitName_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        /// <summary>
        /// <Description>Page Load Functions</Description>
        /// </summary>
        /// <Author>Amit Chawla</Author>
        /// <Created On>09-July-2009</Created>
        /// <ModifiedBy></ModifiedBy>
        /// <ModifiedOn></ModifiedOn> 
        /// <Version No=1>Description of change</Version>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // allows the javascript function to do a postback and call the onClick method
            // associated with the linkButton LinkButton1.
            if (!Page.IsPostBack)
            {
                if (!(HttpContext.Current.User.IsInRole("superadmin")))
                {
                    if (!((List<RMC.DataService.UserInfo>)Session["UserInformation"])[0].IsActive)
                    {
                        Response.Redirect("~/Users/InActiveUser.aspx");
                    }
                }
                string jscript = "function UploadComplete(){";
                jscript += string.Format("__doPostBack('{0}','');", LinkButton1.ClientID.Replace("_", "$"));
                jscript += "};";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "FileCompleteUpload", jscript, true);
                try
                {
                    TextBoxDate.AutoPostBack = false;
                    TextBoxDate.Attributes.Add("readonly", "readonly");
                    TextBoxDate.Text = DateTime.Now.ToShortDateString();
                    TextBoxDate.AutoPostBack = true;
                    Session["Date"] = TextBoxDate.Text;
                    Session["UserID"] = ((List<RMC.DataService.UserInfo>)Session["UserInformation"])[0].UserID;
                }
                catch (Exception ex)
                {
                    ex.Data.Add("Events", "Page_Load");
                    ex.Data.Add("Page", "ExcelUploader.aspx");
                    LogManager._stringObject = "ExcelUploader.aspx ---- Page_Load";
                    LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                    LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                    DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
                }
            }
        }

        /// <summary>
        /// Use to Store value in Session.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 15, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                Session["DemographicID"] = DropDownListUnitName.SelectedValue;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_PreRender");
                ex.Data.Add("Page", "ExcelUploader.aspx");
                LogManager._stringObject = "ExcelUploader.aspx ---- Page_PreRender";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        /// <summary>
        /// <Description>This function is used to get the log of sucess and unsuccess files </Description>
        /// </summary>
        /// <Author>Amit Chawla</Author>
        /// <Created On>09-July-2009</Created>
        /// <ModifiedBy></ModifiedBy>
        /// <ModifiedOn></ModifiedOn> 
        /// <Version No=1>Description of change</Version>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExcelUploaderResults.aspx");
            // Do something that needs to be done such as refresh a gridView
            // say you had a gridView control called gvMyGrid displaying all 
            // the files uploaded. Refresh the data by doing a databind here.
            // gvMyGrid.DataBind();
        }

        /// <summary>
        /// Input the value of a UserID. 
        /// Created By : Davinder Kumar.
        /// Creation Date : July 15, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSourceHospitalName_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                _genericUserInfo = (List<RMC.DataService.UserInfo>)Session["UserInformation"];

                e.InputParameters[0] = _genericUserInfo[0].UserID;
                ScriptManagerUploader.SetFocus(DropDownListUnitName);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ObjectDataSourceHospitalName_Selecting");
                ex.Data.Add("Page", "ExcelUploader.aspx");
                LogManager._stringObject = "ExcelUploader.aspx ---- ObjectDataSourceHospitalName_Selecting";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
            finally
            {
                _genericUserInfo = null;
            }
        }

        /// <summary>
        /// Use to Save Date in a Session.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 15, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBoxDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (TextBoxDate.Text.Length > 0)
                {
                    Session["Date"] = TextBoxDate.Text;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "TextBoxDate_TextChanged");
                ex.Data.Add("Page", "ExcelUploader.aspx");
                LogManager._stringObject = "ExcelUploader.aspx ---- TextBoxDate_TextChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        } 

        #endregion
         
        #region Private Methods

        /// <summary>
        /// Use to Display message of Login Failure.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 10, 2009.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="color"></param>
        private void DisplayMessage(string msg, System.Drawing.Color color)
        {
            try
            {
                LabelErrorMsg.Text = msg;
                LabelErrorMsg.ForeColor = color;
                LabelErrorMsg.Visible = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DisplayMessage");
                ex.Data.Add("Class", "UserRegistration");
                throw ex;
            }
        }

        /// <summary>
        /// <Description>This function is used to get the flash variables </Description>
        /// </summary>
        /// <Author>Amit Chawla</Author>
        /// <Created On>09-July-2009</Created>
        /// <ModifiedBy></ModifiedBy>
        /// <ModifiedOn></ModifiedOn> 
        /// <Version No=1>Description of change</Version>
        /// <returns></returns>
        protected string GetFlashVars()
        {
            // Adds query string info to the upload page
            // you can also do something like:
            // return "?" + Server.UrlEncode("CategoryID="+CategoryID);
            // we UrlEncode it because of how LoadVars works with flash,
            // we want a string to show up like this 'CategoryID=3&UserID=4' in
            // the uploadPage variable in flash.  If we passed this string withou
            // UrlEncode then flash would take UserID as a seperate LoadVar variable
            // instead of passing it into the uploadPage variable.
            // then in the httpHandler we get the CategoryID and UserID values from 
            // the query string. See Upload.cs in App_Code

            //return "?" + Server.UrlEncode(Request.QueryString.ToString());
            //----------------
            int CategoryID = 1;
            return "?" + Server.UrlEncode("CategoryID=" + CategoryID);

        }
        
        #endregion

    }
}
