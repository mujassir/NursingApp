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
using System.Collections.Generic;
using RMC.BussinessService;
using LogExceptions;
using System.IO;

namespace RMC.Web.Administrator
{
    /// <summary>
    /// To display listing of users
    /// <Author>Raman</Author>
    /// <createdOn>July 22, 2009</createdOn>
    /// </summary>
    public partial class UserList : System.Web.UI.Page
    {        
        #region Variables
        //Bussiness Service Objects.
        RMC.BussinessService.BSCommon _objectBSCommon;
        RMC.BussinessService.BSUsers _objectBSUsers = null;
        #endregion
        #region properties
        /// <summary>
        /// Return Logged in user Email
        /// </summary>
        public string ActiveUser
        {
            get
            {
                return ((List<RMC.DataService.UserInfo>)(BSSerialization.Deserialize<List<RMC.DataService.UserInfo>>(HttpContext.Current.Session["UserInformation"] as MemoryStream))).Count > 0 ? CommonClass.UserInformation.Email : "";                
            }
        }
        #endregion
        #region Events
        /// <summary>
        /// This event is used to update the status of the users
        /// </summary>
        /// <Author>Amit Chawla</Author>
        /// <Created On>01-July-2009</Created>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                _objectBSCommon = new BSCommon();
                //GridView _grid = (GridView)(this.Page.FindControl("GridUsers"));
                int i;
                if (GridUsers.Rows.Count > 0)
                {
                    for (i = 0; i < GridUsers.Rows.Count; i++)
                    {

                        CheckBox _chk = (CheckBox)(GridUsers.Rows[i].FindControl("CheckIsActive"));

                        int _userID = Convert.ToInt16(GridUsers.DataKeys[i].Value);
                        if (_chk.Checked)
                        {
                            _objectBSCommon.UpdateUserStatus(_userID, true);
                        }
                        else
                        {
                            _objectBSCommon.UpdateUserStatus(_userID, false);
                        }
                    }
                    GridUsers.DataBind();
                }

            }
            catch (Exception ex)
            {

                ex.Data.Add("Events", "ButtonSubmit Click");
                ex.Data.Add("Page", "Administrator/UserList.aspx");
                LogManager._stringObject = "UserList.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                //DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
            finally
            {
                _objectBSCommon = null;
            }
        }

        /// <summary>
        /// To Reset the page 
        /// <Author>Raman</Author>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                SetPageDefaultSetting();
                GridUsers.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonReset Click");
                ex.Data.Add("Page", "Administrator/UserList.aspx");
                LogManager._stringObject = "UserList.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                //DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));

            }
        }

        /// <summary>
        /// Page_Load Event. 
        /// Set Listing Size, Add User Link and Set page default setting 
        /// <Author>Raman</Author>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GridUsers.PageSize = CommonClass.DefaultListingPageSize;
                //HyperlinkAddUser.Attributes.Add("onclick", "location.href='UserProfile.aspx?CreateUser=Y'");
                SetPageDefaultSetting();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page Load");
                ex.Data.Add("Page", "Administrator/UserList.aspx");
                LogManager._stringObject = "UserList.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                //DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }
        /// <summary>
        /// To delete record
        /// <Author>Raman</Author>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() == "logicallydelete")
                {
                    int userId = Convert.ToInt32(GridUsers.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);
                    _objectBSUsers = new RMC.BussinessService.BSUsers();
                    _objectBSUsers.DeleteUserInformation(userId, ActiveUser);
                    DisplayMessage("Record has been deleted successfully!", System.Drawing.Color.Green);
                    GridUsers.DataBind();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", " protected void GridUsers_RowCommand(object sender, GridViewCommandEventArgs e)");
                ex.Data.Add("Class", "UserList.ascx.cs ---- ");
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                //DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }
        /// <summary>
        /// Add confirmation for deletion operation and set commandargument
        /// <Author>Raman</Author>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton LinkButtonDelete = e.Row.FindControl("LinkButtonDelete") as LinkButton;
                    if (LinkButtonDelete != null)
                    {
                        LinkButtonDelete.CommandArgument = Convert.ToString(e.Row.RowIndex);
                        LinkButtonDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", " protected void GridUsers_RowDataBound(object sender, GridViewRowEventArgs e)");
                ex.Data.Add("Class", "UserList.ascx.cs ---- ");
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                //DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// To set value in "LoggedInUser" parameter of linq datasource
        /// <Author>Raman</Author>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinqDataSourceUserList_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                e.WhereParameters["LoggedInUser"] = (object)ActiveUser;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", " protected void LinqDataSourceUserList_Selecting(object sender, LinqDataSourceSelectEventArgs e)");
                ex.Data.Add("Class", "UserList.ascx.cs ---- ");
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                //DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }        
        #endregion
        
        #region Private Methods        
        /// <summary>
        /// Use to Display message of Login Failure.
        /// <Author>Raman</Author>
        /// <CreatedOn>July 20, 2009</CreatedOn>
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
                ex.Data.Add("Class", "Login");
                throw ex;
            }
        }
        /// <summary>
        /// Set Page default setting
        /// <Author>Raman</Author>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetPageDefaultSetting()
        {
            try
            {
                LabelErrorMsg.Text = "";
                LabelErrorMsg.Visible = false;                
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", " private void SetPageDefaultSetting()");
                ex.Data.Add("Class", "UserList.ascx.cs ---- ");
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }
        #endregion
       
    }
}
