using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.Common;
using System.Data;
using System.IO;

namespace RMC.BussinessService
{
    public class BSValidationData
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        //Generic List of Data Entity Object.
        List<RMC.BusinessEntities.BECategoryProfile> _objectGenericBECategoryProfile = null;

        //Fundamental Data Types.
        int _locationID, _activityID, _subActivityID;

        #endregion

        #region Public Methods
         
        /// <summary>
        /// Use in ValidationData.ascx
        /// </summary>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BECategoryProfile> GetValidationData(string sortExpression = " ", string sortOrder = " ")
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                _objectGenericBECategoryProfile = (from v in _objectRMCDataContext.Validations
                                                   select new RMC.BusinessEntities.BECategoryProfile
                                                   {
                                                       ValidationID = v.ValidationID,
                                                       Activity = (from a in _objectRMCDataContext.Activities
                                                                   where a.ActivityID == v.ActivityID && a.IsActive == true
                                                                   select a.Activity1).FirstOrDefault(),
                                                       ActivityID = v.ActivityID.Value,
                                                       Location = (from l in _objectRMCDataContext.Locations
                                                                   where l.LocationID == v.LocationID && l.IsActive == true
                                                                   select l.Location1).FirstOrDefault(),
                                                       LocationID = v.LocationID.Value,
                                                       SubActivity = (from sa in _objectRMCDataContext.SubActivities
                                                                      where sa.SubActivityID == v.SubActivityID && sa.IsActive == true
                                                                      select sa.SubActivity1).FirstOrDefault(),
                                                       SubActivityID = v.SubActivityID.Value
                                                   }).ToList();

                if (_objectGenericBECategoryProfile.Count == 0)
                {
                    RMC.BusinessEntities.BECategoryProfile objectBECategoryProfile = new RMC.BusinessEntities.BECategoryProfile();

                    objectBECategoryProfile.Activity = string.Empty;
                    objectBECategoryProfile.ActivityID = 0;
                    objectBECategoryProfile.Location = string.Empty;
                    objectBECategoryProfile.LocationID = 0;
                    objectBECategoryProfile.SubActivity = string.Empty;
                    objectBECategoryProfile.SubActivityID = 0;

                    _objectGenericBECategoryProfile.Add(objectBECategoryProfile);
                }

                switch (sortExpression)
                {
                    case "Location":
                        if (sortOrder == "ASC")
                            return _objectGenericBECategoryProfile.OrderBy(p => p.Location).ToList();
                        else
                            return _objectGenericBECategoryProfile.OrderByDescending(p => p.Location).ToList();
                    case "Activity":
                        if (sortOrder == "ASC")
                            return _objectGenericBECategoryProfile.OrderBy(p => p.Activity).ToList();
                        else
                            return _objectGenericBECategoryProfile.OrderByDescending(p => p.Activity).ToList();
                    case "SubActivity":
                        if (sortOrder == "ASC")
                            return _objectGenericBECategoryProfile.OrderBy(p => p.SubActivity).ToList();
                        else
                            return _objectGenericBECategoryProfile.OrderByDescending(p => p.SubActivity).ToList();
                    case "SortAll":
                        if (sortOrder == "ASC")
                            return _objectGenericBECategoryProfile.OrderBy(p => p.Location).ThenBy(p => p.Activity).ThenBy(p => p.SubActivity).ToList();
                        else
                            return _objectGenericBECategoryProfile.OrderByDescending(p => p.Location).ThenByDescending(p => p.Activity).ThenByDescending(p => p.SubActivity).ToList();
                    default:
                        return _objectGenericBECategoryProfile;
                }
                //return _objectGenericBECategoryProfile;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetValidationData");
                ex.Data.Add("Class", "BSValidationData");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public List<RMC.BusinessEntities.BECategoryProfile> GetValidationData(int[] validationIDs)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                _objectGenericBECategoryProfile = (from v in _objectRMCDataContext.Validations
                                                   where validationIDs.Contains(v.ValidationID) == false
                                                   select new RMC.BusinessEntities.BECategoryProfile
                                                   {
                                                       ValidationID = v.ValidationID,
                                                       Activity = (from a in _objectRMCDataContext.Activities
                                                                   where a.ActivityID == v.ActivityID && a.IsActive == true
                                                                   select a.Activity1).FirstOrDefault(),
                                                       ActivityID = v.ActivityID.Value,
                                                       Location = (from l in _objectRMCDataContext.Locations
                                                                   where l.LocationID == v.LocationID && l.IsActive == true
                                                                   select l.Location1).FirstOrDefault(),
                                                       LocationID = v.LocationID.Value,
                                                       SubActivity = (from sa in _objectRMCDataContext.SubActivities
                                                                      where sa.SubActivityID == v.SubActivityID && sa.IsActive == true
                                                                      select sa.SubActivity1).FirstOrDefault(),
                                                       SubActivityID = v.SubActivityID.Value
                                                   }).ToList();

                if (_objectGenericBECategoryProfile.Count == 0)
                {
                    RMC.BusinessEntities.BECategoryProfile objectBECategoryProfile = new RMC.BusinessEntities.BECategoryProfile();

                    objectBECategoryProfile.Activity = string.Empty;
                    objectBECategoryProfile.ActivityID = 0;
                    objectBECategoryProfile.Location = string.Empty;
                    objectBECategoryProfile.LocationID = 0;
                    objectBECategoryProfile.SubActivity = string.Empty;
                    objectBECategoryProfile.SubActivityID = 0;

                    _objectGenericBECategoryProfile.Add(objectBECategoryProfile);
                }

