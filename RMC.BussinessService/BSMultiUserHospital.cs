using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace RMC.BussinessService
{
    public class BSMultiUserHospital
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        //Entity Objects.
        EntitySet<RMC.DataService.MultiUserHospital> _entitySetMultiUserHospital = null;

        #endregion

        #region Methods

        /// <summary>
        /// Get Multi User Hospital By UserID.
        /// Created By : Davinder Kumar.
        /// Created Date : July 18, 2009.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public EntitySet<RMC.DataService.MultiUserHospital> GetMultiUserHospitalByUserID(int userID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _entitySetMultiUserHospital = new EntitySet<RMC.DataService.MultiUserHospital>();

                var multUserHospital = from muh in _objectRMCDataContext.MultiUserHospitals
                                       where muh.UserID == userID && muh.HospitalInfo.IsActive == true && muh.UserInfo.IsActive == true && muh.IsDeleted == false && muh.HospitalInfo.IsDeleted == false
                                       select muh;

                _entitySetMultiUserHospital.AddRange(multUserHospital);
                return _entitySetMultiUserHospital;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetMultiUserHospitalByUserID");
                ex.Data.Add("Class", "BSMultiUserHospital");
                throw ex;
            }
        }

        /// <summary>
        /// Get User Infomation By Hospital Info ID.
        /// Created By : Davinder Kumar.
        /// Creation Date : Aug 04, 2009.
        /// Modified By:Mahesh Sachdeva
        /// Modified Date:June 17 2010
        /// Modified to fetch PermissionId and UserId
        /// </summary>
        /// <param name="hospitalID"></param>
        /// <returns></returns>
        public List<RMC.BusinessEntities.BEUserInfomation> GetUserInfomationByHospitalInfoID(int hospitalInfoID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.BusinessEntities.BEUserInfomation> objectGenericBEUserInfomation = (from ui in _objectRMCDataContext.MultiUserHospitals
                                                                                             where ui.HospitalInfoID == hospitalInfoID
                                                                                             select new RMC.BusinessEntities.BEUserInfomation
                                                                                             {
                                                                                                 UserName = ui.UserInfo.FirstName + " " + ui.UserInfo.LastName,
                                                                                                 Email = ui.UserInfo.Email,
                                                                                                 UserID = Convert.ToInt32(ui.UserID),
                                                                                                 PermissionId = Convert.ToInt32(ui.PermissionID)

                                                                                             }).ToList<RMC.BusinessEntities.BEUserInfomation>();
                return objectGenericBEUserInfomation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetUserInfomationByHospitalInfoID");
                ex.Data.Add("Class", "BSMultiUserHospital");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Check access Permission in hospital for specific user.
        /// Created By : Davinder Kumar
        /// Creation Date : July 23, 2009.
        /// </summary>
        /// <param name="userID">Login User ID</param>
        /// <param name="hospitalInfoID">Hospital Registration ID</param>
        /// <param name="permissionID">Permission</param>
        /// <returns></returns>
        public bool CheckPermissionOnHospitalByUserID(int userID, int hospitalInfoID, int permissionID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                var multUserHospital = from muh in _objectRMCDataContext.MultiUserHospitals
                                       where muh.HospitalInfoID == hospitalInfoID && muh.UserID == userID && muh.PermissionID == permissionID && muh.IsDeleted == false
                                       select muh;

                if (multUserHospital.Count() > 0)
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
                ex.Data.Add("Function", "CheckPermissionOnHospitalByUserID");
                ex.Data.Add("Class", "BSMultiUserHospital");
                throw ex;
            }
        }

        /// <summary>
        /// Save record Only for view.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 29, 2009.
        /// </summary>
        /// <param name="objectMultiUserHospital"></param>
        /// <returns></returns>
        public bool InsertMultiUserHospitalForViewOnly(RMC.DataService.MultiUserHospital objectMultiUserHospital)
        {
            bool flag = false;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                _objectRMCDataContext.MultiUserHospitals.InsertOnSubmit(objectMultiUserHospital);
                _objectRMCDataContext.SubmitChanges();
                flag = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertMultiUserHospitalForViewOnly");
                ex.Data.Add("Class", "BSMultiUserHospital");
                throw ex;
            }

            return flag;
        }

        public bool InsertMultiUserHospitalForViewOnly(List<RMC.DataService.MultiUserHospital> objectGenericMultiUserHospital)
        {
            bool flag = false;
            try
            {
                RMC.DataService.MultiUserHospital multiuserHospital = new RMC.DataService.MultiUserHospital();
                RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.DataService.MultiUserHospital> objectGenericmultiuserHospital = new List<RMC.DataService.MultiUserHospital>();
                foreach (RMC.DataService.MultiUserHospital objectMultiUserHospital in objectGenericMultiUserHospital)
                {

                       multiuserHospital = (from muh in objectRMCDataContext.MultiUserHospitals
                                         where muh.UserID == objectMultiUserHospital.UserID && muh.HospitalInfoID == objectMultiUserHospital.HospitalInfoID
                                         select muh).FirstOrDefault();
                
                        flag = CheckUserExistsOrNot(Convert.ToInt32(objectMultiUserHospital.UserID), Convert.ToInt32(objectMultiUserHospital.HospitalInfoID));
                        if (flag == false && objectMultiUserHospital.PermissionID != 4) //If Insert a New User
                        {
                            if (objectMultiUserHospital.PermissionID == 1) //If inserting a owner
                            {  
                                objectGenericMultiUserHospital = (from muh in objectRMCDataContext.MultiUserHospitals
                                                                  where muh.HospitalInfoID == objectMultiUserHospital.HospitalInfoID && muh.PermissionID == 1
                                                                  select muh).ToList();
                                if (objectGenericMultiUserHospital.Count == 1 && objectGenericMultiUserHospital[0].UserID==1 ) // if existing owner in superadmin and no of owners =1 the update superadmin with new owner
                                {
                                    multiuserHospital = (from muh in objectRMCDataContext.MultiUserHospitals
                                                         where muh.HospitalInfoID == objectMultiUserHospital.HospitalInfoID && muh.PermissionID == 1
                                                         select muh).FirstOrDefault();
                                    multiuserHospital.UserID = objectMultiUserHospital.UserID;
                                    multiuserHospital.CreatedBy = objectMultiUserHospital.CreatedBy;
                                    multiuserHospital.CreatedDate = objectMultiUserHospital.CreatedDate;
                                    multiuserHospital.PermissionID = objectMultiUserHospital.PermissionID;
                                    multiuserHospital.HospitalInfoID = objectMultiUserHospital.HospitalInfoID;
                                }
                                else  //If existing owners are more than 1
                                {
                                    objectRMCDataContext.MultiUserHospitals.InsertOnSubmit(objectMultiUserHospital);
                                }
                            }
                            else  // If new user is not owner
                            {
                                objectRMCDataContext.MultiUserHospitals.InsertOnSubmit(objectMultiUserHospital);
                            }
                            
                        }
                        else 
                        {
                            if (flag && objectMultiUserHospital.PermissionID == 4)  // If deleting
                            {
                                DeletMultiUserHospital(Convert.ToInt32(multiuserHospital.MultiUserHospitalID),Convert.ToInt32(multiuserHospital.HospitalInfoID),Convert.ToInt32(multiuserHospital.UserID));
                               
                            }
                            else if(flag)  // If updating
                            {
                                if (objectMultiUserHospital.PermissionID == 1) // If Updating a Existing user to a owner
                                {
                                    objectGenericMultiUserHospital = (from muh in objectRMCDataContext.MultiUserHospitals
                                                                      where muh.HospitalInfoID == objectMultiUserHospital.HospitalInfoID && muh.PermissionID == 1
                                                                      select muh).ToList();
                                    if (objectGenericMultiUserHospital.Count == 1 && objectGenericMultiUserHospital[0].UserID == 1)
                                    {
                                        objectRMCDataContext.MultiUserHospitals.DeleteOnSubmit(multiuserHospital);
                                        objectGenericMultiUserHospital[0].UserID = objectMultiUserHospital.UserID;
                                        objectGenericMultiUserHospital[0].CreatedBy = objectMultiUserHospital.CreatedBy;
                                        objectGenericMultiUserHospital[0].CreatedDate = objectMultiUserHospital.CreatedDate;
                                        objectGenericMultiUserHospital[0].PermissionID = objectMultiUserHospital.PermissionID;
                                        objectGenericMultiUserHospital[0].HospitalInfoID = objectMultiUserHospital.HospitalInfoID;
                                    }
                                    else 
                                    {
                                        multiuserHospital.UserID = objectMultiUserHospital.UserID;
                                        multiuserHospital.CreatedBy = objectMultiUserHospital.CreatedBy;
                                        multiuserHospital.CreatedDate = objectMultiUserHospital.CreatedDate;
                                        multiuserHospital.PermissionID = objectMultiUserHospital.PermissionID;
                                        multiuserHospital.HospitalInfoID = objectMultiUserHospital.HospitalInfoID;
                                    }
                                }
                                else // If Updating a existing user to other than  owner
                                {
                                    multiuserHospital.UserID = objectMultiUserHospital.UserID;
                                    multiuserHospital.CreatedBy = objectMultiUserHospital.CreatedBy;
                                    multiuserHospital.CreatedDate = objectMultiUserHospital.CreatedDate;
                                    multiuserHospital.PermissionID = objectMultiUserHospital.PermissionID;
                                    multiuserHospital.HospitalInfoID = objectMultiUserHospital.HospitalInfoID;
                                }
                            }
                        }
                        objectRMCDataContext.SubmitChanges();
                    
                }

              
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertMultiUserHospitalForViewOnly");
                ex.Data.Add("Class", "BSMultiUserHospital");
                throw ex;
            }

            return flag;
        }

        public bool IsUserOwnerOfTheHospital(int multiUserHospitalID)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            var query = (from MUH in _objectRMCDataContext.MultiUserHospitals
                        where MUH.MultiUserHospitalID == multiUserHospitalID
                        select MUH).SingleOrDefault();
            if (query != null)
            {
                //Check whether the permission is for Owner level or not
                if (query.PermissionID == 1)
                    return true;
            }

            return false;

        }

        public int GetUserIdForMultiUserHospitalId(int multiUserHospitalID)
        {
            _objectRMCDataContext = new RMC.DataService.RMCDataContext();
            var query = (from MUH in _objectRMCDataContext.MultiUserHospitals
                         where MUH.MultiUserHospitalID == multiUserHospitalID
                         select MUH).SingleOrDefault();

            return query.UserID.Value;
        }
        public bool DeleteHospitalByMultiUserID(int multiUserHospitalID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                var query = (from MUH in _objectRMCDataContext.MultiUserHospitals
                             where MUH.MultiUserHospitalID == multiUserHospitalID
                             select MUH).SingleOrDefault();
                _objectRMCDataContext.MultiUserHospitals.DeleteOnSubmit(query);

                _objectRMCDataContext.SubmitChanges();
                return true;
            }
            catch 
            {
                return false;
            }
        }
        /// <summary>
        /// Update User Permission.
        /// Created By : Raman
        /// Creation Date : July 27, 2009
        /// </summary>
        /// <param name="_userId"></param>
        /// <param name="_active"></param>
        public void UpdatePermissionOnHospital(int multiUserHospitalID, int PermissionId, bool isDelete, string ActiveUser)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                var query = from MUH in _objectRMCDataContext.MultiUserHospitals
                            where MUH.MultiUserHospitalID == multiUserHospitalID
                            select MUH;
                foreach (RMC.DataService.MultiUserHospital MUH in query)
                {
                    if (isDelete == true)
                    {
                        MUH.IsDeleted = true;
                        MUH.DeletedDate = DateTime.Now;
                        MUH.DeletedBy = ActiveUser;
                    }
                }
                _objectRMCDataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "public void UpdatePermissionOnHospital(int multiUserHospitalID, int PermissionId, bool value, string ActiveUser)");
                ex.Data.Add("Class", "BSMultiuserHospital");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext = null;
            }
        }

        public void DeletMultiUserHospital(int multiUserHospitalID,int hospitalId,int userId)
        {
            try
            {
                using (RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext())
                {
                    RMC.DataService.MultiUserHospital objectMultiUserHosp = objectRMCDataContext.MultiUserHospitals.SingleOrDefault(s => s.MultiUserHospitalID == multiUserHospitalID);
                    RMC.BussinessService.BSMultiUserDemographic objectBSMultiUserDemographic = new RMC.BussinessService.BSMultiUserDemographic();
                    bool flag = DeleteMultiUserHospitalByMultiUserHospitalId(multiUserHospitalID);
                    if (flag)
                    {
                        List<RMC.DataService.MultiUserDemographic> objectGenericMultiUserDemographic = (from mu in objectRMCDataContext.MultiUserDemographics
                                                                                                        where mu.HospitalDemographicInfo.HospitalInfoID == hospitalId && mu.UserID == userId
                                                                                                        select mu).ToList();

                        if (objectGenericMultiUserDemographic != null)
                        {
                            if (objectGenericMultiUserDemographic.Count > 0)
                            {
                                foreach(RMC.DataService.MultiUserDemographic objectMultiUserDemographic in objectGenericMultiUserDemographic)
                                {
                                    objectBSMultiUserDemographic.DeleteMultiUserDemographicByMultiUserdemoGraphicId(objectMultiUserDemographic.MultiUserDemographicID);     
                                }
                            }
                        }
                     }
                       
                        objectRMCDataContext.SubmitChanges();
                  }
                    //---Commented by Bharat-------
                    //objectRMCDataContext.MultiUserDemographics.DeleteAllOnSubmit(objectGenericMultiUserDemographic.Where(w => w.PermissionID != 1).ToList());                            
                    //objectGenericMultiUserDemographic.Where(w=>w.PermissionID == 1).ToList().ForEach(delegate(RMC.DataService.MultiUserDemographic objectMultiUserDemographic)
                    //{
                    //    objectMultiUserDemographic.UserID = 1;
                    //});
                    //-----------------------------
                    // }
                    // }

                    //----Added by Bharat----Here it deletes the list from multiUserHospital------

                    // Commented by Mahesh Sachdeva

                    //if (objectMultiUserHosp != null)
                    //{
                    //    List<RMC.DataService.MultiUserHospital> objectGenericMultiUserHospital = (from muh in objectRMCDataContext.MultiUserHospitals
                    //                                                                              where muh.HospitalInfoID == objectMultiUserHosp.HospitalInfoID && muh.UserID == objectMultiUserHosp.UserID
                    //                                                                              select muh).ToList();
                 
                    //    if (objectGenericMultiUserHospital.Count > 0)
                    //    {
                    //        objectRMCDataContext.MultiUserHospitals.DeleteAllOnSubmit(objectGenericMultiUserHospital);
                    //    }
                    //}
                    //-----------------------------------------------------

                    //--Commented by Bharat--------------------------------
                    //if (objectMultiUserHosp.PermissionID > 1)
                    //{
                    //    objectRMCDataContext.MultiUserHospitals.DeleteOnSubmit(objectMultiUserHosp);
                    //}
                    //else
                    //{
                    //    objectMultiUserHosp.UserID = 1;
                    //}
                    //-----------------------------------------------------

                }
          
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteMultiUserHospitalByMultiUserHospitalId(int multiUserHospitalId)
        {
            try
            {
                using (RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext())
                {
                    RMC.DataService.MultiUserHospital objectMultiUserHosp = objectRMCDataContext.MultiUserHospitals.SingleOrDefault(s => s.MultiUserHospitalID == multiUserHospitalId);
                   
                    if (objectMultiUserHosp != null)
                    {
                        if (objectMultiUserHosp.PermissionID == 1)
                        {
                            int noOfOwners = (from muh in objectRMCDataContext.MultiUserHospitals where muh.HospitalInfoID==objectMultiUserHosp.HospitalInfoID && muh.PermissionID==1 select muh).Count() ;
                            if (noOfOwners == 1)
                            {
                                objectMultiUserHosp.UserID = 1;
                            }
                            else
                            {
                                objectRMCDataContext.MultiUserHospitals.DeleteOnSubmit(objectMultiUserHosp);
                            } 

                        }
                        else
                        {
                            objectRMCDataContext.MultiUserHospitals.DeleteOnSubmit(objectMultiUserHosp);
                        }
                        
                         objectRMCDataContext.SubmitChanges();
                         return true;
                    }
                   else 
                   {
                       return false;
                   }
                } 
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteMultiUserHospital");
                ex.Data.Add("Class", "BSMultiuserHospital");
                throw ex;
            }
        }
        /// <summary>
        /// Add View Permission
        /// Created By : Raman
        /// Creation Date : July 27, 2009
        /// </summary>
        /// <param name="_userId"></param>
        /// <param name="_active"></param>

        public bool AddViewPermissionOnHospital(int hospitalInfoID, int userId, int permissionId, string ActiveUser)
        {
            RMC.DataService.MultiUserHospital objectMultiUserHospital = null;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.DataService.MultiUserHospital> listMultiUserHospital = (from MUH in _objectRMCDataContext.MultiUserHospitals
                                                                                 where MUH.HospitalInfoID == hospitalInfoID
                            && MUH.UserID == userId
                            && MUH.PermissionID == permissionId
                            && (MUH.IsDeleted ?? false) == false
                                                                                 select MUH).ToList<RMC.DataService.MultiUserHospital>();
                if (listMultiUserHospital.Count <= 0)
                {
                    objectMultiUserHospital = new RMC.DataService.MultiUserHospital();
                    objectMultiUserHospital.HospitalInfoID = hospitalInfoID;
                    objectMultiUserHospital.UserID = userId;
                    objectMultiUserHospital.PermissionID = permissionId;
                    objectMultiUserHospital.CreatedBy = ActiveUser;
                    objectMultiUserHospital.IsDeleted = false;
                    objectMultiUserHospital.CreatedDate = DateTime.Now;
                    objectMultiUserHospital.ModifiedBy = ActiveUser;
                    objectMultiUserHospital.ModifiedDate = DateTime.Now;
                    _objectRMCDataContext.MultiUserHospitals.InsertOnSubmit(objectMultiUserHospital);
                    _objectRMCDataContext.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "public void UpdatePermissionOnHospital(int multiUserHospitalID, int PermissionId, bool value, string ActiveUser)");
                ex.Data.Add("Class", "BSMultiuserHospital");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext = null;
            }
        }


        /// <summary>
        /// Add View Permission
        /// Created By : Mahesh Sachdeva
        /// Creation Date : June 15, 2010
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="hospitalInfoID"></param>
        public bool CheckUserExistsOrNot(int userID, int hospitalInfoID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                var multiUserHosapital = from muh in _objectRMCDataContext.MultiUserHospitals where muh.UserID == userID && muh.HospitalInfoID == hospitalInfoID select muh;
                if (multiUserHosapital.Count() > 0)
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
                ex.Data.Add("function", "CheckUserExistsOrNot");
                ex.Data.Add("class", "BSMultiUserHospital");
                throw ex;
            }
        }
        //}
        #endregion

    }
    //End Of BSMultiUserHospital Class
}
//End Of NameSpace