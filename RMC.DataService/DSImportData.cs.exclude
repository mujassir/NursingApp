using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMC.BusinessEntities;

namespace RMC.DataService
{
    public class DSImportData
    {

        #region Variables

        //Business Entity Objects.
        BEImportData _importDataBE = null;
        List<BEImportData> _genricBEImportData = null;

        //Input/output Objects.
        System.IO.StreamReader _fileToRead = null;

        //Fundamental Data Types.
        string _fileContent;
        string[] _rows;

        #endregion

        #region Functions/Methods

        /// <summary>
        /// Import the .sda file data into Generic List.
        /// Created By : Davinder Kumar.
        /// Creation Date : June 27, 2009.
        /// </summary>
        /// <param name="fileName"></param>
        public List<BEImportData> ImportData(string fileName, string originalFileName, string year, string month, out bool errorFlag)
        {
            errorFlag = false;
            RMC.DataService.DataImportConfigLocation objectDataImportConfigLocation = null;
            try
            {
                int index = 0;
                string configName = "Nothing";
                _genricBEImportData = new List<BEImportData>();
                _fileToRead = System.IO.File.OpenText(fileName);
                _fileContent = _fileToRead.ReadToEnd();
                _rows = _fileContent.Split('\n');

                foreach (string row in _rows)
                {
                    if (index < _rows.Length - 1)
                    {
                        string[] column = row.Split('\t');
                        _importDataBE = new BEImportData();

                        _importDataBE.FileRef = originalFileName;
                        _importDataBE.Year = year;//column[16];
                        _importDataBE.Month = month;//column[17];

                        if (column[4].ToLower().Trim() != configName.ToLower().Trim())
                        {
                            using (RMC.DataService.RMCDataContext objectDataContext = new RMCDataContext())
                            {
                                objectDataImportConfigLocation = objectDataContext.DataImportConfigLocations.Where(w => w.ConfigurationName.ToLower().Trim() == column[4].ToLower().Trim()).FirstOrDefault();
                            }
                            if (objectDataImportConfigLocation == null)
                            {
                                errorFlag = true;
                            }
                        }

                        _importDataBE.SoftwareVersion = column[Convert.ToInt32(objectDataImportConfigLocation.SoftwareVersionLocation)];
                        _importDataBE.PDAName = column[Convert.ToInt32(objectDataImportConfigLocation.PDANameLocation)];
                        _importDataBE.ConfigName = column[Convert.ToInt32(objectDataImportConfigLocation.ConfigurationNameLocation)];
                        if (objectDataImportConfigLocation != null)
                        {
                            _importDataBE.Date = column[Convert.ToInt32(objectDataImportConfigLocation.MonthLocation)] + "/" + column[Convert.ToInt32(objectDataImportConfigLocation.DateLocation)] + "/" + column[Convert.ToInt32(objectDataImportConfigLocation.YearLocation)];
                            _importDataBE.Time = column[Convert.ToInt32(objectDataImportConfigLocation.HourLocation)] + ":" + column[Convert.ToInt32(objectDataImportConfigLocation.MinuteLocation)] + ":" + column[Convert.ToInt32(objectDataImportConfigLocation.SecondLocation)];
                            _importDataBE.KeyData = column[Convert.ToInt32(objectDataImportConfigLocation.KeyDataLocation)];
                            _importDataBE.InfoSequence = column[Convert.ToInt32(objectDataImportConfigLocation.InfoSequenceLocation)];
                            _importDataBE.KeyDataSequence = column[Convert.ToInt32(objectDataImportConfigLocation.KeyDataSeqLocation)];
                            _importDataBE.Header = column[Convert.ToInt32(objectDataImportConfigLocation.HeaderLocation)];
                            // else case is for RMC Phase VI config
                            if (objectDataImportConfigLocation.TimelessLocation == null)
                            {
                                _importDataBE.Timeless = null;
                            }
                            else
                            {
                                _importDataBE.Timeless = column[Convert.ToInt32(objectDataImportConfigLocation.TimelessLocation)];   
                            }
                        }
                        else
                        {
                            _importDataBE.KeyData = string.Empty;
                            _importDataBE.KeyDataSequence = string.Empty;
                            _importDataBE.InfoSequence = string.Empty;
                            _importDataBE.Header = string.Empty;
                        }

                        //if (_importDataBE.ConfigName.ToLower().Trim() == "RMC Phase V".ToLower().Trim() ||
                        //    _importDataBE.ConfigName.ToLower().Trim() == "RMC Phase IV".ToLower().Trim() ||
                        //    _importDataBE.ConfigName.ToLower().Trim() == "RMC Phase IV-1".ToLower().Trim() ||
                        //    _importDataBE.ConfigName.ToLower().Trim() == "Oct trial".ToLower().Trim())
                        //{
                        //    //Date In SDA File.
                        //    _importDataBE.Date = column[17] + "/" + column[18] + "/" + column[16];
                        //    _importDataBE.Time = column[19] + ":" + column[20] + ":" + column[21];
                        //    _importDataBE.KeyData = column[24];
                        //    _importDataBE.InfoSequence = column[26];
                        //    //_importDataBE.ValueAddedType = column[28];
                        //    _importDataBE.KeyDataSequence = column[29];
                        //    //_importDataBE.DefaultCategory = column[36];
                        //    _importDataBE.Header = column[47];
                        //}
                        //else if (_importDataBE.ConfigName.ToLower().Trim() == "IHI Phase II".ToLower().Trim() ||
                        //    _importDataBE.ConfigName.ToLower().Trim() == "IHI Phase III".ToLower().Trim() ||
                        //    _importDataBE.ConfigName.ToLower().Trim() == "Ascension TCAB".ToLower().Trim())
                        //{
                        //    _importDataBE.Date = column[16] + "/" + column[17] + "/" + column[15];
                        //    _importDataBE.Time = column[18] + ":" + column[19] + ":" + column[20];
                        //    _importDataBE.KeyData = column[23];
                        //    _importDataBE.InfoSequence = column[25];
                        //    //_importDataBE.ValueAddedType = column[28];
                        //    _importDataBE.KeyDataSequence = column[27];
                        //    //_importDataBE.DefaultCategory = column[36];
                        //    _importDataBE.Header = column[41];
                        //}
                        //else if (_importDataBE.ConfigName.ToLower().Trim() == "IHI Phase IV".ToLower().Trim())
                        //{
                        //    _importDataBE.Date = column[17] + "/" + column[18] + "/" + column[16];
                        //    _importDataBE.Time = column[19] + ":" + column[20] + ":" + column[21];
                        //    _importDataBE.KeyData = column[24];
                        //    _importDataBE.InfoSequence = column[26];
                        //    //_importDataBE.ValueAddedType = column[28];
                        //    _importDataBE.KeyDataSequence = column[29];
                        //    //_importDataBE.DefaultCategory = column[36];
                        //    _importDataBE.Header = column[44];
                        //}
                        //else
                        //{
                        //    _importDataBE.KeyData = string.Empty;
                        //    _importDataBE.KeyDataSequence = string.Empty;
                        //    _importDataBE.InfoSequence = string.Empty;
                        //    _importDataBE.Header = string.Empty;
                        //}

                        _genricBEImportData.Add(_importDataBE);
                        index++;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ImportData");
                ex.Data.Add("Class", "DSImportData");
                throw ex;
            }
            finally
            {
                objectDataImportConfigLocation = null;
                _fileToRead.Close();
            }

            return _genricBEImportData;
        }

        public List<BEImportData> ImportData(string fileName, string originalFileName, string year, string month, string configName, out bool errorFlag)
        {
            errorFlag = false;
            RMC.DataService.DataImportConfigLocation objectDataImportConfigLocation = null;
            try
            {
                int index = 0;
                if (configName == null || configName == string.Empty)
                    configName = "Nothing";
                _genricBEImportData = new List<BEImportData>();
                _fileToRead = System.IO.File.OpenText(fileName);
                _fileContent = _fileToRead.ReadToEnd();
                _rows = _fileContent.Split('\n');

                foreach (string row in _rows)
                {
                    if (index < _rows.Length - 1)
                    {
                        string[] column = row.Split('\t');
                        _importDataBE = new BEImportData();

                        _importDataBE.FileRef = originalFileName;
                        _importDataBE.Year = year;//column[16];
                        _importDataBE.Month = month;//column[17];

                        if (column[4].ToLower().Trim() != configName.ToLower().Trim())
                        {
                            using (RMC.DataService.RMCDataContext objectDataContext = new RMCDataContext())
                            {
                                objectDataImportConfigLocation = objectDataContext.DataImportConfigLocations.Where(w => w.ConfigurationName.ToLower().Trim() == configName.ToLower().Trim()).FirstOrDefault();
                            }
                            if (objectDataImportConfigLocation == null)
                            {
                                errorFlag = true;
                            }
                        }
                        _importDataBE.SoftwareVersion = column[Convert.ToInt32(objectDataImportConfigLocation.SoftwareVersionLocation)];
                        _importDataBE.PDAName = column[Convert.ToInt32(objectDataImportConfigLocation.PDANameLocation)];
                        _importDataBE.ConfigName = configName;
                        if (objectDataImportConfigLocation != null)
                        {
                            _importDataBE.Date = column[Convert.ToInt32(objectDataImportConfigLocation.MonthLocation)] + "/" + column[Convert.ToInt32(objectDataImportConfigLocation.DateLocation)] + "/" + column[Convert.ToInt32(objectDataImportConfigLocation.YearLocation)];
                            _importDataBE.Time = column[Convert.ToInt32(objectDataImportConfigLocation.HourLocation)] + ":" + column[Convert.ToInt32(objectDataImportConfigLocation.MinuteLocation)] + ":" + column[Convert.ToInt32(objectDataImportConfigLocation.SecondLocation)];
                            _importDataBE.KeyData = column[Convert.ToInt32(objectDataImportConfigLocation.KeyDataLocation)];
                            _importDataBE.InfoSequence = column[Convert.ToInt32(objectDataImportConfigLocation.InfoSequenceLocation)];
                            _importDataBE.KeyDataSequence = column[Convert.ToInt32(objectDataImportConfigLocation.KeyDataSeqLocation)];
                            _importDataBE.Header = column[Convert.ToInt32(objectDataImportConfigLocation.HeaderLocation)];
                            // else case is for RMC Phase VI config
                            if (objectDataImportConfigLocation.TimelessLocation == null)
                            {
                                _importDataBE.Timeless = null;
                            }
                            else
                            {
                                _importDataBE.Timeless = column[Convert.ToInt32(objectDataImportConfigLocation.TimelessLocation)];
                            }
                        }
                        else
                        {
                            _importDataBE.KeyData = string.Empty;
                            _importDataBE.KeyDataSequence = string.Empty;
                            _importDataBE.InfoSequence = string.Empty;
                            _importDataBE.Header = string.Empty;
                        }

                        _genricBEImportData.Add(_importDataBE);
                        index++;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ImportData");
                ex.Data.Add("Class", "DSImportData");
                throw ex;
            }
            finally
            {
                objectDataImportConfigLocation = null;
                _fileToRead.Close();
            }

            return _genricBEImportData;
        }

        public bool CheckImportDataFileStandard(string filePath)
        {
            bool errorFlag = false;
            RMC.DataService.DataImportConfigLocation objectDataImportConfigLocation = null;
            try
            {
                int index = 0;
                _fileToRead = System.IO.File.OpenText(filePath);
                _fileContent = _fileToRead.ReadToEnd();
                _rows = _fileContent.Split('\n');
                string row = _rows[0];

                if (index < _rows.Length - 1)
                {
                    string[] column = row.Split('\t');

                    using (RMC.DataService.RMCDataContext objectDataContext = new RMCDataContext())
                    {
                        objectDataImportConfigLocation = objectDataContext.DataImportConfigLocations.Where(w => w.ConfigurationName.ToLower().Trim() == column[4].ToLower().Trim()).FirstOrDefault();
                    }
                    if (objectDataImportConfigLocation == null)
                    {
                        errorFlag = true;
                    }
                }

                return errorFlag;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ImportData");
                ex.Data.Add("Class", "DSImportData");
                throw ex;
            }
            finally
            {
                objectDataImportConfigLocation = null;
                _fileToRead.Close();
            }
        }

        #endregion

    }
    //End Of DSImportData Class.
}
//End Of Namespace.

