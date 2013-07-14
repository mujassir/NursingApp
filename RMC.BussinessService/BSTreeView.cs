using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSTreeView
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        #endregion

        #region Public Methods

        /// <summary>
        /// Get All Active Hospital Names.
        /// Created By : Raman
        /// Creation Date : july 30, 2009.
        /// </summary>
        public List<RMC.BusinessEntities.BETreeHospitalInfo> GetAllActiveHospitalInfo(bool IsErrorExist)
        {

            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BETreeHospitalInfo> hospitalInfoList = (from hi in _objectRMCDataContext.HospitalInfos
                                                                                  where hi.IsDeleted == false   //hi.IsActive == true
                                                                                  orderby hi.RecordCounter, hi.HospitalInfoID
                                                                                  select new RMC.BusinessEntities.BETreeHospitalInfo
                                                                                  {
                                                                                      HospitalID = hi.HospitalInfoID,
                                                                                      HospitalName = hi.HospitalName,
                                                                                      City = hi.City,
                                                                                      State = hi.State.StateName,
                                                                                      CreatedDate = hi.CreatedDate.Value,
                                                                                      ModifiedDate = hi.ModifiedDate.Value,
                                                                                      IsCollapseHospital = hi.IsCollapse,
                                                                                      HospitalRecordCount = hi.RecordCounter
                                                                                  }).ToList();

                //hospitalInfoList.ForEach(h => h.HospitalUnitsList = GetAllActiveHospitalUnits(h.HospitalID));
                foreach (RMC.BusinessEntities.BETreeHospitalInfo objectHospitalInfo in hospitalInfoList)
                {
                    RMC.BussinessService.BSNursePDADetail objectBSNursePDADetail = new BSNursePDADetail();

                    objectHospitalInfo.HospitalUnitsList = GetAllActiveHospitalUnits(objectHospitalInfo.HospitalID);
                    foreach (RMC.BusinessEntities.BETreeHospitalUnits objectHospitalUnits in objectHospitalInfo.HospitalUnitsList)
                    {
                        //if (objectBSNursePDADetail.CheckForValidData(objectHospitalUnits.HospitalDemographicID))
                        //{
                        objectHospitalUnits.HospitalUnitsYears = GetAllYearsForHospitalUnits(objectHospitalUnits.HospitalDemographicID, IsErrorExist);
                        //}
                    }
                }

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
        /// Get All Active Hospital Names.
        /// Created By : Sumant
        /// Creation Date : july 30, 2009.
        /// </summary>
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

                //hospitalInfoList.ForEach(h => h.HospitalUnitsList = GetAllActiveHospitalUnits(h.HospitalID));
                foreach (RMC.BusinessEntities.BETreeHospitalInfo objectHospitalInfo in hospitalInfoList)
                {
                    RMC.BussinessService.BSNursePDADetail objectBSNursePDADetail = new BSNursePDADetail();

                    objectHospitalInfo.HospitalUnitsList = GetHospitalUnitsByUserID(userID, objectHospitalInfo.HospitalID);
                    foreach (RMC.BusinessEntities.BETreeHospitalUnits objectHospitalUnits in objectHospitalInfo.HospitalUnitsList)
                    {
                        //if (objectBSNursePDADetail.CheckForValidData(objectHospitalUnits.HospitalDemographicID))
                        //{
                        objectHospitalUnits.HospitalUnitsYears = GetAllYearsForHospitalUnits(objectHospitalUnits.HospitalDemographicID, IsErrorExist);
                        //}
                    }
                }

                return hospitalInfoList;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllActiveHospitalInfoByUserID");
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
        /// Created By : Raman
        /// Creation Date : july 30, 2009.
        /// </summary>
        public List<RMC.BusinessEntities.BETreeHospitalUnits> GetHospitalUnitsByUserID(int userID, int hospitalInfoId)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BETreeHospitalUnits> hospitalDemographicList = (from mud in _objectRMCDataContext.MultiUserDemographics
                                                                                          where mud.HospitalDemographicInfo.HospitalInfoID == hospitalInfoId && mud.UserID == userID
                                                                                          select new RMC.BusinessEntities.BETreeHospitalUnits
                                                                                          {
                                                                                              HospitalDemographicID = mud.HospitalDemographicInfo.HospitalDemographicID,
                                                                                              HospitalUnitName = mud.HospitalDemographicInfo.HospitalUnitName,
                                                                                              CreatedDate = mud.HospitalDemographicInfo.CreatedDate.Value,
                                                                                              ModifiedDate = mud.HospitalDemographicInfo.ModifiedDate.Value,
                                                                                              PermissionID = Convert.ToInt32(mud.PermissionID),
                                                                                              IsCollapseHospitalUnit = mud.IsCollapse
                                                                                          }).ToList();

                return hospitalDemographicList;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetHospitalUnitsByUserID");
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
        /// Created By : Raman
        /// Creation Date : july 30, 2009.
        /// </summary>
        public List<RMC.BusinessEntities.BETreeHospitalUnits> GetAllActiveHospitalUnits(int hospitalInfoId)
        {

            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BETreeHospitalUnits> hospitalDemographicList = (from hd in _objectRMCDataContext.HospitalDemographicInfos
                                                                                          where hd.HospitalInfoID == hospitalInfoId && hd.IsDeleted == false
                                                                                          orderby hd.HospitalDemographicID
                                                                                          select new RMC.BusinessEntities.BETreeHospitalUnits
                                                                                          {
                                                                                              HospitalDemographicID = hd.HospitalDemographicID,
                                                                                              HospitalUnitName = hd.HospitalUnitName,
                                                                                              CreatedDate = hd.CreatedDate.Value,
                                                                                              ModifiedDate = hd.ModifiedDate.Value,
                                                                                              IsCollapseHospitalUnit = hd.IsCollapse
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
        /// Get All Active Hospital units years.
        /// Created By : Raman
        /// Creation Date : july 30, 2009.
        /// </summary>
        public List<RMC.BusinessEntities.BETreeYears> GetAllYearsForHospitalUnits(int hospitalUnitId, bool IsErrorExist)
        {

            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BETreeYears> hospitalUnitYears = (from NPDAI in _objectRMCDataContext.NursePDAInfos
                                                                            where NPDAI.HospitalDemographicID == hospitalUnitId && NPDAI.IsErrorExist == IsErrorExist
                                                                            group NPDAI by NPDAI.Year into yearsGroup
                                                                            select new RMC.BusinessEntities.BETreeYears
                                                                            {
                                                                                Year = yearsGroup.Key
                                                                            }).ToList();
                hospitalUnitYears.ForEach(huy => huy.HospitalUnitsYearsMonths = GetAllMonthsInYearForHospitalUnits(hospitalUnitId, huy.Year, IsErrorExist));

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
        /// Created By : Raman
        /// Creation Date : Aug 07, 2009.
        /// </summary>
        public List<RMC.BusinessEntities.BETreeMonths> GetAllMonthsInYearForHospitalUnits(int hospitalUnitId, string Year, bool IsErrorExist)
        {

            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BETreeMonths> hospitalUnitYearMonths = (from NPDAI in _objectRMCDataContext.NursePDAInfos
                                                                                  where NPDAI.HospitalDemographicID == hospitalUnitId
                                                                                  && NPDAI.Year == Year
                                                                                  group NPDAI by NPDAI.Month into monthsGroup
                                                                                  select new RMC.BusinessEntities.BETreeMonths
                                                                                  {
                                                                                      IsError = GetError(hospitalUnitId, Year, monthsGroup.Key, IsErrorExist),
                                                                                      Month = monthsGroup.Key

                                                                                  }).ToList();

                hospitalUnitYearMonths.ForEach(huym => huym.NursePDAInfoList = GetListNursePDAInfo(hospitalUnitId, Year, huym.Month, IsErrorExist));
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
        public List<RMC.BusinessEntities.BENursePDAInfo> GetListNursePDAInfo(int hospitalDemographicID, string Year, string Month, bool IsErrorExist)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BENursePDAInfo> objectGenericBENursePDAInfo = (from NPDAI in _objectRMCDataContext.NursePDAInfos
                                                                                         where NPDAI.HospitalDemographicID == hospitalDemographicID && NPDAI.Year == Year && NPDAI.Month == Month && NPDAI.IsErrorExist == IsErrorExist
                                                                                         select new RMC.BusinessEntities.BENursePDAInfo
                                                                                         {
                                                                                             NurseID = NPDAI.NurseID,
                                                                                             FileReference = NPDAI.FileRefference,
                                                                                             IsCollapseMonth = NPDAI.IsCollapseMonth,
                                                                                             IsCollapseYear = NPDAI.IsCollapseYear,
                                                                                             IsAdminCollapseMonth = NPDAI.IsAdminCollapseMonth,
                                                                                             IsAdminCollapseYear = NPDAI.IsAdminCollapseYear
                                                                                         }).ToList();
                return objectGenericBENursePDAInfo;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetListNursePDAInfo");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public bool GetError(int hospitalUnitId, string Year, string Month, bool IsErrorExist)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                RMC.DataService.NursePDADetail objectNursePDADetail = (from NPDAI in _objectRMCDataContext.NursePDAInfos
                                                                       join NPDAD in _objectRMCDataContext.NursePDADetails
                                                                       on NPDAI.NurseID equals NPDAD.NurseID
                                                                       where NPDAI.HospitalDemographicID == hospitalUnitId
                                                                       && NPDAI.Year == Year && NPDAI.Month == Month
                                                                       && NPDAD.IsErrorExist == IsErrorExist
                                                                       select NPDAD).FirstOrDefault();

                if (objectNursePDADetail != null)
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
                ex.Data.Add("Function", "GetError");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
            return true;
        }

        /// <summary>
        /// Get All Membrs of hospital by hospitalID.
        /// Created By : Raman
        /// Creation Date : Aug 5, 2009.
        /// </summary>
        public List<RMC.BusinessEntities.BEHospitalMembers> GetAllMembersOfHospital(int hospitalInfoId)
        {

            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BEHospitalMembers> hospitalMemberList = (from MUH in _objectRMCDataContext.MultiUserHospitals
                                                                                   where MUH.HospitalInfoID == hospitalInfoId
                                                                                   && MUH.UserInfo.IsDeleted == false
                                                                                   && MUH.UserInfo.UserTypeID != 1
                                                                                   && MUH.IsDeleted == false
                                                                                   select new RMC.BusinessEntities.BEHospitalMembers
                                                                                   {
                                                                                       Owner = MUH.HospitalInfo.UserID == MUH.UserID ? true : false,
                                                                                       UserID = MUH.UserID,
                                                                                       UserName = MUH.UserInfo.FirstName + " " + MUH.UserInfo.LastName

                                                                                   }).ToList();

                hospitalMemberList.ForEach(ml => ml.UnitList = GetAllMembersUnderUnits(ml.UserID.Value, hospitalInfoId));

                return hospitalMemberList;
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
        /// Get All Membrs of hospital by hospitalID.        
        /// </summary>
        public List<RMC.BusinessEntities.BEHospitalMembers> GetAllMembersByHospitalID(int hospitalInfoId)
        {

            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BEHospitalMembers> hospitalMemberList = (from MUH in _objectRMCDataContext.MultiUserHospitals
                                                                                   where MUH.HospitalInfoID == hospitalInfoId
                                                                                   //&& MUH.UserInfo.UserTypeID != 1
                                                                                   && MUH.IsDeleted == false
                                                                                   //group MUH by new { MUH.HospitalInfoID, MUH.UserID } into a

                                                                                   select new RMC.BusinessEntities.BEHospitalMembers
                                                                                   {
                                                                                       Owner = MUH.HospitalInfo.UserID == MUH.UserID ? true : false,
                                                                                       UserID = MUH.MultiUserHospitalID,
                                                                                       UserName = MUH.UserInfo.FirstName + " " + MUH.UserInfo.LastName+" ("+MUH.Permission.Permission1+")",
                                                                                       IsApproved = true,
                                                                                       uID = MUH.UserID,
                                                                                       hospitalInfoID = MUH.HospitalInfoID
                                                                                   }).ToList();

                //---Added by Bharat--------Removes duplicate records from list---------------
                List<RMC.BusinessEntities.BEHospitalMembers> hospitalMemberListTemp = new List<RMC.BusinessEntities.BEHospitalMembers>();
                
                int count = 0;
                foreach (RMC.BusinessEntities.BEHospitalMembers obj in hospitalMemberList)
                {
                    int? MultiUserHospitalIDTemp = obj.UserID;
                    int? userIDTemp = obj.uID;
                    int? HospitalInfoIDTemp = obj.hospitalInfoID;
                    foreach (RMC.BusinessEntities.BEHospitalMembers objTemp in hospitalMemberList)
                    {
                        if (objTemp.uID == userIDTemp)
                        {
                            count++;
                        }
                        if (count > 1)
                        {
                            hospitalMemberListTemp.Add(objTemp);
                            count = 1;
                        }
                    }
                    count = 0;
                }

                if (hospitalMemberListTemp.Count > 0)
                {
                    foreach (RMC.BusinessEntities.BEHospitalMembers obj in hospitalMemberListTemp)
                    {
                        hospitalMemberList.Remove(obj);
                    }
                }
                //--------------------------------------------------

                hospitalMemberList.ForEach(ml => ml.UnitList = GetAllMembersUnderUnits(ml.UserID.Value, hospitalInfoId));

                return hospitalMemberList;
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
        /// Get hospital Units by hospitalmmberID.
        /// Created By : Raman
        /// Creation Date : Aug 5, 2009.
        /// </summary>
        public List<RMC.BusinessEntities.BETreeHospitalUnits> GetAllMembersUnderUnits(int MemberID, int hospitalInfoId)
        {

            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BETreeHospitalUnits> hospitalUnitsList = (from MUD in _objectRMCDataContext.MultiUserDemographics
                                                                                    where MUD.UserID == MemberID && MUD.HospitalDemographicInfo.HospitalInfoID == hospitalInfoId
                                                                                    && MUD.HospitalDemographicInfo.IsDeleted == false
                                                                                    select new RMC.BusinessEntities.BETreeHospitalUnits
                                                                                   {
                                                                                       HospitalDemographicID = MUD.HospitalDemographicID.Value,
                                                                                       HospitalUnitName = MUD.HospitalDemographicInfo.HospitalUnitName,
                                                                                       CreatedDate = MUD.HospitalDemographicInfo.CreatedDate.Value,
                                                                                       ModifiedDate = MUD.ModifiedDate == null ? null : MUD.ModifiedDate

                                                                                   }).ToList();


                return hospitalUnitsList;
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
        /// Get All Membrs of hospital unit by hospitalUnitID.
        /// Created By : Raman
        /// Creation Date : Aug 5, 2009.
        /// </summary>
        public List<RMC.BusinessEntities.BEHospitalMembers> GetAllMembersOfHospitalUnit(int hospitalDemographicId)
        {

            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BEHospitalMembers> hospitalUnitMembersList = (from MUD in _objectRMCDataContext.MultiUserDemographics
                                                                                        where MUD.HospitalDemographicID == hospitalDemographicId
                                                                                        && MUD.UserInfo.IsDeleted == false
                                                                                        select new RMC.BusinessEntities.BEHospitalMembers
                                                                                        {
                                                                                            UserID = MUD.UserID,
                                                                                            UserName = MUD.UserInfo.FirstName + " " + MUD.UserInfo.LastName + " (" + MUD.Permission.Permission1+")", //+ " (" + MUD.UserInfo.Email + ")",
                                                                                            IsApproved = true
                                                                                        }).ToList();


                return hospitalUnitMembersList;
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
        /// Get the Permission Name and Hospital Unit Name on the basis of UserID.
        /// Created By : Deepakt
        /// Creation Date : Sept 25, 2009.
        /// </summary>
        public List<RMC.BusinessEntities.BEHospitalMembers> GetAllPermission_HospitalUnitName(int userid)
        {

            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BEHospitalMembers> hospitalUnitMembersList = (from MUD in _objectRMCDataContext.MultiUserDemographics
                                                                                        where MUD.UserID == userid
                                                                                            //&& MUD.UserInfo.IsDeleted == false
                                                                                        && MUD.IsDeleted == false
                                                                                        select new RMC.BusinessEntities.BEHospitalMembers
                                                                                        {
                                                                                            PermissionID = MUD.Permission.PermissionID,
                                                                                            Permission = MUD.Permission.Permission1 + " -",
                                                                                            UnitName = MUD.HospitalDemographicInfo.HospitalUnitName,
                                                                                            UserID = MUD.UserID,
                                                                                            IsApproved = Convert.ToBoolean(MUD.IsDeleted),
                                                                                            MultiUserDemographicID = MUD.MultiUserDemographicID,
                                                                                            HospitalName = MUD.HospitalDemographicInfo.HospitalInfo.HospitalName,
                                                                                            HospitalCity = MUD.HospitalDemographicInfo.HospitalInfo.City,
                                                                                            HospitalState = MUD.HospitalDemographicInfo.HospitalInfo.State.StateName
                                                                                        }).ToList();


                return hospitalUnitMembersList;
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

        #endregion

    }
}
