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
using System.Data.OleDb;
using RMC.BussinessService;
using RMC.BusinessEntities;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class DataManagementHospitalUnit : System.Web.UI.UserControl
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
                        bool flag = MaintainSessions.SessionIsBackNavigation;
                        if(Request.UrlReferrer != null && !flag)
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
                //throw ex.InnerException;
                LogManager._stringObject = "DataManagementHospitalUnit.ascx ---- Page_Load";
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
                LogManager._stringObject = "DataManagementHospitalUnit.ascx ---- ImageButtonBack_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonHospitalInformation_Click(object sender, EventArgs e)
        {
            try
            {
                FillTreeView();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DataManagementHospitalUnit.ascx ---- Page_Load";
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
                int counter = objectCommonClass.TotalRecordInBackUrl - 2;
                if (counter >= 0)
                {
                    Response.Redirect(objectCommonClass.RemoveBackButtonUrl(counter), false);
                }
                else
                {
                    Response.Redirect(objectCommonClass.RemoveBackButtonUrl(0), false);
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "DataManagementHospitalUnit.ascx ---- LinkButtonHospitalIndex_Click";
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
                QueryStringHandler.QuerystringParameterEncrpt objectQueryStringEnc = new QueryStringHandler.QuerystringParameterEncrpt();
                LinkButtonHospitalInformation.Text = "#" + HospitalInformation.HospitalRecordCount + "-" + HospitalInformation.HospitalName + ", " + HospitalInformation.City + ", " + HospitalInformation.State;
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    //LinkButtonHospitalInformation.PostBackUrl = "~/Administrator/HospitalDetail.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&PermissionID=" + HospitalInformation.PermissionID.ToString());
                    LinkButtonAddUnit.PostBackUrl = "~/Administrator/HospitalUnitInfomation.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoID=" + HospitalInformation.HospitalID + "&Page=DataManagementUnit");
                    //LinkButtonHospitalIndex.PostBackUrl = "~/Administrator/DataManagement.aspx";
                    strBuilderEdit.Append("<span style='color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Administrator/HospitalDetail.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + Convert.ToString(HospitalInformation.HospitalID)) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + " - " + HospitalInformation.HospitalName + ", " + HospitalInformation.City + ", " + HospitalInformation.State + "'>( Edit )</a></span>");
                }
                else
                {
                    //LinkButtonHospitalInformation.PostBackUrl = "~/Users/HospitalDetail.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&PermissionID=" + HospitalInformation.PermissionID.ToString());
                    LinkButtonAddUnit.PostBackUrl = "~/Users/HospitalUnitInformation.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoID=" + HospitalInformation.HospitalID + "&Page=DataManagementUnit");
                    //LinkButtonHospitalIndex.PostBackUrl = "~/Users/DataManagement.aspx";
                    strBuilderEdit.Append("<span style='color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Users/HospitalDetail.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + Convert.ToString(HospitalInformation.HospitalID)) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + " - " + HospitalInformation.HospitalName + ", " + HospitalInformation.City + ", " + HospitalInformation.State + "'>( Edit )</a></span>");
                }

                divEditHospital.InnerHtml = strBuilderEdit.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FillTreeView()
        {
            BSDataManagement objectBSDataManagement = new BSDataManagement();
            BSNursePDADetail objectNewBSNursePDADetail = new BSNursePDADetail();
            System.Text.StringBuilder objectTableString = new System.Text.StringBuilder();
            List<RMC.BusinessEntities.BETreeHospitalUnits> objectTreeStructure = null;
            List<RMC.BusinessEntities.BENursePDAFileCounter> objectGenericBENursePDAFileCounter = null;
            try
            {
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    objectTreeStructure = objectBSDataManagement.GetAllActiveHospitalUnits(HospitalInformation.HospitalID);
                }
                else
                {
                    int userID = CommonClass.UserInformation.UserID;
                    objectTreeStructure = objectBSDataManagement.GetAllActiveHospitalUnits(userID, HospitalInformation.HospitalID);
                }
                objectGenericBENursePDAFileCounter = objectNewBSNursePDADetail.GetTotalFilesAndDataPoints(objectTreeStructure.Select(s => s.HospitalDemographicID).ToList());
                if (objectTreeStructure.Count == 0)
                {
                    divEmptyMessage.Visible = true;
                    divHospitalUnitNames.Visible = false;
                }
                objectTableString.Append("<table width='555px' cellspacing='6px'>");
                if (objectTreeStructure.Count > 0)
                {
                    int hospitalUnitCounter = 0;
                    foreach (RMC.BusinessEntities.BETreeHospitalUnits objectHospitalUnits in objectTreeStructure)
                    {
                        int totalFiles = 0, totalRecords = 0;
                        QueryStringHandler.QuerystringParameterEncrpt encrption = new QueryStringHandler.QuerystringParameterEncrpt();
                        string hospitalUnitNavigationUrl;
                        hospitalUnitCounter++;
                        RMC.BusinessEntities.BENursePDAFileCounter objectBENursePDAFileCounter = objectGenericBENursePDAFileCounter.Find(delegate(RMC.BusinessEntities.BENursePDAFileCounter objectBENursePDAFC)
                        {
                            return objectBENursePDAFC.HospitalUnitID == objectHospitalUnits.HospitalDemographicID;
                        });

                        if (objectBENursePDAFileCounter != null)
                        {
                            totalFiles = objectBENursePDAFileCounter.TotalFiles;
                            totalRecords = objectBENursePDAFileCounter.TotalRecords;
                        }

                        if (HttpContext.Current.User.IsInRole("superadmin"))
                        {
                            hospitalUnitNavigationUrl = "../Administrator/DataManagementYear.aspx?" + encrption.EncrptQuerystringParam("HospitalInfoID=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + objectHospitalUnits.HospitalDemographicID + "&PermissionID=" + objectHospitalUnits.PermissionID.ToString() + "&UnitCounter=" + hospitalUnitCounter);
                        }
                        else
                        {
                            hospitalUnitNavigationUrl = "../Users/DataManagementYear.aspx?" + encrption.EncrptQuerystringParam("HospitalInfoID=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + objectHospitalUnits.HospitalDemographicID + "&PermissionID=" + objectHospitalUnits.PermissionID.ToString() + "&UnitCounter=" + hospitalUnitCounter);
                        }
                        objectTableString.Append("<tr><td style='width:300px; font-weight:bold; color:#06569d;'><a style='font-weight:bold; fon-size:10px;' href='" + hospitalUnitNavigationUrl + "'>" + "#" + HospitalInformation.HospitalRecordCount + "-" + hospitalUnitCounter + " - " + objectHospitalUnits.HospitalUnitName + "," + objectHospitalUnits.CreatedDate.ToShortDateString() + (objectHospitalUnits.ModifiedDate.HasValue == true ? "," + objectHospitalUnits.ModifiedDate.Value.ToShortDateString() : "") + "</a><span style='visibility:hidden; display:none;'>" + HospitalInformation.HospitalID + "</span><span style='padding-left:5px; color:#06569d; font-weight:bold;'>(" + totalFiles + "," + totalRecords + ")</span></td>");
                        
                        if (HttpContext.Current.User.IsInRole("superadmin"))
                        {
                            RMC.BussinessService.BSNursePDADetail objectBSNursePDADetail = new BSNursePDADetail();

                            if (objectBSNursePDADetail.CheckForNonValidData(objectHospitalUnits.HospitalDemographicID))
                            {
                                objectTableString.Append("<td align='right'><span style='padding-left:5px; color:#06569d; font-weight:bold;'>( <a style='color:#06569d; font-weight:bold;' href='../Administrator/NonValidData.aspx?" + encrption.EncrptQuerystringParam("HospitalUnitID=" + objectHospitalUnits.HospitalDemographicID.ToString()) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + "-" + hospitalUnitCounter + " - " + objectHospitalUnits.HospitalUnitName + "'>Review Non-Valid Data</a> )</span>");
                                objectTableString.Append("<span style='padding-left:5px; color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Administrator/HospitalDemographicDetail.aspx?" + encrption.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + objectHospitalUnits.HospitalDemographicID + "&PermissionID=" + Request.QueryString["PermissionID"]) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + "-" + hospitalUnitCounter + " - " + objectHospitalUnits.HospitalUnitName + "'>( Edit )</a></span><span style='padding-left:5px; color:#06569D; font-weight:bold;'><a style='color:Red; cursor:pointer; font-weight:bold;' href='javascript:ConfirmMessageForAdministratorHospitalUnit(&quot;" + encrption.EncrptQuerystringParam("HospitalInfoID=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + objectHospitalUnits.HospitalDemographicID) + "&quot;);" + "' title='" + "#" + HospitalInformation.HospitalRecordCount + "-" + hospitalUnitCounter + " - " + objectHospitalUnits.HospitalUnitName + "'>( Delete )</a></span></td>");
                            }
                            else
                            {
                                objectTableString.Append("<td align='right'><span style='padding-left:5px; color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Administrator/HospitalDemographicDetail.aspx?" + encrption.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + objectHospitalUnits.HospitalDemographicID + "&PermissionID=" + Request.QueryString["PermissionID"])  + "' title='" + "#" + HospitalInformation.HospitalRecordCount + "-" + hospitalUnitCounter + " - " + objectHospitalUnits.HospitalUnitName + "'>( Edit )</a></span><span style='padding-left:5px; color:#06569D; font-weight:bold;'><a style='color:Red; cursor:pointer; font-weight:bold;' href='javascript:ConfirmMessageForAdministratorHospitalUnit(&quot;" + encrption.EncrptQuerystringParam("HospitalInfoID=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + objectHospitalUnits.HospitalDemographicID) + "&quot;);'>( Delete )</a></span></td>");
                            }
                        }
                        else
                        {
                            RMC.BussinessService.BSPermission objectBSPermission = new BSPermission();
                            RMC.BussinessService.BSNursePDADetail objectBSNursePDADetail = new BSNursePDADetail();
                            string permission = objectBSPermission.GetPermissionByPermissionID(objectHospitalUnits.PermissionID).ToLower().Trim();

                            if (objectBSNursePDADetail.CheckForNonValidData(objectHospitalUnits.HospitalDemographicID))
                            {
                                objectTableString.Append("<td align='right'><span style='padding-left:5px; color:red; font-weight:bold;'>( <a style='color:red;' href='../Users/NonValidData.aspx?" + encrption.EncrptQuerystringParam("HospitalUnitID=" + objectHospitalUnits.HospitalDemographicID.ToString()) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + "-" + hospitalUnitCounter + " - " + objectHospitalUnits.HospitalUnitName + "'>Review Non-Valid Data</a> )</span>");
                                if (permission == "owner")
                                {
                                    objectTableString.Append("<span style='padding-left:5px; color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Users/HospitalDemographicDetail.aspx?" + encrption.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + objectHospitalUnits.HospitalDemographicID + "&PermissionID=" + objectHospitalUnits.PermissionID) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + "-" + hospitalUnitCounter + " - " + objectHospitalUnits.HospitalUnitName + "'>( Edit )</a></span><span style='padding-left:5px; color:#06569D; font-weight:bold;'><a style='color:Red; cursor:pointer; font-weight:bold;' href='javascript:ConfirmMessageForAdministratorHospitalUnit(&quot;" + encrption.EncrptQuerystringParam("HospitalInfoID=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + objectHospitalUnits.HospitalDemographicID) + "&quot;);" + "' title='" + "#" + HospitalInformation.HospitalRecordCount + "-" + hospitalUnitCounter + " - " + objectHospitalUnits.HospitalUnitName + "'>( Delete )</a></span></td>");
                                }
                                else
                                {
                                    objectTableString.Append("<span style='padding-left:5px; color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Users/HospitalDemographicDetail.aspx?" + encrption.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + objectHospitalUnits.HospitalDemographicID + "&PermissionID=" + objectHospitalUnits.PermissionID) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + "-" + hospitalUnitCounter + " - " + objectHospitalUnits.HospitalUnitName + "'>( View )</a></span></span></td>");
                                }
                            }
                            else
                            {
                                if (permission == "owner")
                                {
                                    objectTableString.Append("<td align='right'><span style='padding-left:5px; color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Users/HospitalDemographicDetail.aspx?" + encrption.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + objectHospitalUnits.HospitalDemographicID + "&PermissionID=" + objectHospitalUnits.PermissionID) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + "-" + hospitalUnitCounter + " - " + objectHospitalUnits.HospitalUnitName + "'>( Edit )</a></span><span style='padding-left:5px; color:#06569D; font-weight:bold;'><a style='color:Red; cursor:pointer; font-weight:bold;' href='javascript:ConfirmMessageForAdministratorHospitalUnit(&quot;" + encrption.EncrptQuerystringParam("HospitalInfoID=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + objectHospitalUnits.HospitalDemographicID) + "&quot;);" + "' title='" + "#" + HospitalInformation.HospitalRecordCount + "-" + hospitalUnitCounter + " - " + objectHospitalUnits.HospitalUnitName + "'>( Delete )</a></span></td>");
                                }
                                else
                                {
                                    objectTableString.Append("<td align='right'><span style='padding-left:5px; color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Users/HospitalDemographicDetail.aspx?" + encrption.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + objectHospitalUnits.HospitalDemographicID + "&PermissionID=" + objectHospitalUnits.PermissionID) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + "-" + hospitalUnitCounter + " - " + objectHospitalUnits.HospitalUnitName + "'>( View )</a></span></span></td>");
                                }
                            }
                        }

                       objectTableString.Append("</tr>");                        
                    }

                    objectTableString.Append("</table>");
                    divHospitalUnitNames.InnerHtml = objectTableString.ToString();
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