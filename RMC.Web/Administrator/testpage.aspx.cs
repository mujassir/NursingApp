using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;


namespace RMC.Web.Administrator
{
    public partial class testpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Populate series data
            Random random = new Random();
            for (int pointIndex = 0; pointIndex < 10; pointIndex++)
            {
                Chart1.Series["Series1"].Points.AddY(random.Next(45, 95));
            }

            // Set chart type
            Chart1.Series["Series1"].ChartType = SeriesChartType.StackedArea100;

            // Show point labels
            Chart1.Series["Series1"].IsValueShownAsLabel = true;

            // Disable X axis margin
            Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = false;

            // Enable 3D
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

            // Set the first two series to be grouped into Group1
            Chart1.Series["LightBlue"]["StackedGroupName"] = "Group1";
            Chart1.Series["Gold"]["StackedGroupName"] = "Group1";

            // Set the last two series to be grouped into Group2
            Chart1.Series["Red"]["StackedGroupName"] = "Group2";
            Chart1.Series["DarkBlue"]["StackedGroupName"] = "Group2";

        }
    }
}