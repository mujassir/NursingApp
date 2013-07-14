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
using System.Web.UI.DataVisualization.Charting;

namespace RMC.Web
{
    public partial class dv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RMC.BussinessService.BSReports obj = new BSReports();

            List<RMC.BusinessEntities.BEFunctionNames> objeclist = obj.GetPerformanceTest(null,"67", null, null, 91, null, null, null, null, null, 0, null, 0, null, 0, null, 0, null, null, null, 0, null, 0, 0, null, null, null, 0, null, 0, null, null, null,"", "TCAB Value Add", "o", "l1,l2", null, 0, null, 0, null,null);

            RMC.BussinessService.BSChartControl objectBSChartControl = new RMC.BussinessService.BSChartControl();
            Series objectSeries = objectBSChartControl.GetColumnChartSeries(null, objeclist);

            ChartControlCharts.Series.Add(objectSeries);
            //ChartControlCharts.Series["Series1"]["PointWidth"] = "0.3";
            int count = objeclist.Count();
            ChartControlCharts.Width = 500 + (count * 50) ;

            


            //DataTable dt = AddRows(CreateTable(objeclist), objeclist);
            //Gridview1.DataSource = dt;
            //Gridview1.DataBind();

            //string chartType = "Column";
            //Series objectSeries = new Series();
            ////objectSeries.ChartType = SeriesChartType.Column;
            ////objectSeries.BorderWidth = 5;
            //objectSeries.ChartType = (SeriesChartType)SeriesChartType.Parse(typeof(SeriesChartType), chartType);
            ////objectSeries.BorderDashStyle = ChartDashStyle.Dash;
            ////objectSeries.BorderWidth = 2;
            ////string markerStyle = "Diamond";
            ////objectSeries.MarkerStyle = (MarkerStyle)MarkerStyle.Parse(typeof(MarkerStyle), markerStyle);

