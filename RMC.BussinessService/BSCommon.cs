using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace RMC.BussinessService
{
    public class BSCommon
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        //Fundamental Data Types.
        bool _flag;

        #endregion

        #region Functions/Methods of Fetching Countries.

        /// <summary>
        /// Fetch All Countries Name With ID.
        /// Created By : Davinder Kumar
        /// Creation Date : July 8, 2009
        /// </summary>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BECountry> GetAllCountries()
        {
            try
            {
                List<RMC.BusinessEntities.BECountry> genericBECountry = new List<RMC.BusinessEntities.BECountry>();
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                var country = from c in _objectRMCDataContext.Countries
                              orderby c.CountryName
                              select new { c.CountryID, c.CountryName };

                foreach (var c in country)
                {
                    RMC.BusinessEntities.BECountry objectBECountry = new RMC.BusinessEntities.BECountry();

                    objectBECountry.CountryID = c.CountryID;
                    objectBECountry.CountryName = c.CountryName;

                    genericBECountry.Add(objectBECountry);
                }

                return genericBECountry;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllCountries");
                ex.Data.Add("Class", "BSCommon");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        #endregion

        #region Functions/Methods of fetching states

        /// <summary>
        /// Fetch All States Name.
        /// Created By : Davinder Kumar
        /// Creation Date : June 24, 2009
        /// </summary>
        /// <returns></returns>
        public bool GetAllStateNames(out List<RMC.DataService.State> states)
        {
            states = null;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                states = (from s in _objectRMCDataContext.States
                          orderby s.StateName
                          select s).ToList<RMC.DataService.State>();
                if (states.Count > 0)
                {
                    _flag = true;
                }
                else
                {
                    _flag = false;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "AllStateNames");
                ex.Data.Add("Class", "BSCommon");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return _flag;
        }

        /// <summary>
        /// Fetch All States Name By CountryID.
        /// Created By : Davinder Kumar
        /// Creation Date : July 8, 2009
        /// </summary>
        /// <param name="CountryID"></param>
        /// <returns></returns>
        public List<RMC.DataService.State> GetAllStateNamesByCountryID(int CountryID)
        {
            List<RMC.DataService.State> states = null;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                states = (from s in _objectRMCDataContext.States
                          where s.IsActive == true && s.CountryID == CountryID
                          orderby s.StateName
                          select s).ToList<RMC.DataService.State>();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllStateNamesByCountryID");
                ex.Data.Add("Class", "BSCommon");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return states;
        }

        #endregion

        #region Functions/Methods of fetching Users List and update the user's activity

        /// <summary>
        /// Update User Status.
        /// Created By : Amit Chawla
        /// Creation Date : June 30, 2009
        /// </summary>
        /// <param name="_userId"></param>
        /// <param name="_active"></param>
        public void UpdateUserStatus(int _userId, bool _active)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                var query = from s in _objectRMCDataContext.UserInfos
                            where s.UserID == _userId
                            select s;
                foreach (RMC.DataService.UserInfo s in query)
                {
                    s.IsActive = _active;
                }
                _objectRMCDataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdateUserStatus");
                ex.Data.Add("Class", "BSCommon");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext = null;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Fetch Permission ID from Permission.
        /// Created Date : Davinder Kumar.
        /// Creation Date : July 22, 2009.
        /// </summary>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        public int GetPermissionIDByPermissionName(string permissionName)
        {
            try
            {

                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                int permissionID = (from p in _objectRMCDataContext.Permissions
                                    where p.IsActive == true
                                    select p).FirstOrDefault().PermissionID;

                return permissionID;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetPermissionIDByPermissionName");
                ex.Data.Add("Class", "BSCommon");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Get Month Name
        /// Created Date : Raman.
        /// Creation Date : July 31, 2009.
        /// </summary>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        public static string GetMonthName(string Month)
        {
            try
            {
                switch (Month)
                {
                    case "1":
                    case "01":
                        return "January";
                    case "2":
                    case "02":
                        return "February";
                    case "3":
                    case "03":
                        return "March";
                    case "4":
                    case "04":
                        return "April";
                    case "5":
                    case "05":
                        return "May";
                    case "6":
                    case "06":
                        return "June";
                    case "7":
                    case "07":
                        return "July";
                    case "8":
                    case "08":
                        return "August";
                    case "9":
                    case "09":
                        return "September";
                    case "10":
                        return "October";
                    case "11":
                        return "November";
                    case "12":
                        return "December";
                    default:
                        return String.Empty;

                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetPermissionIDByPermissionName");
                ex.Data.Add("Class", "BSCommon");
                throw ex;
            }
            finally
            {

            }
        }

        public static string GetMonthDays(string Month, int Year)
        {
            try
            {
                switch (Month)
                {
                    case "1":
                    case "01":
                        return "31";
                    case "2":
                    case "02":
                        if (Year % 4 == 0 && (Year % 100 != 0 || (Year % 100 == 0 && Year % 400 == 0)))
                        {
                            return "29";
                        }
                        else
                        {
                            return "28";
                        }
                    case "3":
                    case "03":
                        return "31";
                    case "4":
                    case "04":
                        return "30";
                    case "5":
                    case "05":
                        return "31";
                    case "6":
                    case "06":
                        return "30";
                    case "7":
                    case "07":
                        return "31";
                    case "8":
                    case "08":
                        return "31";
                    case "9":
                    case "09":
                        return "30";
                    case "10":
                        return "31";
                    case "11":
                        return "30";
                    default:
                        return "31";
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetPermissionIDByPermissionName");
                ex.Data.Add("Class", "BSCommon");
                throw ex;
            }
            finally
            {

            }
        }

        public static List<RMC.BusinessEntities.BEMonth> GetAllMonths()
        {
            try
            {
                List<RMC.BusinessEntities.BEMonth> objectGenericBEMonth = new List<RMC.BusinessEntities.BEMonth>();
                
                for (int index = 0; index < 12; index++)
                {
                    RMC.BusinessEntities.BEMonth objectBEMonth = new RMC.BusinessEntities.BEMonth();
                    switch (index)
                    {
                        case 0:
                            objectBEMonth.MonthName = "January";
                            break;
                        case 1:
                            objectBEMonth.MonthName = "Feburary";
                            break;
                        case 2:
                            objectBEMonth.MonthName = "March";
                            break;
                        case 3:
                            objectBEMonth.MonthName = "April";
                            break;
                        case 4:
                            objectBEMonth.MonthName = "May";
                            break;
                        case 5:
                            objectBEMonth.MonthName = "June";
                            break;
                        case 6:
                            objectBEMonth.MonthName = "July";
                            break;
                        case 7:
                            objectBEMonth.MonthName = "August";
                            break;
                        case 8:
                            objectBEMonth.MonthName = "September";
                            break;
                        case 9:
                            objectBEMonth.MonthName = "October";
                            break;
                        case 10:
                            objectBEMonth.MonthName = "November";
                            break;
                       default:
                            objectBEMonth.MonthName = "December";
                            break;
                    }
                    
                    objectBEMonth.MonthID = Convert.ToString(index + 1);
                    objectGenericBEMonth.Add(objectBEMonth);
                }

                return objectGenericBEMonth;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Function/Method use to Hospital and Unit Information

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hospitalUnitID"></param>
        /// <returns></returns>
        public RMC.BusinessEntities.BEHospitalCumUnitInformation GetHospitalCumUnitInformation(int hospitalUnitID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                RMC.BusinessEntities.BEHospitalCumUnitInformation objectBEHospitalCumUnitInfo = (from hu in _objectRMCDataContext.HospitalDemographicInfos
                                                                                                 where hu.HospitalDemographicID == hospitalUnitID && hu.IsDeleted == false
                                                                                                 select new RMC.BusinessEntities.BEHospitalCumUnitInformation
                                                                                                 {
                                                                                                     HospitalName = hu.HospitalInfo.HospitalName,
                                                                                                     UnitName = hu.HospitalUnitName
                                                                                                 }).FirstOrDefault();
                return objectBEHospitalCumUnitInfo;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetPermissionIDByPermissionName");
                ex.Data.Add("Class", "BSCommon");
                throw ex;
            }
        }

        #endregion

        #region Function/Method use to get owner id.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hospitalUnitID"></param>
        /// <returns></returns>
        public int GetOwnerIDByHospitalUnitID(int hospitalUnitID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                int ownerID = (from hdd in _objectRMCDataContext.HospitalDemographicInfos
                               where hdd.HospitalInfoID == hospitalUnitID
                               select hdd.HospitalInfo.UserID).FirstOrDefault();
                return ownerID;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetHospitalDemographicDetailByHospitalInfoID");
                ex.Data.Add("Class", "BSHospitalDemographicDetail");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        #endregion

        #region Function/Method Get Value added and Category Group values

        /// <summary>
        /// Use in Profile Detail.ascx
        /// </summary>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BECategoryType> GetAllCategoryByProfileTypeID(int profileTypeID)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            RMC.DataService.ProfileType objectProfileType = null;
            List<RMC.BusinessEntities.BECategoryType> objectGenericBEProfileCategory = null;
            RMC.BusinessEntities.BECategoryType ObjCatetype = null;
            try
            {
                objectProfileType = (from pt in _objectRMCDataContext.ProfileTypes
                                     where pt.ProfileTypeID == profileTypeID
                                     select pt).FirstOrDefault();

                if (objectProfileType.Type.ToLower().Trim() == "value added")
                {
                    objectGenericBEProfileCategory = (from va in _objectRMCDataContext.ValueAddedTypes
                                                      where va.IsActive == true
                                                      select new RMC.BusinessEntities.BECategoryType
                                                      {
                                                          ProfileCategory = va.TypeName,
                                                          ProfileCategoryID = va.TypeID
                                                      }).ToList();
                }
                else if (objectProfileType.Type.ToLower().Trim() == "activities")
                {
                    objectGenericBEProfileCategory = (from cg in _objectRMCDataContext.ActivitiesCategories                                                     
                                                      select new RMC.BusinessEntities.BECategoryType
                                                      {
                                                          ProfileCategory = cg.ActivitiesCategory1,
                                                          ProfileCategoryID = cg.ActivitiesID
                                                      }).ToList();
                }
                else if (objectProfileType.Type.ToLower().Trim() == "others")
                {
                    objectGenericBEProfileCategory = (from cg in _objectRMCDataContext.CategoryGroups
                                                      where cg.IsActive == true
                                                      select new RMC.BusinessEntities.BECategoryType
                                                      {
                                                          ProfileCategory = cg.CategoryGroup1,
                                                          ProfileCategoryID = cg.CategoryGroupID
                                                      }).ToList();
                }
                else
                {
                    objectGenericBEProfileCategory = (from cg in _objectRMCDataContext.LocationCategories
                                                      select new RMC.BusinessEntities.BECategoryType
                                                      {
                                                          ProfileCategory = cg.LocationCategory1,
                                                          ProfileCategoryID = cg.LocationID
                                                      }).ToList();
                }
                ObjCatetype = new RMC.BusinessEntities.BECategoryType();
                ObjCatetype.ProfileCategory = "Select..";
                ObjCatetype.ProfileCategoryID = 0;
                objectGenericBEProfileCategory.Insert(0, ObjCatetype);
                return objectGenericBEProfileCategory;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllCategoryGroup");
                ex.Data.Add("Class", "BSCategoryGroup");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Use in CreateNewProfile.ascx
        /// </summary>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BECategoryType> GetAllValueAdded_CategoryGroup(string valuetype)
        {

            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BECategoryType> objectGenericBEProfileCategory = null;
                
                if (valuetype == "0")
                {
                    objectGenericBEProfileCategory = (from va in _objectRMCDataContext.ValueAddedTypes
                                                      where va.IsActive == true
                                                      select new RMC.BusinessEntities.BECategoryType
                                                      {                                                          
                                                          ProfileCategory = va.TypeName,
                                                          ProfileCategoryID = va.TypeID
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
                ex.Data.Add("Function", "GetAllValueAdded_CategoryGroup");
                ex.Data.Add("Class", "BSCategoryGroup");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public List<RMC.BusinessEntities.BEUserInfomation> GetEmailByUserId(int userid)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            List<RMC.BusinessEntities.BEUserInfomation> objectGetEmailbyuserId = null;
            try
            {
                objectGetEmailbyuserId = (from a in _objectRMCDataContext.UserInfos

                                          where a.UserID == userid 
                                          select new RMC.BusinessEntities.BEUserInfomation 
                                          {
                                            Email=a.Email 
                                          }).ToList();
                 return objectGetEmailbyuserId;
                
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllValueAdded_CategoryGroup");
                ex.Data.Add("Class", "BSCategoryGroup");
                throw ex;
            }
            finally
            {
            
            } 
            
        }
        #endregion
     
        #region Static Methods

        public static List<RMC.BusinessEntities.BEOperator> Operator()
        {
            try
            {
                List<RMC.BusinessEntities.BEOperator> objectGenericBEOperator = new List<RMC.BusinessEntities.BEOperator>();

                for (int count = 1; count <= 6; count++)
                {
                    RMC.BusinessEntities.BEOperator objectBEOperator = new RMC.BusinessEntities.BEOperator();
                    if (count == 1)
                    {
                        objectBEOperator.Text = "<";
                        objectBEOperator.Value = 1;
                        objectGenericBEOperator.Add(objectBEOperator);
                    }
                    if (count == 2)
                    {
                        objectBEOperator.Text = ">";
                        objectBEOperator.Value = 2;
                        objectGenericBEOperator.Add(objectBEOperator);
                    }
                    if (count == 3)
                    {
                        objectBEOperator.Text = "=";
                        objectBEOperator.Value = 3;
                        objectGenericBEOperator.Add(objectBEOperator);
                    }
                    if (count == 4)
                    {
                        objectBEOperator.Text = ">=";
                        objectBEOperator.Value = 4;
                        objectGenericBEOperator.Add(objectBEOperator);
                    }
                    if (count == 5)
                    {
                        objectBEOperator.Text = "<=";
                        objectBEOperator.Value = 5;
                        objectGenericBEOperator.Add(objectBEOperator);
                    }
                    if (count == 6)
                    {
                        objectBEOperator.Text = "<>";
                        objectBEOperator.Value = 6;
                        objectGenericBEOperator.Add(objectBEOperator);
                    }
                }
                return objectGenericBEOperator;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

                
    }
    //End Of BSCommon Class.
}
//End Of NameSpace.