                return _objectGenericBECategoryProfile;
                
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetValidationData");
                ex.Data.Add("Class", "BSValidationData");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public List<RMC.BusinessEntities.BECategoryProfile> GetValidationData(int noOfSkipRecords, int noOfRecords)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                _objectGenericBECategoryProfile = (from v in _objectRMCDataContext.Validations
                                                   select new RMC.BusinessEntities.BECategoryProfile
                                                   {
                                                       ValidationID = v.ValidationID,
                                                       Activity = (from a in _objectRMCDataContext.Activities
                                                                   where a.ActivityID == v.ActivityID && a.IsActive == true
                                                                   select a.Activity1).FirstOrDefault(),
                                                       ActivityID = v.ActivityID.Value,
                                                       Location = (from l in _objectRMCDataContext.Locations
                                                                   where l.LocationID == v.LocationID && l.IsActive == true
                                                                   select l.Location1).FirstOrDefault(),
                                                       LocationID = v.LocationID.Value,
                                                       SubActivity = (from sa in _objectRMCDataContext.SubActivities
                                                                      where sa.SubActivityID == v.SubActivityID && sa.IsActive == true
                                                                      select sa.SubActivity1).FirstOrDefault(),
                                                       SubActivityID = v.SubActivityID.Value
                                                   }).Skip(noOfSkipRecords).Take(noOfRecords).ToList();

                if (_objectGenericBECategoryProfile.Count == 0)
                {
                    RMC.BusinessEntities.BECategoryProfile objectBECategoryProfile = new RMC.BusinessEntities.BECategoryProfile();

                    objectBECategoryProfile.Activity = string.Empty;
                    objectBECategoryProfile.ActivityID = 0;
                    objectBECategoryProfile.Location = string.Empty;
                    objectBECategoryProfile.LocationID = 0;
                    objectBECategoryProfile.SubActivity = string.Empty;
                    objectBECategoryProfile.SubActivityID = 0;

                    _objectGenericBECategoryProfile.Add(objectBECategoryProfile);
                }

                return _objectGenericBECategoryProfile;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetValidationData");
                ex.Data.Add("Class", "BSValidationData");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public List<RMC.BusinessEntities.BECategoryProfile> GetValidationData(int[] validationIDs, int noOfSkipRecords, int noOfRecords)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                _objectGenericBECategoryProfile = (from v in _objectRMCDataContext.Validations
                                                   where validationIDs.Contains(v.ValidationID) == false
                                                   select new RMC.BusinessEntities.BECategoryProfile
                                                   {
                                                       ValidationID = v.ValidationID,
                                                       Activity = (from a in _objectRMCDataContext.Activities
                                                                   where a.ActivityID == v.ActivityID && a.IsActive == true
                                                                   select a.Activity1).FirstOrDefault(),
                                                       ActivityID = v.ActivityID.Value,
                                                       Location = (from l in _objectRMCDataContext.Locations
                                                                   where l.LocationID == v.LocationID && l.IsActive == true
                                                                   select l.Location1).FirstOrDefault(),
                                                       LocationID = v.LocationID.Value,
                                                       SubActivity = (from sa in _objectRMCDataContext.SubActivities
                                                                      where sa.SubActivityID == v.SubActivityID && sa.IsActive == true
                                                                      select sa.SubActivity1).FirstOrDefault(),
                                                       SubActivityID = v.SubActivityID.Value
                                                   }).Skip(noOfSkipRecords).Take(noOfRecords).ToList();

                if (_objectGenericBECategoryProfile.Count == 0)
                {
                    RMC.BusinessEntities.BECategoryProfile objectBECategoryProfile = new RMC.BusinessEntities.BECategoryProfile();

                    objectBECategoryProfile.Activity = string.Empty;
                    objectBECategoryProfile.ActivityID = 0;
                    objectBECategoryProfile.Location = string.Empty;
                    objectBECategoryProfile.LocationID = 0;
                    objectBECategoryProfile.SubActivity = string.Empty;
                    objectBECategoryProfile.SubActivityID = 0;

                    _objectGenericBECategoryProfile.Add(objectBECategoryProfile);
                }

                return _objectGenericBECategoryProfile;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetValidationData");
                ex.Data.Add("Class", "BSValidationData");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public int CountValidationData()
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                int Count = (from v in _objectRMCDataContext.Validations
                                                   select v).Count();
                
