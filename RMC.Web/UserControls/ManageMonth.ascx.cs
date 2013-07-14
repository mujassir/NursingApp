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
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class ManageMonth : System.Web.UI.UserControl
    {

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    BindMonthDropDownList();
                    Page.Header.Title = "RMC :: Manage Month";
                    
                    if (Request.UrlReferrer != null)
                    {
                        CommonClass objectCommonClass = new CommonClass();
                        objectCommonClass.BackButtonUrl = Request.UrlReferrer.AbsoluteUri;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ManageYears.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    RMC.BussinessService.BSMonth objectBSMonth = new RMC.BussinessService.BSMonth();
                    RMC.DataService.Month objectMonth = new RMC.DataService.Month();

                    objectMonth.MonthName = DropDownListMonth.SelectedValue;

                    if (objectBSMonth.InsertMonth(objectMonth, Convert.ToInt32(Request.QueryString["HospitalDemographicId"]), Convert.ToString(Request.QueryString["Year"])))
                    {

                        //add by cm on 10nov2011
                        string year = objectBSMonth.getYear(objectMonth.YearID);
                        string unitname = objectBSMonth.getUnitname(objectMonth.YearID);
                        string hispitalname = objectBSMonth.getHospitalname(objectMonth.YearID);

                        string strHospitalDir = Server.MapPath(Request.ApplicationPath + "/Uploads/" + hispitalname);
                        string strUnitDir = Server.MapPath(Request.ApplicationPath + "/Uploads/" + hispitalname + "/" + unitname);
                        string strYearDir = Server.MapPath(Request.ApplicationPath + "/Uploads/" + hispitalname + "/" + unitname + "/" + year);
                        string strDirectory = Server.MapPath(Request.ApplicationPath + "/Uploads/" + hispitalname + "/" + unitname + "/" + year+"/"+objectMonth.MonthName);
                        System.IO.DirectoryInfo ObjSearchDir = new System.IO.DirectoryInfo(strDirectory);
                        System.IO.DirectoryInfo ObjSearchHospitalDir = new System.IO.DirectoryInfo(strHospitalDir);
                        System.IO.DirectoryInfo ObjSearchUnitDir = new System.IO.DirectoryInfo(strUnitDir);
                        System.IO.DirectoryInfo ObjSearchYearDir = new System.IO.DirectoryInfo(strYearDir);
                        if (!ObjSearchHospitalDir.Exists)
                        {
                            ObjSearchHospitalDir.Create();
                        }
                        if (!ObjSearchUnitDir.Exists)
                        {
                            ObjSearchUnitDir.Create();
                        }
                        if (!ObjSearchYearDir.Exists)
                        {
                            ObjSearchYearDir.Create();
                        }
                        if (!ObjSearchDir.Exists)
                        {
                            ObjSearchDir.Create();
                        }
                        //end
                        //CommonClass objectCommonClass = new CommonClass();
                        //string backUrl = objectCommonClass.BackButtonUrl;
                        //Response.Redirect(backUrl, false);

                        //to redirect the page directly to the add data page (acc. to client requirement)
                        if (HttpContext.Current.User.IsInRole("superadmin"))
                        {
                            Response.Redirect("~/Administrator/DataManagementFileList.aspx?HospitalInfoId=" + Convert.ToString(Request.QueryString["HospitalInfoId"]) + "&HospitalDemographicId=" + Convert.ToInt32(Request.QueryString["HospitalDemographicId"]) + "&UnitCounter=" + Convert.ToString(Request.QueryString["UnitCounter"]) + "&PermissionID=" + Convert.ToString(Request.QueryString["PermissionID"]) + "&Year=" + Convert.ToString(Request.QueryString["Year"] + "&Month=" + objectMonth.MonthName) + "&IsBackUrlAdd=NO", false);
                        }
                        else
                        {
                            Response.Redirect("~/Users/DataManagementFileList.aspx?HospitalInfoId=" + Convert.ToString(Request.QueryString["HospitalInfoId"]) + "&HospitalDemographicId=" + Convert.ToInt32(Request.QueryString["HospitalDemographicId"]) + "&UnitCounter=" + Convert.ToString(Request.QueryString["UnitCounter"]) + "&PermissionID=" + Convert.ToString(Request.QueryString["PermissionID"]) + "&Year=" + Convert.ToString(Request.QueryString["Year"] + "&Month=" + objectMonth.MonthName) + "&IsBackUrlAdd=NO", false);
                        }
                    }
                    else
                    {
                        CommonClass.Show("Failed to insert record.");
                    }

                    BindMonthDropDownList();
                }
                else
                {
                    CommonClass.Show("Please fill all required fields.");
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ManageYears.ascx ---- ButtonSave_Click";
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
                LogManager._stringObject = "ManageMonth.ascx ---- ImageButtonBack_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        #region Private Methods

        private void BindMonthDropDownList()
        {
            List<RMC.BusinessEntities.BEMonth> objectGenericBEMonth = RMC.BussinessService.BSCommon.GetAllMonths();
            List<string> objectGenericMonthValue = new List<string>();
            ListBoxMonths.DataBind();
            for (int index = 0; index < ListBoxMonths.Items.Count; index++)
            {
                objectGenericMonthValue.Add(ListBoxMonths.Items[index].Value);
            }
            //Bind Month Dropdownlist.
            DropDownListMonth.Items.Clear();
            DropDownListMonth.DataTextField = "MonthName";
            DropDownListMonth.DataValueField = "MonthID";
            if (objectGenericMonthValue.Count > 0)
            {
                List<RMC.BusinessEntities.BEMonth> objectNewGenericBEMonth = (from m in objectGenericBEMonth
                                                                              where objectGenericMonthValue.Contains(m.MonthID) == false
                                                                              select m).ToList();

                DropDownListMonth.DataSource = objectNewGenericBEMonth;
            }
            else
            {
                DropDownListMonth.DataSource = objectGenericBEMonth;
            }

            DropDownListMonth.DataBind();
            if (DropDownListMonth.Items.Count == 0)
            {
                ButtonSave.Enabled = false;
            }
        }

        #endregion

    }
}