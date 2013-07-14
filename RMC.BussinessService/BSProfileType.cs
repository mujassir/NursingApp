using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace RMC.BussinessService
{
    public class BSProfileType
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        //Generic List of Data Service Object.
        List<RMC.DataService.ProfileType> _objectGenericProfileType = null;
        List<RMC.DataService.ProfileUser> _objectGenericProfileUser = null;

        #endregion

        #region Methods

        //public List<RMC.DataService.ValueAddedType> GetValueAddedType()
        //{
        //    try
        //    {
        //        _objectRMCDataContext = new RMC.DataService.RMCDataContext();

        //        List<RMC.DataService.ValueAddedType> objectGenericValueAddedType = null;
        //        objectGenericValueAddedType = (from vat in _objectRMCDataContext.ValueAddedTypes
        //                                       select vat).ToList<RMC.DataService.ValueAddedType>();

        //        return objectGenericValueAddedType;

        //    }
        //    catch (Exception ex)
        //    {
        //        //ex.Data.Add("Function", "GetValueAddedType");
        //        //ex.Data.Add("Class", "BSProfileType")
        //        throw ex;
        //    }
        //    finally
        //    {
        //        _objectRMCDataContext.Dispose();
        //    }
        //}

        public List<RMC.BusinessEntities.BECategoryType> GetValueAddedType(string valuetype)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BECategoryType> objectGenericBEProfileCategory = null;
                if (valuetype == "0")
                {
                    objectGenericBEProfileCategory = (from vat in _objectRMCDataContext.ValueAddedTypes
                                                      where vat.IsActive == true
                                                      select new RMC.BusinessEntities.BECategoryType
                                                      {
                                                          ProfileCategory = vat.TypeName,
                                                          ProfileCategoryID = vat.TypeID
                                                      }).ToList();
                }
                else if (valuetype == "1")
                {
                    objectGenericBEProfileCategory = (from cg in _objectRMCDataContext.CategoryGroups
                                                      where cg.IsActive == true
                                                      select new RMC.BusinessEntities.BECategoryType
                                                      {
                                                          ProfileCategory = cg.CategoryGroup1,
                                                          ProfileCategoryID = cg.CategoryGroupID
                                                      }).ToList();
                }
                else if (valuetype == "3")
                {
                    objectGenericBEProfileCategory = (from ac in _objectRMCDataContext.ActivitiesCategories
                                                      
                                                      select new RMC.BusinessEntities.BECategoryType
                                                      {
                                                          ProfileCategory = ac.ActivitiesCategory1,
                                                          ProfileCategoryID = ac.ActivitiesID
                                                      }).ToList();
                }
                else
                {
                    objectGenericBEProfileCategory = (from lc in _objectRMCDataContext.LocationCategories
                                                      select new RMC.BusinessEntities.BECategoryType
                                                      {
                                                          ProfileCategory = lc.LocationCategory1,
                                                          ProfileCategoryID = lc.LocationID
                                                      }).ToList();
                }

                return objectGenericBEProfileCategory;
            }
            catch (Exception ex)
            {
                //ex.Data.Add("Function", "GetValueAddedType");
                //ex.Data.Add("Class", "BSProfileType")
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }
        public List<RMC.DataService.ProfileType> GetProfileTypeValueAdded()
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.DataService.ProfileType> _objectGenericProfileType = (from gpt in _objectRMCDataContext.ProfileUsers
                                                                               where gpt.IsShared == true && gpt.ProfileType.Type.ToLower().Trim() == "value added"
                                                                               orderby gpt.ProfileType.ProfileName
                                                                               select gpt.ProfileType).ToList<RMC.DataService.ProfileType>();
                return _objectGenericProfileType;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetProfileTypes");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }
        public List<RMC.DataService.ProfileType> GetProfileTypeOthers()
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.DataService.ProfileType> _objectGenericProfileType = (from gpt in _objectRMCDataContext.ProfileUsers
                                                                               where gpt.IsShared == true && gpt.ProfileType.Type.ToLower().Trim() == "others"
                                                                               orderby gpt.ProfileType.ProfileName
                                                                               select gpt.ProfileType).ToList<RMC.DataService.ProfileType>();
                return _objectGenericProfileType;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetProfileTypes");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }
        public List<RMC.DataService.ProfileType> GetProfileTypeLocation()
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.DataService.ProfileType> _objectGenericProfileType = (from gpt in _objectRMCDataContext.ProfileUsers
                                                                               where gpt.IsShared == true && gpt.ProfileType.Type.ToLower().Trim() == "location"
                                                                               orderby gpt.ProfileType.ProfileName
                                                                               select gpt.ProfileType).ToList<RMC.DataService.ProfileType>();
                return _objectGenericProfileType;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetProfileTypes");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public bool InsertValueAddedType(RMC.DataService.ValueAddedType objectValueAddedType)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _objectRMCDataContext.ValueAddedTypes.InsertOnSubmit(objectValueAddedType);
                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertValueAddedType");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
            return flag;
        }
        public bool InsertCategoryGroup(RMC.DataService.CategoryGroup objectCategoryGroup)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _objectRMCDataContext.CategoryGroups.InsertOnSubmit(objectCategoryGroup);
                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertCategoryGroup");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
            return flag;
        }
        public bool InsertActivitiesCategoryGroup(RMC.DataService.ActivitiesCategory objectActivitiesCategoryGroup)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _objectRMCDataContext.ActivitiesCategories.InsertOnSubmit(objectActivitiesCategoryGroup);
                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertActivitiesCategoryGroup");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
            return flag;
        }
        public bool InsertLocationCategory(RMC.DataService.LocationCategory objectLocationCategory)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _objectRMCDataContext.LocationCategories.InsertOnSubmit(objectLocationCategory);
                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertLocationCategory");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
            return flag;
        }

        public bool DeleteValueAddedType(int ProfileTypeId)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                var ProfileType = (from pt in _objectRMCDataContext.ValueAddedTypes
                                   where pt.TypeID == ProfileTypeId
                                   select pt).FirstOrDefault();

                var NationalDatabase = (from nd in _objectRMCDataContext.NationalDatabases
                                        where nd.TypeValueID == ProfileTypeId && nd.Type == "Value Added"
                                        select nd).ToList();

                RMC.DataService.ValueAddedType objectValueAddedType = _objectRMCDataContext.ValueAddedTypes.FirstOrDefault(f => f.TypeID != 0);

                List<RMC.DataService.CategoryProfile> objectGenericCategoryProfile = (from pc in _objectRMCDataContext.CategoryProfiles
                                                                                      where pc.ProfileType.Type.ToLower().Trim() == "value added" && pc.CategoryAssignmentID == ProfileTypeId
                                                                                      select pc).ToList();
                if (objectGenericCategoryProfile.Count > 0)
                {
                    objectGenericCategoryProfile.ForEach(delegate(RMC.DataService.CategoryProfile objectCategoryProfile)
                    {
                        objectCategoryProfile.CategoryAssignmentID = objectValueAddedType.TypeID;
                    });
                }

                _objectRMCDataContext.NationalDatabases.DeleteAllOnSubmit(NationalDatabase);
                _objectRMCDataContext.ValueAddedTypes.DeleteOnSubmit(ProfileType);
                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteValueAddedType");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
            return flag;
        }
        public bool DeleteCategoryGroup(int ProfileTypeId)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                var ProfileType = (from pt in _objectRMCDataContext.CategoryGroups
                                   where pt.CategoryGroupID == ProfileTypeId
                                   select pt).FirstOrDefault();
                var NationalDatabase = (from nd in _objectRMCDataContext.NationalDatabases
                                        where nd.TypeValueID == ProfileTypeId && nd.Type == "Others"
                                        select nd).ToList();

                RMC.DataService.CategoryGroup objectCategoryGroup = _objectRMCDataContext.CategoryGroups.FirstOrDefault(f => f.CategoryGroupID != 0);

                List<RMC.DataService.CategoryProfile> objectGenericCategoryProfile = (from pc in _objectRMCDataContext.CategoryProfiles
                                                                                      where pc.ProfileType.Type.ToLower().Trim() == "others" && pc.CategoryAssignmentID == ProfileTypeId
                                                                                      select pc).ToList();
                if (objectGenericCategoryProfile.Count > 0)
                {
                    objectGenericCategoryProfile.ForEach(delegate(RMC.DataService.CategoryProfile objectCategoryProfile)
                    {
                        objectCategoryProfile.CategoryAssignmentID = objectCategoryGroup.CategoryGroupID;
                    });
                }

                _objectRMCDataContext.NationalDatabases.DeleteAllOnSubmit(NationalDatabase);
                _objectRMCDataContext.CategoryGroups.DeleteOnSubmit(ProfileType);
                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteCategoryGroup");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
            return flag;
        }
        public bool DeleteLocationCategory(int ProfileTypeId)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                var ProfileType = (from lc in _objectRMCDataContext.LocationCategories
                                   where lc.LocationID == ProfileTypeId
                                   select lc).FirstOrDefault();
                var NationalDatabase = (from nd in _objectRMCDataContext.NationalDatabases
                                        where nd.TypeValueID == ProfileTypeId && nd.Type == "Location"
                                        select nd).ToList();

                RMC.DataService.LocationCategory objectLocationCategory = _objectRMCDataContext.LocationCategories.FirstOrDefault(f=>f.LocationID != 0); 

                List<RMC.DataService.CategoryProfile> objectGenericCategoryProfile = (from pc in _objectRMCDataContext.CategoryProfiles
                                                                                      where pc.ProfileType.Type.ToLower().Trim() == "location" && pc.CategoryAssignmentID == ProfileTypeId
                                                                                      select pc).ToList();

                if (objectGenericCategoryProfile.Count > 0)
                {
                    objectGenericCategoryProfile.ForEach(delegate(RMC.DataService.CategoryProfile objectCategoryProfile)
                    {
                        objectCategoryProfile.CategoryAssignmentID = objectLocationCategory.LocationID;
                    });
                }

                _objectRMCDataContext.NationalDatabases.DeleteAllOnSubmit(NationalDatabase);
                _objectRMCDataContext.LocationCategories.DeleteOnSubmit(ProfileType);
                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteLocationCategory");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
            return flag;
        }
        public bool DeleteActivitiesCategory(int ProfileTypeId)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                var ProfileType = (from ac in _objectRMCDataContext.ActivitiesCategories
                                   where ac.ActivitiesID == ProfileTypeId
                                   select ac).FirstOrDefault();
                var NationalDatabase = (from nd in _objectRMCDataContext.NationalDatabases
                                        where nd.TypeValueID == ProfileTypeId && nd.Type == "activities"
                                        select nd).ToList();

                RMC.DataService.ActivitiesCategory objectActivitiesCategory = _objectRMCDataContext.ActivitiesCategories.FirstOrDefault(f => f.ActivitiesID != 0);

                List<RMC.DataService.CategoryProfile> objectGenericCategoryProfile = (from pc in _objectRMCDataContext.CategoryProfiles
                                                                                      where pc.ProfileType.Type.ToLower().Trim() == "activities" && pc.CategoryAssignmentID == ProfileTypeId
                                                                                      select pc).ToList();

                if (objectGenericCategoryProfile.Count > 0)
                {
                    objectGenericCategoryProfile.ForEach(delegate(RMC.DataService.CategoryProfile objectCategoryProfile)
                    {
                        objectCategoryProfile.CategoryAssignmentID = objectActivitiesCategory.ActivitiesID;
                    });
                }

                _objectRMCDataContext.NationalDatabases.DeleteAllOnSubmit(NationalDatabase);
                _objectRMCDataContext.ActivitiesCategories.DeleteOnSubmit(ProfileType);
                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteLocationCategory");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
            return flag;
        }
        public bool UpdateValueAddedType(RMC.DataService.ValueAddedType ValueAddedType)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                RMC.DataService.ValueAddedType objectValueAddedType = null;

                objectValueAddedType = (from pt in _objectRMCDataContext.ValueAddedTypes
                                        where pt.TypeID == ValueAddedType.TypeID
                                        select pt).FirstOrDefault();

                objectValueAddedType.TypeName = ValueAddedType.TypeName;
                objectValueAddedType.Abbreviation = ValueAddedType.Abbreviation;
                objectValueAddedType.IsActive = ValueAddedType.IsActive;
                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdateValueAddedType");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
            return flag;
        }
        public bool UpdateCategoryGroup(RMC.DataService.CategoryGroup CategoryGroup)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                RMC.DataService.CategoryGroup objectCategoryGroup = null;

                objectCategoryGroup = (from pt in _objectRMCDataContext.CategoryGroups
                                       where pt.CategoryGroupID == CategoryGroup.CategoryGroupID
                                       select pt).FirstOrDefault();

                objectCategoryGroup.CategoryGroup1 = CategoryGroup.CategoryGroup1;
                objectCategoryGroup.IsActive = CategoryGroup.IsActive;
                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdateCategoryGroup");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
            return flag;
        }
        public bool UpdateActivitiesCategoryGroup(RMC.DataService.ActivitiesCategory ActivitiesCategoryGroup)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                RMC.DataService.ActivitiesCategory objectCategoryGroup = null;

                objectCategoryGroup = (from pt in _objectRMCDataContext.ActivitiesCategories
                                       where pt.ActivitiesID == ActivitiesCategoryGroup.ActivitiesID
                                       select pt).FirstOrDefault();

                objectCategoryGroup.ActivitiesCategory1 = ActivitiesCategoryGroup.ActivitiesCategory1;                
                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdateActivitiesCategoryGroup");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
            return flag;
        }
        public bool UpdateLocationCategory(RMC.DataService.LocationCategory LocationCategory)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                RMC.DataService.LocationCategory objectLocationCategory = null;

                objectLocationCategory = (from lc in _objectRMCDataContext.LocationCategories
                                          where lc.LocationID == LocationCategory.LocationID
                                          select lc).FirstOrDefault();

                objectLocationCategory.LocationCategory1 = LocationCategory.LocationCategory1;
                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdateLocationCategory");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
            return flag;
        }

        public List<RMC.DataService.ProfileType> GetProfileTypes(int userID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    _objectGenericProfileType = (from gpt in _objectRMCDataContext.ProfileUsers
                                                 select gpt.ProfileType).ToList<RMC.DataService.ProfileType>();
                }
                else
                {
                    _objectGenericProfileType = (from gpt in _objectRMCDataContext.ProfileUsers
                                                 where gpt.UserID == userID || gpt.IsShared == true
                                                 select gpt.ProfileType).ToList<RMC.DataService.ProfileType>();
                }
                return _objectGenericProfileType;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetProfileTypes");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public List<RMC.DataService.ProfileType> GetAllProfileTypes()
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.DataService.ProfileType> _objectGenericProfileType = (from gpt in _objectRMCDataContext.ProfileTypes
                                                                               orderby gpt.ProfileName
                                                                               select gpt).ToList<RMC.DataService.ProfileType>();
                return _objectGenericProfileType;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetProfileTypes");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public List<RMC.DataService.ProfileType> GetProfileNameByUserID(int userID, int profileTypeID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    _objectGenericProfileType = (from gpt in _objectRMCDataContext.ProfileUsers
                                                 select gpt.ProfileType).ToList<RMC.DataService.ProfileType>();
                }
                else
                {
                    _objectGenericProfileType = (from gpt in _objectRMCDataContext.ProfileUsers
                                                 where gpt.UserID == userID || gpt.ProfileTypeID == profileTypeID
                                                 select gpt.ProfileType).ToList<RMC.DataService.ProfileType>();
                }
                return _objectGenericProfileType;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetProfileTypes");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Get Profile Name by ProfileTypeId
        /// </summary>
        /// <returns></returns>
        public List<RMC.DataService.ProfileType> GetProfileNameByProfileTypeID(int profileTypeID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();


                _objectGenericProfileType = (from gpt in _objectRMCDataContext.ProfileTypes
                                             where gpt.ProfileTypeID == profileTypeID || gpt.IsActive == true
                                             select gpt).ToList<RMC.DataService.ProfileType>();

                return _objectGenericProfileType;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetProfileTypes");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Get Delete text from ProfileTypeID and UserID use in ProfileTreeView.ascx
        /// </summary>
        /// <returns></returns>
        public List<RMC.DataService.ProfileUser> GetIDBy_ProfileTypeID_UserID(int userID, int profileTypeID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();


                _objectGenericProfileUser = (from gpt in _objectRMCDataContext.ProfileUsers
                                             where gpt.UserID == userID && gpt.ProfileTypeID == profileTypeID
                                             select gpt).ToList<RMC.DataService.ProfileUser>();

                return _objectGenericProfileUser;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetProfileTypes");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Use in CreateNewProfile.ascx for bind the Profile Type DropDownList.
        /// </summary>
        /// <returns></returns>
        public List<RMC.DataService.ProfileType> GetProfileInformation(string type)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _objectGenericProfileType = (from gpt in _objectRMCDataContext.ProfileTypes
                                             where gpt.IsActive == true && gpt.Type.ToLower() == type
                                             select gpt).ToList<RMC.DataService.ProfileType>();

                return _objectGenericProfileType;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetProfileTypes");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public RMC.BusinessEntities.BEProfileType GetProfileTypeByProfileTypeID(int profileTypeID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                RMC.BusinessEntities.BEProfileType objectBEProfileType = null;

                objectBEProfileType = (from gpt in _objectRMCDataContext.ProfileUsers
                                       where gpt.ProfileTypeID == profileTypeID
                                       select new RMC.BusinessEntities.BEProfileType
                                       {
                                           CategoryProfiles = gpt.ProfileType.CategoryProfiles,
                                           Description = gpt.ProfileType.Description,
                                           IsActive = gpt.ProfileType.IsActive,
                                           IsShare = gpt.IsShared.Value,
                                           ProfileName = gpt.ProfileType.ProfileName,
                                           ProfileTypeID = gpt.ProfileTypeID,
                                           Type = gpt.ProfileType.Type,
                                           UserID = gpt.UserID,
                                           AuthorName = gpt.ProfileType.AuthorName
                                       }).FirstOrDefault();

                return objectBEProfileType;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetProfileTypeByProfileTypeID");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public bool UpdateProfileType(RMC.BusinessEntities.BEProfileType objectBEProfileType)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                RMC.DataService.ProfileUser objectProfileUser = (from pu in _objectRMCDataContext.ProfileUsers
                                                                 where pu.ProfileTypeID == objectBEProfileType.ProfileTypeID
                                                                 select pu).FirstOrDefault();

                objectProfileUser.ProfileType.Description = objectBEProfileType.Description;
                objectProfileUser.ProfileType.ProfileName = objectBEProfileType.ProfileName;
                objectProfileUser.IsShared = objectBEProfileType.IsShare;

                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdateProfileType");
                ex.Data.Add("Class", "BSProfileType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return flag;
        }

        #endregion

    }
}
