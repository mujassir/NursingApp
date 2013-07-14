using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using RMC.DataService;


namespace RMC.BussinessService
{
    public class BSLogin : RMC.Web.Security.Security
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        //Fundamental Data Types.
        bool _flag;

        #endregion

        #region Methods

        /// <summary>
        /// Check Username and Password.
        /// Created By : Davinder Kumar
        /// Creation Date : June 23, 2009
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckCredential(string userName, string password, out string redirectUrl)
        {
            redirectUrl = string.Empty;
            string AccessReq = string.Empty;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                string role = string.Empty;
                var userInfo = from ui in _objectRMCDataContext.UserInfos
                               where ui.IsActive == true && ui.IsDeleted == false && ui.Email.ToLower().Trim() == userName.ToLower().Trim() && ui.Password.ToLower().Trim() == password.ToLower().Trim()
                               select ui;

                if (userInfo.Count() > 0)
                {
                    AccessReq = userInfo.FirstOrDefault().AccessRequest;
                    role = userInfo.FirstOrDefault().UserType.UserType1;
                    _flag = true;
                }
                else
                {
                    _flag = false;
                }
                if (_flag)
                {
                    redirectUrl = SecurityValidation(userName, role, AccessReq);
                    //HttpContext.Current.Session["UserInformation"] = userInfo.ToList<RMC.DataService.UserInfo>();
                    HttpContext.Current.Session["UserInformation"] = BSSerialization.Serialize(userInfo.ToList<RMC.DataService.UserInfo>());
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "CheckCredential");
                ex.Data.Add("Class", "BSLogin");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return _flag;
        }

        /// <summary>
        /// To update logged in user information in session if logged in user has modified his detail
        /// <Author>Raman</Author>
        /// <CreatedOn>July 20, 2009</CreatedOn>
        /// </summary>
        /// <param name="userId"></param>
        public void UpdateUserInformationInSession(int userId)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                System.Collections.Generic.List<RMC.DataService.UserInfo> listUserInfo = (from ui in _objectRMCDataContext.UserInfos
                                                                                          where ui.IsDeleted == false && ui.UserID == userId
                                                                                          select ui).ToList<RMC.DataService.UserInfo>();
                HttpContext.Current.Session["UserInformation"] = BSSerialization.Serialize(listUserInfo);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "private void UpdateUserInformationInSession(int userId)");
                ex.Data.Add("Class", "BSLogin");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

        }
        //End Of CheckCredential Function.
        #endregion


    }
    //End Of BSLogin Class.

}
//End Of NameSpace.
