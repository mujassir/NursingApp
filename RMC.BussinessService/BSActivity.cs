using System;
using System.Collections.Generic;
using System.Linq;

namespace RMC.BussinessService
{
    public class BSActivity
    {

        #region Variables

        //DataContext Object.
        RMC.DataService.RMCDataContext _objectRMCDataContext = null;

        //Generic list of Data Service object.
        List<RMC.DataService.Activity> _objectGenericActivity = null;

        #endregion

        #region Public Methods

        /// <summary>
        /// Use in Profile Detail.ascx
        /// </summary>
        /// <returns></returns>
        public List<RMC.DataService.Activity> GetAllActivity()
        {
            try
            {
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();
                _objectGenericActivity = (from a in _objectRMCDataContext.Activities
                                          where a.IsActive == true
                                          select a).ToList<RMC.DataService.Activity>();

                return _objectGenericActivity;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetAllActivity");
                ex.Data.Add("Class", "BSActivity");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        /// <summary>
        /// Insert new record, if not exist in a database.
        /// </summary>
        /// <param name="locationName"></param>
        /// <returns></returns>
        public int InsertActivity(string activityName)
        {
            try
            {
                RMC.DataService.Activity objectActivity = new RMC.DataService.Activity();
                _objectRMCDataContext = new RMC.DataService.RMCDataContext();

                objectActivity.Activity1 = activityName;
                objectActivity.IsActive = true;

                _objectRMCDataContext.Activities.InsertOnSubmit(objectActivity);
                _objectRMCDataContext.SubmitChanges();
                return objectActivity.ActivityID;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "InsertLocation");
                ex.Data.Add("Class", "BSLocation");
                throw ex;
            }
            finally
            {
                _objectRMCDataContext.Dispose();
            }
        }

        #endregion

    }
    //End of Class
}
//End of Namespace
