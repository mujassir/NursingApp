using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSHospitalInfo
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        //Generic objects of data service Objects.
        List<RMC.DataService.HospitalInfo> _genericHospitalInfo = null;

        //Generic objects of data service Objects.
        List<RMC.DataService.HospitalDemographicInfo> _genericHospitalDemographicInfo = null;

        //Business Service objects.
        RMC.BussinessService.BSEmailNotificationBody _objectBSEmailNotificationBody = null;
        RMC.BussinessService.BSEmail _objectBSEmail = null;

        RMC.DataService.MultiUserHospital _objectMultiUserHospital = null;

        //Data Service Objects.
        RMC.DataService.HospitalInfo _objectHospitalInfo;

        //Fundamental Data Types.
        bool _flag, _emailFlag;
        string _bodyText, _fromAddress, _toAddress;

        #endregion

        #region Functions/Methods

        /// <summary>
        /// Get All Active Hospital Names.
        /// Created By : Davinder Kumar
        /// Creation Date : June 25, 2009.
        /// </summary>
        public bool GetAllActiveHospitalNames(out List<RMC.DataService.HospitalInfo> hostipalInfo)
        {
            _flag = false;
            hostipalInfo = null;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                hostipalInfo = (from hi in _objectRMCDataContext.HospitalInfos
                                where hi.IsActive == true
                                select hi).ToList<RMC.DataService.HospitalInfo>();

                if (hostipalInfo.Count > 0)
                {
                    _flag = true;
                }
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

            return _flag;
        }

        /// <summary>
        /// Get Hospital Name.
        /// </summary>
        /// <param name="hospitalID"></param>
        /// <returns></returns>
        public string GetHospitalNameByHospitalID(int hospitalID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                _objectHospitalInfo = (from hi in _objectRMCDataContext.HospitalInfos
                                       where hi.IsActive == true && hi.IsDeleted == false && hi.HospitalInfoID == hospitalID
                                       select hi).FirstOrDefault();

                if (_objectHospitalInfo == null)
                {
                    return string.Empty;
                }
                else
                {
                    if (_objectHospitalInfo.StateID > 0)
                        return _objectHospitalInfo.HospitalName + ", " + _objectHospitalInfo.State.StateName + ", " + _objectHospitalInfo.City;
                    else
                        return _objectHospitalInfo.HospitalName + ", " + string.Empty + ", " + _objectHospitalInfo.City; ;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetHospitalNameByHospitalID");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
        }

        /// <summary>
        /// Get Active Hospital by Hospital Info ID.
        /// Created By : Davinder Kumar
        /// Creation Date : July 08, 2009.
        /// </summary>
        /// <param name="hospitalInfoID"></param>
        /// <returns></returns>
        public RMC.DataService.HospitalInfo GetHospitalInfoByHospitalInfoID(int hospitalInfoID)
        {
            try
            {
                RMC.DataService.HospitalInfo objectHospitalInfo = null;
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                objectHospitalInfo = (from hi in _objectRMCDataContext.HospitalInfos
                                      where hi.IsActive == true && hi.HospitalInfoID == hospitalInfoID
                                      select hi).FirstOrDefault();

                return objectHospitalInfo;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetHospitalInfoByHospitalInfoID");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
        }

        /// <summary>
        /// Get Hospital Information by UserID through Multiuser.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 15, 2009.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public RMC.DataService.HospitalInfo GetHospitalInfoByUserID(int userID)
        {
            try
            {
                RMC.DataService.HospitalInfo objectHospitalInfo = null;
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                objectHospitalInfo = (from hi in _objectRMCDataContext.HospitalInfos
                                      join muh in _objectRMCDataContext.MultiUserHospitals
                                      on hi.HospitalInfoID equals muh.HospitalInfoID
                                      where hi.IsActive == true && muh.UserID == userID && hi.IsDeleted == false && muh.IsDeleted == false
                                      select hi).FirstOrDefault();

                return objectHospitalInfo;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetHospitalInfoByUserID");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
        }

        /// <summary>
        /// Fetch Hospital Name List.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEHospitalList> GetHospitalNamesByUserID(int userID)
        {
            try
            {
                List<RMC.BusinessEntities.BEHospitalList> objectGenericHospitalInfo = null;
                RMC.DataService.UserInfo objectUserInfo = null;
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                if (userID > 1)
                {
                    // Commented for Location Profile Functionality
                    // 14/12/2010
                    //objectUserInfo = (from ui in _objectRMCDataContext.UserInfos
                    //                  where ui.IsActive == true && ui.IsDeleted == false && ui.UserID == userID
                    //                  select ui).FirstOrDefault();
                    objectUserInfo = (from ui in _objectRMCDataContext.UserInfos
                                      where ui.IsDeleted == false && ui.UserID == userID
                                      select ui).FirstOrDefault();
                }
                else
                {
                    //objectUserInfo = (from ui in _objectRMCDataContext.UserInfos
                    //                  where ui.IsActive == true && ui.IsDeleted == false
                    //                  select ui).FirstOrDefault();
                    objectUserInfo = (from ui in _objectRMCDataContext.UserInfos
                                      where ui.IsDeleted == false
                                      select ui).FirstOrDefault();
                }

                if (objectUserInfo.Email.ToLower().Trim() == "superadmin")
                {
                    objectGenericHospitalInfo = (from hi in _objectRMCDataContext.MultiUserHospitals
                                                 where hi.HospitalInfo.IsActive == true && hi.IsDeleted == false && hi.HospitalInfo.IsDeleted == false && hi.UserInfo.IsActive == true
                                                 select new RMC.BusinessEntities.BEHospitalList
                                                 {
                                                     HospitalExtendedName = "#" + hi.HospitalInfo.RecordCounter + "-" + hi.HospitalInfo.HospitalName + ", " + hi.HospitalInfo.City + ", " + ((hi.HospitalInfo.StateID > 0) ? _objectRMCDataContext.States.Where(w => w.StateID == hi.HospitalInfo.StateID).FirstOrDefault().StateName : string.Empty),
                                                     HospitalInfoID = hi.HospitalInfo.HospitalInfoID,
                                                     RecordCounter = hi.HospitalInfo.RecordCounter,
                                                     HospitalName = hi.HospitalInfo.HospitalName

                                                 }).Distinct().ToList<RMC.BusinessEntities.BEHospitalList>();
                }
                else
                {
                    objectGenericHospitalInfo = (from muh in _objectRMCDataContext.MultiUserHospitals
                                                 where muh.HospitalInfo.IsActive == true && muh.UserID == userID && muh.IsDeleted == false && muh.HospitalInfo.IsDeleted == false
                                                 select new RMC.BusinessEntities.BEHospitalList
                                                 {
                                                     HospitalExtendedName = "#" + muh.HospitalInfo.RecordCounter + "-" + muh.HospitalInfo.HospitalName + ", " + muh.HospitalInfo.City + ", " + muh.HospitalInfo.State.StateName,
                                                     HospitalInfoID = muh.HospitalInfo.HospitalInfoID,
                                                     RecordCounter = muh.HospitalInfo.RecordCounter,
                                                     HospitalName = muh.HospitalInfo.HospitalName
                                                 }).Distinct().ToList<RMC.BusinessEntities.BEHospitalList>();
                }
                return objectGenericHospitalInfo.OrderBy(x => x.RecordCounter).ToList();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetHospitalInfoByUserID");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
        }

        /// <summary>
        /// Fetch Hospital Name List.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEHospitalList> GetHospitalNamesForRequestByUserID(int userID)
        {
            try
            {
                List<RMC.BusinessEntities.BEHospitalList> objectGenericHospitalInfo = null;
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                //List<RMC.DataService.HospitalInfo> objectGenericHospitalInfo = new List<RMC.DataService.HospitalInfo>(); 
                //objectGenericHospitalInfo = (from hi in _objectRMCDataContext.HospitalInfos where hi.UserID != userID  && hi.IsActive==true && hi.IsDeleted==false select hi).ToList();
                objectGenericHospitalInfo = (from hi in _objectRMCDataContext.HospitalInfos
                                             where hi.UserID != userID && hi.IsActive == true && hi.IsDeleted == false
                                             orderby hi.RecordCounter
                                             select new RMC.BusinessEntities.BEHospitalList
                                                 {
                                                     HospitalExtendedName = "#" + hi.RecordCounter + "-" + hi.HospitalName + ", " + hi.City + ", " + hi.State.StateName,
                                                     HospitalInfoID = hi.HospitalInfoID,
                                                     
                                                 }).ToList<RMC.BusinessEntities.BEHospitalList>();

               return objectGenericHospitalInfo;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetHospitalInfoByUserID");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
        }

        /// <summary>
        /// Overload Method To fetch Hospital Information By UserID and HospitalID.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 24, 2009.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public RMC.DataService.HospitalInfo GetHospitalInfoByUserID(int userID, int hospitalID)
        {
            try
            {
                RMC.DataService.HospitalInfo objectHospitalInfo = null;
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                objectHospitalInfo = (from hi in _objectRMCDataContext.HospitalInfos
                                      join muh in _objectRMCDataContext.MultiUserHospitals
                                      on hi.HospitalInfoID equals muh.HospitalInfoID
                                      where hi.IsActive == true && muh.UserID == userID && hi.HospitalInfoID == hospitalID && hi.IsDeleted == false && muh.IsDeleted == false
                                      select hi).FirstOrDefault();

                return objectHospitalInfo;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetHospitalInfoByUserID");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
        }

        /// <summary>
        /// Save Data in DynamicData Objects.
        /// Created By : Davinder Kumar.
        /// Creation Date : Aug 10, 2009.
        /// </summary>
        /// <param name="hospitalInfoID"></param>
        /// <param name="objectBEHospitalInfoDynamicProp"></param>
        /// <returns></returns>
        public bool GetColumnNameByHospitalInfo(int hospitalInfoID, RMC.BusinessEntities.BEHospitalInfoDynamicProp objectBEHospitalInfoDynamicProp)
        {
            bool flag = false;
            try
            {
                System.Data.Linq.EntitySet<RMC.DataService.DynamicData> objectEntityDynamicData = new System.Data.Linq.EntitySet<RMC.DataService.DynamicData>();
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.DataService.ColumnName> objectGenericColumnName = (from cn in _objectRMCDataContext.ColumnNames
                                                                            where cn.TableName.ToLower().Trim() == "hospitalinfo"
                                                                            select cn).ToList<RMC.DataService.ColumnName>();

                foreach (RMC.DataService.ColumnName objectColumnName in objectGenericColumnName)
                {
                    RMC.DataService.DynamicData objectDynamicData = new RMC.DataService.DynamicData();
                    if (objectColumnName.ColumnName1.ToLower().Trim() == "bedsinhospital")
                    {
                        objectDynamicData.ColumnID = objectColumnName.ColumnID;
                        objectDynamicData.Value = objectBEHospitalInfoDynamicProp.BedsInHospital;
                        objectDynamicData.ID = hospitalInfoID;
                    }
                    else if (objectColumnName.ColumnName1.ToLower().Trim() == "ownershiptype")
                    {
                        objectDynamicData.ColumnID = objectColumnName.ColumnID;
                        objectDynamicData.Value = objectBEHospitalInfoDynamicProp.OwnershipType;
                        objectDynamicData.ID = hospitalInfoID;
                    }
                    else if (objectColumnName.ColumnName1.ToLower().Trim() == "hospitaltype")
                    {
                        objectDynamicData.ColumnID = objectColumnName.ColumnID;
                        objectDynamicData.Value = objectBEHospitalInfoDynamicProp.HospitalType;
                        objectDynamicData.ID = hospitalInfoID;
                    }

                    objectEntityDynamicData.Add(objectDynamicData);
                }

                _objectRMCDataContext.DynamicDatas.InsertAllOnSubmit(objectEntityDynamicData);
                _objectRMCDataContext.SubmitChanges();
                flag = true;
                return flag;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetColumnNameByHospitalInfo");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Fetch Hospital Information by HospitalUnitID.
        /// </summary>
        /// <param name="hospitalUnitID">Hospital Unit ID</param>
        /// <returns>Hospital Information</returns>
        public RMC.DataService.HospitalInfo GetHospitalInfoByHospitalUnitID(int hospitalUnitID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                RMC.DataService.HospitalInfo objectHospitalInfo = _objectRMCDataContext.HospitalDemographicInfos.Where(w => w.HospitalDemographicID == hospitalUnitID).FirstOrDefault().HospitalInfo;

                if (objectHospitalInfo != null)
                {
                    return objectHospitalInfo;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insert Initial Information of Hospital.
        /// Created By : Davinder Kumar.
        /// Creation Date : June 24, 2009
        /// </summary>
        /// <param name="hospitalInfo"></param>
        /// <returns></returns>
        public bool InsertHospitalInfomation(RMC.DataService.HospitalInfo hospitalInfo, RMC.DataService.MultiUserHospital multiUserHospital, RMC.BusinessEntities.BEHospitalInfoDynamicProp objectBEHospitalInfoDynamicProp)
        {
            _flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _objectBSEmailNotificationBody = new BSEmailNotificationBody();
                _fromAddress = ConfigurationManager.AppSettings["fromAddress"].ToString();
                _toAddress = ConfigurationManager.AppSettings["superAdminAddress"].ToString();
                if (hospitalInfo.RecordCounter == 0)
                    hospitalInfo.RecordCounter = _objectRMCDataContext.HospitalInfos.Count() + 1;
                hospitalInfo.MultiUserHospitals.Add(multiUserHospital);
                _objectRMCDataContext.HospitalInfos.InsertOnSubmit(hospitalInfo);
                _objectRMCDataContext.SubmitChanges();
                if (objectBEHospitalInfoDynamicProp.BedsInHospital.Length > 0 || objectBEHospitalInfoDynamicProp.HospitalType.Length > 0 || objectBEHospitalInfoDynamicProp.OwnershipType.Length > 0)
                {
                    if (GetColumnNameByHospitalInfo(hospitalInfo.HospitalInfoID, objectBEHospitalInfoDynamicProp))
                    {
                        _flag = true;
                    }
                    else
                    {
                        _flag = false;
                    }
                }
                else
                {
                    _flag = true;
                }
                ////Body Text of Email.
                //_bodyText = Convert.ToString(_objectBSEmailNotificationBody.GetEmailBodyOfHospitalRegistration(hospitalInfo));
                ////Send Email.
                //_objectBSEmail = new BSEmail(_fromAddress, _toAddress, "New Hospital Registration", _bodyText, true);
                //_objectBSEmail.SendMail(true, out _emailFlag);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertHospitalInfomation");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return _flag;
        }

        /// <summary>
        /// Override Method.
        /// </summary>
        /// <param name="hospitalInfo"></param>
        /// <param name="multiUserHospital"></param>
        /// <param name="objectBEHospitalInfoDynamicProp"></param>
        /// <param name="hospitalInfoID">Return HospitalInfoID</param>
        /// <returns></returns>
        public bool InsertHospitalInfomation(RMC.DataService.HospitalInfo hospitalInfo, RMC.DataService.MultiUserHospital multiUserHospital, RMC.BusinessEntities.BEHospitalInfoDynamicProp objectBEHospitalInfoDynamicProp, out int hospitalInfoID)
        {
            _flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _objectBSEmailNotificationBody = new BSEmailNotificationBody();
                _fromAddress = ConfigurationManager.AppSettings["fromAddress"].ToString();
                _toAddress = ConfigurationManager.AppSettings["superAdminAddress"].ToString();
                if (hospitalInfo.RecordCounter == 0)
                    hospitalInfo.RecordCounter = _objectRMCDataContext.HospitalInfos.Count() + 1;
                hospitalInfo.MultiUserHospitals.Add(multiUserHospital);
                _objectRMCDataContext.HospitalInfos.InsertOnSubmit(hospitalInfo);
                _objectRMCDataContext.SubmitChanges();
                if (objectBEHospitalInfoDynamicProp.BedsInHospital.Length > 0 || objectBEHospitalInfoDynamicProp.HospitalType.Length > 0 || objectBEHospitalInfoDynamicProp.OwnershipType.Length > 0)
                {
                    if (GetColumnNameByHospitalInfo(hospitalInfo.HospitalInfoID, objectBEHospitalInfoDynamicProp))
                    {
                        _flag = true;
                    }
                    else
                    {
                        _flag = false;
                    }
                }
                else
                {
                    _flag = true;
                }

                hospitalInfoID = hospitalInfo.HospitalInfoID;
                ////Body Text of Email.
                //_bodyText = Convert.ToString(_objectBSEmailNotificationBody.GetEmailBodyOfHospitalRegistration(hospitalInfo));
                ////Send Email.
                //_objectBSEmail = new BSEmail(_fromAddress, _toAddress, "New Hospital Registration", _bodyText, true);
                ////_objectBSEmail = new BSEmail("bharatg@smartdatainc.net", "bharatg@smartdatainc.net", "New Hospital Registration", _bodyText, true);
                //_objectBSEmail.SendMail(true, out _emailFlag);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertHospitalInfomation");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return _flag;
        }

        /// <summary>
        /// Hospital Activation.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 3, 2009.
        /// </summary>
        /// <param name="genericBEHospitalList">Bussiness Entity used for fetch data from User interface.</param>
        /// <returns></returns>
        public bool InsertActiveDeactiveHospitalList(List<RMC.BusinessEntities.BEHospitalList> genericBEHospitalList)
        {
            int index = 0;
            _flag = false;
            List<RMC.DataService.HospitalInfo> genericHospitalInfo = null;
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            try
            {
                genericHospitalInfo = (from hi in _objectRMCDataContext.HospitalInfos
                                       select hi).ToList<RMC.DataService.HospitalInfo>();

                foreach (RMC.BusinessEntities.BEHospitalList objectBEHospitalList in genericBEHospitalList)
                {
                    index = genericHospitalInfo.IndexOf(genericHospitalInfo.Find(
                               delegate(RMC.DataService.HospitalInfo objectHospitalInfo) { return objectHospitalInfo.HospitalInfoID == objectBEHospitalList.HospitalInfoID; }));
                    genericHospitalInfo[index].IsActive = objectBEHospitalList.IsActive;
                }

                _objectRMCDataContext.SubmitChanges();
                _flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ActiveHospitalList");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                genericHospitalInfo = null;
                _objectRMCDataContext.Dispose();
            }

            return _flag;
        }

        /// <summary>
        /// Update Particular Hospital Information from Hospital List.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 08, 2009.
        /// Modified By : Davinder Kumar
        /// Modified Date : July 24, 2009.
        /// </summary>
        /// <param name="hospitalInfo"></param>
        /// <returns></returns>
        public bool UpdateHospitalInformation(RMC.DataService.HospitalInfo hospitalInfo)
        {
            _flag = false;
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            try
            {
                RMC.DataService.HospitalInfo objectHospitalInfo = (from hi in _objectRMCDataContext.HospitalInfos
                                                                   where hi.HospitalInfoID == hospitalInfo.HospitalInfoID && hi.IsActive == true && hi.IsDeleted == false
                                                                   select hi).FirstOrDefault();
                if (objectHospitalInfo == null)
                {
                    objectHospitalInfo = new RMC.DataService.HospitalInfo();
                    objectHospitalInfo.CreatedBy = "";
                    objectHospitalInfo.CreatedDate = DateTime.Now;
                }

                objectHospitalInfo.Address = hospitalInfo.Address;
                objectHospitalInfo.ChiefNursingOfficerFirstName = hospitalInfo.ChiefNursingOfficerFirstName;
                objectHospitalInfo.ChiefNursingOfficerLastName = hospitalInfo.ChiefNursingOfficerLastName;
                objectHospitalInfo.ChiefNursingOfficerPhone = hospitalInfo.ChiefNursingOfficerPhone;
                objectHospitalInfo.City = hospitalInfo.City;
                objectHospitalInfo.HospitalName = hospitalInfo.HospitalName;
                objectHospitalInfo.ModifiedBy = hospitalInfo.ModifiedBy;
                objectHospitalInfo.ModifiedDate = hospitalInfo.ModifiedDate;
                objectHospitalInfo.StateID = hospitalInfo.StateID;
                objectHospitalInfo.Zip = hospitalInfo.Zip;

                _objectRMCDataContext.SubmitChanges();


                _flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdateHospitalInformation");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return _flag;
        }


        /// <summary>
        /// Cascading Delete using flag.
        /// Created By : Raman.
        /// Creation Date : July 09, 2009.
        /// </summary>
        /// <param name="hospitalInfo"></param>
        /// <returns></returns>
        public bool DeleteHospitalInformation(RMC.DataService.HospitalInfo hospitalInfo)
        {
            _flag = false;
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            try
            {
                //Delete Hospital Information by flag.
                RMC.DataService.HospitalInfo objectHospitalInfo = (from hi in _objectRMCDataContext.HospitalInfos
                                                                   where hi.HospitalInfoID == hospitalInfo.HospitalInfoID
                                                                   select hi).FirstOrDefault();

                //objectHospitalInfo.IsDeleted = hospitalInfo.IsDeleted;
                //objectHospitalInfo.DeletedBy = hospitalInfo.DeletedBy;
                //objectHospitalInfo.DeletedDate = DateTime.Now;

                //Delete Hospital Demographic Detail By flag.
                List<RMC.DataService.HospitalDemographicInfo> genericHospitalDemoInfo = (from hdi in _objectRMCDataContext.HospitalDemographicInfos
                                                                                         where hdi.HospitalInfoID == hospitalInfo.HospitalInfoID
                                                                                         select hdi).ToList<RMC.DataService.HospitalDemographicInfo>();

                foreach (RMC.DataService.HospitalDemographicInfo objectHospitalDemoInfo in genericHospitalDemoInfo)
                {
                    //objectHospitalDemoInfo.IsDeleted = hospitalInfo.IsDeleted;
                    //objectHospitalDemoInfo.DeletedBy = hospitalInfo.DeletedBy;
                    //objectHospitalDemoInfo.DeletedDate = DateTime.Now;

                    //Delete Hospital Upload By flag.
                    List<RMC.DataService.HospitalUpload> genericHospitalUpload = (from hu in _objectRMCDataContext.HospitalUploads
                                                                                  where hu.HospitalDemographicID == objectHospitalDemoInfo.HospitalDemographicID
                                                                                  select hu).ToList<RMC.DataService.HospitalUpload>();

                    foreach (RMC.DataService.HospitalUpload objectHospitalUpload in genericHospitalUpload)
                    {
                        //objectHospitalUpload.IsDeleted = hospitalInfo.IsDeleted;
                        //objectHospitalUpload.DeletedBy = Convert.ToInt32(hospitalInfo.DeletedBy);
                        objectHospitalUpload.DeletedDate = DateTime.Now;
                    }

                    //Delete PDA Nurse Info By flag.
                    List<RMC.DataService.NursePDAInfo> genericNursePDAInfo = (from n in _objectRMCDataContext.NursePDAInfos
                                                                              where n.HospitalDemographicID == objectHospitalDemoInfo.HospitalDemographicID
                                                                              select n).ToList<RMC.DataService.NursePDAInfo>();

                    foreach (RMC.DataService.NursePDAInfo objectNursePDAInfo in genericNursePDAInfo)
                    {
                        //objectNursePDAInfo.IsDeleted = hospitalInfo.IsDeleted;
                        //objectNursePDAInfo.DeletedBy = Convert.ToInt32(hospitalInfo.DeletedBy);
                        objectNursePDAInfo.DeletedDate = DateTime.Now;

                        //Delete PDA Nurse Detail By flag.
                        List<RMC.DataService.NursePDADetail> genericNursePDADetail = (from n in _objectRMCDataContext.NursePDADetails
                                                                                      where n.NurseID == objectNursePDAInfo.NurseID
                                                                                      select n).ToList<RMC.DataService.NursePDADetail>();

                        foreach (RMC.DataService.NursePDADetail objectNursePDADetail in genericNursePDADetail)
                        {
                            //objectNursePDADetail.IsDeleted = hospitalInfo.IsDeleted;
                            //objectNursePDADetail.DeletedBy = hospitalInfo.DeletedBy;
                            objectNursePDADetail.DeletedDate = DateTime.Now;
                        }
                    }
                }

                //Delete User Info By flag.
                //List<RMC.DataService.UserInfo> genericUserInfo = (from ui in _objectRMCDataContext.UserInfos
                //                                                  where ui.HospitalInfoID == hospitalInfo.HospitalInfoID
                //                                                  select ui).ToList<RMC.DataService.UserInfo>();

                //foreach (RMC.DataService.UserInfo objectUserInfo in genericUserInfo)
                //{
                //    objectUserInfo.IsDeleted = hospitalInfo.IsDeleted;
                //    objectUserInfo.DeletedBy = hospitalInfo.DeletedBy;
                //    objectUserInfo.DeletedDate = DateTime.Now;
                //}

                _objectRMCDataContext.SubmitChanges();
                _flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdateHospitalInformation");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return _flag;
        }
        /// <summary>
        /// Return all hospitals
        /// Created By : Raman.
        /// Creation Date : July 22, 2009.
        /// </summary>
        /// <param name="hospitalInfoID"></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEHospitalList> GetHospitals()
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BEHospitalList> objectGenericBEHospitalList = (from hi in _objectRMCDataContext.HospitalInfos
                                                                                         where hi.IsDeleted == false
                                                                                         select new RMC.BusinessEntities.BEHospitalList
                                                                                         {
                                                                                             HospitalExtendedName = "#" + hi.RecordCounter.ToString() + "-" + hi.HospitalName + ", " + hi.City + ", " + hi.State.StateName,
                                                                                             HospitalInfoID = hi.HospitalInfoID
                                                                                         }).ToList<RMC.BusinessEntities.BEHospitalList>();
                return objectGenericBEHospitalList;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "public List<RMC.DataService.HospitalInfo> GetHospitals()");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _genericHospitalInfo = null;
                _objectRMCDataContext.Dispose();
            }
        }
        /// <summary>
        /// Return all hospitals
        /// Created By : Raman.
        /// Creation Date : July 22, 2009.
        /// </summary>
        /// <param name="hospitalInfoID"></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEHospitalList> GetHospitals(int userID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BEHospitalList> objectGenericBEHospitalList = (from hi in _objectRMCDataContext.MultiUserHospitals
                                                                                         where hi.HospitalInfo.IsDeleted == false && hi.UserID == userID
                                                                                         select new RMC.BusinessEntities.BEHospitalList
                                                                                         {
                                                                                             HospitalExtendedName = "#" + hi.HospitalInfo.RecordCounter.ToString() + "-" + hi.HospitalInfo.HospitalName + ", " + hi.HospitalInfo.City + ", " + hi.HospitalInfo.State.StateName,
                                                                                             HospitalInfoID = hi.HospitalInfo.HospitalInfoID
                                                                                         }).ToList<RMC.BusinessEntities.BEHospitalList>();
                return objectGenericBEHospitalList;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "public List<RMC.DataService.HospitalInfo> GetHospitals()");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _genericHospitalInfo = null;
                _objectRMCDataContext.Dispose();
            }
        }
        /// <summary>
        /// Return all hospitals Units
        /// Created By : Raman.
        /// Creation Date : July 28, 2009.
        /// </summary>
        /// <param name="hospitalInfoID"></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEHospitalDemographicInfo> GetAllHospitalUnitsByHospitalInfoID(int HospitalInfoID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BEHospitalDemographicInfo> _genericHospitalDemographicInfo = (from hdi in _objectRMCDataContext.HospitalDemographicInfos
                                                                                                        where hdi.HospitalInfoID == HospitalInfoID & hdi.IsDeleted == false
                                                                                                        select new RMC.BusinessEntities.BEHospitalDemographicInfo { HospitalDemographicID = hdi.HospitalDemographicID, HospitalUnitName = hdi.HospitalUnitName }).ToList();
                return _genericHospitalDemographicInfo;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "List<RMC.DataService.HospitalDemographicInfo> GetAllHospitalUnitsByHospitalInfoID(int HospitalInfoID)");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                //_genericHospitalInfo = null;
                _objectRMCDataContext.Dispose();
            }
        }
        /// <summary>
        /// Return all hospitals
        /// Created By : Raman.
        /// Creation Date : July 22, 2009.
        /// </summary>
        /// <param name="hospitalInfoID"></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEHospitalIdName> GetHospitalsIdName()
        {
            List<RMC.BusinessEntities.BEHospitalIdName> listBEHospitalIdName = null;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                listBEHospitalIdName = (from hi in _objectRMCDataContext.HospitalInfos
                                        where hi.IsDeleted == false
                                        orderby hi.HospitalName, hi.City
                                        select new RMC.BusinessEntities.BEHospitalIdName { HospitalInfoID = hi.HospitalInfoID, HospitalName = hi.HospitalName + "(" + hi.City + ")" }).ToList<RMC.BusinessEntities.BEHospitalIdName>();
                return listBEHospitalIdName;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "public List<RMC.BusinessEntities.BEHospitalIdName> GetHospitals()");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                listBEHospitalIdName = null;
                _objectRMCDataContext.Dispose();
            }
        }
        /// <summary>
        /// Change hospital active status
        /// Created By : Raman.
        /// Creation Date : July 22, 2009.
        /// </summary>
        /// <param name="hospitalInfoID"></param>
        /// <returns></returns>
        public void UpdateHospitalActiveStatus(int hospitalInfoId, bool active)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                var query = from hi in _objectRMCDataContext.HospitalInfos
                            where hi.HospitalInfoID == hospitalInfoId
                            select hi;
                foreach (RMC.DataService.HospitalInfo hi in query)
                {
                    hi.IsActive = active;
                }
                _objectRMCDataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "public void UpdateHospitalActiveStatus(int hospitalInfoID, bool active)");
                ex.Data.Add("Class", "BShospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext = null;
            }
        }

        /// <summary>
        /// <Description>To return HospitalInfo</Description>
        /// <Author>Raman</Author>
        /// <CreatedOn>July 23, 2009</CreatedOn>
        /// </summary>
        /// <param name="hospitalInfoId"></param>
        /// <returns></returns>
        public RMC.BusinessEntities.BEHospitalInfo GetHospitalInformation(int hospitalInfoId)
        {

            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            try
            {
                RMC.BusinessEntities.BEHospitalInfo objectBEHospitalInfo = (from hi in _objectRMCDataContext.HospitalInfos
                                                                            where hi.HospitalInfoID == hospitalInfoId && (hi.IsDeleted ?? false) == false
                                                                            select new RMC.BusinessEntities.BEHospitalInfo
                                                                            {
                                                                                HospitalInfoID = hi.HospitalInfoID,
                                                                                UserID = hi.UserID,
                                                                                HospitalName = hi.HospitalName,
                                                                                ChiefNursingOfficerFirstName = hi.ChiefNursingOfficerFirstName,
                                                                                ChiefNursingOfficerLastName = hi.ChiefNursingOfficerLastName,
                                                                                ChiefNursingOfficerPhone = hi.ChiefNursingOfficerPhone,
                                                                                ChiefNursingOfficerEmail = hi.ChiefNursingOfficerEmail,
                                                                                City = hi.City,
                                                                                Address = hi.Address,
                                                                                CountryID = (hi.StateID != 0) ? _objectRMCDataContext.States.FirstOrDefault(f => f.StateID == hi.StateID).CountryID : 0,
                                                                                StateID = hi.StateID,
                                                                                IsActive = hi.IsActive,
                                                                                Zip = hi.Zip,
                                                                                HospitalRecordCount = hi.RecordCounter
                                                                            }).FirstOrDefault<RMC.BusinessEntities.BEHospitalInfo>();



                return objectBEHospitalInfo;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", " public List<RMC.DataService.UserInfo> GetHospitalInformation(int hospitalInfoId)");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }


        /// <summary>
        /// <Description>To return HospitalInfo</Description>
        /// <Author>Raman</Author>
        /// <CreatedOn>July 23, 2009</CreatedOn>
        /// </summary>
        /// <param name="hospitalInfoId"></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEHospitalInfo> GetHospitalDynamicFieldInformation(int hospitalInfoId)
        {

            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            try
            {
                List<RMC.BusinessEntities.BEHospitalInfo> objectBEHospitalInfo = (from hi in _objectRMCDataContext.DynamicDatas
                                                                                  where hi.ID == hospitalInfoId
                                                                                  select new RMC.BusinessEntities.BEHospitalInfo
                                                                                  {
                                                                                      ColumnID = hi.ColumnID,
                                                                                      Value = hi.Value,
                                                                                      DynamicDataID = hi.DynamicDataID

                                                                                  }).ToList();



                return objectBEHospitalInfo;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", " public List<RMC.DataService.UserInfo> GetHospitalInformation(int hospitalInfoId)");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }


        /// <summary>
        /// <Description>To update hospital information</Description>
        /// <Author>Raman</Author>
        /// <CreatedOn>July 24,2009</CreatedOn>
        /// </summary>
        /// <param name="hospitalInfoId"></param>
        /// <returns></returns>
        public bool UpdateHospitalInfo(RMC.BusinessEntities.BEHospitalInfo objectBEHospitalInfo, string ActiveUser)
        {
            RMC.DataService.HospitalInfo objectHospitalInfo = null;

            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                if (objectBEHospitalInfo.HospitalInfoID > 0)
                {
                    objectHospitalInfo = (from hi in _objectRMCDataContext.HospitalInfos
                                          where hi.HospitalInfoID == objectBEHospitalInfo.HospitalInfoID
                                          select hi).FirstOrDefault<RMC.DataService.HospitalInfo>();
                    if (objectHospitalInfo == null)
                    {
                        return false;
                    }
                }
                else
                {
                    objectHospitalInfo = new RMC.DataService.HospitalInfo();
                    if (objectBEHospitalInfo.HospitalInfoID <= 0)
                    {
                        objectHospitalInfo.CreatedBy = ActiveUser;
                        objectHospitalInfo.CreatedDate = DateTime.Now;

                        //_objectMultiUserHospital = new RMC.DataService.MultiUserHospital();
                        //_objectMultiUserHospital.UserID = objectBEHospitalInfo.UserID;
                        //_objectMultiUserHospital.PermissionID = 1;
                        //_objectMultiUserHospital.IsDeleted = objectBEHospitalInfo.IsDeleted;
                        //_objectMultiUserHospital.CreatedBy = ActiveUser;
                        //_objectMultiUserHospital.CreatedDate = DateTime.Now;
                        //_objectMultiUserHospital.ModifiedBy = ActiveUser;
                        //_objectMultiUserHospital.ModifiedDate = DateTime.Now;

                        // objectHospitalInfo.MultiUserHospitals.Add(_objectMultiUserHospital);
                        _objectRMCDataContext.HospitalInfos.InsertOnSubmit(objectHospitalInfo);

                    }
                }
                objectHospitalInfo.Address = objectBEHospitalInfo.Address;
                if (System.Web.HttpContext.Current.User.IsInRole("superadmin"))
                {
                    objectHospitalInfo.RecordCounter = objectBEHospitalInfo.HospitalRecordCount;
                }
                objectHospitalInfo.ChiefNursingOfficerFirstName = objectBEHospitalInfo.ChiefNursingOfficerFirstName;
                objectHospitalInfo.ChiefNursingOfficerLastName = objectBEHospitalInfo.ChiefNursingOfficerLastName;
                objectHospitalInfo.ChiefNursingOfficerPhone = objectBEHospitalInfo.ChiefNursingOfficerPhone;
                objectHospitalInfo.ChiefNursingOfficerEmail = objectBEHospitalInfo.ChiefNursingOfficerEmail;
                objectHospitalInfo.City = objectBEHospitalInfo.City;
                objectHospitalInfo.HospitalName = objectBEHospitalInfo.HospitalName;
                objectHospitalInfo.StateID = objectBEHospitalInfo.StateID;
               // objectHospitalInfo.UserID = objectBEHospitalInfo.UserID;
                objectHospitalInfo.Zip = objectBEHospitalInfo.Zip;
                objectHospitalInfo.IsDeleted = objectBEHospitalInfo.IsDeleted;
                objectHospitalInfo.CreatedBy = ActiveUser;
                objectHospitalInfo.CreatedDate = DateTime.Now;
                objectHospitalInfo.ModifiedBy = ActiveUser;
                objectHospitalInfo.ModifiedDate = DateTime.Now;

                _objectRMCDataContext.SubmitChanges();

                // also update userid in MultiUserHospital table
             //   RMC.DataService.MultiUserHospital objectMultiUserHospital = null;
              //  objectMultiUserHospital = (from MUH in _objectRMCDataContext.MultiUserHospitals
                //                           where MUH.HospitalInfoID == objectBEHospitalInfo.HospitalInfoID
                                               //&& MUH.UserID == objectBEHospitalInfo.UserID
                  //                         && MUH.PermissionID == 1
                                               //&& MUH.UserInfo.UserTypeID != 1
                    //                       && MUH.IsDeleted == false
                      //                     select MUH).FirstOrDefault<RMC.DataService.MultiUserHospital>();


                // Added by Bharat-------------------------------
              //  if (objectMultiUserHospital != null)
               // {
                //    objectMultiUserHospital.UserID = objectBEHospitalInfo.UserID;
                 //   _objectRMCDataContext.SubmitChanges();
                //}
                //else
                //{
                //    _objectMultiUserHospital = new RMC.DataService.MultiUserHospital();
                //    _objectMultiUserHospital.UserID = objectBEHospitalInfo.UserID;
                //    _objectMultiUserHospital.PermissionID = 1;
                //    _objectMultiUserHospital.IsDeleted = objectBEHospitalInfo.IsDeleted;
                //    _objectMultiUserHospital.CreatedBy = ActiveUser;
                //    _objectMultiUserHospital.CreatedDate = DateTime.Now;
                //    _objectMultiUserHospital.ModifiedBy = ActiveUser;
                //    _objectMultiUserHospital.ModifiedDate = DateTime.Now;
                //    _objectMultiUserHospital.HospitalInfoID = objectBEHospitalInfo.HospitalInfoID;

                //    _objectRMCDataContext.MultiUserHospitals.InsertOnSubmit(_objectMultiUserHospital);
                //    _objectRMCDataContext.SubmitChanges();
                //}
                //-----------------------------------------------
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
                objectHospitalInfo = null;
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// <Description>To update hospital Dyanamic Field  information</Description>
        /// <Author>Raman</Author>
        /// <CreatedOn>11 August 2009</CreatedOn>
        /// </summary>
        /// <param name="hospitalInfoId"></param>
        /// <returns></returns>
        public bool UpdateDynamicFieldData(RMC.BusinessEntities.BEHospitalInfoDynamicProp objectBEDynamicHospitalInfo, int HospitalInfoID)
        {
            List<RMC.DataService.DynamicData> objectDSHospitalInfo = null;

            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                if (HospitalInfoID > 0)
                {
                    objectDSHospitalInfo = (from hi in _objectRMCDataContext.DynamicDatas
                                            where hi.ID == HospitalInfoID
                                            select hi).ToList<RMC.DataService.DynamicData>();
                    if (objectDSHospitalInfo == null)
                    {
                        return false;
                    }
                    else if (objectDSHospitalInfo.Count == 0)
                    {
                        GetColumnNameByHospitalInfo(HospitalInfoID, objectBEDynamicHospitalInfo);
                    }
                    else
                    {
                        foreach (RMC.DataService.DynamicData objectDynamicData in objectDSHospitalInfo)
                        {
                            if (objectDynamicData.ColumnID == 1)
                            {
                                objectDynamicData.Value = objectBEDynamicHospitalInfo.BedsInHospital;
                            }
                            else if (objectDynamicData.ColumnID == 2)
                            {
                                objectDynamicData.Value = objectBEDynamicHospitalInfo.OwnershipType;
                            }
                            else if (objectDynamicData.ColumnID == 3)
                            {
                                objectDynamicData.Value = objectBEDynamicHospitalInfo.HospitalType;
                            }
                        }
                        _objectRMCDataContext.SubmitChanges();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", " public List<RMC.DataService.UserInfo> UpdateDynamicFieldData()");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                objectDSHospitalInfo = null;
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// To Delete user detail
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// Created By : Raman
        /// Creation Date : July 20, 2009       
        public bool DeleteHospitalInfo(int hospitalInfoId, string ActiveUser)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            try
            {
                //Logically Deleteing Data from MultiUserHospital Table.

                List<RMC.DataService.MultiUserHospital> objectMultiUserHospitalList = (from muh in _objectRMCDataContext.MultiUserHospitals
                                                                                       where muh.HospitalInfoID == hospitalInfoId
                                                                                       select muh).ToList();
                if (objectMultiUserHospitalList != null)
                {
                    if (objectMultiUserHospitalList.Count > 0)
                    {

                        objectMultiUserHospitalList.ForEach(muh =>
                        {
                            muh.IsDeleted = true;
                            muh.DeletedBy = ActiveUser;
                            muh.DeletedDate = DateTime.Now;

                        });
                    }
                }


                //Deleting the MultiUserDemographic Data from table.
                List<RMC.DataService.MultiUserDemographic> objectMultiUserDemographicList = (from mud in _objectRMCDataContext.MultiUserDemographics
                                                                                             where mud.HospitalDemographicInfo.HospitalDemographicID == hospitalInfoId
                                                                                             select mud).ToList();
                if (objectMultiUserDemographicList != null)
                {
                    if (objectMultiUserDemographicList.Count > 0)
                    {
                        objectMultiUserDemographicList.ForEach(mud =>
                        {

                            //Deleting Date from Multiuser Demogarphic.
                            mud.IsDeleted = true;
                            mud.DeletedBy = ActiveUser;
                            mud.CreatedDate = DateTime.Now;
                        });
                    }
                }


                //Deleteting the HospitalDemographicInfo.
                List<RMC.DataService.HospitalDemographicInfo> objectHospitalDemographicInfoList = (from hdl in _objectRMCDataContext.HospitalDemographicInfos
                                                                                                   where hdl.HospitalInfoID == hospitalInfoId
                                                                                                   select hdl).ToList();
                if (objectHospitalDemographicInfoList != null)
                {
                    if (objectHospitalDemographicInfoList.Count > 0)
                    {
                        objectHospitalDemographicInfoList.ForEach(hdl =>
                        {
                            hdl.IsDeleted = true;
                            hdl.DeletedBy = ActiveUser;
                            hdl.DeletedDate = DateTime.Now;
                        });
                    }
                }

                //Deleting the hospital.

                RMC.DataService.HospitalInfo objectHospitalInfo = (from hi in _objectRMCDataContext.HospitalInfos
                                                                   where hi.HospitalInfoID == hospitalInfoId
                                                                   select hi).FirstOrDefault();
                if (objectHospitalInfo != null)
                {
                    objectHospitalInfo.IsDeleted = true;
                    objectHospitalInfo.DeletedDate = DateTime.Now;
                    objectHospitalInfo.DeletedBy = ActiveUser;
                }
                _objectRMCDataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdateUserInformation");
                ex.Data.Add("Class", "BSUserInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }
        /// <summary>
        /// Check if Hospital has already been added
        /// Created By : Raman.
        /// Creation Date : July 22, 2009.
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        public bool ExistHospital(string hospitalName, string city)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                var existHospitalInfo = (from hi in _objectRMCDataContext.HospitalInfos
                                         where hi.HospitalName.Trim().ToLower() == hospitalName.Trim().ToLower()
                                         && hi.City.Trim().ToLower() == city.Trim().ToLower()
                                         //&& (hi.IsDeleted ?? false) == false
                                         select hi).FirstOrDefault();
                if (existHospitalInfo == null)
                {
                    return false;
                }
                else
                {
                    if (Convert.ToBoolean(existHospitalInfo.IsDeleted))
                    {
                        DeletePhysicallyRelatedRecord(existHospitalInfo.HospitalInfoID);
                        _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                        existHospitalInfo = (from hi in _objectRMCDataContext.HospitalInfos
                                             where hi.HospitalName.Trim().ToLower() == hospitalName.Trim().ToLower()
                                             && hi.City.Trim().ToLower() == city.Trim().ToLower()
                                             //&& (hi.IsDeleted ?? false) == false
                                             select hi).FirstOrDefault();
                        _objectRMCDataContext.HospitalInfos.DeleteOnSubmit(existHospitalInfo);

                        _objectRMCDataContext.SubmitChanges();
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "public bool ExistHospital(string hospitalName, string city)");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }
        ///// <summary>
        ///// <Description>To return HospitalInfo</Description>
        ///// <Author>Raman</Author>
        ///// <CreatedOn>July 23, 2009</CreatedOn>
        ///// </summary>
        ///// <param name="hospitalInfoId"></param>
        ///// <returns></returns>
        //public RMC.BusinessEntities.BEHospitalInfo GetUsersByHospital(int hospitalInfoId)
        //{

        //    _objectRMCDataContext = new RMC.DataService.RMCDataContext();
        //    try
        //    {
        //        RMC.BusinessEntities.BEHospitalInfo objectBEHospitalInfo = (from hi in _objectRMCDataContext.HospitalInfos
        //                                                                    from hi in _objectRMCDataContext.MultiUserHospitals
        //                                                                    where hi.HospitalInfoID == hospitalInfoId && (hi.IsDeleted ?? false) == false
        //                                                                    select new RMC.BusinessEntities.BEHospitalInfo
        //                                                                    {
        //                                                                        HospitalInfoID = hi.HospitalInfoID,
        //                                                                        UserID = hi.UserID,
        //                                                                        HospitalName = hi.HospitalName,
        //                                                                        ChiefNursingOfficerFirstName = hi.ChiefNursingOfficerFirstName,
        //                                                                        ChiefNursingOfficerLastName = hi.ChiefNursingOfficerLastName,
        //                                                                        ChiefNursingOfficerPhone = hi.ChiefNursingOfficerPhone,
        //                                                                        City = hi.City,
        //                                                                        Address = hi.Address,
        //                                                                        CountryID = hi.State.CountryID,
        //                                                                        StateID = hi.StateID,
        //                                                                        IsActive = hi.IsActive,
        //                                                                        Zip = hi.Zip
        //                                                                    }).FirstOrDefault<RMC.BusinessEntities.BEHospitalInfo>();



        //        return objectBEHospitalInfo;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Data.Add("Function", " public List<RMC.DataService.UserInfo> GetHospitalInformation(int hospitalInfoId)");
        //        ex.Data.Add("Class", "BSHospitalInfo");
        //        throw ex;
        //    }
        //    finally
        //    {
        //        _objectRMCDataContext.Dispose();
        //    }
        //}

        /// <summary>
        /// To Delete  hospital   all information
        /// </summary>
        /// <param name="hospitalDemographicId"></param>
        /// <param name="ActiveUser"></param>
        /// <returns></returns>
        /// Created By : Raman
        /// Creation Date : August 18, 2009       
        public bool LogicalDeleteHospitalInfo(int hospitalInfo, string ActiveUser)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            try
            {
                int superAdminID = (from ui in _objectRMCDataContext.UserInfos
                                    where ui.Email.ToLower().Trim() == "superadmin"
                                    select ui).FirstOrDefault().UserID;
                //Delete Hospital Information .
                RMC.DataService.HospitalInfo objectHospitalInfo = (from hi in _objectRMCDataContext.HospitalInfos
                                                                   where hi.HospitalInfoID == hospitalInfo
                                                                   select hi).FirstOrDefault();
                if (objectHospitalInfo != null)
                {
                    objectHospitalInfo.IsDeleted = true;
                    objectHospitalInfo.DeletedBy = ActiveUser;
                    objectHospitalInfo.DeletedDate = DateTime.Now;
                    objectHospitalInfo.UserID = superAdminID;
                }


                //Deleting the MultiUserHospital Data
                List<RMC.DataService.MultiUserHospital> objectMultiUserHospitalList = (from muh in _objectRMCDataContext.MultiUserHospitals
                                                                                       where muh.HospitalInfoID == hospitalInfo
                                                                                       select muh).ToList();
                if (objectMultiUserHospitalList != null)
                {
                    if (objectMultiUserHospitalList.Count > 0)
                    {
                        foreach (RMC.DataService.MultiUserHospital objectmuh in objectMultiUserHospitalList)
                        {
                            objectmuh.IsDeleted = true;
                            objectmuh.DeletedBy = ActiveUser;
                            objectmuh.DeletedDate = DateTime.Now;
                            objectmuh.UserID = superAdminID;
                        }

                    }
                }


                //Deleting the HospitalDemographicInfo 
                List<RMC.DataService.HospitalDemographicInfo> objectHospitalDemographicInfoList = (from hdi in _objectRMCDataContext.HospitalDemographicInfos
                                                                                                   where hdi.HospitalInfoID == hospitalInfo
                                                                                                   select hdi).ToList();
                if (objectHospitalDemographicInfoList != null)
                {
                    if (objectMultiUserHospitalList.Count > 0)
                    {
                        foreach (RMC.DataService.HospitalDemographicInfo objecthdi in objectHospitalDemographicInfoList)
                        {
                            objecthdi.IsDeleted = true;
                            objecthdi.DeletedDate = DateTime.Now;
                            objecthdi.DeletedBy = ActiveUser;


                            //Deleting the MultiUserDemographic Data
                            List<RMC.DataService.MultiUserDemographic> objectMultiUserDemographicList = (from mud in _objectRMCDataContext.MultiUserDemographics
                                                                                                         where mud.HospitalDemographicID == objecthdi.HospitalDemographicID
                                                                                                         select mud).ToList();

                            if (objectMultiUserDemographicList != null)
                            {
                                if (objectMultiUserDemographicList.Count > 0)
                                {
                                    foreach (RMC.DataService.MultiUserDemographic objectmud in objectMultiUserDemographicList)
                                    {
                                        objectmud.IsDeleted = true;
                                        objectmud.DeletedBy = ActiveUser;
                                        objectmud.DeletedDate = DateTime.Now;
                                        objectmud.UserID = superAdminID;
                                    }

                                }
                            }


                            //Deleting the HospitalUpload Data
                            List<RMC.DataService.HospitalUpload> objectHostpitalUpload = (from hu in _objectRMCDataContext.HospitalUploads
                                                                                          where hu.HospitalDemographicID == objecthdi.HospitalDemographicID
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
                                                                                     where npi.HospitalDemographicID == objecthdi.HospitalDemographicID
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

                        }
                    }
                }


                _objectRMCDataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "public bool LogicalDeleteHospitalInfo(int hospitalInfo, string ActiveUser)");
                ex.Data.Add("Class", "BSHospitalDemographicDetail");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public bool DeletePhysicallyRelatedRecord(int hospitalInfoID)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            try
            {
                //Deleting the MultiUserHospital Data
                List<RMC.DataService.MultiUserHospital> objectMultiUserHospitalList = (from muh in _objectRMCDataContext.MultiUserHospitals
                                                                                       where muh.HospitalInfoID == hospitalInfoID
                                                                                       select muh).ToList();

                if (objectMultiUserHospitalList != null)
                {
                    _objectRMCDataContext.MultiUserHospitals.DeleteAllOnSubmit(objectMultiUserHospitalList);
                }

                //Deleting the HospitalDemographicInfo 
                List<RMC.DataService.HospitalDemographicInfo> objectHospitalDemographicInfoList = (from hdi in _objectRMCDataContext.HospitalDemographicInfos
                                                                                                   where hdi.HospitalInfoID == hospitalInfoID
                                                                                                   select hdi).ToList();
                if (objectHospitalDemographicInfoList != null)
                {
                    _objectRMCDataContext.HospitalDemographicInfos.DeleteAllOnSubmit(objectHospitalDemographicInfoList);

                    foreach (RMC.DataService.HospitalDemographicInfo objectHospitalDemographicInfo in objectHospitalDemographicInfoList)
                    {
                        //Deleting the MultiUserDemographic Data
                        List<RMC.DataService.MultiUserDemographic> objectMultiUserDemographicList = (from mud in _objectRMCDataContext.MultiUserDemographics
                                                                                                     where mud.HospitalDemographicID == objectHospitalDemographicInfo.HospitalDemographicID
                                                                                                     select mud).ToList();

                        if (objectMultiUserDemographicList != null)
                        {
                            _objectRMCDataContext.MultiUserDemographics.DeleteAllOnSubmit(objectMultiUserDemographicList);
                        }

                        //Deleting the HospitalUpload Data
                        List<RMC.DataService.HospitalUpload> objectHostpitalUpload = (from hu in _objectRMCDataContext.HospitalUploads
                                                                                      where hu.HospitalDemographicID == objectHospitalDemographicInfo.HospitalDemographicID
                                                                                      select hu).ToList();

                        if (objectHostpitalUpload != null)
                        {
                            _objectRMCDataContext.HospitalUploads.DeleteAllOnSubmit(objectHostpitalUpload);
                        }

                        //Deleting the NursePDAInfo Data
                        List<RMC.DataService.NursePDAInfo> objectNursePdaInfo = (from npi in _objectRMCDataContext.NursePDAInfos
                                                                                 where npi.HospitalDemographicID == objectHospitalDemographicInfo.HospitalDemographicID
                                                                                 select npi).ToList();

                        if (objectNursePdaInfo != null)
                        {
                            _objectRMCDataContext.NursePDAInfos.DeleteAllOnSubmit(objectNursePdaInfo);

                            foreach (RMC.DataService.NursePDAInfo objectNursePDAInfo in objectNursePdaInfo)
                            {
                                //Deleting the NursePDADetail Data
                                List<RMC.DataService.NursePDADetail> objectNursePDADetail = (from npd in _objectRMCDataContext.NursePDADetails
                                                                                             where npd.NurseID == objectNursePDAInfo.NurseID
                                                                                             select npd).ToList();

                                if (objectNursePDADetail != null)
                                {
                                    _objectRMCDataContext.NursePDADetails.DeleteAllOnSubmit(objectNursePDADetail);
                                }
                            }
                        }
                    }
                }

                _objectRMCDataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "public bool LogicalDeleteHospitalInfo(int hospitalInfo, string ActiveUser)");
                ex.Data.Add("Class", "BSHospitalDemographicDetail");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public bool DeletePhysicallyHospitalInfoRelatedRecord(int hospitalInfoID)
        {
            bool Flag = false;
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            try
            {
                //Deleting the HospitalInfo Data
                RMC.DataService.HospitalInfo objectHospitalInfo = (from hi in _objectRMCDataContext.HospitalInfos
                                                                   where hi.HospitalInfoID == hospitalInfoID
                                                                   select hi).FirstOrDefault();
                if (objectHospitalInfo != null)
                {
                    //Deleting the HospitalUpload Data
                    List<RMC.DataService.HospitalUpload> objectGenericHostpitalUpload = (from hu in _objectRMCDataContext.HospitalUploads
                                                                                         where hu.HospitalDemographicInfo.HospitalInfoID == hospitalInfoID
                                                                                         select hu).ToList();

                    foreach (RMC.DataService.HospitalUpload objectHospUpload in objectGenericHostpitalUpload)
                    {
                        if (System.IO.File.Exists(objectHospUpload.FilePath))
                        {
                            System.IO.File.Delete(objectHospUpload.FilePath);
                        }
                    }

                    _objectRMCDataContext.HospitalInfos.DeleteOnSubmit(objectHospitalInfo);
                    _objectRMCDataContext.SubmitChanges();
                    Flag = true;
                }

                return Flag;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeletePhysicallyHospitalInfoRelatedRecord");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public bool DeletePhysicallyHospitalUnitRelatedRecord(int hospitalUnitID)
        {
            bool Flag = false;
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            try
            {

                //Deleting the HospitalDemographicInfo 
                RMC.DataService.HospitalDemographicInfo objectHospitalDemographicInfo = (from hdi in _objectRMCDataContext.HospitalDemographicInfos
                                                                                         where hdi.HospitalDemographicID == hospitalUnitID
                                                                                         select hdi).SingleOrDefault();
                if (objectHospitalDemographicInfo != null)
                {
                    //Deleting the HospitalUpload Data
                    List<RMC.DataService.HospitalUpload> objectGenericHostpitalUpload = (from hu in _objectRMCDataContext.HospitalUploads
                                                                                         where hu.HospitalDemographicID == hospitalUnitID
                                                                                         select hu).ToList();

                    foreach (RMC.DataService.HospitalUpload objectHospUpload in objectGenericHostpitalUpload)
                    {
                        if (System.IO.File.Exists(objectHospUpload.FilePath))
                        {
                            System.IO.File.Delete(objectHospUpload.FilePath);
                        }
                    }

                    _objectRMCDataContext.HospitalDemographicInfos.DeleteOnSubmit(objectHospitalDemographicInfo);
                    _objectRMCDataContext.SubmitChanges();
                    Flag = true;
                }

                return Flag;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeletePhysicallyHospitalUnitRelatedRecord");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public bool CheckForHospitalExistence(int userID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.DataService.MultiUserHospital> existHospitalInfo = (from hi in _objectRMCDataContext.MultiUserHospitals
                                                                             where hi.UserID == userID && hi.IsDeleted == false
                                                                             select hi).ToList<RMC.DataService.MultiUserHospital>();

                if (existHospitalInfo.Count > 0)
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
                ex.Data.Add("Function", "public bool ExistHospital(string hospitalName, string city)");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public bool CheckForHospitalExistenceByUserID(int userID, int hospitalInfoID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.DataService.MultiUserHospital> existHospitalInfo = (from hi in _objectRMCDataContext.MultiUserHospitals
                                                                             where hi.UserID == userID && hi.HospitalInfoID == hospitalInfoID && hi.IsDeleted == false
                                                                             select hi).ToList<RMC.DataService.MultiUserHospital>();

                if (existHospitalInfo.Count > 0)
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
                ex.Data.Add("Function", "CheckForHospitalExistenceByUserID");
                ex.Data.Add("Class", "BSHospitalInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        public void UpdateIsCollapseField(int hospitalInfoID, bool IsCollapse, string userRole, int userID)
        {
            try
            {
                RMC.DataService.HospitalInfo objectOriginalHospitalInfo = null;
                RMC.DataService.MultiUserHospital objectMultiUserHospital = null;
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                if (userRole.ToLower().Trim() == "superadmin")
                {
                    objectOriginalHospitalInfo = _objectRMCDataContext.HospitalInfos.Single(h => h.HospitalInfoID == hospitalInfoID);
                    objectOriginalHospitalInfo.IsCollapse = IsCollapse;
                }
                else
                {
                    objectMultiUserHospital = _objectRMCDataContext.MultiUserHospitals.Single(h => h.HospitalInfoID == hospitalInfoID && h.UserID == userID);
                    objectMultiUserHospital.IsCollapse = IsCollapse;
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

        public bool UpdateIndexInHospitalInfo(int hospitalInfoID, int index)
        {
            bool flag = false;
            try
            {
                RMC.DataService.HospitalInfo objectOriginalHospitalInfo = null;
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                objectOriginalHospitalInfo = _objectRMCDataContext.HospitalInfos.Single(h => h.HospitalInfoID == hospitalInfoID);
                objectOriginalHospitalInfo.RecordCounter = index;

                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
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

    //End Of BSHospitalInfo Class
}
//End Of NameSpace