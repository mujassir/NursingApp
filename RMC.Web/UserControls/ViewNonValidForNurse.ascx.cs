using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class ViewNonValidForNurse : System.Web.UI.UserControl
    {

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkButtonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow grdRow = (GridViewRow)((LinkButton)sender).NamingContainer;
                GridViewNonValidDataOfNurse.EditIndex = grdRow.RowIndex;
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ViewNonValidForNurse.ascx ---- LinkButtonEdit_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int patientsPerNurse = 0;
                bool endEdit = true;
                GridViewRow grdRow = (GridViewRow)((LinkButton)sender).NamingContainer;
                TextBox txtBoxConfigName = (TextBox)grdRow.FindControl("TextBoxConfigurationName");
                TextBox txtBoxNurseName = (TextBox)grdRow.FindControl("TextBoxNurseName");
                TextBox txtBoxPatientsPerNurse = (TextBox)grdRow.FindControl("TextBoxPatientsPerNurse");

                if (txtBoxConfigName.Text.Length == 0)
                {
                    CommonClass.Show("Must enter the Valid Configuration Name.");
                    endEdit = false;
                }

                if (txtBoxNurseName.Text.Length == 0)
                {
                    CommonClass.Show("Must enter the Nurse Name.");
                    endEdit = false;
                }

                bool flag = int.TryParse(txtBoxPatientsPerNurse.Text, out patientsPerNurse);

                if (!flag)
                {
                    CommonClass.Show("Pleae enter the valid Numeric Value in Patients Per Nurse.");
                    endEdit = false;
                }

                if (endEdit)
                {
                    RMC.BussinessService.BSNursePDADetail objectBSNursePDADetail = new RMC.BussinessService.BSNursePDADetail();
                    int nurseID = Convert.ToInt32(GridViewNonValidDataOfNurse.DataKeys[grdRow.RowIndex].Value);

                    objectBSNursePDADetail.UpdateNursePDAInfoFields(nurseID, txtBoxNurseName.Text, txtBoxConfigName.Text, patientsPerNurse);
                    GridViewNonValidDataOfNurse.EditIndex = -1;
                    GridViewNonValidDataOfNurse.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ViewNonValidForNurse.ascx ---- LinkButtonUpdate_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewNonValidDataOfNurse.EditIndex = -1;
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ViewNonValidForNurse.ascx ---- LinkButtonCancel_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void GridViewNonValidDataOfNurse_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                int edit = Convert.ToString(e.Row.RowState).IndexOf("Edit");
                if (edit > -1)
                {

                    TextBox txtBoxConfigName = (TextBox)e.Row.FindControl("TextBoxConfigurationName");
                    TextBox txtBoxNurseName = (TextBox)e.Row.FindControl("TextBoxNurseName");
                    TextBox txtBoxPatientsPerNurse = (TextBox)e.Row.FindControl("TextBoxPatientsPerNurse");

                    Literal literalConfigName = (Literal)e.Row.FindControl("LiteralConfigName");
                    Literal literalNurseName = (Literal)e.Row.FindControl("LiteralNurseName");
                    Literal literalPatientPerNurse = (Literal)e.Row.FindControl("LiteralPatientsPerNurseName");

                    if (literalConfigName.Text.ToLower() == "true")
                    {
                        txtBoxConfigName.ForeColor = System.Drawing.Color.Red;
                    }

                    if (literalNurseName.Text.ToLower() == "true")
                    {
                        txtBoxNurseName.ForeColor = System.Drawing.Color.Red;
                    }

                    if (literalPatientPerNurse.Text.ToLower() == "true")
                    {
                        txtBoxPatientsPerNurse.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ViewNonValidForNurse.ascx ---- GridViewNonValidDataOfNurse_RowDataBound";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                RMC.BussinessService.BSNursePDADetail objectBSNursePDADetail = new RMC.BussinessService.BSNursePDADetail();
                GridViewRow grdRow = (GridViewRow)((LinkButton)sender).NamingContainer;

                int nurseID = Convert.ToInt32(GridViewNonValidDataOfNurse.DataKeys[grdRow.RowIndex].Value);
                objectBSNursePDADetail.DeleteNursePDAInfo(nurseID);
                GridViewNonValidDataOfNurse.DataBind();
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "ViewNonValidForNurse.ascx ---- LinkButtonDelete_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

    }
}