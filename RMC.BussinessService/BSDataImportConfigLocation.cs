using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSDataImportConfigLocation
    {

        #region Enumerator

        enum alphabets
        {
            A = 1, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z
        };

        #endregion

        #region Public Methods

        public List<RMC.BusinessEntities.BEDropDownListData> GetDataImportConfigLocation()
        {
            List<RMC.BusinessEntities.BEDropDownListData> objectGenericDataImportConfigLoc = null;
            try
            {
                using (RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext())
                {
                    objectGenericDataImportConfigLoc = (from dcl in objectRMCDataContext.DataImportConfigLocations
                                                        select new RMC.BusinessEntities.BEDropDownListData
                                                        {
                                                            Key = dcl.ConfigurationName,
                                                            Value = Convert.ToString(dcl.ConfigurationID)
                                                        }).Distinct().ToList();
                }

                return objectGenericDataImportConfigLoc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RMC.DataService.DataImportConfigLocation GetDataImportConfigLocation(int configID)
        {
            RMC.DataService.DataImportConfigLocation objectDataImportConfigLocation = null;
            try
            {
                using (RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext())
                {
                    objectDataImportConfigLocation = objectRMCDataContext.DataImportConfigLocations.Where(w => w.ConfigurationID == configID).FirstOrDefault();
                }

                return objectDataImportConfigLocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objectDataImportConfigLocation = null;
            }
        }

        public bool InsertDataImportConfigLocation(RMC.DataService.DataImportConfigLocation objectDataImportConfigLocation)
        {
            bool flag = false;
            try
            {
                using (RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext())
                {
                    objectRMCDataContext.DataImportConfigLocations.InsertOnSubmit(objectDataImportConfigLocation);
                    objectRMCDataContext.SubmitChanges();
                    flag = true;
                }

                return flag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateDataImportConfigLocation(int configID, RMC.DataService.DataImportConfigLocation objectNewDataImportConfigLocation)
        {
            RMC.DataService.DataImportConfigLocation obejectDataImportConfigLocation;
            try
            {
                bool flag = false;
                using (RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext())
                {
                    obejectDataImportConfigLocation = objectRMCDataContext.DataImportConfigLocations.Where(w => w.ConfigurationID == configID).FirstOrDefault();

                    if (obejectDataImportConfigLocation != null)
                    {
                        obejectDataImportConfigLocation.ConfigurationName = objectNewDataImportConfigLocation.ConfigurationName;
                        obejectDataImportConfigLocation.ConfigurationNameLocation = objectNewDataImportConfigLocation.ConfigurationNameLocation;
                        obejectDataImportConfigLocation.DateLocation = objectNewDataImportConfigLocation.DateLocation;
                        obejectDataImportConfigLocation.HeaderLocation = objectNewDataImportConfigLocation.HeaderLocation;
                        obejectDataImportConfigLocation.HourLocation = objectNewDataImportConfigLocation.HourLocation;
                        obejectDataImportConfigLocation.InfoSequenceLocation = objectNewDataImportConfigLocation.InfoSequenceLocation;
                        obejectDataImportConfigLocation.KeyDataLocation = objectNewDataImportConfigLocation.KeyDataLocation;
                        obejectDataImportConfigLocation.KeyDataSeqLocation = objectNewDataImportConfigLocation.KeyDataSeqLocation;
                        obejectDataImportConfigLocation.MinuteLocation = objectNewDataImportConfigLocation.MinuteLocation;
                        obejectDataImportConfigLocation.MonthLocation = objectNewDataImportConfigLocation.MonthLocation;
                        obejectDataImportConfigLocation.PDANameLocation = objectNewDataImportConfigLocation.PDANameLocation;
                        obejectDataImportConfigLocation.SecondLocation = objectNewDataImportConfigLocation.SecondLocation;
                        obejectDataImportConfigLocation.SoftwareVersionLocation = objectNewDataImportConfigLocation.SoftwareVersionLocation;
                        obejectDataImportConfigLocation.YearLocation = objectNewDataImportConfigLocation.YearLocation;

                        objectRMCDataContext.SubmitChanges();
                        flag = true;
                    }
                }

                return flag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteDataImportConfigLocation(int configID)
        {
            RMC.DataService.DataImportConfigLocation obejectDataImportConfigLocation;
            try
            {
                bool flag = false;
                using (RMC.DataService.RMCDataContext objectRMCDataContext = new RMC.DataService.RMCDataContext())
                {
                    obejectDataImportConfigLocation = objectRMCDataContext.DataImportConfigLocations.Where(w => w.ConfigurationID == configID).FirstOrDefault();

                    if (obejectDataImportConfigLocation != null)
                    {
                        objectRMCDataContext.DataImportConfigLocations.DeleteOnSubmit(obejectDataImportConfigLocation);
                        objectRMCDataContext.SubmitChanges();
                        flag = true;
                    }
                }

                return flag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Static Public Methods

        public static int ConvertStringColumnToInt(string value)
        {
            int valueLength = 0, extractValue = 0, totalAlphabets = 1, power = 0, result = 0;
            try
            {
                valueLength = value.Length;
                for (int Index = 0; Index < valueLength; Index++)
                {
                    string extract = Convert.ToString(value[Index]);
                    extractValue = (int)Enum.Parse(typeof(alphabets), extract, true);
                    if (Index != (valueLength - 1))
                    {
                        power = valueLength - (Index + 1);
                        while (power > 0)
                        {
                            totalAlphabets *= 26;
                            power--;
                        }

                        result += totalAlphabets * extractValue;
                    }
                    else
                    {
                        result += extractValue;
                        break;
                    }

                    totalAlphabets = 1;
                }

                return result - 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ConvertIntColumnToString(int value)
        {
            try
            {
                string result = string.Empty;
                int dividor = 26, dividend = 0, rem = 0, negation = 0;

                if ((dividend = value / dividor) <= 0)
                {
                    result = Enum.GetName(typeof(alphabets), value + 1);
                }
                else
                {
                    while (dividor != 1)
                    {
                        rem = value / dividor;
                        result += Enum.GetName(typeof(alphabets), rem);
                        value = value % dividor;
                        dividor = dividor / 26;

                        if (dividor == 1)
                        {
                            result += Enum.GetName(typeof(alphabets), value + 1);
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
