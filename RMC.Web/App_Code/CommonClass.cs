using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Microsoft.VisualBasic;
using System.Text;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using RMC.BussinessService;

namespace RMC.Web
{
    /// <summary>
    /// Defined common class for common functionality
    /// <CreatedBy>Raman</CreatedBy>
    /// <CreatedOn>July 20, 2009</CreatedOn>
    /// </summary>
    public class CommonClass
    {
        public CommonClass()
        {
        }

        #region Properties

        /// <summary>
        /// Use to Access and Save Infomation in a Session.
        /// </summary>
        public static RMC.BusinessEntities.BESessionInfomation SessionInfomation
        {
            get
            {
                //return HttpContext.Current.Session["Infomation"] as RMC.BusinessEntities.BESessionInfomation;
                if (HttpContext.Current.Session["Infomation"] != null)
                {
                    return BSSerialization.Deserialize<RMC.BusinessEntities.BESessionInfomation>(HttpContext.Current.Session["Infomation"] as MemoryStream) as RMC.BusinessEntities.BESessionInfomation;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                //HttpContext.Current.Session["Infomation"] = value;
                HttpContext.Current.Session["Infomation"] = BSSerialization.Serialize(value);
            }
        }

        public static RMC.DataService.UserInfo UserInformation
        {
            get
            {
                if (HttpContext.Current.Session["UserInformation"] != null)
                {
                    //return ((List<RMC.DataService.UserInfo>)HttpContext.Current.Session["UserInformation"])[0];
                    return ((List<RMC.DataService.UserInfo>)(BSSerialization.Deserialize<List<RMC.DataService.UserInfo>>(HttpContext.Current.Session["UserInformation"] as MemoryStream)))[0];
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        /// <summary>
        /// Set Size for grid on listing pages
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>
        public static int DefaultListingPageSize
        {
            get
            {
                return ConfigurationManager.AppSettings["DefaultListingPageSize"] != null ? int.Parse(Convert.ToString(ConfigurationManager.AppSettings["DefaultListingPageSize"])) : 10;
            }
        }

        /// <summary>
        /// Use for saving back button navigation throughout the web pages
        /// </summary>
        public string BackButtonUrl
        {
            get
            {
                List<string> objectStackBackUrl = null;
                string popBackUrl = string.Empty;
                try
                {
                    if (HttpContext.Current.Session["BackUrl"] != null)
                    {
                        //objectStackBackUrl = HttpContext.Current.Session["BackUrl"] as List<string>;
                        objectStackBackUrl = BSSerialization.Deserialize<List<string>>(HttpContext.Current.Session["BackUrl"] as MemoryStream) as List<string>;
                        popBackUrl = objectStackBackUrl[objectStackBackUrl.Count - 1];
                        if (objectStackBackUrl.Count == 0)
                        {
                            HttpContext.Current.Session.Remove("BackUrl");
                        }
                    }
                    return popBackUrl;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (objectStackBackUrl != null)
                    {
                        objectStackBackUrl = null;
                    }
                }
            }
            set
            {
                List<string> objectStackBackUrl = null;
                string currentRequest, lastValue, currentValue;
                try
                {
                    if (HttpContext.Current.Session["BackUrl"] != null)
                    {
                        //objectStackBackUrl = HttpContext.Current.Session["BackUrl"] as List<string>;
                        objectStackBackUrl = BSSerialization.Deserialize<List<string>>(HttpContext.Current.Session["BackUrl"] as MemoryStream) as List<string>;
                    }
                    else
                    {
                        objectStackBackUrl = new List<string>();
                    }

                    if (objectStackBackUrl.Count > 0)
                    {
                        int lastIndex = objectStackBackUrl[objectStackBackUrl.Count - 1].IndexOf('?');
                        int currentIndex = HttpContext.Current.Request.Url.AbsoluteUri.IndexOf('?');
                        int currentValueIndex = value.IndexOf('?');

                        if (currentIndex > 0)
                        {
                            currentRequest = HttpContext.Current.Request.Url.AbsoluteUri.Substring(0, currentIndex);
                        }
                        else
                        {
                            currentRequest = HttpContext.Current.Request.Url.AbsoluteUri;
                        }
                        if (lastIndex > 0)
                        {
                            lastValue = objectStackBackUrl[objectStackBackUrl.Count - 1].Substring(0, lastIndex);
                        }
                        else
                        {
                            lastValue = objectStackBackUrl[objectStackBackUrl.Count - 1];
                        }

                        if (currentValueIndex > 0)
                        {
                            currentValue = value.Substring(0, currentValueIndex);
                        }
                        else
                        {
                            currentValue = value;
                        }

                        if (currentValue != lastValue)
                        {
                            if (currentRequest != lastValue)
                            {
                                objectStackBackUrl.Add(value);
                            }
                            else
                            {
                                objectStackBackUrl.RemoveAt(objectStackBackUrl.Count - 1);
                            }
                        }
                        else
                        {
                            if (currentRequest != lastValue)
                            {
                                objectStackBackUrl.RemoveAt(objectStackBackUrl.Count - 1);
                                objectStackBackUrl.Add(value);
                            }
                            else
                            {
                                objectStackBackUrl.RemoveAt(objectStackBackUrl.Count - 1);
                            }
                        }
                    }
                    else if (objectStackBackUrl.Count == 0)
                    {
                        objectStackBackUrl.Add(value);
                    }

                    HttpContext.Current.Session["BackUrl"] = BSSerialization.Serialize(objectStackBackUrl);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (objectStackBackUrl != null)
                    {
                        objectStackBackUrl = null;
                    }
                }
            }
        }

        /// <summary>
        /// Remove Back Navigation url nodes from stack.
        /// </summary>
        /// <param name="noOfUrls"></param>
        public string RemoveBackButtonUrl(int noOfUrls)
        {
            List<string> objectStackBackUrl = null;
            string backUrl = string.Empty;
            try
            {
                if (HttpContext.Current.Session["BackUrl"] != null)
                {
                    //objectStackBackUrl = HttpContext.Current.Session["BackUrl"] as List<string>;
                    objectStackBackUrl = BSSerialization.Deserialize<List<string>>(HttpContext.Current.Session["BackUrl"] as MemoryStream) as List<string>;
                    int Index = objectStackBackUrl.Count - 1;
                    if (Index >= noOfUrls)
                    {
                        while (noOfUrls >= 0)
                        {
                            if (noOfUrls == 0)
                            {
                                backUrl = objectStackBackUrl[objectStackBackUrl.Count - 1];
                            }
                            else
                            {
                                objectStackBackUrl.RemoveAt(objectStackBackUrl.Count - 1);
                            }
                            noOfUrls--;
                        }

                        HttpContext.Current.Session["BackUrl"] = BSSerialization.Serialize(objectStackBackUrl);
                    }
                }

                return backUrl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objectStackBackUrl = null;
            }
        }

        /// <summary>
        /// Remove Back Navigation url nodes from stack.
        /// </summary>
        /// <param name="noOfUrls"></param>
        public string RemoveBackButtonUrlString(string UrlText)
        {
            List<string> objectStackBackUrl = null;
            List<string> objectStackBackUrlAdd = null;
            string backUrl = string.Empty;
            try
            {
                if (HttpContext.Current.Session["BackUrl"] != null)
                {
                    bool removeUrlFlag = false;
                    
                    //objectStackBackUrl = HttpContext.Current.Session["BackUrl"] as List<string>;
                    objectStackBackUrl = BSSerialization.Deserialize<List<string>>(HttpContext.Current.Session["BackUrl"] as MemoryStream) as List<string>;
                    objectStackBackUrlAdd = new List<string>();
                    foreach (string txt in objectStackBackUrl)
                    {
                        if (txt.Contains(UrlText) && removeUrlFlag == false)
                        {
                            backUrl = txt;
                            removeUrlFlag = true;
                        }

                        if (!removeUrlFlag)
                        {
                            objectStackBackUrlAdd.Add(txt);
                        }
                    }
                    if(objectStackBackUrlAdd != null && objectStackBackUrlAdd.Count > 0)
                        HttpContext.Current.Session["BackUrl"] = BSSerialization.Serialize(objectStackBackUrlAdd);
                    else
                        HttpContext.Current.Session["BackUrl"] = BSSerialization.Serialize(objectStackBackUrl);
                }

                return backUrl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objectStackBackUrl = null;
            }
        }


        public int TotalRecordInBackUrl
        {
            get
            {
                int counter = 0;
                List<string> objectStackBackUrl = new List<string>();
                if (HttpContext.Current.Session["BackUrl"] != null)
                {
                    objectStackBackUrl = BSSerialization.Deserialize<List<string>>(HttpContext.Current.Session["BackUrl"] as MemoryStream) as List<string>;
                    if (objectStackBackUrl != null)
                    {
                        counter = objectStackBackUrl.Count;
                    }
                }
                return counter;
            }
        }

        #region Variables

        protected static Hashtable handlerPages = new Hashtable();

        #endregion

        #region Public Method

        public static void Show(string Message)
        {
            if (!(handlerPages.Contains(HttpContext.Current.Handler)))
            {
                Page currentPage = (Page)HttpContext.Current.Handler;

                if (!((currentPage == null)))
                {
                    Queue messageQueue = new Queue();
                    messageQueue.Enqueue(Message);
                    handlerPages.Add(HttpContext.Current.Handler, messageQueue);
                    currentPage.Unload += new EventHandler(CurrentPageUnload);
                }
            }
            else
            {
                Queue queue = ((Queue)(handlerPages[HttpContext.Current.Handler]));
                queue.Enqueue(Message);
            }
        }

        #endregion

        #region Private Method

        private static void CurrentPageUnload(object sender, EventArgs e)
        {
            Queue queue = ((Queue)(handlerPages[HttpContext.Current.Handler]));
            if (queue != null)
            {
                StringBuilder builder = new StringBuilder();
                int iMsgCount = queue.Count;
                builder.Append("<script language='javascript'>");
                string sMsg;
                while ((iMsgCount > 0))
                {
                    iMsgCount = iMsgCount - 1;
                    sMsg = System.Convert.ToString(queue.Dequeue());
                    sMsg = sMsg.Replace("\"", "'");
                    builder.Append("alert( \"" + sMsg + "\" );");
                }
                builder.Append("</script>");
                handlerPages.Remove(HttpContext.Current.Handler);
                HttpContext.Current.Response.Write(builder.ToString());
                HttpContext.Current.Response.Write("<script language='javascript'> { if(opener) self.close();}</script>");
            }
        }

        #endregion

    }
    /// <summary>
    /// Enumerate user role
    /// <CreatedBy>Raman</CreatedBy>
    /// <CreatedOn>July 20, 2009</CreatedOn>
    /// </summary>
    public enum UserRole
    {
        SuperAdmin = 1,
        Admin = 2,
        PowerUser = 3,
        User = 4
    }
    /// <summary>
    /// Enumerate Permissions
    /// <CreatedBy>Raman</CreatedBy>
    /// <CreatedOn>July 27, 2009</CreatedOn>
    /// </summary>
    public enum PermissionType
    {
        ReadOnly = 1,
        Admin = 2
    }

    public enum CollaborationReportView
    {
        SummarizeAllData = 1,
        SummarizeByYear = 2
    }
}
