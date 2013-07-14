using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using LogExceptions;
using RMC.BussinessService;

namespace RMC.Web.Administrator
{
    public partial class HospitalList : System.Web.UI.Page
    {

        #region Variables

        //Business Service Objects
        BSHospitalInfo _objectBSHospitalInfo = null;        

        //Fundamental Data Types.
        bool _flag;

        #endregion

        #region Events

        /// <summary>
        /// Reset the Gridview Records.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewHospitalInfoList.DataSourceID = "LinqDataSourceHospitalInfoList";
                GridViewHospitalInfoList.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonReset_Click");
                ex.Data.Add("Page", "HospitalList.aspx");
                LogManager._stringObject = "HospitalList.aspx ---- ButtonReset_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        /// <summary>
        /// Update Hospital Records.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 3, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                List<RMC.BusinessEntities.BEHospitalList> genericBEHospitalList = new List<RMC.BusinessEntities.BEHospitalList>();
                _objectBSHospitalInfo = new BSHospitalInfo();

                if (GridViewHospitalInfoList.Rows.Count > 0)
                {
                    foreach (GridViewRow grdRow in GridViewHospitalInfoList.Rows)
                    {
                        RMC.BusinessEntities.BEHospitalList objectBEHospitalList = new RMC.BusinessEntities.BEHospitalList();
                        CheckBox chkBox = (CheckBox)grdRow.FindControl("CheckBoxActive");

                        objectBEHospitalList.HospitalInfoID = Convert.ToInt32(GridViewHospitalInfoList.DataKeys[grdRow.RowIndex].Value);
                        objectBEHospitalList.IsActive = chkBox.Checked;

                        genericBEHospitalList.Add(objectBEHospitalList);
                    }
                    _flag = _objectBSHospitalInfo.InsertActiveDeactiveHospitalList(genericBEHospitalList);

                    if (_flag)
                    {
                        DisplayMessage("Hospital List Update Successfully.", System.Drawing.Color.Green);
                    }
                    else
                    {
                        DisplayMessage("Fail to Update Hospital List.", System.Drawing.Color.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSubmit_Click");
                ex.Data.Add("Page", "HospitalList.aspx");
                LogManager._stringObject = "HospitalList.aspx ---- ButtonSubmit_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }

        /// <summary>
        /// Use in Hospital Information GridView to Delete Particular Record.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 8, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
        {
            RMC.DataService.HospitalInfo objectHospitalInfo = new RMC.DataService.HospitalInfo();
            _objectBSHospitalInfo = new BSHospitalInfo();
            try
            {
                ImageButton imageButtonForDelete = (ImageButton)sender;
                GridViewRow grdRow = (GridViewRow)imageButtonForDelete.NamingContainer;

                objectHospitalInfo.HospitalInfoID = Convert.ToInt32(GridViewHospitalInfoList.DataKeys[grdRow.RowIndex].Value);
                //objectHospitalInfo.IsDeleted = true;
                //objectHospitalInfo.DeletedBy = CommonClass.UserInformation.UserID;
                //objectHospitalInfo.DeletedDate = DateTime.Now;

                _flag = _objectBSHospitalInfo.DeleteHospitalInformation(objectHospitalInfo);

                if (_flag)
                {
                    DisplayMessage("Hospital Infomation Delete Successfully.", System.Drawing.Color.Green);
                }
                else
                {
                    DisplayMessage("Fail to Delete Hospital Infomation.", System.Drawing.Color.Red);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ImageButtonDelete_Click");
                ex.Data.Add("Page", "HospitalList.aspx");
                LogManager._stringObject = "HospitalList.aspx ---- ImageButtonDelete_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
            finally
            {
                objectHospitalInfo = null;
            }
        }

        /// <summary>
        /// Use in Hospital Information GridView to Edit Particular Record.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 8, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImageButtonEdit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton imageButtonForEdit = (ImageButton)sender;
                GridViewRow grdRow = (GridViewRow)imageButtonForEdit.NamingContainer;
               
                Session["HospitalInfoID"] = GridViewHospitalInfoList.DataKeys[grdRow.RowIndex].Value;
                Response.Redirect("EditHospitalInfomation.aspx", false);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ImageButtonEdit_Click");
                ex.Data.Add("Page", "HospitalList.aspx");
                LogManager._stringObject = "HospitalList.aspx ---- ImageButtonEdit_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        }


        /// <summary>
        /// Page Load Method.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 06, 2009.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "Administrator/AdminMaster.Master");
                LogManager._stringObject = "AdminMaster.Master ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
            }
        }
        
        #endregion

        #region Private Methods

        //Use to Display message of Login Failure.
        private void DisplayMessage(string msg, System.Drawing.Color color)
        {
            try
            {
                LabelErrorMsg.Text = msg;
                LabelErrorMsg.ForeColor = color;
                LabelErrorMsg.Visible = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DisplayMessage");
                ex.Data.Add("Class", "Login");
                throw ex;
            }
        }
        //End Of DisplayMessage Methods.

        #endregion

    }
    //End Of HospitalList Class.
}
//End Of Namespace.