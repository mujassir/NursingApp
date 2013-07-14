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
using LogExceptions;

namespace RMC.Web.Users
{
    public partial class EditOrViewDemographicDetail : System.Web.UI.Page
    {

        #region Variables

        //Bussiness Service Objects. 
        RMC.BussinessService.BSCommon _objectBSCommon = null;
        RMC.BussinessService.BSPermission _objectBSPermission = null;
        RMC.BussinessService.BSHospitalInfo _objectBSHospitalInfo = null;
        RMC.BussinessService.BSHospitalDemographicDetail _objectBSHospitalDemgraphicDetail = null;
        RMC.BussinessService.BSMultiUserDemographic _objectBSMultiUserDemographic = null;

        //Data Service Objects.
        RMC.DataService.HospitalDemographicInfo _objectHospitalDemographicInfo = null;
        RMC.DataService.MultiUserDemographic _objectMultiUserDemographic = null;

        //Generic Data Service Objects.
        List<RMC.DataService.HospitalInfo> _objectGenericHospitalInfo = null;
        List<RMC.BusinessEntities.BEHospitalList> _objectGenericBEHospitalList = null;

        //Fundamental Data Types
        bool _flag;
        string _permissionName;

        #endregion

        #region Events

       
     

        /// <summary>
        /// Page Load Events.
        /// Created By : Davinder Kumar
        /// Creation Date : June 25, 2009
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 21, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                _flag = false;
                if (Session["UserInformation"] != null)
                {
                    if (Request.QueryString.Count > 0)
                    {
                        _objectBSMultiUserDemographic = new RMC.BussinessService.BSMultiUserDemographic();
                        int hospitalDemographicDetailID = Convert.ToInt32(Request.QueryString["HospitalDemographicDetailID"]);
                        int permissionID = Convert.ToInt32(Request.QueryString["PermissionID"]);
                        int userID = CommonClass.UserInformation.UserID;
                        _flag = _objectBSMultiUserDemographic.CheckPermissionOnHospitalDemographicDetailByUserID(userID, hospitalDemographicDetailID, permissionID);
                    }
                    else
                    {
                        CommonClass.SessionInfomation.ErrorMessage = "Invalid Hospital Information and User Permission.";
                        Response.Redirect("~/Users/ErrorMessage.aspx", false);
                    }
                }
                else
                {
                    CommonClass.SessionInfomation.ErrorMessage = "Invalid User Information.";
                    Response.Redirect("~/Users/ErrorMessage.aspx", false);
                }

                if (_flag)
                {
                    _objectBSPermission = new RMC.BussinessService.BSPermission();
                    int permissionID = Convert.ToInt32(Request.QueryString["PermissionID"]);
                    //_permissionName = _objectBSPermission.GetPermissionByPermissionID(permissionID);
                    //if (_permissionName.ToLower().Trim() == "readonly")
                    //{
                    //    MultiViewEditOrViewDemographic.ActiveViewIndex = 0;
                    //}
                    //else
                    //{
                    //    MultiViewEditOrViewDemographic.ActiveViewIndex = 1;
                    //}
                }
                else
                {
                    CommonClass.SessionInfomation.ErrorMessage = "Invalid User Permission.";
                    Response.Redirect("~/Users/ErrorMessage.aspx", false);
                }

                if (!Page.IsPostBack)
                {
                    int userID = CommonClass.UserInformation.UserID;
                    GetAllHospitalNames(userID);
                    TextBoxStartDate.Text = DateTime.Now.ToShortDateString();
                    PopulateDataForEdit(userID);
                }

