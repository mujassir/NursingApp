using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogExceptions;

namespace RMC.Web.Administrator
{
    public partial class ExportExcelFile : System.Web.UI.Page
    {

        #region Events
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string fileName = AppDomain.CurrentDomain.BaseDirectory + "ExcelSheet\\ValidationTableExport" + CommonClass.UserInformation.UserID.ToString() + ".xls";
                //string fileName = "ValidationTableExport" + CommonClass.UserInformation.UserID.ToString() + ".xls";
                ExportXMLExcelFile(fileName);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "ExportExcelFile.aspx");
                LogManager._stringObject = "ExportExcelFile.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }
 
        #endregion

        #region Private Methods
        
        private void ExportXMLExcelFile(string path)
        {
            try
            {
                RMC.BussinessService.BSImportXMLExcelFile objectBSImportXMLExcelFile = new RMC.BussinessService.BSImportXMLExcelFile();
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                objectBSImportXMLExcelFile.GenerateXMLExcelFile(path);
                string fileName = System.IO.Path.GetFileName(path);
                Response.Write("GenerateXMLExcelFile executed");
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.TransmitFile(path);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ExportXMLExcelFile");
                ex.Data.Add("Class", "ExportExcelFile");
                LogManager._stringObject = "ExportExcelFile.aspx ---- ExportXMLExcelFile";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
                //throw ex;
            }
        }

        #endregion

    }
}