            //int j = 0;
            //for (int i = 0; i < 50; i++)
            //{
            //    objectSeries.Points.AddXY("a" + i.ToString(), i + 10);
            //    //j = j + 100;
            //    ChartControlCharts.Width = 1200;
            //}
            //ChartControlCharts.Series.Add(objectSeries);
            //ChartControlCharts.Series["Series1"]["PointWidth"] = "0.3";
            ////ChartControlCharts.ChartAreas["ChartArea1"].AxisX.MinorGrid.Enabled = true;
            ////ChartControlCharts.ChartAreas["ChartArea1"].AxisX.MinorTickMark.Enabled = true;

        }

        public System.Data.DataTable CreateTable(List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEReports)
        {
            try
            {
                System.Data.DataTable tbl = new System.Data.DataTable();
                DataColumn dcHospitalUnit = new DataColumn("Function Values", typeof(System.String));
                tbl.Columns.Add(dcHospitalUnit);
                foreach (string objectColumn in objectGenericBEReports.Select(s => s.ColumnName).Distinct().ToList())
                {
                    RMC.BusinessEntities.BEFunctionNames objectBEReport = objectGenericBEReports.Find(delegate(RMC.BusinessEntities.BEFunctionNames objectBERep)
                                                                    {
                                                                        return objectBERep.ColumnName == objectColumn;
                                                                    });

                    DataColumn dc = new DataColumn(objectBEReport.ColumnName, typeof(System.String));
                    tbl.Columns.Add(dc);
                }

                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable AddRows(System.Data.DataTable tbl, List<RMC.BusinessEntities.BEFunctionNames> objectGenericBEReports)
        {
            try
            {
                foreach (string hospitalUnitIndex in objectGenericBEReports.Select(s => s.FunctionName).Distinct().ToList())
                {
                    List<RMC.BusinessEntities.BEFunctionNames> objectListBEReports = objectGenericBEReports.FindAll(delegate(RMC.BusinessEntities.BEFunctionNames objectBERep)
                    {
                        return objectBERep.FunctionName == hospitalUnitIndex;
                    });

                    DataRow dr = tbl.NewRow();
                    dr["Function Values"] = objectListBEReports[0].FunctionName;

                    foreach (RMC.BusinessEntities.BEFunctionNames objectBEReports in objectListBEReports)
                    {
                        dr[objectBEReports.ColumnName] = objectBEReports.FunctionValueText;
                    }
                    tbl.Rows.Add(dr);
                }

                return tbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void populateData(string markerStyle, int? markerSize, string markerColor, string chartType, int? lineWidth, string lineColor, int? shadowOffSet)
        //{
        //    try
        //    {
        //        if (markerStyle == null)
        //        {
        //            markerStyle = "Diamond";
        //        }

        //        if (markerSize == null)
        //        {
        //            markerSize = 6;
        //        }

        //        if (markerColor == null)
        //        {
        //            markerColor = "Blue";
        //        }

        //        if (chartType == null)
        //        {
        //            chartType = "Line";
        //        }

        //        if (lineWidth == null)
        //        {
        //            lineWidth = 2;
        //        }

        //        if (lineColor == null)
        //        {
        //            lineColor = "Blue";
        //        }

        //        if (shadowOffSet == null)
        //        {
        //            shadowOffSet = 1;
        //        }
        //        RMC.BussinessService.BSReports objectBSReports = new BSReports();

        //        List<RMC.BusinessEntities.BEReports> objectGenericBEReports = objectBSReports.GetDataForLineChart("Value Added", "Value Added", 58, 65, 66, 82, null, null, null, null, null, 0, null, 0, null, null, null, 0, 0, null, null, 0, null);

        //        foreach (var objectVar in objectGenericBEReports.GroupBy(o => o.ColumnName).ToList())
        //        {

        //            List<RMC.BusinessEntities.BEReports> objectNewGenericBEReports = objectVar.ToList<RMC.BusinessEntities.BEReports>();
        //            Series series = new Series(objectVar.Key);

        //            //set Dynamic
        //            if (objectVar.Key != "Minimum" && objectVar.Key != "Maximum" && objectVar.Key != "Median" && objectVar.Key != "Average" && objectVar.Key != "Quartile(1)" && objectVar.Key != "Quartile(3)")
        //            {
        //                series.BorderDashStyle = ChartDashStyle.Solid;
        //                series.MarkerStyle = (MarkerStyle)MarkerStyle.Parse(typeof(MarkerStyle), markerStyle);
        //                series.MarkerSize = markerSize.Value;
        //                series.MarkerColor = System.Drawing.Color.FromName(markerColor);
        //            }
        //            else
        //            {
        //                series.BorderDashStyle = ChartDashStyle.Dash;
        //            }
        //            series.ChartType = (SeriesChartType)SeriesChartType.Parse(typeof(SeriesChartType), chartType);
        //            series.BorderWidth = lineWidth.Value;
        //            series.BorderColor = System.Drawing.Color.FromName(lineColor);
        //            series.ShadowOffset = shadowOffSet.Value;

        //            foreach (RMC.BusinessEntities.BEReports objectBEReports in objectNewGenericBEReports)
        //            {
        //                series.Points.AddXY(objectBEReports.MonthName, objectBEReports.ValuesSum);
        //            }

        //            Chart1.Series.Add(series);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void GetPieChartSeries(string chartType, string drawingStyle, string labelStyle, string explodedPoint)
        //{
        //    try
        //    {
        //        //Pie, Doughnut.
        //        if (chartType == null)
        //        {
        //            chartType = "Pie";
        //        }
        //        //Default, SoftEdge, Concave
        //        if (drawingStyle == null)
        //        {
        //            drawingStyle = "SoftEdge";
        //        }
        //        //Disable, Inside, Outside
        //        if (labelStyle == null)
        //        {
        //            labelStyle = "Disabled";
        //        }
        //        //Exploded
        //        if (explodedPoint == null)
        //        {
        //            explodedPoint = "None";
        //        }

        //        RMC.BussinessService.BSReports objectBSReports = new BSReports();

        //        List<RMC.BusinessEntities.BEReports> objectGenericBEReports = objectBSReports.GetDataForPieChart("Value Added", "Value Added", 67, 68, 69, null, null, null, null, null, null, 0, null, 0, null, null, null, 0, 0, null, null, 0, null);

        //        Series series = new Series("Default");
        //        List<string> XValue = new List<string>();
        //        List<double> YValue = new List<double>();

        //        foreach (var objectVar in objectGenericBEReports.GroupBy(o => o.ColumnName).ToList())
        //        {
        //            List<RMC.BusinessEntities.BEReports> objectNewGenericBEReports = objectVar.ToList<RMC.BusinessEntities.BEReports>();

        //            XValue.Add(objectVar.Key);
        //            YValue.Add(objectNewGenericBEReports.Sum(s => s.ValuesSum) / objectNewGenericBEReports.Count);                    
        //        }
        //        series.Points.DataBindXY(XValue, YValue);
        //        series["PieDrawingStyle"] = drawingStyle;
        //        series.BorderColor = System.Drawing.Color.Black;
        //        series.Color = System.Drawing.Color.Blue;
        //        series["PieLabelStyle"] = labelStyle;
        //        series.ChartType = (SeriesChartType)SeriesChartType.Parse(typeof(SeriesChartType), chartType);
        //        //Exploded Point
        //        foreach (DataPoint point in series.Points)
        //        {
        //            point["Exploded"] = "false";
        //            if (point.AxisLabel == explodedPoint)
        //            {
        //                point["Exploded"] = "true";
        //            }
        //        }



        //        Chart1.Series.Add(series);                
        //        Chart1.Titles["Title1"].Text = "Value Added"; 

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

            e.InputParameters[0] = null;
            e.InputParameters[1] = null;
            e.InputParameters[2] = null;
            e.InputParameters[3] = null;
            e.InputParameters[4] = null;
            e.InputParameters[5] = null;
            e.InputParameters[6] = 0;
            e.InputParameters[7] = null;
            e.InputParameters[8] = 0;
            e.InputParameters[9] = null;//Beds In Hospital operator
            e.InputParameters[10] = 0;
            e.InputParameters[11] = null;//Budget Patients operator
            e.InputParameters[12] = 0;
            e.InputParameters[13] = null;
            e.InputParameters[14] = null;
            e.InputParameters[15] = null;// Electronic Documents operator
            e.InputParameters[16] = 0;//Doc By Exception
            e.InputParameters[17] = null;
            e.InputParameters[18] = 0;
            e.InputParameters[19] = null;//HospitalSize operator
            e.InputParameters[20] = null;
            e.InputParameters[21] = null;
            e.InputParameters[22] = null;
            e.InputParameters[23] = 0;
            e.InputParameters[24] = null;
            e.InputParameters[25] = 0;
            e.InputParameters[26] = null;
            e.InputParameters[27] = null;
            e.InputParameters[28] = null;
            //e.InputParameters[29] = null;
            //e.InputParameters[30] = null;
            //e.InputParameters[31] = null;
            //e.InputParameters[32] = null;
            //e.InputParameters[33] = null;
            //e.InputParameters[34] = null;
            //e.InputParameters[35] = null;
            //e.InputParameters[36] = 0;
            //e.InputParameters[37] = null;
            //e.InputParameters[38] = 0;

        }

        protected void ObjectDataSource1_Selecting1(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters[0] = 58;
            e.InputParameters[1] = 58;
            e.InputParameters[2] = 58;
            e.InputParameters[3] = null;
            e.InputParameters[4] = null;
            e.InputParameters[5] = null;
            e.InputParameters[6] = null;
            e.InputParameters[7] = null;
            e.InputParameters[8] = null;
            e.InputParameters[9] = null;
            e.InputParameters[10] = null;
            e.InputParameters[11] = null;
            e.InputParameters[12] = null;
            e.InputParameters[13] = null;
            e.InputParameters[14] = null;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Chart1.SaveImage(Server.MapPath(@"~\Uploads\ChartImg.png"));            
        }

        protected void ObjectDataSource1_Selecting2(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters[0] = null;
            e.InputParameters[1] = null;
            e.InputParameters[2] = "70";
            e.InputParameters[3] = null;
            e.InputParameters[4] = null;
            e.InputParameters[5] = null;
            e.InputParameters[6] = null;
            e.InputParameters[7] = null;
            e.InputParameters[8] = null;
            e.InputParameters[9] = 0;
            e.InputParameters[10] = null;
            e.InputParameters[11] = 0;
            e.InputParameters[12] = null;
            e.InputParameters[13] = 0;
            e.InputParameters[14] = null;
            e.InputParameters[15] = 0;
            e.InputParameters[16] = null;
            e.InputParameters[17] = null;
            e.InputParameters[18] = null;
            e.InputParameters[19] = 0;
            e.InputParameters[20] = null;
            e.InputParameters[21] = 0;
            e.InputParameters[22] = 0;
            e.InputParameters[23] = null;
            e.InputParameters[24] = null;
            e.InputParameters[25] = null;
            e.InputParameters[26] = 0;
            e.InputParameters[27] = null;
            e.InputParameters[28] = 0;
            e.InputParameters[29] = null;
            e.InputParameters[30] = null;
            e.InputParameters[31] = null;
            e.InputParameters[32] = "";
            e.InputParameters[33] = "";
            e.InputParameters[34] = "RMC Location Categories";
            e.InputParameters[35] = null;
            e.InputParameters[36] = 0;
            e.InputParameters[37] = null;
            e.InputParameters[38] = 0;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //TextBox1.Text = BSDataImportConfigLocation.ConvertStringColumnToInt(TextBox1.Text).ToString();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //TextBox1.Text = BSDataImportConfigLocation.ConvertIntColumnToString(Convert.ToInt32(TextBox1.Text)).ToString();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            //ExportToExcel("Report.xls", Gridview1);
            Response.Clear();
            Response.AddHeader("Content-Disposition", "Attachment; FileName = Export.xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "Application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            Gridview1.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void Gridview1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;

                //for (int i = 1; i < drv.DataView.Table.Columns.Count; i++)
                //{
                //    e.Row.Cells[i].BackColor = System.Drawing.Color.Green;
                //}
                //e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#80ffff");
                e.Row.Cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffff80");
                e.Row.Cells[0].Font.Bold = true;
            }
        }



    }
}
