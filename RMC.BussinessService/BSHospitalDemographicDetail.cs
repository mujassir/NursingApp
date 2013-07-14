using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Configuration;

namespace RMC.BussinessService
{
    public class BSHospitalDemographicDetail
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        //Generic Data Service Object.
        List<RMC.DataService.HospitalDemographicInfo> _genericHospitalDemographicInfo = null;
        RMC.DataService.HospitalDemographicInfo _objectHospitalDemographicInfo = null;

        //EntitySet of DataService Objects.
        EntitySet<RMC.DataService.MultiUserHospital> _entitySetMultiUserHospital = null;
        EntitySet<RMC.DataService.MultiUserDemographic> _entitySetMultiUserDemographic = null;

        //Business Service Object.
        RMC.BussinessService.BSMultiUserDemographic _objectBSMultiUserDemographic = null;
        RMC.BussinessService.BSMultiUserHospital _objectBSMultiUserHospital = null;
        RMC.BussinessService.BSEmailNotificationBody _objectBSEmailNotificationBody = null;
        RMC.BussinessService.BSEmail _objectBSEmail = null;

        //Fundamental Data Types.
        bool _flag, _emailFlag;
        string _bodyText, _fromAddress, _toAddress;

        //String Buillder Object.
        StringBuilder _treeNodeCollection;

        #endregion

        #region Functions/Methods

