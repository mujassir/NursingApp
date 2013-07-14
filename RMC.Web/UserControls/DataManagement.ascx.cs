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
    public partial class DataManagement : System.Web.UI.UserControl
    {
        public RMC.DataService.UserInfo UserInformation
        {
            get
            {
                //return CommonClass.UserInformation;
                return CommonClass.UserInformation;
            }
        }

        #region Variables

        int maxRecords = 30;

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    QueryStringHandler.QuerystringParameterEncrpt objectQueryStringEncrypt = new QueryStringHandler.QuerystringParameterEncrpt();
                    CommonClass objectCommonClass = new CommonClass();
                    int totalBackUrl = objectCommonClass.TotalRecordInBackUrl;
                    if (Request.UrlReferrer != null && totalBackUrl > 1)
                    {
                        objectCommonClass.RemoveBackButtonUrl(totalBackUrl - 2);                    
                    }
                    if(Request.UrlReferrer != null)
                        objectCommonClass.BackButtonUrl = Request.UrlReferrer.AbsoluteUri;

                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        LinkButtonAddHospital.PostBackUrl = "~/Administrator/HospitalRegistration.aspx?" + objectQueryStringEncrypt.EncrptQuerystringParam("Page=DataManagement");
                        LinkButtonAddHospital.Visible = true;
                    }
                    else if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        LinkButtonAddHospital.PostBackUrl = "~/Users/HospitalRegistration.aspx?" + objectQueryStringEncrypt.EncrptQuerystringParam("Page=DataManagement");
                        LinkButtonAddHospital.Visible = false;
                    }
                    else if (HttpContext.Current.User.IsInRole("admin") && (((UserInformation.IsActive && UserInformation.AccessRequest == "Owner") || (UserInformation.IsActive && UserInformation.AccessRequest == null))))
                    {
                        LinkButtonAddHospital.PostBackUrl = "~/Users/HospitalRegistration.aspx?" + objectQueryStringEncrypt.EncrptQuerystringParam("Page=DataManagement");
                        LinkButtonAddHospital.Visible = true;
                    }
                    else if (HttpContext.Current.User.IsInRole("admin") && (((UserInformation.IsActive && UserInformation.AccessRequest == "ReadOnly") || (UserInformation.IsActive && UserInformation.AccessRequest == null))))
                    {
                        LinkButtonAddHospital.PostBackUrl = "~/Users/HospitalRegistration.aspx?" + objectQueryStringEncrypt.EncrptQuerystringParam("Page=DataManagement");
                        trAddHospital.Visible = false;
                        LinkButtonAddHospital.Visible = false;
                    }
                }

                FillTreeView();
            }
            catch (Exception ex)
            {
                //throw ex.InnerException;
                LogManager._stringObject = "DataManagement.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Fill tree view with Hospital Names.
        /// </summary>
        private void FillTreeView()
        {
            System.Text.StringBuilder objectTableString = new System.Text.StringBuilder();
            BSDataManagement objectBSDataManagement = new BSDataManagement();
            List<RMC.BusinessEntities.BETreeHospitalInfo> objectTreeStructure = null;
            try
            {
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    objectTreeStructure = objectBSDataManagement.GetAllActiveHospitalInfo(false);
                }
                else
                {
                    int userID = CommonClass.UserInformation.UserID;
                    objectTreeStructure = objectBSDataManagement.GetAllActiveHospitalInfoByUserID(userID, false);
                }

                if (objectTreeStructure.Count == 0)
                {
                    divEmptyMessage.Visible = true;
                    divHospitalNames.Visible = false;
                }

                objectTableString.Append("<table width='555px' cellspacing='6px'>");
                foreach (RMC.BusinessEntities.BETreeHospitalInfo objectHospitalInfo in objectTreeStructure)
                {
                    string hospitalNavigateUrl;

                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        QueryStringHandler.QuerystringParameterEncrpt encrption = new QueryStringHandler.QuerystringParameterEncrpt();
                        hospitalNavigateUrl = "../Administrator/DataManagementUnit.aspx?" + encrption.EncrptQuerystringParam("HospitalInfoId=" + objectHospitalInfo.HospitalID + "&PermissionID=" + objectHospitalInfo.PermissionID.ToString());
                        objectTableString.Append("<tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold; font-size:10px;' href='" + hospitalNavigateUrl + "'>" + "#" + objectHospitalInfo.HospitalRecordCount + " - " + objectHospitalInfo.HospitalName + ", " + objectHospitalInfo.City + ", " + objectHospitalInfo.State + "</a></td></td><td align='right'><span style='padding-left:5px; color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Administrator/HospitalDetail.aspx?" + encrption.EncrptQuerystringParam("HospitalInfoId=" + Convert.ToString(objectHospitalInfo.HospitalID)) + "' title='" + "#" + objectHospitalInfo.HospitalRecordCount + " - " + objectHospitalInfo.HospitalName + ", " + objectHospitalInfo.City + ", " + objectHospitalInfo.State + "'>( Edit )</a></span>" + "<span style='padding-left:5px; color:Red'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:red;' href='Javascript:ConfirmMessageForAdministrator(&quot;" + encrption.EncrptQuerystringParam("HospitalID=" + Convert.ToString(objectHospitalInfo.HospitalID)) + "&quot;);' title='" + "#" + objectHospitalInfo.HospitalRecordCount + " - " + objectHospitalInfo.HospitalName + ", " + objectHospitalInfo.City + ", " + objectHospitalInfo.State + "'>( Delete )</a></span></td></tr>");
                    }
                    else
                    {
                        QueryStringHandler.QuerystringParameterEncrpt encrption = new QueryStringHandler.QuerystringParameterEncrpt();
                        hospitalNavigateUrl = "../Users/DataManagementUnit.aspx?" + encrption.EncrptQuerystringParam("HospitalInfoId=" + objectHospitalInfo.HospitalID + "&PermissionID=" + objectHospitalInfo.PermissionID.ToString());
                        if (objectHospitalInfo.PermissionID > 1)
                        {
                            objectTableString.Append("<tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold; font-size:10px;' href='" + hospitalNavigateUrl + "'>" + "#" + objectHospitalInfo.HospitalRecordCount + " - " + objectHospitalInfo.HospitalName + ", " + objectHospitalInfo.City + ", " + objectHospitalInfo.State + "</a></td><td align='right'><span style='padding-left:5px; color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Users/HospitalDetail.aspx?" + encrption.EncrptQuerystringParam("HospitalInfoId=" + Convert.ToString(objectHospitalInfo.HospitalID) + "&PermissionID=" + objectHospitalInfo.PermissionID.ToString()) + "' title='" + "#" + objectHospitalInfo.HospitalRecordCount + " - " + objectHospitalInfo.HospitalName + ", " + objectHospitalInfo.City + ", " + objectHospitalInfo.State + "'>( View )</a></span></td></tr>");
                        }
                        else
                        {
                            objectTableString.Append("<tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold; font-size:10px;' href='" + hospitalNavigateUrl + "'>" + "#" + objectHospitalInfo.HospitalRecordCount + " - " + objectHospitalInfo.HospitalName + ", " + objectHospitalInfo.City + ", " + objectHospitalInfo.State + "</a></td><td align='right'><span style='padding-left:5px; color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Users/HospitalDetail.aspx?" + encrption.EncrptQuerystringParam("HospitalInfoId=" + Convert.ToString(objectHospitalInfo.HospitalID) + "&PermissionID=" + objectHospitalInfo.PermissionID.ToString()) + "' title='" + "#" + objectHospitalInfo.HospitalRecordCount + " - " + objectHospitalInfo.HospitalName + ", " + objectHospitalInfo.City + ", " + objectHospitalInfo.State + "'>( Edit )</a></span>" + "<span style='padding-left:5px; color:Red'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:Red;' href='Javascript:ConfirmMessageForAdministrator(&quot;" + encrption.EncrptQuerystringParam("HospitalID=" + Convert.ToString(objectHospitalInfo.HospitalID)) + "&quot;);' title='" + "#" + objectHospitalInfo.HospitalRecordCount + " - " + objectHospitalInfo.HospitalName + ", " + objectHospitalInfo.City + ", " + objectHospitalInfo.State + "'>( Delete )</a></span></td></tr>");
                        }
                    }
                }

                objectTableString.Append("</table>");
                divHospitalNames.InnerHtml = objectTableString.ToString();
            }
            catch (Exception ex)
            {
                //Response.Write(ex);
                //throw new Exception("FillTreeView");
                throw ex;
            }
        }

        #endregion

    }
}