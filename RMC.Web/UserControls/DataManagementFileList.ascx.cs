using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using RMC.BussinessService;
using LogExceptions;
using System.IO;

namespace RMC.Web.UserControls
{
    public partial class DataManagementFileList : System.Web.UI.UserControl
    {

        #region Properties

        public string Permission
        {
            get
            {
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    return string.Empty;
                }
                else
                {
                    RMC.BussinessService.BSPermission objectBSPermission = new RMC.BussinessService.BSPermission();
                    int permissionID = 0;
                    int.TryParse(Convert.ToString(Request.QueryString["PermissionID"]), out permissionID);
                    return objectBSPermission.GetPermissionByPermissionID(permissionID).ToLower().Trim();
                }
            }
        }

        private RMC.BusinessEntities.BETreeHospitalInfo HospitalInformation
        {
            get
            {
                RMC.BussinessService.BSDataManagement objectBSDataManagement = null;

                try
                {
                    if (Request.QueryString["HospitalInfoID"] != null)
                    {
                        int hospitalInfoID = 0;
                        int.TryParse(Convert.ToString(Request.QueryString["HospitalInfoID"]), out hospitalInfoID);
                        objectBSDataManagement = new BSDataManagement();

                        if (hospitalInfoID > 0)
                        {
                            return objectBSDataManagement.GetHospitalInfoByHospitalInfoID(hospitalInfoID);
                        }
                        else
                        {
                            return null;
                        }
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

        private RMC.BusinessEntities.BETreeHospitalUnits HospitalUnitInformation
        {
            get
            {
                RMC.BussinessService.BSDataManagement objectBSDataManagement = null;

                try
                {
                    if (Request.QueryString["HospitalDemographicId"] != null)
                    {
                        int hospitalUnitID = 0;
                        int.TryParse(Convert.ToString(Request.QueryString["HospitalDemographicId"]), out hospitalUnitID);
                        objectBSDataManagement = new BSDataManagement();

                        if (hospitalUnitID > 0)
                        {
                            return objectBSDataManagement.GetHospitalUnitsByHospitalUnitID(hospitalUnitID);
                        }
                        else
                        {
                            return null;
                        }
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

        private int HospitalUnitCount
        {
            get
            {
                try
                {
                    if (Request.QueryString["UnitCounter"] != null)
                    {
                        int hospitalUnitCounter = 0;
                        int.TryParse(Convert.ToString(Request.QueryString["UnitCounter"]), out hospitalUnitCounter);
                        return hospitalUnitCounter;
                    }
                    else
                    {
                        return 1;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string Year
        {
            get
            {
                return Convert.ToString(Request.QueryString["Year"]);
            }
        }

        public string Month
        {
            get
            {
                return RMC.BussinessService.BSCommon.GetMonthName(Convert.ToString(Request.QueryString["Month"]));
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {                
                Page.Header.Title = "RMC :: Data Management";
                if (!Page.IsPostBack)
                {
                    CommonClass objectCommonClass = new CommonClass();

                    //Session use in a File Uploader Form.
                    if (Session["FileUploader"] != null)
                    {
                        QueryStringHandler.QuerystringParameterEncrpt objectQuerystringEncrpt = new QueryStringHandler.QuerystringParameterEncrpt();
                        string url = objectCommonClass.BackButtonUrl;
                        //if(HttpContext.Current.User.IsInRole("superadmin"))
                        //{
                        //    url = "~/Administrator/DataManagementFileList.aspx?" + objectQuerystringEncrpt.EncrptQuerystringParam("HospitalInfoId=" + Session["HospitalInfoId"] + "&HospitalDemographicId=" + Session["UnitID"] + "&UnitCounter=" + Session["UnitCounter"] + "&PermissionID=" + Session["PermissionID"] + "&Year=" + Session["Year"] + "&Month=" + Session["Month"] + "&HospitalUnitID=" + Session["UnitID"]); 
                        //}
                        //else
                        //{
                        //    url = "~/Users/DataManagementFileList.aspx?" + objectQuerystringEncrpt.EncrptQuerystringParam("HospitalInfoId=" + Session["HospitalInfoId"] + "&HospitalDemographicId=" + Session["UnitID"] + "&UnitCounter=" + Session["UnitCounter"] + "&PermissionID=" + Session["PermissionID"] + "&Year=" + Session["Year"] + "&Month=" + Session["Month"] + "&HospitalUnitID=" + Session["UnitID"]); 
                        //}
                        if (Session["UnitID"] != null)
                            Session.Remove("UnitID");
                        if (Session["Year"] != null)
                            Session.Remove("Year");
                        if (Session["Month"] != null)
                            Session.Remove("Month");
                        if (Session["HospitalInfoId"] != null)
                            Session.Remove("HospitalInfoId");
                        if (Session["UnitCounter"] != null)
                            Session.Remove("UnitCounter");
                        if (Session["ConfigName"] != null)
                            Session.Remove("ConfigName");
                        Session.Remove("FileUploader");                        
                        Response.Redirect(url, false);
                    }
                    else
                    {
                        if (Session["NonValidFileName"] != null)
                        {
                            string message = @"The following file(s) have invalid/incomplete data : \n\n" + Convert.ToString(Session["NonValidFileName"]);
                            //string message = @"The following file(s) have invalid/incomplete data : \n\n" + Convert.ToString(BSSerialization.Deserialize<string>(Session["NonValidFileName"] as MemoryStream));
                            CommonClass.Show(message);
                            Session["NonValidFileName"] = null;
                        }

                        if (Permission == "readonly" || Permission == "upload data")
                        {
                            MultiViewShowFiles.ActiveViewIndex = 1;
                            LinkButtonDeleteAll.Visible = false;
                            if (Permission == "readonly")
                                LinkButtonAddData.Enabled = false;
                            else
                                LinkButtonAddData.Enabled = true;
                            RepeaterShowUsersFiles.DataBind();
                            if (RepeaterShowUsersFiles.Items.Count == 0)
                            {
                                RepeaterShowUsersFiles.Visible = false;
                                divShowEmptyMessage.Visible = true;
                            }
                        }
                        else
                        {
                            MultiViewShowFiles.ActiveViewIndex = 0;
                            RepeaterShowFileType.DataBind();
                            if (RepeaterShowFileType.Items.Count == 0)
                            {
                                RepeaterShowFileType.Visible = false;
                                divShowEmptyMessage.Visible = true;
                                LinkButtonDeleteAll.Enabled = false;
                            }
                        }

                        LinkHierarchy();
                        if ((Request.UrlReferrer.AbsoluteUri.Substring(Request.UrlReferrer.AbsoluteUri.LastIndexOf('/') + 1) != "EAFUpload.swf" && (Request.QueryString["IsBackUrlAdd"] == null || (Request.QueryString["IsBackUrlAdd"] != null && Request.QueryString["IsBackUrlAdd"].ToString() != "NO"))) || MaintainSessions.SessionIsBackNavigation == true)
                        {
                            objectCommonClass.BackButtonUrl = Request.UrlReferrer.AbsoluteUri;
                        }
                        else if(Request.QueryString["IsBackUrlAdd"] == null || (Request.QueryString["IsBackUrlAdd"] != null && Request.QueryString["IsBackUrlAdd"].ToString() != "NO"))
                        {
                            objectCommonClass.BackButtonUrl = Request.Url.AbsoluteUri; 
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DataManagementFileList.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonDeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> objectGenericHospitalUploadIDs = new List<int>();
                List<int> objectGenericNurseIDs = new List<int>();

                foreach (RepeaterItem rItemParent in RepeaterShowFileType.Items)
                {
                    Repeater rptFileName = (Repeater)rItemParent.FindControl("RepeaterShowFileList");

                    foreach (RepeaterItem rItemChild in rptFileName.Items)
                    {
                        CheckBox chkFileName = (CheckBox)rItemChild.FindControl("CheckBoxFileName");

                        if (chkFileName.Checked)
                        {
                            HiddenField hdfNurseID = (HiddenField)rItemChild.FindControl("HiddenFieldNurseID");
                            HiddenField hdfHospitalUploadID = (HiddenField)rItemChild.FindControl("HiddenFieldHospitalUploadID");
                            int hospitalUploadID = 0, NurseID = 0;

                            int.TryParse(hdfHospitalUploadID.Value, out hospitalUploadID);
                            int.TryParse(hdfNurseID.Value, out NurseID);
                            if (hospitalUploadID > 0)
                            {
                                objectGenericHospitalUploadIDs.Add(hospitalUploadID);
                            }

                            if (NurseID > 0)
                            {
                                objectGenericNurseIDs.Add(NurseID);
                            }
                        }
                    }
                }

                if (objectGenericNurseIDs.Count > 0 || objectGenericHospitalUploadIDs.Count > 0)
                {
                    RMC.BussinessService.BSDataManagement objectBSDataManagement = new RMC.BussinessService.BSDataManagement();

                    objectBSDataManagement.DeleteFilesFromNurseDetail(objectGenericNurseIDs, objectGenericHospitalUploadIDs);
                    RepeaterShowFileType.DataBind();
                    if (RepeaterShowFileType.Items.Count == 0)
                    {
                        CommonClass objectCommonClass = new CommonClass();
                        string backUrl = objectCommonClass.BackButtonUrl;
                        Response.Redirect(backUrl, false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DataManagementFileList.ascx ---- LinkButtonDeleteAll_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonFileName_Click(object sender, EventArgs e)
        {
            try
            {
                RepeaterItem rItemParentRow = (RepeaterItem)((LinkButton)sender).NamingContainer;
                HiddenField hdfNurseID;
                if (rItemParentRow != null)
                {
                    hdfNurseID = (HiddenField)rItemParentRow.FindControl("HiddenFieldNurseID");
                    if (!HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        Response.Redirect("ShowValidData.aspx?NurseID=" + hdfNurseID.Value + "&Permission=" + Permission, false);
                    }
                    else
                    {
                        Response.Redirect("ShowValidData.aspx?NurseID=" + hdfNurseID.Value, false);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DataManagementFileList.ascx ---- LinkButtonFileName_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                CommonClass objectCommonClass = new CommonClass();
                string backUrl = objectCommonClass.BackButtonUrl;
                Response.Redirect(backUrl, false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DataManagementFileList.ascx ---- ImageButtonBack_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonHospitalIndex_Click(object sender, EventArgs e)
        {
            try
            {
                CommonClass objectCommonClass = new CommonClass();

                //Response.Redirect(objectCommonClass.RemoveBackButtonUrl(3), false);
                Response.Redirect(objectCommonClass.RemoveBackButtonUrlString("DataManagement.aspx"), false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DataManagementFileList.ascx ---- LinkButtonHospitalIndex_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonHospitalInformation_Click(object sender, EventArgs e)
        {
            try
            {
                CommonClass objectCommonClass = new CommonClass();
                //objectCommonClass.RemoveBackButtonUrl(2);
                //Response.Redirect(objectCommonClass.BackButtonUrl, false);
                MaintainSessions.SessionIsBackNavigation = true;
                Response.Redirect(objectCommonClass.RemoveBackButtonUrlString("DataManagementUnit.aspx"), false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DataManagementMonth.ascx ---- LinkButtonHospitalInformation_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonHospitalUnitInformation_Click(object sender, EventArgs e)
        {
            try
            {
                CommonClass objectCommonClass = new CommonClass();
                //objectCommonClass.RemoveBackButtonUrl(1);
                //Response.Redirect(objectCommonClass.BackButtonUrl, false);
                MaintainSessions.SessionIsBackNavigation = true;
                Response.Redirect(objectCommonClass.RemoveBackButtonUrlString("DataManagementYear"), false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DataManagementMonth.ascx ---- LinkButtonHospitalUnitInformation_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonYear_Click(object sender, EventArgs e)
        {
            try
            {
                CommonClass objectCommonClass = new CommonClass();
                //objectCommonClass.RemoveBackButtonUrl(0);
                //Response.Redirect(objectCommonClass.BackButtonUrl, false);
                MaintainSessions.SessionIsBackNavigation = true;
                Response.Redirect(objectCommonClass.RemoveBackButtonUrlString("DataManagementMonth"), false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DataManagementMonth.ascx ---- LinkButtonHospitalUnitInformation_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonMonth_Click(object sender, EventArgs e)
        {
            try
            {
                if (Permission == "readonly" || Permission == "upload data")
                {
                    MultiViewShowFiles.ActiveViewIndex = 1;
                    LinkButtonDeleteAll.Visible = false;
                    RepeaterShowUsersFiles.DataBind();
                    if (RepeaterShowUsersFiles.Items.Count == 0)
                    {
                        RepeaterShowUsersFiles.Visible = false;
                        divShowEmptyMessage.Visible = true;
                    }
                }
                else
                {
                    MultiViewShowFiles.ActiveViewIndex = 0;
                    RepeaterShowFileType.DataBind();
                    if (RepeaterShowFileType.Items.Count == 0)
                    {
                        RepeaterShowFileType.Visible = false;
                        divShowEmptyMessage.Visible = true;
                        LinkButtonDeleteAll.Enabled = false;
                    }
                }

                LinkHierarchy();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DataManagementMonth.ascx ---- LinkButtonHospitalUnitInformation_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        #region Private Methods

        private void LinkHierarchy()
        {
            try
            {
                System.Text.StringBuilder strBuilderEdit = new System.Text.StringBuilder();
                System.Text.StringBuilder strBuilderEditUnit = new System.Text.StringBuilder();
                QueryStringHandler.QuerystringParameterEncrpt objectQueryStringEnc = new QueryStringHandler.QuerystringParameterEncrpt();
                LinkButtonHospitalInformation.Text = "#" + HospitalInformation.HospitalRecordCount + "-" + HospitalInformation.HospitalName + ", " + HospitalInformation.City + ", " + HospitalInformation.State;
                LinkButtonHospitalUnitInformation.Text = "#" + HospitalInformation.HospitalRecordCount + "-" + HospitalUnitCount + " - " + HospitalUnitInformation.HospitalUnitName + ", " + HospitalUnitInformation.CreatedDate.ToShortDateString();
                LinkButtonYear.Text = Year;
                LinkButtonMonth.Text = Month;
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    LinkButtonAddData.PostBackUrl = "~/Administrator/FileUploader.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&UnitCounter=" + HospitalUnitCount + "&PermissionID=" + Request.QueryString["PermissionID"] + "&Year=" + Convert.ToString(Request.QueryString["Year"]) + "&Month=" + Convert.ToString(Request.QueryString["Month"]));
                    //LabelYear.PostBackUrl = "~/Administrator/ManageYears.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("Mode=Edit&HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&UnitCounter=" + HospitalUnitCount + "&PermissionID=" + Request.QueryString["PermissionID"] + "&Year=" + Convert.ToString(Request.QueryString["Year"]));
                    //LinkButtonHospitalIndex.PostBackUrl = "~/Administrator/DataManagement.aspx";
                    strBuilderEdit.Append("<span style='color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Administrator/HospitalDetail.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + Convert.ToString(HospitalInformation.HospitalID)) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + " - " + HospitalInformation.HospitalName + ", " + HospitalInformation.City + ", " + HospitalInformation.State + "'>( Edit )</a></span>");
                    strBuilderEditUnit.Append("<span style='padding-left:2px; color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Administrator/HospitalDemographicDetail.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&PermissionID=" + Request.QueryString["PermissionID"]) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + "-" + HospitalUnitCount + " - " + HospitalUnitInformation.HospitalUnitName + "'>( Edit )</a></span>");
                }
                else
                {
                    LinkButtonAddData.PostBackUrl = "~/Users/FileUploader.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&UnitCounter=" + HospitalUnitCount + "&PermissionID=" + Request.QueryString["PermissionID"] + "&Year=" + Convert.ToString(Request.QueryString["Year"]) + "&Month=" + Convert.ToString(Request.QueryString["Month"]));
                    //LabelYear.PostBackUrl = "~/Users/ManageYears.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("Mode=Edit&HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&UnitCounter=" + HospitalUnitCount + "&PermissionID=" + Request.QueryString["PermissionID"] + "&Year=" + Convert.ToString(Request.QueryString["Year"]));
                    //LinkButtonHospitalIndex.PostBackUrl = "~/Users/DataManagement.aspx";
                    strBuilderEdit.Append("<span style='color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Users/HospitalDetail.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + Convert.ToString(HospitalInformation.HospitalID)) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + " - " + HospitalInformation.HospitalName + ", " + HospitalInformation.City + ", " + HospitalInformation.State + "'>( Edit )</a></span>");
                    strBuilderEditUnit.Append("<span style='padding-left:2px; color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Users/HospitalDemographicDetail.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&PermissionID=" + Request.QueryString["PermissionID"]) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + "-" + HospitalUnitCount + " - " + HospitalUnitInformation.HospitalUnitName + "'>( Edit )</a></span>");
                }

                divEditHospital.InnerHtml = strBuilderEdit.ToString();
                divEditHospitalUnit.InnerHtml = strBuilderEditUnit.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
               
    }
}