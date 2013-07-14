using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSCustomizedPaging
    {

        #region Public Methods
        
        public static int GetNoOfSkipRecords(int noOfRecordPerPage, int pageNo)
        {
            try
            {
                return noOfRecordPerPage * (pageNo - 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int GetNoOfPages(int noOfRecordsPerPage, int totalNoOfRecords)
        {
            try
            {
                int totalPages = totalNoOfRecords / noOfRecordsPerPage;
                if (totalNoOfRecords % noOfRecordsPerPage != 0)
                {
                    totalPages++;
                }

                return totalPages;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Public Methods For Paging

        public static int GetForwardStep(int stepForwardValue, int lastValue)
        {
            try
            {
                if (stepForwardValue < lastValue)
                    stepForwardValue++;
                return stepForwardValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int GetBackwardStep(int stepBackwardValue, int firstValue)
        {
            try
            {
                if (stepBackwardValue > firstValue)
                    stepBackwardValue--;
                return stepBackwardValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int GetFirstValue()
        {
            try
            {
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int GetLastValue(int lastValue)
        {
            try
            {
                return lastValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 

        #endregion

    }
}
