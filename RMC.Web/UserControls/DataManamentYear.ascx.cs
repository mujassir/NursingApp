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
using RMC.BusinessEntities;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class DataManamentYear : System.Web.UI.UserControl
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
                    if (Request.QueryString["FromPage"] == null)
                    {
                        bool flag = RMC.BussinessService.MaintainSessions.SessionIsBackNavigation;
                        if (Request.UrlReferrer != null && !flag)
                            objectCommonClass.BackButtonUrl = Request.UrlReferrer.AbsoluteUri;
                    }
                    else
                    {
                        if (Request.Url != null)
                        {
                            string currentUrl;
                            string listLastUrl = objectCommonClass.BackButtonUrl;
                            int currentIndex = Request.Url.AbsoluteUri.IndexOf('?');
                            int listIndex = listLastUrl.IndexOf('?');

                            if (currentIndex > 0)
                            {
                                currentUrl = Request.Url.AbsoluteUri.Substring(0, currentIndex);
                            }
                            else
                            {
                                currentUrl = Request.Url.AbsoluteUri;
                            }

                            if (listIndex > 0)
                            {
                                listLastUrl = listLastUrl.Substring(0, listIndex);
                            }

                            if (listLastUrl == currentUrl)
                            {
                                objectCommonClass.RemoveBackButtonUrl(1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DataManamentYear.ascx ---- Page_Load";
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
                Response.Redirect(objectCommonClass.BackButtonUrl, false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DataManamentYear.ascx ---- ImageButtonBack_Click";
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
                Response.Redirect(objectCommonClass.BackButtonUrl, false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DataManamentYear.ascx ---- LinkButtonHospitalInformation_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonHospitalUnitInformation_Click(object sender, EventArgs e)
        {
            try
            {
                FillTreeView();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DataManamentYear.ascx ---- LinkButtonHospitalUnitInformation_Click";
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

                Response.Redirect(objectCommonClass.RemoveBackButtonUrl(1), false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DataManamentYear.ascx ---- LinkButtonHospitalIndex_Click";
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
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {                    
                    LinkButtonAddYear.PostBackUrl = "~/Administrator/ManageYears.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&UnitCounter=" + HospitalUnitCount + "&PermissionID=" + Request.QueryString["PermissionID"]);
                    strBuilderEdit.Append("<span style='color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Administrator/HospitalDetail.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + Convert.ToString(HospitalInformation.HospitalID)) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + " - " + HospitalInformation.HospitalName + ", " + HospitalInformation.City + ", " + HospitalInformation.State + "'>( Edit )</a></span>");
                    strBuilderEditUnit.Append("<span style='padding-left:2px; color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Administrator/HospitalDemographicDetail.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&PermissionID=" + Request.QueryString["PermissionID"]) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + "-" + HospitalUnitCount + " - " + HospitalUnitInformation.HospitalUnitName + "'>( Edit )</a></span>");
                }
                else
                {                   
                    LinkButtonAddYear.PostBackUrl = "~/Users/ManageYears.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&UnitCounter=" + HospitalUnitCount + "&PermissionID=" + Request.QueryString["PermissionID"]);
                    strBuilderEdit.Append("<span style='color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Users/HospitalDetail.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + Convert.ToString(HospitalInformation.HospitalID)) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + " - " + HospitalInformation.HospitalName + ", " + HospitalInformation.City + ", " + HospitalInformation.State + "'>( Edit )</a></span>");
                    strBuilderEditUnit.Append("<span style='padding-left:2px; color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Users/HospitalDemographicDetail.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&PermissionID=" + Request.QueryString["PermissionID"]) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + "-" + HospitalUnitCount + " - " + HospitalUnitInformation.HospitalUnitName + "'>( Edit )</a></span>");
                    if(Permission.ToLower().Trim() == "readonly")
                        LinkButtonAddYear.Enabled = false;
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
            BSNursePDADetail objectBSNursePDADetail = new BSNursePDADetail();
            System.Text.StringBuilder objectTableString = new System.Text.StringBuilder();
            List<RMC.DataService.Year> objectTreeStructure = null;
            List<RMC.BusinessEntities.BENursePDAFileCounter> objectGenericBENursePDAFileCounter = null;
            try
            {
                objectTreeStructure = objectBSDataManagement.GetYearByHospitalUnitID(HospitalUnitInformation.HospitalDemographicID);
                objectGenericBENursePDAFileCounter = objectBSNursePDADetail.GetTotalFilesAndDataPoints(HospitalUnitInformation.HospitalDemographicID, objectTreeStructure.Select(s=>s.Year1).ToList());
                if (objectTreeStructure.Count == 0)
                {                
                    divEmptyMessage.Visible = true;
                    divYears.Visible = false;
                }

                if (objectTreeStructure.Count > 0)
                {
                    objectTableString.Append("<table width='555px' cellspacing='6px'>");
                    foreach (RMC.DataService.Year objectYear in objectTreeStructure)
                    {
                        int totalFiles = 0, totalRecords = 0;
                        QueryStringHandler.QuerystringParameterEncrpt objectQuerystringEncrypt = new QueryStringHandler.QuerystringParameterEncrpt();
                        RMC.BusinessEntities.BENursePDAFileCounter objectBENursePDAFileCounter = objectGenericBENursePDAFileCounter.Find(delegate (RMC.BusinessEntities.BENursePDAFileCounter objectBENursePDAFC)
                        {
                            return objectBENursePDAFC.Year.Trim() == objectYear.Year1;
                        });

                        if (objectBENursePDAFileCounter != null)
                        {
                            totalFiles = objectBENursePDAFileCounter.TotalFiles;
                            totalRecords = objectBENursePDAFileCounter.TotalRecords;
                        }

                        if (HttpContext.Current.User.IsInRole("superadmin"))
                        {
                            objectTableString.Append("<tr><td style='width:450px; font-weight:bold;'><a style='font-weight:bold; font-size:10px;' href='DataManagementMonth.aspx?" + objectQuerystringEncrypt.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&UnitCounter=" + HospitalUnitCount + "&PermissionID=" + Request.QueryString["PermissionID"] + "&Year=" + objectYear.Year1) + "'>" + objectYear.Year1.ToString() + "</a><span style='padding-left:5px; color:#06569d; font-weight:bold;'>( Files : " + totalFiles + ", Records : " + totalRecords + " )</span></td>" + "<td align='right'><span style='padding-left:5px; color:red; font-weight:bold;'>( <a style='color:red; cursor:pointer; font-weight:bold;' onclick='deleteFilesByYear(&quot;" + objectQuerystringEncrypt.EncrptQuerystringParam("Type=dataimportwithoutMonth&Year=" + objectYear.Year1 + "&HospitalUnitID=" + HospitalUnitInformation.HospitalDemographicID.ToString() + "&HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&UnitCounter=" + HospitalUnitCount + "&PermissionID=" + Request.QueryString["PermissionID"]) + "&quot;)' title='" + objectYear.Year1.ToString() + "'>Delete Year</a> )</span></td></tr>");
                        }
                        else
                        {
                            if (Permission.ToLower().Trim() == "readonly" || Permission.ToLower().Trim() == "upload data")
                            {
                                objectTableString.Append("<tr><td style='width:450px; font-weight:bold;'><a style='font-weight:bold; font-size:10px;' href='DataManagementMonth.aspx?" + objectQuerystringEncrypt.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&UnitCounter=" + HospitalUnitCount + "&PermissionID=" + Request.QueryString["PermissionID"] + "&Year=" + objectYear.Year1) + "'>" + objectYear.Year1.ToString() + "</a><span style='padding-left:5px; color:#06569d; font-weight:bold;'>( Files : " + totalFiles + ", Records : " + totalRecords + " )</span></td></tr>");
                            }
                            else
                            {
                                objectTableString.Append("<tr><td style='width:450px; font-weight:bold;'><a style='font-weight:bold; font-size:10px;' href='DataManagementMonth.aspx?" + objectQuerystringEncrypt.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&UnitCounter=" + HospitalUnitCount + "&PermissionID=" + Request.QueryString["PermissionID"] + "&Year=" + objectYear.Year1) + "'>" + objectYear.Year1.ToString() + "</a><span style='padding-left:5px; color:#06569d; font-weight:bold;'>( Files : " + totalFiles + ", Records : " + totalRecords + " )</span></td>" + "<td align='right'><span style='padding-left:5px; color:red; font-weight:bold;'>( <a style='color:red; cursor:pointer; font-weight:bold;' onclick='deleteFilesByYear(&quot;" + objectQuerystringEncrypt.EncrptQuerystringParam("Type=dataimportwithoutMonth&Year=" + objectYear.Year1 + "&HospitalUnitID=" + HospitalUnitInformation.HospitalDemographicID.ToString() + "&HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&UnitCounter=" + HospitalUnitCount + "&PermissionID=" + Request.QueryString["PermissionID"]) + "&quot;)' title='" + objectYear.Year1.ToString() + "'>Delete Year</a> )</span></td></tr>");                                
                            }
                        }
                    }

                    objectTableString.Append("</table>");
                    divYears.InnerHtml = objectTableString.ToString();
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