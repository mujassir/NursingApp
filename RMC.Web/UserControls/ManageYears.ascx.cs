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
    public partial class ManageYears : System.Web.UI.UserControl
    {

        #region Properties

        public int HospitalUnitID
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Request.QueryString["HospitalDemographicId"]);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.Header.Title = "RMC :: Manage Year";
                if (!Page.IsPostBack)
                {
                    if (Request.UrlReferrer != null)
                    {
                        CommonClass objectCommonClass = new CommonClass();
                        objectCommonClass.BackButtonUrl = Request.UrlReferrer.AbsoluteUri;
                    }
                }

                //if (Request.QueryString["Mode"] != null)
                //{
                //    if (Convert.ToString(Request.QueryString["Mode"]) == "Edit")
                //    {
                //        ButtonSave.Visible = false;
                //        ButtonUpdate.Visible = true;
                //        ListBoxYears.Enabled = false;
                //        TextBoxYears.Text = Convert.ToString(Request.QueryString["Year"]);
                //    }
                //}
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


                    #region"mp.Use to validate on adding duplicate year"
                    bool flag=true;
                                 
                    for (int i = 0; i <= ListBoxYears.Items.Count - 1; i++)
                        {

                            if (ListBoxYears.Items[i].Text == TextBoxYears.Text)
                            
                            {
                               // CommonClass.Show("Year Already exist");
                                flag=false;
                            }
                           //}
                   }
                      if(flag==false)   
                      {
                          CommonClass.Show("Year Already exist");
                      }

                    #endregion
                      else
                    {
                    RMC.BussinessService.BSYear objectBSYear = new RMC.BussinessService.BSYear();
                    RMC.DataService.Year objectYear = new RMC.DataService.Year();

                    objectYear.Year1 = TextBoxYears.Text;
                    objectYear.HospitalDemographicID = HospitalUnitID;
                    if (objectBSYear.InsertYear(objectYear))
                    {
                        
                        //FileIO.FileSystem.RenameDirectory(oldDirectoryPathandName, newDirectoryName);
                        //System.IO.DirectoryInfo obj = new System.IO.DirectoryInfo();
                        //add by cm on 10nov2011
                        string unitname = objectBSYear.getUnitname(objectYear.HospitalDemographicID);
                        string hispitalname = objectBSYear.getHospitalname(objectYear.HospitalDemographicID);

                        CheckDirectory(Server.MapPath(Request.ApplicationPath + "/Uploads/" + hispitalname));
                        string strHospitalDir = Server.MapPath(Request.ApplicationPath + "/Uploads/" + hispitalname);

                        CheckDirectory(Server.MapPath(Request.ApplicationPath + "/Uploads/" + hispitalname + "/" + unitname));
                        string strUnitDir = Server.MapPath(Request.ApplicationPath + "/Uploads/" + hispitalname + "/" + unitname);

                        CheckDirectory(Server.MapPath(Request.ApplicationPath + "/Uploads/" + hispitalname + "/" + unitname.Trim() + "/" + objectYear.Year1));
                        string strDirectory = Server.MapPath(Request.ApplicationPath + "/Uploads/" + hispitalname + "/" + unitname.Trim() + "/" + objectYear.Year1);

                        //subPath = strDirectory; // your code goes here
                        //isExists = System.IO.Directory.Exists(subPath);
                        //if (!isExists)
                        //    System.IO.Directory.CreateDirectory(subPath);

                        System.IO.DirectoryInfo ObjSearchDir = new System.IO.DirectoryInfo(strDirectory);
                        System.IO.DirectoryInfo ObjSearchHospitalDir = new System.IO.DirectoryInfo(strHospitalDir);
                        System.IO.DirectoryInfo ObjSearchUnitDir = new System.IO.DirectoryInfo(strUnitDir);
                        if (!ObjSearchHospitalDir.Exists)
                        {
                            ObjSearchHospitalDir.Create();
                        }
                        if (!ObjSearchUnitDir.Exists)
                        {
                            ObjSearchUnitDir.Create();
                        }
                        if (!ObjSearchDir.Exists)
                        {
                            ObjSearchDir.Create();
                        }
                        //end
                        //CommonClass objectCommonClass = new CommonClass();    
                        //string backUrl = objectCommonClass.BackButtonUrl;
                        //Response.Redirect(backUrl, false);

                        //to redirect the page directly to the month page (acc. to client requirement)
                        if (HttpContext.Current.User.IsInRole("superadmin"))
                        {
                            Response.Redirect("~/Administrator/DataManagementMonth.aspx?HospitalInfoId=" + Convert.ToString(Request.QueryString["HospitalInfoId"]) + "&HospitalDemographicId=" + objectYear.HospitalDemographicID + "&UnitCounter=" + Convert.ToString(Request.QueryString["UnitCounter"]) + "&PermissionID=" + Convert.ToString(Request.QueryString["PermissionID"]) + "&Year=" + objectYear.Year1 + "&IsBackUrlAdd=NO", false);
                        }
                        else
                        {
                            Response.Redirect("~/Users/DataManagementMonth.aspx?HospitalInfoId=" + Convert.ToString(Request.QueryString["HospitalInfoId"]) + "&HospitalDemographicId=" + objectYear.HospitalDemographicID + "&UnitCounter=" + Convert.ToString(Request.QueryString["UnitCounter"]) + "&PermissionID=" + Convert.ToString(Request.QueryString["PermissionID"]) + "&Year=" + objectYear.Year1 + "&IsBackUrlAdd=NO", false);
                        }
                    }
                    else
                    {
                        CommonClass.Show("Failed to insert record.");
                    }

                    ListBoxYears.DataBind();
                }
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
       
        public bool CheckDirectory(string direction)
        {
            bool isExists = System.IO.Directory.Exists(direction);

            if (!isExists)
            {
                System.IO.Directory.CreateDirectory(direction);
                return true;
            }
            else
            {
                return false;
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
                LogManager._stringObject = "ManageYears.ascx ---- ImageButtonBack_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

    }
}