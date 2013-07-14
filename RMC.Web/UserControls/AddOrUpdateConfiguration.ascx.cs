using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class AddOrUpdateConfiguration : System.Web.UI.UserControl
    {

        #region Private Methods

        private RMC.DataService.DataImportConfigLocation SaveDataImportConfigLocation()
        {
            try
            {
                RMC.DataService.DataImportConfigLocation objectDataImportConfigLoc = new RMC.DataService.DataImportConfigLocation();

                objectDataImportConfigLoc.ConfigurationName = TextBoxConfigName.Text;
                objectDataImportConfigLoc.ConfigurationNameLocation = (byte)RMC.BussinessService.BSDataImportConfigLocation.ConvertStringColumnToInt(TextBoxConfigLocation.Text);
                objectDataImportConfigLoc.DateLocation = (byte)RMC.BussinessService.BSDataImportConfigLocation.ConvertStringColumnToInt(TextBoxDateLocation.Text);
                objectDataImportConfigLoc.HeaderLocation = (byte)RMC.BussinessService.BSDataImportConfigLocation.ConvertStringColumnToInt(TextBoxHeaderLocation.Text);
                objectDataImportConfigLoc.HourLocation = (byte)RMC.BussinessService.BSDataImportConfigLocation.ConvertStringColumnToInt(TextBoxHourLocation.Text);
                objectDataImportConfigLoc.InfoSequenceLocation = (byte)RMC.BussinessService.BSDataImportConfigLocation.ConvertStringColumnToInt(TextBoxInfoSeqLocation.Text);
                objectDataImportConfigLoc.KeyDataLocation = (byte)RMC.BussinessService.BSDataImportConfigLocation.ConvertStringColumnToInt(TextBoxKeyDataLocation.Text);
                objectDataImportConfigLoc.KeyDataSeqLocation = (byte)RMC.BussinessService.BSDataImportConfigLocation.ConvertStringColumnToInt(TextBoxKeyDataSeqLocation.Text);
                objectDataImportConfigLoc.MinuteLocation = (byte)RMC.BussinessService.BSDataImportConfigLocation.ConvertStringColumnToInt(TextBoxMinuteLocation.Text);
                objectDataImportConfigLoc.MonthLocation = (byte)RMC.BussinessService.BSDataImportConfigLocation.ConvertStringColumnToInt(TextBoxMonthLocation.Text);
                objectDataImportConfigLoc.PDANameLocation = (byte)RMC.BussinessService.BSDataImportConfigLocation.ConvertStringColumnToInt(TextBoxPDANameLocation.Text);
                objectDataImportConfigLoc.SecondLocation = (byte)RMC.BussinessService.BSDataImportConfigLocation.ConvertStringColumnToInt(TextBoxSecondLocation.Text);
                objectDataImportConfigLoc.SoftwareVersionLocation = (byte)RMC.BussinessService.BSDataImportConfigLocation.ConvertStringColumnToInt(TextBoxSoftwareVersionLocation.Text);
                objectDataImportConfigLoc.YearLocation = (byte)RMC.BussinessService.BSDataImportConfigLocation.ConvertStringColumnToInt(TextBoxYearLocation.Text);

                return objectDataImportConfigLoc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PopulateData(int configID)
        {
            try
            {
                RMC.DataService.DataImportConfigLocation objectDataImportConfigLocation = null;
                RMC.BussinessService.BSDataImportConfigLocation objectBSDataImportConfigLocation = new RMC.BussinessService.BSDataImportConfigLocation();

                objectDataImportConfigLocation = objectBSDataImportConfigLocation.GetDataImportConfigLocation(configID);
                if (objectDataImportConfigLocation != null)
                {
                    TextBoxConfigLocation.Text = RMC.BussinessService.BSDataImportConfigLocation.ConvertIntColumnToString(Convert.ToInt32(objectDataImportConfigLocation.ConfigurationNameLocation));
                    TextBoxConfigName.Text = objectDataImportConfigLocation.ConfigurationName;
                    TextBoxDateLocation.Text = RMC.BussinessService.BSDataImportConfigLocation.ConvertIntColumnToString(Convert.ToInt32(objectDataImportConfigLocation.DateLocation));
                    TextBoxHeaderLocation.Text = RMC.BussinessService.BSDataImportConfigLocation.ConvertIntColumnToString(Convert.ToInt32(objectDataImportConfigLocation.HeaderLocation));
                    TextBoxHourLocation.Text = RMC.BussinessService.BSDataImportConfigLocation.ConvertIntColumnToString(Convert.ToInt32(objectDataImportConfigLocation.HourLocation));
                    TextBoxInfoSeqLocation.Text = RMC.BussinessService.BSDataImportConfigLocation.ConvertIntColumnToString(Convert.ToInt32(objectDataImportConfigLocation.InfoSequenceLocation));
                    TextBoxKeyDataLocation.Text = RMC.BussinessService.BSDataImportConfigLocation.ConvertIntColumnToString(Convert.ToInt32(objectDataImportConfigLocation.KeyDataLocation));
                    TextBoxKeyDataSeqLocation.Text = RMC.BussinessService.BSDataImportConfigLocation.ConvertIntColumnToString(Convert.ToInt32(objectDataImportConfigLocation.KeyDataSeqLocation));
                    TextBoxMinuteLocation.Text = RMC.BussinessService.BSDataImportConfigLocation.ConvertIntColumnToString(Convert.ToInt32(objectDataImportConfigLocation.MinuteLocation));
                    TextBoxMonthLocation.Text = RMC.BussinessService.BSDataImportConfigLocation.ConvertIntColumnToString(Convert.ToInt32(objectDataImportConfigLocation.MonthLocation));
                    TextBoxPDANameLocation.Text = RMC.BussinessService.BSDataImportConfigLocation.ConvertIntColumnToString(Convert.ToInt32(objectDataImportConfigLocation.PDANameLocation));
                    TextBoxSecondLocation.Text = RMC.BussinessService.BSDataImportConfigLocation.ConvertIntColumnToString(Convert.ToInt32(objectDataImportConfigLocation.SecondLocation));
                    TextBoxSoftwareVersionLocation.Text = RMC.BussinessService.BSDataImportConfigLocation.ConvertIntColumnToString(Convert.ToInt32(objectDataImportConfigLocation.SoftwareVersionLocation));
                    TextBoxYearLocation.Text = RMC.BussinessService.BSDataImportConfigLocation.ConvertIntColumnToString(Convert.ToInt32(objectDataImportConfigLocation.YearLocation));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Reset()
        {
            try
            {
                TextBoxConfigLocation.Text = string.Empty;
                TextBoxConfigName.Text = string.Empty;
                TextBoxDateLocation.Text = string.Empty;
                TextBoxHeaderLocation.Text = string.Empty;
                TextBoxHourLocation.Text = string.Empty;
                TextBoxInfoSeqLocation.Text = string.Empty;
                TextBoxKeyDataLocation.Text = string.Empty;
                TextBoxKeyDataSeqLocation.Text = string.Empty;
                TextBoxMinuteLocation.Text = string.Empty;
                TextBoxMonthLocation.Text = string.Empty;
                TextBoxPDANameLocation.Text = string.Empty;
                TextBoxSecondLocation.Text = string.Empty;
                TextBoxSoftwareVersionLocation.Text = string.Empty;
                TextBoxYearLocation.Text = string.Empty;
                DropDownListConfigName.Items.Clear();
                DropDownListConfigName.Items.Add("Select Config Name");
                DropDownListConfigName.Items[0].Value = "0";
                DropDownListConfigName.DataBind();
                DropDownListConfigName.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Events
        
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    RMC.BussinessService.BSDataImportConfigLocation objectBSDataImportConfigLoc = new RMC.BussinessService.BSDataImportConfigLocation();

                    bool flag = objectBSDataImportConfigLoc.UpdateDataImportConfigLocation(Convert.ToInt32(DropDownListConfigName.SelectedValue), SaveDataImportConfigLocation());
                    if (flag)
                    {
                        CommonClass.Show("Configuration Record Updated successfully.");
                    }
                    else
                    {
                        CommonClass.Show("Failed To Update Configuration Record.");
                    }

                    Reset();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Event", "ButtonSave_Click");
                LogManager._stringObject = "AddOrUpdateConfiguration.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    RMC.BussinessService.BSDataImportConfigLocation objectBSDataImportConfigLoc = new RMC.BussinessService.BSDataImportConfigLocation();

                    bool flag = objectBSDataImportConfigLoc.InsertDataImportConfigLocation(SaveDataImportConfigLocation());
                    if (flag)
                    {
                        CommonClass.Show("Configuration Record Saved Successfully.");
                    }
                    else
                    {
                        CommonClass.Show("Failed To Save Configuration Record.");
                    }

                    Reset();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Event", "ButtonSave_Click");
                LogManager._stringObject = "AddOrUpdateConfiguration.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void DropDownListConfigName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListConfigName.SelectedIndex > 0)
                    PopulateData(Convert.ToInt32(DropDownListConfigName.SelectedValue));
            }
            catch (Exception ex)
            {
                ex.Data.Add("Event", "DropDownListConfigName_SelectedIndexChanged");
                LogManager._stringObject = "AddOrUpdateConfiguration.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.Header.Title = "RMC :: ADD/Update Configuration";
                if (DropDownListConfigName.SelectedIndex > 0)
                {
                    ButtonUpdate.Enabled = true;
                }
                else
                {
                    ButtonUpdate.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Event", "Page_Load");
                LogManager._stringObject = "AddOrUpdateConfiguration.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        } 

        #endregion

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                    RMC.BussinessService.BSDataImportConfigLocation objectBSDataImportConfigLoc = new RMC.BussinessService.BSDataImportConfigLocation();

                    bool flag = objectBSDataImportConfigLoc.DeleteDataImportConfigLocation(Convert.ToInt32(DropDownListConfigName.SelectedValue));
                    if (flag)
                    {
                        CommonClass.Show("Configuration Record Deleted successfully.");
                    }
                    else
                    {
                        CommonClass.Show("Failed To Delete Configuration Record.");
                    }

                    Reset();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Event", "ButtonSave_Click");
                LogManager._stringObject = "AddOrUpdateConfiguration.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

    }
}