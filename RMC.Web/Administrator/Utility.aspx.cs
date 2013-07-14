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

namespace RMC.Web.Administrator
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        RMC.DataService.RMCDataContext _objectRMCDataContext = new RMC.DataService.RMCDataContext();
        protected void Button1_Click(object sender, EventArgs e)
        {
            List<RMC.BusinessEntities.BEHospitalUpdate> objHospitalUploads = (from l1 in _objectRMCDataContext.HospitalUploads                                                                            
                                                                                       
                                                                             
                                                                             select new RMC.BusinessEntities.BEHospitalUpdate
                                                                             {
                                                                                 HospitalDemographicId = l1.HospitalDemographicID,
                                                                                 HospitalUploadId=l1.HospitalUploadID,
                                                                                 Year= l1.Year,
                                                                                 Month=l1.Month,
                                                                                 OriginalFileName=l1.OriginalFileName,
                                                                                 FilePath=l1.FilePath,
                                                                                 UploadedFileName=l1.UploadedFileName
                                                                             }).ToList();

            if (objHospitalUploads != null)
            {
                objHospitalUploads.ForEach(delegate(RMC.BusinessEntities.BEHospitalUpdate objectBERep)
               {
                   string filepath = Server.MapPath(Request.ApplicationPath + "/Uploads/" + objectBERep.UploadedFileName);
                   if (System.IO.File.Exists(filepath))
                     {
                         RMC.BussinessService.BSYear objectBSYear = new RMC.BussinessService.BSYear();
                         string unitname = objectBSYear.getUnitname(objectBERep.HospitalDemographicId);
                         string hospitalname = objectBSYear.getHospitalname(objectBERep.HospitalDemographicId);

                         //RMC.BussinessService.BSSDAValidation objectBSSDAValidation = new RMC.BussinessService.BSSDAValidation();
                         //Session["FileUploader"] = "FileUpload";
                         //HttpPostedFile uploadeFile = Request.Files[0];

                         string strHospitalDir = Server.MapPath(Request.ApplicationPath + "/Uploads/" + hospitalname);
                         string strUnitDir = Server.MapPath(Request.ApplicationPath + "/Uploads/" + hospitalname + "/" + unitname);
                         string strYearDir = Server.MapPath(Request.ApplicationPath + "/Uploads/" + hospitalname + "/" + unitname + "/" + objectBERep.Year);
                         string strMonthDir = Server.MapPath(Request.ApplicationPath + "/Uploads/" + hospitalname + "/" + unitname + "/" + objectBERep.Year + "/" + objectBERep.Month);
                         System.IO.DirectoryInfo ObjSearchHospitalDir = new System.IO.DirectoryInfo(strHospitalDir);
                         System.IO.DirectoryInfo ObjSearchUnitDir = new System.IO.DirectoryInfo(strUnitDir);
                         System.IO.DirectoryInfo ObjSearchYearDir = new System.IO.DirectoryInfo(strYearDir);
                         System.IO.DirectoryInfo ObjSearchMonthDir = new System.IO.DirectoryInfo(strMonthDir);
                         if (!ObjSearchHospitalDir.Exists)
                         {
                             ObjSearchHospitalDir.Create();
                         }
                         if (!ObjSearchUnitDir.Exists)
                         {
                             ObjSearchUnitDir.Create();
                         }
                         if (!ObjSearchYearDir.Exists)
                         {
                             ObjSearchYearDir.Create();
                         }
                         if (!ObjSearchMonthDir.Exists)
                         {
                             ObjSearchMonthDir.Create();
                         }

                         string strDirectory = Server.MapPath(Request.ApplicationPath + "/Uploads/" + hospitalname + "/" + unitname + "/" + objectBERep.Year + "/" + objectBERep.Month);
                         string destFile = System.IO.Path.Combine(strDirectory, objectBERep.OriginalFileName);
                        

                         System.IO.File.Copy(filepath, destFile, true);
                         // string filepath = Path.GetFullPath(uploadeFile.FileName);

                         //string pat = @"\\(?:.+)\\(.+)\.(.+)";
                         //Regex r = new Regex(pat);
                         //Match m = r.Match(filepath);
                         //string file_ext = m.Groups[2].Captures[0].ToString();
                         //string filename = m.Groups[1].Captures[0].ToString();
                         //string file = filename + "." + file_ext;

                         //if (file_ext.ToLower().Trim() == "sda")
                         //{
                             //System.Guid guid = Guid.NewGuid();

                             //string guidFileName = uploadeFile.FileName;
                             ////FileUploadSDA.SaveAs(Path.Combine(strDirectory, guidFileName));
                             //uploadeFile.SaveAs(Path.Combine(strDirectory, guidFileName));
                             //filepath = Path.Combine(strDirectory, guidFileName);


                             //if (System.IO.File.Exists(filepath))
                             //    System.IO.File.Delete(filepath);
                             //Response.Write("False^" + uploadeFile.FileName + "^");

                         //}
                         //else
                         //{
                         //    CommonClass.Show("Only .sda Extension Files Allowed!");
                         //}
                     }
               });
            }



            //bool flag = false;
            

            
        }
    }
}