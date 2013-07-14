using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Collections;
using System.Net;
using System.Xml;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace LogExceptions
{

    /// <summary>
    /// To Write common function of Application 
    /// </summary>
    public class CommonFunctions
    {

        /// <summary>
        /// Constructor of the class
        /// </summary>
        public CommonFunctions()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Read ApplicationSetting.XMl and Fill Dropdowns
        /// </summary>
        /// <Author>Pralyankar Kumar Singh</Author>
        /// <CreatedDate>Jan 19, 2009</CreatedDate>
        /// <param name="category"></param>
        /// <param name="DropDownListToBind"></param>
        /// <param name="AddSelectOption"></param>
        public static void FillDropdown(string category, DropDownList DropDownListToBind, bool AddSelectOption, string DefaultText)
        {
            DataSet DatasetObject = null;
            try
            {

                FileStream FileStreamObject = new FileStream(System.Web.HttpContext.Current.Server.MapPath("AppSetting.xml"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlDocument XmlDocumentObject = new XmlDocument();
                XmlDocumentObject.Load(FileStreamObject);
                XmlNodeList XmlNodeListObject = XmlDocumentObject.GetElementsByTagName("ErrorLogType");
                string TypeName = "";
                string TypeValue = null;

                DatasetObject = new DataSet();
                DataTable DataTableObject = new DataTable("Error");
                DataTableObject.Columns.Add("TypeName");
                DataTableObject.Columns.Add("TypeValue");


                for (int i = 0; i < XmlNodeListObject.Count; i++)
                {
                    TypeName = "";
                    TypeValue = "";
                    if (XmlNodeListObject[i].HasChildNodes)
                    {
                        for (int k = 0; k < XmlNodeListObject[i].ChildNodes.Count; k++)
                        {
                            if (XmlNodeListObject[i].ChildNodes[k].Name == "TypeName")
                            {
                                TypeName = XmlNodeListObject[i].ChildNodes[k].InnerText;
                            }
                            if (XmlNodeListObject[i].ChildNodes[k].Name == "TypeValue")
                            {
                                TypeValue = XmlNodeListObject[i].ChildNodes[k].InnerText;
                            }
                        }

                        // Create dataset to bind dorpdown
                        DataRow DataRowObject = DataTableObject.NewRow();
                        DataRowObject["TypeName"] = TypeName;
                        DataRowObject["TypeValue"] = TypeValue;
                        DataTableObject.Rows.Add(DataRowObject);
                        //---------------------------------
                    }
                }

                DatasetObject.Tables.Add(DataTableObject);
                
                // Bind Dropdown
                DropDownListToBind.DataSource = DatasetObject;
                DropDownListToBind.DataTextField = "TypeName";
                DropDownListToBind.DataValueField = "TypeValue";
                DropDownListToBind.DataBind();
                //----------------------

                if (AddSelectOption == true)
                {
                    DropDownListToBind.Items.Insert(0, new ListItem(DefaultText, "-1"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// <Description>method to read all child node of a specified tag</Description>
        /// <Author>Pralyankar Kumar Singh</Author>
        /// <CreatedOn>Jan 8,2009</CreatedOn>
        /// </summary>
        /// <param name="Topic"></param>
        /// <returns>String</returns>
        public static ApplicationSetting GetAppSettingInfo(string ApplicationSettingName)
        {
            ApplicationSetting ApplicationSettingObj = null;
            FileStream FileStreamObject = null;
            XmlDocument XmlDocumentObject = null;
            XmlNodeList XmlNodeListObject = null;
            try
            {

                FileStreamObject = new FileStream(System.Web.HttpContext.Current.Server.MapPath("AppSetting.xml"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlDocumentObject = new XmlDocument();
                XmlDocumentObject.Load(FileStreamObject);
                XmlNodeListObject = XmlDocumentObject.GetElementsByTagName(ApplicationSettingName);

                ApplicationSettingObj = new ApplicationSetting();


                for (int i = 0; i < XmlNodeListObject.Count; i++)
                {
                    if (XmlNodeListObject[i].ChildNodes[i].HasChildNodes)
                    {
                        for (int k = 0; k < XmlNodeListObject[i].ChildNodes.Count; k++)
                        {
                            if (XmlNodeListObject[i].ChildNodes[k].Name == "FieldValue")
                            {
                                ApplicationSettingObj.ErrorLogFormat = XmlNodeListObject[i].ChildNodes[k].InnerText;
                            }
                            if (XmlNodeListObject[i].ChildNodes[k].Name == "LogSource")
                            {
                                ApplicationSettingObj.ErrorLogLocation = XmlNodeListObject[i].ChildNodes[k].InnerText;
                            }
                        }
                    }
                }

                return ApplicationSettingObj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// <Description>method to read all child node of a specified tag</Description>
        /// <Author>Pralyankar Kumar Singh</Author>
        /// <CreatedOn>Jan 8,2009</CreatedOn>
        /// </summary>
        /// <param name="Topic"></param>
        /// <returns>String</returns>
        public static bool UpdateAppSetting(string AppSettingName, ApplicationSetting Object)
        {
            XmlDocument XMLDocObj = null;
            try
            {
                XMLDocObj = new XmlDocument();
                XMLDocObj.Load(HttpContext.Current.Server.MapPath("AppSetting.xml"));

                XmlNode node;
                node = XMLDocObj.DocumentElement;

                foreach (XmlNode node1 in node.ChildNodes)
                {
                    if (node1.Name == AppSettingName)
                    {
                        foreach (XmlNode node2 in node1.ChildNodes)
                        {
                            if (node2.Name.ToLower() == "fieldvalue")
                            {
                                node2.InnerText = Object.ErrorLogFormat ;
                            }
                            if (node2.Name.ToLower() == "logsource")
                            {
                                node2.InnerText = Object.ErrorLogLocation;
                            }
                        }
                    }
                }

                XMLDocObj.Save(HttpContext.Current.Server.MapPath("AppSetting.xml"));

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                XMLDocObj = null;
            }
        }

    }// End Of class 'CommonFunctions'.


    /// <summary>
    /// Class for retaining application settings.
    /// </summary>
    public class ApplicationSetting
    {
        string _ErrorLogFormat;
        string _ErrorLogLocation;

        public string ErrorLogFormat
        {
            get
            {
                return _ErrorLogFormat;
            }
            set
            {
                _ErrorLogFormat = value;
            }
        }

        public string ErrorLogLocation
        {
            get
            {
                return _ErrorLogLocation;
            }
            set
            {
                _ErrorLogLocation = value;
            }
        }
    }// End of class ApplicationSetting

}// End of namespace
