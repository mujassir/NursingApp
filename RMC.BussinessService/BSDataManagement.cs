using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSDataManagement
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        #endregion

        #region Public Methods

        /// <summary>
        /// Get All Active Hospital Names For SuperAdmin.        
        /// </summary>
        /// <param name="IsErrorExist">For Valid Data : false; For Non-Valid : true</param>
        /// <returns>BusinessEntity BETreeHospitalInfo List</returns>
        public List<RMC.BusinessEntities.BETreeHospitalInfo> GetAllActiveHospitalInfo(bool IsErrorExist)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BETreeHospitalInfo> hospitalInfoList = (from hi in _objectRMCDataContext.HospitalInfos
                                                                                  where hi.IsDeleted == false
                                                                                  orderby hi.RecordCounter, hi.HospitalInfoID
                                                                                  select new RMC.BusinessEntities.BETreeHospitalInfo
                                                                                  {
                                                                                      HospitalID = hi.HospitalInfoID,
                                                                                      HospitalName = hi.HospitalName,
                                                                                      City = hi.City,
                                                                                      State = (hi.StateID != 0)? _objectRMCDataContext.States.FirstOrDefault(f=>f.StateID == hi.StateID).StateName : string.Empty,
                                                                                      CreatedDate = hi.CreatedDate.Value,
                                                                                      ModifiedDate = hi.ModifiedDate.Value,
                                                                                      IsCollapseHospital = hi.IsCollapse,
                                                                                      HospitalRecordCount = hi.RecordCounter
                                                                                  }).ToList();

                return hospitalInfoList;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllActiveHospitalNames");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Get All Active Hospital For specific user.  
        /// </summary>
        /// <param name="userID">Login User</param>
        /// <param name="IsErrorExist">For Valid Data : false; For Non-Valid : true</param>
        /// <returns>BusinessEntity BETreeHospitalInfo List</returns>
        public List<RMC.BusinessEntities.BETreeHospitalInfo> GetAllActiveHospitalInfoByUserID(int userID, bool IsErrorExist)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BETreeHospitalInfo> hospitalInfoList = (from muh in _objectRMCDataContext.MultiUserHospitals
                                                                                  where muh.HospitalInfo.IsDeleted == false && muh.UserID == userID  //hi.IsActive == true
                                                                                  orderby muh.HospitalInfo.RecordCounter, muh.HospitalInfoID
                                                                                  select new RMC.BusinessEntities.BETreeHospitalInfo
                                                                                  {
                                                                                      HospitalID = muh.HospitalInfo.HospitalInfoID,
                                                                                      HospitalName = muh.HospitalInfo.HospitalName,
                                                                                      City = muh.HospitalInfo.City,
                                                                                      State = muh.HospitalInfo.State.StateName,
                                                                                      CreatedDate = muh.HospitalInfo.CreatedDate.Value,
                                                                                      ModifiedDate = muh.HospitalInfo.ModifiedDate.Value,
                                                                                      PermissionID = Convert.ToInt32(muh.PermissionID),
                                                                                      IsCollapseHospital = muh.IsCollapse,
                                                                                      HospitalRecordCount = muh.HospitalInfo.RecordCounter
                                                                                  }).ToList();

                return hospitalInfoList;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllActiveHospitalNames");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Fetch single element of BETreeHospitalInfo.
        /// </summary>
        /// <param name="hospitalInfoID"></param>
        /// <returns>Object of RMC.BusinessEntities.BETreeHospitalInfo Class</returns>
        public RMC.BusinessEntities.BETreeHospitalInfo GetHospitalInfoByHospitalInfoID(int hospitalInfoID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                RMC.BusinessEntities.BETreeHospitalInfo hospitalInfo = (from muh in _objectRMCDataContext.MultiUserHospitals
                                                                        where muh.HospitalInfo.IsDeleted == false && muh.HospitalInfoID == hospitalInfoID  //hi.IsActive == true
                                                                        orderby muh.HospitalInfo.RecordCounter, muh.HospitalInfoID
                                                                        select new RMC.BusinessEntities.BETreeHospitalInfo
                                                                        {
                                                                            HospitalID = muh.HospitalInfo.HospitalInfoID,
                                                                            HospitalName = muh.HospitalInfo.HospitalName,
                                                                            City = muh.HospitalInfo.City,
                                                                            State = muh.HospitalInfo.State.StateName,
                                                                            CreatedDate = muh.HospitalInfo.CreatedDate.Value,
                                                                            ModifiedDate = muh.HospitalInfo.ModifiedDate.Value,
                                                                            PermissionID = Convert.ToInt32(muh.PermissionID),
                                                                            IsCollapseHospital = muh.IsCollapse,
                                                                            HospitalRecordCount = muh.HospitalInfo.RecordCounter
                                                                        }).FirstOrDefault();

                return hospitalInfo;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllActiveHospitalNames");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Get All Active Hospital units Names.
        /// </summary>
        /// <param name="hospitalInfoID">Hospital ID</param>
        /// <returns>BusinessEntity BETreeHospitalUnits List</returns>
        public List<RMC.BusinessEntities.BETreeHospitalUnits> GetAllActiveHospitalUnits(int hospitalInfoID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BETreeHospitalUnits> hospitalDemographicList = (from hd in _objectRMCDataContext.HospitalDemographicInfos
                                                                                          where hd.IsDeleted == false && hd.HospitalInfoID.Value == hospitalInfoID
                                                                                          orderby hd.HospitalDemographicID
                                                                                          select new RMC.BusinessEntities.BETreeHospitalUnits
                                                                                          {
                                                                                              HospitalDemographicID = hd.HospitalDemographicID,
                                                                                              HospitalUnitName = hd.HospitalUnitName,
                                                                                              CreatedDate = hd.CreatedDate.Value,
                                                                                              ModifiedDate = hd.ModifiedDate.Value,
                                                                                              IsCollapseHospitalUnit = hd.IsCollapse,
                                                                                              HospitalInfoID = hd.HospitalInfoID.Value
                                                                                          }).ToList();

                return hospitalDemographicList;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllActiveHospitalNames");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Get All Active Hospital units Names.
        /// </summary>
        /// <param name="userID">Login User</param>
        /// <param name="hospitalInfoID">Hospital ID</param>
        /// <returns>BusinessEntity BETreeHospitalUnits List</returns>
        public List<RMC.BusinessEntities.BETreeHospitalUnits> GetAllActiveHospitalUnits(int userID, int hospitalInfoID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BETreeHospitalUnits> hospitalDemographicList = (from hd in _objectRMCDataContext.MultiUserDemographics
                                                                                          where hd.HospitalDemographicInfo.IsDeleted == false && hd.HospitalDemographicInfo.HospitalInfoID.Value == hospitalInfoID && hd.UserID == userID
                                                                                          orderby hd.HospitalDemographicInfo.HospitalDemographicID
                                                                                          select new RMC.BusinessEntities.BETreeHospitalUnits
                                                                                          {
                                                                                              HospitalDemographicID = hd.HospitalDemographicInfo.HospitalDemographicID,
                                                                                              HospitalUnitName = hd.HospitalDemographicInfo.HospitalUnitName,
                                                                                              CreatedDate = hd.HospitalDemographicInfo.CreatedDate.Value,
                                                                                              ModifiedDate = hd.HospitalDemographicInfo.ModifiedDate.Value,
                                                                                              IsCollapseHospitalUnit = hd.IsCollapse,
                                                                                              HospitalInfoID = hd.HospitalDemographicInfo.HospitalInfoID.Value,
                                                                                              PermissionID = hd.PermissionID.Value
                                                                                          }).ToList();

                return hospitalDemographicList;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllActiveHospitalNames");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Fetch single Hospital Unit Information. 
        /// </summary>
        /// <param name="hospitalUnitID">Hospital Unit ID</param>
        /// <returns>Bussiness Entity BETreeHospitalUnits.</returns>
        public RMC.BusinessEntities.BETreeHospitalUnits GetHospitalUnitsByHospitalUnitID(int hospitalUnitID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                RMC.BusinessEntities.BETreeHospitalUnits objectHospitalDemographicList = (from hd in _objectRMCDataContext.MultiUserDemographics
                                                                                          where hd.HospitalDemographicInfo.IsDeleted == false && hd.HospitalDemographicInfo.HospitalDemographicID == hospitalUnitID
                                                                                          orderby hd.HospitalDemographicInfo.HospitalDemographicID
                                                                                          select new RMC.BusinessEntities.BETreeHospitalUnits
                                                                                          {
                                                                                              HospitalDemographicID = hd.HospitalDemographicInfo.HospitalDemographicID,
                                                                                              HospitalUnitName = hd.HospitalDemographicInfo.HospitalUnitName,
                                                                                              CreatedDate = hd.HospitalDemographicInfo.CreatedDate.Value,
                                                                                              ModifiedDate = hd.HospitalDemographicInfo.ModifiedDate.Value,
                                                                                              IsCollapseHospitalUnit = hd.IsCollapse,
                                                                                              HospitalInfoID = hd.HospitalDemographicInfo.HospitalInfoID.Value,
                                                                                              PermissionID = hd.PermissionID.Value
                                                                                          }).FirstOrDefault();

                return objectHospitalDemographicList;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetHospitalUnitsByHospitalUnitID");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// File List 
        /// </summary>
        /// <param name="hospitalUnitID"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEDataManagementFileList> GetFileListByHospitalUnitID(int hospitalUnitID, string year, string month)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BEDataManagementFileList> objectGenericBEDataManagementFileList = null;
                List<RMC.DataService.NursePDAInfo> objectGenericNursePDAInfo = (from fl in _objectRMCDataContext.NursePDAInfos
                                                                                where fl.HospitalDemographicID == hospitalUnitID && fl.Year == year && fl.Month == month && fl.IsErrorExist == false
                                                                                select fl).ToList();

                objectGenericBEDataManagementFileList = (from ni in objectGenericNursePDAInfo
                                                         group ni by ni.ConfigName into ConfigGroup
                                                         select new RMC.BusinessEntities.BEDataManagementFileList
                                                         {
                                                             ConfigName = ConfigGroup.Key,
                                                             FileList = (from c in ConfigGroup
                                                                         where c.ConfigName == ConfigGroup.Key
                                                                         select new RMC.BusinessEntities.BEDataManagementFileReffList
                                                                         {
                                                                             FileReff = c.FileRefference,
                                                                             NurseID = c.NurseID,
                                                                             HospitalUploadID = c.HospitalUploadID.Value
                                                                         }).ToList()
                                                         }).ToList();

                return objectGenericBEDataManagementFileList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Fetch year for particular hospital unit.
        /// </summary>
        /// <param name="hospitalUnitID">Hospital Unit ID</param>
        /// <returns>Data Service object of Class Year.</returns>
        public List<RMC.DataService.Year> GetYearByHospitalUnitID(int hospitalUnitID)
        {
            try
            {
                List<RMC.DataService.Year> objectGenericYear = null;
                using (_objectRMCDataContext = new RMC.DataService.RMCDataContext())
                {
                    objectGenericYear = (from y in _objectRMCDataContext.Years
                                         orderby y.Year1
                                         where y.HospitalDemographicID == hospitalUnitID
                                         select y).ToList();
                }

                return objectGenericYear;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  Fetch Month for particular hospital unit and year.
        /// </summary>
        /// <param name="hospitalUnitID">Hospital Unit ID</param>
        /// <param name="year">Year contains all month according Hospital unit id</param>
        /// <returns>Data Service object of Class Month.</returns>
        public List<RMC.DataService.Month> GetMonthByHospitalUnitID(int hospitalUnitID, string year)
        {
            try
            {
                List<RMC.DataService.Month> objectGenericMonth = null;
                using (_objectRMCDataContext = new RMC.DataService.RMCDataContext())
                {
                    objectGenericMonth = (from y in _objectRMCDataContext.Months                                      
                                         where y.Year.Year1.Trim() == year.Trim() && y.Year.HospitalDemographicID == hospitalUnitID 
                                         select y).ToList();
                }

                return objectGenericMonth.OrderBy(o=> Convert.ToInt32(o.MonthName)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// Delete All Record From NursePDAInfo and NursePDADetail Tables.
        /// </summary>
        /// <param name="objectGenericNurseIDs">NurseID's List in a NursePDAInfo Table</param>
        public void DeleteFilesFromNurseDetail(List<int> objectGenericNurseIDs, List<int> objectGenericHospitalUploadIDs)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.DataService.HospitalUpload> objectGenericHospitalUpload = (from hu in _objectRMCDataContext.HospitalUploads
                                                                                    where objectGenericHospitalUploadIDs.Contains(hu.HospitalUploadID)
                                                                                    select hu).ToList();

                List<RMC.DataService.NursePDAInfo> objectGenericNursePDAInfo = (from npd in _objectRMCDataContext.NursePDAInfos
                                                                                where objectGenericNurseIDs.Contains(npd.NurseID)
                                                                                select npd).ToList();

                List<RMC.DataService.NursePDADetail> objectGenericNursePDADetail = (from npd in _objectRMCDataContext.NursePDADetails
                                                                                    where objectGenericNurseIDs.Contains(npd.NurseID)
                                                                                    select npd).ToList();
                //Special case for RMC Phase VI in SpecialType table
                List<RMC.DataService.NursePDASpecialType> objectGenericNursePDASpecialType = (from npst in _objectRMCDataContext.NursePDASpecialTypes
                                                                                              where objectGenericNurseIDs.Contains(Convert.ToInt32(npst.NurseID))
                                                                                              select npst).ToList();

                if (objectGenericNursePDADetail.Count > 0)
                {
                    _objectRMCDataContext.NursePDADetails.DeleteAllOnSubmit(objectGenericNursePDADetail);
                    // Deletes NursePDASpecialType data according to nurseID if exists
                    if (objectGenericNursePDASpecialType.Count > 0)
                    {
                        _objectRMCDataContext.NursePDASpecialTypes.DeleteAllOnSubmit(objectGenericNursePDASpecialType);
                    }
                    if (objectGenericNursePDAInfo.Count > 0)
                    {
                        _objectRMCDataContext.NursePDAInfos.DeleteAllOnSubmit(objectGenericNursePDAInfo);
                    }

                    if (objectGenericHospitalUpload.Count > 0)
                    {
                        objectGenericHospitalUpload.ForEach(delegate(RMC.DataService.HospitalUpload objectHospitalUpload)
                        {
                            if (objectHospitalUpload != null)
                            {
                                if (objectHospitalUpload.FilePath.Length > 0)
                                {
                                    if (System.IO.File.Exists(objectHospitalUpload.FilePath))
                                    {
                                        System.IO.File.Delete(objectHospitalUpload.FilePath);
                                    }
                                }
                            }
                        });
                        _objectRMCDataContext.HospitalUploads.DeleteAllOnSubmit(objectGenericHospitalUpload);
                    }

                    _objectRMCDataContext.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Get All Active Hospital units years.      
        /// </summary>
        private List<RMC.BusinessEntities.BETreeYears> GetAllYearsForHospitalUnits(List<int> objectGenericHospitalInfoIDs, bool IsErrorExist)
        {

            try
            {
                List<RMC.BusinessEntities.BETreeMonths> objectGenericBETreeMonths = GetAllMonthsInYearForHospitalUnits(objectGenericHospitalInfoIDs, IsErrorExist);
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BETreeYears> hospitalUnitYears = (from NPDAI in _objectRMCDataContext.NursePDAInfos
                                                                            where objectGenericHospitalInfoIDs.Contains(NPDAI.HospitalDemographicInfo.HospitalInfoID.Value) && NPDAI.IsErrorExist == IsErrorExist
                                                                            select new RMC.BusinessEntities.BETreeYears
                                                                            {
                                                                                HospitalDemographicID = NPDAI.HospitalDemographicID,
                                                                                Year = NPDAI.Year
                                                                            }).Distinct().ToList();

                hospitalUnitYears.ForEach(huy => huy.HospitalUnitsYearsMonths = objectGenericBETreeMonths.FindAll(delegate(RMC.BusinessEntities.BETreeMonths objectBETreeMonths)
                    {
                        return objectBETreeMonths.HospitalDemographicID == huy.HospitalDemographicID && objectBETreeMonths.Year == huy.Year;
                    }));

                return hospitalUnitYears;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllYearsForHospitalUnits");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Get All Active Hospital unit months.
        /// </summary>
        private List<RMC.BusinessEntities.BETreeMonths> GetAllMonthsInYearForHospitalUnits(List<int> objectGenericHospitalInfoIDs, bool IsErrorExist)
        {

            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BETreeMonths> hospitalUnitYearMonths = (from NPDAI in _objectRMCDataContext.NursePDAInfos
                                                                                  where objectGenericHospitalInfoIDs.Contains(NPDAI.HospitalDemographicInfo.HospitalInfoID.Value)
                                                                                  select new RMC.BusinessEntities.BETreeMonths
                                                                                  {
                                                                                      Year = NPDAI.Year,
                                                                                      Month = NPDAI.Month,
                                                                                      HospitalDemographicID = NPDAI.HospitalDemographicID
                                                                                  }).Distinct().ToList();

                return hospitalUnitYearMonths;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllMonthsInYearForHospitalUnits");
                ex.Data.Add("Class", "BSHospitalInfo");
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