                return Count;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "CountValidationData");
                ex.Data.Add("Class", "BSValidationData");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public int CountValidationData(int[] validationIDs)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                int Count = (from v in _objectRMCDataContext.Validations
                             where validationIDs.Contains(v.ValidationID) == false
                             select v).Count();

                return Count;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "CountValidationData");
                ex.Data.Add("Class", "BSValidationData");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }


        /// <summary>
        /// Use in ValidationData.ascx
        /// </summary>
        /// <returns></returns>
        public void InsertValidationData(RMC.BusinessEntities.BECategoryProfile objectBECategoryProfile)
        {
            try
            {
                RMC.DataService.Location objectLocation = new RMC.DataService.Location();
                RMC.DataService.Activity objectActivity = new RMC.DataService.Activity();
                RMC.DataService.SubActivity objectSubActivity = new RMC.DataService.SubActivity();
                RMC.DataService.Validation objectValidation = new RMC.DataService.Validation();
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                _locationID = (from l in _objectRMCDataContext.Locations
                               where l.Location1.ToLower().Trim() == objectBECategoryProfile.Location.ToLower().Trim() && l.IsActive == true
                               select l.LocationID).FirstOrDefault();
                if (_locationID == 0)
                {
                    objectLocation.Location1 = objectBECategoryProfile.Location;
                    objectLocation.IsActive = true;
                    objectLocation.RenameLocation = string.Empty;
                    _objectRMCDataContext.Locations.InsertOnSubmit(objectLocation);
                }
                else
                {
                    objectLocation.LocationID = _locationID;
                }

                if (objectBECategoryProfile.Activity != string.Empty)
                {
                    _activityID = (from a in _objectRMCDataContext.Activities
                                   where a.Activity1.ToLower().Trim() == objectBECategoryProfile.Activity.ToLower().Trim() && a.IsActive == true
                                   select a.ActivityID).FirstOrDefault();
                    if (_activityID == 0)
                    {
                        objectActivity.Activity1 = objectBECategoryProfile.Activity;
                        objectActivity.IsActive = true;
                        _objectRMCDataContext.Activities.InsertOnSubmit(objectActivity);
                    }
                    else
                    {
                        objectActivity.ActivityID = _activityID;
                    }
                }

                if (objectBECategoryProfile.Activity != string.Empty && objectBECategoryProfile.SubActivity != string.Empty)
                {
                    _subActivityID = (from sa in _objectRMCDataContext.SubActivities
                                      where sa.SubActivity1.ToLower().Trim() == objectBECategoryProfile.SubActivity.ToLower().Trim() && sa.IsActive == true
                                      select sa.SubActivityID).FirstOrDefault();
                    if (_subActivityID == 0)
                    {
                        objectSubActivity.SubActivity1 = objectBECategoryProfile.SubActivity;
                        objectSubActivity.IsActive = true;
                        _objectRMCDataContext.SubActivities.InsertOnSubmit(objectSubActivity);
                    }
                    else
                    {
                        objectSubActivity.SubActivityID = _subActivityID;
                    }
                }

                _objectRMCDataContext.SubmitChanges();
                _locationID = objectLocation.LocationID;
                if (objectActivity.ActivityID > 0)
                {
                    _activityID = objectActivity.ActivityID;
                }
                else
                {
                    _activityID = 0;
                }

                if (objectSubActivity.SubActivityID > 0)
                {
                    _subActivityID = objectSubActivity.SubActivityID;
                }
                else
                {
                    _subActivityID = 0;
                }

                _objectRMCDataContext.Dispose();
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                objectValidation = new RMC.DataService.Validation();
                objectValidation.ActivityID = _activityID;
                objectValidation.CreatedBy = objectBECategoryProfile.CreatedBy;
                objectValidation.CreatedDate = objectBECategoryProfile.CreatedDate;
                objectValidation.LocationID = _locationID;
                objectValidation.SubActivityID = _subActivityID;

                _objectRMCDataContext.Validations.InsertOnSubmit(objectValidation);
                _objectRMCDataContext.SubmitChanges();
                _objectRMCDataContext.Dispose();

                InsertInCategoryProfile(objectValidation.ValidationID);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetValidationData");
                ex.Data.Add("Class", "BSValidationData");
                throw ex;
            }
        }

        /// <summary>
        /// Use in ValidationData.ascx
        /// </summary>
        /// <returns></returns>
        public bool UpdateValidationData(RMC.DataService.Validation objectValidation)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                RMC.DataService.Validation objectNewValidation = (from v in _objectRMCDataContext.Validations
                                                                  where v.ValidationID == objectValidation.ValidationID
                                                                  select v).FirstOrDefault();

                if (objectNewValidation != null)
                {
                    objectNewValidation.ActivityID = objectValidation.ActivityID;
                    objectNewValidation.CreatedBy = objectValidation.CreatedBy;
                    objectNewValidation.CreatedDate = objectValidation.CreatedDate;
                    objectNewValidation.LocationID = objectValidation.LocationID;
                    objectNewValidation.SubActivityID = objectValidation.SubActivityID;

                    _objectRMCDataContext.SubmitChanges();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdateValidationData");
                ex.Data.Add("Class", "BSValidationData");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return flag;
        }

        /// <summary>
        /// Use in ValidationData.ascx
        /// </summary>
        /// <returns></returns>
        public bool UpdateEditValidationData(RMC.DataService.Location objectLoc, RMC.DataService.Activity objectAct, RMC.DataService.SubActivity objectSubAct)
        {
            bool flag = false, flagLocation = false, flagActivity = false, flagSubActivity = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                RMC.DataService.Location objectLocation = (from v in _objectRMCDataContext.Locations
                                                           where v.LocationID == objectLoc.LocationID && v.IsActive == true
                                                           select v).FirstOrDefault();

                RMC.DataService.Activity objectActivity = (from v in _objectRMCDataContext.Activities
                                                           where v.ActivityID == objectAct.ActivityID && v.IsActive == true
                                                           select v).FirstOrDefault();

                RMC.DataService.SubActivity objectSubActivity = (from v in _objectRMCDataContext.SubActivities
                                                                 where v.SubActivityID == objectSubAct.SubActivityID && v.IsActive == true
                                                                 select v).FirstOrDefault();

                if (objectLocation != null)
                {
                    objectLocation.LocationID = Convert.ToInt32(objectLoc.LocationID);
                    objectLocation.Location1 = objectLoc.Location1;
                    flagLocation = true;
                }

                if (objectActivity != null)
                {
                    objectActivity.ActivityID = Convert.ToInt32(objectAct.ActivityID);
                    objectActivity.Activity1 = objectAct.Activity1;
                    flagActivity = true;
                }

                if (objectSubActivity != null)
                {
                    objectSubActivity.SubActivityID = Convert.ToInt32(objectSubAct.SubActivityID);
                    objectSubActivity.SubActivity1 = objectSubAct.SubActivity1;
                    flagSubActivity = true;
                }

                if (flagLocation == true || flagActivity == true || flagSubActivity == true)
                {
                    _objectRMCDataContext.SubmitChanges();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdateEditValidationData");
                ex.Data.Add("Class", "BSValidationData");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return flag;
        }

        /// <summary>
        /// Use in ValidationData.ascx
        /// </summary>
        /// <returns></returns>
        public void DeleteValidationData(int validationID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                RMC.DataService.Validation objectNewValidation = (from v in _objectRMCDataContext.Validations
                                                                  where v.ValidationID == validationID
                                                                  select v).FirstOrDefault();
                if (objectNewValidation != null)
                {
                    List<RMC.DataService.CategoryProfile> objectGenericCategoryProfile = (from cp in _objectRMCDataContext.CategoryProfiles
                                                                                          where cp.ValidationID == objectNewValidation.ValidationID
                                                                                          select cp).ToList();

                    _objectRMCDataContext.CategoryProfiles.DeleteAllOnSubmit(objectGenericCategoryProfile);
                }
                _objectRMCDataContext.Validations.DeleteOnSubmit(objectNewValidation);
                _objectRMCDataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteValidationData");
                ex.Data.Add("Class", "BSValidationData");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public void DeleteValidationData()
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.DataService.Validation> objectGenericNewValidation = (from v in _objectRMCDataContext.Validations
                                                                               select v).ToList();
                if (objectGenericNewValidation.Count > 0)
                {
                    List<RMC.DataService.CategoryProfile> objectGenericCategoryProfile = null;
                    List<RMC.DataService.ProfileUser> objectGenericProfileUser = null;
                    List<RMC.DataService.ProfileType> objectGenericProfileType = null;
                    objectGenericCategoryProfile = (from cp in _objectRMCDataContext.CategoryProfiles                                                    
                                                    select cp).ToList();

                    if (objectGenericCategoryProfile.Count > 0)
                    {
                        _objectRMCDataContext.CategoryProfiles.DeleteAllOnSubmit(objectGenericCategoryProfile);
                    }

                    objectGenericProfileUser = (from pu in _objectRMCDataContext.ProfileUsers
                                                select pu).ToList();
                    
                    if (objectGenericProfileUser.Count > 0)
                    {
                        _objectRMCDataContext.ProfileUsers.DeleteAllOnSubmit(objectGenericProfileUser);
                    }

                    objectGenericProfileType = (from pt in _objectRMCDataContext.ProfileTypes
                                                select pt).ToList();

                    if (objectGenericProfileUser.Count > 0)
                    {
                        _objectRMCDataContext.ProfileTypes.DeleteAllOnSubmit(objectGenericProfileType);
                    }

                }
                _objectRMCDataContext.Validations.DeleteAllOnSubmit(objectGenericNewValidation);
                _objectRMCDataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteValidationData");
                ex.Data.Add("Class", "BSValidationData");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableValidation"></param>
        /// <returns></returns>
        public bool InsertValidationDataFromTable(DataTable TableValidation)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                if (TableValidation.Rows.Count > 0)
                {
                    List<RMC.DataService.Location> objectGenericLocation = new List<RMC.DataService.Location>();
                    List<RMC.DataService.Activity> objectGenericAcitivity = new List<RMC.DataService.Activity>();
                    List<RMC.DataService.SubActivity> objectGenericSubActivity = new List<RMC.DataService.SubActivity>();
                    List<RMC.DataService.Validation> objectGenericValidation = new List<RMC.DataService.Validation>();

                    objectGenericLocation = (from l in _objectRMCDataContext.Locations
                                             where l.IsActive == true
                                             select l).ToList();
                    objectGenericAcitivity = (from a in _objectRMCDataContext.Activities
                                              where a.IsActive == true
                                              select a).ToList();
                    objectGenericSubActivity = (from a in _objectRMCDataContext.SubActivities
                                                where a.IsActive == true
                                                select a).ToList();

                    foreach (DataRow dr in TableValidation.Rows)
                    {
                        RMC.DataService.Validation objectValidation = new RMC.DataService.Validation();
                        RMC.DataService.Location objectLocation = null;
                        RMC.DataService.Activity objectActivity = null;
                        RMC.DataService.SubActivity objectSubActivity = null;

                        objectLocation = objectGenericLocation.Find(delegate(RMC.DataService.Location objectLoc)
                                        {
                                            return objectLoc.Location1.ToLower().Trim() == dr[0].ToString().ToLower().Trim();
                                        });
                        if (objectLocation != null)
                        {
                            objectValidation.LocationID = objectLocation.LocationID;
                        }
                        else
                        {
                            if (dr[0] != DBNull.Value || dr[0].ToString() != string.Empty)
                            {
                                BSLocation objectBSLocation = new BSLocation();
                                objectValidation.LocationID = objectBSLocation.InsertLocation(dr[0].ToString());
                                objectGenericLocation = (from l in _objectRMCDataContext.Locations
                                                         where l.IsActive == true
                                                         select l).ToList();
                            }
                            else
                            {
                                continue;
                            }
                        }
                        string str;
                        objectActivity = objectGenericAcitivity.Find(delegate(RMC.DataService.Activity objectAct)
                        {
                            str = dr[1].ToString().ToLower().Trim();
                            return objectAct.Activity1.ToLower().Trim() == StringCorrection(dr[1].ToString().ToLower().Trim());
                        });
                        if (objectActivity != null)
                        {
                            objectValidation.ActivityID = objectActivity.ActivityID;
                        }
                        else
                        {
                            if (dr[1] != DBNull.Value || dr[1].ToString() != string.Empty)
                            {
                                BSActivity objectBSActivity = new BSActivity();
                                objectValidation.ActivityID = objectBSActivity.InsertActivity(dr[1].ToString());
                                objectGenericAcitivity = (from a in _objectRMCDataContext.Activities
                                                          where a.IsActive == true
                                                          select a).ToList();
                            }
                            else
                            {
                                objectValidation.ActivityID = 0;
                            }
                        }

                        objectSubActivity = objectGenericSubActivity.Find(delegate(RMC.DataService.SubActivity objectSubAct)
                        {
                            return objectSubAct.SubActivity1.ToLower().Trim() == dr[2].ToString().ToLower().Trim();
                        });
                        if (objectSubActivity != null)
                        {
                            objectValidation.SubActivityID = objectSubActivity.SubActivityID;
                        }
                        else
                        {
                            if (dr[2] != DBNull.Value || dr[2].ToString() != string.Empty)
                            {
                                BSSubActivity objectBSubActivity = new BSSubActivity();
                                objectValidation.SubActivityID = objectBSubActivity.InsertSubActivity(dr[2].ToString());
                                objectGenericSubActivity = (from a in _objectRMCDataContext.SubActivities
                                                            where a.IsActive == true
                                                            select a).ToList();
                            }
                            else
                            {
                                objectValidation.SubActivityID = 0;
                            }
                        }

                        objectValidation.CreatedBy = "Super Admin";
                        objectValidation.CreatedDate = DateTime.Now;

                        objectGenericValidation.Add(objectValidation);
                    }

                    _objectRMCDataContext.Validations.InsertAllOnSubmit(objectGenericValidation);
                    _objectRMCDataContext.SubmitChanges();
                    
                    //added to insert data into category profile table while importing from excel file
                    foreach (DataService.Validation val in objectGenericValidation)
                    {
                        InsertInCategoryProfile(val.ValidationID);
                    }

                    flag = true;
                }

                return flag;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertValidationDataFromTable");
                ex.Data.Add("Class", "BSValidationData");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locationID"></param>
        /// <param name="activityID"></param>
        /// <param name="subActivityID"></param>
        /// <param name="isErrorInLocation"></param>
        /// <param name="isErrorInActivity"></param>
        /// <param name="isErrorInSubActivity"></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEValidation> GetLocationsFromValidationTable(int locationID, int activityID, int subActivityID, bool isErrorInLocation, bool isErrorInActivity, bool isErrorInSubActivity, bool isErrorExist, bool isActiveError)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation = null;

                objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                                             select new RMC.BusinessEntities.BEValidation
                                             {
                                                 LocationID = Convert.ToInt32(v.LocationID),
                                                 LocationName = (from l in _objectRMCDataContext.Locations
                                                                 where l.LocationID == Convert.ToInt32(v.LocationID)
                                                                 select l).FirstOrDefault().Location1
                                             }).Distinct().ToList();
                //if (isErrorInLocation == false)
                //{
                //    if (isErrorInActivity == false)
                //    {
                //        if (isErrorInSubActivity == false)
                //        {
                //            if (isErrorExist && isActiveError == false)
                //            {
                //                objectGenericBEValidation = GetLocationFromValidation();
                //            }
                //        }
                //        else
                //        {
                //            objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                //                                         where v.LocationID == locationID && v.ActivityID == activityID
                //                                         select new RMC.BusinessEntities.BEValidation
                //                                     {
                //                                         LocationID = Convert.ToInt32(v.LocationID),
                //                                         LocationName = (from l in _objectRMCDataContext.Locations
                //                                                         where l.LocationID == Convert.ToInt32(v.LocationID)
                //                                                         select l).FirstOrDefault().Location1
                //                                     }).Distinct().ToList();
                //        }
                //    }
                //    else
                //    {
                //        if (isErrorInSubActivity == false)
                //        {
                //            objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                //                                         where v.LocationID == locationID && v.SubActivityID == subActivityID
                //                                         select new RMC.BusinessEntities.BEValidation
                //                                         {
                //                                             LocationID = Convert.ToInt32(v.LocationID),
                //                                             LocationName = (from l in _objectRMCDataContext.Locations
                //                                                             where l.LocationID == Convert.ToInt32(v.LocationID)
                //                                                             select l).FirstOrDefault().Location1
                //                                         }).Distinct().ToList();
                //        }
                //        else
                //        {
                //            objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                //                                         where v.LocationID == locationID
                //                                         select new RMC.BusinessEntities.BEValidation
                //                                         {
                //                                             LocationID = Convert.ToInt32(v.LocationID),
                //                                             LocationName = (from l in _objectRMCDataContext.Locations
                //                                                             where l.LocationID == Convert.ToInt32(v.LocationID)
                //                                                             select l).FirstOrDefault().Location1
                //                                         }).Distinct().ToList();
                //        }
                //    }
                //}
                //else
                //{
                //    if (isErrorInActivity == false)
                //    {
                //        if (isErrorInSubActivity == false)
                //        {
                //            objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                //                                         where v.ActivityID == activityID && v.SubActivityID == subActivityID
                //                                         select new RMC.BusinessEntities.BEValidation
                //                                     {
                //                                         LocationID = Convert.ToInt32(v.LocationID),
                //                                         LocationName = (from l in _objectRMCDataContext.Locations
                //                                                         where l.LocationID == Convert.ToInt32(v.LocationID)
                //                                                         select l).FirstOrDefault().Location1
                //                                     }).Distinct().ToList();
                //        }
                //        else
                //        {
                //            objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                //                                         where v.ActivityID == activityID
                //                                         select new RMC.BusinessEntities.BEValidation
                //                                     {
                //                                         LocationID = Convert.ToInt32(v.LocationID),
                //                                         LocationName = (from l in _objectRMCDataContext.Locations
                //                                                         where l.LocationID == Convert.ToInt32(v.LocationID)
                //                                                         select l).FirstOrDefault().Location1
                //                                     }).Distinct().ToList();
                //        }
                //    }
                //    else
                //    {
                //        //if (isErrorInSubActivity == false)
                //        //{
                //        objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                //                                     select new RMC.BusinessEntities.BEValidation
                //                                 {
                //                                     LocationID = Convert.ToInt32(v.LocationID),
                //                                     LocationName = (from l in _objectRMCDataContext.Locations
                //                                                     where l.LocationID == Convert.ToInt32(v.LocationID)
                //                                                     select l).FirstOrDefault().Location1
                //                                 }).Distinct().ToList();
                //        //}
                //        //else
                //        //{
                //        //    objectGenericLocation = (from v in _objectRMCDataContext.Validations
                //        //                             select new RMC.DataService.Location
                //        //                             {
                //        //                                 LocationID = Convert.ToInt32(v.LocationID),
                //        //                                 Location1 = (from l in _objectRMCDataContext.Locations
                //        //                                              where l.LocationID == Convert.ToInt32(v.LocationID)
                //        //                                              select l).FirstOrDefault().Location1
                //        //                             }).ToList();
                //        //}
                //    }
                //}

                return objectGenericBEValidation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetLocationsFromValidationTable");
                ex.Data.Add("Class", "BSValidationData");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locationID"></param>
        /// <param name="activityID"></param>
        /// <param name="subActivityID"></param>
        /// <param name="isErrorInLocation"></param>
        /// <param name="isErrorInActivity"></param>
        /// <param name="isErrorInSubActivity"></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEValidation> GetActivitiesFromValidationTable(int locationID, int activityID, int subActivityID, bool isErrorInLocation, bool isErrorInActivity, bool isErrorInSubActivity, bool isErrorExist, bool isActiveError)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation = null;
                if (locationID > 0)
                {
                    objectGenericBEValidation = GetActivityFromValidationByLocationID(locationID);
                }
                else
                {
                    if (isErrorInActivity == false)
                    {
                        if (isErrorInLocation == false)
                        {
                            if (isErrorInSubActivity == false)
                            {
                                if (isErrorExist && isActiveError == false)
                                {
                                    objectGenericBEValidation = GetActivityFromValidationByLocationID(locationID);
                                }
                            }
                            else
                            {
                                objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                                                             where v.LocationID == locationID && v.ActivityID == activityID
                                                             select new RMC.BusinessEntities.BEValidation
                                                         {
                                                             ActivityID = Convert.ToInt32(v.ActivityID),
                                                             ActivityName = (from a in _objectRMCDataContext.Activities
                                                                             where a.ActivityID == Convert.ToInt32(v.ActivityID)
                                                                             select a).FirstOrDefault().Activity1
                                                         }).Distinct().ToList();
                            }
                        }
                        else
                        {
                            if (isErrorInSubActivity == false)
                            {
                                objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                                                             where v.ActivityID == activityID && v.SubActivityID == subActivityID
                                                             select new RMC.BusinessEntities.BEValidation
                                                         {
                                                             ActivityID = Convert.ToInt32(v.ActivityID),
                                                             ActivityName = (from a in _objectRMCDataContext.Activities
                                                                             where a.ActivityID == Convert.ToInt32(v.ActivityID)
                                                                             select a).FirstOrDefault().Activity1
                                                         }).Distinct().ToList();
                            }
                            else
                            {
                                objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                                                             where v.ActivityID == activityID
                                                             select new RMC.BusinessEntities.BEValidation
                                                         {
                                                             ActivityID = Convert.ToInt32(v.ActivityID),
                                                             ActivityName = (from a in _objectRMCDataContext.Activities
                                                                             where a.ActivityID == Convert.ToInt32(v.ActivityID)
                                                                             select a).FirstOrDefault().Activity1
                                                         }).Distinct().ToList();
                            }
                        }
                    }
                    else
                    {
                        if (isErrorInLocation == false)
                        {
                            if (isErrorInSubActivity == false)
                            {
                                objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                                                             where v.LocationID == locationID && v.SubActivityID == subActivityID
                                                             select new RMC.BusinessEntities.BEValidation
                                                         {
                                                             ActivityID = Convert.ToInt32(v.ActivityID),
                                                             ActivityName = (from a in _objectRMCDataContext.Activities
                                                                             where a.ActivityID == Convert.ToInt32(v.ActivityID)
                                                                             select a).FirstOrDefault().Activity1
                                                         }).Distinct().ToList();
                            }
                            else
                            {
                                objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                                                             where v.LocationID == locationID
                                                             select new RMC.BusinessEntities.BEValidation
                                                         {
                                                             ActivityID = Convert.ToInt32(v.ActivityID),
                                                             ActivityName = (from a in _objectRMCDataContext.Activities
                                                                             where a.ActivityID == Convert.ToInt32(v.ActivityID)
                                                                             select a).FirstOrDefault().Activity1
                                                         }).Distinct().ToList();
                            }
                        }
                        else
                        {
                            if (isErrorInSubActivity == false)
                            {
                                objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                                                             where v.SubActivityID == subActivityID
                                                             select new RMC.BusinessEntities.BEValidation
                                                         {
                                                             ActivityID = Convert.ToInt32(v.ActivityID),
                                                             ActivityName = (from a in _objectRMCDataContext.Activities
                                                                             where a.ActivityID == Convert.ToInt32(v.ActivityID)
                                                                             select a).FirstOrDefault().Activity1
                                                         }).ToList();
                            }
                            else
                            {
                                objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                                                             select new RMC.BusinessEntities.BEValidation
                                                         {
                                                             ActivityID = Convert.ToInt32(v.ActivityID),
                                                             ActivityName = (from a in _objectRMCDataContext.Activities
                                                                             where a.ActivityID == Convert.ToInt32(v.ActivityID)
                                                                             select a).FirstOrDefault().Activity1
                                                         }).ToList();
                            }
                        }
                    }
                }
                return objectGenericBEValidation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetActivitiesFromValidationTable");
                ex.Data.Add("Class", "BSValidationData");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subActivityID"></param>
        /// <param name="isErrorInSubActivity"></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEValidation> GetSubActivitiesFromValidationTable(int locationID, int activityID, int subActivityID, bool isErrorInLocation, bool isErrorInActivity, bool isErrorInSubActivity, bool isErrorExist, bool isActiveError)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation = null;

                if (isErrorInSubActivity == false)
                {
                    if (isErrorInActivity == false)
                    {
                        if (isErrorInLocation == false)
                        {
                            if (isErrorExist && isActiveError == false)
                            {
                                objectGenericBEValidation = GetSubActivityFromValidationByActivityID(activityID);
                            }
                        }
                        else
                        {
                            objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                                                         where v.ActivityID == activityID && v.SubActivityID == subActivityID
                                                         select new RMC.BusinessEntities.BEValidation
                                                        {
                                                            SubActivityID = Convert.ToInt32(v.SubActivityID),
                                                            SubActivityName = (from a in _objectRMCDataContext.SubActivities
                                                                               where a.SubActivityID == Convert.ToInt32(v.SubActivityID)
                                                                               select a).FirstOrDefault().SubActivity1
                                                        }).Distinct().ToList();
                        }
                    }
                    else
                    {
                        if (isErrorInLocation == false)
                        {
                            objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                                                         where v.LocationID == locationID && v.SubActivityID == subActivityID
                                                         select new RMC.BusinessEntities.BEValidation
                                                        {
                                                            SubActivityID = Convert.ToInt32(v.SubActivityID),
                                                            SubActivityName = (from a in _objectRMCDataContext.SubActivities
                                                                               where a.SubActivityID == Convert.ToInt32(v.SubActivityID)
                                                                               select a).FirstOrDefault().SubActivity1
                                                        }).Distinct().ToList();
                        }
                        else
                        {
                            objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                                                         where v.SubActivityID == subActivityID
                                                         select new RMC.BusinessEntities.BEValidation
                                                        {
                                                            SubActivityID = Convert.ToInt32(v.SubActivityID),
                                                            SubActivityName = (from a in _objectRMCDataContext.SubActivities
                                                                               where a.SubActivityID == Convert.ToInt32(v.SubActivityID)
                                                                               select a).FirstOrDefault().SubActivity1
                                                        }).Distinct().ToList();
                        }
                    }
                }
                else
                {
                    if (isErrorInActivity == false)
                    {
                        if (isErrorInLocation == false)
                        {
                            objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                                                         where v.ActivityID == activityID && v.LocationID == locationID
                                                         select new RMC.BusinessEntities.BEValidation
                                                        {
                                                            SubActivityID = Convert.ToInt32(v.SubActivityID),
                                                            SubActivityName = (from a in _objectRMCDataContext.SubActivities
                                                                               where a.SubActivityID == Convert.ToInt32(v.SubActivityID)
                                                                               select a).FirstOrDefault().SubActivity1
                                                        }).Distinct().ToList();
                        }
                        else
                        {
                            objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                                                         where v.ActivityID == activityID
                                                         select new RMC.BusinessEntities.BEValidation
                                                        {
                                                            SubActivityID = Convert.ToInt32(v.SubActivityID),
                                                            SubActivityName = (from a in _objectRMCDataContext.SubActivities
                                                                               where a.SubActivityID == Convert.ToInt32(v.SubActivityID)
                                                                               select a).FirstOrDefault().SubActivity1
                                                        }).Distinct().ToList();
                        }
                    }
                    else
                    {
                        if (isErrorInLocation == false)
                        {
                            objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                                                         where v.LocationID == locationID
                                                         select new RMC.BusinessEntities.BEValidation
                                                        {
                                                            SubActivityID = Convert.ToInt32(v.SubActivityID),
                                                            SubActivityName = (from a in _objectRMCDataContext.SubActivities
                                                                               where a.SubActivityID == Convert.ToInt32(v.SubActivityID)
                                                                               select a).FirstOrDefault().SubActivity1
                                                        }).Distinct().ToList();
                        }
                        else
                        {
                            objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                                                         select new RMC.BusinessEntities.BEValidation
                                                        {
                                                            SubActivityID = Convert.ToInt32(v.SubActivityID),
                                                            SubActivityName = (from a in _objectRMCDataContext.SubActivities
                                                                               where a.SubActivityID == Convert.ToInt32(v.SubActivityID)
                                                                               select a).FirstOrDefault().SubActivity1
                                                        }).Distinct().ToList();
                        }
                    }
                }

                return objectGenericBEValidation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetSubActivitiesFromValidationTable");
                ex.Data.Add("Class", "BSValidationData");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public List<RMC.BusinessEntities.BEValidation> GetLocationFromValidation()
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation = null;

                objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                                             select new RMC.BusinessEntities.BEValidation
                                             {
                                                 LocationID = Convert.ToInt32(v.LocationID),
                                                 LocationName = (from l in _objectRMCDataContext.Locations
                                                                 where l.LocationID == Convert.ToInt32(v.LocationID)
                                                                 select l).FirstOrDefault().Location1
                                             }).Distinct().ToList();
                return objectGenericBEValidation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetLocationFromValidationByLocationID");
                ex.Data.Add("Class", "BSValidationData");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public List<RMC.BusinessEntities.BEValidation> GetActivityFromValidationByLocationID(int locationID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation = null;

                objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                                             where v.LocationID == locationID
                                             select new RMC.BusinessEntities.BEValidation
                                             {
                                                 ActivityID = Convert.ToInt32(v.ActivityID),
                                                 ActivityName = (from a in _objectRMCDataContext.Activities
                                                                 where a.ActivityID == Convert.ToInt32(v.ActivityID)
                                                                 select a).FirstOrDefault().Activity1
                                             }).Distinct().ToList();
                return objectGenericBEValidation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetActivityFromValidationByLocationID");
                ex.Data.Add("Class", "BSValidationData");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public List<RMC.BusinessEntities.BEValidation> GetSubActivityFromValidationByActivityID(int activityID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BEValidation> objectGenericBEValidation = null;

                objectGenericBEValidation = (from v in _objectRMCDataContext.Validations
                                             where v.ActivityID == activityID
                                             select new RMC.BusinessEntities.BEValidation
                                             {
                                                 SubActivityID = Convert.ToInt32(v.SubActivityID),
                                                 SubActivityName = (from sa in _objectRMCDataContext.SubActivities
                                                                    where sa.SubActivityID == Convert.ToInt32(v.SubActivityID)
                                                                    select sa).FirstOrDefault().SubActivity1
                                             }).Distinct().ToList();
                return objectGenericBEValidation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetSubActivityFromValidationByActivityID");
                ex.Data.Add("Class", "BSValidationData");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        #endregion

        #region Private Methods

        private void InsertInCategoryProfile(int validationID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.DataService.CategoryProfile> objectGenericCategoryProfile = new List<RMC.DataService.CategoryProfile>();

                //RMC.DataService.UserInfo objectUserInfo = ((List<RMC.DataService.UserInfo>)HttpContext.Current.Session["UserInformation"])[0];
                RMC.DataService.UserInfo objectUserInfo = ((List<RMC.DataService.UserInfo>)(BSSerialization.Deserialize<List<RMC.DataService.UserInfo>>(HttpContext.Current.Session["UserInformation"] as MemoryStream)))[0];

                List<RMC.DataService.ProfileType> objectGenericProfileType = (from pt in _objectRMCDataContext.ProfileTypes
                                                                              select pt).ToList();

                foreach (RMC.DataService.ProfileType objectProfileType in objectGenericProfileType)
                {
                    RMC.DataService.CategoryProfile objectCategoryProfile = new RMC.DataService.CategoryProfile();

                    objectCategoryProfile.CategoryAssignmentID = 0;
                    objectCategoryProfile.CategoryProfileName = objectProfileType.ProfileName;
                    objectCategoryProfile.CreatedBy = objectUserInfo.FirstName + " " + objectUserInfo.LastName;
                    objectCategoryProfile.CreatedDate = DateTime.Now;
                    objectCategoryProfile.ProfileTypeID = objectProfileType.ProfileTypeID;
                    objectCategoryProfile.ValidationID = validationID;
                    objectCategoryProfile.IsDeleted = false;

                    objectGenericCategoryProfile.Add(objectCategoryProfile);
                }

                _objectRMCDataContext.CategoryProfiles.InsertAllOnSubmit(objectGenericCategoryProfile);
                _objectRMCDataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertInCategoryProfile");
                ex.Data.Add("Class", "BSValidationData");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        private string StringCorrection(string keyword)
        {
            string wordString = keyword;
            if ("hunting for…" == keyword)
            {
                wordString = keyword.Substring(0, 11);
                wordString += "...";
            }

            return wordString;
        }

        #endregion

    }
}
