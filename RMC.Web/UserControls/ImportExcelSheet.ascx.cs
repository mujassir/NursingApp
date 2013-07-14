using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class ImportExcelSheet : System.Web.UI.UserControl
    {

        #region Events

        protected void ButtonImportExcelSheet_Click(object sender, EventArgs e)
        {
            //string path = AppDomain.CurrentDomain.BaseDirectory + "ExcelSheet\\";
            string path = Server.MapPath(@"\Excelsheet\");
            string filePath = path + FileUploadExcelSheet.FileName;

            try
            {
                if (FileUploadExcelSheet.HasFile)
                {
                    RMC.BussinessService.BSValidationData objectBSValidationData = new RMC.BussinessService.BSValidationData();
                    string query = "SELECT * FROM [" + TextBoxSheetName.Text.Trim() + "$]";                   
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    FileUploadExcelSheet.SaveAs(filePath);
                    String strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                                     "Data Source=" + filePath +
                                     "; Extended Properties=Excel 8.0;";

                    DataSet ds = new DataSet();
                    //You must use the $ after the object
                    //you reference in the spreadsheet
                    OleDbDataAdapter da = new OleDbDataAdapter
                    (query, strConn);
                    da.Fill(ds);
                    for (int index = ds.Tables[0].Columns.Count - 1; index >= 0; index--)
                    {
                        if (ds.Tables[0].Columns[index].ColumnName.ToLower().Trim() != "Location".ToLower().Trim() &&
                            ds.Tables[0].Columns[index].ColumnName.ToLower().Trim() != "Activity".ToLower().Trim() &&
                            ds.Tables[0].Columns[index].ColumnName.ToLower().Trim() != "SubActivity".ToLower().Trim() &&
                            ds.Tables[0].Columns[index].ColumnName.ToLower().Trim() != "Sub-Activity".ToLower().Trim())
                        {
                            ds.Tables[0].Columns.RemoveAt(index);
                        }
                    }
                    if (ds.Tables[0].Columns.Count > 0)
                    {
                        if (objectBSValidationData.InsertValidationDataFromTable(ds.Tables[0]))
                        {
                            Response.Redirect("~/Administrator/ValidationTable.aspx", false);
                        }
                        else
                        {
                            CommonClass.Show("Failed to Insert Records.");
                        }
                    }
                    else
                    {
                        CommonClass.Show("Invalid Format of Excel File.");
                    }
                    TextBoxSheetName.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                // Response.Write(ex.Message.ToString() + "---------- " + ex.StackTrace.ToString());
                ex.Data.Add("Events", "ButtonImportExcelSheet_Click");
                ex.Data.Add("Page", "ImportExcelSheet.ascx");
                LogManager._stringObject = "ImportExcelSheet.ascx ---- ButtonImportExcelSheet_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
            finally
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
        }

        #endregion

    }
}