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
using System.Data.OleDb;
using RMC.BussinessService;
using RMC.BusinessEntities;
using LogExceptions;

namespace RMC.Web.UserControls
{


    public partial class BenchmarkingFilters : System.Web.UI.UserControl
    {
        #region Variables
        RMC.DataService.BenchmarkFilter objectBenchmarkFilter = null;
        RMC.BussinessService.BSReports objectBSReports = null;
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Saves Filter info in Benchmark Filter Table
        /// </summary>
        protected void ButtonSaveFilter_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                string unitType = string.Empty, pharmacyType = string.Empty, hospitalType = string.Empty , configName = string.Empty;

                objectBenchmarkFilter = new RMC.DataService.BenchmarkFilter();
                objectBSReports = new RMC.BussinessService.BSReports();

                objectBenchmarkFilter.FilterName = TextBoxFilterName.Text;
                //objectBenchmarkFilter.CreatedBy = ((List<RMC.DataService.UserInfo>)HttpContext.Current.Session["UserInformation"])[0].ToString();
                objectBenchmarkFilter.CreatedBy = CommonClass.UserInformation.FirstName + CommonClass.UserInformation.LastName;
               
                objectBenchmarkFilter.Share = CheckBoxShared.Checked;
                objectBenchmarkFilter.Comment = TextBoxComment.Text;

                if (TextBoxBedsInUnitFrom.Text == string.Empty)
                {
                    objectBenchmarkFilter.BedsInUnitFrom = 0;
                }
                else
                {
                    objectBenchmarkFilter.BedsInUnitFrom = Convert.ToInt32(TextBoxBedsInUnitFrom.Text);
                }

                objectBenchmarkFilter.optBedsInUnitFrom = Convert.ToInt32(DropDownListBedsInUnitOperatorFrom.SelectedValue);
                if (TextBoxBedsInUnitTo.Text == string.Empty)
                {
                    objectBenchmarkFilter.BedsInUnitTo = 0;
                }
                else
                {
                    objectBenchmarkFilter.BedsInUnitTo = Convert.ToInt32(TextBoxBedsInUnitTo.Text);
                }

                objectBenchmarkFilter.optBedsInUnitTo = Convert.ToInt32(DropDownListBedsInUnitOperatorTo.SelectedValue);
                if (TextBoxBudgetedPatientFrom.Text == string.Empty)
                {
                    objectBenchmarkFilter.BudgetedPatientFrom = 0;
                }
                else
                {
                    objectBenchmarkFilter.BudgetedPatientFrom = Convert.ToInt32(TextBoxBudgetedPatientFrom.Text);
                }

                objectBenchmarkFilter.optBudgetedPatientFrom = Convert.ToInt32(DropDownListBudgetedPatientOperatorFrom.SelectedValue);
                if (TextBoxBudgetedPatientTo.Text == string.Empty)
                {
                    objectBenchmarkFilter.BudgetedPatientTo = 0;
                }
                else
                {
                    objectBenchmarkFilter.BudgetedPatientTo = Convert.ToInt32(TextBoxBudgetedPatientTo.Text);
                }

                objectBenchmarkFilter.optBudgetedPatientTo = Convert.ToInt32(DropDownListBudgetedPatientOperatorTo.SelectedValue);
                if (TextBoxElectronicDocumentationFrom.Text == string.Empty)
                {
                    objectBenchmarkFilter.ElectronicDocumentationFrom = 0;
                }
                else
                {
                    objectBenchmarkFilter.ElectronicDocumentationFrom = Convert.ToInt32(TextBoxElectronicDocumentationFrom.Text);
                }

                objectBenchmarkFilter.optElectronicDocumentationFrom = Convert.ToInt32(DropDownListElectronicDocumentationOperatorFrom.SelectedValue);
                if (TextBoxElectronicDocumentationTo.Text == string.Empty)
                {
                    objectBenchmarkFilter.ElectronicDocumentationTo = 0;
                }
                else
                {
                    objectBenchmarkFilter.ElectronicDocumentationTo = Convert.ToInt32(TextBoxElectronicDocumentationTo.Text); 
                }
                
