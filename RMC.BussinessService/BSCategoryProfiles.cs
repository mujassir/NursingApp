using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSCategoryProfiles
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        //Data Service Object.
        RMC.DataService.ProfileType objectProfileType = null;

        #endregion

        #region Methods

        /// <summary>
        /// Get Profile to show in Category profile page for particular user.
        /// </summary>
        /// <param name="userID">Login User</param>
        /// <param name="profileTypeID">Profile Type of Category Profile</param>
        /// <param name="noOfSkipRecords">Use for paging, number of records skip according to paging</param>
        /// <param name="noOfRecords">Use for paging, to show number of records.</param>
        /// <returns>Bussiness Entity BECategoryProfile List.</returns>
        public List<RMC.BusinessEntities.BECategoryProfile> GetCategoryProfileByUserID(int userID, int profileTypeID, int noOfSkipRecords, int noOfRecords, string sortExpression=" ",string sortOrder=" ")
        {
            try
            {
                List<RMC.BusinessEntities.BECategoryProfile> objectGenericBECategoryProfile;
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                objectGenericBECategoryProfile = (from cu in _objectRMCDataContext.CategoryProfiles
                                                  where cu.ProfileTypeID == profileTypeID && cu.IsDeleted == false
                                                  orderby cu.CategoryProfileID 
                                                  select new RMC.BusinessEntities.BECategoryProfile
                                                  {
                                                      //ActivityID = cu.ActivityID,
                                                      ValidationID = cu.ValidationID,
                                                      CategoryAssignmentID = cu.CategoryAssignmentID.Value,
                                                      CategoryProfileID = cu.CategoryProfileID,
                                                      CategoryProfileName = cu.CategoryProfileName,
                                                      CreatedBy = cu.CreatedBy,
                                                      CreatedDate = cu.CreatedDate,
                                                      LocationID = cu.Validation.LocationID.Value,
                                                      SubActivityID = cu.Validation.SubActivityID.Value,
                                                      Location = (from l in _objectRMCDataContext.Locations
                                                                  where l.IsActive == true && l.LocationID == cu.Validation.LocationID
                                                                  select l).FirstOrDefault().Location1,
                                                      Activity = (from a in _objectRMCDataContext.Activities
                                                                  where a.IsActive == true && a.ActivityID == cu.Validation.ActivityID
                                                                  select a.Activity1).FirstOrDefault(),
                                                      SubActivity = (from sa in _objectRMCDataContext.SubActivities
                                                                     where sa.IsActive == true && sa.SubActivityID == cu.Validation.SubActivityID
                                                                     select sa.SubActivity1).FirstOrDefault(),
                                                      CategoryAssignmentName = (cu.ProfileType.Type.ToLower().Trim() == "value added" ?
                                                                                (from can in _objectRMCDataContext.ValueAddedTypes
                                                                                 where can.TypeID == cu.CategoryAssignmentID
                                                                                 select can.TypeName).FirstOrDefault() :
                                                                                 (cu.ProfileType.Type.ToLower().Trim() == "others" ?
                                                                                  (from can in _objectRMCDataContext.CategoryGroups
                                                                                   where can.CategoryGroupID == cu.CategoryAssignmentID
                                                                                   select can.CategoryGroup1).FirstOrDefault() :
                                                                                   (from can in _objectRMCDataContext.LocationCategories
                                                                                    where can.LocationID == cu.CategoryAssignmentID
                                                                                    select can.LocationCategory1).FirstOrDefault()))

                                                  }).Skip(noOfSkipRecords).Take(noOfRecords).ToList<RMC.BusinessEntities.BECategoryProfile>();

                if (System.Web.HttpContext.Current.User.IsInRole("superadmin"))
                {
                    //objectGenericBECategoryProfile = (from pu in _objectRMCDataContext.ProfileUsers
                    //                                  where pu.UserID == userID 
                    //                                  select new RMC.BusinessEntities.BECategoryProfile
                    //                                  {
                    //                                      ActivityID = pu.CategoryProfile.ActivityID,
                    //                                      CategoryAssignmentID = pu.CategoryProfile.CategoryAssignmentID,
                    //                                      CategoryProfileID = pu.CategoryProfile.CategoryProfileID
                    //                                  }).ToList<RMC.BusinessEntities.BECategoryProfile>();
                }

                switch(sortExpression)
                {
                    case "Location":
                        if(sortOrder == "ASC")
                            return objectGenericBECategoryProfile.OrderBy(p => p.Location).ToList();
                        else
                            return objectGenericBECategoryProfile.OrderByDescending(p => p.Location).ToList();
                    case "Activity":
                        if (sortOrder == "ASC")
                            return objectGenericBECategoryProfile.OrderBy(p => p.Activity).ToList();
                        else
                            return objectGenericBECategoryProfile.OrderByDescending(p => p.Activity).ToList();
                    case "SubActivity":
                        if (sortOrder == "ASC")
                            return objectGenericBECategoryProfile.OrderBy(p => p.SubActivity).ToList();
                        else
                            return objectGenericBECategoryProfile.OrderByDescending(p => p.SubActivity).ToList();
                    case "Category":
                        if (sortOrder == "ASC")
                            return objectGenericBECategoryProfile.OrderBy(p => p.CategoryAssignmentName).ToList();
                        else
                            return objectGenericBECategoryProfile.OrderByDescending(p => p.CategoryAssignmentName).ToList();
                    case "SortAll":
                        if (sortOrder == "ASC")
                            return objectGenericBECategoryProfile.OrderBy(p => p.Location).ThenBy(p => p.Activity).ThenBy(p => p.SubActivity).ThenBy(p => p.CategoryAssignmentName).ToList();
                        else
                            return objectGenericBECategoryProfile.OrderByDescending(p => p.Location).ThenByDescending(p => p.Activity).ThenByDescending(p => p.SubActivity).ThenByDescending(p => p.CategoryAssignmentName).ToList();
                    default:
                        return objectGenericBECategoryProfile;
                }

               // return objectGenericBECategoryProfile;

            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetCategoryProfileByUserID");
                ex.Data.Add("Class", "BSCategoryProfiles");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }
        
        /// <summary>
        /// Get Profile to show in Category profile page for particular user.
        /// </summary>
        /// <param name="userID">Login User</param>
        /// <param name="profileTypeID">Profile Type of Category Profile</param>
        /// <param name="validationIDs">Skip number of records</param>
        /// <param name="noOfSkipRecords">Use for paging, number of records skip according to paging</param>
        /// <param name="noOfRecords">Use for paging, to show number of records.</param>
        /// <returns>Bussiness Entity BECategoryProfile List.</returns>
        public List<RMC.BusinessEntities.BECategoryProfile> GetCategoryProfileByUserID(int userID, int profileTypeID, int[] validationIDs, int noOfSkipRecords, int noOfRecords)
        {
            try
            {
                List<RMC.BusinessEntities.BECategoryProfile> objectGenericBECategoryProfile;
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                objectGenericBECategoryProfile = (from cu in _objectRMCDataContext.CategoryProfiles
                                                  where cu.ProfileTypeID == profileTypeID && cu.IsDeleted == false && validationIDs.Contains(cu.ValidationID) == false
                                                  orderby cu.CategoryProfileID 
                                                  select new RMC.BusinessEntities.BECategoryProfile
                                                  {
                                                      //ActivityID = cu.ActivityID,
                                                      ValidationID = cu.ValidationID,
                                                      CategoryAssignmentID = cu.CategoryAssignmentID.Value,
                                                      CategoryProfileID = cu.CategoryProfileID,
                                                      CategoryProfileName = cu.CategoryProfileName,
                                                      CreatedBy = cu.CreatedBy,
                                                      CreatedDate = cu.CreatedDate,
                                                      LocationID = cu.Validation.LocationID.Value,
                                                      SubActivityID = cu.Validation.SubActivityID.Value,
                                                      Location = (from l in _objectRMCDataContext.Locations
                                                                  where l.IsActive == true && l.LocationID == cu.Validation.LocationID
                                                                  select l).FirstOrDefault().Location1,
                                                      Activity = (from a in _objectRMCDataContext.Activities
                                                                  where a.IsActive == true && a.ActivityID == cu.Validation.ActivityID
                                                                  select a.Activity1).FirstOrDefault(),
                                                      SubActivity = (from sa in _objectRMCDataContext.SubActivities
                                                                     where sa.IsActive == true && sa.SubActivityID == cu.Validation.SubActivityID
                                                                     select sa.SubActivity1).FirstOrDefault(),
                                                      CategoryAssignmentName = (cu.ProfileType.Type.ToLower().Trim() == "value added" ?
                                                                                (from can in _objectRMCDataContext.ValueAddedTypes
                                                                                 where can.TypeID == cu.CategoryAssignmentID
                                                                                 select can.TypeName).FirstOrDefault() :
                                                                                 (cu.ProfileType.Type.ToLower().Trim() == "others" ?
                                                                                  (from can in _objectRMCDataContext.CategoryGroups
                                                                                   where can.CategoryGroupID == cu.CategoryAssignmentID
                                                                                   select can.CategoryGroup1).FirstOrDefault() :
                                                                                   (from can in _objectRMCDataContext.LocationCategories
                                                                                    where can.LocationID == cu.CategoryAssignmentID
                                                                                    select can.LocationCategory1).FirstOrDefault()))

                                                  }).Skip(noOfSkipRecords).Take(noOfRecords).ToList<RMC.BusinessEntities.BECategoryProfile>();
                               
                return objectGenericBECategoryProfile;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetCategoryProfileByUserID");
                ex.Data.Add("Class", "BSCategoryProfiles");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Count Category Profile for specific user and profile type.
        /// </summary>
        /// <param name="userID">Login User</param>
        /// <param name="profileTypeID">Profile Type of Category Profile.</param>
        /// <returns>Number of records</returns>
        public int CountCategoryProfileByUserID(int userID, int profileTypeID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                int Count = (from cu in _objectRMCDataContext.CategoryProfiles
                             where cu.ProfileTypeID == profileTypeID && cu.IsDeleted == false
                             select cu).Count();

                return Count;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetCategoryProfileByUserID");
                ex.Data.Add("Class", "BSCategoryProfiles");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Count Category Profile for specific user and profile type.
        /// </summary>
        /// <param name="userID">Login User</param>
        /// <param name="profileTypeID">Profile Type of Category Profile.</param>
        /// <param name="validationIDs">Skip number of records</param>
        /// <returns>Number of records</returns>
        public int CountCategoryProfileByUserID(int userID, int profileTypeID, int[] validationIDs)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                int Count = (from cu in _objectRMCDataContext.CategoryProfiles
                             where cu.ProfileTypeID == profileTypeID && cu.IsDeleted == false && validationIDs.Contains(cu.ValidationID) == false
                             select cu).Count();

                return Count;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetCategoryProfileByUserID");
                ex.Data.Add("Class", "BSCategoryProfiles");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Upadate Category Profile in a profile Detail.
        /// </summary>
        /// <param name="objectBECategoryProfile">Bussiness Entity BECategoryProfile single object</param>
        public void UpdateCategoryProfile(RMC.BusinessEntities.BECategoryProfile objectBECategoryProfile)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                RMC.DataService.CategoryProfile objectCategoryProfile = (from cp in _objectRMCDataContext.CategoryProfiles
                                                                         where cp.CategoryProfileID == objectBECategoryProfile.CategoryProfileID
                                                                         select cp).FirstOrDefault();

                if (objectCategoryProfile != null)
                {
                    //objectCategoryProfile.ActivityID = objectBECategoryProfile.ActivityID;
                    objectCategoryProfile.CategoryAssignmentID = objectBECategoryProfile.CategoryAssignmentID;
                    //objectCategoryProfile.LocationID = objectBECategoryProfile.LocationID;
                    //objectCategoryProfile.SubActivityID = objectBECategoryProfile.SubActivityID;
                }

                _objectRMCDataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdateCategoryProfileByUserID");
                ex.Data.Add("Class", "BSCategoryProfiles");
                throw ex;
            }
        }

        /// <summary>
        /// Update Category Proflie records in a bulk.
        /// </summary>
        /// <param name="objectGenericCategoryProfileIDs">List of Category Profile ID's</param>
        /// <param name="objectGenericBECategoryProfile">List of BECategoryProfile for update</param>
        public void UpdateCategoryProfile(List<int> objectGenericCategoryProfileIDs, List<RMC.BusinessEntities.BECategoryProfile> objectGenericBECategoryProfile)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.DataService.CategoryProfile> objectGenericCategoryProfile = (from cp in _objectRMCDataContext.CategoryProfiles
                                                                                      where objectGenericCategoryProfileIDs.Contains(cp.CategoryProfileID)
                                                                                      select cp).ToList();

                if (objectGenericCategoryProfile.Count > 0)
                {
                    objectGenericCategoryProfile.ForEach(delegate(RMC.DataService.CategoryProfile objectCategoryProfile)
                    {
                        RMC.BusinessEntities.BECategoryProfile objectNewBECategoryProfile = objectGenericBECategoryProfile.Find(delegate(RMC.BusinessEntities.BECategoryProfile objectBECategoryProfile)
                        {
                            return objectBECategoryProfile.CategoryProfileID == objectCategoryProfile.CategoryProfileID;
                        });

                        if (objectNewBECategoryProfile != null)
                            objectCategoryProfile.CategoryAssignmentID = objectNewBECategoryProfile.CategoryAssignmentID;
                    });
                }

                _objectRMCDataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdateCategoryProfile");
                ex.Data.Add("Class", "BSCategoryProfiles");
                throw ex;
            }
        }

        /// <summary>
        /// Insert records in ProfileType, ProfileUser and CategoryProfile tables.
        /// Method Used in CreateNewProfile.ascx
        /// </summary>
        /// <param name="objectProfileType">Profile Type in a Database.</param>
        /// <param name="objectProfileUser">Profile User in a Database.</param>
        /// <param name="objectGenericCategoryProfile">List of CategoryProfile in a database.</param>
        /// <returns>boolean type</returns>
        public bool InsertRecordsCreateNewProfile(RMC.DataService.ProfileType objectProfileType, RMC.DataService.ProfileUser objectProfileUser, List<RMC.DataService.CategoryProfile> objectGenericCategoryProfile)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                objectProfileType.ProfileUsers.Add(objectProfileUser);
                objectProfileType.CategoryProfiles.AddRange(objectGenericCategoryProfile);
                _objectRMCDataContext.ProfileTypes.InsertOnSubmit(objectProfileType);
                _objectRMCDataContext.SubmitChanges();
                flag = true;

                return flag;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdateCategoryProfileByUserID");
                ex.Data.Add("Class", "BSCategoryProfiles");
                throw ex;
            }
        }

        /// <summary>
        /// Delete records from ProfileType, ProfileUser and CategoryProfile tables.
        /// Method Used in 
        /// </summary>
        /// <param name="profileTypeID">Profile type of Category Profile.</param>
        /// <returns>boolean</returns>
        public bool DeleteCategoryProfile(int profileTypeID)
        {
            bool Flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                RMC.DataService.ProfileType objectGenericProfileType = new RMC.DataService.ProfileType();
                RMC.DataService.ProfileUser objectGenericProfileUser = new RMC.DataService.ProfileUser();
                List<RMC.DataService.CategoryProfile> objectGenericCategoryProfile = new List<RMC.DataService.CategoryProfile>();

                objectGenericProfileType = (from p in _objectRMCDataContext.ProfileTypes
                                            where p.ProfileTypeID == profileTypeID
                                            select p).FirstOrDefault();
                objectGenericProfileUser = (from p in _objectRMCDataContext.ProfileUsers
                                            where p.ProfileTypeID == profileTypeID
                                            select p).FirstOrDefault();
                objectGenericCategoryProfile = (from p in _objectRMCDataContext.CategoryProfiles
                                                where p.ProfileTypeID == profileTypeID
                                                select p).ToList<RMC.DataService.CategoryProfile>();

                if (objectGenericProfileType != null)
                {
                    _objectRMCDataContext.CategoryProfiles.DeleteAllOnSubmit(objectGenericCategoryProfile);
                    _objectRMCDataContext.ProfileUsers.DeleteOnSubmit(objectGenericProfileUser);
                    _objectRMCDataContext.ProfileTypes.DeleteOnSubmit(objectGenericProfileType);
                    _objectRMCDataContext.SubmitChanges();
                    Flag = true;
                }
                return Flag;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdateCategoryProfileByUserID");
                ex.Data.Add("Class", "BSCategoryProfiles");
                throw ex;
            }
        }

        /// <summary>
        /// Delete single record of CategoryProfile.
        /// </summary>
        /// <param name="objectBECategoryProfile">Bussiness Entity BECategoryProfile</param>
        /// <returns>boolean type</returns>
        public bool DeleteCategoryProfileByCategoryProfileID(RMC.BusinessEntities.BECategoryProfile objectBECategoryProfile)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                RMC.DataService.CategoryProfile objectCategoryProfile = (from cp in _objectRMCDataContext.CategoryProfiles
                                                                         where cp.CategoryProfileID == objectBECategoryProfile.CategoryProfileID
                                                                         select cp).FirstOrDefault();
                if (objectCategoryProfile != null)
                {
                    _objectRMCDataContext.CategoryProfiles.DeleteOnSubmit(objectCategoryProfile);
                    _objectRMCDataContext.SubmitChanges();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteCategoryProfileByCategoryProfileID");
                ex.Data.Add("Class", "BSCategoryProfiles");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return flag;
        }

        /// <summary>
        /// Delete multiple record in a Category Profile.
        /// </summary>
        /// <param name="objectGenericCategoryProfileIDs">List of Category Profiles.</param>
        public void DeleteCategoryProfileByCategoryProfileID(List<int> objectGenericCategoryProfileIDs)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.DataService.CategoryProfile> objectGenericCategoryProfile = (from cp in _objectRMCDataContext.CategoryProfiles
                                                                               where objectGenericCategoryProfileIDs.Contains(cp.CategoryProfileID)
                                                                               select cp).ToList();
                if (objectGenericCategoryProfile.Count > 0)
                {
                    objectGenericCategoryProfile.ForEach(delegate(RMC.DataService.CategoryProfile objectCategoryProfile)
                    {
                        _objectRMCDataContext.CategoryProfiles.DeleteOnSubmit(objectCategoryProfile);
                    });
                    _objectRMCDataContext.SubmitChanges();                    
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteCategoryProfileByCategoryProfileID");
                ex.Data.Add("Class", "BSCategoryProfiles");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        #endregion

    }
    //End of Class
}
//End of Namespace