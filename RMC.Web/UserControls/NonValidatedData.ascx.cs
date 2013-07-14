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
    public partial class NonValidatedData : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool flag =false;
            BSTreeView objectTreeView = new BSTreeView();
            List<RMC.BusinessEntities.BETreeHospitalInfo> objectTreeStructure = null;


            if (HttpContext.Current.User.IsInRole("superadmin"))
            {
                objectTreeStructure = objectTreeView.GetAllActiveHospitalInfo(true);
            }
            else
            {
                //if (!CommonClass.UserInformation.IsActive)
                //{
                //    Response.Redirect("~/Users/InActiveUser.aspx");
                //}

                int userID = CommonClass.UserInformation.UserID;
                objectTreeStructure = objectTreeView.GetAllActiveHospitalInfoByUserID(userID, true);
            }

            StringBuilder objectSeringButilder = new StringBuilder();
            objectSeringButilder.Append("<ul id='ulHospitalTree' class='treeview-gray' style='color:#06569D' ><b>List of hospitals</b>");
            foreach (RMC.BusinessEntities.BETreeHospitalInfo objectHospitalInfo in objectTreeStructure)
            {
                objectSeringButilder.Append("<li>");
                objectSeringButilder.Append("<span'><a href='#'>" + objectHospitalInfo.HospitalName + "</a></span>");
                if (objectHospitalInfo.HospitalUnitsList != null)
                {
                    if (objectHospitalInfo.HospitalUnitsList.Count > 0)
                    {
                        objectSeringButilder.Append("<ul>");
                        foreach (RMC.BusinessEntities.BETreeHospitalUnits objectHospitalUnits in objectHospitalInfo.HospitalUnitsList)
                        {
                            objectSeringButilder.Append("<li>");
                            objectSeringButilder.Append("<span><a href='#'><u>" + objectHospitalUnits.HospitalDemographicID + "#" + " " + "-" + objectHospitalUnits.HospitalUnitName + "," + objectHospitalUnits.CreatedDate.ToShortDateString() + (objectHospitalUnits.ModifiedDate.HasValue == true ? "," + objectHospitalUnits.ModifiedDate.Value.ToShortDateString() : "") + "</u></a>");
                            //if (HttpContext.Current.User.IsInRole("superadmin"))
                            //{
                            //    objectSeringButilder.Append("   ( <a href='../Administrator/FileUploader.aspx?HospitalDemographicId=" + objectHospitalUnits.HospitalDemographicID + "&PermissionID=" + objectHospitalUnits.PermissionID.ToString() + "'>Add Data</a> )</span>");
                            //}
                            //else
                            //{
                            //    objectSeringButilder.Append("   ( <a href='../Users/FileUploader.aspx?HospitalDemographicId=" + objectHospitalUnits.HospitalDemographicID + "&PermissionID=" + objectHospitalUnits.PermissionID.ToString() + "'>Add Data</a> )</span>");
                            //}
                            if (objectHospitalUnits.HospitalUnitsYears != null)
                            {
                                if (objectHospitalUnits.HospitalUnitsYears.Count > 0)
                                {
                                    objectSeringButilder.Append(GetYearsMonth(objectHospitalUnits.HospitalUnitsYears).ToString());
                                    flag = true;
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
            if (!flag)
            {
                objectSeringButilder.Remove(0, objectSeringButilder.Length);
            }
            divTreeView.InnerHtml = objectSeringButilder.ToString();

        }


        private StringBuilder GetYearsMonth(List<RMC.BusinessEntities.BETreeYears> objectHospitalUnitsYears)
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
                                objectSeringButilder.Append("<span><a href='#'><u>" + BSCommon.GetMonthName(objectBETreeMonths.Month) + "</u></a></span>");
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
                        objectStringBuilder.Append("<span><a href='../Administrator/ShowValidData.aspx?NurseID=" + objectNursePDAInfo.NurseID.ToString() + "'><u>" + objectNursePDAInfo.FileReference + "</u></a></span>");
                    }
                    else
                    {
                        objectStringBuilder.Append("<span><a href='../Users/ShowValidData.aspx?NurseID=" + objectNursePDAInfo.NurseID.ToString() + "'><u>" + objectNursePDAInfo.FileReference + "</u></a></span>");
                    }

                    objectStringBuilder.Append("</li>");
                }
            }

            objectStringBuilder.Append("</ul>");
            return objectStringBuilder;
        }

    }
}