                objectBenchmarkFilter.optElectronicDocumentationTo = Convert.ToInt32(DropDownListElectronicDocumentationOperatorTo.SelectedValue);
                if (TextBoxHospitalSizeFrom.Text == string.Empty)
                {
                    objectBenchmarkFilter.HospitalSizeFrom = 0;
                }
                else
                {
                    objectBenchmarkFilter.HospitalSizeFrom = Convert.ToInt32(TextBoxHospitalSizeFrom.Text);
                }

                objectBenchmarkFilter.optHospitalSizeFrom = Convert.ToInt32(DropDownListHospitalSizeOperatorFrom.SelectedValue);
                if (TextBoxHospitalSizeTo.Text == string.Empty)
                {
                    objectBenchmarkFilter.HospitalSizeTo = 0;
                }
                else
                {
                    objectBenchmarkFilter.HospitalSizeTo = Convert.ToInt32(TextBoxHospitalSizeTo.Text);
                }
                
                objectBenchmarkFilter.optHospitalSizeTo = Convert.ToInt32(DropDownListHospitalSizeOperatorTo.SelectedValue);

                if (TextBoxMinimumDataPointsFrom.Text == string.Empty)
                {
                    objectBenchmarkFilter.DataPointsFrom = 0;
                }
                else
                {
                    objectBenchmarkFilter.DataPointsFrom = Convert.ToInt32(TextBoxMinimumDataPointsFrom.Text);
                }
                objectBenchmarkFilter.optDataPointsFrom = Convert.ToInt32(DropDownListMinimumDataPointsFrom.SelectedValue);

                if (TextBoxMinimumDataPointsTo.Text == string.Empty)
                {
                    objectBenchmarkFilter.DataPointsTo = 0;
                }
                else
                {
                    objectBenchmarkFilter.DataPointsTo = Convert.ToInt32(TextBoxMinimumDataPointsTo.Text);
                }
                objectBenchmarkFilter.optDataPointsTo = Convert.ToInt32(DropDownListMinimumDataPointsTo.SelectedValue);


                for (int Index = 0; Index < ListBoxUnitType.Items.Count; Index++)
                {
                    if (ListBoxUnitType.Items[Index].Selected)
                    {
                        unitType += ListBoxUnitType.Items[Index].Text + ",";
                    }
                    if (Index == (ListBoxUnitType.Items.Count - 1) && unitType != "")
                    {
                        unitType = unitType.Remove(unitType.Length - 1, 1);
                    }
                }

                if (unitType == "")
                {
                    objectBenchmarkFilter.UnitType = "0";
                }
                else
                {
                    objectBenchmarkFilter.UnitType = unitType;
                }
                
                for (int Index = 0; Index < ListBoxPharmacyType.Items.Count; Index++)
                {
                    if (ListBoxPharmacyType.Items[Index].Selected)
                    {
                        pharmacyType += ListBoxPharmacyType.Items[Index].Text + ",";
                    }
                    if (Index == (ListBoxPharmacyType.Items.Count - 1) && pharmacyType != "")
                    {
                        pharmacyType = pharmacyType.Remove(pharmacyType.Length - 1, 1);
                    }
                }

                if (pharmacyType == "")
                {
                    objectBenchmarkFilter.PharmacyType = "0";
                }
                else
                {
                    objectBenchmarkFilter.PharmacyType = pharmacyType;
                }
                
                for (int Index = 0; Index < ListBoxHospitalType.Items.Count; Index++)
                {
                    if (ListBoxHospitalType.Items[Index].Selected)
                    {
                        hospitalType += ListBoxHospitalType.Items[Index].Text + ",";
                    }
                    if (Index == (ListBoxHospitalType.Items.Count - 1) && hospitalType != "")
                    {
                        hospitalType = hospitalType.Remove(hospitalType.Length - 1, 1);
                    }
                }

                if (hospitalType == "")
                {
                    objectBenchmarkFilter.HospitalType = "0";
                }
                else
                {
                    objectBenchmarkFilter.HospitalType = hospitalType;                
                }


