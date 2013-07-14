using System;
using System.Collections.Generic;
using System.Collections;
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
using System.Text;
using RMC.BussinessService;

namespace RMC.Web.UserControls
{
    public partial class ucAdminTreeView : System.Web.UI.UserControl
    {

        #region Properties

        public int HospitalUnitID
        {
            get
            {
                int hospitalUnitID = 0;
                bool flag = int.TryParse(Convert.ToString(Request.QueryString["HospitalUnitID"]), out hospitalUnitID);
                if (flag)
                {
                    return hospitalUnitID;
                }
                else
                {
                    return 0;
                }
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            BSTreeView objectTreeView = new BSTreeView();
            List<RMC.BusinessEntities.BETreeHospitalInfo> objectTreeStructure = null;

            if (Session["NonValidFileName"] != null)
            {
                string message = "The following file(s) have invalid/incomplete data : " + Convert.ToString(Session["NonValidFileName"]);
                CommonClass.Show(message);
                Session["NonValidFileName"] = null;
            }
            if (HttpContext.Current.User.IsInRole("superadmin"))
            {
                objectTreeStructure = objectTreeView.GetAllActiveHospitalInfo(false);
            }
            else
            {
                //if (!CommonClass.UserInformation.IsActive)
                //{
                //    Response.Redirect("~/Users/InActiveUser.aspx");
                //}

                int userID = CommonClass.UserInformation.UserID;
                objectTreeStructure = objectTreeView.GetAllActiveHospitalInfoByUserID(userID, false);
            }

            StringBuilder objectSeringButilder = new StringBuilder();
            objectSeringButilder.Append("<ul id='ulHospitalTree' class='treeview-gray' style='color:#06569D' ><b>List of hospitals</b>");
            foreach (RMC.BusinessEntities.BETreeHospitalInfo objectHospitalInfo in objectTreeStructure)
            {
                objectSeringButilder.Append("<li>");
                objectSeringButilder.Append("<span'><a href='HospitalDetail.aspx?HospitalInfoId=" + objectHospitalInfo.HospitalID + "&PermissionID=" + objectHospitalInfo.PermissionID.ToString() + "'>" + "#" + objectHospitalInfo.HospitalID + "-" + objectHospitalInfo.HospitalName + ", " + objectHospitalInfo.City + ", " + objectHospitalInfo.State + "</a></span>");
                if (objectHospitalInfo.HospitalUnitsList != null)
                {
                    if (objectHospitalInfo.HospitalUnitsList.Count > 0)
                    {
                        objectSeringButilder.Append("<ul>");
                        foreach (RMC.BusinessEntities.BETreeHospitalUnits objectHospitalUnits in objectHospitalInfo.HospitalUnitsList)
                        {
                            if (HospitalUnitID > 0)
                            {
                                if (HospitalUnitID == objectHospitalUnits.HospitalDemographicID)
                                {
                                    objectSeringButilder.Append("<li>");
                                }
                                else
                                {
                                    objectSeringButilder.Append("<li class='closed'>");
                                }
                            }
                            else
                            {
                                objectSeringButilder.Append("<li class='closed'>");
                            }
                            objectSeringButilder.Append("<span><a href='HospitalDemographicDetail.aspx?HospitalDemographicId=" + objectHospitalUnits.HospitalDemographicID + "&PermissionID=" + objectHospitalUnits.PermissionID.ToString() + "'><u>" + objectHospitalUnits.HospitalDemographicID + "#" + " " + "-" + objectHospitalUnits.HospitalUnitName + "," + objectHospitalUnits.CreatedDate.ToShortDateString() + (objectHospitalUnits.ModifiedDate.HasValue == true ? "," + objectHospitalUnits.ModifiedDate.Value.ToShortDateString() : "") + "</u></a>");
                            if (HttpContext.Current.User.IsInRole("superadmin"))
                            {
                                RMC.BussinessService.BSNursePDADetail objectBSNursePDADetail = new BSNursePDADetail();
                                objectSeringButilder.Append("   ( <a href='../Administrator/FileUploader.aspx?HospitalDemographicId=" + objectHospitalUnits.HospitalDemographicID + "&PermissionID=" + objectHospitalUnits.PermissionID.ToString() + "'>Add Data</a> )");

                                if (objectBSNursePDADetail.CheckForNonValidData(objectHospitalUnits.HospitalDemographicID))
                                {
                                    objectSeringButilder.Append("   ( <a href='../Administrator/NonValidData.aspx?HospitalUnitID=" + objectHospitalUnits.HospitalDemographicID.ToString() + "'>Review Non-Valid Data</a> )");
                                }
                                objectSeringButilder.Append("</span>");
                            }
                            else
                            {
                                RMC.BussinessService.BSPermission objectBSPermission = new BSPermission();
                                RMC.BussinessService.BSNursePDADetail objectBSNursePDADetail = new BSNursePDADetail();
                                string permission = objectBSPermission.GetPermissionByPermissionID(objectHospitalUnits.PermissionID).ToLower().Trim();
                                if (permission == "upload data" || permission == "owner")
                                {
                                    objectSeringButilder.Append("   ( <a href='../Users/FileUploader.aspx?HospitalDemographicId=" + objectHospitalUnits.HospitalDemographicID + "&PermissionID=" + objectHospitalUnits.PermissionID.ToString() + "'>Add Data</a> )</span>");
                                }

                                if (objectBSNursePDADetail.CheckForNonValidData(objectHospitalUnits.HospitalDemographicID))
                                {
                                    objectSeringButilder.Append("   ( <a href='../Users/NonValidData.aspx?HospitalUnitID=" + objectHospitalUnits.HospitalDemographicID.ToString() + "'>Review Non-Valid Data</a> )");
                                }
                                objectSeringButilder.Append("</span>");
                            }
                            if (objectHospitalUnits.HospitalUnitsYears != null)
                            {
                                if (objectHospitalUnits.HospitalUnitsYears.Count > 0)
                                {
                                    objectSeringButilder.Append(GetYearsMonth(objectHospitalUnits.HospitalUnitsYears, objectHospitalUnits.HospitalDemographicID, objectHospitalInfo.PermissionID));
                                }
                            }
                            objectSeringButilder.Append("</li>");
                        }
                        objectSeringButilder.Append("</ul>");
                    }
                }
                objectSeringButilder.Append("</li>");
            }
            objectSeringButilder.Append("</ul>");
            divTreeView.InnerHtml = objectSeringButilder.ToString();

        }

        #endregion

        #region Private Methods

        private StringBuilder GetYearsMonth(List<RMC.BusinessEntities.BETreeYears> objectHospitalUnitsYears, int hospitalUnitID, int permissionID)
        {
            StringBuilder objectSeringButilder = new StringBuilder();
            objectSeringButilder.Append("<ul>");
            foreach (RMC.BusinessEntities.BETreeYears objectBETreeYears in objectHospitalUnitsYears)
            {
                if (objectBETreeYears.Year != null)
                {
                    objectSeringButilder.Append("<li>");
                    objectSeringButilder.Append("<span>" + objectBETreeYears.Year.ToString() + "</span>");
                    if (objectBETreeYears.HospitalUnitsYearsMonths != null)
                    {
                        if (objectBETreeYears.HospitalUnitsYearsMonths.Count > 0)
                        {
                            objectSeringButilder.Append("<ul>");
                            foreach (RMC.BusinessEntities.BETreeMonths objectBETreeMonths in objectBETreeYears.HospitalUnitsYearsMonths)
                            {
                                objectSeringButilder.Append("<li>");
                                if (HttpContext.Current.User.IsInRole("superadmin"))
                                {
                                    objectSeringButilder.Append("<span><a href='#'><u>" + BSCommon.GetMonthName(objectBETreeMonths.Month) + "</u></a></span>" + "<span style='padding-left:5px; color:red;'>(<a style='color:red;' onclick='deleteAllFiles(" + objectBETreeYears.Year + "," + objectBETreeMonths.Month + "," + hospitalUnitID.ToString() + ")'>Delete All</a>)</span>");
                                }
                                else
                                {
                                    if (permissionID > 1)
                                    {
                                        objectSeringButilder.Append("<span><a href='#'><u>" + BSCommon.GetMonthName(objectBETreeMonths.Month) + "</u></a></span>");
                                    }
                                    else
                                    {
                                        objectSeringButilder.Append("<span><a href='#'><u>" + BSCommon.GetMonthName(objectBETreeMonths.Month) + "</u></a></span>" + "<span style='padding-left:5px; color:red;'>(<a style='color:red;' onclick='deleteAllFiles(" + objectBETreeYears.Year + "," + objectBETreeMonths.Month + "," + hospitalUnitID.ToString() + ")'>Delete All</a>)</span>");
                                    }
                                }
                                //if (objectBETreeMonths.IsError)
                                //{
                                //    objectSeringButilder.Append("<span style='color:Red;'>(<a href='javascript:void(0);' onclick='return OpenErrorWindow();'><u style='color:Red;'>Error</u></a>)</span>");
                                //}
                                if (objectBETreeMonths.NursePDAInfoList != null)
                                {
                                    if (objectBETreeMonths.NursePDAInfoList.Count > 0)
                                    {
                                        objectSeringButilder.Append(GetNursePDAInfo(objectBETreeMonths.NursePDAInfoList));
                                    }
                                }
                                objectSeringButilder.Append("</li>");
                            }
                            objectSeringButilder.Append("</ul>");
                        }
                    }
                    objectSeringButilder.Append("</li>");
                }
            }
            objectSeringButilder.Append("</ul>");
            return objectSeringButilder;

        }

        private StringBuilder GetNursePDAInfo(List<RMC.BusinessEntities.BENursePDAInfo> objectGenericBENursePDAInfo)
        {
            StringBuilder objectStringBuilder = new StringBuilder();
            objectStringBuilder.Append("<ul>");
            foreach (RMC.BusinessEntities.BENursePDAInfo objectNursePDAInfo in objectGenericBENursePDAInfo)
            {
                if (objectNursePDAInfo != null)
                {
                    objectStringBuilder.Append("<li>");
                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        objectStringBuilder.Append("<span><a href='../Administrator/ShowValidData.aspx?NurseID=" + objectNursePDAInfo.NurseID.ToString() + "'><u>" + objectNursePDAInfo.FileReference + "</u></a>  <a onclick=deleteFile('" + objectNursePDAInfo.NurseID + "') style=\"color:red; cursor:pointer; \">( Delete )</a></span>");
                    }
                    else
                    {
                        objectStringBuilder.Append("<span><a href='../Users/ShowValidData.aspx?NurseID=" + objectNursePDAInfo.NurseID.ToString() + "'><u>" + objectNursePDAInfo.FileReference + "</u></a>  <a onclick=deleteFile('" + objectNursePDAInfo.NurseID + "') style=\"color:red; cursor:pointer; \">( Delete )</a></span>");
                    }

                    objectStringBuilder.Append("</li>");
                }
            }

            objectStringBuilder.Append("</ul>");
            return objectStringBuilder;
        }

        #endregion
        
    }
}