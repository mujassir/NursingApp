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
using RMC.BussinessService;
using LogExceptions;
using System.IO;

namespace RMC.Web.UserControls
{
    public partial class HospitalEditRegistration : System.Web.UI.UserControl
    {

        #region Variables

        //Bussiness Service Objects.
        BSCommon _objectBSCommon = null;
        BSHospitalInfo _objectBSHospitalInfo = null;
        BSMultiUserHospital _objectBSMultiUserInfo = null;
        //Data Service Objects
        RMC.DataService.HospitalInfo _objectHospitalInfo = null;
        RMC.DataService.MultiUserHospital _objectMultiUserHospital = null;
        RMC.BusinessEntities.BEHospitalInfo _objectBEHospitalInfo = null;
        List<RMC.BusinessEntities.BEHospitalInfo> _objectBSHospitalDynamicInfo;
        //Bussiness Entity Objects.
        RMC.BusinessEntities.BEHospitalInfoDynamicProp _objectBEHospitalInfoDynamicProp;
        //Fundamental Data Types.
        bool _flag, _IsExist = false;
        private int _hospitalInfoId, _permissionID;
        #endregion

        #region Properties

        /// <summary>
        /// Return passed HospitalInfoId
        /// <Author>Raman</Author>
        /// <createdOn>July 22, 2009</createdOn>
        /// </summary>
        private int HospitalInfoId
        {
            get
            {

                return _hospitalInfoId = Request.QueryString["HospitalInfoId"] != null ? Convert.ToInt32(Request.QueryString["HospitalInfoId"]) : 0;

            }
            set
            {

                _hospitalInfoId = value;
            }
        }

        /// <summary>
        /// Return passed HospitalInfoId
        /// <Author>Raman</Author>
        /// <createdOn>17 August, 2009</createdOn>
        /// </summary>
        private int PermissionID
        {
            get
            {
                return _permissionID = Request.QueryString["PermissionID"] != null ? Convert.ToInt32(Request.QueryString["PermissionID"]) : 0;
            }
            set
            {
                _permissionID = value;
            }
        }

        /// <summary>
        /// Return Email of Logged in user
        /// <Author>Raman</Author>
        /// <createdOn>July 22, 2009</createdOn>
        /// </summary>
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

        #endregion

        #region Private Methods