                for (int Index = 0; Index < ListBoxConfiurationName.Items.Count; Index++)
                {
                    if (ListBoxConfiurationName.Items[Index].Selected)
                    {
                        configName += ListBoxConfiurationName.Items[Index].Text + ",";
                    }
                    if (Index == (ListBoxConfiurationName.Items.Count - 1) && configName != "")
                    {
                        configName = configName.Remove(configName.Length - 1, 1);
                    }
                }

                if (configName == "")
                {
                    objectBenchmarkFilter.ConfigurationName = "0";
                }
                else
                {
                    objectBenchmarkFilter.ConfigurationName = configName;
                }

                objectBenchmarkFilter.DocByException = Convert.ToInt32(DropDownListDocByException.SelectedValue);
                objectBenchmarkFilter.CountryId = Convert.ToInt32(DropDownListCountry.SelectedValue);
                objectBenchmarkFilter.CreatedDate = DateTime.Now;
                //objectBenchmarkFilter.ModifiedBy = TextBoxFilterName.Text;
                //objectBenchmarkFilter.ModifiedDate = DateTime.Now;
                objectBenchmarkFilter.StateId = Convert.ToInt32(DropDownListState.SelectedValue);
                objectBenchmarkFilter.HospitalUnitIds = "0";
                flag = objectBSReports.InsertBenchmarkFilter(objectBenchmarkFilter);

                if (flag)
                {
                    CommonClass.Show("Benchmark Filter Saved Successfully.");
                    ResetControls();
                }
                else
                {
                    CommonClass.Show("Failed To Save Benchmark Filter.");
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "UserProfile.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Reset all values of control in a form to its default value
        /// </summary>
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetControls();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// State data populates when any of country item selected
        /// </summary>
        protected void DropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListState.Items.Clear();
            DropDownListState.Items.Add("Select State");
            DropDownListState.Items[0].Value = 0.ToString();
        }

        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            if (HttpContext.Current.User.IsInRole("superadmin"))
            {
                Response.Redirect("~/Administrator/FiltersList.aspx", false);
            }
            else
            {
                Response.Redirect("~/Users/UserHomePage.aspx", false);
            }
        }

        #endregion

        #region Private

        private void ResetControls()
        {
            try
            {
                TextBoxFilterName.Text = string.Empty;
                TextBoxCreatorName.Text = string.Empty;
                TextBoxComment.Text = string.Empty;
                CheckBoxShared.Checked = false;
                TextBoxBedsInUnitFrom.Text = string.Empty;
                TextBoxBedsInUnitTo.Text = string.Empty;
                TextBoxBudgetedPatientFrom.Text = string.Empty;
                TextBoxBudgetedPatientTo.Text = string.Empty;
                TextBoxElectronicDocumentationFrom.Text = string.Empty;
                TextBoxElectronicDocumentationTo.Text = string.Empty;
                TextBoxHospitalSizeFrom.Text = string.Empty;
                TextBoxHospitalSizeTo.Text = string.Empty;
                DropDownListBedsInUnitOperatorFrom.SelectedIndex = 0;
                DropDownListBedsInUnitOperatorTo.SelectedIndex = 0;
                DropDownListBudgetedPatientOperatorFrom.SelectedIndex = 0;
                DropDownListBudgetedPatientOperatorTo.SelectedIndex = 0;
                DropDownListElectronicDocumentationOperatorFrom.SelectedIndex = 0;
                DropDownListElectronicDocumentationOperatorTo.SelectedIndex = 0;
                DropDownListHospitalSizeOperatorFrom.SelectedIndex = 0;
                DropDownListHospitalSizeOperatorTo.SelectedIndex = 0;
                ListBoxHospitalType.ClearSelection();
                ListBoxPharmacyType.ClearSelection();
                ListBoxUnitType.ClearSelection();
                ListBoxConfiurationName.ClearSelection();
                DropDownListDocByException.SelectedIndex = 0;
                DropDownListCountry.SelectedIndex = 0;
                DropDownListState.SelectedIndex = 0;
                DropDownListCountry.SelectedIndex = 0;
                TextBoxState.Text = string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}