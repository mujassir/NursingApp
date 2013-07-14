using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using System.Data.Linq.SqlClient;
using RMC.DataService;

namespace RMC.BussinessService
{
    public class BSUsers
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        //Bussiness Service Objects.
        RMC.BussinessService.BSEmailNotificationBody _objectBSEmailNotificationBody = null;
        RMC.BussinessService.BSEmail _objectBSEmail = null;

        //Fundamental Data Types.
        bool _flag, _emailFlag;
        string _bodyText, _fromAddress, _toAddress;

        #endregion

        #region Functions/Methods

        /// <summary>
        /// Insert User Infomation.
        /// Created By : Davinder Kumar.
        /// Creation Date : June 25, 2009
        /// </summary>
        /// <param name="hospitalInfo"></param>
        /// <returns></returns>
        public bool InsertUsersInfomation(RMC.DataService.UserInfo userInfo, out int userInfoID)
        {
            _flag = false;
            userInfoID = 0;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                _objectRMCDataContext.UserInfos.InsertOnSubmit(userInfo);
                _objectRMCDataContext.SubmitChanges();
                userInfoID = userInfo.UserID;
                _flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertUsersInfomation");
                ex.Data.Add("Class", "BSUsers");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return _flag;
        }

        /// <summary>
        /// Insert User Infomation.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 10, 2009
        /// </summary>
        /// <param name="hospitalInfo"></param>
        /// <returns></returns>
        public bool InsertUsersInfomation(RMC.DataService.UserInfo primaryUserInfo)
        {
            _flag = false;

            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _objectBSEmailNotificationBody = new BSEmailNotificationBody();
                _fromAddress = ConfigurationManager.AppSettings["fromAddress"].ToString();
                _toAddress = ConfigurationManager.AppSettings["superAdminAddress"].ToString();
                _objectRMCDataContext.UserInfos.InsertOnSubmit(primaryUserInfo);
                _objectRMCDataContext.SubmitChanges();
                _flag = true;
                ////Body Text of Email.
                //_bodyText = Convert.ToString(_objectBSEmailNotificationBody.GetEmailBodyOfUserRegistration(primaryUserInfo));
                ////Send Email.
                //_objectBSEmail = new BSEmail(_fromAddress, _toAddress, "New User Registered", _bodyText, true);
                //_objectBSEmail.SendMail(true, out _emailFlag);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertUsersInfomation");
                ex.Data.Add("Class", "BSUsers");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return _flag;
        }

        public bool InsertUsersInfomationByUserID(RMC.DataService.UserInfo primaryUserInfo, out int userID)
        {
            _flag = false;

            //try
            //{
                RMCDataContext _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                BSEmailNotificationBody  _objectBSEmailNotificationBody = new BSEmailNotificationBody();
                _fromAddress = ConfigurationManager.AppSettings["fromAddress"].ToString();
                _toAddress = ConfigurationManager.AppSettings["superAdminAddress"].ToString();

                _objectRMCDataContext.UserInfos.InsertOnSubmit(primaryUserInfo);
                _objectRMCDataContext.SubmitChanges();
                _flag = true;
                userID = primaryUserInfo.UserID;
                //Body Text of Email.
                _bodyText = Convert.ToString(_objectBSEmailNotificationBody.GetEmailBodyOfUserRegistration(primaryUserInfo));
                //Send Email.
                _objectBSEmail = new BSEmail(_fromAddress, _toAddress, "New User Registered", _bodyText, true);
                _objectBSEmail.SendMail(true, out _emailFlag);
            //}
            //catch (Exception ex)
            //{
            //    ex.Data.Add("Function", "InsertUsersInfomation");
            //    ex.Data.Add("Class", "BSUsers");
            //    throw ex;
            //}
            //finally
            //{
            //    _objectRMCDataContext.Dispose();
            //}

            return _flag;
        }

        /// <summary>
        /// Insert User Infomation.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 10, 2009
        /// </summary>
        /// <param name="hospitalInfo"></param>
        /// <returns></returns>
        public bool InsertUsersInfomation(RMC.DataService.UserInfo primaryUserInfo, RMC.DataService.MultiUserDemographic multiUserDemographic)
        {
            _flag = false;

            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _objectBSEmailNotificationBody = new BSEmailNotificationBody();
                _fromAddress = ConfigurationManager.AppSettings["fromAddress"].ToString();
                _toAddress = ConfigurationManager.AppSettings["superAdminAddress"].ToString();

                primaryUserInfo.MultiUserDemographics.Add(multiUserDemographic);
                _objectRMCDataContext.UserInfos.InsertOnSubmit(primaryUserInfo);
                _objectRMCDataContext.SubmitChanges();
                _flag = true;
                ////Body Text of Email.
                //_bodyText = Convert.ToString(_objectBSEmailNotificationBody.GetEmailBodyOfUserRegistration(primaryUserInfo));
                ////Send Email.
                //_objectBSEmail = new BSEmail(_fromAddress, _toAddress, "New User Registered", _bodyText, true);
                //_objectBSEmail.SendMail(true, out _emailFlag);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertUsersInfomation");
                ex.Data.Add("Class", "BSUsers");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return _flag;
        }

