using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogExceptions;
using RMC.BussinessService;
using System.IO;

namespace RMC.Web.UserControls
{
    public partial class HospitalDemographicInfo : System.Web.UI.UserControl
    {

        #region Variables
        //Bussiness Service Objects.
        //BSCommon _objectBSCommon;
        RMC.BussinessService.BSHospitalInfo _objectBSHospitalInfo = null;
        RMC.DataService.HospitalDemographicInfo _objectHospitalDemographicInfo = null;
        RMC.BussinessService.BSHospitalDemographicDetail _objectBSHospitalDemographicDetail = null;
        RMC.BussinessService.BSPharmacyType _objectBSPharmacyType = null;


        #endregion

        #region Properties

        public string ActiveUser
        {
            get
            {
                return ((List<RMC.DataService.UserInfo>)(BSSerialization.Deserialize<List<RMC.DataService.UserInfo>>(HttpContext.Current.Session["UserInformation"] as MemoryStream))).Count > 0 ? CommonClass.UserInformation.Email : "";
            }
        }

        public RMC.DataService.UserInfo UserInfo
        {
            get
            {
                return CommonClass.UserInformation;
            }
        }

        private int HospitalDemographicId
        {
            get
            {
                return Request.QueryString["HospitalDemographicId"] != null ? Convert.ToInt32(Request.QueryString["HospitalDemographicId"].ToString()) : 0;
                // return ViewState["HospitalDemographicId"] != null ? Convert.ToInt32(ViewState["HospitalDemographicId"]) : 0;
            }
            set
            {
                ViewState["HospitalDemographicId"] = value;
            }
        }

