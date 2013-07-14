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
    /// <summary>
    /// This page is for displaying hospital listing
    /// <Author>Raman</Author>
    /// <createdOn>July 22, 2009</createdOn>
    /// </summary>
    public partial class ManageHospitals : System.Web.UI.Page
    {
        #region Variables
        //Bussiness Service Objects.
        RMC.BussinessService.BSUsers _objectBSUsers = null;
        RMC.BussinessService.BSHospitalInfo _objectBSHospitalInfo = null;
        #endregion
        #region Properties
        /// <summary>
        /// Return Logged In user email
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>
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
                DisplayMessage("", System.Drawing.Color.Green);
                GridHospital.PageSize = CommonClass.DefaultListingPageSize;
                if (Page.IsPostBack == false)
                {
                    BindHospitals();
                    BindUsers();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page Load");
                ex.Data.Add("Page", "AManageHospitals.aspx");                
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }
        /// <summary>
        /// Bind grid according to the filter selected in dropdowns
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonApplyFilter_Click(object sender, EventArgs e)
        {
            try
            {
                BindHospitalGrid();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "protected void ButtonApplyFilter_Click(object sender, EventArgs e)");
                ex.Data.Add("Page", "ManageHospitals.aspx");
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }
        /// <summary>
        /// Reset Page Data
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                DropDownListHospital.ClearSelection();
                DropDownListHospital.Items[0].Selected = true;
                DropDownListAdmin.ClearSelection();
                DropDownListAdmin.Items[0].Selected = true;
                BindHospitalGrid();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "protected void ButtonReset_Click(object sender, EventArgs e)");
                ex.Data.Add("Page", "ManageHospitals.aspx");
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }
        /// <summary>
        /// Set active status of selected hospitals
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            RMC.BussinessService.BSHospitalInfo objectBSHospitalInfo = null;
            try
            {
                objectBSHospitalInfo = new RMC.BussinessService.BSHospitalInfo();
                if (GridHospital.Rows.Count > 0)
                {
                    for (int i = 0; i < GridHospital.Rows.Count; i++)
                    {


                        CheckBox _chk = (CheckBox)(GridHospital.Rows[i].FindControl("CheckIsActive"));

                        int hospitalInfoId = Convert.ToInt16(GridHospital.DataKeys[i].Value);
                        if (_chk.Checked)
                        {
                            objectBSHospitalInfo.UpdateHospitalActiveStatus(hospitalInfoId, true);
                        }
                        else
                        {
                            objectBSHospitalInfo.UpdateHospitalActiveStatus(hospitalInfoId, false);
                        }
                    }
                    GridHospital.DataBind();
                }

            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSubmit Click");
                ex.Data.Add("Page", "Administrator/ManageHospitals.aspx");
                LogManager._stringObject = "ManageHospitals.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);

            }
            finally
            {
                objectBSHospitalInfo = null;
            }
        }
        /// <summary>
        /// Add confirmation and commandargument with delete button
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>        
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridHospital_RowDataBound(object sender, GridViewRowEventArgs e)
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
                ex.Data.Add("Events", "protected void GridHospital_RowDataBound(object sender, GridViewRowEventArgs e)");
                ex.Data.Add("Page", "Administrator/ManageHospitals.aspx");
                LogManager._stringObject = "ManageHospitals.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }
        /// <summary>
        /// Delete selected record
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>        
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridHospital_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() == "logicallydelete")
                {
                    int hospitalInfoId = Convert.ToInt32(GridHospital.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);
                    _objectBSHospitalInfo = new RMC.BussinessService.BSHospitalInfo();
                    if (_objectBSHospitalInfo.DeleteHospitalInfo(hospitalInfoId, ActiveUser) == true)
                    {
                        DisplayMessage("Record has been deleted successfully!", System.Drawing.Color.Green);
                    }
                    else
                    {
                        DisplayMessage("Record has already been deleted!", System.Drawing.Color.Green);
                    }
                    GridHospital.DataBind();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "protected void GridHospital_RowCommand(object sender, GridViewRowEventArgs e)");
                ex.Data.Add("Page", "Administrator/ManageHospitals.aspx");
                LogManager._stringObject = "ManageHospitals.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
            finally
            {
                _objectBSHospitalInfo = null;
            }
        }
        #endregion
        #region Functions
        /// <summary>
        /// Use to Display message of Login Failure.
        /// <CreatedBy>Raman</CreatedBy>
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
                ex.Data.Add("Class", "ManageHospitals");
                throw ex;
            }
        }
        /// <summary>
        /// Bind Hospital Grid
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>
        private void BindHospitalGrid()
        {
            try
            {
                GridHospital.DataSourceID = LinqDataSourceHospital.ID;
                GridHospital.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "protected void ButtonReset_Click(object sender, EventArgs e)");
                ex.Data.Add("Page", "ManageHospitals.aspx");
                throw ex;
            }
        }
        /// <summary>
        /// Bind Hospitals to Dropdownlis
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BindHospitals()
        {
            try
            {
                _objectBSHospitalInfo = new RMC.BussinessService.BSHospitalInfo();

                if (DropDownListHospital.Items.Count > 0)
                {
                    DropDownListHospital.Items.Clear();
                }
                DropDownListHospital.DataSource = _objectBSHospitalInfo.GetHospitals(); ;
                DropDownListHospital.DataTextField = "HospitalExtendedName";
                DropDownListHospital.DataValueField = "HospitalInfoID";
                DropDownListHospital.DataBind();
                DropDownListHospital.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "private void BindHospitals()");
                ex.Data.Add("Class", "ManageHospitals.aspx");
                throw ex;
            }
            finally
            {
                _objectBSHospitalInfo = null;
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
                listUserInfo = _objectBSUsers.GetUsers();
                DropDownListAdmin.DataSource = listUserInfo;
                DropDownListAdmin.DataTextField = "UserNameEmail";
                DropDownListAdmin.DataValueField = "UserId";
                DropDownListAdmin.DataBind();
                DropDownListAdmin.Items.Insert(0, new ListItem("Select", "0"));
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
        #endregion
    }
}