        /// <summary>
        /// Use to Display message of Login Failure.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 21, 2009.
        /// Modified By : Davinder Kumar
        /// Modified Date : July 22, 2009
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="color"></param>
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
                ex.Data.Add("Class", "HospitalRegistration");
                 throw ex;
            }
        }

        /// <summary>
        /// Reset all control status.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 21, 2009.
        /// Modified By : Davinder Kumar
        /// Modified Date : July 22, 2009
        /// </summary>
        private void ResetControls()
        {
            try
            {
                TextBoxAddress.Text = string.Empty;
                TextBoxCity.Text = string.Empty;
                TextBoxHospitalName.Text = string.Empty;
                TextBoxZip.Text = string.Empty;
                TextBoxCNOFirstName.Text = string.Empty;
                TextBoxCNOLastName.Text = string.Empty;
                TextBoxCNOPhone.Text = string.Empty;
                TextBoxCNOEmail.Text = string.Empty;
                TextBoxBedsInHospital.Text = string.Empty;
                // TextBoxCaptchaText.Text = string.Empty;
                DropDownListOwnershipType.SelectedIndex = 0;
                DropDownListOwner.SelectedIndex = 0;
                DropDownListCountry.SelectedIndex = 0;
                if (DropDownListState.Items.Count > 1)
                {
                    DropDownListState.Items.Clear();
                    DropDownListState.Items.Add("Select State");
                    DropDownListState.Items[0].Value = 0.ToString();
                    DropDownListState.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "Reset");
                ex.Data.Add("Class", "HospitalRegistration");
                throw ex;
            }
        }

        /// <summary>
        /// Save Hospital Infomation In a Data service object.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 21, 2009.
        /// Modified By : Davinder Kumar
        /// Modified Date : July 22, 2009
        /// </summary>
        /// <returns></returns>
        private RMC.DataService.HospitalInfo SaveHospitalInfo()
        {
            _objectHospitalInfo = new RMC.DataService.HospitalInfo();
            try
            {
                int index = 0;
                int userID = CommonClass.UserInformation.UserID;
                string userName = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
                int.TryParse(TextBoxHospitalIndex.Text, out index);

                _objectHospitalInfo.Address = TextBoxAddress.Text;
                _objectHospitalInfo.City = TextBoxCity.Text;
                _objectHospitalInfo.CreatedBy = userName;
                _objectHospitalInfo.CreatedDate = DateTime.Now;
                _objectHospitalInfo.HospitalName = TextBoxHospitalName.Text;
                _objectHospitalInfo.StateID = Convert.ToInt32(DropDownListState.SelectedValue);
                _objectHospitalInfo.Zip = TextBoxZip.Text;
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    _objectHospitalInfo.RecordCounter = index;
                }
                _objectHospitalInfo.ChiefNursingOfficerFirstName = TextBoxCNOFirstName.Text;
                _objectHospitalInfo.ChiefNursingOfficerLastName = TextBoxCNOLastName.Text;
                _objectHospitalInfo.ChiefNursingOfficerPhone = TextBoxCNOPhone.Text;
                _objectHospitalInfo.ChiefNursingOfficerEmail = TextBoxCNOEmail.Text;
                _objectHospitalInfo.UserID = userID;
                _objectHospitalInfo.IsActive = true;
                _objectHospitalInfo.IsDeleted = false;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveHospitalInfo");
                ex.Data.Add("Class", "HospitalInfomation");
                throw ex;
            }

            return _objectHospitalInfo;
        }

        /// <summary>
        /// Save Permission to access Hospital.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 22, 2009. 
        /// </summary>
        /// <returns></returns>
        private RMC.DataService.MultiUserHospital SaveInMultiUserHospital()
        {
            _objectMultiUserHospital = new RMC.DataService.MultiUserHospital();
            _objectBSCommon = new BSCommon();
            try
            {
                int userID = CommonClass.UserInformation.UserID;
                string userName = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
                _objectMultiUserHospital.CreatedBy = userName;
                _objectMultiUserHospital.CreatedDate = DateTime.Now;
                _objectMultiUserHospital.IsDeleted = false;
                _objectMultiUserHospital.PermissionID = _objectBSCommon.GetPermissionIDByPermissionName("owner");
                _objectMultiUserHospital.UserID = userID;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "SaveInMultiUserHospital");
                ex.Data.Add("Class", "HospitalInfomation");
                throw ex;
            }

            return _objectMultiUserHospital;
        }

        /// <summary>
        /// To populate data on the page
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>
         
        private void PopulateData()
        {
            try
            {
                _objectBSHospitalInfo = new RMC.BussinessService.BSHospitalInfo();
                _objectBEHospitalInfo = _objectBSHospitalInfo.GetHospitalInformation(HospitalInfoId);
                _objectBSMultiUserInfo=new RMC.BussinessService.BSMultiUserHospital();
                 bool checkOwnerExistInMultiUser=false;
                if (_objectBEHospitalInfo != null)
                {
                    TextBoxAddress.Text = _objectBEHospitalInfo.Address;
                    TextBoxHospitalName.Text = _objectBEHospitalInfo.HospitalName;

                    if (DropDownListOwner.Items.Count > 0)
                    {
                        for (int count = 0; count < DropDownListOwner.Items.Count; count++)
                        {
                            if (Convert.ToString(_objectBEHospitalInfo.UserID) == DropDownListOwner.Items[count].Value)
                            {
                                _IsExist = true;
                                break;
                            }
                        }
                        //--Added by Mahesh Sachdeva // To check whether user exists as owner or not in MultiUserHospital
                        int permissionType=1;
                        checkOwnerExistInMultiUser = _objectBSMultiUserInfo.CheckPermissionOnHospitalByUserID(_objectBEHospitalInfo.UserID, HospitalInfoId, permissionType);
                       //-----------------------------------------
                    }
                    if (_IsExist && checkOwnerExistInMultiUser)
                    {
                        //DropDownListOwner.Items.FindByValue(Convert.ToString(_objectBEHospitalInfo.UserID)).Selected = true;
                        DropDownListOwner.SelectedValue = Convert.ToString(_objectBEHospitalInfo.UserID);
                    }
                    else
                    {
                        DropDownListOwner.SelectedValue ="0";
                    }
                    TextBoxCNOFirstName.Text = _objectBEHospitalInfo.ChiefNursingOfficerFirstName;
                    TextBoxCNOLastName.Text = _objectBEHospitalInfo.ChiefNursingOfficerLastName;
                    TextBoxCNOPhone.Text = _objectBEHospitalInfo.ChiefNursingOfficerPhone;
                    TextBoxCNOEmail.Text = _objectBEHospitalInfo.ChiefNursingOfficerEmail;
                    TextBoxCity.Text = _objectBEHospitalInfo.City;
                    TextBoxZip.Text = _objectBEHospitalInfo.Zip;
                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        trHospitalIndex.Visible = true;
                        TextBoxHospitalIndex.Text = Convert.ToString(_objectBEHospitalInfo.HospitalRecordCount);
                    }
                    else
                    {
                        trHospitalIndex.Visible = false;
                        RequiredFieldValidatorHospitalIndex.Visible = false;
                        RegularExpressionValidatorHospitalIndex.Visible = false;
                    }
                    DropDownListCountry.DataBind();
                    DropDownListCountry.SelectedValue = Convert.ToString(_objectBEHospitalInfo.CountryID);
                    DropDownListState.DataBind();
                    DropDownListState.SelectedValue = Convert.ToString(_objectBEHospitalInfo.StateID);
                }
                else
                {
                    ResetControls();
                }

            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "PopulateData");
                ex.Data.Add("Class", "HospitalInfo.ascx.cs");
                LogManager._stringObject = "HospitalInfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                throw ex;
            }
            finally { _objectBEHospitalInfo = null; }
        }
        /// <summary>
        /// To populate dynamic fields data on the page
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>12 August, 2009</CreatedOn>
        /// </summary>
        private void PopulateDynamicFieldData()
        {
            try
            {

                _objectBSHospitalDynamicInfo = new List<RMC.BusinessEntities.BEHospitalInfo>();
                _objectBSHospitalInfo = new RMC.BussinessService.BSHospitalInfo();
                _objectBSHospitalDynamicInfo = (List<RMC.BusinessEntities.BEHospitalInfo>)_objectBSHospitalInfo.GetHospitalDynamicFieldInformation(HospitalInfoId);

                if (_objectBSHospitalDynamicInfo != null)
                {

                    foreach (RMC.BusinessEntities.BEHospitalInfo objectBE in _objectBSHospitalDynamicInfo)
                    {
                        if (objectBE.ColumnID == 1)
                        {
                            TextBoxBedsInHospital.Text = objectBE.Value;
                        }
                        else if (objectBE.ColumnID == 2)
                        {
                            // DropDownListOwnershipType.SelectedValue = Convert.ToString(objectBE.Value);
                            DropDownListOwnershipType.Items.FindByText(Convert.ToString(objectBE.Value)).Selected = true;
                        }
                        else if (objectBE.ColumnID == 3)
                        {
                            if (objectBE.Value != "")
                            {
                                string[] hospitalType = objectBE.Value.Split(',');
                                for (int kcount = 0; kcount < hospitalType.Length; kcount++)
                                {
                                    for (int count = 0; count < ListBoxHospitalType.Items.Count; count++)
                                    {

                                        if (hospitalType[kcount].Equals(ListBoxHospitalType.Items[count].Text))
                                        {
                                            ListBoxHospitalType.Items[count].Selected = true;
                                            break;
                                        }

                                    }
                                }
                            }

                        }
                    }
                    //for (int count = 0; count < _objectBSHospitalDynamicInfo.Count; count++)
                    //{

                    //}

                }

            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "PopulateData");
                ex.Data.Add("Class", "HospitalInfo.ascx.cs");
                LogManager._stringObject = "HospitalInfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                throw ex;
            }
            finally { _objectBEHospitalInfo = null; }
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

                //int hospitalInfoId = 0;
                //RMC.BussinessService.BSTreeView objectTreeView = new RMC.BussinessService.BSTreeView();
                //if (Request.QueryString != null)
                //{
                //    hospitalInfoId = Convert.ToInt32(Request.QueryString["HospitalInfoId"]);
                //}
                //List<RMC.BusinessEntities.BEHospitalMembers> objectApprovedUserList = objectTreeView.GetAllMembersOfHospital(hospitalInfoId);
                //if (objectApprovedUserList != null)
                //{
                //    if (objectApprovedUserList.Count > 0)
                //    {
                //        int count = 0;
                //        foreach (RMC.BusinessEntities.BEHospitalMembers objectApproved in objectApprovedUserList)
                //        {
                //            string appprovedUser = objectApproved.UserName;
                //            // DropDownListOwner.Items.Insert(count, new ListItem(objectApproved.UserName, Convert.ToString(objectApproved.UserID)));
                //            if (objectApproved.Owner)
                //            {
                //                appprovedUser = objectApproved.UserName + " " + "(owner)";
                //            }
                //            ListBoxApprovedUsers.Items.Insert(count, new ListItem(appprovedUser, Convert.ToString(objectApproved.UserID)));


                //            count = count + 1;
                //        }
                //    }

                //}
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetApprovedUsersList");
                ex.Data.Add("Class", "HospitalInfo.ascx.cs");
                throw ex;
            }

        }

        private void BackNavigation(string backUrl)
        {
            string strPageUrl = backUrl.Substring(0, backUrl.IndexOf('?'));
            string pageName = strPageUrl.Substring(strPageUrl.LastIndexOf('/') + 1);
            int pageNameLength = pageName.Length - 5;
            if (strPageUrl.Substring(strPageUrl.LastIndexOf('/') + 1, pageNameLength) == "DataManagementFileList" || strPageUrl.Substring(strPageUrl.LastIndexOf('/') + 1, pageNameLength) == "DataManagementMonth")
            {
                MaintainSessions.SessionIsBackNavigation = true;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Button Event to reset Control status.
        /// Created By : Davinder Kumar
        /// Creation Date : June 24, 2009
        /// Modified By : Davinder Kumar
        /// Modified Date : July 8, 2009
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetControls();
                LabelErrorMsg.Text = string.Empty;
                PanelErrorMsg.Visible = false;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonReset_Click");
                ex.Data.Add("Page", "HospitalEditRegistration.ascx");
                LogManager._stringObject = "HospitalEditRegistration.ascx ---- ButtonReset_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Save Button Events to Save Hospital Infomation in a Database.
        /// Created By : Davinder`Q+7 4E14` Kumar.
        /// Creation Date : June 24, 2009
        /// Modified By : Davinder Kumar
        /// Modified Date : July 22, 2009
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {


                    #region"mp.Code modified to validate the Index Number"

                    RMC.DataService.RMCDataContext _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                    List<RMC.DataService.HospitalInfo> objlist = new List<RMC.DataService.HospitalInfo>();
                    if (TextBoxHospitalIndex.Text != "")
                    {
                        objlist = (from r in _objectRMCDataContext.HospitalInfos
                                   where r.RecordCounter == Convert.ToInt32(TextBoxHospitalIndex.Text) && r.HospitalInfoID ==Convert.ToInt32( Request.QueryString["HospitalInfoId"])
                                   select r).ToList();
                    }
                    else 
                    {
                        objlist = (from r in _objectRMCDataContext.HospitalInfos
                                    //where r.RecordCounter == Convert.ToInt32(TextBoxHospitalIndex.Text)
                                   select r).ToList();                    
                    }

                        bool flag = true;
                        //foreach (RMC.DataService.HospitalInfo obj in objlist)
                        //{
                        //    if (TextBoxHospitalIndex.Text != "")
                        //    {
                        //        if (obj.RecordCounter == int.Parse(TextBoxHospitalIndex.Text))
                        //        {
                        //            flag = false;
                        //        }
                        //    }
                        //}
                        foreach (RMC.DataService.HospitalInfo obj in objlist)
                        {
                            if (TextBoxHospitalIndex.Text != "")
                            {
                                if (obj.RecordCounter == int.Parse(TextBoxHospitalIndex.Text) && obj.HospitalInfoID == int.Parse(Request.QueryString["HospitalInfoId"]))
                                {
                                    flag = true;
                                }
                            }
                        }
                        if (flag == false)
                        {
                            CommonClass.Show("Hospital Index Already exist");
                        }
                    
                    #endregion
                    else
                    {
                        _objectBEHospitalInfo = new RMC.BusinessEntities.BEHospitalInfo();
                        _objectBSHospitalInfo = new RMC.BussinessService.BSHospitalInfo();
                        _objectBEHospitalInfoDynamicProp = new RMC.BusinessEntities.BEHospitalInfoDynamicProp();
                        if (HospitalInfoId <= 0)
                        {
                            if (_objectBSHospitalInfo.ExistHospital(TextBoxHospitalName.Text, TextBoxCity.Text) == true)
                            {
                                CommonClass.Show("This Hospital Added Already!");
                                //DisplayMessage("This hospital has already been added!", System.Drawing.Color.Red);
                                return;
                            }
                        }
                        _objectBEHospitalInfo.Address = TextBoxAddress.Text;
                        _objectBEHospitalInfo.ChiefNursingOfficerFirstName = TextBoxCNOFirstName.Text;
                        _objectBEHospitalInfo.ChiefNursingOfficerLastName = TextBoxCNOLastName.Text;
                        _objectBEHospitalInfo.ChiefNursingOfficerPhone = TextBoxCNOPhone.Text;
                        _objectBEHospitalInfo.ChiefNursingOfficerEmail = TextBoxCNOEmail.Text;
                        _objectBEHospitalInfo.City = TextBoxCity.Text;
                        _objectBEHospitalInfo.CountryID = Convert.ToInt32(DropDownListCountry.SelectedValue);
                        _objectBEHospitalInfo.HospitalInfoID = HospitalInfoId;
                        _objectBEHospitalInfo.HospitalName = TextBoxHospitalName.Text;
                        _objectBEHospitalInfo.StateID = Convert.ToInt32(DropDownListState.SelectedValue);
                        if (HttpContext.Current.User.IsInRole("superadmin"))
                        {
                            int index = 0;
                            int.TryParse(TextBoxHospitalIndex.Text, out index);
                            _objectBEHospitalInfo.UserID = Convert.ToInt32(DropDownListOwner.SelectedValue);
                            //LabelOwner.Visible = true;
                            //HyperLinkOwnerName.Visible = true;

                            //RequiredFieldValidatorOwner.Enabled = true;
                            spanOwner.Visible = true;
                            _objectBEHospitalInfo.HospitalRecordCount = index;
                            //ImageButtonBack.PostBackUrl = "~/Administrator/DataManagement.aspx";
                        }
                        else
                        {
                            int userID = CommonClass.UserInformation.UserID;
                            _objectBEHospitalInfo.UserID = userID;
                            //LabelOwner.Visible = false;
                            //HyperLinkOwnerName.Visible = false;

                            //RequiredFieldValidatorOwner.Enabled = false;
                            spanOwner.Visible = false;
                            //ImageButtonBack.PostBackUrl = "~/Users/DataManagement.aspx";
                        }

                        _objectBEHospitalInfo.Zip = TextBoxZip.Text;
                        _objectBEHospitalInfo.IsDeleted = false;

                        //Filling Dynamic Value data 
                        _objectBEHospitalInfoDynamicProp.BedsInHospital = TextBoxBedsInHospital.Text;
                        _objectBEHospitalInfoDynamicProp.OwnershipType = DropDownListOwnershipType.SelectedItem.Text;
                        string objectHospitalType = string.Empty;
                        for (int index = 0; index < ListBoxHospitalType.Items.Count; index++)
                        {
                            if (ListBoxHospitalType.Items[index].Selected && index < ListBoxHospitalType.Items.Count - 1)
                            {
                                objectHospitalType += ListBoxHospitalType.Items[index].Text + ",";
                            }
                        }
                        if (objectHospitalType != "")
                        {
                            objectHospitalType = objectHospitalType.Substring(0, objectHospitalType.Length - 1);
                        }
                        _objectBEHospitalInfoDynamicProp.HospitalType = objectHospitalType;
                        _objectBSHospitalInfo.UpdateHospitalInfo(_objectBEHospitalInfo, ActiveUser);
                        _objectBSHospitalInfo.UpdateDynamicFieldData(_objectBEHospitalInfoDynamicProp, HospitalInfoId);


                        //------------Code used to validate the Hospital Index number------------------------//
                        // RMC.BusinessEntities.BEHospitalInfo objBEinfo=new RMC.BusinessEntities.BEHospitalInfo();

                        //------------------------------------//
                        //DisplayMessage("Record has been updated successfully!", System.Drawing.Color.Green);
                        CommonClass objectCommonClass = new CommonClass();
                        string backUrl = objectCommonClass.BackButtonUrl;
                        BackNavigation(backUrl);
                        Response.Redirect(backUrl, false);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSave_Click");
                ex.Data.Add("Page", "HospitalInfo.ascx.cs");
                LogManager._stringObject = "HospitalInfo.ascx.cs ---- DropDownListCountry_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
            finally
            {
                _objectBEHospitalInfo = null;
                _objectBSHospitalInfo = null;
            }
        }

        /// <summary>
        /// Use to fill state dropdownlist.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 8, 2009.
        /// Modified By : Davinder Kumar
        /// Modified Date : July 22, 2009
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListState.Items.Count > 1)
                {
                    DropDownListState.Items.Clear();
                    DropDownListState.Items.Add("Select State");
                    DropDownListState.Items[0].Value = 0.ToString();
                }

                // ScriptManagerHospitalRegistration.FindControl("DropDownListCountry").Focus();
                // UpdatePanelState.Update();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "DropDownListCountry_SelectedIndexChanged");
                ex.Data.Add("Page", "HospitalEditRegistration.ascx");
                LogManager._stringObject = "HospitalEditRegistration.ascx ---- DropDownListCountry_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void CheckBoxApproval_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = false;
            try
            {
                RMC.BussinessService.BSHospitalInfo objectBSHospitalInfo = new BSHospitalInfo();
                RMC.BussinessService.BSMultiUserHospital objectBSMultiUserHospital = new RMC.BussinessService.BSMultiUserHospital();
                CheckBox chkBox = (CheckBox)(sender);
                DataListItem dataLstItem = (DataListItem)(chkBox.NamingContainer);
                int multiUserHospitalID = Convert.ToInt32(DataListApprovedUsers.DataKeys[dataLstItem.ItemIndex].ToString());
                int userIdOfApprovedUser = objectBSMultiUserHospital.GetUserIdForMultiUserHospitalId(multiUserHospitalID);
                int loggedInUserID = UserInfo.UserID;
                bool isTryingToRemoveHimself=(loggedInUserID==userIdOfApprovedUser);

                 //added by Bharat======
                //RMC.BussinessService.BSTreeView objectBSTreeView = new BSTreeView();
                //List<RMC.BusinessEntities.BEHospitalMembers> objectBEHospitalMembers = null;
                //objectBEHospitalMembers = objectBSTreeView.GetAllMembersByHospitalID(HospitalInfoId);
                //objectBEHospitalMembers = objectBEHospitalMembers.Where(x => x.UserID == userID).ToList();
                //List<RMC.BusinessEntities.BEUserInfomation> approvedUsers = objectMultiUserHospital.GetUserInfomationByHospitalInfoID(HospitalInfoId);

                                
                //bool isLoggedInUserSuperOwnerOfHospital = ((objectBSHospitalInfo.GetHospitalInfoByHospitalInfoID(HospitalInfoId)).UserID == loggedInUserID);

                //superadmin has right to remove any user
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    flag = objectBSMultiUserHospital.DeleteHospitalByMultiUserID(multiUserHospitalID);
                }
                //logged in user can not remove himself from the approved user list or any user who is owner
                else if (!isTryingToRemoveHimself)
                {
                    //check is user to be removed has owner level permissions
                    if (!objectBSMultiUserHospital.IsUserOwnerOfTheHospital(multiUserHospitalID))
                        flag = objectBSMultiUserHospital.DeleteHospitalByMultiUserID(multiUserHospitalID);
                    else
                        CommonClass.Show("This user has owner level permission so it can be removed by superadmin only.");
                }
                else
                {
                    CommonClass.Show("You can not remove yourself from the approved users list");
                }

                if (flag)
                {
                    CommonClass.Show("Record Updated Successfully.");
                }
                else
                {
                    CommonClass.Show("Failed to Update Record.");
                }
                DataListApprovedUsers.DataBind();                
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


        /// <summary>
        /// Page Event.
        /// Created By : Davinder Kumar
        /// Creation Date : June 24, 2009
        /// Modified By : Davinder Kumar
        /// Modified Date : July 22, 2009
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int argHospitalInfoId = 0;
                QueryStringHandler.QuerystringParameterEncrpt objectQuerystringEncrypt = new QueryStringHandler.QuerystringParameterEncrpt();
                //Set Default Country.
                if (!Page.IsPostBack)
                {
                    CommonClass objectCommonClass = new CommonClass();
                    objectCommonClass.BackButtonUrl = Request.UrlReferrer.AbsoluteUri;
                    DropDownListCountry.DataBind();
                    DropDownListCountry.SelectedIndex = DropDownListCountry.Items.IndexOf(DropDownListCountry.Items.FindByText("US"));
                }

                if (Page.IsPostBack == false)
                {
                    int.TryParse(Request.QueryString["HospitalInfoId"], out argHospitalInfoId);
                    HospitalInfoId = argHospitalInfoId;
                    if (HospitalInfoId > 0)
                    {
                        DropDownListOwner.DataBind();
                        DropDownListOwnershipType.DataBind();
                        ListBoxHospitalType.DataBind();
                        PopulateData();
                        PopulateDynamicFieldData();
                        GetApprovedUsersList();
                    }
                }
                //Check the current user type.
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    //HyperLinkOwnerName.Visible = true;
                   // DropDownListOwner.Visible = true;
                  //  RequiredFieldValidatorOwner.Enabled = true;
                    spanOwner.Visible = true;
                    LinkButtonHospitalType.PostBackUrl = "~/Administrator/HospitalType.aspx?" + objectQuerystringEncrypt.EncrptQuerystringParam("HospitalName=" + TextBoxHospitalName.Text + " " + DropDownListState.SelectedItem.Text + " " + TextBoxCity.Text);
                    LinkButtonOwnershipType.PostBackUrl = "~/Administrator/OwnershipType.aspx?" + objectQuerystringEncrypt.EncrptQuerystringParam("HospitalName=" + TextBoxHospitalName.Text + " " + DropDownListState.SelectedItem.Text + " " + TextBoxCity.Text);
                    LinkButtonHospitalTypeForUser.Visible = false;
                    LinkButtonOwnershipTypeForUsers.Visible = false;
                    ButtonDelete.Visible = true;
                    //if (Request.QueryString["Page"] != null)
                    //{
                    //    if (Convert.ToString(Request.QueryString["Page"]) == "DataManagementYear")
                    //    {
                    //        //ImageButtonBack.PostBackUrl = "~/Administrator/DataManagementYear.aspx?" + objectQuerystringEncrypt.EncrptQuerystringParam("HospitalInfoID=" + HospitalInfoId + "&PermissionID=" + Convert.ToString(Request.QueryString["PermissionUnitID"]) + "&HospitalDemographicId=" + Convert.ToString(Request.QueryString["HospitalDemographicId"]) + "&UnitCounter=" + Convert.ToString(Request.QueryString["UnitCounter"]));
                    //    }
                    //}
                    //else
                    //{
                    //    //ImageButtonBack.PostBackUrl = "~/Administrator/DataManagementUnit.aspx?" + objectQuerystringEncrypt.EncrptQuerystringParam("HospitalInfoID=" + HospitalInfoId);
                    //}
                }
                else
                {
                    bool flag = false;
                    int permissionID = 0;
                    LinkButtonHospitalTypeForUser.Visible = true;
                    LinkButtonOwnershipTypeForUsers.Visible = true;
                    LinkButtonHospitalType.Visible = false;
                    LinkButtonOwnershipType.Visible = false;
                    LinkButtonHospitalType.PostBackUrl = "~/Users/HospitalTypes.aspx";
                    LinkButtonOwnershipType.PostBackUrl = "~/Users/OwnershipType.aspx";
                    flag = int.TryParse(Request.QueryString["PermissionID"].ToString(), out permissionID);
                    if (flag)
                    {
                        if (permissionID != 1)
                        {
                            DropDownListCountry.Enabled = false;
                            DropDownListOwnershipType.Enabled = false;
                            DropDownListState.Enabled = false;

                            TextBoxAddress.Enabled = false;
                            TextBoxBedsInHospital.Enabled = false;
                            TextBoxCity.Enabled = false;
                            TextBoxCNOEmail.Enabled = false;
                            TextBoxCNOFirstName.Enabled = false;
                            TextBoxCNOLastName.Enabled = false;
                            TextBoxCNOPhone.Enabled = false;
                            TextBoxHospitalName.Enabled = false;
                            TextBoxZip.Enabled = false;

                            DataListApprovedUsers.Enabled = false;
                            //ListBoxApprovedUsers.Enabled = false;
                            ListBoxHospitalType.Enabled = false;
                            ButtonSave.Visible = false;
                            divSaveDisable.Visible = true;
                            ButtonReset.Enabled = false;
                            LinkButtonHospitalType.Visible = false;
                            LinkButtonOwnershipType.Visible = false;
                        }
                    }

                    //HyperLinkOwnerName.Visible = false;
                    //DropDownListOwner.Visible = false;
                    //RequiredFieldValidatorOwner.Enabled = false;
                    spanOwner.Visible = false;
                    ButtonDelete.Visible = false;
                    //if (Request.QueryString["Page"] != null)
                    //{
                    //    if (Convert.ToString(Request.QueryString["Page"]) == "DataManagementYear")
                    //    {
                    //        ImageButtonBack.PostBackUrl = "~/Users/DataManagementYear.aspx?" + objectQuerystringEncrypt.EncrptQuerystringParam("HospitalInfoID=" + HospitalInfoId + "&PermissionID=" + Convert.ToString(Request.QueryString["PermissionUnitID"]) + "&HospitalDemographicId=" + Convert.ToString(Request.QueryString["HospitalDemographicId"]) + "&UnitCounter=" + Convert.ToString(Request.QueryString["UnitCounter"]));
                    //    }
                    //}
                    //else
                    //{
                    //    ImageButtonBack.PostBackUrl = "~/Users/DataManagementUnit.aspx?" + objectQuerystringEncrypt.EncrptQuerystringParam("HospitalInfoID=" + HospitalInfoId);
                    //}
                }
                //Add Javascript Event to restrict the special character in Textboxes.
                TextBoxHospitalName.Attributes.Add("onKeyDown", "return FilterAlphaNumeric(event);");
                TextBoxCNOFirstName.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
                TextBoxCNOLastName.Attributes.Add("onKeyDown", "return validateNonNumber(event);");
                TextBoxZip.Attributes.Add("onKeyDown", "return FilterAlphaNumericDash(event);");
                TextBoxCity.Attributes.Add("onKeyDown", "return validateNonNumber(event);");

                if (ViewState["PerviousURL"] == null)
                {
                    ViewState["PerviousURL"] = Request.UrlReferrer.ToString();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "HospitalEditRegistration.ascx");
                LogManager._stringObject = "HospitalEditRegistration.ascx ---- Page_Load";
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
                CommonClass.SessionInfomation.HospitalName = TextBoxHospitalName.Text + " " + DropDownListState.SelectedItem.Text + " " + TextBoxCity.Text;
                CommonClass.SessionInfomation.HospitalUnitName = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "Page_PreRender");
                LogManager._stringObject = "HospitalEditRegistration.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Used for Redirct to User Profile Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ListBoxApprovedUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ListBoxApprovedUsers.SelectedIndex > -1)
            //{
            //    if (HttpContext.Current.User.IsInRole("superadmin"))
            //    {
            //        Response.Redirect("~/Administrator/UserProfile.aspx?UserId=" + Convert.ToInt32(ListBoxApprovedUsers.SelectedItem.Value) + "&HospitalInfoId=" + HospitalInfoId + "&PermissionID=" + PermissionID);
            //    }
            //    else
            //    {
            //        Response.Redirect("~/Users/UserProfile.aspx?UserId=" + Convert.ToInt32(ListBoxApprovedUsers.SelectedItem.Value) + "&HospitalInfoId=" + HospitalInfoId + "&PermissionID=" + PermissionID);
            //    }
            //}
        }

        /// <summary>
        /// Button Used for Redirect to Request Approval Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatedBy></CreatedBy>
        /// <CreatedOn></CreatedOn>
        protected void LinkButtonApproveUser_Click(object sender, EventArgs e)
        {
            try
            {
                QueryStringHandler.QuerystringParameterEncrpt objectQuerystringParameterEncrypt = new QueryStringHandler.QuerystringParameterEncrpt();
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    Response.Redirect("~/Administrator/RequestApprovalForHospital.aspx?" + objectQuerystringParameterEncrypt.EncrptQuerystringParam("PermissionID=" + PermissionID + "&HospitalID=" + HospitalInfoId), false);
                }
                else
                {
                    Response.Redirect("~/Users/RequestApprovalForHospital.aspx?" + objectQuerystringParameterEncrypt.EncrptQuerystringParam("PermissionID=" + PermissionID + "&HospitalID=" + HospitalInfoId), false);                    
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
        /// <CreatedOn></CreatedOn>
        protected void LinkButtonRegisterNewUser_Click(object sender, EventArgs e)
        {
            try
            {
                QueryStringHandler.QuerystringParameterEncrpt objectQuerystringParameterEncrypt = new QueryStringHandler.QuerystringParameterEncrpt();
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    Response.Redirect("~/Administrator/UserRegistration.aspx?" + objectQuerystringParameterEncrypt.EncrptQuerystringParam("HospitalInfoId=" + HospitalInfoId + "&PermissionID=" + PermissionID), false);
                }
                else if (PermissionID == 1)
                {
                    Response.Redirect("~/Users/UserRegistration.aspx?" + objectQuerystringParameterEncrypt.EncrptQuerystringParam("HospitalInfoId=" + HospitalInfoId + "&PermissionID=" + PermissionID), false);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "LinkButtonRegisterNewUser_Click");
                LogManager._stringObject = "HospitalEditRegistration.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Delete Events Used for delete Hospital Info
        /// Created By : Raman.
        /// Creation Date : August 24, 2009  
        /// </summary>
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _objectBSHospitalInfo = new RMC.BussinessService.BSHospitalInfo();
                if (_objectBSHospitalInfo.LogicalDeleteHospitalInfo(HospitalInfoId, ActiveUser) == true)
                {
                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        Response.Redirect("~/Administrator/DataManagement.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("~/Users/DataManagement.aspx", false);
                    }
                }
                else
                {
                    CommonClass.Show("Record Deleted Already!");
                    //DisplayMessage("Record has already been deleted!", System.Drawing.Color.Green);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Event", "ButtonDelete_Click");
                ex.Data.Add("Class", "HospitalInfo.ascx.cs");
                LogManager._stringObject = "HospitalInfo.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                CommonClass objectCommonClass = new CommonClass();
                string backUrl = objectCommonClass.BackButtonUrl;
                //BackNavigation(backUrl);
                Response.Redirect(backUrl, false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "HospitalEditRegistration.ascx ---- ImageButtonBack_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        protected void DataListApprovedUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}