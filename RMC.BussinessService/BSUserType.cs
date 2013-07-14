using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSUserType
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        #endregion

        #region Functions/Methods

        /// <summary>
        /// Name Of UserType are SuperAdmin, Admin, PowerUser, User.
        /// Created By : Davinder Kumar.
        /// Creation Date : June 26, 2009.
        /// </summary>
        /// <param name="userTypeName"></param>
        /// <returns></returns>
        public int GetUserTypeByName(string userTypeName)
        {
            int userTypeID = 0;
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                var userType = from ut in _objectRMCDataContext.UserTypes
                               where ut.UserType1.ToLower().Trim() == userTypeName.ToLower().Trim()
                               select ut;

                if (userType.Count() > 0)
                {
                    userTypeID = userType.FirstOrDefault().UserTypeID;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetUserTypeByName");
                ex.Data.Add("Class", "BSUserType");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return userTypeID;
        }
               
        #endregion

    }
    //End Of BSUserType Class
}
//End Of NameSpace
