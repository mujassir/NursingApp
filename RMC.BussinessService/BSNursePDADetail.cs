using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSNursePDADetail
    {

        #region Variables

        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets data from NursePDADetails
        /// </summary>
        /// <param name="NurseID"></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEValidationData> GetValidData(int NurseID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BEValidationData> objectGenericBEValidationData = null;

                objectGenericBEValidationData = (from vd in _objectRMCDataContext.NursePDADetails
                                                 where vd.NurseID == NurseID && vd.IsDeleted == false && vd.IsActive == true && vd.IsErrorExist == false
                                                 select new RMC.BusinessEntities.BEValidationData
                                                 {
                                                     LastLocationName = GetLastLocationName(Convert.ToString(vd.LastLocationID)),
                                                     LocationName = GetLocationName(vd.LocationID),
                                                     LocationDate = vd.LocationDate,
                                                     LocationTime = vd.LocationTime,
                                                     ActivityName = GetActivityName(vd.ActivityID),
                                                     SubActivityName = GetSubActivityName(Convert.ToString(vd.SubActivityID)),
                                                     ResourceRequirementName = GetResourceRequirement(Convert.ToString(vd.ResourceRequirementID)),
                                                     //TypeName = GetTypeName(vd.TypeID),
                                                     //CategoryGroupName = GetCategoryGroupName(vd.CategoryGroupID)
                                                     CongnitiveCategories = vd.CognitiveCategory,
                                                     NursePDADetailID = vd.NurserDetailID
                                                 }).ToList();

                objectGenericBEValidationData.ForEach(delegate(RMC.BusinessEntities.BEValidationData objectBEValidationData)
                {
                    if (objectBEValidationData.LastLocationName.Length > 5)
                    {
                        objectBEValidationData.LastLocationName = objectBEValidationData.LastLocationName.Substring(5);
                    }
                });
                return objectGenericBEValidationData;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetNursePDADetail");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
        }

        /// <summary>
        /// Gets data from NursePDASpecialType
        /// </summary>
        /// <param name="NurseID"></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEValidationSpecialType> GetSpecialTypeData(int NurseID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BEValidationSpecialType> objectGenericBEValidationSpecialType = null;

                objectGenericBEValidationSpecialType = (from st in _objectRMCDataContext.NursePDASpecialTypes
                                                        where st.NurseID == NurseID
                                                        select new RMC.BusinessEntities.BEValidationSpecialType
                                                        {
                                                            Date = st.Date,
                                                            Time = st.Time,
                                                            SpecialActivity = st.SpecialActivity,
                                                            SpecialCategory = st.SpecialCategory,
                                                            SpecialTypeID = st.SpecialTypeID
                                                        }).ToList();

                return objectGenericBEValidationSpecialType;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetSpecialTypeData");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NurseID"></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEValidationData> GetNonValidData(int NurseID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BEValidationData> objectGenericBEValidationData = null;

                objectGenericBEValidationData = (from vd in _objectRMCDataContext.NursePDADetails
                                                 where vd.NurseID == NurseID && vd.IsDeleted == false && vd.IsActive == true
                                                 select new RMC.BusinessEntities.BEValidationData
                                                 {
                                                     LastLocationName = GetLastLocationName(vd.LastLocationID),
                                                     LocationName = GetLocationName(vd.LocationID),
                                                     LocationDate = vd.LocationDate,
                                                     LocationTime = vd.LocationTime,
                                                     ActivityName = GetActivityName(vd.ActivityID),
                                                     SubActivityName = GetSubActivityName(Convert.ToInt32(vd.SubActivityID)),
                                                     ResourceRequirementName = GetResourceRequirement(Convert.ToInt32(vd.ResourceRequirementID)),
                                                     TypeName = GetTypeName(vd.TypeID),
                                                     CategoryGroupName = GetCategoryGroupName(vd.CategoryGroupID),
                                                     IsErrorExist = Convert.ToBoolean(vd.IsErrorExist)
                                                 }).ToList();

                return objectGenericBEValidationData;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetNursePDADetail");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
        }

        public List<RMC.BusinessEntities.BEValidation> GetNonValidDataByHospitalUnitID(int hospitalUntiID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation = null;

                objectGenericBEValidation = (from NPD in _objectRMCDataContext.NursePDADetails
                                             where NPD.NursePDAInfo.HospitalDemographicID == hospitalUntiID && NPD.IsErrorExist == true && NPD.IsActive == true && NPD.IsDeleted == false
                                             select new RMC.BusinessEntities.BEValidation
                                             {
                                                 ActivityID = NPD.ActivityID,
                                                 ActivityName = (NPD.ActivityID != 0) ? GetActivityName(NPD.ActivityID) : NPD.ActivityText,
                                                 ActivityText = NPD.ActivityText,
                                                 IsActive = NPD.IsActive,
                                                 IsDeleted = NPD.IsDeleted,
                                                 IsActiveError = NPD.IsActiveError,
                                                 IsErrorExist = NPD.IsErrorExist,
                                                 IsErrorInActivity = NPD.IsErrorInActivity,
                                                 IsErrorInLocation = NPD.IsErrorInLocation,
                                                 IsErrorInSubActivity = NPD.IsErrorInSubActivity,
                                                 LastLocationID = NPD.LastLocationID,
                                                 LastLocationName = GetLastLocationName(NPD.LastLocationID),
                                                 LocationDate = NPD.LocationDate,
                                                 LocationID = NPD.LocationID,
                                                 LocationName = (NPD.LocationID != 0) ? GetLocationName(NPD.LocationID) : NPD.LocationText,
                                                 LocationText = NPD.LocationText,
                                                 LocationTime = NPD.LocationTime,
                                                 NurseID = NPD.NurseID,
                                                 NurserDetailID = NPD.NurserDetailID,
                                                 SubActivityID = Convert.ToInt32(NPD.SubActivityID),
                                                 SubActivityName = (NPD.SubActivityID != 0) ? GetSubActivityName(Convert.ToString(NPD.SubActivityID)) : Convert.ToString(NPD.SubActivityText),
                                                 SubActivityText = NPD.SubActivityText,
                                                 ActiveError1 = NPD.ActiveError1,
                                                 ActiveError2 = NPD.ActiveError2,
                                                 ActiveError3 = NPD.ActiveError3,
                                                 ActiveError4 = NPD.ActiveError4,
                                                 ResourceRequirementID = NPD.ResourceRequirementID.Value,
                                                 ResourceText = GetResourceRequirement(NPD.ResourceRequirementID.Value),
                                                 CognitiveCategory = NPD.CognitiveCategory
                                             }).ToList<RMC.BusinessEntities.BEValidation>();

                objectGenericBEValidation.ForEach(delegate(RMC.BusinessEntities.BEValidation objectBEValidation)
                {
                    if (objectBEValidation.LastLocationName.Length > 5)
                    {
                        objectBEValidation.LastLocationName = objectBEValidation.LastLocationName.Substring(5);
                    }
                });

                return objectGenericBEValidation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetNonValidDataByHospitalUnitID");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public string GetLastLocationName(int lastLocationID)
        {
            try
            {
                string lastLocationName;
                if (lastLocationID > 0)
                {
                    _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                    var LastLocation = (from ll in _objectRMCDataContext.LastLocations
                                        where ll.LastLocationID == lastLocationID && ll.IsActive == true
                                        select ll).FirstOrDefault();
                    lastLocationName = LastLocation.LastLocation1;
                }
                else
                {
                    lastLocationName = string.Empty;
                }
                return lastLocationName;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetLastLocationName");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
        }

        public string GetLastLocationName(string lastLocID)
        {
            try
            {
                string lastLocationName;
                int lastLocationID = 0;
                if (!int.TryParse(lastLocID, out lastLocationID))
                {
                    lastLocationID = 0;
                }

                if (lastLocationID > 0)
                {
                    _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                    var LastLocation = (from ll in _objectRMCDataContext.LastLocations
                                        where ll.LastLocationID == lastLocationID && ll.IsActive == true
                                        select ll).FirstOrDefault();
                    lastLocationName = LastLocation.LastLocation1;
                }
                else
                {
                    lastLocationName = string.Empty;
                }
                return lastLocationName;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetLastLocationName");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
        }

        public string GetLocationName(int locationID)
        {
            try
            {
                string location;
                if (locationID > 0)
                {
                    _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                    var Location = (from l in _objectRMCDataContext.Locations
                                    where l.LocationID == locationID && l.IsActive == true
                                    select l).FirstOrDefault();

                    location = Location.Location1;
                }
                else
                {
                    location = string.Empty;
                }
                return location;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetLocationName");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
        }

        public string GetActivityName(int activityID)
        {
            try
            {
                string activityName;
                if (activityID > 0)
                {
                    _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                    var activity = (from a in _objectRMCDataContext.Activities
                                    where a.ActivityID == activityID && a.IsActive == true
                                    select a).FirstOrDefault();

                    activityName = activity.Activity1;
                }
                else
                {
                    activityName = string.Empty;
                }

                return activityName;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetActivityName");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
        }

        public string GetSubActivityName(string subActivityIDs)
        {
            try
            {
                int subActivityID = 0;
                string subActivityName = string.Empty;

                if (!int.TryParse(subActivityIDs, out subActivityID))
                {
                    subActivityID = 0;
                }

                if (subActivityID > 0)
                {
                    _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                    var subActivity = (from sa in _objectRMCDataContext.SubActivities
                                       where sa.SubActivityID == subActivityID && sa.IsActive == true
                                       select sa).FirstOrDefault();

                    subActivityName = subActivity.SubActivity1;
                }
                else
                {
                    subActivityName = string.Empty;
                }

                return subActivityName;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetSubActivityName");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
        }

        public string GetSubActivityName(int subActivityID)
        {
            try
            {
                string subActivityName = string.Empty;

                if (subActivityID > 0)
                {
                    _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                    var subActivity = (from sa in _objectRMCDataContext.SubActivities
                                       where sa.SubActivityID == subActivityID && sa.IsActive == true
                                       select sa).FirstOrDefault();

                    subActivityName = subActivity.SubActivity1;
                }
                else
                {
                    subActivityName = string.Empty;
                }

                return subActivityName;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetSubActivityName");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
        }

        public string GetCategoryGroupName(int categoryGroupID)
        {
            try
            {
                string categoryGroupName;
                if (categoryGroupID > 0)
                {
                    _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                    var categoryGroup = (from cg in _objectRMCDataContext.CategoryGroups
                                         where cg.CategoryGroupID == categoryGroupID && cg.IsActive == true
                                         select cg).FirstOrDefault();

                    categoryGroupName = categoryGroup.CategoryGroup1;
                }
                else
                {
                    categoryGroupName = string.Empty;
                }

                return categoryGroupName;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetCategoryGroupName");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
        }

        public string GetTypeName(int typeID)
        {
            try
            {
                string typeName;
                if (typeID > 0)
                {
                    _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                    var Type = (from t in _objectRMCDataContext.ValueAddedTypes
                                where t.TypeID == typeID && t.IsActive == true
                                select t).FirstOrDefault();

                    typeName = Type.TypeName;
                }
                else
                {
                    typeName = string.Empty;
                }

                return typeName;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetTypeName");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
        }

        public string GetResourceRequirement(int resourceRequirementID)
        {
            try
            {
                string resourceReq;
                if (resourceRequirementID > 0)
                {
                    _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                    var ResourceRequirement = (from rr in _objectRMCDataContext.ResourceRequirements
                                               where rr.ResourceRequirementID == resourceRequirementID && rr.IsActive == true
                                               select rr).FirstOrDefault();

                    resourceReq = ResourceRequirement.ResourceRequirement1;
                }
                else
                {
                    resourceReq = string.Empty;
                }

                return resourceReq;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetResourceRequirement");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
        }

        public string GetResourceRequirement(string resRequirementID)
        {
            try
            {
                string resourceReq;
                int resourceRequirementID = 0;
                if (!int.TryParse(resRequirementID, out resourceRequirementID))
                {
                    resourceRequirementID = 0;
                }

                if (resourceRequirementID > 0)
                {
                    _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                    var ResourceRequirement = (from rr in _objectRMCDataContext.ResourceRequirements
                                               where rr.ResourceRequirementID == resourceRequirementID && rr.IsActive == true
                                               select rr).FirstOrDefault();

                    resourceReq = ResourceRequirement.ResourceRequirement1;
                }
                else
                {
                    resourceReq = string.Empty;
                }

                return resourceReq;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetResourceRequirement");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
        }

        /// <summary>
        /// Fetch Total Number of files and data points.
        /// </summary>
        /// <param name="hospitalUnitID">Hospital Unit ID</param>
        /// <param name="year">Year</param>
        /// <param name="objectGenericMonth">List of Month</param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BENursePDAFileCounter> GetTotalFilesAndDataPoints(int hospitalUnitID, string year, List<string> objectGenericMonth)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BENursePDAFileCounter> objectBENursePDAFileCounter = (from p in _objectRMCDataContext.NursePDAInfos
                                                                                                where p.HospitalDemographicID == hospitalUnitID && p.Year == year && objectGenericMonth.Contains(p.Month) && p.IsErrorExist == false
                                                                                                group p by p.Month into x
                                                                                                select new RMC.BusinessEntities.BENursePDAFileCounter
                                                                                                {
                                                                                                    HospitalUnitID = x.FirstOrDefault().HospitalDemographicID,
                                                                                                    Month = x.Key,
                                                                                                    TotalFiles = x.Count(),
                                                                                                    TotalRecords = _objectRMCDataContext.NursePDADetails.Where(w => x.Select(s => s.NurseID).Contains(w.NurseID) && w.IsErrorExist == false).Count()
                                                                                                }).ToList();

                return objectBENursePDAFileCounter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Overload Methods.
        /// </summary>
        /// <param name="hospitalUnitID">Hospital Unit ID</param>
        /// <param name="objectGenericYear">Year</param>
        /// <returns>List of NuresePDAFileCounter objects.</returns>
        public List<RMC.BusinessEntities.BENursePDAFileCounter> GetTotalFilesAndDataPoints(int hospitalUnitID, List<string> objectGenericYear)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BENursePDAFileCounter> objectBENursePDAFileCounter = (from p in _objectRMCDataContext.NursePDAInfos
                                                                                                where p.HospitalDemographicID == hospitalUnitID && objectGenericYear.Contains(p.Year) && p.IsErrorExist == false
                                                                                                group p by p.Year into x
                                                                                                select new RMC.BusinessEntities.BENursePDAFileCounter
                                                                                                {
                                                                                                    HospitalUnitID = x.FirstOrDefault().HospitalDemographicID,
                                                                                                    Year = x.Key,
                                                                                                    TotalFiles = x.Count(),
                                                                                                    TotalRecords = _objectRMCDataContext.NursePDADetails.Where(w => x.Select(s => s.NurseID).Contains(w.NurseID) && w.IsErrorExist == false).Count()
                                                                                                }).ToList();

                return objectBENursePDAFileCounter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Overload Method.
        /// </summary>
        /// <param name="objectGenericHospitalUnitID"></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BENursePDAFileCounter> GetTotalFilesAndDataPoints(List<int> objectGenericHospitalUnitID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BENursePDAFileCounter> objectBENursePDAFileCounter = (from p in _objectRMCDataContext.NursePDAInfos
                                                                                                where objectGenericHospitalUnitID.Contains(p.HospitalDemographicID) && p.IsErrorExist == false
                                                                                                group p by p.HospitalDemographicID into x
                                                                                                select new RMC.BusinessEntities.BENursePDAFileCounter
                                                                                                {
                                                                                                    HospitalUnitID = x.FirstOrDefault().HospitalDemographicID,
                                                                                                    TotalFiles = x.Count(),
                                                                                                    TotalRecords = _objectRMCDataContext.NursePDADetails.Where(w => x.Select(s => s.NurseID).Contains(w.NurseID) && w.IsErrorExist == false).Count()
                                                                                                }).ToList();

                return objectBENursePDAFileCounter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Return TotalFiles and TotalDataPoints By HospitalUnitID.
        /// </summary>
        /// <param name="nurseDetailID"></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEValidation> GetNonValidDataByNurseDetailID(int nurseDetailID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation = null;

                objectGenericBEValidation = (from NPD in _objectRMCDataContext.NursePDADetails
                                             where NPD.NurserDetailID == nurseDetailID
                                             select new RMC.BusinessEntities.BEValidation
                                             {
                                                 ActivityID = NPD.ActivityID,
                                                 ActivityName = (NPD.ActivityID != 0) ? GetActivityName(NPD.ActivityID) : NPD.ActivityText,
                                                 ActivityText = NPD.ActivityText,
                                                 IsActive = NPD.IsActive,
                                                 IsDeleted = NPD.IsDeleted,
                                                 IsActiveError = NPD.IsActiveError,
                                                 IsErrorExist = NPD.IsErrorExist,
                                                 IsErrorInActivity = NPD.IsErrorInActivity,
                                                 IsErrorInLocation = NPD.IsErrorInLocation,
                                                 IsErrorInSubActivity = NPD.IsErrorInSubActivity,
                                                 LastLocationID = NPD.LastLocationID,
                                                 LastLocationName = GetLastLocationName(NPD.LastLocationID),
                                                 LocationDate = NPD.LocationDate,
                                                 LocationID = NPD.LocationID,
                                                 LocationName = (NPD.LocationID != 0) ? GetLocationName(NPD.LocationID) : NPD.LocationText,
                                                 LocationText = NPD.LocationText,
                                                 LocationTime = NPD.LocationTime,
                                                 NurseID = NPD.NurseID,
                                                 NurserDetailID = NPD.NurserDetailID,
                                                 SubActivityID = Convert.ToInt32(NPD.SubActivityID),
                                                 SubActivityName = (NPD.SubActivityID != 0) ? GetSubActivityName(Convert.ToString(NPD.SubActivityID)) : Convert.ToString(NPD.SubActivityText),
                                                 SubActivityText = NPD.SubActivityText,
                                                 ActiveError1 = NPD.ActiveError1,
                                                 ActiveError2 = NPD.ActiveError2,
                                                 ActiveError3 = NPD.ActiveError3,
                                                 ActiveError4 = NPD.ActiveError4,
                                                 ResourceRequirementID = NPD.ResourceRequirementID.Value,
                                                 ResourceText = GetResourceRequirement(NPD.ResourceRequirementID.Value),
                                                 CognitiveCategory = NPD.CognitiveCategory,
                                                 Month = BSCommon.GetMonthName(NPD.NursePDAInfo.Month),
                                                 Year = NPD.NursePDAInfo.Year,
                                                 FileName = NPD.NursePDAInfo.FileRefference
                                             }).ToList<RMC.BusinessEntities.BEValidation>();

                objectGenericBEValidation.ForEach(delegate(RMC.BusinessEntities.BEValidation objectBEValidation)
                {
                    if (objectBEValidation.LastLocationName.Length > 5)
                    {
                        objectBEValidation.LastLocationName = objectBEValidation.LastLocationName.Substring(5);
                    }
                });

                return objectGenericBEValidation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetNonValidDataByNurseDetailID");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public bool CheckForNonValidData(int hospitalUnitID)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.DataService.NursePDADetail> objectGenericNursePDADetail = (from npdd in _objectRMCDataContext.NursePDADetails
                                                                                    where npdd.NursePDAInfo.HospitalDemographicID == hospitalUnitID && npdd.IsErrorExist == true
                                                                                    select npdd).ToList();

                if (objectGenericNursePDADetail.Count > 0)
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "CheckForNonValidData");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }

            return flag;
        }

        public bool CheckForValidData(int hospitalUnitID)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.DataService.NursePDADetail> objectGenericNursePDADetail = (from npdd in _objectRMCDataContext.NursePDADetails
                                                                                    where npdd.NursePDAInfo.HospitalDemographicID == hospitalUnitID && npdd.IsErrorExist == false
                                                                                    select npdd).ToList();

                if (objectGenericNursePDADetail.Count > 0)
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "CheckForValidData");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }

            return flag;
        }

        public bool DeleteNursePDADetail(RMC.BusinessEntities.BEValidationData objectBEValidationData)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                RMC.DataService.NursePDADetail objectNursePDADetail = null;

                objectNursePDADetail = (from npd in _objectRMCDataContext.NursePDADetails
                                        where npd.NurserDetailID == objectBEValidationData.NursePDADetailID
                                        select npd).FirstOrDefault();
                if (objectNursePDADetail != null)
                {
                    _objectRMCDataContext.NursePDADetails.DeleteOnSubmit(objectNursePDADetail);
                }

                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteNursePDADetail");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return flag;
        }

        public bool DeleteNursePDADetail(List<int> objectGenericNurseDetailIDs)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.DataService.NursePDADetail> objectGenericNursePDADetail = null;

                objectGenericNursePDADetail = (from npd in _objectRMCDataContext.NursePDADetails
                                               where objectGenericNurseDetailIDs.Contains(npd.NurserDetailID)
                                               select npd).ToList();
                if (objectGenericNursePDADetail.Count > 0)
                {
                    _objectRMCDataContext.NursePDADetails.DeleteAllOnSubmit(objectGenericNursePDADetail);
                }

                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteNursePDADetail");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return flag;
        }

        /// <summary>
        /// Deletes the selected record from NursePDASpecialTypeIDs 
        /// </summary>
        public bool DeleteNursePDASpecialType(List<int> objectGenericNursePDASpecialTypeIDs)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.DataService.NursePDASpecialType> objectGenericNursePDASpecialType = null;

                objectGenericNursePDASpecialType = (from npd in _objectRMCDataContext.NursePDASpecialTypes
                                                    where objectGenericNursePDASpecialTypeIDs.Contains(npd.SpecialTypeID)
                                                    select npd).ToList();
                if (objectGenericNursePDASpecialType.Count > 0)
                {
                    _objectRMCDataContext.NursePDASpecialTypes.DeleteAllOnSubmit(objectGenericNursePDASpecialType);
                }

                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteNursePDADetail");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return flag;
        }

        public bool DeleteNursePDADetail(int nursePDADetailID)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                RMC.DataService.NursePDADetail objectNursePDADetail = null;

                objectNursePDADetail = (from npd in _objectRMCDataContext.NursePDADetails
                                        where npd.NurserDetailID == nursePDADetailID
                                        select npd).FirstOrDefault();
                if (objectNursePDADetail != null)
                {
                    _objectRMCDataContext.NursePDADetails.DeleteOnSubmit(objectNursePDADetail);
                }

                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteNursePDADetail");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return flag;
        }

        public int DeleteNursePDAInfo(int nurseID)
        {
            int hospitalUnitID = 0;
            try
            {
                string uploadedFileName = string.Empty;
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.DataService.NursePDADetail> objectGenericNursePDADetail = null;
                RMC.DataService.NursePDAInfo objectNursePDAInfo = null;
                RMC.DataService.HospitalUpload objectHospitalUpload = null;

                objectGenericNursePDADetail = (from npd in _objectRMCDataContext.NursePDADetails
                                               where npd.NurseID == nurseID
                                               select npd).ToList();

                objectNursePDAInfo = (from npd in _objectRMCDataContext.NursePDAInfos
                                      where npd.NurseID == nurseID
                                      select npd).FirstOrDefault();

                objectHospitalUpload = (from hu in _objectRMCDataContext.HospitalUploads
                                        where hu.HospitalUploadID == objectNursePDAInfo.HospitalUploadID
                                        select hu).FirstOrDefault();
                if (objectHospitalUpload != null)
                {
                    uploadedFileName = objectHospitalUpload.UploadedFileName;
                    hospitalUnitID = objectHospitalUpload.HospitalDemographicID;
                }
                if (objectGenericNursePDADetail.Count > 0)
                {
                    _objectRMCDataContext.NursePDADetails.DeleteAllOnSubmit(objectGenericNursePDADetail);
                }

                if (objectNursePDAInfo != null)
                {
                    _objectRMCDataContext.NursePDAInfos.DeleteOnSubmit(objectNursePDAInfo);
                }

                if (objectHospitalUpload != null)
                {
                    _objectRMCDataContext.HospitalUploads.DeleteOnSubmit(objectHospitalUpload);
                }

                _objectRMCDataContext.SubmitChanges();

                string path = AppDomain.CurrentDomain.BaseDirectory + "\\Uploads\\" + uploadedFileName;
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteNursePDADetail");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return hospitalUnitID;
        }

        public bool DeleteNursePDAInfo(int hospitalUnitID, string Year, string month)
        {
            bool flag = false;
            try
            {
                List<string> objectGenericFilePath = new List<string>();
                List<int> objectGenericHospitalUploadIDs = new List<int>();
                List<int> objectGenericNurseIDs = new List<int>();
                List<RMC.DataService.NursePDADetail> objectGenericNursePDADetail = null;
                List<RMC.DataService.NursePDAInfo> objectGenericNursePDAInfo = null;
                List<RMC.DataService.HospitalUpload> objectGenericHospitalUpload = null;
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.DataService.Month> objectGenericMonth = (from m in _objectRMCDataContext.Months
                                                                  where m.Year.HospitalDemographicID == hospitalUnitID && m.Year.Year1.Trim() == Year.Trim() && m.MonthName.Trim() == month.Trim()
                                                                  select m).ToList();

                objectGenericHospitalUpload = (from hu in _objectRMCDataContext.HospitalUploads
                                               where hu.HospitalDemographicID == hospitalUnitID && hu.Year.Trim() == Year.Trim() && hu.Month.Trim() == month.Trim()
                                               select hu).ToList();

                if (objectGenericMonth.Count > 0)
                {
                    _objectRMCDataContext.Months.DeleteAllOnSubmit(objectGenericMonth);
                }

                if (objectGenericHospitalUpload.Count > 0)
                {
                    foreach (RMC.DataService.HospitalUpload objectHospitalUpload in objectGenericHospitalUpload)
                    {
                        objectGenericFilePath.Add(objectHospitalUpload.FilePath);
                        objectGenericHospitalUploadIDs.Add(objectHospitalUpload.HospitalUploadID);
                    }

                    if (objectGenericHospitalUploadIDs.Count > 0)
                    {
                        objectGenericNursePDAInfo = (from npd in _objectRMCDataContext.NursePDAInfos
                                                     where objectGenericHospitalUploadIDs.Contains(npd.HospitalUploadID.Value) && npd.HospitalDemographicID == hospitalUnitID
                                                     select npd).ToList();

                        if (objectGenericNursePDAInfo.Count > 0)
                        {
                            foreach (RMC.DataService.NursePDAInfo objectNursePDAInfo in objectGenericNursePDAInfo)
                            {
                                objectGenericNurseIDs.Add(objectNursePDAInfo.NurseID);
                            }

                            if (objectGenericNurseIDs.Count > 0)
                            {
                                objectGenericNursePDADetail = (from npd in _objectRMCDataContext.NursePDADetails
                                                               where objectGenericNurseIDs.Contains(npd.NurseID)
                                                               select npd).ToList();

                                if (objectGenericNursePDADetail.Count > 0)
                                {
                                    _objectRMCDataContext.NursePDADetails.DeleteAllOnSubmit(objectGenericNursePDADetail);
                                }
                            }

                            _objectRMCDataContext.NursePDAInfos.DeleteAllOnSubmit(objectGenericNursePDAInfo);
                        }

                        _objectRMCDataContext.HospitalUploads.DeleteAllOnSubmit(objectGenericHospitalUpload);
                    }
                }

                _objectRMCDataContext.SubmitChanges();

                foreach (string filePath in objectGenericFilePath)
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + "\\Uploads\\" + filePath;
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }

                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteNursePDAInfo");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return flag;
        }

        public bool DeleteNursePDAInfo(int hospitalUnitID, string Year)
        {
            bool flag = false;
            try
            {
                List<string> objectGenericFilePath = new List<string>();
                List<int> objectGenericHospitalUploadIDs = new List<int>();
                List<int> objectGenericNurseIDs = new List<int>();
                List<RMC.DataService.NursePDADetail> objectGenericNursePDADetail = null;
                List<RMC.DataService.NursePDAInfo> objectGenericNursePDAInfo = null;
                List<RMC.DataService.HospitalUpload> objectGenericHospitalUpload = null;
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.DataService.Year> objectGenericYear = (from y in _objectRMCDataContext.Years
                                                                where y.HospitalDemographicID == hospitalUnitID && y.Year1.Trim() == Year.Trim()
                                                                select y).ToList();

                objectGenericHospitalUpload = (from hu in _objectRMCDataContext.HospitalUploads
                                               where hu.HospitalDemographicID == hospitalUnitID && hu.Year.Trim() == Year.Trim()
                                               select hu).ToList();

                if (objectGenericYear.Count > 0)
                {
                    _objectRMCDataContext.Years.DeleteAllOnSubmit(objectGenericYear);
                }

                
                if (objectGenericHospitalUpload.Count > 0)
                {
                    foreach (RMC.DataService.HospitalUpload objectHospitalUpload in objectGenericHospitalUpload)
                    {
                        objectGenericFilePath.Add(objectHospitalUpload.FilePath);
                        objectGenericHospitalUploadIDs.Add(objectHospitalUpload.HospitalUploadID);
                    }

                    if (objectGenericHospitalUploadIDs.Count > 0)
                    {
                        objectGenericNursePDAInfo = (from npd in _objectRMCDataContext.NursePDAInfos
                                                     where objectGenericHospitalUploadIDs.Contains(npd.HospitalUploadID.Value) && npd.HospitalDemographicID == hospitalUnitID
                                                     select npd).ToList();

                        if (objectGenericNursePDAInfo.Count > 0)
                        {
                            foreach (RMC.DataService.NursePDAInfo objectNursePDAInfo in objectGenericNursePDAInfo)
                            {
                                objectGenericNurseIDs.Add(objectNursePDAInfo.NurseID);
                            }

                            if (objectGenericNurseIDs.Count > 0)
                            {
                                objectGenericNursePDADetail = (from npd in _objectRMCDataContext.NursePDADetails
                                                               where objectGenericNurseIDs.Contains(npd.NurseID)
                                                               select npd).ToList();

                                if (objectGenericNursePDADetail.Count > 0)
                                {
                                    foreach (RMC.DataService.NursePDAInfo objectNursePDAInfo in objectGenericNursePDAInfo)
                                    {
                                        objectGenericNurseIDs.Add(objectNursePDAInfo.NurseID);
                                    }

                                    _objectRMCDataContext.NursePDADetails.DeleteAllOnSubmit(objectGenericNursePDADetail);
                                }
                            }

                            _objectRMCDataContext.NursePDAInfos.DeleteAllOnSubmit(objectGenericNursePDAInfo);
                        }

                        _objectRMCDataContext.HospitalUploads.DeleteAllOnSubmit(objectGenericHospitalUpload);
                    }
                }

                _objectRMCDataContext.SubmitChanges();

                foreach (string filePath in objectGenericFilePath)
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + "\\Uploads\\" + filePath;
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }

                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteNursePDAInfo");
                ex.Data.Add("Class", "BSNursePDADetail");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return flag;
        }

        public void UpdateIsCollapseYearField(int hospitalUnitID, string year, bool IsCollapse, string userRole)
        {
            try
            {
                List<RMC.DataService.NursePDAInfo> objectGenericNursePDAInfo = null;
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                objectGenericNursePDAInfo = _objectRMCDataContext.NursePDAInfos.Where(h => h.HospitalDemographicID == hospitalUnitID && h.Year.Trim() == year.Trim()).ToList();
                if (userRole.ToLower().Trim() == "superadmin")
                {
                    objectGenericNursePDAInfo.ForEach(delegate(RMC.DataService.NursePDAInfo objectNursePDAInfo)
                    {
                        objectNursePDAInfo.IsAdminCollapseYear = IsCollapse;
                    });
                }
                else
                {
                    objectGenericNursePDAInfo.ForEach(delegate(RMC.DataService.NursePDAInfo objectNursePDAInfo)
                    {
                        objectNursePDAInfo.IsCollapseYear = IsCollapse;
                    });
                }

                _objectRMCDataContext.SubmitChanges();
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

        public void UpdateIsCollapseMonthField(int hospitalUnitID, string year, string month, bool IsCollapse, string userRole)
        {
            try
            {
                List<RMC.DataService.NursePDAInfo> objectGenericNursePDAInfo = null;
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                objectGenericNursePDAInfo = _objectRMCDataContext.NursePDAInfos.Where(h => h.HospitalDemographicID == hospitalUnitID && h.Year.Trim() == year.Trim() && h.Month.Trim() == month.Trim()).ToList();
                if (userRole.ToLower().Trim() == "superadmin")
                {
                    objectGenericNursePDAInfo.ForEach(delegate(RMC.DataService.NursePDAInfo objectNursePDAInfo)
                    {
                        objectNursePDAInfo.IsAdminCollapseMonth = IsCollapse;
                    });
                }
                else
                {
                    objectGenericNursePDAInfo.ForEach(delegate(RMC.DataService.NursePDAInfo objectNursePDAInfo)
                    {
                        objectNursePDAInfo.IsCollapseMonth = IsCollapse;
                    });
                }

                _objectRMCDataContext.SubmitChanges();
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

        public void UpdateNursePDAInfoFields(int nurseID, string nurseName, string configName, int patientsPerNurse)
        {
            try
            {
                RMC.DataService.NursePDAInfo objectNursePDAInfo = null;
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                objectNursePDAInfo = _objectRMCDataContext.NursePDAInfos.Single(x => x.NurseID == nurseID);

                objectNursePDAInfo.NurseName = nurseName;
                objectNursePDAInfo.ConfigName = configName;
                objectNursePDAInfo.PatientsPerNurse = patientsPerNurse;
                objectNursePDAInfo.IsErrorInConfigName = false;
                objectNursePDAInfo.IsErrorInNurseName = false;
                objectNursePDAInfo.IsErrorInPatientsPerNurse = false;
                objectNursePDAInfo.IsErrorExist = false;

                _objectRMCDataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
