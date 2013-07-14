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
using System.Web.UI.DataVisualization.Charting;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class ReportTimeStudyRN : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DropDownListProfileCategory.Enabled = true;

            try
            {

                MultiViewReport.ActiveViewIndex = 0;

                if (DropDownListChart.SelectedValue == "0")
                {
                    DropDownListProfileCategory.Enabled = false;
                    DropDownListProfileSubCategory.Enabled = false;
                    DropDownListProfileSubCategory.Items.Clear();
                    DropDownListProfileSubCategory.Items.Add("Select Sub Profile");
                    DropDownListProfileSubCategory.Items[0].Value = 0.ToString();
                }
                else
                {
                    DropDownListProfileCategory.Enabled = true;
                    DropDownListProfileSubCategory.Enabled = true;
                }

                if (DropDownListChart.SelectedValue == "Pie")
                {
                    DropDownListProfileSubCategory.Items.Clear();
                    DropDownListProfileSubCategory.Items.Add("Select Sub Profile");
                    DropDownListProfileSubCategory.Items[0].Value = 0.ToString();
                    DropDownListProfileSubCategory.Enabled = false;
                }
                else if (DropDownListChart.SelectedValue == "Line")
                {
                    DropDownListProfileSubCategory.Enabled = true;
                }

                //for chart
                if (Page.IsPostBack != true)
                {
                    List<RMC.BusinessEntities.BEReports> objectGenericBEReports = GetDataForLineChart();
                    RMC.BussinessService.BSChartControl objectBSChartControl = new RMC.BussinessService.BSChartControl();
                    DropDownListMarkerSize.SelectedValue = "6";
                    DropDownListLineWidth.SelectedValue = "2";
                    //List<Series> objectGenericSeries = objectBSChartControl.GetLineChartSeries(DropDownListMarkerStyle.SelectedItem.Text, Convert.ToInt32(DropDownListMarkerSize.SelectedValue), DropDownListMarkerColor.SelectedItem.Text, DropDownListChartType.SelectedValue, Convert.ToInt32(DropDownListLineWidth.SelectedValue), DropDownListColor.SelectedItem.Text, Convert.ToInt32(DropDownListShadowOffset.SelectedValue), DropDownListPointLabel.SelectedItem.Text, objectGenericBEReports);
                    //List<Series> objectGenericSeries = objectBSChartControl.GetLineChartSeries(null, Convert.ToInt32(DropDownListMarkerSize.SelectedValue), null, null, Convert.ToInt32(DropDownListLineWidth.SelectedValue), null, null, null, objectGenericBEReports);
                    //foreach (Series seriesChart in objectGenericSeries)
                    //{
                    //    Chart1.Series.Add(seriesChart);
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void DropDownListMarkerStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLineChartSeries();
        }

        protected void DropDownListMarkerSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLineChartSeries();
        }

        protected void DropDownListMarkerColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLineChartSeries();
        }

        protected void DropDownListPointLabel_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLineChartSeries();
        }

        protected void DropDownListChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLineChartSeries();
        }

        protected void DropDownListLineWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLineChartSeries();
        }

        protected void DropDownListColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLineChartSeries();
        }

        protected void DropDownListShadowOffset_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLineChartSeries();
        }


        #region Private Methods

        private List<RMC.BusinessEntities.BEReports> GetDataForLineChart()
        {
            RMC.BussinessService.BSReports objectBSReports = new RMC.BussinessService.BSReports();
            string ProfileCategoryValue;
            if (DropDownListChart.SelectedValue == "Pie")
            {
                //ProfileCategoryValue = DropDownListProfileCategory.SelectedValue.ToString();
                ProfileCategoryValue = "Value Added";
            }
            else
            {
                //ProfileCategoryValue = DropDownListProfileSubCategory.SelectedValue.ToString();
                ProfileCategoryValue = "Value Added";
            }
            int? hospitalUnitID = Convert.ToInt32(Request.QueryString["hospitalUnitID"]);
            if (hospitalUnitID == 0)
            {
                hospitalUnitID = null;
            }
            int? firstYear = Convert.ToInt32(Request.QueryString["firstYear"]);
            if (firstYear == 0)
            {
                firstYear = null;
            }
            int? lastYear = Convert.ToInt32(Request.QueryString["lastYear"]);
            if (lastYear == 0)
            {
                lastYear = null;
            }
            int? firstMonth = Convert.ToInt32(Request.QueryString["firstMonth"]);
            if (firstMonth == 0)
            {
                firstMonth = null;
            }
            int? lastMonth = Convert.ToInt32(Request.QueryString["lastMonth"]);
            if (lastMonth == 0)
            {
                lastMonth = null;
            }
            int? bedInUnit = Convert.ToInt32(Request.QueryString["bedInUnit"]);
            if (bedInUnit == 0)
            {
                bedInUnit = null;
            }
            int? budgetedPatient = Convert.ToInt32(Request.QueryString["budgetedPatient"]);
            if (budgetedPatient == 0)
            {
                budgetedPatient = null;
            }
            string startDate = Request.QueryString["startDate"];
            if (startDate == "")
            {
                startDate = null;
            }
            string endDate = Request.QueryString["endDate"];
            if (endDate == "")
            {
                endDate = null;
            }
            int? electronicDocument = Convert.ToInt32(Request.QueryString["electronicDocument"]);
            if (electronicDocument == 0)
            {
                electronicDocument = null;
            }
            string unitType = Request.QueryString["unitType"];
            if (unitType == "")
            {
                unitType = null;
            }
            string pharmacyType = Request.QueryString["pharmacyType"];
            if (pharmacyType == "")
            {
                pharmacyType = null;
            }
            int? hospitalSize = Convert.ToInt32(Request.QueryString["hospitalSize"]);
            if (hospitalSize == 0)
            {
                hospitalSize = null;
            }
            List<RMC.BusinessEntities.BEReports> objectGenericBEReports = objectBSReports.GetDataForLineChart(ProfileCategoryValue, ProfileCategoryValue, Convert.ToInt32(Request.QueryString["valueAddedCategoryID"]), Convert.ToInt32(Request.QueryString["OthersCategoryID"]), Convert.ToInt32(Request.QueryString["LocationCategoryID"]), hospitalUnitID, firstYear, lastYear, firstMonth, lastMonth, bedInUnit, Convert.ToInt32(Request.QueryString["optBedInUnit"]), budgetedPatient, Convert.ToInt32(Request.QueryString["optBudgetedPatient"]), startDate, endDate, electronicDocument, Convert.ToInt32(Request.QueryString["optElectronicDocument"]), Convert.ToInt32(Request.QueryString["docByException"]), unitType, pharmacyType, Convert.ToInt32(Request.QueryString["optHospitalSize"]), hospitalSize);
            return objectGenericBEReports;
        }

        private void GetLineChartSeries()
        {
            List<RMC.BusinessEntities.BEReports> objectGenericBEReports = GetDataForLineChart();

            RMC.BussinessService.BSChartControl objectBSChartControl = new RMC.BussinessService.BSChartControl();

            //List<Series> objectGenericSeries = objectBSChartControl.GetLineChartSeries(DropDownListMarkerStyle.SelectedItem.Text, Convert.ToInt32(DropDownListMarkerSize.SelectedValue), DropDownListMarkerColor.SelectedItem.Text, DropDownListChartType.SelectedValue, Convert.ToInt32(DropDownListLineWidth.SelectedValue), DropDownListColor.SelectedItem.Text, Convert.ToInt32(DropDownListShadowOffset.SelectedValue), DropDownListPointLabel.SelectedItem.Text, objectGenericBEReports);
            //List<Series> objectGenericSeries = objectBSChartControl.GetLineChartSeries(DropDownListMarkerStyle.SelectedItem.Text, Convert.ToInt32(DropDownListMarkerSize.SelectedValue), DropDownListMarkerColor.SelectedItem.Text, DropDownListChartType.SelectedValue, Convert.ToInt32(DropDownListLineWidth.SelectedValue), DropDownListColor.SelectedItem.Text, Convert.ToInt32(DropDownListShadowOffset.SelectedValue), DropDownListPointLabel.SelectedItem.Text, objectGenericBEReports);

            //foreach (Series seriesChart in objectGenericSeries)
            //{
            //    Chart1.Series.Add(seriesChart);
            //}
        }

        #endregion

        protected void LinkButtonGenerateChart_Click(object sender, EventArgs e)
        {
            MultiViewReport.ActiveViewIndex = 1;
        }

        protected void LinkButtonShowReport_Click(object sender, EventArgs e)
        {
            MultiViewReport.ActiveViewIndex = 0;
        }

        protected void DropDownListChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListProfileCategory.SelectedIndex = 0;
        }

        protected void DropDownListProfileCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListProfileSubCategory.Items.Clear();
            DropDownListProfileSubCategory.Items.Add("Select Sub Profile");
            DropDownListProfileSubCategory.Items[0].Value = 0.ToString();
            DropDownListProfileSubCategory.Focus();
        }

        protected void ObjectDataSourceReport1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            try
            {
                e.InputParameters["valueAddedCategoryID"] = Convert.ToInt32(Request.QueryString["valueAddedCategoryID"]);
                e.InputParameters["OthersCategoryID"] = Convert.ToInt32(Request.QueryString["OthersCategoryID"]);
                e.InputParameters["LocationCategoryID"] = Convert.ToInt32(Request.QueryString["LocationCategoryID"]);

                string hospitalUnitID = Request.QueryString["hospitalUnitID"];
                if (hospitalUnitID == "0")
                {
                    hospitalUnitID = null;
                }
                e.InputParameters["hospitalUnitID"] = hospitalUnitID;

                string firstYear = Request.QueryString["firstYear"];
                if (firstYear == "0")
                {
                    firstYear = null;
                }
                e.InputParameters["firstYear"] = firstYear;

                string lastYear = Request.QueryString["lastYear"];
                if (lastYear == "0")
                {
                    lastYear = null;
                }
                e.InputParameters["lastYear"] = lastYear;

                string firstMonth = Request.QueryString["firstMonth"];
                if (firstMonth == "0")
                {
                    firstMonth = null;
                }
                e.InputParameters["firstMonth"] = firstMonth;

                string lastMonth = Request.QueryString["lastMonth"];
                if (lastMonth == "0")
                {
                    lastMonth = null;
                }
                e.InputParameters["lastMonth"] = lastMonth;

                string bedInUnit = Request.QueryString["bedInUnit"];
                if (bedInUnit == "0")
                {
                    bedInUnit = null;
                }
                e.InputParameters["bedInUnit"] = bedInUnit;

                e.InputParameters["optBedInUnit"] = Convert.ToInt32(Request.QueryString["optBedInUnit"]);

                string budgetedPatient = Request.QueryString["budgetedPatient"];
                if (budgetedPatient == "0")
                {
                    budgetedPatient = null;
                }
                e.InputParameters["budgetedPatient"] = budgetedPatient;

                e.InputParameters["optBudgetedPatient"] = Convert.ToInt32(Request.QueryString["optBudgetedPatient"]);

                string startDate = Request.QueryString["startDate"];
                if (startDate == "")
                {
                    startDate = null;
                }
                e.InputParameters["startDate"] = startDate;

                string endDate = Request.QueryString["endDate"];
                if (endDate == "")
                {
                    endDate = null;
                }
                e.InputParameters["endDate"] = endDate;

                string electronicDocument = Request.QueryString["electronicDocument"];
                if (electronicDocument == "0")
                {
                    electronicDocument = null;
                }
                e.InputParameters["electronicDocument"] = electronicDocument;

                e.InputParameters["optElectronicDocument"] = Convert.ToInt32(Request.QueryString["optElectronicDocument"]);

                //string docByException = Request.QueryString["docByException"];
                //if (docByException == "0")
                //{
                //    docByException = null;
                //}
                //e.InputParameters["docByException"] = docByException;

                e.InputParameters["docByException"] = Convert.ToInt32(Request.QueryString["docByException"]);

                string unitType = Request.QueryString["unitType"];
                if (unitType == "")
                {
                    unitType = null;
                }
                e.InputParameters["unitType"] = unitType;

                string pharmacyType = Request.QueryString["pharmacyType"];
                if (pharmacyType == "")
                {
                    pharmacyType = null;
                }
                e.InputParameters["pharmacyType"] = pharmacyType;

                e.InputParameters["optHospitalSize"] = Convert.ToInt32(Request.QueryString["optHospitalSize"]);

                string hospitalSize = Request.QueryString["hospitalSize"];
                if (hospitalSize == "0")
                {
                    hospitalSize = null;
                }
                e.InputParameters["hospitalSize"] = hospitalSize;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Page", "CreateNewProfile.ascx");
                LogManager._stringObject = "CreateNewProfile.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

    }
}