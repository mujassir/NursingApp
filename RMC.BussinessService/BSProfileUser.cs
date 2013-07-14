using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace RMC.BussinessService
{
    public class BSProfileUser
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        //Fundamental Data Types.
        bool _flag;

        #endregion

        #region Public Methods

        public RMC.DataService.ProfileUser GetProfileUser(int profileTypeID)
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                RMC.DataService.ProfileUser objectProfileUser = null;

                if (HttpContext.Current.User.IsInRole("superadmin"))
                {
                    objectProfileUser = (from gpt in _objectRMCDataContext.ProfileUsers
                                         where gpt.ProfileTypeID == profileTypeID
                                         select gpt).FirstOrDefault();
                }
                else
                {
                    objectProfileUser = (from gpt in _objectRMCDataContext.ProfileUsers
                                         where gpt.ProfileTypeID == profileTypeID
                                         select gpt).FirstOrDefault();
                }
                return objectProfileUser;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetProfileUser");
                ex.Data.Add("Class", "BSProfileUser");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Purpose : Check the status of profile mode (i.e. editable or readonly).
        /// Use in Profile Detail.ascx.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="profileTypeID"></param>
        /// <returns></returns>
        public bool checkProfileStatus(int userID, int profileTypeID)
        {
            _flag = false;
            try
            {
                List<RMC.DataService.ProfileUser> objectGenericProfileUser = new List<RMC.DataService.ProfileUser>();
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                objectGenericProfileUser = (from pu in _objectRMCDataContext.ProfileUsers
                                            where pu.UserID == userID && pu.ProfileTypeID == profileTypeID && pu.ProfileType.IsActive == true
                                            select pu).ToList<RMC.DataService.ProfileUser>();

                if (objectGenericProfileUser.Count > 0)
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
                ex.Data.Add("Function", "checkProfileStatus");
                ex.Data.Add("Class", "BSProfileUser");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }

            return _flag;
        }

        #endregion

    }
}
