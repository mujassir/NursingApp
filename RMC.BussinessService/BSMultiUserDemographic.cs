using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace RMC.BussinessService
{
    public class BSMultiUserDemographic
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        //Entity Objects.
        EntitySet<RMC.DataService.MultiUserDemographic> _entitySetMultiUserDemographic = null;

        #endregion

        #region Methods

        /// <summary>
        /// Get Multi User Demographic By UserID.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 18, 2009.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public EntitySet<RMC.DataService.MultiUserDemographic> GetMultiUserDemographicByUserID(int userID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _entitySetMultiUserDemographic = new EntitySet<RMC.DataService.MultiUserDemographic>();

                var multUserDemographic = from mud in _objectRMCDataContext.MultiUserDemographics
                                          where mud.UserID == userID && mud.HospitalDemographicInfo.HospitalInfo.IsActive == true && mud.UserInfo.IsActive == true 
                                          select mud;

                _entitySetMultiUserDemographic.AddRange(multUserDemographic);
                return _entitySetMultiUserDemographic;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetMultiUserDemographicByUserID");
                ex.Data.Add("Class", "BSMultiUserHospital");
                throw ex;
            }           
        }

        /// <summary>
        /// Save Permission for view Only.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 29, 2009.
        /// </summary>
        /// <param name="objectMultiUserDemographic"></param>
        /// <returns></returns>
        public void InsertMultiUserDemographic(RMC.DataService.MultiUserDemographic objectMultiUserDemographic)
        {
            bool flag = false;
            try
            {
                 _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                 RMC.DataService.MultiUserHospital objectMultiUserHospital = new RMC.DataService.MultiUserHospital();
                 List<RMC.DataService.MultiUserDemographic> objectGenericDemographic  = new List<RMC.DataService.MultiUserDemographic>();
                 RMC.DataService.HospitalDemographicInfo hospitalDemoGraphicInfo = new RMC.DataService.HospitalDemographicInfo();
                 RMC.BussinessService.BSMultiUserHospital objectBSMultiUsers = new RMC.BussinessService.BSMultiUserHospital();
                 objectGenericDemographic = (from mud in _objectRMCDataContext.MultiUserDemographics where mud.HospitalDemographicID == objectMultiUserDemographic.HospitalDemographicID && mud.PermissionID == 1 select mud).ToList();
                 if (objectMultiUserDemographic.PermissionID == 1)
                 {
                     if (objectGenericDemographic.Count() == 1 && objectGenericDemographic[0].UserID == 1)
                     {
                         objectGenericDemographic[0].UserID = objectMultiUserDemographic.UserID;
                         objectGenericDemographic[0].HospitalDemographicID = objectMultiUserDemographic.HospitalDemographicID;
                         objectGenericDemographic[0].CreatedBy = objectMultiUserDemographic.CreatedBy;
                         objectGenericDemographic[0].CreatedDate = objectMultiUserDemographic.CreatedDate;
                         objectGenericDemographic[0].IsDeleted = objectMultiUserDemographic.IsDeleted;
                         objectGenericDemographic[0].PermissionID = objectMultiUserDemographic.PermissionID;
                     }
                     else
                     {
                         _objectRMCDataContext.MultiUserDemographics.InsertOnSubmit(objectMultiUserDemographic);
                     }
                 }
                 else 
                 {
                     _objectRMCDataContext.MultiUserDemographics.InsertOnSubmit(objectMultiUserDemographic);
                 }
                 hospitalDemoGraphicInfo = (from hdg in _objectRMCDataContext.HospitalDemographicInfos where  hdg.HospitalDemographicID == objectMultiUserDemographic.HospitalDemographicID select hdg).FirstOrDefault();
                 flag= objectBSMultiUsers.CheckUserExistsOrNot(Convert.ToInt32(objectMultiUserDemographic.UserID), Convert.ToInt32( hospitalDemoGraphicInfo.HospitalInfoID ));
                 if (flag == false)
                 {
                     objectMultiUserHospital.CreatedBy = objectMultiUserDemographic.CreatedBy;
                     objectMultiUserHospital.CreatedDate =Convert.ToDateTime(objectMultiUserDemographic.CreatedDate);
                     objectMultiUserHospital.HospitalInfoID = Convert.ToInt32(objectMultiUserDemographic.HospitalDemographicInfo.HospitalInfoID);
                     objectMultiUserHospital.IsDeleted = objectMultiUserDemographic.IsDeleted;
                     objectMultiUserHospital.PermissionID = 2;
                     objectMultiUserHospital.UserID = objectMultiUserDemographic.UserID;
                     _objectRMCDataContext.MultiUserHospitals.InsertOnSubmit(objectMultiUserHospital);
                 }
                _objectRMCDataContext.SubmitChanges();

               
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertMultiUserDemographic");
                ex.Data.Add("Class", "BSMultiUserHospital");
                throw ex;
            }

            
        }

        /// <summary>
        /// Save Permission for view Only.       
        /// </summary>
        /// <param name="objectMultiUserDemographic"></param>
        /// <returns></returns>
        public bool InsertBulkMultiUserDemographic(List<RMC.DataService.MultiUserDemographic> objectGenericMultiUserDemographic)
        {
            bool flag = false;
            try
            {
               RMC.DataService.MultiUserDemographic multiUserDemographic = new RMC.DataService.MultiUserDemographic();
               RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext();
               List<RMC.DataService.MultiUserDemographic> objectGenericDemographic = new List<RMC.DataService.MultiUserDemographic>();
               foreach(RMC.DataService.MultiUserDemographic objectMultiUserdemographic in objectGenericMultiUserDemographic)
               {
                  multiUserDemographic=(from mud in objectRMCDataContext.MultiUserDemographics where mud.HospitalDemographicID==objectMultiUserdemographic.HospitalDemographicID && mud.UserID==objectMultiUserdemographic.UserID select mud).FirstOrDefault();
                  if(multiUserDemographic==null) // Check User Wheather already Exists or Not
                  {
                      flag=false;
                  }
                  else
                  {
                      flag=true;  
                  }
                  if (flag == false && objectMultiUserdemographic.PermissionID != 4) //Insert User 
                  {
                      InsertMultiUserDemographic(objectMultiUserdemographic);
                  }
                  else
                  {
                      if (flag ==true && objectMultiUserdemographic.PermissionID == 4) // Delete User
                      {
                          DeleteMultiUserDemoGraphic(Convert.ToInt32(multiUserDemographic.MultiUserDemographicID),Convert.ToInt32(multiUserDemographic.UserID),Convert.ToInt32(multiUserDemographic.HospitalDemographicID),Convert.ToInt32(multiUserDemographic.HospitalDemographicInfo.HospitalInfoID));
                         
                      }
                      else if(flag) //Update User 
                      {
                          if (objectMultiUserdemographic.PermissionID == 1) // If Updating a existing user to owner
                          {
                              objectGenericDemographic = (from mud in objectRMCDataContext.MultiUserDemographics where mud.HospitalDemographicID == objectMultiUserdemographic.HospitalDemographicID && mud.PermissionID == 1 select mud).ToList();
                              if (objectGenericDemographic.Count() == 1 && objectGenericDemographic[0].UserID == 1) //If only one owner exists and that is owner
                              {
                                  objectRMCDataContext.MultiUserDemographics.DeleteOnSubmit(multiUserDemographic);
                                  objectGenericDemographic[0].UserID = objectMultiUserdemographic.UserID;
                                  objectGenericDemographic[0].HospitalDemographicID = objectMultiUserdemographic.HospitalDemographicID;
                                  objectGenericDemographic[0].CreatedBy = objectMultiUserdemographic.CreatedBy;
                                  objectGenericDemographic[0].CreatedDate = objectMultiUserdemographic.CreatedDate;
                                  objectGenericDemographic[0].IsDeleted = objectMultiUserdemographic.IsDeleted;
                                  objectGenericDemographic[0].PermissionID = objectMultiUserdemographic.PermissionID;
                              }
                              else  // If existing owner is not super admin
                              {
                              multiUserDemographic.UserID = objectMultiUserdemographic.UserID;
                              multiUserDemographic.HospitalDemographicID = objectMultiUserdemographic.HospitalDemographicID;
                              multiUserDemographic.PermissionID = objectMultiUserdemographic.PermissionID;
                              multiUserDemographic.CreatedBy = objectMultiUserdemographic.CreatedBy;
                              multiUserDemographic.CreatedDate = objectMultiUserdemographic.CreatedDate;
                              multiUserDemographic.IsDeleted = objectMultiUserdemographic.IsDeleted;
                              }
                          }
                          else // If updating a existing user other than other
                          {
                              multiUserDemographic.UserID = objectMultiUserdemographic.UserID;
                              multiUserDemographic.HospitalDemographicID = objectMultiUserdemographic.HospitalDemographicID;
                              multiUserDemographic.PermissionID = objectMultiUserdemographic.PermissionID;
                              multiUserDemographic.CreatedBy = objectMultiUserdemographic.CreatedBy;
                              multiUserDemographic.CreatedDate = objectMultiUserdemographic.CreatedDate;
                              multiUserDemographic.IsDeleted = objectMultiUserdemographic.IsDeleted;
                          }
                      }
                  }
                  objectRMCDataContext.SubmitChanges();
               }
                
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertBulkMultiUserDemographic");
                ex.Data.Add("Class", "BSMultiUserHospital");
                throw ex;
            }

            return flag;
        }
        
        /// <summary>
        /// Add View Permission
        /// Created By : Raman
        /// Creation Date : July 28, 2009
        /// </summary>
        /// <param name="_userId"></param>
        /// <param name="_active"></param>
        public bool AddViewPermissionOnHospitalUnits(int hospitalDemographicID, int userId, int permissionId, string ActiveUser)
        {
            RMC.DataService.MultiUserDemographic objectMultiUserDemographic = null;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                List<RMC.DataService.MultiUserDemographic> listMultiUserDemographic = (from MUD in _objectRMCDataContext.MultiUserDemographics
                                                                                 where MUD.HospitalDemographicID == hospitalDemographicID
                                                                                    && MUD.UserID == userId
                                                                                    && MUD.PermissionID == permissionId
                                                                                    && (MUD.IsDeleted ?? false) == false
                                                                                       select MUD).ToList<RMC.DataService.MultiUserDemographic>();
                if (listMultiUserDemographic.Count <= 0)
                {
                    objectMultiUserDemographic = new RMC.DataService.MultiUserDemographic();
                    objectMultiUserDemographic.HospitalDemographicID = hospitalDemographicID;
                    objectMultiUserDemographic.UserID = userId;
                    objectMultiUserDemographic.PermissionID = permissionId;
                    objectMultiUserDemographic.CreatedBy = ActiveUser;
                    objectMultiUserDemographic.IsDeleted = false;
                    objectMultiUserDemographic.CreatedDate = DateTime.Now;
                    objectMultiUserDemographic.ModifiedBy = ActiveUser;
                    objectMultiUserDemographic.ModifiedDate = DateTime.Now;
                    _objectRMCDataContext.MultiUserDemographics.InsertOnSubmit(objectMultiUserDemographic);
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
                ex.Data.Add("Function", "AddViewPermissionOnHospitalUnits(int hospitalDemographicID, int userId, int permissionId, string ActiveUser)");
                ex.Data.Add("Class", "BSMultiUserDemographic");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext = null;
            }
        }
       
        /// <summary>
        /// Update User Permission.
        /// Created By : Raman
        /// Creation Date : July 28, 2009
        /// </summary>
        /// <param name="_userId"></param>
        /// <param name="_active"></param>
        public void UpdatePermissionOnHospitalUnits(int MultiUserDemographicID, int PermissionId, bool isDelete, string ActiveUser)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                var query = from MUD in _objectRMCDataContext.MultiUserDemographics
                            where MUD.MultiUserDemographicID == MultiUserDemographicID
                            select MUD;
                foreach (RMC.DataService.MultiUserDemographic MUD in query)
                {
                    if (isDelete == true)
                    {
                        MUD.IsDeleted = true;
                        MUD.DeletedDate = DateTime.Now;
                        MUD.DeletedBy = ActiveUser;
                    }
                }
                _objectRMCDataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdatePermissionOnHospitalUnits(int hospitalDemographicID, int PermissionId, bool isDelete, string ActiveUser)");
                ex.Data.Add("Class", "BSMultiUserDemographic");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext = null;
            }
        }

        /// <summary>
        /// Check access Permission in hospital Demographic Detail for specific user.
        /// Created By : Davinder Kumar
        /// Creation Date : July 28, 2009.
        /// </summary>
        /// <param name="userID">Login User ID</param>
        /// <param name="hospitalInfoID">Hospital Registration ID</param>
        /// <param name="permissionID">Permission</param>
        /// <returns></returns>
        public bool CheckPermissionOnHospitalDemographicDetailByUserID(int userID, int hospitalDemogrphicDetailID, int permissionID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                var multUserDemographic = from mud in _objectRMCDataContext.MultiUserDemographics
                                          where mud.HospitalDemographicID == hospitalDemogrphicDetailID && mud.UserID == userID && mud.PermissionID == permissionID && mud.IsDeleted == false
                                          select mud;

                if (multUserDemographic.Count() > 0)
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
                ex.Data.Add("Function", "CheckPermissionOnHospitalDemographicDetailByUserID");
                ex.Data.Add("Class", "BSMultiUserDemographic");
                throw ex;
            }
        }

        /// <summary>
        /// Update Permission by SuperAdmin.
        /// Created By : Raman
        /// Creation Date : Sept. 30, 2009
        /// </summary>
        /// <param name="_userId"></param>
        /// <param name="_active"></param>
        public void UpdatePermissionBySuperAdmin(int MultiUserDemographicID, int PermissionID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                var query = from MUD in _objectRMCDataContext.MultiUserDemographics
                            where MUD.MultiUserDemographicID == MultiUserDemographicID
                            select MUD;
                foreach (RMC.DataService.MultiUserDemographic MUD in query)
                {
                    MUD.PermissionID = PermissionID;
                }
                _objectRMCDataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "UpdatePermissionBySuperAdmin(int MultiUserDemographicID, int PermissionID)");
                ex.Data.Add("Class", "BSMultiUserDemographic");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext = null;
            }
        }



        /// <summary>
        /// Get Approved users by HospitalDemograhic Id .
        /// Created By : Mahesh Sachdeva 
        /// Creation Date : June 18, 2010
        /// </summary>
        /// <param name="hospitaldemographicI"></param>
        public List <RMC.BusinessEntities.BEUserInfomation>  GetMultiUserDemographicByHospitalDemogaphicId(int hospitaldemographicId)
        {
            try
            {
                List<RMC.BusinessEntities.BEUserInfomation> objectBEUserInformation = new List<RMC.BusinessEntities.BEUserInfomation>();
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                objectBEUserInformation = (from mud in _objectRMCDataContext.MultiUserDemographics
                                           where mud.HospitalDemographicID == hospitaldemographicId
                                           select new RMC.BusinessEntities.BEUserInfomation
                                               {
                                                 UserName=mud.UserInfo.FirstName+""+mud.UserInfo.LastName,
                                                 UserID=Convert.ToInt32(mud.UserID),
                                                 PermissionId=Convert.ToInt32(mud.PermissionID),
                                                 Email=mud.UserInfo.Email

                                               }).ToList();
                return objectBEUserInformation;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetMembersOfUnitByHospitalDemogaphicId");
                ex.Data.Add("Class", "BSMultiUserDemographic");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext = null;
            }
        }

        /// <summary>
        /// Delete From multiuserdemographic and from multi user  hospital
        /// Created By : Mahesh Sachdeva 
        /// Creation Date : June 18, 2010
        /// </summary>
     
        public void DeleteMultiUserDemoGraphic(int multiUserDemographicId,int userId,int hospitalDemographicId,int hospitalId)
        {
            try
            {
                int noOfSameUserInOtherUnits;
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                RMC.DataService.MultiUserHospital objectMultiUserHospital = new RMC.DataService.MultiUserHospital();
                RMC.BussinessService.BSMultiUserHospital objectBSMultiUserHospital=new RMC.BussinessService.BSMultiUserHospital();
                bool flag = DeleteMultiUserDemographicByMultiUserdemoGraphicId(multiUserDemographicId);
                if (flag)
                {
                    noOfSameUserInOtherUnits = (from mud in _objectRMCDataContext.MultiUserDemographics where mud.UserID == userId && mud.HospitalDemographicInfo.HospitalInfoID == hospitalId select mud).Count();
                    if (noOfSameUserInOtherUnits == 0)
                    {
                        objectMultiUserHospital=(from muh in _objectRMCDataContext.MultiUserHospitals where muh.UserID==userId && muh.HospitalInfoID==hospitalId select muh).FirstOrDefault();
                        objectBSMultiUserHospital.DeleteMultiUserHospitalByMultiUserHospitalId(objectMultiUserHospital.MultiUserHospitalID); 
                    }
                    _objectRMCDataContext.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DeleteMultiUserDemoGraphic");
                ex.Data.Add("Class", "BSMultiUserDemographic");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext = null;
            }
        }
        public bool DeleteMultiUserDemographicByMultiUserdemoGraphicId(int multiUserDemographicId)
        {
           try
            {
                using (RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext())
                {
                    RMC.DataService.MultiUserDemographic objectMultiUserDemographic = objectRMCDataContext.MultiUserDemographics.SingleOrDefault(s => s.MultiUserDemographicID == multiUserDemographicId);

                    if (objectMultiUserDemographic != null)
                    {
                        if (objectMultiUserDemographic.PermissionID == 1)
                        {
                            int noOfOwners = (from muh in objectRMCDataContext.MultiUserDemographics where muh.PermissionID == 1 && muh.HospitalDemographicID==objectMultiUserDemographic.HospitalDemographicID select muh).Count();
                            if (noOfOwners == 1)
                            {
                                objectMultiUserDemographic.UserID = 1;
                            }
                            else
                            {
                                objectRMCDataContext.MultiUserDemographics.DeleteOnSubmit(objectMultiUserDemographic);
                            }

                        }
                        else
                        {
                            objectRMCDataContext.MultiUserDemographics.DeleteOnSubmit(objectMultiUserDemographic);
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
        /// CheckUserExistdOrNot
        /// Created By : Mahesh Sachdeva 
        /// Creation Date : June 24, 2010
        /// </summary>
        /// <param name="hospitaldemographicI"></param>
        public bool CheckUserExistdOrNot(int userId, int hospItalDemographicId)
        {  
            bool flag;
            try
            {
               
               RMC.DataService.RMCDataContext objectDataContext=new RMC.DataService.RMCDataContext();
               var obj =(from mud in  objectDataContext.MultiUserDemographics where mud.UserID==userId && mud.HospitalDemographicID==hospItalDemographicId select mud).FirstOrDefault();
               if (obj == null)
               {
                   flag = false;
               }
               else
               {
                   flag= true;
               }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }
        #endregion

    }
    //End Of BSMultiUserDemographic Class
}
//End Of NameSpace
