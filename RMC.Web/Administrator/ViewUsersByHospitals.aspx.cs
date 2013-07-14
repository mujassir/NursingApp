using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogExceptions;
using RMC.BussinessService;
using System.IO;

namespace RMC.Web.Administrator
{
    public partial class ViewUsersByHospitals : System.Web.UI.Page
    {
        #region Variables
        int _hospitalInfoId = 0;
        RMC.BussinessService.BSUsers _objectBSUsers = null;
        RMC.BussinessService.BSMultiUserHospital _objectBSMultiUserHospital = null;
        List<RMC.BussinessService.BSMultiUserHospital> _listBSMultiUserHospital = null;
        RMC.BussinessService.BSHospitalInfo _objectBSHospitalInfo = null;
        RMC.BussinessService.BSHospitalDemographicDetail _objectBSHospitalDemographicDetail = null;        
        #endregion
        #region Properties
        public string ActiveUser
        {
            get
            {
                if (Session["UserInformation"] != null)
                {
                    return ((List<RMC.DataService.UserInfo>)(BSSerialization.Deserialize<List<RMC.DataService.UserInfo>>(HttpContext.Current.Session["UserInformation"] as MemoryStream))).Count > 0 ? CommonClass.UserInformation.Email : "";
                }
                else
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// Return true if Page has been opened for Creating Hospital
        /// <Author>Raman</Author>
        /// <createdOn>July 26, 2009</createdOn>
        /// </summary>
        public int HospitalInfoId
        {
            get
            {
                if (Request.QueryString["HospitalInfoId"] != null)
                {
                    int.TryParse(Request.QueryString["HospitalInfoId"], out _hospitalInfoId);
                }
                return _hospitalInfoId;
            }
        }
        #endregion
        #region Events
        /// <summary>
        /// Page load event
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                _objectBSMultiUserHospital = new RMC.BussinessService.BSMultiUserHospital();
                //_objectBSMultiUserHospital.GetUserByPermissions(13);
                GridHospitalUsers.PageSize = CommonClass.DefaultListingPageSize;
                DisplayMessage("", System.Drawing.Color.Green);
                if (Page.IsPostBack == false)
                {
                    BindHospitals();
                    BindUsers();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page Load");
                ex.Data.Add("Page", "ViewUsersByHospitals.aspx");
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }
        /// <summary>
        /// To Reset Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        protected void ButtonApplyFilter_Click(object sender, EventArgs e)
        {
            try
            {
                BindUsers();
                GridHospitalUsers.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", " protected void ButtonApplyFilter_Click(object sender, EventArgs e)");
                LogManager._stringObject = "ViewUsersByHospitals.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }
        #endregion
        #region Function
        /// <summary>
        /// Use to Display message of Login Failure.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="color"></param>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
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
                ex.Data.Add("Class", "ManageHospitals");
                throw ex;
            }
        }
        /// <summary>
        /// Use Bind Hospital Dropdown
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="color"></param>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        private void BindHospitals()
        {
            List<RMC.BusinessEntities.BEHospitalIdName> listBEHospitalIdName = null;
            try
            {
                _objectBSHospitalInfo = new RMC.BussinessService.BSHospitalInfo();
                listBEHospitalIdName = _objectBSHospitalInfo.GetHospitalsIdName();
                DropDownListHospital.DataSource = listBEHospitalIdName;
                DropDownListHospital.DataTextField = "HospitalName";
                DropDownListHospital.DataValueField = "HospitalInfoId";
                DropDownListHospital.DataBind();
                if (DropDownListHospital.Items.FindByValue(Convert.ToString(HospitalInfoId)) != null)
                {
                    DropDownListHospital.SelectedValue = Convert.ToString(HospitalInfoId);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "BindHospitals");
                ex.Data.Add("Class", "ManageHospitals");
                throw ex;
            }
            finally { listBEHospitalIdName = null; }
        }
        #endregion
        /// <summary>
        /// To add check mark to show read only permissions
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 27, 2009</CreatedOn>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridHospitalUnit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "PermissionId")) == 1)
                    {
                        CheckBox CheckReadOnly = e.Row.FindControl("CheckReadOnly") as CheckBox;
                        if (CheckReadOnly != null)
                        {
                            CheckReadOnly.Checked = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "protected void GridHospitalUnit_RowDataBound(object sender, GridViewRowEventArgs e)");
                ex.Data.Add("Page", "Administrator/ViewUsersByHospitals.aspx");
                LogManager._stringObject = "ViewUsersByHospitals.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }
        /// <summary>
        /// To reset page
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 27, 2009</CreatedOn>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                BindHospitals();
                BindUsers();
                GridHospitalUsers.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "protected void ButtonReset_Click(object sender, GridViewRowEventArgs e)");
                ex.Data.Add("Page", "Administrator/ViewUsersByHospitals.aspx");
                LogManager._stringObject = "ViewUsersByHospitals.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }
        /// <summary>
        /// update Permissions in database
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 27, 2009</CreatedOn>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //GridView _grid = (GridView)(this.Page.FindControl("GridUsers"));
                if (GridHospitalUsers.Rows.Count > 0)
                {
                    for (int i = 0; i < GridHospitalUsers.Rows.Count; i++)
                    {

                        CheckBox CheckReadOnly = (GridHospitalUsers.Rows[i].FindControl("CheckReadOnly")) as CheckBox;
                        if (CheckReadOnly != null)
                        {
                            if (CheckReadOnly.Checked == false)
                            {
                                _objectBSMultiUserHospital = new RMC.BussinessService.BSMultiUserHospital();
                                int multiUserHospitalID = Convert.ToInt16(GridHospitalUsers.DataKeys[i].Value);
                                _objectBSMultiUserHospital.UpdatePermissionOnHospital(multiUserHospitalID, Convert.ToInt32(RMC.Web.PermissionType.ReadOnly), true, ActiveUser);
                            }
                        }
                    }
                    GridHospitalUsers.DataBind();
                }
            }
            catch (Exception ex)
            {

                ex.Data.Add("Events", "ButtonSubmit Click");
                ex.Data.Add("Page", "Administrator/ViewUsersByHospitals.aspx");
                LogManager._stringObject = "ViewUsersByHospitals.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);

            }
            finally
            {
                _objectBSMultiUserHospital = null;
            }
        }
        /// <summary>
        /// Add view permissions to users
        /// </summary>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 27, 2009</CreatedOn>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonAddViewPermission_Click(object sender, EventArgs e)
        {
            try
            {
                _objectBSMultiUserHospital =new RMC.BussinessService.BSMultiUserHospital();
                if (_objectBSMultiUserHospital.AddViewPermissionOnHospital(Convert.ToInt32(DropDownListHospital.SelectedValue), Convert.ToInt32(DropDownListUsers.SelectedValue), Convert.ToInt32(PermissionType.ReadOnly), ActiveUser) == true)
                {
                    DisplayMessage("Permission added successfully!", System.Drawing.Color.Green);
                }
                else
                {
                    DisplayMessage("Permission has already added!", System.Drawing.Color.Green);
                }
                BindUsers();
                GridHospitalUsers.DataBind();
            }
            catch (Exception ex)
            {

                ex.Data.Add("Events", "ButtonAddViewPermission_Click Click");
                ex.Data.Add("Page", "Administrator/ViewUsersByHospitals.aspx");
                LogManager._stringObject = "ViewUsersByHospitals.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);

            }
            finally
            {
                _objectBSMultiUserHospital = null;
            }
        }
        /// <summary>
        /// Bind Users to Dropdownlis
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BindUsers()
        {
            List<RMC.BusinessEntities.BEUserInfoTye> listUserInfo = null;
            try
            {
                _objectBSUsers = new RMC.BussinessService.BSUsers();
                listUserInfo = _objectBSUsers.GetUsersHavingPermission(Convert.ToInt32(DropDownListHospital.SelectedValue));
                DropDownListUsers.DataSource = listUserInfo;
                DropDownListUsers.DataTextField = "UserNameEmail";
                DropDownListUsers.DataValueField = "UserId";
                DropDownListUsers.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", " private void BindUsers()");
                ex.Data.Add("Class", "ManageHospitals.aspx");
                throw ex;
            }
            finally
            {
                _objectBSHospitalInfo = null;
                listUserInfo = null;
            }
        }
    }
}
