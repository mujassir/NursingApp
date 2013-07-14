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
    public partial class StandardizeLocationName : System.Web.UI.UserControl
    {
        #region Variables

        //Bussiness Service Object.
        RMC.BussinessService.BSReports _objectBSReports = null;

        //Bussiness Entity Object.
        RMC.BusinessEntities.BECategoryProfile _objectBECategoryProfile = null;

        #endregion

        protected void GridViewLastLocation_PreRender(object sender, EventArgs e)
        {
            try
            {
                //if (ViewState["RowIndex"] != null && ViewState["Mode"] != null)
                //{
                //    GridViewRow grdRow = GridViewLocation.Rows[Convert.ToInt32(ViewState["RowIndex"])];

                //    //System.Web.UI.WebControls.TextBox txtLocation = (System.Web.UI.WebControls.TextBox)grdRow.FindControl("TextBoxLocation");
                //    DropDownList ddlLastLocation = (DropDownList)grdRow.FindControl("DropDownListLastLocation");
                //    LinkButton lnkUpdateEditLast = (LinkButton)grdRow.FindControl("LinkButtonEditUpdateLast");
                //    if (Convert.ToString(ViewState["Mode"]) == "Edit")
                //    {


                //        //txtLocation.Visible = true;
                //        ddlLastLocation.Visible = true;
                //        lnkUpdateEditLast.Visible = true;

                //    }
                //    ViewState.Remove("RowIndex");
                //    ViewState.Remove("Mode");
                //}
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "GridViewValidation_PreRender");
                ex.Data.Add("Page", "ValidationData.ascx");
                LogManager._stringObject = "ValidationData.ascx ---- GridViewValidation_PreRender";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void GridViewLocation_PreRender(object sender, EventArgs e)
        {
            try
            {
                //if (ViewState["RowIndex"] != null && ViewState["Mode"] != null)
                //{
                //    GridViewRow grdRow = GridViewLocation.Rows[Convert.ToInt32(ViewState["RowIndex"])];

                //    //System.Web.UI.WebControls.TextBox txtLocation = (System.Web.UI.WebControls.TextBox)grdRow.FindControl("TextBoxLocation");
                //    DropDownList ddlLocation = (DropDownList)grdRow.FindControl("DropDownListLocation");
                //    LinkButton lnkUpdateEdit = (LinkButton)grdRow.FindControl("LinkButtonEditUpdate");
                //    if (Convert.ToString(ViewState["Mode"]) == "Edit")
                //    {
                        
                        
                //        //txtLocation.Visible = true;
                //        ddlLocation.Visible = true;
                //        lnkUpdateEdit.Visible = true;
                        
                //    }
                //    ViewState.Remove("RowIndex");
                //    ViewState.Remove("Mode");
                //}
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "GridViewValidation_PreRender");
                ex.Data.Add("Page", "ValidationData.ascx");
                LogManager._stringObject = "ValidationData.ascx ---- GridViewValidation_PreRender";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkButtonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewLocation.EditIndex = -1;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "LinkButtonCancel_Click");
                ex.Data.Add("Page", "ValidationData.ascx");
                LogManager._stringObject = "ValidationData.ascx ---- LinkButtonCancel_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonEditUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int  locationID;
                string renameLocation, lastLocation;
                RMC.DataService.Location objectLocation = new RMC.DataService.Location();
                
                _objectBSReports = new RMC.BussinessService.BSReports();

                GridViewRow grdRow = (GridViewRow)((LinkButton)sender).NamingContainer;
                locationID = Convert.ToInt32(((Literal)GridViewLocation.Rows[grdRow.RowIndex].FindControl("LiteralLocationID")).Text);
                lastLocation = ((Literal)GridViewLocation.Rows[grdRow.RowIndex].FindControl("LiteralLastLocation")).Text;
                //renameLocation = ((System.Web.UI.WebControls.TextBox)GridViewLocation.Rows[grdRow.RowIndex].FindControl("TextBoxLocation")).Text;
                //locationID = Convert.ToInt32(((DropDownList)GridViewLocation.Rows[grdRow.RowIndex].FindControl("DropDownListLocation")).SelectedValue);
                renameLocation = ((DropDownList)GridViewLocation.Rows[grdRow.RowIndex].FindControl("DropDownListLocation")).SelectedItem.Text;

                objectLocation.LocationID = locationID;
                objectLocation.RenameLocation = renameLocation;



                _objectBSReports.UpdateEditLocationRename(objectLocation, lastLocation);
                GridViewLocation.EditIndex = -1;
                GridViewLocation.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "LinkButtonEditUpdate_Click");
                ex.Data.Add("Page", "ValidationData.ascx");
                LogManager._stringObject = "ValidationData.ascx ---- LinkButtonEditUpdate_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdRow = (GridViewRow)((LinkButton)sender).NamingContainer;
                GridViewLocation.EditIndex = grdRow.RowIndex;
                ViewState["RowIndex"] = grdRow.RowIndex;
                ViewState["Mode"] = "Edit";
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "LinkButtonEdit_Click");
                ex.Data.Add("Page", "ValidationData.ascx");
                LogManager._stringObject = "ValidationData.ascx ---- LinkButtonEdit_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonCancelLast_Click(object sender, EventArgs e)
        {
            try
            {
                //GridViewLastLocation.EditIndex = -1;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "LinkButtonCancel_Click");
                ex.Data.Add("Page", "ValidationData.ascx");
                LogManager._stringObject = "ValidationData.ascx ---- LinkButtonCancel_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonEditUpdateLast_Click(object sender, EventArgs e)
        {
            try
            {
                //int lastLocationID;
                //string renameLastLocation;
                //RMC.DataService.LastLocation objectLastLocation = new RMC.DataService.LastLocation();

                //_objectBSReports = new RMC.BussinessService.BSReports();

                //GridViewRow grdRow = (GridViewRow)((LinkButton)sender).NamingContainer;
                //lastLocationID = Convert.ToInt32(((Literal)GridViewLastLocation.Rows[grdRow.RowIndex].FindControl("LiteralLastLocationID")).Text);
                ////renameLocation = ((System.Web.UI.WebControls.TextBox)GridViewLocation.Rows[grdRow.RowIndex].FindControl("TextBoxLocation")).Text;
                ////locationID = Convert.ToInt32(((DropDownList)GridViewLocation.Rows[grdRow.RowIndex].FindControl("DropDownListLocation")).SelectedValue);
                //renameLastLocation = ((DropDownList)GridViewLastLocation.Rows[grdRow.RowIndex].FindControl("DropDownListLastLocation")).SelectedItem.Text;

                //objectLastLocation.LastLocationID = lastLocationID;
                //objectLastLocation.RenameLastLocation = renameLastLocation;


                //_objectBSReports.UpdateEditLastLocationRename(objectLastLocation);
                //GridViewLastLocation.EditIndex = -1;
                //GridViewLastLocation.DataBind();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ValidationData.ascx ---- LinkButtonEditUpdate_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonEditLast_Click(object sender, EventArgs e)
        {
            try
            {
                //GridViewRow grdRow = (GridViewRow)((LinkButton)sender).NamingContainer;
                //GridViewLastLocation.EditIndex = grdRow.RowIndex;
                //ViewState["RowIndex"] = grdRow.RowIndex;
                //ViewState["Mode"] = "Edit";
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "LinkButtonEdit_Click");
                ex.Data.Add("Page", "ValidationData.ascx");
                LogManager._stringObject = "ValidationData.ascx ---- LinkButtonEdit_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            if (HttpContext.Current.User.IsInRole("superadmin"))
            {
                Response.Redirect("~/Administrator/LocationProfile.aspx", false);
            }
            else
            {
                Response.Redirect("~/Users/LocationProfile.aspx", false);
            }
        }

     

    }
}