using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Linq.SqlClient;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace RMC.Web.UserControls
{
    public partial class Validation : System.Web.UI.UserControl
    {
        RMC.DataService.RMCDataContext _objectRMCDataContext = new RMC.DataService.RMCDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {            

            List<RMC.BusinessEntities.BEnursePDADetail> objNursePDADetail = (from l1 in _objectRMCDataContext.NursePDADetails.ToList()
                                                                                    join l2 in _objectRMCDataContext.Locations.ToList() on l1.LocationID equals l2.LocationID into joinlist1list2
                                                                                    from l2 in joinlist1list2.DefaultIfEmpty()
                                                                                    join l3 in _objectRMCDataContext.Activities.ToList() on l1.ActivityID equals l3.ActivityID into joinlist1list3
                                                                                    from l3 in joinlist1list3.DefaultIfEmpty()
                                                                                    join l4 in _objectRMCDataContext.SubActivities.ToList() on l1.SubActivityID equals l4.SubActivityID into joinlist1list4
                                                                                    from l4 in joinlist1list4.DefaultIfEmpty()
                                                                                    where l1.IsActive == true && l1.IsDeleted == false && l1.IsErrorExist == false && l1.IsActiveError == false
                                                                                    select new RMC.BusinessEntities.BEnursePDADetail
                                                                                    {
                                                                                        Location = l2 != null ? l2.Location1 : "",
                                                                                        Activity = l3 != null ? l3.Activity1 : "",
                                                                                        Subactivity = l4 != null ? l4.SubActivity1 : ""
                                                                                    }).Distinct().ToList();

            if (objNursePDADetail != null)
            {
                GridValidData.DataSource = objNursePDADetail;
                GridValidData.DataBind();
            }
            List<RMC.BusinessEntities.BEnursePDADetail> objNursePDADetailInvalid = (from l1 in _objectRMCDataContext.NursePDADetails.ToList()
                                                                                    join l2 in _objectRMCDataContext.Locations.ToList() on l1.LocationID equals l2.LocationID into joinlist1list2
                                                                                    from l2 in joinlist1list2.DefaultIfEmpty()
                                                                                    join l3 in _objectRMCDataContext.Activities.ToList() on l1.ActivityID equals l3.ActivityID into joinlist1list3
                                                                                    from l3 in joinlist1list3.DefaultIfEmpty()
                                                                                    join l4 in _objectRMCDataContext.SubActivities.ToList() on l1.SubActivityID equals l4.SubActivityID into joinlist1list4
                                                                                    from l4 in joinlist1list4.DefaultIfEmpty()
                                                                                    where l1.IsActive == true && l1.IsDeleted == false && l1.IsErrorExist == true && l1.IsActiveError == true
                                                                                    select new RMC.BusinessEntities.BEnursePDADetail
                                                                                    {
                                                                                        Location = l2 != null ? l2.Location1 : "",
                                                                                        Activity = l3 != null ? l3.Activity1 : "",
                                                                                        Subactivity = l4 != null ? l4.SubActivity1 : ""
                                                                                    }).Distinct().ToList();

            if (objNursePDADetailInvalid != null)
            {
                GridViewInValiedData.DataSource = objNursePDADetailInvalid;
                GridViewInValiedData.DataBind();
            }
           

        }
        private void ExportGridView()
        {
            string attachment = "attachment; filename=testReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            // Create a form to contain the grid
            HtmlForm frm = new HtmlForm();           
            GridValidData.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(GridValidData);
            frm.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        private void ExportGridView2()
        {

            string attachment = "attachment; filename=testReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            // Create a form to contain the grid
            HtmlForm frm = new HtmlForm();            
            GridViewInValiedData.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(GridViewInValiedData);
            frm.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();            
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            ExportGridView();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ExportGridView2();
        }
    }
}