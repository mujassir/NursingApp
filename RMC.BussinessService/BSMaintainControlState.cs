using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSMaintainControlState
    {

        #region Public Methods

        /// <summary>
        /// Set Year and Month in FileUpload.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public RMC.DataService.MaintainControlState GetMaintainControlState(int userID)
        {
            try
            {
                RMC.DataService.MaintainControlState objectMaintainControlState = null;
                using (RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext())
                {
                    objectMaintainControlState = (from mcs in objectRMCDataContext.MaintainControlStates
                                                  where mcs.UserID == userID
                                                  select mcs).FirstOrDefault();
                }

                return objectMaintainControlState;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update Month and Year in File Upload form.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        public void UpdateMaintainControlStateForYearMonth(int userID, string year, string month)
        {
            try
            {
                using (RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext())
                {
                    RMC.DataService.MaintainControlState objectMaintainControlState = objectRMCDataContext.MaintainControlStates.Where(w => w.UserID == userID).FirstOrDefault();
                    if (objectMaintainControlState == null)
                    {
                        RMC.DataService.MaintainControlState objectNewMaintainControlState = new RMC.DataService.MaintainControlState();

                        objectNewMaintainControlState.Year = year;
                        objectNewMaintainControlState.Month = month;
                        objectNewMaintainControlState.UserID = userID;

                        objectRMCDataContext.MaintainControlStates.InsertOnSubmit(objectNewMaintainControlState);
                        objectRMCDataContext.SubmitChanges();

                        objectMaintainControlState = objectRMCDataContext.MaintainControlStates.Where(w => w.UserID == userID).FirstOrDefault();
                    }

                    if (year != null)
                    {
                        objectMaintainControlState.Year = year;
                    }

                    if (month != null)
                    {
                        objectMaintainControlState.Month = month;
                    }

                    objectRMCDataContext.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
