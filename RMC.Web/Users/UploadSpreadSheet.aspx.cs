using System;
using System.IO;
using RMC.BussinessService;
using LogExceptions;
namespace RMC.Web.Users
{
    public partial class UploadSpreadSheet : System.Web.UI.Page
    {

        #region Variable
        //Bussiness Service Objects.
        BSUpload ObjUpload;
        #endregion

        #region Events

        /// <summary>
        /// This event is used to upload the CSV files to the Upload Folder
        /// Created By : Amit Chawla.
        /// Creation Date : June 24, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            bool Flag;
            try
            {
                ObjUpload = new BSUpload();
                if (FileUploadSpreadSheet.HasFile)         // If Upload control contains the file
                {
                    Flag = ObjUpload.CheckExtension(FileUploadSpreadSheet.FileName);             // To check the extension of the file which is to be uploaded
                    if (Flag == true)
                    {
                        string FullfileName = ObjUpload.FindFullPath(FileUploadSpreadSheet.FileName);   // To find the full file path where the file to be stored
                        FileUploadSpreadSheet.SaveAs(FullfileName);                                     // To Save the particular file in a given folder
                        DisplayMessage("File Uploaded Successfully", System.Drawing.Color.Green);
                    }
                    else
                    {
                        DisplayMessage("Please select .sda file to upload only", System.Drawing.Color.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonUpload");
                ex.Data.Add("Page", "UploadSpreadSheet.aspx");
                LogManager._stringObject = "UploadSpreadSheet.aspx ---- ButtonUpload_Click ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
            finally
            {
                ObjUpload = null;
            }

        }
        //End Of ButtonUpload_Click Events.

        /// <summary>
        /// Page Events.
        /// Created By : Amit Chawla.
        /// Created Date : June 24, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ss"] = "ss";
        }
        //End Of Page_Load Events.

        #endregion

        #region Private Methods

        //Use to Display message of Login Failure.
        private void DisplayMessage(string msg, System.Drawing.Color color)
        {
            LabelErrorMsg.Text = msg;
            LabelErrorMsg.ForeColor = color;
            LabelErrorMsg.Visible = true;
        }
        //End Of DisplayMessage Methods.

        #endregion

    }
}
