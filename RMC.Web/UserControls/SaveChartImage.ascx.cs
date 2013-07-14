using System;
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
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class SaveChartImage : System.Web.UI.UserControl
    {

        #region Events
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string fileName = AppDomain.CurrentDomain.BaseDirectory + "Uploads\\ChartImg" + CommonClass.UserInformation.UserID.ToString() + ".png";
                imageFile(fileName);
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

        private void imageFile(string path)
        {
            try
            {               
                string fileName = System.IO.Path.GetFileName(path);
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "image/png";
                Response.Charset = "";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.TransmitFile(path);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ExportGridView");
                ex.Data.Add("Class", "ExportExcelFile");
                throw ex;
            }
        }

        #endregion

    }
}