        private RMC.DataService.HospitalInfo HospitalInformation
        {
            get
            {
                int hospitalUnitID = 0;
                RMC.DataService.HospitalInfo objectHospitalInfo = null;
                RMC.BussinessService.BSHospitalInfo objectBSHospitalInfo = new RMC.BussinessService.BSHospitalInfo();
                try
                {
                    int.TryParse(Convert.ToString(Request.QueryString["HospitalDemographicId"]), out hospitalUnitID);
                    objectHospitalInfo = objectBSHospitalInfo.GetHospitalInfoByHospitalUnitID(hospitalUnitID);

                    if (objectHospitalInfo != null)
                    {
                        return objectHospitalInfo;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private int PermissionID
        {
            get
            {
                return Request.QueryString["PermissionID"] != null ? Convert.ToInt32(Request.QueryString["PermissionID"].ToString()) : 0;
                // return ViewState["HospitalDemographicId"] != null ? Convert.ToInt32(ViewState["HospitalDemographicId"]) : 0;
            }
            set
            {
                ViewState["PermissionID"] = value;
            }
        }

        public bool CreateHospitalUnit
        {
            get
            {
                if (Request.QueryString["CreateHospitalUnit"] != null)
                {
                    return Convert.ToString(Request.QueryString["CreateHospitalUnit"]).ToUpper() == "Y";
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region Functions

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
                PanelErrorMsg.Visible = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DisplayMessage");
                LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                throw ex;
            }
        }

        /// <summary>
        /// To Bind Pharmacy in List
        /// </summary>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>13 August, 2009</CreatedOn>        
        private void BindPharmacyType()
        {
            try
            {
                List<RMC.DataService.PharmacyType> _objecDSPharmacyType = new List<RMC.DataService.PharmacyType>();
                _objectBSPharmacyType = new RMC.BussinessService.BSPharmacyType();
                int kcount = 0;
                _objecDSPharmacyType = _objectBSPharmacyType.GetAllPharmacyType();
                if (_objecDSPharmacyType.Count > 0)
                {
                    ListBoxPharmacyType.Items.Clear();
                }
                foreach (RMC.DataService.PharmacyType _objectPharmacy in _objecDSPharmacyType)
                {
                    ListBoxPharmacyType.Items.Insert(kcount, new ListItem(_objectPharmacy.PharmacyName, Convert.ToString(_objectPharmacy.PharmacyTypeID)));
                    kcount = kcount + 1;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "BindPharmacyType");
                LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                throw ex;
            }
        }


        private void SetControlAccessByPermission()
        {
            if (PermissionID != 1)
            {
                TextBoxUnitName.Enabled = false;
                CheckBoxTCABUnit.Enabled = false;
                TextBoxBedsInUnit.Enabled = false;
                TextBoxElectronicDocumentation.Enabled = false;
                TextBoxPatientsPerNurse.Enabled = false;
                TextBoxStartDate.Enabled = false;
                TextBoxEndDate.Enabled = false;
                ListBoxUnitType.Enabled = false;
                ListBoxPharmacyType.Enabled = false;
                CheckBoxDocByException.Enabled = false;
                //ListBoxApprovedUsers.Enabled = true;
                DataListApprovedUsers.Enabled = true;
                LinkButtonAddPharmacy.Visible = false;
                LinkButtonApproveUser.Visible = false;
                LinkButtonRegisterNewUser.Visible = false;
                LinkButtonUnitType.Visible = false;
                ButtonSave.Visible = false;
                divSaveDisable.Visible = true;
            }
            else
            {
                TextBoxUnitName.Enabled = true;
                CheckBoxTCABUnit.Enabled = true;
                TextBoxBedsInUnit.Enabled = true;
                TextBoxElectronicDocumentation.Enabled = true;
                TextBoxPatientsPerNurse.Enabled = true;
                TextBoxStartDate.Enabled = true;
                TextBoxEndDate.Enabled = true;
                ListBoxUnitType.Enabled = true;
                ListBoxPharmacyType.Enabled = true;
                CheckBoxDocByException.Enabled = true;
                //ListBoxApprovedUsers.Enabled = true;
                DataListApprovedUsers.Enabled = true;
                LinkButtonAddPharmacy.Visible = false;
                LinkButtonApproveUser.Visible = true;
                LinkButtonRegisterNewUser.Visible = true;
                LinkButtonUnitType.Visible = false;
                ButtonSave.Visible = true;
                divSaveDisable.Visible = false;
            }
        }

        /// <summary>
        /// To populate user information on page controls
        /// </summary>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 24, 2009</CreatedOn>
        /// <Modified By>Raman</ModifiedBy>
        /// <ModifiedOn>August 12, 2009</ModifiedOn>
        private void PopulateData()
        {
            bool isExist = false;
            string[] unitType;
            string[] pharmacyType;
            try
            {
                RMC.BussinessService.BSCommon objectBSCommon = new RMC.BussinessService.BSCommon();
                _objectBSHospitalDemographicDetail = new RMC.BussinessService.BSHospitalDemographicDetail();
                _objectHospitalDemographicInfo = new RMC.DataService.HospitalDemographicInfo();

                int ownerID = objectBSCommon.GetOwnerIDByHospitalUnitID(HospitalDemographicId);
                _objectHospitalDemographicInfo = _objectBSHospitalDemographicDetail.GetHospitalDemographicDetail(HospitalDemographicId);
                if (_objectHospitalDemographicInfo != null)
                {
                    //DropDownListHospital.SelectedValue = Convert.ToString(_objectHospitalDemographicInfo.HospitalInfoID);
                    TextBoxUnitName.Text = Convert.ToString(_objectHospitalDemographicInfo.HospitalUnitName);
                    CheckBoxTCABUnit.Checked = _objectHospitalDemographicInfo.TCABUnit;
                    TextBoxBedsInUnit.Text = Convert.ToString(_objectHospitalDemographicInfo.BedsInUnit);
                    CheckBoxDocByException.Checked = _objectHospitalDemographicInfo.DocByException;
                    //// For adding items in List Unit Type
                    if (_objectHospitalDemographicInfo.UnitType != "")
                    {
                        unitType = _objectHospitalDemographicInfo.UnitType.Split(',');

                        for (int count = 0; count < unitType.Length; count++)
                        {
                            for (int kcount = 0; kcount < ListBoxUnitType.Items.Count; kcount++)
                            {
                                if (unitType[count].Equals(ListBoxUnitType.Items[kcount].Text))
                                {
                                    ListBoxUnitType.Items[kcount].Selected = true;
                                    break;
                                }
                            }

                        }
                    }

                    TextBoxElectronicDocumentation.Text = Convert.ToString(_objectHospitalDemographicInfo.ElectronicDocumentation);
                    TextBoxPatientsPerNurse.Text = Convert.ToString(_objectHospitalDemographicInfo.BudgetedPatientsPerNurse);

                    //// For adding items in List Pharmacy Type 
                    if (_objectHospitalDemographicInfo.PharmacyType != "")
                    {
                        pharmacyType = _objectHospitalDemographicInfo.PharmacyType.Split(',');

                        for (int count = 0; count < pharmacyType.Length; count++)
                        {
                            for (int kcount = 0; kcount < ListBoxPharmacyType.Items.Count; kcount++)
                            {
                                if (pharmacyType[count].Equals(ListBoxPharmacyType.Items[kcount].Text))
                                {
                                    ListBoxPharmacyType.Items[kcount].Selected = true;
                                    break;
                                }
                            }
                        }
                    }

                    if (_objectHospitalDemographicInfo.StartDate == null || Convert.ToDateTime(_objectHospitalDemographicInfo.StartDate).ToShortDateString() == Convert.ToDateTime("01/01/1901").ToShortDateString())
                    {
                        TextBoxStartDate.Text = string.Empty;
                    }
                    else
                    {
                        string startDate = _objectHospitalDemographicInfo.StartDate.Value.ToString("MM/dd/yyyy", new System.Globalization.CultureInfo("en-US"));
                        TextBoxStartDate.Text = startDate;
                    }
                    if (Convert.ToDateTime(_objectHospitalDemographicInfo.EndedDate) == Convert.ToDateTime("01/01/1901"))
                    {
                        ViewState["EndDate"] = "01/01/1901";
                        TextBoxEndDate.Text = string.Empty;
                    }
                    else if (_objectHospitalDemographicInfo.EndedDate == null)
                    {
                        ViewState["EndDate"] = "01/01/1901";
                        TextBoxEndDate.Text = string.Empty;
                    }
                    else
                    {
                        ViewState["EndDate"] = null;
                        string endDate = _objectHospitalDemographicInfo.EndedDate.Value.ToString("MM/dd/yyyy", new System.Globalization.CultureInfo("en-US"));
                        TextBoxEndDate.Text = endDate;
                    }
                    //if (DropDownListOwner.Items.Count > 0)
                    //{
                    //    for (int count = 0; count < DropDownListOwner.Items.Count; count++)
                    //    {
                    //        if (Convert.ToString(_objectHospitalDemographicInfo.HospitalInfo.UserID) == DropDownListOwner.Items[count].Value)
                    //        {
                    //            isExist = true;
                    //            break;
                    //        }
                    //    }
                    //}
                    //if (isExist)
                    //    DropDownListOwner.SelectedValue = Convert.ToString(_objectHospitalDemographicInfo.HospitalInfo.UserID);
                   
                    // Commented by Mahesh Sachdeva due to change in busssiness logic  
                    //if (DropDownListOwner.Items.Count > 0)
                    //{
                    //    try
                    //    {
                    //        DropDownListOwner.Items.FindByValue(ownerID.ToString()).Selected = true;
                    //    }
                    //    catch
                    //    {
                    //        DropDownListOwner.SelectedIndex = 0;
                    //    }
                    //}
                }
                else
                {
                    ResetDate();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "PopulateData");
                LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                throw ex;
            }
            finally
            {
                _objectBSHospitalDemographicDetail = null;
                _objectHospitalDemographicInfo = null;
            }
        }

        /// <summary>
        /// To Reset controls
        /// </summary>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 24, 2009</CreatedOn>
        /// <Modified By>Raman</ModifiedBy>
        /// <ModifiedOn>August 12, 2009</ModifiedOn>
        private void ResetDate()
        {
            try
            {
                TextBoxUnitName.Text = "";
                CheckBoxTCABUnit.Checked = false;
                TextBoxBedsInUnit.Text = "";
                TextBoxElectronicDocumentation.Text = "";
                TextBoxPatientsPerNurse.Text = "";
                TextBoxStartDate.Text = "";
                TextBoxEndDate.Text = "";
                ListBoxUnitType.ClearSelection();
                ListBoxPharmacyType.ClearSelection();
                CheckBoxDocByException.Checked = false;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ResetDate");
                LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                throw ex;
            }

        }

        /// <summary>
        /// To Bind Approved Users
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>August 13, 2009</CreatedOn>
        private void GetApprovedUsersList()
        {
            try
            {
                RMC.BussinessService.BSTreeView objectTreeView = new RMC.BussinessService.BSTreeView();
                List<RMC.BusinessEntities.BEHospitalMembers> objectApprovedUserList = objectTreeView.GetAllMembersOfHospitalUnit(HospitalDemographicId);
                if (objectApprovedUserList != null)
                {
                    if (objectApprovedUserList.Count > 0)
                    {
                        int count = 0;
                        DropDownListOwner.Items.Clear();
                        DropDownListOwner.Items.Insert(count, new ListItem("Select Owner", Convert.ToString(0)));
                        count++;
                        foreach (RMC.BusinessEntities.BEHospitalMembers objectApproved in objectApprovedUserList)
                        {
                            //ListBoxApprovedUsers.Items.Insert(count, new ListItem(objectApproved.UserName, Convert.ToString(objectApproved.UserID)));
                            DropDownListOwner.Items.Insert(count, new ListItem(objectApproved.UserName, Convert.ToString(objectApproved.UserID)));
                            count = count + 1;
                        }
                    }
                    // DropDownListOwner.Items.Insert(0, new ListItem("Select Owner Name", "Select Owner Name"));

                }
                
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetApprovedUsersList");
                LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                throw ex;
            }

        }

        /// <summary>
        /// 
        /// </summary>
         
        /// Commented By Mahesh Sachdeva
        /// Commented Because Now check boxes are Removed
        private void SetPermissionForApprovedList()
        {
            try
            {
                if (PermissionID > 1)
                {
                    if (DataListApprovedUsers.Items.Count > 0)
                    {
                        foreach (DataListItem datList in DataListApprovedUsers.Items)
                        {
                            CheckBox chkBox = (CheckBox)datList.FindControl("CheckBoxApproval");
                            chkBox.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SetPermissionForApprovedList");
                LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                throw ex;
            }
        }

        private void DisableLinkButtonInApprovedUser()
        {
            try
            {
                if (DataListApprovedUsers.Items.Count > 0)
                {
                    foreach (DataListItem datList in DataListApprovedUsers.Items)
                    {
                        LinkButton lnkButtonApprovedUser = (LinkButton)datList.FindControl("LinkButtonApprovedUser");
                        LinkButton lnkButtonForApprovedUser = (LinkButton)datList.FindControl("LinkButtonForApprovedUser");

                        lnkButtonApprovedUser.Visible = false;
                        lnkButtonForApprovedUser.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            int argHospitalDemographicId = 0;
            try
            {
                LiteralHospitalName.Text = HospitalInformation.HospitalName;
                if (Page.IsPostBack == false)
                {
                    CommonClass objectCommonClass = new CommonClass();
                    objectCommonClass.BackButtonUrl = Request.UrlReferrer.AbsoluteUri;
                    ListBoxUnitType.DataBind();
                    BindPharmacyType();
                    GetApprovedUsersList();
                    int.TryParse(Request.QueryString["HospitalDemographicId"], out argHospitalDemographicId);
                    HospitalDemographicId = argHospitalDemographicId;
                    if (CreateHospitalUnit == false)
                    {
                        if (argHospitalDemographicId > 0)
                        {
                            PopulateData();
                        }
                    }
                    else
                    {
                        ButtonReset.Enabled = false;
                    }
                }

                QueryStringHandler.QuerystringParameterEncrpt objectQuerystringParameterEncrypt = new QueryStringHandler.QuerystringParameterEncrpt();

                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    //LabelOwner.Visible = true;
                   // DropDownListOwner.Visible = true;
                    //  RequiredFieldValidatorOwner.Enabled = true;
                   // spanOwner.Visible = true;
                    //if (Request.QueryString["Page"] != null)
                    //{
                    //    if (Convert.ToString(Request.QueryString["Page"]) == "DataManagementYear")
                    //    {
                    //        ImageButtonBack.PostBackUrl = "~/Administrator/DataManagementYear.aspx?" + objectQuerystringParameterEncrypt.EncrptQuerystringParam("HospitalInfoId=" + Convert.ToString(Request.QueryString["HospitalInfoId"]) + "&HospitalDemographicId=" + HospitalDemographicId + "&UnitCounter=" + Convert.ToString(Request.QueryString["UnitCounter"]) + "&PermissionID=" + PermissionID);
                    //    }
                    //}
                    LinkButtonRegisterNewUser.Visible = true;
                    ButtonDelete.Visible = true;
                    LinkButtonUnitTypeForUser.Visible = false;
                    LinkButtonAddPharmacyForUser.Visible = false;
                }
                else
                {
                  //  LabelOwner.Visible = false;
                   // DropDownListOwner.Visible = false;
                    //  RequiredFieldValidatorOwner.Enabled = false;
                   // spanOwner.Visible = false;
                    //if (Request.QueryString["Page"] != null)
                    //{
                    //    if (Convert.ToString(Request.QueryString["Page"]) == "DataManagementYear")
                    //    {
                    //        ImageButtonBack.PostBackUrl = "~/Users/DataManagementYear.aspx?" + objectQuerystringParameterEncrypt.EncrptQuerystringParam("HospitalInfoId=" + Convert.ToString(Request.QueryString["HospitalInfoId"]) + "&HospitalDemographicId=" + HospitalDemographicId + "&UnitCounter=" + Convert.ToString(Request.QueryString["UnitCounter"]) + "&PermissionID=" + PermissionID);
                    //    }
                    //}
                    LinkButtonRegisterNewUser.Visible = false;
                    LinkButtonUnitTypeForUser.Visible = true;
                    LinkButtonAddPharmacyForUser.Visible = true;
                    ButtonDelete.Visible = false;
                    SetControlAccessByPermission();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", " protected void Page_Load(object sender, EventArgs e)");
                LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (CommonClass.SessionInfomation == null)
                {
                    RMC.BusinessEntities.BESessionInfomation objectBESessionInfomation = new RMC.BusinessEntities.BESessionInfomation();
                    CommonClass.SessionInfomation = objectBESessionInfomation;
                }
                CommonClass.SessionInfomation.HospitalName = HospitalInformation.HospitalName;
                CommonClass.SessionInfomation.HospitalUnitName = TextBoxUnitName.Text;

                if (HttpContext.Current.User.IsInRole("superadmin") == false)
                {
                    SetPermissionForApprovedList();
                }
                else
                {
                    DisableLinkButtonInApprovedUser();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "Page_PreRender");
                LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Use to Display message of Login Failure.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {

            try
            {
                if (Page.IsValid)
                {
                    _objectBSHospitalDemographicDetail = new RMC.BussinessService.BSHospitalDemographicDetail();

                    _objectHospitalDemographicInfo = new RMC.DataService.HospitalDemographicInfo();
                    _objectBSHospitalDemographicDetail = new RMC.BussinessService.BSHospitalDemographicDetail();

                    if (CreateHospitalUnit == false)
                    {
                        _objectHospitalDemographicInfo.HospitalDemographicID = HospitalDemographicId;
                    }
                    else
                    {
                        _objectHospitalDemographicInfo.CreatedDate = DateTime.Now;
                        _objectHospitalDemographicInfo.CreatedBy = ActiveUser;
                    }
                    _objectHospitalDemographicInfo.ModifiedDate = DateTime.Now;
                    _objectHospitalDemographicInfo.ModifiedBy = ActiveUser;
                    _objectHospitalDemographicInfo.HospitalInfoID = HospitalInformation.HospitalInfoID;
                    _objectHospitalDemographicInfo.HospitalUnitName = TextBoxUnitName.Text;
                    _objectHospitalDemographicInfo.IsDeleted = false;
                    _objectHospitalDemographicInfo.TCABUnit = CheckBoxTCABUnit.Checked;
                    int getIntValue = 0;
                    double getDoubleValue = 0;
                    int.TryParse(TextBoxBedsInUnit.Text, out getIntValue);
                    _objectHospitalDemographicInfo.BedsInUnit = Convert.ToInt16(getIntValue);
                    string objectUnitType = string.Empty;
                    for (int index = 0; index < ListBoxUnitType.Items.Count; index++)
                    {
                        if (ListBoxUnitType.Items[index].Selected && index < ListBoxUnitType.Items.Count - 1)
                        {
                            objectUnitType += ListBoxUnitType.Items[index].Text + ",";
                        }
                    }
                    if (objectUnitType != "")
                    {
                        objectUnitType = objectUnitType.Substring(0, objectUnitType.Length - 1);
                    }
                    _objectHospitalDemographicInfo.UnitType = objectUnitType;
                    int.TryParse(TextBoxElectronicDocumentation.Text, out getIntValue);
                    _objectHospitalDemographicInfo.ElectronicDocumentation = Convert.ToInt16(getIntValue);
                    double.TryParse(TextBoxPatientsPerNurse.Text, out getDoubleValue);
                    _objectHospitalDemographicInfo.BudgetedPatientsPerNurse = getDoubleValue;

                    string objectPharmacyType = string.Empty;
                    for (int index = 0; index < ListBoxPharmacyType.Items.Count; index++)
                    {
                        if (ListBoxPharmacyType.Items[index].Selected && index < ListBoxPharmacyType.Items.Count - 1)
                        {
                            objectPharmacyType += ListBoxPharmacyType.Items[index].Text + ",";
                        }
                    }
                    if (objectPharmacyType != "")
                    {
                        objectPharmacyType = objectPharmacyType.Substring(0, objectPharmacyType.Length - 1);
                    }
                    _objectHospitalDemographicInfo.PharmacyType = objectPharmacyType;

                    DateTime objectDate;
                    
                    if (TextBoxStartDate.Text.Length > 0)
                    {
                        DateTime.TryParseExact(TextBoxStartDate.Text, "MM/dd/yyyy", new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out objectDate);
                    }
                    else
                    {
                        DateTime.TryParseExact(Convert.ToString("01/01/1901"), "MM/dd/yyyy", new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out objectDate);
                        
                    }
                    _objectHospitalDemographicInfo.StartDate = objectDate;
                    if (ViewState["EndDate"] == null)
                    {
                        DateTime.TryParseExact(TextBoxEndDate.Text, "MM/dd/yyyy", new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out objectDate);
                    }
                    else
                    {
                        DateTime.TryParseExact(Convert.ToString(ViewState["EndDate"]), "MM/dd/yyyy", new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out objectDate);
                    }
                    _objectHospitalDemographicInfo.EndedDate = objectDate;
                    _objectHospitalDemographicInfo.DocByException = CheckBoxDocByException.Checked;
                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        //LabelOwner.Visible = true;
                        //DropDownListOwner.Visible = true;
                        //   RequiredFieldValidatorOwner.Enabled = true;
                       // spanOwner.Visible = true;
                        //ImageButtonBack.PostBackUrl = "~/Administrator/DataManagement.aspx";
                        LinkButtonRegisterNewUser.Visible = true;
                    }
                    else
                    {
                       // LabelOwner.Visible = false;
                       // DropDownListOwner.Visible = false;
                        //  RequiredFieldValidatorOwner.Enabled = false;
                        //spanOwner.Visible = false;
                        //ImageButtonBack.PostBackUrl = "~/Users/DataManagement.aspx";
                        LinkButtonRegisterNewUser.Visible = false;
                    }

                    _objectBSHospitalDemographicDetail.UpdateHospitalDemographicInformation(_objectHospitalDemographicInfo, Convert.ToInt32(DropDownListOwner.SelectedValue));
                    
                    if (CreateHospitalUnit == true)
                    {
                        CommonClass.Show("Record Saved Successfully!");
                        //DisplayMessage("Record has been saved successfully!", System.Drawing.Color.Green);
                    }
                    else
                    {
                        CommonClass.Show("Record Updated Successfully!");
                        //DisplayMessage("Record has been updated successfully!", System.Drawing.Color.Green);
                    }
                    DataListApprovedUsers.DataBind();
                    ListBoxUnitType.DataBind();
                    BindPharmacyType();
                    GetApprovedUsersList();
                    PopulateData();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", " protected void ButtonSave_Click(object sender, EventArgs e)");
                LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
            finally
            {
                _objectHospitalDemographicInfo = null;
                _objectBSHospitalDemographicDetail = null;
            }

        }

        /// <summary>
        /// To Reset Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                PopulateData();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", " protected void ButtonReset_Click(object sender, EventArgs e)");
                LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Redirects to previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>Aug 06, 2009</CreatedOn>
        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                CommonClass objectCommonClass = new CommonClass();
                string backUrl = objectCommonClass.BackButtonUrl;
                string strPageUrl = backUrl.Substring(0, backUrl.IndexOf('?'));
                string pageName = strPageUrl.Substring(strPageUrl.LastIndexOf('/') + 1);
                int pageNameLength = pageName.Length - 5;
                if (strPageUrl.Substring(strPageUrl.LastIndexOf('/') + 1, pageNameLength) == "DataManagementFileList" || strPageUrl.Substring(strPageUrl.LastIndexOf('/') + 1, pageNameLength) == "DataManagementMonth")
                {
                    MaintainSessions.SessionIsBackNavigation = true;
                }
                Response.Redirect(backUrl, false);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ImageButtonBack_Click");
                LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }


        /// <summary>
        ///  Unit Type Link
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>Aug 13, 2009</CreatedOn>
        protected void LinkButtonUnitType_Click(object sender, EventArgs e)
        {
            try
            {
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    Response.Redirect("~/Administrator/AdminUnitType.aspx?HospitalDemographicId=" + HospitalDemographicId, false);
                }
                else
                {
                    Response.Redirect("~/Users/UnitType.aspx?HospitalDemographicId=" + HospitalDemographicId, false);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "LinkButtonUnitType_Click");
                LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Pharmacy Type Link
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>Aug 13, 2009</CreatedOn>
        protected void LinkButtonAddPharmacy_Click(object sender, EventArgs e)
        {
            try
            {
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    Response.Redirect("~/Administrator/PharmacyType.aspx?HospitalDemographicId=" + HospitalDemographicId, false);
                }
                else
                {
                    Response.Redirect("~/Users/PharmacyType.aspx?HospitalDemographicId=" + HospitalDemographicId, false);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "LinkButtonAddPharmacy_Click");
                LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Button Used for Redirect to Request Approval Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>Aug 13, 2009</CreatedOn>
        protected void LinkButtonApproveUser_Click(object sender, EventArgs e)
        {
            try
            {
                QueryStringHandler.QuerystringParameterEncrpt objectQuerystringParameterEncrypt = new QueryStringHandler.QuerystringParameterEncrpt();
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    if (Request.QueryString["Page"] != null)
                    {
                        if (Convert.ToString(Request.QueryString["Page"]) == "DataManagementYear")
                        {
                            Response.Redirect("~/Administrator/RequestApproval.aspx?" + objectQuerystringParameterEncrypt.EncrptQuerystringParam("Page=DataManagementYear&HospitalInfoId=" + Convert.ToString(Request.QueryString["HospitalInfoId"]) + "&HospitalDemographicId=" + HospitalDemographicId + "&UnitCounter=" + Convert.ToString(Request.QueryString["UnitCounter"]) + "&PermissionID=" + PermissionID + "&HospitalID=" + HospitalInformation.HospitalInfoID), false);
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Administrator/RequestApproval.aspx?" + objectQuerystringParameterEncrypt.EncrptQuerystringParam("HospitalDemographicId=" + HospitalDemographicId.ToString() + "&HospitalID=" + HospitalInformation.HospitalInfoID), false);
                    }
                }
                else
                {
                    if (Request.QueryString["Page"] != null)
                    {
                        if (Convert.ToString(Request.QueryString["Page"]) == "DataManagementYear")
                        {
                            Response.Redirect("~/Users/RequestApproval.aspx?" + objectQuerystringParameterEncrypt.EncrptQuerystringParam("Page=DataManagementYear&HospitalInfoId=" + Convert.ToString(Request.QueryString["HospitalInfoId"]) + "&HospitalDemographicId=" + HospitalDemographicId + "&UnitCounter=" + Convert.ToString(Request.QueryString["UnitCounter"]) + "&PermissionID=" + PermissionID + "&HospitalID=" + HospitalInformation.HospitalInfoID), false);
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Users/RequestApproval.aspx?" + objectQuerystringParameterEncrypt.EncrptQuerystringParam("HospitalDemographicId=" + HospitalDemographicId + "&HospitalID=" + HospitalInformation.HospitalInfoID), false);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "LinkButtonApproveUser_Click");
                LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Button Used for Register New User
        /// </summary>       
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>Aug 13, 2009</CreatedOn>
        protected void LinkButtonRegisterNewUser_Click(object sender, EventArgs e)
        {
            try
            {
                QueryStringHandler.QuerystringParameterEncrpt objectQuerystringParameterEncrypt = new QueryStringHandler.QuerystringParameterEncrpt();
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    if (Request.QueryString["Page"] != null)
                    {
                        if (Convert.ToString(Request.QueryString["Page"]) == "DataManagementYear")
                        {
                            Response.Redirect("~/Administrator/UserRegistration.aspx?" + objectQuerystringParameterEncrypt.EncrptQuerystringParam("Page=DataManagementYear&HospitalInfoId=" + Convert.ToString(Request.QueryString["HospitalInfoId"]) + "&HospitalDemographicId=" + HospitalDemographicId + "&UnitCounter=" + Convert.ToString(Request.QueryString["UnitCounter"]) + "&PermissionID=" + PermissionID), false);
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Administrator/UserRegistration.aspx?" + objectQuerystringParameterEncrypt.EncrptQuerystringParam("HospitalDemographicId=" + HospitalDemographicId), false);
                    }
                }
                else if (PermissionID == 1)
                {
                    Response.Redirect("~/Users/UserRegistration.aspx?" + objectQuerystringParameterEncrypt.EncrptQuerystringParam("HospitalInfoId=" + Convert.ToString(Request.QueryString["HospitalInfoId"]) + "&HospitalDemographicId=" + HospitalDemographicId + "&UnitCounter=" + Convert.ToString(Request.QueryString["UnitCounter"]) + "&PermissionID=" + PermissionID), false);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "LinkButtonRegisterNewUser_Click");
                LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Add New Hospital Link
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>Aug 13, 2009</CreatedOn>        
        protected void LinkButtonAddHospital_Click1(object sender, EventArgs e)
        {
            try
            {
                QueryStringHandler.QuerystringParameterEncrpt objectQuerystringParameterEncrypt = new QueryStringHandler.QuerystringParameterEncrpt();
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    if (Request.QueryString["Page"] != null)
                    {
                        if (Convert.ToString(Request.QueryString["Page"]) == "DataManagementYear")
                        {
                            Response.Redirect("~/Administrator/HospitalRegistration.aspx?" + objectQuerystringParameterEncrypt.EncrptQuerystringParam("Page=DataManagementYear&HospitalInfoId=" + Convert.ToString(Request.QueryString["HospitalInfoId"]) + "&HospitalDemographicId=" + HospitalDemographicId + "&UnitCounter=" + Convert.ToString(Request.QueryString["UnitCounter"]) + "&PermissionID=" + PermissionID), false);
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Administrator/HospitalRegistration.aspx?HospitalDemographicId=" + HospitalDemographicId, false);
                    }
                }
                else
                {
                    if (Request.QueryString["Page"] != null)
                    {
                        if (Convert.ToString(Request.QueryString["Page"]) == "DataManagementYear")
                        {
                            Response.Redirect("~/Users/HospitalRegistration.aspx?" + objectQuerystringParameterEncrypt.EncrptQuerystringParam("Page=DataManagementYear&HospitalInfoId=" + Convert.ToString(Request.QueryString["HospitalInfoId"]) + "&HospitalDemographicId=" + HospitalDemographicId + "&UnitCounter=" + Convert.ToString(Request.QueryString["UnitCounter"]) + "&PermissionID=" + PermissionID), false);
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Users/HospitalRegistration.aspx?HospitalDemographicId=" + HospitalDemographicId + "&PermissionID=" + PermissionID.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "LinkButtonAddHospital_Click1");
                LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Delete all Hospital Unit Information
        /// </summary>       
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>Aug 18, 2009</CreatedOn>  
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _objectBSHospitalDemographicDetail = new RMC.BussinessService.BSHospitalDemographicDetail();
                if (_objectBSHospitalDemographicDetail.LogicalDeleteHospitalUnitInfo(HospitalDemographicId, ActiveUser) == true)
                {
                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        Response.Redirect("~/Administrator/DataManagement.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("~/Users/DataManagement.aspx", false);
                    }
                    // DisplayMessage("Record has been deleted successfully!", System.Drawing.Color.Green);
                }
                else
                {
                    CommonClass.Show("Record Already Deleted!");
                    //DisplayMessage("Record has already been deleted!", System.Drawing.Color.Green);
                }
            }
            catch (Exception ex)
            {

                ex.Data.Add("Events", "ButtonDelete_Click");
                LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void CheckBoxApproval_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = false;
            try
            {
                RMC.BussinessService.BSViewRequest objectBSViewRequest = new RMC.BussinessService.BSViewRequest();
                CheckBox chkBox = (CheckBox)(sender);
                DataListItem dataLstItem = (DataListItem)(chkBox.NamingContainer);
                int userID = Convert.ToInt32(DataListApprovedUsers.DataKeys[dataLstItem.ItemIndex].ToString());

                //if (userID != UserInfo.UserID)
                if (HttpContext.Current.User.IsInRole("superadmin") || objectBSViewRequest.IsUserOwnerOfHospitalUnit(UserInfo.UserID, HospitalDemographicId))
                {
                    flag = objectBSViewRequest.UpdateViewRequestForDisapproval(userID, UserInfo.UserID, HospitalDemographicId);
                }
                else
                {
                    //if (HttpContext.Current.User.IsInRole("superadmin"))
                    //{
                    //    CommonClass.Show("User is a Super Administrator.");
                    //}
                    //else
                    //{
                    //    CommonClass.Show("User is an Owner of Hospital Unit.");
                    //    //DisplayMessage("User is an owner of Hospital Unit.", System.Drawing.Color.Red);
                    //}

                    CommonClass.Show("You are not authorized for this operation");
                   
                }
                if (flag)
                {
                    CommonClass.Show("Record Updated Successfully.");
                    //DisplayMessage("Record Update Successfully.", System.Drawing.Color.Green);
                }
                else
                {
                    CommonClass.Show("Failed to Update Record.");
                    //DisplayMessage("Fail to Update Record.", System.Drawing.Color.Red);
                }
                DataListApprovedUsers.DataBind();
                ListBoxUnitType.DataBind();
                BindPharmacyType();
                GetApprovedUsersList();
                PopulateData();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "CheckBoxApproval_CheckedChanged");
                LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonApprovedUser_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkButton = (LinkButton)(sender);
                DataListItem dataLstItem = (DataListItem)(lnkButton.NamingContainer);
                Int32 userID = Convert.ToInt32(DataListApprovedUsers.DataKeys[dataLstItem.ItemIndex].ToString());
                if (!HttpContext.Current.User.IsInRole("superadmin"))
                {
                    if (userID != CommonClass.UserInformation.UserID)
                    {
                        Response.Redirect("Notification.aspx?UserId=" + userID.ToString(), false);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "LinkButtonApprovedUser_Click");
                LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonUserUnitType_Click(object sender, EventArgs e)
        {
            try
            {

                // RequestForTypes1.Type = "Unit Type";                
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "LinkButtonUserUnitType_Click");
                LogManager._stringObject = "HospitalDemographicinfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

    }
}