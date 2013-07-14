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
    public partial class ViewUsersByUnits : System.Web.UI.Page
    {
        #region Variables
        int _hospitalInfoId = 0;
        int _hospitalDemographicId = 0;
        RMC.BussinessService.BSUsers _objectBSUsers = null;
        RMC.BussinessService.BSMultiUserHospital _objectBSMultiUserHospital = null;
        RMC.BussinessService.BSMultiUserDemographic _objectBSMultiUserDemographic = null;
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
        /// <createdOn>July 28, 2009</createdOn>
        /// </summary>
        public int HospitalDemographicID
        {
            get
            {
                if (Request.QueryString["HospitalDemographicID"] != null)
                {
                    int.TryParse(Request.QueryString["HospitalDemographicID"], out _hospitalDemographicId);
                }
                return _hospitalDemographicId;
            }
        }
        /// <summary>
        /// Return true if Page has been opened for Creating Hospital
        /// <Author>Raman</Author>
        /// <createdOn>July 28, 2009</createdOn>
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
        /// <CreatedOn>July 28, 2009</CreatedOn>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //_objectBSMultiUserHospital = new RMC.BussinessService.BSMultiUserHospital();
                GridHospitalUnitUsers.PageSize = CommonClass.DefaultListingPageSize;
                DisplayMessage("", System.Drawing.Color.Green);
                if (!Page.IsPostBack)
                {
                    BindHospitals();
                    DropDownListHospitalUnits.SelectedValue = Convert.ToString(HospitalDemographicID);
                    BindUsers(HospitalDemographicID);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page Load");
                ex.Data.Add("Page", "ViewUsersByUnits.aspx");
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }
        /// <summary>
        /// Use to fill Units dropdownlist.
        /// Created By : Raman.
        /// Creation Date : July 28, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListHospital_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListUsers.Items.Count > 1)
                {
                    DropDownListUsers.Items.Clear();
                    DropDownListUsers.Items.Add("Select User");
                    DropDownListUsers.Items[0].Value = 0.ToString();
                }
                //UpdatePanelHospitalUnits.Update();
                DropDownListHospitalUnits.DataBind();
                if (DropDownListHospitalUnits.SelectedValue != "")
                {
                    BindUsers(Convert.ToInt32(DropDownListHospitalUnits.SelectedValue));
                }
                else
                {
                    BindUsers(0);
                }

            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "DropDownListHospital_SelectedIndexChanged");
                ex.Data.Add("Page", "ViewUsersByUnits.aspx");
                LogManager._stringObject = "ViewUsersByUnits.aspx ---- DropDownListHospital_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }
        #endregion
        
        #region Function
        /// <summary>
        /// Use to Display message.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="color"></param>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 28, 2009</CreatedOn>
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
                ex.Data.Add("Class", "ViewUsersByUnits.aspx");
                throw ex;
            }
        }
        /// <summary>
        /// Use Bind Hospital Dropdown
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="color"></param>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 28, 2009</CreatedOn>
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

        protected void ButtonApplyFilter_Click(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListUsers.Items.Count > 1)
                {
                    DropDownListUsers.Items.Clear();
                    DropDownListUsers.Items.Add("Select Users");
                    DropDownListUsers.Items[0].Value = 0.ToString();
                }
                if (DropDownListHospitalUnits.SelectedValue != "")
                {
                    BindUsers(Convert.ToInt32(DropDownListHospitalUnits.SelectedValue));
                }
                else
                {
                    BindUsers(0);
                }
                GridHospitalUnitUsers.DataBind();
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

        protected void ButtonAddViewPermission_Click(object sender, EventArgs e)
        {
            try
            {
                _objectBSMultiUserDemographic = new RMC.BussinessService.BSMultiUserDemographic();
                if (_objectBSMultiUserDemographic.AddViewPermissionOnHospitalUnits(Convert.ToInt32(DropDownListHospital.SelectedValue), Convert.ToInt32(DropDownListUsers.SelectedValue), Convert.ToInt32(PermissionType.ReadOnly), ActiveUser) == true)
                {
                    DisplayMessage("Permission added successfully!", System.Drawing.Color.Green);
                }
                else
                {
                    DisplayMessage("Permission has already added!", System.Drawing.Color.Green);
                }
                BindUsers(Convert.ToInt32(DropDownListHospitalUnits.SelectedValue));
                GridHospitalUnitUsers.DataBind();
            }
            catch (Exception ex)
            {

                ex.Data.Add("Events", "ButtonAddViewPermission_Click Click");
                ex.Data.Add("Page", "Administrator/ViewUsersByUnits.aspx");
                LogManager._stringObject = "ViewUsersByUnits.aspx ---- ButtonAddViewPermission_Click";
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
                if (GridHospitalUnitUsers.Rows.Count > 0)
                {
                    for (int i = 0; i < GridHospitalUnitUsers.Rows.Count; i++)
                    {

                        CheckBox CheckReadOnly = (GridHospitalUnitUsers.Rows[i].FindControl("CheckReadOnly")) as CheckBox;
                        if (CheckReadOnly != null)
                        {
                            if (CheckReadOnly.Checked == false)
                            {
                                _objectBSMultiUserDemographic = new RMC.BussinessService.BSMultiUserDemographic();
                                int multiUserHospitalDemographicID = Convert.ToInt16(GridHospitalUnitUsers.DataKeys[i].Value);
                                _objectBSMultiUserDemographic.UpdatePermissionOnHospitalUnits(multiUserHospitalDemographicID, Convert.ToInt32(RMC.Web.PermissionType.ReadOnly), true, ActiveUser);
                            }
                        }
                    }
                    GridHospitalUnitUsers.DataBind();
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

        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                BindHospitals();
                DropDownListHospitalUnits.DataBind();
                if (DropDownListHospitalUnits.Items.FindByValue(Convert.ToString(HospitalDemographicID)) != null)
                {
                    DropDownListHospitalUnits.SelectedValue = Convert.ToString(HospitalDemographicID);
                }
                if (DropDownListHospitalUnits.SelectedValue != "")
                {
                    BindUsers(Convert.ToInt32(DropDownListHospitalUnits.SelectedValue));
                }
                else
                {
                    BindUsers(0);
                }

                GridHospitalUnitUsers.DataBind();

            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "protected void ButtonReset_Click(object sender, GridViewRowEventArgs e)");
                ex.Data.Add("Page", "Administrator/ViewUsersByUnits.aspx");
                LogManager._stringObject = "ViewUsersByUnits.aspx ---- ButtonReset_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        protected void GridHospitalUnitUsers_RowDataBound(object sender, GridViewRowEventArgs e)
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
                ex.Data.Add("Events", "GridHospitalUnitUsers_RowDataBound(object sender, GridViewRowEventArgs e)");
                ex.Data.Add("Page", "Administrator/ViewUsersByUnits.aspx");
                LogManager._stringObject = "ViewUsersByUnits.aspx.aspx ---- GridHospitalUnitUsers_RowDataBound";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }
        

        protected void ObjectDataSourceHospitalUnits_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["HospitalInfoId"] = DropDownListHospital.SelectedValue;
               
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ObjectDataSourceHospitalUnits_Selecting");
                ex.Data.Add("Page", "ViewUsersByUnits.aspx");
                LogManager._stringObject = "ViewUsersByUnits.aspx ---- ObjectDataSourceHospitalUnits_Selecting";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }

        }

        protected void DropDownListHospitalUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
             
                if (DropDownListUsers.Items.Count > 1)
                {
                    DropDownListUsers.Items.Clear();
                    DropDownListUsers.Items.Add("Select Users");
                    DropDownListUsers.Items[0].Value = 0.ToString();
                }

                BindUsers(Convert.ToInt32(DropDownListHospitalUnits.SelectedValue));
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "DropDownListHospitalUnits_SelectedIndexChanged");
                ex.Data.Add("Page", "ViewUsersByUnits.aspx");
                LogManager._stringObject = "ViewUsersByUnits.aspx ---- DropDownListHospitalUnits_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        protected void ObjectDataSourceUsers_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["HospitalDemographicID"] = DropDownListHospitalUnits.SelectedValue;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ObjectDataSourceUsers_Selecting");
                ex.Data.Add("Page", "ViewUsersByUnits.aspx");
                LogManager._stringObject = "ViewUsersByUnits.aspx ---- ObjectDataSourceUsers_Selecting";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        /// <summary>
        /// Bind Users to Dropdownlis
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 28, 2009</CreatedOn>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BindUsers(int HospitalDemographicID)
        {
            List<RMC.BusinessEntities.BEUserInfoTye> listUserInfo = null;
            try
            {
                _objectBSUsers = new RMC.BussinessService.BSUsers();
                listUserInfo = _objectBSUsers.GetUsersHavingPermissionOnUnitsByHospitalDemographicId(HospitalDemographicID);
                if (listUserInfo != null)
                {
                    DropDownListUsers.DataSource = listUserInfo;
                    DropDownListUsers.DataTextField = "UserNameEmail";
                    DropDownListUsers.DataValueField = "UserId";
                    DropDownListUsers.DataBind();
                }
                else
                {
                    DropDownListUsers.DataBind();
                }
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

        protected void ObjectDataSourceHospitalUnits_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
          
            if (e.ReturnValue != null)
            {
                //List<RMC.BusinessEntities.BEHospitalDemographicInfo> listHospitalDemographicInfo = (List<RMC.BusinessEntities.BEHospitalDemographicInfo>)e.ReturnValue;
                //if (listHospitalDemographicInfo.Count > 0)
                //{
                //    if (listHospitalDemographicInfo.Where(d => d.HospitalDemographicID == HospitalDemographicID).SingleOrDefault() == null)
                //    {
                //        BindUsers(Convert.ToInt32(listHospitalDemographicInfo[0].HospitalDemographicID));
                //        //DropDownListHospitalUnits.SelectedValue = listHospitalDemographicInfo[0].HospitalDemographicID.ToString();
                //    }
                //    else
                //    {
                //        if (HospitalDemographicID > 0)
                //        {
                //            BindUsers(HospitalDemographicID);
                //        }
                //    }
                //}
               
            }
        }

       

    }
}