        /// <summary>
        /// To Update user detail
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// Created By : Raman
        /// Creation Date : July 20, 2009       
        public RMC.DataService.UserInfo GetUserInformation(int userId)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            RMC.DataService.UserInfo objectUserInfo = null;
            try
            {
                objectUserInfo = (from ui in _objectRMCDataContext.UserInfos
                                  where ui.UserID == userId && (ui.IsDeleted ?? false) == false
                                  select ui).FirstOrDefault();

                return objectUserInfo;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdateUserInformation");
                ex.Data.Add("Class", "BSUserInfo");
                throw ex;
            }
            finally
            {
                objectUserInfo = null;
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// To Check Hospital Units
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// Created By : Bharat
        /// Creation Date : Nov 12, 2009       
        public bool CheckHospitalUnitsByHospitalInfoID(int hospitalInfoId)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            List<RMC.DataService.MultiUserDemographic> objectGenericMultiUserDemographic = null;
            objectGenericMultiUserDemographic = (from mud in _objectRMCDataContext.MultiUserDemographics
                                                 where mud.HospitalDemographicInfo.IsDeleted == false && mud.HospitalDemographicInfo.HospitalInfoID == hospitalInfoId && mud.UserInfo.IsActive == true
                                                 select mud).Distinct().ToList();

            if (objectGenericMultiUserDemographic.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get all user in Hospital Registration for OwnerName.
        /// Created By : Davinder Kumar.
        /// Creation Date : Aug 08, 2009.
        /// </summary>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEUserInfomation> GetUsersByHospitalInfoID(int hospitalInfoID)
        {
            List<RMC.BusinessEntities.BEUserInfomation> objectGenericBEUserInfomation = null;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                // Commented By Mahesh Sachdeva To populate users from the MultiUserHospital table,every time when New unit is created
                // if (CheckHospitalUnitsByHospitalInfoID(hospitalInfoID))
                // {
                // objectGenericBEUserInfomation = (from mud in _objectRMCDataContext.MultiUserDemographics
                //                                where mud.HospitalDemographicInfo.IsDeleted == false && mud.HospitalDemographicInfo.HospitalInfoID == hospitalInfoID && mud.UserInfo.IsActive == true
                //                                orderby mud.UserInfo.FirstName, mud.UserInfo.LastName
                //                               select new RMC.BusinessEntities.BEUserInfomation
                //                              {
                //                                 UserName = mud.UserInfo.FirstName + " " + mud.UserInfo.LastName + " (" + mud.UserInfo.Email + ")",
                //                                Email = mud.UserInfo.Email,
                //                               UserID = mud.UserInfo.UserID
                //                          }).Distinct().ToList<RMC.BusinessEntities.BEUserInfomation>();
                //}
                // else
                //{
                objectGenericBEUserInfomation = (from muh in _objectRMCDataContext.MultiUserHospitals
                                                 where muh.HospitalInfo.IsDeleted == false && muh.HospitalInfo.HospitalInfoID == hospitalInfoID && muh.UserInfo.IsActive == true
                                                 orderby muh.UserInfo.FirstName, muh.UserInfo.LastName
                                                 select new RMC.BusinessEntities.BEUserInfomation
                                                 {
                                                     UserName = muh.UserInfo.FirstName + " " + muh.UserInfo.LastName + " (" + muh.UserInfo.Email + ")",
                                                     Email = muh.UserInfo.Email,
                                                     UserID = muh.UserInfo.UserID
                                                 }).Distinct().ToList<RMC.BusinessEntities.BEUserInfomation>();

                //}
                return objectGenericBEUserInfomation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllUsersExceptSuperAdmin");
                ex.Data.Add("Class", "BSUserInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Get UserName.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetUserNameByUserID(int userID)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            RMC.DataService.UserInfo objectUserInfo = null;
            try
            {
                objectUserInfo = (from ui in _objectRMCDataContext.UserInfos
                                  where ui.UserID == userID && (ui.IsDeleted ?? false) == false
                                  select ui).FirstOrDefault();
                if (objectUserInfo != null)
                {
                    return objectUserInfo.FirstName + " " + objectUserInfo.LastName;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetUserNameByUserID");
                ex.Data.Add("Class", "BSUserInfo");
                throw ex;
            }
            finally
            {
                objectUserInfo = null;
                _objectRMCDataContext.Dispose();
            }
        }

        // added for email functionality
        public string GetUserEmailByUserID(int userID)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            RMC.DataService.UserInfo objectUserInfo = null;
            try
            {
                objectUserInfo = (from ui in _objectRMCDataContext.UserInfos
                                  where ui.UserID == userID && (ui.IsDeleted ?? false) == false
                                  select ui).FirstOrDefault();
                if (objectUserInfo != null)
                {
                    return objectUserInfo.Email;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetUserNameByUserID");
                ex.Data.Add("Class", "BSUserInfo");
                throw ex;
            }
            finally
            {
                objectUserInfo = null;
                _objectRMCDataContext.Dispose();
            }
        }


        // added for email functionality


        /// <summary>
        /// Get Hospital Creator.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 29, 2009.
        /// </summary>
        /// <param name="hospitalInfoID"></param>
        /// <returns></returns>
        public int GetUserIDByHospitalID(int hospitalInfoID)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();

            try
            {
                int userID = (from hi in _objectRMCDataContext.HospitalInfos
                              where hi.HospitalInfoID == hospitalInfoID
                              select hi).FirstOrDefault().UserID;

                return userID;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetUserIDByHospitalID");
                ex.Data.Add("Class", "BSUserInfo");
                throw ex;
            }
        }

        public List<int?> GetUserIDByHospitalUnitID(int hospitalUnitID)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();

            try
            {
                // Commented by Mahesh Sachdeva
                //int userID = (int)(from hi in _objectRMCDataContext.MultiUserDemographics
                //                   where hi.HospitalDemographicID == hospitalUnitID && hi.PermissionID == 1
                //                   select hi).FirstOrDefault().UserID;

                // Added by Mahesh Sachdeva to get all owners of Unit
                List<int?> userID = (from hi in _objectRMCDataContext.MultiUserDemographics
                                     where hi.HospitalDemographicID == hospitalUnitID && hi.PermissionID == 1
                                     select hi.UserID).ToList();
                return userID;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetUserIDByHospitalID");
                ex.Data.Add("Class", "BSUserInfo");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEUserInfomation> GetAllUsersExceptSuperAdmin(string search, int hospitalUnitID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BEUserInfomation> userInformation = null;


                List<int> userIDs = (from mdh in _objectRMCDataContext.MultiUserDemographics
                                     where mdh.HospitalDemographicID == hospitalUnitID && mdh.IsDeleted == false
                                     select Convert.ToInt32(mdh.UserID)).ToList();

                if (search == 0.ToString())
                {
                    userInformation = (from ui in _objectRMCDataContext.UserInfos
                                       where ui.IsDeleted == false && ui.Email.ToLower().Trim() != "superadmin"  // && userIDs.Contains(Convert.ToInt32(ui.UserID)) == false
                                       orderby ui.FirstName, ui.LastName, ui.Email
                                       select new RMC.BusinessEntities.BEUserInfomation
                                       {
                                           UserName = ui.FirstName + " " + ui.LastName + " (Email : " + ui.Email + ")",
                                           UserID = ui.UserID,
                                           Email = ui.Email
                                       }).ToList<RMC.BusinessEntities.BEUserInfomation>();
                }
                else
                {
                    userInformation = (from ui in _objectRMCDataContext.UserInfos
                                       where ui.IsDeleted == false && (ui.FirstName + " " + ui.LastName + " " + ui.Email).ToLower().Trim().Contains(search.ToLower().Trim()) && ui.Email.ToLower().Trim() != "superadmin" && userIDs.Contains(Convert.ToInt32(ui.UserID)) == false
                                       orderby ui.FirstName, ui.LastName, ui.Email
                                       select new RMC.BusinessEntities.BEUserInfomation
                                       {
                                           UserName = ui.FirstName + " " + ui.LastName + " (Email : " + ui.Email + ")",
                                           UserID = ui.UserID,
                                           Email = ui.Email
                                       }).ToList<RMC.BusinessEntities.BEUserInfomation>();
                }
                return userInformation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetUserIDByHospitalID");
                ex.Data.Add("Class", "BSUserInfo");
                throw ex;
            }
        }

        public List<RMC.BusinessEntities.BEUserInfomation> GetAllUsersForHospApprovalExceptSuperAdmin(string search, int hospitalInfoID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                List<RMC.BusinessEntities.BEUserInfomation> userInformation = null;

                List<int> userIDs = (from mdh in _objectRMCDataContext.MultiUserHospitals
                                     where mdh.HospitalInfoID == hospitalInfoID && mdh.IsDeleted == false
                                     select Convert.ToInt32(mdh.UserID)).ToList();

                //string ownerEmailID = (from hi in _objectRMCDataContext.HospitalInfos
                //                       where hi.HospitalInfoID == hospitalInfoID && hi.IsDeleted == false
                //                       select hi.UserInfo.Email).SingleOrDefault();

                if (search == 0.ToString())
                {
                    userInformation = (from ui in _objectRMCDataContext.UserInfos
                                       where ui.IsDeleted == false && ui.Email.ToLower().Trim() != "superadmin" // && userIDs.Contains(Convert.ToInt32(ui.UserID)) == false
                                       orderby ui.FirstName, ui.LastName, ui.Email
                                       select new RMC.BusinessEntities.BEUserInfomation
                                       {
                                           UserName = ui.FirstName + " " + ui.LastName + " (Email : " + ui.Email + ")",
                                           UserID = ui.UserID,
                                           Email = ui.Email
                                       }).ToList<RMC.BusinessEntities.BEUserInfomation>();

                }
                else
                {
                    userInformation = (from ui in _objectRMCDataContext.UserInfos
                                       where ui.IsDeleted == false && (ui.FirstName + " " + ui.LastName + " " + ui.Email).ToLower().Trim().Contains(search.ToLower().Trim()) && ui.Email.ToLower().Trim() != "superadmin" && userIDs.Contains(Convert.ToInt32(ui.UserID)) == false
                                       orderby ui.FirstName, ui.LastName, ui.Email
                                       select new RMC.BusinessEntities.BEUserInfomation
                                       {
                                           UserName = ui.FirstName + " " + ui.LastName + " (Email : " + ui.Email + ")",
                                           UserID = ui.UserID,
                                           Email = ui.Email
                                       }).ToList<RMC.BusinessEntities.BEUserInfomation>();
                }
                return userInformation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetUserIDByHospitalID");
                ex.Data.Add("Class", "BSUserInfo");
                throw ex;
            }
        }

        /// <summary>
        /// Get User Information By FirstName, LastName and Email.
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEUserInfomation> GetUserInformationBySearch(string search)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();

            try
            {
                List<RMC.BusinessEntities.BEUserInfomation> userInformation = null;
                if (search == 0.ToString())
                {
                    userInformation = (from ui in _objectRMCDataContext.UserInfos
                                       where ui.IsDeleted == false
                                       orderby ui.FirstName, ui.LastName, ui.Email
                                       select new RMC.BusinessEntities.BEUserInfomation
                                       {
                                           UserName = ui.FirstName + " " + ui.LastName + " (" + ui.Email + ")",
                                           UserID = ui.UserID,
                                           Email = ui.Email
                                       }).ToList<RMC.BusinessEntities.BEUserInfomation>();
                }
                else
                {
                    userInformation = (from ui in _objectRMCDataContext.UserInfos
                                       where ui.IsDeleted == false && (ui.FirstName + " " + ui.LastName + " " + ui.Email).ToLower().Trim().Contains(search.ToLower().Trim())
                                       orderby ui.FirstName, ui.LastName, ui.Email
                                       select new RMC.BusinessEntities.BEUserInfomation
                                       {
                                           UserName = ui.FirstName + " " + ui.LastName + " (" + ui.Email + ")",
                                           UserID = ui.UserID,
                                           Email = ui.Email
                                       }).ToList<RMC.BusinessEntities.BEUserInfomation>();
                }
                return userInformation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetUserIDByHospitalID");
                ex.Data.Add("Class", "BSUserInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Use in a NewsLetter.ascx, filter according to Owner and Users
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEUserInfomation> FilterUserInformation(string filter)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();

            try
            {
                List<RMC.BusinessEntities.BEUserInfomation> userInformation = null;
                if (filter == 0.ToString())
                {
                    userInformation = (from ui in _objectRMCDataContext.UserInfos
                                       where ui.IsDeleted == false && (ui.Email.ToLower().Trim() != "superadmin")
                                       orderby ui.FirstName, ui.LastName, ui.Email
                                       select new RMC.BusinessEntities.BEUserInfomation
                                       {
                                           UserName = ui.FirstName + " " + ui.LastName + " (" + ui.Email + ")",
                                           UserID = ui.UserID,
                                           Email = ui.Email
                                       }).ToList<RMC.BusinessEntities.BEUserInfomation>();
                }
                else
                {
                    userInformation = (from ui in _objectRMCDataContext.MultiUserHospitals
                                       where ui.UserInfo.IsDeleted == false && (ui.PermissionID == 1 && ui.UserInfo.Email.ToLower().Trim() != "superadmin")
                                       orderby ui.UserInfo.FirstName, ui.UserInfo.LastName, ui.UserInfo.Email
                                       select new RMC.BusinessEntities.BEUserInfomation
                                       {
                                           UserName = ui.UserInfo.FirstName + " " + ui.UserInfo.LastName + " (" + ui.UserInfo.Email + ")",
                                           UserID = ui.UserInfo.UserID,
                                           Email = ui.UserInfo.Email
                                       }).ToList<RMC.BusinessEntities.BEUserInfomation>();
                }
                return userInformation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "FilterUserInformation");
                ex.Data.Add("Class", "BSUserInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// To Update user detail
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// Created By : Raman
        /// Creation Date : July 20, 2009       
        public bool UpdateUserInformation(RMC.DataService.UserInfo userInfo)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            RMC.DataService.UserInfo objectUserInfo = null;
            try
            {
                if (userInfo.UserID > 0)
                {
                    objectUserInfo = (from ui in _objectRMCDataContext.UserInfos
                                      where ui.UserID == userInfo.UserID
                                      select ui).FirstOrDefault();
                    if (objectUserInfo == null)
                    {
                        return false;
                    }
                }
                else
                {
                    objectUserInfo = new RMC.DataService.UserInfo();
                    objectUserInfo.CreatedDate = DateTime.Now;
                    objectUserInfo.CreatedBy = userInfo.CreatedBy;
                    objectUserInfo.UserTypeID = userInfo.UserTypeID;
                    objectUserInfo.Email = userInfo.Email;
                    objectUserInfo.IsDeleted = false;
                }
                //objectUserInfo.IsActive = userInfo.IsActive;   // Change by Deepakt for removing the Activation Field, becuause this thing is handle from Admin's Home page.
                objectUserInfo.ModifiedBy = userInfo.ModifiedBy;
                objectUserInfo.ModifiedDate = DateTime.Now;
                objectUserInfo.CompanyName = userInfo.CompanyName;
                objectUserInfo.FirstName = userInfo.FirstName;
                objectUserInfo.LastName = userInfo.LastName;
                objectUserInfo.Fax = userInfo.Fax;
                objectUserInfo.Phone = userInfo.Phone;
                objectUserInfo.Password = userInfo.Password;
                objectUserInfo.SecurityQuestion = userInfo.SecurityQuestion;
                objectUserInfo.SecurityAnswer = userInfo.SecurityAnswer;
                objectUserInfo.IsActive = userInfo.IsActive;
                //objectUserInfo.UserActivationRequest = userInfo.UserActivationRequest; // Change Deepakt for removing the Activation Field, becuause this thing is handle from Admin's Home page.
                if (userInfo.UserID <= 0)
                {
                    _objectRMCDataContext.UserInfos.InsertOnSubmit(objectUserInfo);
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
                objectUserInfo = null;
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// To Update User's Activation Request 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// Created By : Ramanb
        /// Creation Date : Sept 24, 2009       
        public bool UpdateUserActivation(List<RMC.DataService.UserInfo> userInfolist, bool IsAcive, string Reuqest)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            try
            {
                //for (int i = 0; i < userInfolist.Count; i++)
                //{
                userInfolist = (from ui in _objectRMCDataContext.UserInfos
                                where ui.UserID == userInfolist.Select(p => p.UserID).FirstOrDefault()
                                //where Convert.ToString(ui.UserID).Contains(userInfolist.[i].UserID.ToString())
                                select ui).ToList<RMC.DataService.UserInfo>();
                //}
                foreach (RMC.DataService.UserInfo objectViewRequest in userInfolist)
                {
                    if (IsAcive == true)
                    {
                        objectViewRequest.IsActive = true;
                    }
                    objectViewRequest.UserActivationRequest = Reuqest;

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
        /// To Delete user detail
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// Created By : Raman
        /// Creation Date : July 20, 2009       
        public bool DeleteUserInformation(int userId, string ActiveUser)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            try
            {
                //Deleting the User from Hospital List.

                List<RMC.DataService.MultiUserHospital> objectMultiUserHospitalList = (from muh in _objectRMCDataContext.MultiUserHospitals
                                                                                       where muh.UserID == userId
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

                //Deleting the users from hospital units.
                List<RMC.DataService.MultiUserDemographic> objectMultiUserDemographicList = (from mud in _objectRMCDataContext.MultiUserDemographics
                                                                                             where mud.UserID == userId
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

                RMC.DataService.UserInfo objectUserInfo = (from ui in _objectRMCDataContext.UserInfos
                                                           where ui.UserID == userId
                                                           select ui).FirstOrDefault();
                objectUserInfo.IsDeleted = true;
                objectUserInfo.DeletedDate = DateTime.Now;
                objectUserInfo.DeletedBy = ActiveUser;
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
        /// Return Users.
        /// Created By : Raman.
        /// Creation Date : July 22, 2009.
        /// </summary>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEUserInfoTye> GetUsers()
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BEUserInfoTye> listBEUserInfoType = (from ui in _objectRMCDataContext.UserInfos
                                                                               where ui.IsDeleted == false
                                                                               select new RMC.BusinessEntities.BEUserInfoTye { UserId = ui.UserID, UserName = (ui.FirstName ?? "") + " " + (ui.LastName ?? ""), Email = (ui.Email ?? ""), UserNameEmail = (ui.FirstName ?? "") + " " + (ui.LastName ?? "") + "(" + (ui.Email ?? "") + ")" }).ToList<RMC.BusinessEntities.BEUserInfoTye>();
                return listBEUserInfoType;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "public List<RMC.DataService.UserInfo> GetUsers()");
                ex.Data.Add("Class", "BSUserInfo");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }
        /// <summary>
        /// Fetch Hospital Demographic Detail By Hospital Information ID.
        /// Created By : Raman.
        /// Creation Date : July 22, 2009.
        /// </summary>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEUserInfoTye> GetUsersHavingPermission(int hospitalInfoId)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                //List<RMC.DataService.UserInfo> listUserInfo = 
                //List<RMC.BusinessEntities.BEUserInfoTye> listBEUserInfoType = 
                //                                                              (from muh in _objectRMCDataContext.MultiUserHospitals.Where(m => m.HospitalInfoID == hospitalInfoId)
                //                                                               from ui in _objectRMCDataContext.UserInfos.Where(ui=>ui.UserID != muh.UserID)
                //                                                               where (muh.IsDeleted ?? false) == false && (ui.IsDeleted ?? false) == false
                //                                                               select  new RMC.BusinessEntities.BEUserInfoTye { UserId = ui.UserID, UserName = (ui.FirstName ?? "") + " " + (ui.LastName ?? ""), Email = (ui.Email ?? ""), UserNameEmail = (ui.FirstName ?? "") + " " + (ui.LastName ?? "") + "(" + (ui.Email ?? "") + ")" }).Distinct().ToList<RMC.BusinessEntities.BEUserInfoTye>();
                int[] ArrayUserID = _objectRMCDataContext.MultiUserHospitals.Where(m => m.HospitalInfoID == hospitalInfoId && m.IsDeleted == false).Select(m => m.UserID.Value).ToArray();
                List<RMC.BusinessEntities.BEUserInfoTye> listBEUserInfoType = _objectRMCDataContext.UserInfos.Where(u => !ArrayUserID.Contains(u.UserID)).Select(ui => new RMC.BusinessEntities.BEUserInfoTye { UserId = ui.UserID, UserName = (ui.FirstName ?? "") + " " + (ui.LastName ?? ""), Email = (ui.Email ?? ""), UserNameEmail = (ui.FirstName ?? "") + " " + (ui.LastName ?? "") + "(" + (ui.Email ?? "") + ")" }).ToList();
                return listBEUserInfoType;

            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "public List<RMC.DataService.UserInfo> GetUsers()");
                ex.Data.Add("Class", "BSUserInfo");
                throw ex;
            }
            finally
            {
                //_objectRMCDataContext.Dispose();
            }
        }
        /// <summary>
        /// Fetch Hospital Demographic user Detail By Hospital Demographic ID.
        /// Created By : Raman.
        /// Creation Date : July 28, 2009.
        /// </summary>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEUserInfoTye> GetUsersHavingPermissionOnUnitsByHospitalDemographicId(int hospitalDemographicId)
        {
            try
            {
                if (hospitalDemographicId != 0)
                {
                    _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                    int[] ArrayUserID = _objectRMCDataContext.MultiUserDemographics.Where(m => m.HospitalDemographicID == hospitalDemographicId && m.IsDeleted == false).Select(m => m.UserID.Value).ToArray();
                    List<RMC.BusinessEntities.BEUserInfoTye> listBEUserInfoType = _objectRMCDataContext.UserInfos.Where(u => !ArrayUserID.Contains(u.UserID)).Select(ui => new RMC.BusinessEntities.BEUserInfoTye { UserId = ui.UserID, UserName = (ui.FirstName ?? "") + " " + (ui.LastName ?? ""), Email = (ui.Email ?? ""), UserNameEmail = (ui.FirstName ?? "") + " " + (ui.LastName ?? "") + "(" + (ui.Email ?? "") + ")" }).Distinct().ToList();
                    return listBEUserInfoType;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "public List<RMC.DataService.UserInfo> GetUsers()");
                ex.Data.Add("Class", "BSUserInfo");
                throw ex;
            }
            finally
            {
                //_objectRMCDataContext.Dispose();
            }
        }
        /// <summary>
        /// Check if email id already exist in userinfos table
        /// Created By : Raman.
        /// Creation Date : July 22, 2009.
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        public bool ExistUserEmailId(string emailId, out bool IsDeleted, out int userID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                var existEmailId = (from ui in _objectRMCDataContext.UserInfos
                                    where ui.Email.Trim().ToLower() == emailId.Trim().ToLower() //&& (ui.IsDeleted ?? false) == false
                                    select ui).FirstOrDefault();
                if (existEmailId != null)
                {
                    IsDeleted = Convert.ToBoolean(existEmailId.IsDeleted);
                    userID = Convert.ToInt32(existEmailId.UserID);

                    if (!IsDeleted)
                    {
                        if (existEmailId == null)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    IsDeleted = false;
                    userID = 0;
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "public bool ExistUserEmailId(string emailId)");
                ex.Data.Add("Class", "BSUsers");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Check if email id already exist in userinfos table
        /// Created By : Raman.
        /// Creation Date : July 22, 2009.
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        public bool ExistUserEmailId(string emailId)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                var existEmailId = (from ui in _objectRMCDataContext.UserInfos
                                    where ui.Email.Trim().ToLower() == emailId.Trim().ToLower() //&& (ui.IsDeleted ?? false) == false
                                    select ui).FirstOrDefault();

                if (existEmailId == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "public bool ExistUserEmailId(string emailId)");
                ex.Data.Add("Class", "BSUsers");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Get Security Questions and Answer for forget Password For,
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        /// Created By : Raman
        /// Creation Date : August 17, 2009       
        public List<RMC.DataService.UserInfo> GetUserSecurityInformation(string userEmail)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            List<RMC.DataService.UserInfo> objectUserInfo = null;
            try
            {
                objectUserInfo = (from ui in _objectRMCDataContext.UserInfos
                                  where ui.Email == userEmail && (ui.IsDeleted ?? false) == false
                                  select ui).ToList<RMC.DataService.UserInfo>();

                return objectUserInfo;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdateUserInformation");
                ex.Data.Add("Class", "BSUserInfo");
                throw ex;
            }
            finally
            {
                objectUserInfo = null;
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Check if email id already exist in userinfos table
        /// Created By : Raman.
        /// Creation Date : August 18, 2009.
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        public bool UpdateUserPassword(string password, int userId)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                var objectUpdatePassword = (from ui in _objectRMCDataContext.UserInfos
                                            where ui.UserID == userId && (ui.IsDeleted ?? false) == false
                                            select ui).FirstOrDefault();
                if (objectUpdatePassword == null)
                {

                    return false;
                }
                else
                {
                    objectUpdatePassword.Password = password;
                    //_objectRMCDataContext.UserInfos.InsertOnSubmit(objectUpdatePassword);
                    _objectRMCDataContext.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "public bool ExistUserEmailId(string emailId)");
                ex.Data.Add("Class", "BSUsers");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Request for Activation in User Information.
        /// </summary>
        /// <param name="activationRequest"></param>
        /// <returns></returns>
        public bool UpdateRequestActivation(string activationRequest, int userID, out string message)
        {
            _flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                RMC.DataService.UserInfo objectUserInfo = (from ui in _objectRMCDataContext.UserInfos
                                                           where ui.UserID == userID
                                                           select ui).FirstOrDefault();

                if (objectUserInfo != null)
                {
                    if (!objectUserInfo.IsActive)
                    {
                        objectUserInfo.UserActivationRequest = activationRequest;
                        _objectRMCDataContext.SubmitChanges();
                        message = "Activation Request Sent Successfully.";
                        _flag = true;
                    }
                    else
                    {
                        message = "User already Active.";
                    }
                }
                else
                {
                    message = "User does not Exist.";
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "public bool ExistUserEmailId(string emailId)");
                ex.Data.Add("Class", "BSUsers");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return _flag;
        }

        /// <summary>
        /// Logical Delete by userid.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="deletedBy"></param>
        /// <returns></returns>
        public bool DeleteLogicallyUserByUserID(int userID, string deletedBy)
        {
            _flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                int superAdminID = (from ui in _objectRMCDataContext.UserInfos
                                    where ui.Email.ToLower().Trim() == "superadmin"
                                    select ui).FirstOrDefault().UserID;
                if (userID != superAdminID)
                {
                    RMC.DataService.UserInfo userInfo = null;
                    if (deletedBy.ToLower().Trim() == "superadmin")
                    {
                        userInfo = (from ui in _objectRMCDataContext.UserInfos
                                    where ui.UserID == userID && ui.IsDeleted == false
                                    select ui).FirstOrDefault();
                    }
                    else
                    {
                        userInfo = (from ui in _objectRMCDataContext.UserInfos
                                    where ui.UserID == userID && ui.IsActive == true && ui.IsDeleted == false
                                    select ui).FirstOrDefault();
                    }
                    if (userInfo != null)
                    {
                        userInfo.IsDeleted = true;
                        userInfo.DeletedBy = deletedBy;
                        userInfo.DeletedDate = DateTime.Now;
                    }

                    List<RMC.DataService.MultiUserHospital> objectGenericMultiUserHospital = (from muh in _objectRMCDataContext.MultiUserHospitals
                                                                                              where muh.UserID == userID && muh.HospitalInfo.IsActive == true && muh.IsDeleted == false && muh.HospitalInfo.IsDeleted == false
                                                                                              select muh).ToList<RMC.DataService.MultiUserHospital>();

                    foreach (RMC.DataService.MultiUserHospital objectMultiUserHospital in objectGenericMultiUserHospital)
                    {
                        objectMultiUserHospital.UserID = superAdminID;
                        objectMultiUserHospital.HospitalInfo.UserID = superAdminID;
                    }

                    List<RMC.DataService.MultiUserDemographic> objectGenericMultiUserDemographic = (from mud in _objectRMCDataContext.MultiUserDemographics
                                                                                                    where mud.UserID == userID && mud.IsDeleted == false && mud.HospitalDemographicInfo.IsDeleted == false
                                                                                                    select mud).ToList<RMC.DataService.MultiUserDemographic>();

                    foreach (RMC.DataService.MultiUserDemographic objectMultiUserDemographic in objectGenericMultiUserDemographic)
                    {
                        objectMultiUserDemographic.UserID = superAdminID;
                    }

                    _objectRMCDataContext.SubmitChanges();
                    _flag = true;
                }
                else
                {
                    _flag = false;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteLogicallyUserByUserID");
                ex.Data.Add("Class", "BSUsers");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return _flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool DeleteUserByUserID(int userID)
        {
            _flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                RMC.DataService.UserInfo objectUserInfo = (from ui in _objectRMCDataContext.UserInfos
                                                           where ui.IsDeleted == true && ui.UserID == userID
                                                           select ui).FirstOrDefault();

                if (objectUserInfo != null)
                {
                    _objectRMCDataContext.UserInfos.DeleteOnSubmit(objectUserInfo);
                    _objectRMCDataContext.SubmitChanges();
                    _flag = true;
                }
                else
                {
                    _flag = false;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteUserByUserID");
                ex.Data.Add("Class", "BSUsers");
                throw ex;
            }

            return _flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool DeleteUserBySuperAdmin(int userID)
        {
            _flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                RMC.DataService.UserInfo objectUserInfo = (from ui in _objectRMCDataContext.UserInfos
                                                           where ui.IsDeleted == false && ui.UserID == userID
                                                           select ui).FirstOrDefault();

                if (objectUserInfo != null)
                {
                    _objectRMCDataContext.UserInfos.DeleteOnSubmit(objectUserInfo);
                    _objectRMCDataContext.SubmitChanges();
                    _flag = true;
                }
                else
                {
                    _flag = false;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteUserByUserID");
                ex.Data.Add("Class", "BSUsers");
                throw ex;
            }

            return _flag;
        }

        /// <summary>
        /// Update the UserID in MultiUserDemographic table, if this user is the owner of that UNIT.
        /// </summary>
        /// <param name="activationRequest"></param>
        /// <returns>Cretaed By : Deepakt</returns>
        /// <returns>Cretaed Date : Sept. 25</returns>
        public bool UpdateRequestHospitalUnitByUserId(int userID)
        {
            _flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                RMC.DataService.MultiUserDemographic objectMultiUserDemographic = (from ui in _objectRMCDataContext.MultiUserDemographics
                                                                                   //where ui.UserID == userID
                                                                                   where ui.MultiUserDemographicID == userID
                                                                                   select ui).FirstOrDefault();

                if (objectMultiUserDemographic != null)
                {
                    objectMultiUserDemographic.UserID = 1;
                    _objectRMCDataContext.SubmitChanges();
                    _flag = true;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "public bool ExistUserEmailId(string emailId)");
                ex.Data.Add("Class", "BSUsers");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return _flag;
        }

        /// <summary>
        ///Delete the records from MultiUserDemographic table, if this user is not the owner of that UNIT.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool DeleteRequestHospitalUnitByUserId(int MultiUserDemographicID)
        {
            _flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                RMC.DataService.MultiUserDemographic objectMultiUserDemographic = (from ui in _objectRMCDataContext.MultiUserDemographics
                                                                                   //where ui.IsDeleted == false && ui.UserID == userID
                                                                                   where ui.IsDeleted == false && ui.MultiUserDemographicID == MultiUserDemographicID
                                                                                   select ui).FirstOrDefault();

                if (objectMultiUserDemographic != null)
                {
                    _objectRMCDataContext.MultiUserDemographics.DeleteOnSubmit(objectMultiUserDemographic);
                    _objectRMCDataContext.SubmitChanges();
                    _flag = true;
                }
                else
                {
                    _flag = false;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteUserByUserID");
                ex.Data.Add("Class", "BSUsers");
                throw ex;
            }

            return _flag;
        }
        #endregion

    }
    //End Of BSUserInfo Class    
}
//End Of NameSpace
