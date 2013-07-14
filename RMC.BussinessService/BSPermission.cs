using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSPermission
    {

        #region Variables

        //Data Context Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        #endregion

        #region Public Methods

        /// <summary>
        /// Fetch Permission Name.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 24, 2009.
        /// </summary>
        /// <param name="permissionID"></param>
        /// <returns></returns>
        public string GetPermissionByPermissionID(int permissionID)
        {
            try
            {
                string Permission = string.Empty;
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                Permission = (from p in _objectRMCDataContext.Permissions
                              where p.PermissionID == permissionID && p.IsActive == true
                              select p).FirstOrDefault().Permission1;
                return Permission;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetPermissionByPermissionID");
                ex.Data.Add("Class", "BSPermission");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }
        
        #endregion

    }
    //End Of BSPermission Class
}
//End Of NameSpace
