using System;
using System.Collections;
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
    public partial class ViewBenchmarkingFilters : System.Web.UI.UserControl
    {

        #region Variables
        RMC.DataService.BenchmarkFilter _objectBenchmarkFilter = null;
        RMC.BussinessService.BSReports _objectBSReports = null;
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack == true)
                {
                    PageLoadDataBind();

                    _objectBenchmarkFilter = new RMC.DataService.BenchmarkFilter();
                    _objectBSReports = new RMC.BussinessService.BSReports();

                    int filterId = Convert.ToInt32(Request.QueryString["filterId"]);
                    _objectBenchmarkFilter = _objectBSReports.GetBenchmarkFilterData(filterId);

                    TextBoxFilterName.Text = _objectBenchmarkFilter.FilterName;
                    LabelCreatorName.Text = _objectBenchmarkFilter.CreatedBy;
                    TextBoxComment.Text = _objectBenchmarkFilter.Comment;
                    CheckBoxShared.Checked = _objectBenchmarkFilter.Share;
                    TextBoxBedsInUnitFrom.Text = Convert.ToString(_objectBenchmarkFilter.BedsInUnitFrom);
                    DropDownListBedsInUnitOperatorFrom.SelectedValue = Convert.ToString(_objectBenchmarkFilter.optBedsInUnitFrom);
                    TextBoxBedsInUnitTo.Text = Convert.ToString(_objectBenchmarkFilter.BedsInUnitTo);
                    DropDownListBedsInUnitOperatorTo.SelectedValue = Convert.ToString(_objectBenchmarkFilter.optBedsInUnitTo);
                    TextBoxElectronicDocumentationFrom.Text = Convert.ToString(_objectBenchmarkFilter.ElectronicDocumentationFrom);
                    DropDownListElectronicDocumentationOperatorFrom.SelectedValue = Convert.ToString(_objectBenchmarkFilter.optElectronicDocumentationFrom);
                    TextBoxElectronicDocumentationTo.Text = Convert.ToString(_objectBenchmarkFilter.ElectronicDocumentationTo);
                    DropDownListElectronicDocumentationOperatorTo.SelectedValue = Convert.ToString(_objectBenchmarkFilter.optElectronicDocumentationTo);
                    TextBoxBudgetedPatientFrom.Text = Convert.ToString(_objectBenchmarkFilter.BudgetedPatientFrom);
                    DropDownListBudgetedPatientOperatorFrom.SelectedValue = Convert.ToString(_objectBenchmarkFilter.optBudgetedPatientFrom);
                    TextBoxBudgetedPatientTo.Text = Convert.ToString(_objectBenchmarkFilter.BudgetedPatientTo);
                    DropDownListBudgetedPatientOperatorTo.SelectedValue = Convert.ToString(_objectBenchmarkFilter.BudgetedPatientTo);
                    TextBoxHospitalSizeFrom.Text = Convert.ToString(_objectBenchmarkFilter.HospitalSizeFrom);
                    DropDownListHospitalSizeOperatorFrom.SelectedValue = Convert.ToString(_objectBenchmarkFilter.optHospitalSizeFrom);
                    TextBoxHospitalSizeTo.Text = Convert.ToString(_objectBenchmarkFilter.HospitalSizeTo);
                    DropDownListHospitalSizeOperatorTo.SelectedValue = Convert.ToString(_objectBenchmarkFilter.optHospitalSizeTo);
                    TextBoxMinimumDataPointsFrom.Text = Convert.ToString(_objectBenchmarkFilter.DataPointsFrom);
                    DropDownListMinimumDataPointsFrom.SelectedValue = Convert.ToString(_objectBenchmarkFilter.optDataPointsFrom);
                    TextBoxMinimumDataPointsTo.Text = Convert.ToString(_objectBenchmarkFilter.DataPointsTo);
                    DropDownListMinimumDataPointsTo.SelectedValue = Convert.ToString(_objectBenchmarkFilter.optDataPointsTo);


                    string[] hospitalType;
                    if (_objectBenchmarkFilter.HospitalType != "0")
                    {
                        hospitalType = _objectBenchmarkFilter.HospitalType.Split(',');
                        for (int count = 0; count < hospitalType.Length; count++)
                        {
                            for (int kcount = 0; kcount < ListBoxHospitalType.Items.Count; kcount++)
                            {
                                if (hospitalType[count].Equals(ListBoxHospitalType.Items[kcount].Text))
                                {
                                    ListBoxHospitalType.Items[kcount].Selected = true;
                                    break;
                                }
                            }
                        }
                    }

                    DropDownListDocByException.SelectedValue = Convert.ToString(_objectBenchmarkFilter.DocByException);
                    DropDownListCountry.SelectedValue = Convert.ToString(_objectBenchmarkFilter.CountryId);
                    DropDownListState.SelectedValue = Convert.ToString(_objectBenchmarkFilter.StateId);

                    string[] unitType;
                    if (_objectBenchmarkFilter.UnitType != "0")
                    {
                        unitType = _objectBenchmarkFilter.UnitType.Split(',');
                        for (int count = 0; count < unitType.Length; count++)
                        {
                            for (int kcount = 0; kcount < ListBoxUnitType.Items.Count; kcount++)
                            {
                                if (unitType[count].Equals(ListBoxUnitType.Items[kcount].Text))
                                {
                                    ListBoxUnitType.Items[kcount].Selected = true;
                                    break;
                                }
                            }
                        }
                    }

                    string[] pharmacyType;
                    if (_objectBenchmarkFilter.PharmacyType != "0")
                    {
                        pharmacyType = _objectBenchmarkFilter.PharmacyType.Split(',');
                        for (int count = 0; count < pharmacyType.Length; count++)
                        {
                            for (int kcount = 0; kcount < ListBoxPharmacyType.Items.Count; kcount++)
                            {
                                if (pharmacyType[count].Equals(ListBoxPharmacyType.Items[kcount].Text))
                                {
                                    ListBoxPharmacyType.Items[kcount].Selected = true;
                                    break;
                                }
                            }
                        }
                    }

                    string[] configName;
                    if (_objectBenchmarkFilter.ConfigurationName != "0")
                    {
                        configName = _objectBenchmarkFilter.ConfigurationName.Split(',');
                        for (int count = 0; count < configName.Length; count++)
                        {
                            for (int kcount = 0; kcount < ListBoxConfiurationName.Items.Count; kcount++)
                            {
                                if (configName[count].Equals(ListBoxConfiurationName.Items[kcount].Text))
                                {
                                    ListBoxConfiurationName.Items[kcount].Selected = true;
                                    break;
                                }
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "PharmacyType.aspx ---- ButtonReset_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
            finally
            {
                _objectBenchmarkFilter = null;
                _objectBSReports = null;
            }

        }

        /// <summary>
        /// Updates Filter Row of the Selected FilterID
        /// </summary>
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                bool flag = false;
                string unitType = string.Empty, pharmacyType = string.Empty, hospitalType = string.Empty, configName = string.Empty;

                _objectBenchmarkFilter = new RMC.DataService.BenchmarkFilter();
                _objectBSReports = new RMC.BussinessService.BSReports();

                _objectBenchmarkFilter.FilterId = Convert.ToInt32(Request.QueryString["filterId"]);
                _objectBenchmarkFilter.FilterName = TextBoxFilterName.Text;
                //_objectBenchmarkFilter.CreatedBy = TextBoxCreatorName.Text;
                _objectBenchmarkFilter.Share = CheckBoxShared.Checked;
                _objectBenchmarkFilter.Comment = TextBoxComment.Text;

                if (TextBoxBedsInUnitFrom.Text == string.Empty)
                {
                    _objectBenchmarkFilter.BedsInUnitFrom = 0;
                }
                else
                {
                    _objectBenchmarkFilter.BedsInUnitFrom = Convert.ToInt32(TextBoxBedsInUnitFrom.Text);
                }

                _objectBenchmarkFilter.optBedsInUnitFrom = Convert.ToInt32(DropDownListBedsInUnitOperatorFrom.SelectedValue);
                if (TextBoxBedsInUnitTo.Text == string.Empty)
                {
                    _objectBenchmarkFilter.BedsInUnitTo = 0;
                }
                else
                {
                    _objectBenchmarkFilter.BedsInUnitTo = Convert.ToInt32(TextBoxBedsInUnitTo.Text);
                }

                _objectBenchmarkFilter.optBedsInUnitTo = Convert.ToInt32(DropDownListBedsInUnitOperatorTo.SelectedValue);
                if (TextBoxBudgetedPatientFrom.Text == string.Empty)
                {
                    _objectBenchmarkFilter.BudgetedPatientFrom = 0;
                }
                else
                {
                    _objectBenchmarkFilter.BudgetedPatientFrom = Convert.ToInt32(TextBoxBudgetedPatientFrom.Text);
                }

                _objectBenchmarkFilter.optBudgetedPatientFrom = Convert.ToInt32(DropDownListBudgetedPatientOperatorFrom.SelectedValue);
                if (TextBoxBudgetedPatientTo.Text == string.Empty)
                {
                    _objectBenchmarkFilter.BudgetedPatientTo = 0;
                }
                else
                {
                    _objectBenchmarkFilter.BudgetedPatientTo = Convert.ToInt32(TextBoxBudgetedPatientTo.Text);
                }

                _objectBenchmarkFilter.optBudgetedPatientTo = Convert.ToInt32(DropDownListBudgetedPatientOperatorTo.SelectedValue);
                if (TextBoxElectronicDocumentationFrom.Text == string.Empty)
                {
                    _objectBenchmarkFilter.ElectronicDocumentationFrom = 0;
                }
                else
                {
                    _objectBenchmarkFilter.ElectronicDocumentationFrom = Convert.ToInt32(TextBoxElectronicDocumentationFrom.Text);
                }

                _objectBenchmarkFilter.optElectronicDocumentationFrom = Convert.ToInt32(DropDownListElectronicDocumentationOperatorFrom.SelectedValue);
                if (TextBoxElectronicDocumentationTo.Text == string.Empty)
                {
                    _objectBenchmarkFilter.ElectronicDocumentationTo = 0;
                }
                else
                {
                    _objectBenchmarkFilter.ElectronicDocumentationTo = Convert.ToInt32(TextBoxElectronicDocumentationTo.Text);
                }

                _objectBenchmarkFilter.optElectronicDocumentationTo = Convert.ToInt32(DropDownListElectronicDocumentationOperatorTo.SelectedValue);
                if (TextBoxHospitalSizeFrom.Text == string.Empty)
                {
                    _objectBenchmarkFilter.HospitalSizeFrom = 0;
                }
                else
                {
                    _objectBenchmarkFilter.HospitalSizeFrom = Convert.ToInt32(TextBoxHospitalSizeFrom.Text);
                }

                _objectBenchmarkFilter.optHospitalSizeFrom = Convert.ToInt32(DropDownListHospitalSizeOperatorFrom.SelectedValue);
                if (TextBoxHospitalSizeTo.Text == string.Empty)
                {
                    _objectBenchmarkFilter.HospitalSizeTo = 0;
                }
                else
                {
                    _objectBenchmarkFilter.HospitalSizeTo = Convert.ToInt32(TextBoxHospitalSizeTo.Text);
                }

                _objectBenchmarkFilter.optHospitalSizeTo = Convert.ToInt32(DropDownListHospitalSizeOperatorTo.SelectedValue);


                if (TextBoxMinimumDataPointsFrom.Text == string.Empty)
                {
                    _objectBenchmarkFilter.DataPointsFrom = 0;
                }
                else
                {
                    _objectBenchmarkFilter.DataPointsFrom = Convert.ToInt32(TextBoxMinimumDataPointsFrom.Text);
                }

                _objectBenchmarkFilter.optDataPointsFrom = Convert.ToInt32(DropDownListMinimumDataPointsFrom.SelectedValue);
                if (TextBoxMinimumDataPointsTo.Text == string.Empty)
                {
                    _objectBenchmarkFilter.DataPointsTo = 0;
                }
                else
                {
                    _objectBenchmarkFilter.DataPointsTo = Convert.ToInt32(TextBoxMinimumDataPointsTo.Text);
                }

                _objectBenchmarkFilter.optDataPointsTo = Convert.ToInt32(DropDownListMinimumDataPointsTo.SelectedValue);





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
                    _objectBenchmarkFilter.UnitType = "0";
                }
                else
                {
                    _objectBenchmarkFilter.UnitType = unitType;
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
                    _objectBenchmarkFilter.PharmacyType = "0";
                }
                else
                {
                    _objectBenchmarkFilter.PharmacyType = pharmacyType;
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
                    _objectBenchmarkFilter.HospitalType = "0";
                }
                else
                {
                    _objectBenchmarkFilter.HospitalType = hospitalType;
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
                    _objectBenchmarkFilter.ConfigurationName = "0";
                }
                else
                {
                    _objectBenchmarkFilter.ConfigurationName = configName;
                }

                _objectBenchmarkFilter.DocByException = Convert.ToInt32(DropDownListDocByException.SelectedValue);
                _objectBenchmarkFilter.CountryId = Convert.ToInt32(DropDownListCountry.SelectedValue);
                //_objectBenchmarkFilter.CreatedDate = DateTime.Now;
                _objectBenchmarkFilter.ModifiedBy = TextBoxFilterName.Text;
                _objectBenchmarkFilter.ModifiedDate = DateTime.Now;
                _objectBenchmarkFilter.StateId = Convert.ToInt32(DropDownListState.SelectedValue);

                flag = _objectBSReports.UpdateBenchmarkFilter(_objectBenchmarkFilter);

                if (flag)
                {
                    CommonClass.Show("Record Updated Successfully.");
                    //ResetControls();
                }
                else
                {
                    CommonClass.Show("Failed to Update Benchmark Filter.");
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "PharmacyType.aspx ---- ButtonReset_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                _objectBSReports = new RMC.BussinessService.BSReports();
                flag = _objectBSReports.DeleteBenchmarkFilter(Convert.ToInt32(Request.QueryString["filterId"]));
                if (flag)
                {
                    CommonClass.Show("Record Deleted Successfully.");

                    //ResetControls();
                }
                else
                {
                    CommonClass.Show("Failed to Delete Record.");
                }

            }
            catch (Exception ex)
            {
                LogManager._stringObject = "PharmacyType.aspx ---- ButtonReset_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
            finally
            {
                Response.Redirect("FiltersList.aspx", false);
            }
        }

        protected void DropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListState.Items.Clear();
            DropDownListState.Items.Add("Select State");
            DropDownListState.Items[0].Value = 0.ToString();
        }

        protected void DropDownListState_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            if (HttpContext.Current.User.IsInRole("superadmin"))
            {
                Response.Redirect("~/Administrator/FiltersList.aspx", false);
            }
            else
            {
                Response.Redirect("~/Users/FiltersList.aspx", false);
            }
        }

        #endregion

        #region Private Methods

        private void PageLoadDataBind()
        {
            ListBoxUnitType.DataBind();
            ListBoxPharmacyType.DataBind();
            ListBoxHospitalType.DataBind();
            ListBoxConfiurationName.DataBind();
            DropDownListBedsInUnitOperatorFrom.DataBind();
            DropDownListBedsInUnitOperatorTo.DataBind();
            DropDownListBudgetedPatientOperatorFrom.DataBind();
            DropDownListBudgetedPatientOperatorTo.DataBind();
            DropDownListElectronicDocumentationOperatorFrom.DataBind();
            DropDownListElectronicDocumentationOperatorTo.DataBind();
            DropDownListHospitalSizeOperatorFrom.DataBind();
            DropDownListHospitalSizeOperatorTo.DataBind();
            DropDownListCountry.DataBind();
            DropDownListState.DataBind();
            DropDownListDocByException.DataBind();
        }

        #endregion


        protected void ButtonSavePopup_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                string unitType = string.Empty, pharmacyType = string.Empty, hospitalType = string.Empty, configName = string.Empty;

                _objectBenchmarkFilter = new RMC.DataService.BenchmarkFilter();
                _objectBSReports = new RMC.BussinessService.BSReports();

                _objectBenchmarkFilter.FilterName = TextBoxNewFilterName.Text;
                //objectBenchmarkFilter.CreatedBy = ((List<RMC.DataService.UserInfo>)HttpContext.Current.Session["UserInformation"])[0].ToString();
                _objectBenchmarkFilter.CreatedBy = CommonClass.UserInformation.FirstName + CommonClass.UserInformation.LastName;

                _objectBenchmarkFilter.Share = CheckBoxShared.Checked;
                _objectBenchmarkFilter.Comment = TextBoxComment.Text;

                if (TextBoxBedsInUnitFrom.Text == string.Empty)
                {
                    _objectBenchmarkFilter.BedsInUnitFrom = 0;
                }
                else
                {
                    _objectBenchmarkFilter.BedsInUnitFrom = Convert.ToInt32(TextBoxBedsInUnitFrom.Text);
                }

                _objectBenchmarkFilter.optBedsInUnitFrom = Convert.ToInt32(DropDownListBedsInUnitOperatorFrom.SelectedValue);
                if (TextBoxBedsInUnitTo.Text == string.Empty)
                {
                    _objectBenchmarkFilter.BedsInUnitTo = 0;
                }
                else
                {
                    _objectBenchmarkFilter.BedsInUnitTo = Convert.ToInt32(TextBoxBedsInUnitTo.Text);
                }

                _objectBenchmarkFilter.optBedsInUnitTo = Convert.ToInt32(DropDownListBedsInUnitOperatorTo.SelectedValue);
                if (TextBoxBudgetedPatientFrom.Text == string.Empty)
                {
                    _objectBenchmarkFilter.BudgetedPatientFrom = 0;
                }
                else
                {
                    _objectBenchmarkFilter.BudgetedPatientFrom = Convert.ToInt32(TextBoxBudgetedPatientFrom.Text);
                }

                _objectBenchmarkFilter.optBudgetedPatientFrom = Convert.ToInt32(DropDownListBudgetedPatientOperatorFrom.SelectedValue);
                if (TextBoxBudgetedPatientTo.Text == string.Empty)
                {
                    _objectBenchmarkFilter.BudgetedPatientTo = 0;
                }
                else
                {
                    _objectBenchmarkFilter.BudgetedPatientTo = Convert.ToInt32(TextBoxBudgetedPatientTo.Text);
                }

                _objectBenchmarkFilter.optBudgetedPatientTo = Convert.ToInt32(DropDownListBudgetedPatientOperatorTo.SelectedValue);
                if (TextBoxElectronicDocumentationFrom.Text == string.Empty)
                {
                    _objectBenchmarkFilter.ElectronicDocumentationFrom = 0;
                }
                else
                {
                    _objectBenchmarkFilter.ElectronicDocumentationFrom = Convert.ToInt32(TextBoxElectronicDocumentationFrom.Text);
                }

                _objectBenchmarkFilter.optElectronicDocumentationFrom = Convert.ToInt32(DropDownListElectronicDocumentationOperatorFrom.SelectedValue);
                if (TextBoxElectronicDocumentationTo.Text == string.Empty)
                {
                    _objectBenchmarkFilter.ElectronicDocumentationTo = 0;
                }
                else
                {
                    _objectBenchmarkFilter.ElectronicDocumentationTo = Convert.ToInt32(TextBoxElectronicDocumentationTo.Text);
                }

                _objectBenchmarkFilter.optElectronicDocumentationTo = Convert.ToInt32(DropDownListElectronicDocumentationOperatorTo.SelectedValue);
                if (TextBoxHospitalSizeFrom.Text == string.Empty)
                {
                    _objectBenchmarkFilter.HospitalSizeFrom = 0;
                }
                else
                {
                    _objectBenchmarkFilter.HospitalSizeFrom = Convert.ToInt32(TextBoxHospitalSizeFrom.Text);
                }

                _objectBenchmarkFilter.optHospitalSizeFrom = Convert.ToInt32(DropDownListHospitalSizeOperatorFrom.SelectedValue);
                if (TextBoxHospitalSizeTo.Text == string.Empty)
                {
                    _objectBenchmarkFilter.HospitalSizeTo = 0;
                }
                else
                {
                    _objectBenchmarkFilter.HospitalSizeTo = Convert.ToInt32(TextBoxHospitalSizeTo.Text);
                }

                _objectBenchmarkFilter.optHospitalSizeTo = Convert.ToInt32(DropDownListHospitalSizeOperatorTo.SelectedValue);

                if (TextBoxMinimumDataPointsFrom.Text == string.Empty)
                {
                    _objectBenchmarkFilter.DataPointsFrom = 0;
                }
                else
                {
                    _objectBenchmarkFilter.DataPointsFrom = Convert.ToInt32(TextBoxMinimumDataPointsFrom.Text);
                }
                _objectBenchmarkFilter.optDataPointsFrom = Convert.ToInt32(DropDownListMinimumDataPointsFrom.SelectedValue);

                if (TextBoxMinimumDataPointsTo.Text == string.Empty)
                {
                    _objectBenchmarkFilter.DataPointsTo = 0;
                }
                else
                {
                    _objectBenchmarkFilter.DataPointsTo = Convert.ToInt32(TextBoxMinimumDataPointsTo.Text);
                }
                _objectBenchmarkFilter.optDataPointsTo = Convert.ToInt32(DropDownListMinimumDataPointsTo.SelectedValue);


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
                    _objectBenchmarkFilter.UnitType = "0";
                }
                else
                {
                    _objectBenchmarkFilter.UnitType = unitType;
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
                    _objectBenchmarkFilter.PharmacyType = "0";
                }
                else
                {
                    _objectBenchmarkFilter.PharmacyType = pharmacyType;
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
                    _objectBenchmarkFilter.HospitalType = "0";
                }
                else
                {
                    _objectBenchmarkFilter.HospitalType = hospitalType;
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
                    _objectBenchmarkFilter.ConfigurationName = "0";
                }
                else
                {
                    _objectBenchmarkFilter.ConfigurationName = configName;
                }

                _objectBenchmarkFilter.DocByException = Convert.ToInt32(DropDownListDocByException.SelectedValue);
                _objectBenchmarkFilter.CountryId = Convert.ToInt32(DropDownListCountry.SelectedValue);
                _objectBenchmarkFilter.CreatedDate = DateTime.Now;
                //objectBenchmarkFilter.ModifiedBy = TextBoxFilterName.Text;
                //objectBenchmarkFilter.ModifiedDate = DateTime.Now;
                _objectBenchmarkFilter.StateId = Convert.ToInt32(DropDownListDocByException.SelectedValue);

                flag = _objectBSReports.InsertBenchmarkFilter(_objectBenchmarkFilter);

                if (flag)
                {
                    CommonClass.Show("Benchmark Filter Saved Successfully.");
                    //ResetControls();
                }
                else
                {
                    CommonClass.Show("Failed To Save Benchmark Filter.");
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ViewBenchmarkFilters.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }


    }
}