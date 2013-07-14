using System;
using System.Collections;
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
using LogExceptions;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using RMC.BussinessService;

namespace RMC.Web.Users
{
    public partial class FileUploader : System.Web.UI.Page
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
            bool flag = false;
            try
            {
                //if (!Page.IsPostBack)
                //{
                //    ImageButtonBack.PostBackUrl = Request.UrlReferrer.AbsoluteUri;
                //}                
                if (Request.QueryString["HospitalDemographicId"] != null)
                {
                    Session["UnitID"] = Convert.ToInt32(Request.QueryString["HospitalDemographicId"]);

                    SetControlState();
                    if (Request.UrlReferrer != null && !Page.IsPostBack)
                    {
                        CommonClass objectCommonClass = new CommonClass();
                        objectCommonClass.BackButtonUrl = Request.UrlReferrer.AbsoluteUri;
                        LinkHierarchy();
                    }
                }
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    RMC.BussinessService.BSSDAValidation objectBSSDAValidation = new RMC.BussinessService.BSSDAValidation();
                    Session["FileUploader"] = "FileUpload";
                    HttpPostedFile uploadeFile = Request.Files[0];

                    string strDirectory = Server.MapPath(Request.ApplicationPath + "/Uploads/");
                    string filepath = Path.GetFullPath(uploadeFile.FileName);

                    string pat = @"\\(?:.+)\\(.+)\.(.+)";
                    Regex r = new Regex(pat);
                    Match m = r.Match(filepath);
                    string file_ext = m.Groups[2].Captures[0].ToString();
                    string filename = m.Groups[1].Captures[0].ToString();
                    string file = filename + "." + file_ext;

                    if (file_ext.ToLower().Trim() == "sda")
                    {
                        System.Guid guid = Guid.NewGuid();
                        string guidFileName = Convert.ToString(guid) + ".sda";
                        //FileUploadSDA.SaveAs(Path.Combine(strDirectory, guidFileName));
                        uploadeFile.SaveAs(Path.Combine(strDirectory, guidFileName));
                        filepath = Path.Combine(strDirectory, guidFileName);
                        if (Session["ConfigName"] != null)
                        {
                            flag = SaveHospitalUpload(uploadeFile.FileName, guidFileName, filepath, Convert.ToString(Session["ConfigName"]));
                        }
                        else if (!objectBSSDAValidation.ValidateStandardFileFormat(filepath))
                        {
                            //flag = SaveHospitalUpload(FileUploadSDA.FileName, guidFileName, filepath);
                            flag = SaveHospitalUpload(uploadeFile.FileName, guidFileName, filepath);
                        }
                        else
                        {
                            if (System.IO.File.Exists(filepath))
                                System.IO.File.Delete(filepath);
                            Response.Write("False^" + uploadeFile.FileName + "^");
                        }
                    }
                    else
                    {
                        CommonClass.Show("Only .sda files allowed!");
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "FileUploader.aspx");
                LogManager._stringObject = "FileUploader.aspx ---- Page_Load";
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
                Session.Remove("FileUploader");
                Response.Redirect(backUrl, false);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ImageButtonBack_Click");
                ex.Data.Add("Page", "FileUploader.aspx");
                LogManager._stringObject = "FileUploader.aspx ---- ImageButtonBack_Click";
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

                Response.Redirect(objectCommonClass.RemoveBackButtonUrl(4), false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "FileUploader.ascx ---- LinkButtonHospitalIndex_Click";
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
                objectCommonClass.RemoveBackButtonUrl(3);
                Response.Redirect(objectCommonClass.BackButtonUrl, false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "FileUploader.ascx ---- LinkButtonHospitalInformation_Click";
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
                objectCommonClass.RemoveBackButtonUrl(2);
                Response.Redirect(objectCommonClass.BackButtonUrl, false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "FileUploader.ascx ---- LinkButtonHospitalUnitInformation_Click";
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
                objectCommonClass.RemoveBackButtonUrl(1);
                Response.Redirect(objectCommonClass.BackButtonUrl, false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "FileUploader.ascx ---- LinkButtonHospitalUnitInformation_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonMonth_Click(object sender, EventArgs e)
        {
            try
            {
                CommonClass objectCommonClass = new CommonClass();
                objectCommonClass.RemoveBackButtonUrl(0);
                Response.Redirect(objectCommonClass.BackButtonUrl, false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "FileUploader.ascx ---- LinkButtonHospitalUnitInformation_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Use to Save File Information.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="uniqueFileName"></param>
        /// <param name="filePath"></param>
        /// <param name="context"></param>
        private bool SaveHospitalUpload(string fileName, string uniqueFileName, string filePath)
        {
            bool errorFlag = false, flag = false;
            try
            {
                RMC.DataService.HospitalUpload objectHospitalUpload = new RMC.DataService.HospitalUpload();
                RMC.BussinessService.BSSDAValidation objectBSSDAValidation = new RMC.BussinessService.BSSDAValidation();
                string userName = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;

                objectHospitalUpload.CreatedBy = userName;
                objectHospitalUpload.CreatedDate = DateTime.Now;
                if (Session["Year"] != null)
                {
                    objectHospitalUpload.Year = Convert.ToString(Session["Year"]);
                }
                else
                {
                    objectHospitalUpload.Year = "2002";
                }

                if (Session["Month"] != null)
                {
                    objectHospitalUpload.Month = Convert.ToString(Session["Month"]);
                }
                else
                {
                    objectHospitalUpload.Month = Request.QueryString["Month"];
                }
                objectHospitalUpload.HospitalDemographicID = Convert.ToInt32(Session["UnitID"]);
                objectHospitalUpload.OriginalFileName = fileName;
                objectHospitalUpload.UploadedFileName = uniqueFileName;
                objectHospitalUpload.FilePath = filePath;
                objectHospitalUpload.IsDataSaved = true;
                objectHospitalUpload.IsDeleted = false;

                flag = objectBSSDAValidation.SaveFileData(filePath, fileName, Convert.ToInt32(Session["UnitID"]), objectHospitalUpload, out errorFlag);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "LogFiles");
                ex.Data.Add("Class", "Upload");
                throw ex;
            }

            return flag;
        }

        private bool SaveHospitalUpload(string fileName, string uniqueFileName, string filePath, string configName)
        {
            bool errorFlag = false, flag = false;
            try
            {
                RMC.DataService.HospitalUpload objectHospitalUpload = new RMC.DataService.HospitalUpload();
                RMC.BussinessService.BSSDAValidation objectBSSDAValidation = new RMC.BussinessService.BSSDAValidation();
                string userName = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;

                objectHospitalUpload.CreatedBy = userName;
                objectHospitalUpload.CreatedDate = DateTime.Now;
                if (Session["Year"] != null)
                {
                    objectHospitalUpload.Year = Convert.ToString(Session["Year"]);
                }
                else
                {
                    objectHospitalUpload.Year = "2002";
                }

                if (Session["Month"] != null)
                {
                    objectHospitalUpload.Month = Convert.ToString(Session["Month"]);
                }
                else
                {
                    objectHospitalUpload.Month = Request.QueryString["Month"];
                }
                objectHospitalUpload.HospitalDemographicID = Convert.ToInt32(Session["UnitID"]);
                objectHospitalUpload.OriginalFileName = fileName;
                objectHospitalUpload.UploadedFileName = uniqueFileName;
                objectHospitalUpload.FilePath = filePath;
                objectHospitalUpload.IsDataSaved = true;
                objectHospitalUpload.IsDeleted = false;

                flag = objectBSSDAValidation.SaveFileData(filePath, fileName, Convert.ToInt32(Session["UnitID"]), objectHospitalUpload, configName, out errorFlag);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "LogFiles");
                ex.Data.Add("Class", "Upload");
                throw ex;
            }

            return flag;
        }

        private void SetControlState()
        {
            try
            {
                //RMC.BussinessService.BSMaintainControlState objectBSMaintainControlState = new RMC.BussinessService.BSMaintainControlState();
                //RMC.DataService.MaintainControlState objectMaintainControlState = null;

                //objectMaintainControlState = objectBSMaintainControlState.GetMaintainControlState(CommonClass.UserInformation.UserID);
                //DropDownListYear.DataBind();
                //if (objectMaintainControlState != null)
                //{       
                //if (objectMaintainControlState.Year != null)
                //{
                //    //DropDownListYear.Items.FindByValue(objectMaintainControlState.Year).Selected = true;
                //    Session["Year"] = objectMaintainControlState.Year;
                //}

                //if (objectMaintainControlState.Month != null)
                //{
                //    DropDownListMonth.Items.FindByValue(objectMaintainControlState.Month).Selected = true;
                //    Session["Month"] = objectMaintainControlState.Month;
                //}

                if (Session["Year"] == null)
                {
                    Session["Year"] = Request.QueryString["Year"];
                }
                if (Session["Month"] == null)
                {
                    Session["Month"] = Request.QueryString["Month"];
                }
                Session["HospitalInfoId"] = Request.QueryString["HospitalInfoId"];
                Session["UnitCounter"] = Request.QueryString["UnitCounter"];

                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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
                strBuilderEdit.Append("<span style='color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Users/HospitalDetail.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + Convert.ToString(HospitalInformation.HospitalID)) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + " - " + HospitalInformation.HospitalName + ", " + HospitalInformation.City + ", " + HospitalInformation.State + "'>( Edit )</a></span>");
                strBuilderEditUnit.Append("<span style='padding-left:2px; color:#06569d;'><a style='cursor:pointer; font-weight:bold; font-size:10px; color:#06569d;' href='../Users/HospitalDemographicDetail.aspx?" + objectQueryStringEnc.EncrptQuerystringParam("HospitalInfoId=" + HospitalInformation.HospitalID + "&HospitalDemographicId=" + HospitalUnitInformation.HospitalDemographicID + "&PermissionID=" + Request.QueryString["PermissionID"]) + "' title='" + "#" + HospitalInformation.HospitalRecordCount + "-" + HospitalUnitCount + " - " + HospitalUnitInformation.HospitalUnitName + "'>( Edit )</a></span>");
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
