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
    public partial class OwnerShipType : System.Web.UI.UserControl
    {

        #region Variables

        //Bussiness Service Objects.
        RMC.BussinessService.BSOwnership _objectBSOwnership = null;
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
                    TextBoxOwnershipType.Text = TypeName;
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
                        LiteralUnitName.Visible = false;
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
                ex.Data.Add("Page", "OwnerShipType.ascx");
                LogManager._stringObject = "OwnerShipType.aspx ---- Page_Load";
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
                _objectBSOwnership = new RMC.BussinessService.BSOwnership();
                for (int index = 0; index < ListBoxOwnershipTypes.Items.Count; index++)
                {
                    if (ListBoxOwnershipTypes.Items[index].Selected)
                    {
                        if (_objectBSOwnership.DeleteOwnershipType(Convert.ToInt32(ListBoxOwnershipTypes.Items[index].Value)))
                        {
                            CommonClass.Show("Ownership Type Delete Successfully.");
                            //DisplayMessage("Ownership Type Delete Successfully.", System.Drawing.Color.Green);
                        }
                        else
                        {
                            CommonClass.Show("Fail to Delete Ownership Type.");
                            //DisplayMessage("Fail to Delete Ownership Type.", System.Drawing.Color.Red);
                        }
                    }
                }
                ResetControls();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSave_Click");
                ex.Data.Add("Page", "OwnerShipType.ascx");
                LogManager._stringObject = "OwnerShipType.ascx ---- ButtonSave_Click";
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
                    _objectBSOwnership = new RMC.BussinessService.BSOwnership();

                    if (_objectBSOwnership.InsertOwnershipType(SaveOwnerShipType()))
                    {
                        if (RequestID > 0)
                        {
                            _objectBSRequestForTypes = new RMC.BussinessService.BSRequestForTypes();
                            _objectBSRequestForTypes.DeleteRequestForTypes(RequestID);
                            Response.Redirect("~/Administrator/AdminHomePage.aspx", false);
                        }
                        CommonClass.Show("Ownership Type Save Successfully.");
                        //DisplayMessage("Ownership Type Save Successfully.", System.Drawing.Color.Green);
                    }
                    else
                    {
                        CommonClass.Show("Fail to Save Ownership Type.");
                        //DisplayMessage("Fail to Save Ownership Type.", System.Drawing.Color.Red);
                    }

                    ResetControls();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSave_Click");
                ex.Data.Add("Page", "OwnerShipType.ascx");
                LogManager._stringObject = "OwnerShipType.ascx ---- ButtonSave_Click";
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
                LogManager._stringObject = "OwnerShipType.ascx ---- ImageButtonBack_Click";
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
        private RMC.DataService.OwnerShipType SaveOwnerShipType()
        {
            try
            {
                RMC.DataService.OwnerShipType objectOwnerShipType = new RMC.DataService.OwnerShipType();

                objectOwnerShipType.OwnerShipTypeName = TextBoxOwnershipType.Text;
                return objectOwnerShipType;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "DisplayMessage");
                ex.Data.Add("Page", "OwnerShipType.ascx");
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
                TextBoxOwnershipType.Text = string.Empty;
                ListBoxOwnershipTypes.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "ResetControls");
                ex.Data.Add("Page", "OwnerShipType.ascx");
                throw ex;
            }
        }

        #endregion

    }
}