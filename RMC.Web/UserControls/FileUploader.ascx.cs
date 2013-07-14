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
using System.Text.RegularExpressions;
using System.IO;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class FileUploader : System.Web.UI.UserControl
    {

        #region Events
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                if (!Page.IsPostBack)
                {
                    ImageButtonBack.PostBackUrl = Request.UrlReferrer.AbsoluteUri;
                    CommonClass.SessionInfomation.Year = Convert.ToString(DropDownListYear.SelectedValue);
                    CommonClass.SessionInfomation.Month = Convert.ToString(DropDownListMonth.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "FileUploader.aspx");
                LogManager._stringObject = "FileUploader.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            bool flag = false;
            try
            {
                if (Page.IsValid)
                {
                    if (FileUploadSDA.HasFile)
                    {
                        string strDirectory = Server.MapPath(Request.ApplicationPath + "/Uploads/");
                        string filepath = FileUploadSDA.PostedFile.FileName;
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
                            FileUploadSDA.SaveAs(Path.Combine(strDirectory, guidFileName));
                            flag = SaveHospitalUpload(FileUploadSDA.FileName, guidFileName, filepath);
                            if (flag)
                            {
                                CommonClass.Show("File Uploaded Successfully.");
                            }
                            else
                            {
                                CommonClass.Show("Failed to Upload File.");
                            }
                        }
                        else
                        {
                            //DisplayMessage("Only .sda files allowed!", System.Drawing.Color.Red);
                        }
                        //save the file to the server
                        //fileUpEx.PostedFile.SaveAs(Server.MapPath(".\\") + file);
                        //lblStatus.Text = "File Saved to: " + Server.MapPath(".\\") + file;
                    }
                    else
                    {
                        CommonClass.Show("You have not Specified a File.");
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSubmit_Click");
                ex.Data.Add("Page", "FileUploader.aspx");
                LogManager._stringObject = "FileUploader.aspx ---- ButtonSubmit_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }


        protected void DropDownListYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CommonClass.SessionInfomation.Year = Convert.ToString(DropDownListYear.SelectedValue);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "DropDownListYear_SelectedIndexChanged");
                ex.Data.Add("Page", "FileUploader.aspx");
                LogManager._stringObject = "FileUploader.aspx ---- DropDownListYear_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CommonClass.SessionInfomation.Month = Convert.ToString(DropDownListMonth.SelectedValue);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "DropDownListMonth_SelectedIndexChanged");
                ex.Data.Add("Page", "FileUploader.aspx");
                LogManager._stringObject = "FileUploader.aspx ---- DropDownListMonth_SelectedIndexChanged";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        #region Private Methods
        
        /// <summary>
        /// Use to Display message of Login Failure.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 10, 2009.
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
                ex.Data.Add("Class", "UserRegistration");
                throw ex;
            }
        }

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
            System.Text.StringBuilder objectMessage = new System.Text.StringBuilder();
            try
            {
                RMC.DataService.HospitalUpload objectHospitalUpload = new RMC.DataService.HospitalUpload();
                RMC.BussinessService.BSSDAValidation objectBSSDAValidation = new RMC.BussinessService.BSSDAValidation();
                string userName = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;

                objectHospitalUpload.CreatedBy = userName;
                objectHospitalUpload.CreatedDate = DateTime.Now;
                objectHospitalUpload.Year = Convert.ToString(DropDownListYear.SelectedValue);
                objectHospitalUpload.Month = Convert.ToString(DropDownListMonth.SelectedValue);
                objectHospitalUpload.HospitalDemographicID = Convert.ToInt32(Request.QueryString["HospitalDemographicId"]);
                objectHospitalUpload.OriginalFileName = fileName;
                objectHospitalUpload.UploadedFileName = uniqueFileName;
                objectHospitalUpload.FilePath = filePath;
                objectHospitalUpload.IsDataSaved = true;
                objectHospitalUpload.IsDeleted = false;

                flag = objectBSSDAValidation.SaveFileData(filePath, fileName, Convert.ToInt32(Request.QueryString["HospitalDemographicId"]), objectHospitalUpload, out errorFlag);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "LogFiles");
                ex.Data.Add("Class", "Upload");
                throw ex;
            }

            return flag;
        }

        #endregion
      
    }
}