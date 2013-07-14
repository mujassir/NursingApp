using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement;


namespace LogExceptions
{

    /// <summary>
    /// Summary description for LogManager
    /// </summary>
    #region Delegate for Event that invoke function in Global.asax
    public delegate void LogManagerDeleGateSetExceptionDetails(Exception ex, string FunctionName, DataSet ds);
    #endregion

    public class LogManager
    {

        public static event LogManagerDeleGateSetExceptionDetails LogManagerEventSetExceptionDetails;
        public static string _stringObject; // it is defined to hold error description and will be used in  catch of  every .aspx or .ascx 

        public enum LoggingLevel
        {
            Error = 10,
            Warning = 8,
            Informational = 6,
            Debug = 4,
            Extended = 2,
            All = 0
        }

        public enum LoggingCategory
        {
            General = 0
        }

        private LoggingLevel _AuditLogLevel;

        private LoggingLevel _AuditQueueLevel;

        public LoggingLevel AuditLogLevel
        {
            get
            {
                return _AuditLogLevel;
            }
            set
            {
                _AuditLogLevel = value;
            }
        }

        public LoggingLevel AuditQueueLevel
        {
            get
            {
                return _AuditQueueLevel;
            }
            set
            {
                _AuditQueueLevel = value;
            }
        }

        //public bool LogMessage(string message,
        public void LogMessage(string message, LoggingCategory category, LoggingLevel level)
        {
            // TODO: Implement Method - LogManager.LogMessage()
            DataSet objds = new DataSet();
            if (_AuditLogLevel == level || _AuditLogLevel < level)
            {

            }
        }

