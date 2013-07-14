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
    public partial class HospitalType : System.Web.UI.UserControl
    {

        #region Variables

        //Bussiness Service Objects.
        RMC.BussinessService.BSHospitalType _objectBSHospitalType = null;
        RMC.BussinessService.BSRequestForTypes _objectBSRequestForTypes = null;
        
        //Bussiness Entity Objects.        
        RMC.BusinessEntities.BERequestForTypes _objectBERequestForTypes = null;
        
        #endregion

        #region Properties

        public int RequestID
        {
            get
            {
                return Convert.ToInt32(ViewState["RequestID"]);
            }
            set
            {
                ViewState["RequestID"] = value;
            }
        }

        public string TypeName
        {
            get
            {
                return Convert.ToString(ViewState["TypeName"]);
            }
            set
            {
                ViewState["TypeName"] = value;
            }
        }

        public string HospitalName
        {
            get
            {
                return (Request.QueryString["HospitalName"] != null ? Convert.ToString(Request.QueryString["HospitalName"]) : string.Empty);
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LabelErrorMsg.Text = string.Empty;
                PanelErrorMsg.Visible = false;

                if (!Page.IsPostBack)
                {
                    CommonClass objectCommonClass = new CommonClass();
                    objectCommonClass.BackButtonUrl = Request.UrlReferrer.AbsoluteUri;
                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        ButtonDelete.Visible = true;
                    }
                    else
                    {
                        ButtonDelete.Visible = false;
                    }
                }

                if (RequestID > 0)
                {                    
                    TextBoxHospitalType.Text = TypeName;
                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        _objectBSRequestForTypes = new RMC.BussinessService.BSRequestForTypes();

                        _objectBERequestForTypes = _objectBSRequestForTypes.GetRequestByRequestID(Convert.ToInt32(Request.QueryString["RequestID"]));
                        LiteralHospitalName.Visible = true;
                        LiteralUnitName.Visible = false;
                        if (_objectBERequestForTypes != null)
                        {
                            LiteralHospitalName.Text = " (" + _objectBERequestForTypes.HospitalName + ")";
                        }
                    }
                    else
                    {
                        LiteralHospitalName.Visible = false;
                        LiteralUnitName.Visible = false;
                    }
                }
                else
                {                    
                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        LiteralHospitalName.Visible = true;
                        LiteralUnitName.Visible = false ;
                        if (HospitalName != string.Empty)
                        {
                            LiteralHospitalName.Text = " (" + HospitalName + ")";
                        }
                    }
                    else
                    {
                        LiteralHospitalName.Visible = false;
                        LiteralUnitName.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "HospitalType.ascx");
                LogManager._stringObject = "HospitalType.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _objectBSHospitalType = new RMC.BussinessService.BSHospitalType();
                for (int index = 0; index < ListBoxHospitalTypes.Items.Count; index++)
                {
                    if (ListBoxHospitalTypes.Items[index].Selected)
                    {
                        if (_objectBSHospitalType.DeleteHospitalType(Convert.ToInt32(ListBoxHospitalTypes.Items[index].Value)))
                        {
                            CommonClass.Show("Hospital Type Delete Successfully.");
                            //DisplayMessage("Hospital Type Delete Successfully.", System.Drawing.Color.Green);
                        }
                        else
                        {
                            CommonClass.Show("Fail to Delete Hospital Type.");
                            //DisplayMessage("Fail to Delete Hospital Type.", System.Drawing.Color.Red);
                        }
                    }
                }
                ResetControls();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSave_Click");
                ex.Data.Add("Page", "HospitalType.ascx");
                LogManager._stringObject = "HospitalType.aspx ---- ButtonSave_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    _objectBSHospitalType = new RMC.BussinessService.BSHospitalType();

                    if (_objectBSHospitalType.InsertHospitalUnit(SaveHospitalType()))
                    {
                        if (RequestID > 0)
                        {
                            _objectBSRequestForTypes = new RMC.BussinessService.BSRequestForTypes();
                            _objectBSRequestForTypes.DeleteRequestForTypes(RequestID);
                            Response.Redirect("~/Administrator/AdminHomePage.aspx", false);
                        }
                        CommonClass.Show("Hospital Type Save Successfully.");
                        //DisplayMessage("Hospital Type Save Successfully.", System.Drawing.Color.Green);
                    }
                    else
                    {
                        CommonClass.Show("Fail to Save Hospital Type.");
                        //DisplayMessage("Fail to Save Hospital Type.", System.Drawing.Color.Red);
                    }

                    ResetControls();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSave_Click");
                ex.Data.Add("Page", "HospitalType.ascx");
                LogManager._stringObject = "HospitalType.aspx ---- ButtonSave_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                CommonClass objectCommonClass = new CommonClass();
                string backUrl = objectCommonClass.BackButtonUrl;
                Response.Redirect(backUrl, false);
            }
            catch (Exception ex)
            {
                LogManager._stringObject = "HospitalType.ascx ---- ImageButtonBack_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Use to Display message of Login Failure.
        /// Created By : Davinder Kumar.
        /// Creation Date : June 25, 2009
        /// Modified By : Davinder Kumar.
        /// Modified Date : July 21, 2009.
        /// </summary>
        /// <param name="msg">Message</param>
        /// <param name="color">Color</param>
        private void DisplayMessage(string msg, System.Drawing.Color color)
        {
            try
            {
                LabelErrorMsg.Text = msg;
                PanelErrorMsg.ForeColor = color;
                PanelErrorMsg.Visible = true;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DisplayMessage");
                ex.Data.Add("Page", "HospitalType.ascx");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private RMC.DataService.HospitalType SaveHospitalType()
        {
            try
            {
                RMC.DataService.HospitalType objectHospitalType = new RMC.DataService.HospitalType();

                objectHospitalType.HospitalTypeName = TextBoxHospitalType.Text;
                return objectHospitalType;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DisplayMessage");
                ex.Data.Add("Page", "HospitalType.ascx");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetControls()
        {
            try
            {
                TextBoxHospitalType.Text = string.Empty;
                ListBoxHospitalTypes.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ResetControls");
                ex.Data.Add("Page", "HospitalType.ascx");
                throw ex;
            }
        }

        #endregion

    }
}