                //Add Javascript Event to restrict the special character in Textboxes.
                TextBoxStartDate.Attributes.Add("readonly", "readonly");
                TextBoxEndDate.Attributes.Add("readonly", "readonly");
                TextBoxUnitName.Attributes.Add("onKeyDown", "return FilterAlphaNumeric(event);");
                //TextBoxUnitType.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
                TextBoxPharmacyType.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "EditOrViewDemographicDetail.aspx");
                LogManager._stringObject = "EditOrViewDemographicDetail.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        /// <summary>
        /// Load Event for view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ViewReadOnly_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    int userID = CommonClass.UserInformation.UserID;
                    PopulateDataForView(userID);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ViewReadOnly_Load");
                ex.Data.Add("Page", "EditOrViewDemographicDetail.aspx");
                LogManager._stringObject = "EditOrViewDemographicDetail.aspx ---- ViewReadOnly_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        /// <summary>
        /// Load Event for Edit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ViewEdit_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    int userID = CommonClass.UserInformation.UserID;
                    PopulateDataForEdit(userID);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ViewEdit_Load");
                ex.Data.Add("Page", "EditOrViewDemographicDetail.aspx");
                LogManager._stringObject = "EditOrViewDemographicDetail.aspx ---- ViewEdit_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Bind DropDown list with HospitalInfo.
        /// Created By : Davinder Kumar.
        /// Creation Date : June 25, 2009
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 21, 2009.
        /// </summary>
        /// <param name="objectGenericHospitalInfo"></param>
        private void BindDropDownListHospitalNames(List<RMC.BusinessEntities.BEHospitalList> objectGenericHospitalInfo)
        {
            try
            {
                if (DropDownListHospitalName.Items.Count > 1)
                {
                    DropDownListHospitalName.Items.Clear();
                    DropDownListHospitalName.Items.Add("Select Hospital");
                    DropDownListHospitalName.Items[0].Value = 0.ToString();
                    DropDownListHospitalName.Items[0].Selected = true;
                }

                DropDownListHospitalName.DataTextField = "HospitalExtendedName";
                DropDownListHospitalName.DataValueField = "HospitalInfoID";
                DropDownListHospitalName.DataSource = objectGenericHospitalInfo;
                DropDownListHospitalName.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "BindDropDownListHospitalNames");
                ex.Data.Add("Class", "EditOrViewDemographicDetail");
                throw ex;
            }
            finally
            {
                objectGenericHospitalInfo = null;
            }
        }

        /// <summary>
        /// Use to Display message of Login Failure.
        /// Created By : Davinder Kumar.
        /// Creation Date : June 25, 2009
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 21, 2009.
        /// </summary>
        /// <param name="msg">Message</param>
        /// <param name="color">Color</param>
        private void DisplayMessage(string msg, System.Drawing.Color color)
        {
            try
            {
                LabelErrorMsg.Text = msg;
                PanelErrorMsg.ForeColor = color;
                PanelErrorMsg.Visible = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DisplayMessage");
                ex.Data.Add("Class", "EditOrViewDemographicDetail");
                throw ex;
            }
        }

        /// <summary>
        /// Use to fetch all hospital names.
        /// Created By : Davinder Kumar.
        /// Creation Date : June 25, 2009.
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 21, 2009.
        /// </summary>
        private void GetAllHospitalNames(int userID)
        {
            try
            {
                _objectBSHospitalInfo = new RMC.BussinessService.BSHospitalInfo();

                _objectGenericBEHospitalList = _objectBSHospitalInfo.GetHospitalNamesByUserID(userID);

                if (_objectGenericBEHospitalList.Count > 0)
                {
                    BindDropDownListHospitalNames(_objectGenericBEHospitalList);
                }
                else
                {
                    DisplayMessage("Hospital does not exist.", System.Drawing.Color.Red);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllHospitalNames");
                ex.Data.Add("Class", "EditOrViewDemographicDetail");
                throw ex;
            }
            finally
            {
                _objectBSHospitalInfo = null;
            }
        }

        /// <summary>
        /// Fetch Data for view only.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 29, 2009.
        /// </summary>
        /// <param name="userID"></param>
        private void PopulateDataForView(int userID)
        {
            try
            {
                //_objectBSHospitalDemgraphicDetail = new RMC.BussinessService.BSHospitalDemographicDetail();
                //int hospitalDemographicDetailID = Convert.ToInt32(Request.QueryString["HospitalDemographicDetailID"]);

                //_objectHospitalDemographicInfo = _objectBSHospitalDemgraphicDetail.GetHospitalDemographicDetailByUserID(userID, hospitalDemographicDetailID);
                ////LabelBedInUnit.Text = Convert.ToString(_objectHospitalDemographicInfo.BedsInHospital);
                //LabelBedsInHospital.Text = Convert.ToString(_objectHospitalDemographicInfo.BedsInUnit);
                //LabelBudgetedPatientsPerNurse.Text = Convert.ToString(_objectHospitalDemographicInfo.BudgetedPatientsPerNurse);
                ////LabelDemographic.Text = _objectHospitalDemographicInfo.Demographic;
                //LabelElectronicDocumentation.Text = Convert.ToString(_objectHospitalDemographicInfo.ElectronicDocumentation);
                //LabelEndDate.Text = Convert.ToString(_objectHospitalDemographicInfo.EndedDate);
                //LabelHospitalName.Text = _objectHospitalDemographicInfo.HospitalInfo.HospitalName;
                //LabelPharmacyType.Text = _objectHospitalDemographicInfo.PharmacyType;
                //LabelStartDate.Text = Convert.ToString(_objectHospitalDemographicInfo.StartDate);
                //LabelUnitName.Text = _objectHospitalDemographicInfo.HospitalUnitName;
                //LabelUnitType.Text = _objectHospitalDemographicInfo.UnitType;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllHospitalNames");
                ex.Data.Add("Class", "EditOrViewDemographicDetail");
                throw ex;
            }
        }

        /// <summary>
        /// Fetch Data for view only.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 29, 2009.
        /// </summary>
        /// <param name="userID"></param>
        private void PopulateDataForEdit(int userID)
        {
            try
            {
                _objectBSHospitalDemgraphicDetail = new RMC.BussinessService.BSHospitalDemographicDetail();
                int hospitalDemographicDetailID = Convert.ToInt32(Request.QueryString["HospitalDemographicDetailID"]);

                _objectHospitalDemographicInfo = _objectBSHospitalDemgraphicDetail.GetHospitalDemographicDetailByUserID(userID, hospitalDemographicDetailID);
                TextBoxBedsInUnit.Text = Convert.ToString(_objectHospitalDemographicInfo.BedsInUnit);
                //TextBoxBedsInHospital.Text = Convert.ToString(_objectHospitalDemographicInfo.BedsInHospital);
                TextBoxPatientsPerNurse.Text = Convert.ToString(_objectHospitalDemographicInfo.BudgetedPatientsPerNurse);
                //TextBoxDemographic.Text = _objectHospitalDemographicInfo.Demographic;
                TextBoxElectronicDocumentation.Text = Convert.ToString(_objectHospitalDemographicInfo.ElectronicDocumentation);
                TextBoxEndDate.Text = Convert.ToDateTime(_objectHospitalDemographicInfo.EndedDate).ToShortDateString();
                DropDownListHospitalName.SelectedIndex = DropDownListHospitalName.Items.IndexOf(DropDownListHospitalName.Items.FindByText(_objectHospitalDemographicInfo.HospitalInfo.HospitalName));
                TextBoxPharmacyType.Text = _objectHospitalDemographicInfo.PharmacyType;
                TextBoxStartDate.Text = Convert.ToDateTime(_objectHospitalDemographicInfo.StartDate).ToShortDateString();
                TextBoxUnitName.Text = _objectHospitalDemographicInfo.HospitalUnitName;
                ListBoxUnitType.Text = _objectHospitalDemographicInfo.UnitType;

                CheckBoxDocByException.Checked = _objectHospitalDemographicInfo.DocByException;
                CheckBoxTCABUnit.Checked = _objectHospitalDemographicInfo.TCABUnit;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllHospitalNames");
                ex.Data.Add("Class", "EditOrViewDemographicDetail");
                throw ex;
            }
        }

        /// <summary>
        /// Save Hospital Demographic Detail In a Data service object.
        /// Created By : Davinder Kumar.
        /// Creation Date : June 25, 2009
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 10, 2009.
        /// </summary>
        /// <returns></returns>
        private RMC.DataService.HospitalDemographicInfo UpdateHospitalDemographicDetail()
        {
            _objectHospitalDemographicInfo = new RMC.DataService.HospitalDemographicInfo();
            try
            {
                int intReturn;
                short shortReturn;
                double doubleReturn;
                DateTime datTimeReturn;
                bool flag = false;

                int userID = CommonClass.UserInformation.UserID;
                int hospitalDemographicDetailID = Convert.ToInt32(Request.QueryString["HospitalDemographicDetailID"]);
                string userName = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
               // flag = int.TryParse(TextBoxBedsInHospital.Text, out intReturn);

                _objectHospitalDemographicInfo.HospitalDemographicID = hospitalDemographicDetailID;
                if (flag)
                {
                    // _objectHospitalDemographicInfo.BedsInHospital = intReturn;
                }
                else
                {
                    //_objectHospitalDemographicInfo.BedsInHospital = 0;
                }

                flag = short.TryParse(TextBoxBedsInUnit.Text, out shortReturn);
                if (flag)
                {
                    _objectHospitalDemographicInfo.BedsInUnit = shortReturn;
                }
                else
                {
                    _objectHospitalDemographicInfo.BedsInUnit = 0;
                }

                flag = double.TryParse(TextBoxPatientsPerNurse.Text, out doubleReturn);
                if (flag)
                {
                    _objectHospitalDemographicInfo.BudgetedPatientsPerNurse = doubleReturn;
                }
                else
                {
                    _objectHospitalDemographicInfo.BudgetedPatientsPerNurse = 0.00;
                }

                _objectHospitalDemographicInfo.CreatedBy = userName;
                //_objectHospitalDemographicInfo.Demographic = TextBoxDemographic.Text;
                _objectHospitalDemographicInfo.DocByException = CheckBoxDocByException.Checked;

                flag = short.TryParse(TextBoxElectronicDocumentation.Text, out shortReturn);
                if (flag)
                {
                    _objectHospitalDemographicInfo.ElectronicDocumentation = shortReturn;
                }
                else
                {
                    _objectHospitalDemographicInfo.ElectronicDocumentation = 0;
                }

                flag = DateTime.TryParse(TextBoxStartDate.Text, out datTimeReturn);
                if (flag)
                {
                    _objectHospitalDemographicInfo.StartDate = datTimeReturn;
                }
                else
                {
                    _objectHospitalDemographicInfo.StartDate = DateTime.Now;
                }

                flag = DateTime.TryParse(TextBoxEndDate.Text, out datTimeReturn);
                if (flag)
                {
                    _objectHospitalDemographicInfo.IsEndDate = true;
                    _objectHospitalDemographicInfo.EndedDate = datTimeReturn;
                }
                else
                {
                    _objectHospitalDemographicInfo.IsEndDate = false;
                }

                _objectHospitalDemographicInfo.HospitalInfoID = Convert.ToInt32(DropDownListHospitalName.SelectedValue);
                _objectHospitalDemographicInfo.HospitalUnitName = TextBoxUnitName.Text;
                _objectHospitalDemographicInfo.IsDeleted = false;
                _objectHospitalDemographicInfo.PharmacyType = TextBoxPharmacyType.Text;
                _objectHospitalDemographicInfo.TCABUnit = CheckBoxTCABUnit.Checked;
                _objectHospitalDemographicInfo.UnitType = ListBoxUnitType.Text;
                _objectHospitalDemographicInfo.CreatedDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveHospitalDemographicDetail");
                ex.Data.Add("Class", "EditOrViewDemographicDetail");
                throw ex;
            }

            return _objectHospitalDemographicInfo;
        }

        #endregion
       
       
       
        /// <summary>
        /// Save Data of Hospital Demographic Detail.
        /// Created By : Davinder Kumar.
        /// Creation Date : June 25, 2009.
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 21, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    bool flag = false;
                    _objectBSHospitalDemgraphicDetail = new RMC.BussinessService.BSHospitalDemographicDetail();

                    flag = _objectBSHospitalDemgraphicDetail.UpdateHospitalDemographicInformation(UpdateHospitalDemographicDetail());
                    if (flag)
                    {
                        DisplayMessage("Update Hospital Demographic Detail Successfully.", System.Drawing.Color.Green);
                        int userID = CommonClass.UserInformation.UserID;
                        PopulateDataForEdit(userID);
                    }
                    else
                    {
                        DisplayMessage("Fail to Update Hospital Demographic Detail.", System.Drawing.Color.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSave_Click");
                ex.Data.Add("Page", "EditOrViewDemographicDetail.aspx");
                LogManager._stringObject = "EditOrViewDemographicDetail.aspx ---- ButtonSave_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        /// <summary>
        /// Reset all controls.
        /// Created By : Davinder Kumar
        /// Creation Date : June 25, 2009
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 21, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    bool flag = false;
                    _objectBSHospitalDemgraphicDetail = new RMC.BussinessService.BSHospitalDemographicDetail();

                    flag = _objectBSHospitalDemgraphicDetail.UpdateHospitalDemographicInformation(UpdateHospitalDemographicDetail());
                    if (flag)
                    {
                        DisplayMessage("Update Hospital Demographic Detail Successfully.", System.Drawing.Color.Green);
                        int userID = CommonClass.UserInformation.UserID;
                        PopulateDataForEdit(userID);
                    }
                    else
                    {
                        DisplayMessage("Fail to Update Hospital Demographic Detail.", System.Drawing.Color.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSave_Click");
                ex.Data.Add("Page", "EditOrViewDemographicDetail.aspx");
                LogManager._stringObject = "EditOrViewDemographicDetail.aspx ---- ButtonSave_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        protected void ButtonReset1_Click(object sender, EventArgs e)
        {
            try
            {
                int userID = CommonClass.UserInformation.UserID;
                PopulateDataForEdit(userID);
                LabelErrorMsg.Text = string.Empty;
                PanelErrorMsg.Visible = false;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonReset_Click");
                ex.Data.Add("Page", "EditOrViewDemographicDetail.aspx");
                LogManager._stringObject = "EditOrViewDemographicDetail.aspx ---- ButtonReset_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

    }
}