        /// <summary>
        /// Get total Hospital Unit under particular hospital.
        /// </summary>
        /// <param name="hospitalInfoID"></param>
        /// <returns></returns>
        public int GetTotalHospitalUnit(int hospitalInfoID)
        {
            try
            {
                int totalHospital = 0;
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                totalHospital = (from h in _objectRMCDataContext.HospitalDemographicInfos
                                 where h.HospitalInfoID == hospitalInfoID && h.IsDeleted == false
                                 select h).Count();

                return totalHospital;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Fetch Hospital Demographic Detail By Hospital Information ID.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 10, 2009.
        /// </summary>
        /// <param name="hospitalInfoID"></param>
        /// <returns></returns>
        public List<RMC.DataService.HospitalDemographicInfo> GetHospitalDemographicDetailByHospitalInfoID(int hospitalInfoID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _genericHospitalDemographicInfo = (from hdd in _objectRMCDataContext.HospitalDemographicInfos
                                                   where hdd.HospitalInfoID == hospitalInfoID
                                                   select hdd).ToList<RMC.DataService.HospitalDemographicInfo>();
                return _genericHospitalDemographicInfo;
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

        /// <summary>
        /// Insert Demographic Information of Hospital.
        /// Created By : Davinder Kumar.
        /// Creation Date : June 25, 2009
        /// </summary>
        /// <param name="hospitalInfo"></param>
        /// <returns></returns>
        public bool InsertHospitalDemographicDetail(RMC.DataService.HospitalDemographicInfo hospitalDemographicInfo, RMC.DataService.MultiUserDemographic multiUserDemographic)
        {
            _flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _objectBSEmailNotificationBody = new BSEmailNotificationBody();
                _fromAddress = ConfigurationManager.AppSettings["fromAddress"].ToString();
                _toAddress = ConfigurationManager.AppSettings["superAdminAddress"].ToString();

                hospitalDemographicInfo.MultiUserDemographics.Add(multiUserDemographic);
                _objectRMCDataContext.HospitalDemographicInfos.InsertOnSubmit(hospitalDemographicInfo);
                _objectRMCDataContext.SubmitChanges();
                _flag = true;
                ////Body Text of Email.
                //_bodyText = Convert.ToString(_objectBSEmailNotificationBody.GetEmailBodyOfHospitalDemographicDetail(hospitalDemographicInfo));
                ////Send Email.
                //_objectBSEmail  = new BSEmail(_fromAddress, _toAddress, "New Demographic Detail", _bodyText, true);
                //_objectBSEmail.SendMail(true, out _emailFlag);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertHospitalDemographicDetail");
                ex.Data.Add("Class", "BSHospitalDemographicDetail");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return _flag;
        }

        /// <summary>
        /// OverLoad Methods
        /// </summary>
        /// <param name="hospitalDemographicInfo"></param>
        /// <param name="multiUserDemographic"></param>
        /// <param name="hospitaUnitID"></param>
        /// <returns></returns>
        public bool InsertHospitalDemographicDetail(RMC.DataService.HospitalDemographicInfo hospitalDemographicInfo, RMC.DataService.MultiUserDemographic multiUserDemographic, out int hospitaUnitID)
        {
            _flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _objectBSEmailNotificationBody = new BSEmailNotificationBody();
                _fromAddress = ConfigurationManager.AppSettings["fromAddress"].ToString();
                _toAddress = ConfigurationManager.AppSettings["superAdminAddress"].ToString();

                hospitalDemographicInfo.MultiUserDemographics.Add(multiUserDemographic);
                _objectRMCDataContext.HospitalDemographicInfos.InsertOnSubmit(hospitalDemographicInfo);
                _objectRMCDataContext.SubmitChanges();
                _flag = true;
                hospitaUnitID = hospitalDemographicInfo.HospitalDemographicID;    
                ////Body Text of Email.
                //_bodyText = Convert.ToString(_objectBSEmailNotificationBody.GetEmailBodyOfHospitalDemographicDetail(hospitalDemographicInfo));
                ////Send Email.
                //_objectBSEmail = new BSEmail(_fromAddress, _toAddress, "New Demographic Detail", _bodyText, true);
                //_objectBSEmail.SendMail(true, out _emailFlag);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertHospitalDemographicDetail");
                ex.Data.Add("Class", "BSHospitalDemographicDetail");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return _flag;
        }

        /// <summary>
        /// To Delete user detail
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// Created By : Raman
        /// Creation Date : Julu 24, 2009       
        public bool DeleteHospitalDemographicInfo(int hospitalDemographicId, string ActiveUser)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            try
            {
                //Deleting the MultiUserDemographic Data
                List<RMC.DataService.MultiUserDemographic> objectMultiUserDemographicList = (from mud in _objectRMCDataContext.MultiUserDemographics
                                                                                             where mud.HospitalDemographicID == hospitalDemographicId
                                                                                             select mud).ToList();

                if (objectMultiUserDemographicList != null)
                {
                    if (objectMultiUserDemographicList.Count > 0)
                    {
                        objectMultiUserDemographicList.ForEach(mud =>
                        {
                            mud.IsDeleted = true;
                            mud.DeletedBy = ActiveUser;
                            mud.DeletedDate = DateTime.Now;
                        });
                    }
                }

                //Deleting the HospitalDemographicInfo 
                RMC.DataService.HospitalDemographicInfo objectHospitalDemographicInfo = (from hdi in _objectRMCDataContext.HospitalDemographicInfos
                                                                                         where hdi.HospitalDemographicID == hospitalDemographicId
                                                                                         select hdi).FirstOrDefault();
                if (objectHospitalDemographicInfo != null)
                {
                    objectHospitalDemographicInfo.IsDeleted = true;
                    objectHospitalDemographicInfo.DeletedDate = DateTime.Now;
                    objectHospitalDemographicInfo.DeletedBy = ActiveUser;
                }
                _objectRMCDataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "public bool DeleteHospitalDemographicInfo(int hospitalDemographicId, string ActiveUser)");
                ex.Data.Add("Class", "BSHospitalDemographicDetail");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Fetch Hospital Demographic Detail By hospitalDemographicId.
        /// Created By : Sanvir Kumar.
        /// Creation Date : July 24, 2009.
        /// </summary>
        /// <param name="hospitalInfoID"></param>
        /// <returns></returns>
        public RMC.DataService.HospitalDemographicInfo GetHospitalDemographicDetail(int hospitalDemographicId)
        {
            RMC.DataService.HospitalDemographicInfo objectHospitalDemographicInfo = null;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                objectHospitalDemographicInfo = (from hdi in _objectRMCDataContext.HospitalDemographicInfos
                                                 where hdi.HospitalDemographicID == hospitalDemographicId
                                                 select hdi).FirstOrDefault<RMC.DataService.HospitalDemographicInfo>();
                return objectHospitalDemographicInfo;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetHospitalDemographicDetailByHospitalInfoID");
                ex.Data.Add("Class", "BSHospitalDemographicDetail");
                throw ex;
            }
            finally
            {
                objectHospitalDemographicInfo = null;
               // _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Fetch Hospital Demographic Detail By hospitalDemographicId.
        /// Created By : Sanvir Kumar.
        /// Creation Date : July 24, 2009.
        /// </summary>
        /// <param name="hospitalInfoID"></param>
        /// <returns></returns>
        public string GetHospitalUnitNameByHospitalDemographicID(int hospitalDemographicId)
        {
            RMC.DataService.HospitalDemographicInfo objectHospitalDemographicInfo = null;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                objectHospitalDemographicInfo = (from hdi in _objectRMCDataContext.HospitalDemographicInfos
                                                 where hdi.HospitalDemographicID == hospitalDemographicId
                                                 select hdi).FirstOrDefault<RMC.DataService.HospitalDemographicInfo>();
                if (objectHospitalDemographicInfo == null)
                {
                    return string.Empty;
                }
                else
                {
                    return objectHospitalDemographicInfo.HospitalUnitName;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetHospitalDemographicDetailByHospitalInfoID");
                ex.Data.Add("Class", "BSHospitalDemographicDetail");
                throw ex;
            }
            finally
            {
                objectHospitalDemographicInfo = null;
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Get Hospital Demographic Detail By UserID.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 28, 2009.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="demographicID"></param>
        /// <returns></returns>
        public RMC.DataService.HospitalDemographicInfo GetHospitalDemographicDetailByUserID(int userID, int hospitalDemographicID)
        {
            try
            {
                _objectHospitalDemographicInfo = new RMC.DataService.HospitalDemographicInfo();
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                _objectHospitalDemographicInfo = (from mud in _objectRMCDataContext.MultiUserDemographics
                                                  join hd in _objectRMCDataContext.HospitalDemographicInfos
                                                  on mud.HospitalDemographicID equals hd.HospitalDemographicID
                                                  where mud.UserID == userID && mud.HospitalDemographicID == hospitalDemographicID && hd.IsDeleted == false && mud.IsDeleted == false
                                                  select hd).FirstOrDefault();

                return _objectHospitalDemographicInfo;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetHospitalDemographicByUserID");
                ex.Data.Add("Class", "BSHospitalDemographicDetail");
                throw ex;
            }
        }

        /// <summary>
        /// Get Hospital Demographic Detail By UserID.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 28, 2009.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="demographicID"></param>
        /// <returns></returns>
        public List<RMC.DataService.HospitalDemographicInfo> GetHospitalDemographicDetailByHospitalID(int hospitalID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _genericHospitalDemographicInfo = null;

                if (hospitalID > 0)
                {
                    _genericHospitalDemographicInfo = (from mud in _objectRMCDataContext.MultiUserDemographics
                                                       where mud.HospitalDemographicInfo.HospitalInfoID == hospitalID && mud.HospitalDemographicInfo.IsDeleted == false && mud.IsDeleted == false
                                                       select mud.HospitalDemographicInfo).Distinct().ToList<RMC.DataService.HospitalDemographicInfo>();

                }
                return _genericHospitalDemographicInfo;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetHospitalDemographicByUserID");
                ex.Data.Add("Class", "BSHospitalDemographicDetail");
                throw ex;
            }
        }

        public List<RMC.DataService.HospitalDemographicInfo> GetHospitalDemographicDetailByHospitalID(int hospitalID, int userID)
        {
            try
            {   
                // Edited by Mahesh Sachdeva on 25-06-2010
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                var objectViewRequest = (from vr in _objectRMCDataContext.ViewRequests
                                         where vr.FromUserID == userID && vr.IsApproved==false
                                         select new { vr.HospitalDemographicDetailID }).ToList();
                var objectUnitExists = (from vr in _objectRMCDataContext.MultiUserDemographics where vr.UserID == userID select vr).ToList();
                List<int> viewRequest = new List<int>();
                List<int> unitExists = new List<int>();
                for (int index = 0; index < objectViewRequest.Count; index++)
                {
                    viewRequest.Add(Convert.ToInt32(objectViewRequest[index].HospitalDemographicDetailID));
                }
                for (int index = 0; index < objectUnitExists.Count; index++)
                {
                   unitExists.Add(Convert.ToInt32(objectUnitExists[index].HospitalDemographicID));
                }
                _genericHospitalDemographicInfo = (from mud in _objectRMCDataContext.MultiUserDemographics
                                                   where mud.HospitalDemographicInfo.HospitalInfoID == hospitalID && mud.HospitalDemographicInfo.IsDeleted == false && unitExists.Contains(Convert.ToInt32(mud.HospitalDemographicID)) == false && viewRequest.Contains(Convert.ToInt32(mud.HospitalDemographicID)) == false
                                                   select mud.HospitalDemographicInfo).Distinct().ToList<RMC.DataService.HospitalDemographicInfo>();

               
                return _genericHospitalDemographicInfo;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetHospitalDemographicByUserID");
                ex.Data.Add("Class", "BSHospitalDemographicDetail");
                throw ex;
            }
        }

        /// <summary>
        /// <Description>To update hospital demographic information</Description>
        /// <Author>Raman</Author>
        /// <CreatedOn>July 24, 3009</CreatedOn>
        /// </summary>
        /// <param name="hospitalInfoId"></param>
        /// <returns></returns>
        public bool UpdateHospitalDemographicInformation(RMC.DataService.HospitalDemographicInfo hospitalDemographicInfo)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                if (hospitalDemographicInfo.HospitalDemographicID > 0)
                {
                    _objectHospitalDemographicInfo = (from hdi in _objectRMCDataContext.HospitalDemographicInfos
                                                      where hdi.HospitalDemographicID == hospitalDemographicInfo.HospitalDemographicID
                                                      select hdi).FirstOrDefault<RMC.DataService.HospitalDemographicInfo>();
                    if (_objectHospitalDemographicInfo == null)
                    {
                        return false;
                    }
                }
                else
                {
                    _objectHospitalDemographicInfo = new RMC.DataService.HospitalDemographicInfo();
                    //if (_objectHospitalDemographicInfo.HospitalInfoID >= 0)
                    //{
                    _objectHospitalDemographicInfo.CreatedDate = hospitalDemographicInfo.CreatedDate;
                    _objectHospitalDemographicInfo.CreatedBy = hospitalDemographicInfo.CreatedBy;
                    _objectRMCDataContext.HospitalDemographicInfos.InsertOnSubmit(_objectHospitalDemographicInfo);

                    //}
                }
                                
                _objectHospitalDemographicInfo.ModifiedDate = hospitalDemographicInfo.ModifiedDate;
                _objectHospitalDemographicInfo.ModifiedBy = hospitalDemographicInfo.ModifiedBy;
                _objectHospitalDemographicInfo.HospitalInfoID = hospitalDemographicInfo.HospitalInfoID;
                _objectHospitalDemographicInfo.HospitalUnitName = hospitalDemographicInfo.HospitalUnitName;
                _objectHospitalDemographicInfo.TCABUnit = hospitalDemographicInfo.TCABUnit;
                //_objectHospitalDemographicInfo.BedsInHospital = hospitalDemographicInfo.BedsInHospital;
                _objectHospitalDemographicInfo.BedsInUnit = hospitalDemographicInfo.BedsInUnit;
                _objectHospitalDemographicInfo.UnitType = hospitalDemographicInfo.UnitType;
                //_objectHospitalDemographicInfo.Demographic = hospitalDemographicInfo.Demographic;
                _objectHospitalDemographicInfo.IsDeleted = hospitalDemographicInfo.IsDeleted;

                _objectHospitalDemographicInfo.ElectronicDocumentation = hospitalDemographicInfo.ElectronicDocumentation;
                _objectHospitalDemographicInfo.BudgetedPatientsPerNurse = hospitalDemographicInfo.BudgetedPatientsPerNurse;
                _objectHospitalDemographicInfo.PharmacyType = hospitalDemographicInfo.PharmacyType;
                _objectHospitalDemographicInfo.StartDate = hospitalDemographicInfo.StartDate;
                _objectHospitalDemographicInfo.EndedDate = hospitalDemographicInfo.EndedDate;
                _objectHospitalDemographicInfo.DocByException = hospitalDemographicInfo.DocByException;

                _objectRMCDataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", " public List<RMC.DataService.UserInfo> GetHospitalInformation(int hospitalInfoId)");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectHospitalDemographicInfo = null;
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// <Description>To update hospital demographic information</Description>
        /// <Author>Raman</Author>
        /// <CreatedOn>July 24, 3009</CreatedOn>
        /// </summary>
        /// <param name="hospitalInfoId"></param>
        /// <returns></returns>
        public bool UpdateHospitalDemographicInformation(RMC.DataService.HospitalDemographicInfo hospitalDemographicInfo, int ownerID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                if (hospitalDemographicInfo.HospitalDemographicID > 0)
                {
                    _objectHospitalDemographicInfo = (from hdi in _objectRMCDataContext.HospitalDemographicInfos
                                                      where hdi.HospitalDemographicID == hospitalDemographicInfo.HospitalDemographicID
                                                      select hdi).FirstOrDefault<RMC.DataService.HospitalDemographicInfo>();
                    if (_objectHospitalDemographicInfo == null)
                    {
                        return false;
                    }
                    else
                    {
                        _objectHospitalDemographicInfo.ModifiedDate = hospitalDemographicInfo.ModifiedDate;
                        _objectHospitalDemographicInfo.ModifiedBy = hospitalDemographicInfo.ModifiedBy;
                        _objectHospitalDemographicInfo.HospitalInfoID = hospitalDemographicInfo.HospitalInfoID;
                        _objectHospitalDemographicInfo.HospitalUnitName = hospitalDemographicInfo.HospitalUnitName;
                        _objectHospitalDemographicInfo.TCABUnit = hospitalDemographicInfo.TCABUnit;
                        //_objectHospitalDemographicInfo.BedsInHospital = hospitalDemographicInfo.BedsInHospital;
                        _objectHospitalDemographicInfo.BedsInUnit = hospitalDemographicInfo.BedsInUnit;
                        _objectHospitalDemographicInfo.UnitType = hospitalDemographicInfo.UnitType;
                        //_objectHospitalDemographicInfo.Demographic = hospitalDemographicInfo.Demographic;
                        _objectHospitalDemographicInfo.IsDeleted = hospitalDemographicInfo.IsDeleted;

                        _objectHospitalDemographicInfo.ElectronicDocumentation = hospitalDemographicInfo.ElectronicDocumentation;
                        _objectHospitalDemographicInfo.BudgetedPatientsPerNurse = hospitalDemographicInfo.BudgetedPatientsPerNurse;
                        _objectHospitalDemographicInfo.PharmacyType = hospitalDemographicInfo.PharmacyType;
                        _objectHospitalDemographicInfo.StartDate = hospitalDemographicInfo.StartDate;
                        _objectHospitalDemographicInfo.EndedDate = hospitalDemographicInfo.EndedDate;
                        _objectHospitalDemographicInfo.DocByException = hospitalDemographicInfo.DocByException;
                        _objectRMCDataContext.SubmitChanges();
                        return true;
                    }
                }

                // Commented by Mahesh Sachdeva due tochanges in bussiness logic
              //  else
               // {
                    //_objectHospitalDemographicInfo = new RMC.DataService.HospitalDemographicInfo();
                    //_objectHospitalDemographicInfo.CreatedDate = hospitalDemographicInfo.CreatedDate;
                    //_objectHospitalDemographicInfo.CreatedBy = hospitalDemographicInfo.CreatedBy;
                    //_objectRMCDataContext.HospitalDemographicInfos.InsertOnSubmit(_objectHospitalDemographicInfo);
               // }

                //if (ownerID > 0)
                //{
                //    RMC.DataService.HospitalInfo objectHospitalInfo = (from hdi in _objectRMCDataContext.HospitalDemographicInfos
                //                                                       where hdi.HospitalDemographicID == hospitalDemographicInfo.HospitalDemographicID
                //                                                       select hdi.HospitalInfo).FirstOrDefault();

                //    if (objectHospitalInfo != null)
                //    {
                //        if (objectHospitalInfo.UserID != ownerID)   //Commented by bharat
                //        {
                //            RMC.DataService.MultiUserDemographic objectMultiUserDemographic = (from mud in _objectRMCDataContext.MultiUserDemographics
                //                                                                               where mud.HospitalDemographicID == hospitalDemographicInfo.HospitalDemographicID && mud.UserID == ownerID && mud.IsDeleted == false
                //                                                                               select mud).FirstOrDefault();

                //            if (objectMultiUserDemographic.UserID != 1)
                //            {
                //                objectMultiUserDemographic.PermissionID = 1;
                //                if (objectHospitalInfo.UserID == 1)
                //                {
                //                    RMC.DataService.MultiUserDemographic objectMultiUserDemographic1 = (from mud in _objectRMCDataContext.MultiUserDemographics
                //                                                                                       where mud.HospitalDemographicID == hospitalDemographicInfo.HospitalDemographicID && mud.UserID == objectHospitalInfo.UserID && mud.IsDeleted == false
                //                                                                                       select mud).FirstOrDefault();

                //                    _objectRMCDataContext.MultiUserDemographics.DeleteOnSubmit(objectMultiUserDemographic1);
                //                }
                //            }
                //            else
                //            {
                //                _objectRMCDataContext.MultiUserDemographics.DeleteOnSubmit(objectMultiUserDemographic);
                //            }
                            

                //            RMC.DataService.MultiUserHospital objectMultiUserHospital = (from muh in _objectRMCDataContext.MultiUserHospitals
                //                                                                         where muh.HospitalInfoID == objectHospitalInfo.HospitalInfoID && muh.UserID == ownerID && muh.IsDeleted == false
                //                                                                         select muh).FirstOrDefault();
                //            if (objectMultiUserHospital != null)
                //            {
                //                if (objectMultiUserHospital.UserID != 1)
                //                {
                //                   // objectMultiUserHospital.PermissionID = 1; // Commented by Mahesh Sachdeva
                //                    if (objectHospitalInfo.UserID == 1)
                //                    {
                //                        RMC.DataService.MultiUserHospital objectMultiUserHospital1 = (from muh in _objectRMCDataContext.MultiUserHospitals
                //                                                                                      where muh.HospitalInfoID == objectHospitalInfo.HospitalInfoID && muh.UserID == objectHospitalInfo.UserID && muh.IsDeleted == false
                //                                                                                      select muh).FirstOrDefault();

                //                        _objectRMCDataContext.MultiUserHospitals.DeleteOnSubmit(objectMultiUserHospital1);
                //                    }
                //                }
                //                else
                //                {
                //                    _objectRMCDataContext.MultiUserHospitals.DeleteOnSubmit(objectMultiUserHospital);
                //                }
                //            }

                //        //    objectHospitalInfo.UserID = ownerID;   // comment by Mahesh Sachdeva
                //        } //Commented by bharat
                //    }
                //}
                //_objectHospitalDemographicInfo.ModifiedDate = hospitalDemographicInfo.ModifiedDate;
                //_objectHospitalDemographicInfo.ModifiedBy = hospitalDemographicInfo.ModifiedBy;
                //_objectHospitalDemographicInfo.HospitalInfoID = hospitalDemographicInfo.HospitalInfoID;
                //_objectHospitalDemographicInfo.HospitalUnitName = hospitalDemographicInfo.HospitalUnitName;
                //_objectHospitalDemographicInfo.TCABUnit = hospitalDemographicInfo.TCABUnit;
                ////_objectHospitalDemographicInfo.BedsInHospital = hospitalDemographicInfo.BedsInHospital;
                //_objectHospitalDemographicInfo.BedsInUnit = hospitalDemographicInfo.BedsInUnit;
                //_objectHospitalDemographicInfo.UnitType = hospitalDemographicInfo.UnitType;
                ////_objectHospitalDemographicInfo.Demographic = hospitalDemographicInfo.Demographic;
                //_objectHospitalDemographicInfo.IsDeleted = hospitalDemographicInfo.IsDeleted;

                //_objectHospitalDemographicInfo.ElectronicDocumentation = hospitalDemographicInfo.ElectronicDocumentation;
                //_objectHospitalDemographicInfo.BudgetedPatientsPerNurse = hospitalDemographicInfo.BudgetedPatientsPerNurse;
                //_objectHospitalDemographicInfo.PharmacyType = hospitalDemographicInfo.PharmacyType;
                //_objectHospitalDemographicInfo.StartDate = hospitalDemographicInfo.StartDate;
                //_objectHospitalDemographicInfo.EndedDate = hospitalDemographicInfo.EndedDate;
                //_objectHospitalDemographicInfo.DocByException = hospitalDemographicInfo.DocByException;
                _objectRMCDataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", " public List<RMC.DataService.UserInfo> GetHospitalInformation(int hospitalInfoId)");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectHospitalDemographicInfo = null;
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Get Hospital Unit Information By Searching Keyword.
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<RMC.DataService.HospitalDemographicInfo> GetHospitalUnitInformationBySearch(string search)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                if (search == 0.ToString())
                {
                    _genericHospitalDemographicInfo = (from hd in _objectRMCDataContext.HospitalDemographicInfos
                                                       where hd.IsDeleted == false
                                                       select hd).ToList<RMC.DataService.HospitalDemographicInfo>();
                }                           
                else
                {
                    _genericHospitalDemographicInfo = (from hd in _objectRMCDataContext.HospitalDemographicInfos
                                                       where hd.IsDeleted == false && hd.HospitalUnitName.Contains(search)
                                                       select hd).ToList<RMC.DataService.HospitalDemographicInfo>();
                }


                return _genericHospitalDemographicInfo;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetHospitalUnitInformationBySearch");
                ex.Data.Add("Class", "BSHospitalDemographicDetail");
                throw ex;
            }
        }

        /// <summary>
        /// To Delete Demographic or hospital Unit all information
        /// </summary>
        /// <param name="hospitalDemographicId"></param>
        /// <param name="ActiveUser"></param>
        /// <returns></returns>
        /// Created By : Raman
        /// Creation Date : August 18, 2009       
        public bool LogicalDeleteHospitalUnitInfo(int hospitalDemographicId, string ActiveUser)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            try
            {
                int superAdminID = (from ui in _objectRMCDataContext.UserInfos
                                    where ui.Email.ToLower().Trim() == "superadmin"
                                    select ui).FirstOrDefault().UserID;
                //Deleting the MultiUserDemographic Data
                List<RMC.DataService.MultiUserDemographic> objectMultiUserDemographicList = (from mud in _objectRMCDataContext.MultiUserDemographics
                                                                                             where mud.HospitalDemographicID == hospitalDemographicId
                                                                                             select mud).ToList();

                if (objectMultiUserDemographicList != null)
                {
                    if (objectMultiUserDemographicList.Count > 0)
                    {
                        foreach (RMC.DataService.MultiUserDemographic objectmud in objectMultiUserDemographicList)
                        {
                            objectmud.UserID = superAdminID;
                            objectmud.IsDeleted = true;
                            objectmud.DeletedBy = ActiveUser;
                            objectmud.DeletedDate = DateTime.Now;
                        }
                    }
                }

                //Deleting the HospitalDemographicInfo 
                RMC.DataService.HospitalDemographicInfo objectHospitalDemographicInfo = (from hdi in _objectRMCDataContext.HospitalDemographicInfos
                                                                                         where hdi.HospitalDemographicID == hospitalDemographicId
                                                                                         select hdi).FirstOrDefault();
                if (objectHospitalDemographicInfo != null)
                {
                    objectHospitalDemographicInfo.IsDeleted = true;
                    objectHospitalDemographicInfo.DeletedDate = DateTime.Now;
                    objectHospitalDemographicInfo.DeletedBy = ActiveUser;
                }



                //Deleting the HospitalUpload Data
                List<RMC.DataService.HospitalUpload> objectHostpitalUpload = (from hu in _objectRMCDataContext.HospitalUploads
                                                                              where hu.HospitalDemographicID == hospitalDemographicId
                                                                              select hu).ToList();

                if (objectHostpitalUpload != null)
                {
                    if (objectHostpitalUpload.Count > 0)
                    {
                        foreach (RMC.DataService.HospitalUpload objectup in objectHostpitalUpload)
                        {
                            objectup.IsDeleted = true;
                            objectup.DeletedBy = ActiveUser;
                            objectup.DeletedDate = DateTime.Now;
                        }

                    }
                }

                //Deleting the NursePDAInfo Data
                List<RMC.DataService.NursePDAInfo> objectNursePdaInfo = (from npi in _objectRMCDataContext.NursePDAInfos
                                                                         where npi.HospitalDemographicID == hospitalDemographicId
                                                                         select npi).ToList();

                if (objectNursePdaInfo != null)
                {
                    if (objectNursePdaInfo.Count > 0)
                    {
                        foreach (RMC.DataService.NursePDAInfo objectnpi in objectNursePdaInfo)
                        {
                            objectnpi.IsDeleted = true;
                            objectnpi.DeletedBy = ActiveUser;
                            objectnpi.DeletedDate = DateTime.Now;

                            //Deleting the NursePDADetail Data
                            List<RMC.DataService.NursePDADetail> objectNursePDADetail = (from npd in _objectRMCDataContext.NursePDADetails
                                                                                         where npd.NurseID == objectnpi.NurseID
                                                                                         select npd).ToList();

                            if (objectNursePDADetail != null)
                            {
                                if (objectNursePDADetail.Count > 0)
                                {
                                    foreach (RMC.DataService.NursePDADetail objectnpd in objectNursePDADetail)
                                    {
                                        objectnpd.IsDeleted = true;
                                        objectnpd.DeletedBy = ActiveUser;
                                        objectnpd.DeletedDate = DateTime.Now;
                                    }

                                }
                            }
                        }
                    }
                }
                _objectRMCDataContext.SubmitChanges();
                return false;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "public bool LogicalDeleteHospitalUnitInfo(int hospitalDemographicId, string ActiveUser)");
                ex.Data.Add("Class", "BSHospitalDemographicDetail");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public void UpdateIsCollapseField(int hospitalUnitID, bool IsCollapse, string userRole, int userID)
        {
            try
            {
                RMC.DataService.HospitalDemographicInfo objectOriginalHospitalDemographicInfo = null;
                RMC.DataService.MultiUserDemographic objectMultiUserDemographic = null;
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                if (userRole.ToLower().Trim() == "superadmin")
                {
                    objectOriginalHospitalDemographicInfo = _objectRMCDataContext.HospitalDemographicInfos.Single(h => h.HospitalDemographicID == hospitalUnitID);
                    objectOriginalHospitalDemographicInfo.IsCollapse = IsCollapse;
                }
                else
                {
                    objectMultiUserDemographic = _objectRMCDataContext.MultiUserDemographics.Single(h => h.HospitalDemographicID == hospitalUnitID && h.UserID == userID);
                    objectMultiUserDemographic.IsCollapse = IsCollapse; 
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

        #endregion

    }

    //End Of BSHospitalDemographicDetail Class
}
//End Of NameSpace
