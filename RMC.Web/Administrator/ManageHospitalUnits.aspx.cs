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
    public partial class ManageUnits : System.Web.UI.Page
    {
        #region Variables
        int _hospitalInfoId = 0;

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
                GridHospitalUnit.PageSize = CommonClass.DefaultListingPageSize;
                DisplayMessage("", System.Drawing.Color.Green);
                if (Page.IsPostBack == false)
                {                    
                    BindHospitals();
                    if (DropDownListHospital.Items.FindByValue(Convert.ToString(HospitalInfoId)) != null)
                    {
                        DropDownListHospital.SelectedValue = Convert.ToString(HospitalInfoId);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page Load");
                ex.Data.Add("Page", "ManageHospitalUnits.aspx");
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
        protected void GridHospitalUnit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.ToLower() == "logicallydelete")
                {
                    int hospitalDemographicId = Convert.ToInt32(GridHospitalUnit.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);
                    _objectBSHospitalDemographicDetail = new RMC.BussinessService.BSHospitalDemographicDetail();
                    if (_objectBSHospitalDemographicDetail.DeleteHospitalDemographicInfo(hospitalDemographicId, ActiveUser) == true)
                    {
                        DisplayMessage("Record has been deleted successfully!", System.Drawing.Color.Green);
                    }
                    else
                    {
                        DisplayMessage("Record has already been deleted!", System.Drawing.Color.Green);
                    }
                    GridHospitalUnit.DataBind();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "protected void GridHospitalUnit_RowCommand(object sender, GridViewRowEventArgs e)");
                ex.Data.Add("Page", "Administrator/ManageHospitalUnits.aspx");
                LogManager._stringObject = "ManageHospitalUnits.aspx.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }
        /// <summary>
        /// Add confirmation and commandargument with delete button
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="color"></param>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        protected void GridHospitalUnit_RowDataBound(object sender, GridViewRowEventArgs e)
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
                ex.Data.Add("Events", "protected void GridHospitalUnit_RowDataBound(object sender, GridViewRowEventArgs e)");
                ex.Data.Add("Page", "Administrator/ManageHospitalUnits.aspx");
                LogManager._stringObject = "ManageHospitalUnits.aspx ---- Page_Load";
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
                GridHospitalUnit.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", " protected void ButtonApplyFilter_Click(object sender, EventArgs e)");
                LogManager._stringObject = "ManageHospitalUnits.ascx.cs ---- ";
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
                ex.Data.Add("Function", "BindHospitals");
                ex.Data.Add("Class", "ManageHospitals");
                throw ex;
            }
        }
        #endregion
    }
}
