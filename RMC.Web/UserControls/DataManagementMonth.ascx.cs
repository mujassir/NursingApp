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

namespace RMC.Web.UserControls
{
    public partial class DataManagementMonth : System.Web.UI.UserControl
    {

        #region Properties

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

        private string Permission
        {
            get
            {
                if (Request.QueryString["PermissionID"] != null)
                {
                    int permissionID = 0;
                    if (int.TryParse(Convert.ToString(Request.QueryString["PermissionID"]), out permissionID))
                    {
                        RMC.BussinessService.BSPermission objectPermission = new BSPermission();
                        return objectPermission.GetPermissionByPermissionID(permissionID);
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private string Year
        {
            get
            {
                if (Request.QueryString["Year"] != null)
                {
                    return Convert.ToString(Request.QueryString["Year"]);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.Header.Title = "RMC :: Data Management";
                LinkHierarchy();
                FillTreeView();
                if (!Page.IsPostBack)
                {
                    CommonClass objectCommonClass = new CommonClass();
                    bool flag = RMC.BussinessService.MaintainSessions.SessionIsBackNavigation;
                    if ((Request.UrlReferrer != null && !flag && (Request.QueryString["IsBackUrlAdd"] == null || (Request.QueryString["IsBackUrlAdd"] != null && Request.QueryString["IsBackUrlAdd"].ToString() != "NO"))) || flag)
                        objectCommonClass.BackButtonUrl = Request.UrlReferrer.AbsoluteUri;
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DataManagementMonth.ascx ---- Page_Load";
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
                LogManager._stringObject = "DataManagementMonth.ascx ---- ImageButtonBack_Click";
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

                Response.Redirect(objectCommonClass.RemoveBackButtonUrl(2), false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DataManagementMonth.ascx ---- LinkButtonHospitalIndex_Click";
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
                objectCommonClass.RemoveBackButtonUrl(1);
                Response.Redirect(objectCommonClass.BackButtonUrl, false);
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
                objectCommonClass.RemoveBackButtonUrl(0);
                Response.Redirect(objectCommonClass.BackButtonUrl, false);
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
                FillTreeView();
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
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    LinkButtonAddMonth.PostBackUrl = "~/Administrator/ManageMonth.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&UnitCounter=" + HospitalUnitCount + "&PermissionID=" + Request.QueryString["PermissionID"] + "&Year=" + Convert.ToString(Request.QueryString["Year"]));
                    //LabelYear.PostBackUrl = "~/Administrator/ManageYears.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("Mode=Edit&HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&UnitCounter=" + HospitalUnitCount + "&PermissionID=" + Request.QueryString["PermissionID"] + "&Year=" + Convert.ToString(Request.QueryString["Year"]));
                    //LinkButtonHospitalIndex.PostBackUrl = "~/Administrator/DataManagement.aspx";
                    strBuilderEdit.Append("<span style='color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Administrator/HospitalDetail.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + Convert.ToString(HospitalInformation.HospitalID)) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + " - " + HospitalInformation.HospitalName + ", " + HospitalInformation.City + ", " + HospitalInformation.State + "'>( Edit )</a></span>");
                    strBuilderEditUnit.Append("<span style='padding-left:2px; color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Administrator/HospitalDemographicDetail.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&PermissionID=" + Request.QueryString["PermissionID"]) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + "-" + HospitalUnitCount + " - " + HospitalUnitInformation.HospitalUnitName + "'>( Edit )</a></span>");
                }
                else
                {
                    LinkButtonAddMonth.PostBackUrl = "~/Users/ManageMonth.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&UnitCounter=" + HospitalUnitCount + "&PermissionID=" + Request.QueryString["PermissionID"] + "&Year=" + Convert.ToString(Request.QueryString["Year"]));
                    strBuilderEdit.Append("<span style='color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Users/HospitalDetail.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + Convert.ToString(HospitalInformation.HospitalID)) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + " - " + HospitalInformation.HospitalName + ", " + HospitalInformation.City + ", " + HospitalInformation.State + "'>( Edit )</a></span>");
                    strBuilderEditUnit.Append("<span style='padding-left:2px; color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Users/HospitalDemographicDetail.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&PermissionID=" + Request.QueryString["PermissionID"]) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + "-" + HospitalUnitCount + " - " + HospitalUnitInformation.HospitalUnitName + "'>( Edit )</a></span>");

                    if (Permission.ToLower().Trim() == "readonly")
                    {
                        LinkButtonAddMonth.Enabled = false;
                    }
                    //LabelYear.PostBackUrl = "~/Users/ManageYears.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("Mode=Edit&HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&UnitCounter=" + HospitalUnitCount + "&PermissionID=" + Request.QueryString["PermissionID"] + "&Year=" + Convert.ToString(Request.QueryString["Year"]));
                    //LinkButtonHospitalIndex.PostBackUrl = "~/Users/DataManagement.aspx";
                }
                divEditHospital.InnerHtml = strBuilderEdit.ToString();
                divEditHospitalUnit.InnerHtml = strBuilderEditUnit.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FillTreeView()
        {
            BSDataManagement objectBSDataManagement = new BSDataManagement();
            System.Text.StringBuilder objectTableString = new System.Text.StringBuilder();
            BSNursePDADetail objectBSNursePDADetail = new BSNursePDADetail();
            List<RMC.DataService.Month> objectTreeStructure = null;
            List<RMC.BusinessEntities.BENursePDAFileCounter> objectBENursePDAFileCounter = null;
            try
            {
                objectTreeStructure = objectBSDataManagement.GetMonthByHospitalUnitID(HospitalUnitInformation.HospitalDemographicID, Year);
                objectBENursePDAFileCounter = objectBSNursePDADetail.GetTotalFilesAndDataPoints(HospitalUnitInformation.HospitalDemographicID, Year, objectTreeStructure.Select(s => s.MonthName).ToList());
                if (objectTreeStructure.Count == 0)
                {
                    divEmptyMessage.Visible = true;
                    divMonths.Visible = false;
                }

                if (objectTreeStructure.Count > 0)
                {
                    if (objectTreeStructure.Count == 12)
                    {
                        LinkButtonAddMonth.Enabled = false;
                    }
                    objectTableString.Append("<table width='555px' cellspacing='6px'>");
                    foreach (RMC.DataService.Month objectMonth in objectTreeStructure)
                    {
                        QueryStringHandler.QuerystringParameterEncrpt objectQuerystringEncrypt = new QueryStringHandler.QuerystringParameterEncrpt();

                        string url = string.Empty;
                        int totalFiles = 0, totalRecords = 0;
                        RMC.BusinessEntities.BENursePDAFileCounter objectBENursePDAFC = objectBENursePDAFileCounter.Find(delegate(RMC.BusinessEntities.BENursePDAFileCounter objectBENursePDA)
                            {
                                return objectBENursePDA.Month == objectMonth.MonthName;
                            });
                        if (objectBENursePDAFC != null)
                        {
                            totalFiles = objectBENursePDAFC.TotalFiles;
                            totalRecords = objectBENursePDAFC.TotalRecords;
                        }
                        if (HttpContext.Current.User.IsInRole("superadmin"))
                        {
                            url = "../Administrator/DataManagementFileList.aspx?" + objectQuerystringEncrypt.EncrptQuerystringParam("HospitalUnitID=" + HospitalUnitInformation.HospitalDemographicID + "&PermissionID=" + Convert.ToString(Request.QueryString["PermissionID"]) + "&Year=" + Year + "&Month=" + objectMonth.MonthName + "&HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&UnitCounter=" + HospitalUnitCount);
                            objectTableString.Append("<tr><td style='width:440px; font-weight:bold; color:#06569d;'><a style='font-weight:bold; fon-size:10px;' href='" + url + "' title='" + BSCommon.GetMonthName(objectMonth.MonthName) + "'>" + BSCommon.GetMonthName(objectMonth.MonthName) + "</a><span style='padding-left:5px; color:#06569d; font-weight:bold;'>( Files : " + totalFiles
                            + ", Records : " + totalRecords
                            + " )</span></td>" + "<td align='right'><span style='padding-left:5px; color:red; font-weight:bold;'>( <a style='color:red; cursor:pointer; font-weight:bold;' onclick='deleteAllFiles(&quot;" + objectQuerystringEncrypt.EncrptQuerystringParam("Type=DataImport&Year=" + Year + "&Month=" + objectMonth.MonthName + "&HospitalUnitID=" + HospitalUnitInformation.HospitalDemographicID + "&HospitalInfoID=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&UnitCounter=" + HospitalUnitCount + "&PermissionID=" + Request.QueryString["PermissionID"]) + "&quot;)" + "' title='" + BSCommon.GetMonthName(objectMonth.MonthName) + "'>Delete Month</a> )</span></td></tr>");
                        }
                        else
                        {
                            url = "../Users/DataManagementFileList.aspx?" + objectQuerystringEncrypt.EncrptQuerystringParam("HospitalUnitID=" + HospitalUnitInformation.HospitalDemographicID + "&PermissionID=" + Convert.ToString(Request.QueryString["PermissionID"]) + "&Year=" + Year + "&Month=" + objectMonth.MonthName + "&HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&UnitCounter=" + HospitalUnitCount);
                            if (Permission.ToLower().Trim() == "owner")
                            {
                                objectTableString.Append("<tr><td style='width:440px; font-weight:bold; color:#06569d;'><a style='font-weight:bold; fon-size:10px;' href='" + url + "' title='" + BSCommon.GetMonthName(objectMonth.MonthName) + "'>" + BSCommon.GetMonthName(objectMonth.MonthName) + "</a><span style='padding-left:5px; color:#06569d; font-weight:bold;'>( Files : " + totalFiles
                                + ", Records : " + totalRecords
                                + " )</span></td>" + "<td align='right'><span style='padding-left:5px; color:red; font-weight:bold;'>( <a style='color:red; cursor:pointer; font-weight:bold;' onclick='deleteAllFiles(&quot;" + objectQuerystringEncrypt.EncrptQuerystringParam("Type=DataImport&Year=" + Year + "&Month=" + objectMonth.MonthName + "&HospitalUnitID=" + HospitalUnitInformation.HospitalDemographicID + "&HospitalInfoID=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&UnitCounter=" + HospitalUnitCount + "&PermissionID=" + Request.QueryString["PermissionID"]) + "&quot;)" + "' title='" + BSCommon.GetMonthName(objectMonth.MonthName) + "'>Delete Month</a> )</span></td></tr>");
                            }
                            else
                            {
                                objectTableString.Append("<tr><td style='width:440px; font-weight:bold; color:#06569d;'><a style='font-weight:bold; fon-size:10px;' href='" + url + "' title='" + BSCommon.GetMonthName(objectMonth.MonthName) + "'>" + BSCommon.GetMonthName(objectMonth.MonthName) + "</a><span style='padding-left:5px; color:#06569d; font-weight:bold;'>( Files : " + totalFiles
                                + ", Records : " + totalRecords
                                + " )</span></td></tr>");
                            }
                        }
                    }

                    objectTableString.Append("</table>");
                    divMonths.InnerHtml = objectTableString.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }

}