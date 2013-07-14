using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
namespace RMC.BussinessService
{
    public class BSUpload
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        #endregion

        #region Public Methods

        /// <summary>
        /// This function is used to 
        /// Created By : Amit Chawla.
        /// Created Date : June 24, 2009
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="role"></param>
        public bool CheckExtension(string fullFileName)
        {
            try
            {
                bool isExtensionExist = false;
                string[] strExtension = { ".sda" };
                string strFileExtension = Path.GetExtension(fullFileName.ToLower());
                for (int i = 0; i < strExtension.Length; i++)
                {
                    if (strExtension[i].ToLower() == strFileExtension.ToLower())
                    {
                        isExtensionExist = true;
                    }
                }
                if (isExtensionExist == true)
                {
                    return true;

                }
                else
                {
                    return false;

                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UploadFile");
                ex.Data.Add("Class", "BSUpload");
                throw ex;
            }
            finally
            {

            }
        }

        /// <summary>
        /// This function is used to find the full file name
        /// Created By : Amit Chawla.
        /// Created Date : June 24, 2009
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="role"></param>
        public string FindFullPath(string fullFileName)
        {
            try
            {
                String FileName = "";
                string strFileExtension = Path.GetExtension(fullFileName.ToLower());
                FileName = System.Guid.NewGuid().ToString();
                FileName = FileName.Replace("-", "");
                fullFileName = HttpContext.Current.Server.MapPath("~/Uploads" + "//" + FileName + strFileExtension);

                return fullFileName;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "FindFullPath");
                ex.Data.Add("Class", "BSUpload");
                throw ex;
            }
        }

        /// <summary>
        /// Get Upload File for saving data from file to database.
        /// Created By : Davinder Kumar.
        /// Creation Date : Aug 5, 2009.
        /// </summary>
        /// <param name="hospitalDemographicID"></param>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        //public List<RMC.DataService.HospitalUpload> GetLatestUploadFile(int hospitalDemographicID, DateTime fileDate)
        //{
        //    try
        //    {
        //        _objectRMCDataContext = new RMC.DataService.RMCDataContext();

        //        List<RMC.DataService.HospitalUpload> objectGenericHospitalUpload = (from hu in _objectRMCDataContext.HospitalUploads
        //                                                                            where hu.HospitalDemographicID == hospitalDemographicID && hu.IsDataSaved == false && hu.IsDeleted == false && Convert.ToDateTime(hu.FileDate).Date == fileDate.Date
        //                                                                            select hu).ToList<RMC.DataService.HospitalUpload>();

        //        return objectGenericHospitalUpload;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Data.Add("Function", "GetLatestUploadFile");
        //        ex.Data.Add("Class", "BSUpload");
        //        throw ex;
        //    }
        //    finally
        //    {
        //        _objectRMCDataContext.Dispose();
        //    }
        //}

        /// <summary>
        /// Insert File Information Into Database.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 15, 2009.
        /// </summary>
        /// <param name="hospitalUpload"></param>
        public void InsertFileInformation(RMC.DataService.HospitalUpload hospitalUpload)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                _objectRMCDataContext.HospitalUploads.InsertOnSubmit(hospitalUpload);
                _objectRMCDataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertFileInformation");
                ex.Data.Add("Class", "BSUpload");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Update Hospital Upload Information.
        /// Created By : Davinder Kumar.
        /// Creation Date : Aug 05, 2009.
        /// </summary>
        /// <param name="objectUpdHospitalUpload"></param>
        public void UpdateFileInformation(RMC.DataService.HospitalUpload  objectUpdHospitalUpload)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                RMC.DataService.HospitalUpload objectHospitalUpload = (from hu in _objectRMCDataContext.HospitalUploads
                                                                       where hu.HospitalUploadID == objectUpdHospitalUpload.HospitalUploadID && hu.IsDeleted == false
                                                                       select hu).FirstOrDefault();

                objectHospitalUpload.IsDataSaved = true;
                _objectRMCDataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdateFileInformation");
                ex.Data.Add("Class", "BSUpload");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        #endregion

    }
}