        public static bool LogException(System.Exception ex, LoggingCategory category, LoggingLevel level)
        {
            try
            {
                DefaultPublisher.CallErrorWriteToDatabase -= new DataAvailableEventHandler(DefaultPublisher_CallErrorWriteToDatabase);
                DefaultPublisher.CallErrorWriteToDatabase += new DataAvailableEventHandler(DefaultPublisher_CallErrorWriteToDatabase);
                Microsoft.ApplicationBlocks.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            catch
            {
                string str = ex.Message.ToString();
            }
            return true;
        }

        public static bool LogException(System.Exception ex, LoggingCategory category, LoggingLevel level, string VerboseMode)
        {
            //ex.Source = VerboseMode;
            ex.Data["CustomExceptionInformation"] = VerboseMode;
            Microsoft.ApplicationBlocks.ExceptionManagement.ExceptionManager.Publish(ex);
            return true;
        }

        public bool SaveQueue(string fileName)
        {
            // TODO: Implement Method - LogManager.SaveQueue()
            string path = "";

            // to be replaced with data table of queue
            System.Data.DataTable dt = null;

            path = System.Configuration.ConfigurationSettings.AppSettings["XmlLogPath"];

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            if (!path.EndsWith("\\"))
            {
                path = path + "\\";
            }

            System.Data.DataSet ds = new System.Data.DataSet();

            ds.Tables.Add(dt);

            ds.WriteXml(path + fileName, System.Data.XmlWriteMode.IgnoreSchema);

            ds.Dispose();

            return true;
        }

        /// <summary>
        /// To assign values to Exception Object to log into DB
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="ErrorSource"></param>
        /// <param name="ds"></param>
        /// <CreatedOn></CreatedOn>
        /// <Author></Author>
        public static void SetExceptionDetails(Exception _ex, string _FunctionName, DataSet _ds)
        {
            if (_ex.Data["CustomExceptionInformation"] == null)
                _ex.Data["CustomExceptionInformation"] = "### Source " + _FunctionName + " ###";
        }

        /// <summary>
        /// This Function will return error details  that will be displayed to user
        /// and this will be called from catch block wherever we have used LogException (into DB)
        /// </summary>
        /// <param name="_ex"></param>
        /// <returns></returns>
        /// <CreatedOn></CreatedOn>
        /// <Author></Author>
        public static string ShowErrorDetail(Exception _ex)
        {
            string ParseMessage = _ex.Message;

            if (_ex.Message.IndexOf("Concurrency") != -1)
            {
                try
                {
                    ParseMessage = HttpContext.GetGlobalResourceObject("GlobalResource", "ConcurrencyErrorMessage").ToString();
                }
                catch (Exception ex)
                {
                    ParseMessage = "The information you are updating has been changed by another user. Please refresh the information you are updating.";
                }
            }


            else if (ParseMessage.IndexOf("System.Data.SqlClient.SqlException:") > 0)
            {
                int SubstringLen = ParseMessage.IndexOf("\n") - ParseMessage.IndexOf("System.Data.SqlClient.SqlException:");
                ParseMessage = ParseMessage.Substring(ParseMessage.IndexOf("System.Data.SqlClient.SqlException:") + 35, SubstringLen - 35);
            }
            else
            {
                try
                {
                    ParseMessage = HttpContext.GetGlobalResourceObject("GlobalResource", "GeneralErrorMessage").ToString();
                }
                catch (Exception ex)
                {
                    ParseMessage = "Error Occurred - Please Contact Your System Administrator!";
                }
            }
            return ParseMessage;
        }

        #region Write log into Database...

        /// <summary>
        /// This is used to insert the Error Messages in the Database and into the queue
        /// </summary>;
        /// <param name="entry"></param>
        /// <param name="type"></param>
        /// <author>Pralyankar Kumar SIngh</author>
        /// <cretaedOn>Jan 8, 2009</cretaedOn>
        public static void WriteToDatabase(string entry, string type, System.Data.DataSet DatasetWhichGenratedException)
        {
            try
            {
                System.Data.DataSet dsError = new System.Data.DataSet();
                System.Data.DataTable DtErrorMessage = new System.Data.DataTable("ErrorMessages");
                DtErrorMessage.Columns.Add("ErrorMessage");
                DtErrorMessage.Columns.Add("DatasetInfo");
                DtErrorMessage.Columns.Add("ErrorType");
                DtErrorMessage.Columns.Add("CreatedBy");
                DtErrorMessage.Columns.Add("CreatedDate");

                System.Data.DataRow drError = DtErrorMessage.NewRow();
                drError["ErrorMessage"] = entry;
                if (DatasetWhichGenratedException != null)// check for presence of Dataset
                    drError["DatasetInfo"] = DatasetWhichGenratedException.GetXml(); // Store Dataset in Database which actually genrated Exception
                else
                    drError["DatasetInfo"] = null;
                drError["ErrorType"] = type;
                drError["CreatedBy"] = "DefaultUser";
                drError["CreatedDate"] = DateTime.Now;
                DtErrorMessage.Rows.Add(drError);
                dsError.Tables.Add(DtErrorMessage);

                // Get Connection String And Write Log into database
                WriteToDatabase(dsError);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// to write exception detail in DB
        /// </summary>
        /// <param name="dsError"></param>
        /// <CreatedOn>Jan 8, 2009</CreatedOn>
        /// <Author>Pralyankar Kumar Singh</Author>
        public static void WriteToDatabase(DataSet dsError)
        {
            SqlParameter[] objParm = null;
            //XmlDocument XMLDocObj = null;
            //XmlNode RootNode = null;
            string _connectionString = "";
            try
            {
                //string AppSettingInfo;
                //if (System.Web.HttpContext.Current.Session["ERRORLOGConfig"] == null)
                //{
                //    System.Web.HttpContext.Current.Session["ERRORLOGConfig"] = CommonFunctions.GetAppSettingInfo("ERRORLOG");
                //}

                //AppSettingInfo = ((ApplicationSetting)System.Web.HttpContext.Current.Session["ERRORLOGConfig"]).ErrorLogFormat;
                _connectionString = ConfigurationManager.ConnectionStrings["RMCConnectionString1"].ToString();
                //((ApplicationSetting)System.Web.HttpContext.Current.Session["ERRORLOGConfig"]).ErrorLogLocation;
                //if (AppSettingInfo == "DB") // Log Error in Database.
                //{
                #region Log Error In Database
                objParm = new SqlParameter[5];
                objParm[0] = new SqlParameter("@ErrorMessage", dsError.Tables["ErrorMessages"].Rows[0]["ErrorMessage"].ToString());
                objParm[1] = new SqlParameter("@ErrorType", dsError.Tables["ErrorMessages"].Rows[0]["ErrorType"].ToString());
                objParm[2] = new SqlParameter("@CreatedBy", dsError.Tables["ErrorMessages"].Rows[0]["CreatedBy"].ToString());
                objParm[3] = new SqlParameter("@CreatedDate", Convert.ToDateTime(dsError.Tables["ErrorMessages"].Rows[0]["CreatedDate"]));
                objParm[4] = new SqlParameter("@DatasetInfo", dsError.Tables["ErrorMessages"].Rows[0]["DatasetInfo"].ToString());// Added by Piyush on 21st June 2007, so as to pass another parameter for Dataset
                SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "ssp_LogError", objParm);
                #endregion
                //}
                //else if (AppSettingInfo == "TXT") // Log Error in Text File.
                //{
                //    #region Log Error in Text File

                //    string FilePath = _connectionString;// System.Configuration.ConfigurationSettings.AppSettings["ErrorLogTextFilePath"].ToString();
                //    FilePath = System.Web.HttpContext.Current.Server.MapPath(FilePath);

                //    string ErrorMessage = dsError.Tables["ErrorMessages"].Rows[0]["ErrorMessage"].ToString();

                //    FileInfo file1 = new FileInfo(FilePath);
                //    StreamWriter sw = File.AppendText(FilePath);

                //    sw.WriteLine(ErrorMessage);
                //    sw.WriteLine(dsError.Tables["ErrorMessages"].Rows[0]["ErrorType"].ToString());
                //    sw.WriteLine(dsError.Tables["ErrorMessages"].Rows[0]["CreatedBy"].ToString());
                //    sw.WriteLine(dsError.Tables["ErrorMessages"].Rows[0]["CreatedDate"].ToString());
                //    sw.WriteLine("--------------------------------------------------------------------------------------------------------------------");
                //    sw.Close();
                //    #endregion
                //}
                //else if (AppSettingInfo == "XML") // Log Error in XML File.
                //{
                //    #region Log Error In XML File
                //    XMLDocObj = new XmlDocument();
                //    string FilePath = _connectionString;// System.Configuration.ConfigurationSettings.AppSettings["ErrorLogXmlFilePath"].ToString();
                //    XMLDocObj.Load(System.Web.HttpContext.Current.Server.MapPath(FilePath));

                //    RootNode = XMLDocObj.DocumentElement;

                //    XmlElement XmlElementObj = XMLDocObj.CreateElement("Error");
                //    XmlElement XMLChieldNodeMessage = XMLDocObj.CreateElement("ErrorMessage");
                //    XMLChieldNodeMessage.InnerText = dsError.Tables["ErrorMessages"].Rows[0]["ErrorMessage"].ToString();

                //    XmlElement XMLChieldNodeErrorType = XMLDocObj.CreateElement("ErrorType");
                //    XMLChieldNodeErrorType.InnerText = dsError.Tables["ErrorMessages"].Rows[0]["ErrorType"].ToString();

                //    XmlElement XMLChieldNodeCreatedBy = XMLDocObj.CreateElement("CreatedBy");
                //    XMLChieldNodeCreatedBy.InnerText = dsError.Tables["ErrorMessages"].Rows[0]["CreatedBy"].ToString();

                //    XmlElement XMLChieldNodeCreatedDate = XMLDocObj.CreateElement("CreatedDate");
                //    XMLChieldNodeCreatedDate.InnerText = dsError.Tables["ErrorMessages"].Rows[0]["CreatedDate"].ToString();

                //    XmlElementObj.AppendChild(XMLChieldNodeMessage);
                //    XmlElementObj.AppendChild(XMLChieldNodeErrorType);
                //    XmlElementObj.AppendChild(XMLChieldNodeCreatedBy);
                //    XmlElementObj.AppendChild(XMLChieldNodeCreatedDate);

                //    XmlElement XMLFirstClield = RootNode.FirstChild["Error"]; ;

                //    if (XMLFirstClield != null)
                //    {
                //        RootNode.InsertAfter(XmlElementObj, XMLFirstClield);
                //    }
                //    else
                //    {
                //        RootNode.AppendChild(XmlElementObj);
                //    }

                //    XMLDocObj.Save(System.Web.HttpContext.Current.Server.MapPath(FilePath));

                //    #endregion
                //}
                //else if (AppSettingInfo == "NONE") // Do Not Log Error.
                //{
                //    // Do not log error......
                //}
            }
            catch (Exception ex)
            {
                //_stringObject += " DAL -- void WriteToDatabase(DataSet dsError), parameter Count = 1, ";
                //_stringObject += " Parameter1 Name=dsError";
                //CommonFunctions.ManageExceptions(ex, _stringObject, dsError);
                //throw (ex);
            }
            //finally
            //{
            //    XMLDocObj = null;
            //    RootNode = null;
            //}
        }

        #endregion


        /// <summary>
        /// Event to Insert the error messages to Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <author>Pralyankar Kumar Singh</author>
        /// <cretaedOn>Jan 19, 2009</cretaedOn>
        public static void DefaultPublisher_CallErrorWriteToDatabase(object sender, Microsoft.ApplicationBlocks.ExceptionManagement.DataAvailableForError e)
        {
            System.Data.DataSet DatasetWhichGenratedException = new System.Data.DataSet();
            DatasetWhichGenratedException = (System.Data.DataSet)e.GetAppData;
            WriteToDatabase(Microsoft.ApplicationBlocks.ExceptionManagement.DefaultPublisher.strErrorInfo, Microsoft.ApplicationBlocks.ExceptionManagement.DefaultPublisher.strErrorType, DatasetWhichGenratedException);
        }

    }

}
