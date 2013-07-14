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

namespace RMC.Web.UserControls
{
    public partial class NationalDatabase : System.Web.UI.UserControl
    {
        #region Variables
        
        List<RMC.DataService.NationalDatabase> objectGenericNationalDatabase = null;
        RMC.BussinessService.BSNationalDatabase objectBSNationalDatabase = null;

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            bool flag = false;
            try
            {
               objectGenericNationalDatabase = new List<RMC.DataService.NationalDatabase>();
               objectBSNationalDatabase = new RMC.BussinessService.BSNationalDatabase();

                for (int i = 0; i < this.GridViewNationalDataBase.Rows.Count; i++)
                {
                    TextBox textboxValue = (TextBox)(this.GridViewNationalDataBase.Rows[i].FindControl("TextBoxValue"));
                    if (textboxValue.Text != string.Empty)
                    {
                        RMC.DataService.NationalDatabase objectNationalDatabase = new RMC.DataService.NationalDatabase();
                        //FunctionTypeId
                        objectNationalDatabase.NationalDatabaseCategoryID = Convert.ToInt32(this.GridViewNationalDataBase.Rows[i].Cells[2].Text);
                        objectNationalDatabase.Type = this.GridViewNationalDataBase.Rows[i].Cells[0].Text;
                        //GroupSequence
                        objectNationalDatabase.TypeValueID = Convert.ToInt32(this.GridViewNationalDataBase.Rows[i].Cells[3].Text);
                        objectNationalDatabase.ValueText = "%";
                        objectNationalDatabase.Value = Convert.ToDouble(((TextBox)(this.GridViewNationalDataBase.Rows[i].FindControl("TextboxValue"))).Text);

                        objectGenericNationalDatabase.Add(objectNationalDatabase);
                    }
                }
                if (objectGenericNationalDatabase.Count > 0)
                {
                    flag = objectBSNationalDatabase.InsertBulkNationalDatabase(objectGenericNationalDatabase);
                }
                GridViewNationalDataBase.DataBind();
                MultiView1.ActiveViewIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void GridViewNationalDataBase_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void GridViewUpdateNationalDatabase_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            bool flag = false;
            try
            {
                objectGenericNationalDatabase = new List<RMC.DataService.NationalDatabase>();
                objectBSNationalDatabase = new RMC.BussinessService.BSNationalDatabase();

                for (int i = 0; i < this.GridViewUpdateNationalDatabase.Rows.Count; i++)
                {

                    TextBox textboxNewValue = (TextBox)(this.GridViewUpdateNationalDatabase.Rows[i].FindControl("TextBoxNewValue"));
                    if (textboxNewValue.Text != string.Empty)
                    {
                        RMC.DataService.NationalDatabase objectNationalDatabase = new RMC.DataService.NationalDatabase();

                        objectNationalDatabase.NationalDatabaseID = Convert.ToInt32(this.GridViewUpdateNationalDatabase.Rows[i].Cells[0].Text);
                        objectNationalDatabase.Value = Convert.ToDouble(((TextBox)(this.GridViewUpdateNationalDatabase.Rows[i].FindControl("TextBoxNewValue"))).Text);

                        objectGenericNationalDatabase.Add(objectNationalDatabase);
                    }
                }

                if (objectGenericNationalDatabase.Count > 0)
                {
                    flag = objectBSNationalDatabase.UpdateBulkNationalDatabase(objectGenericNationalDatabase);
                }

                GridViewNationalDataBase.DataBind();
                GridViewUpdateNationalDatabase.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ButtonToInsertData_Click(object sender, EventArgs e)
        {
            GridViewNationalDataBase.DataBind();
            MultiView1.ActiveViewIndex = 0;
            ButtonToInsertData.Visible = false;
            ButtonToUpdateData.Visible = true;
        }

        protected void ButtonToUpdateData_Click(object sender, EventArgs e)
        {
            GridViewUpdateNationalDatabase.DataBind();
            MultiView1.ActiveViewIndex = 1;
            ButtonToInsertData.Visible = true;
            ButtonToUpdateData.Visible = false;
        }

        #endregion
    }
}