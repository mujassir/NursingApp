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
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class UnitType : System.Web.UI.UserControl
    {

        #region Variables

        //Business Service Objects.
        RMC.BussinessService.BSUnitType _objectBSUnitType = null;
        RMC.BussinessService.BSRequestForTypes _objectBSRequestForTypes = null;

        //Data Service Objects.
        RMC.DataService.UnitType _objectUnitType = null;

        //Bussiness Entity Objects.        
        RMC.BusinessEntities.BERequestForTypes _objectBERequestForTypes = null;

        int _hospitalDemographicId;

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
                if (!Page.IsPostBack)
                {
                    CommonClass objectCommonClass = new CommonClass();
                    objectCommonClass.BackButtonUrl = Request.UrlReferrer.AbsoluteUri;
                }
                if (Request.QueryString["HospitalDemographicId"] != null)
                {
                    _hospitalDemographicId = Convert.ToInt32(Request.QueryString["HospitalDemographicId"]);

                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        ButtonDelete.Visible = true;
                        //ImageButtonBack.PostBackUrl = "~/Administrator/HospitalDemographicDetail.aspx?HospitalDemographicId=" + _hospitalDemographicId;
                    }
                    else
                    {
                        ButtonDelete.Visible = false;
                        //ImageButtonBack.PostBackUrl = "~/Users/HospitalDemographicDetail.aspx?HospitalDemographicId=" + _hospitalDemographicId;
                    }
                }
                else
                {
                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        ButtonDelete.Visible = true;
                        //ImageButtonBack.PostBackUrl = "~/Administrator/HospitalUnitInfomation.aspx";
                    }
                    else
                    {
                        ButtonDelete.Visible = false;
                        //ImageButtonBack.PostBackUrl = "~/Users/HospitalUnitInformation.aspx";
                    }
                }
                if (RequestID > 0)
                {
                    TextBoxUnitType.Text = TypeName;
                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        _objectBSRequestForTypes = new RMC.BussinessService.BSRequestForTypes();

                        _objectBERequestForTypes = _objectBSRequestForTypes.GetRequestByRequestID(Convert.ToInt32(Request.QueryString["RequestID"]));
                        LiteralHospitalName.Visible = true;
                        LiteralUnitName.Visible = true;
                        if (_objectBERequestForTypes != null)
                        {
                            LiteralHospitalName.Text = " (" + _objectBERequestForTypes.HospitalName + ")";
                            LiteralUnitName.Text = " (" + _objectBERequestForTypes.HospitalUnitName + ")";
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
                        LiteralUnitName.Visible = true;
                        if (CommonClass.SessionInfomation != null)
                        {
                            if (CommonClass.SessionInfomation.HospitalName.Length > 0)
                            {
                                LiteralHospitalName.Text = " (" + CommonClass.SessionInfomation.HospitalName + ")";
                            }
                            if (CommonClass.SessionInfomation.HospitalUnitName.Length > 0)
                            {
                                LiteralUnitName.Text = " (" + CommonClass.SessionInfomation.HospitalUnitName + ")";
                            }
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
                ex.Data.Add("Page", "UnitType.aspx");
                LogManager._stringObject = "UnitType.aspx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            bool flag = false;
            try
            {
                RMC.BussinessService.BSUnitType objectBSUnitType = new RMC.BussinessService.BSUnitType();

                flag = objectBSUnitType.DeleteUnitType(Convert.ToInt32(ListBoxUnitTypes.SelectedValue));
                if (flag)
                {
                    CommonClass.Show("Delete Record Successfully.");
                    //DisplayMessage("Delete Record Successfully.", System.Drawing.Color.Green);
                }
                else
                {
                    CommonClass.Show("Fail to Delete Record.");
                    //DisplayMessage("Fail to Delete Record.", System.Drawing.Color.Red);
                }
                ListBoxUnitTypes.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonDelete_Click");
                ex.Data.Add("Page", "UnitType.aspx");
                LogManager._stringObject = "UnitType.aspx ---- ButtonDelete_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Redirects to previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <CreatedBy>Raman</CreatedBy>
        /// <CreatedOn>Aug 06, 2009</CreatedOn>
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
                ex.Data.Add("Function", "ImageButtonBack_Click");
                LogManager._stringObject = "UnitType.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        #region Private Methods

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                int userID = 0;
                string userName = string.Empty;
                bool flag = false;

                if (Page.IsValid)
                {
                    _objectBSUnitType = new RMC.BussinessService.BSUnitType();
                    _objectUnitType = new RMC.DataService.UnitType();

                    if (HttpContext.Current.User.IsInRole("admin"))
                    {
                        userID = CommonClass.UserInformation.UserID;
                        userName = CommonClass.UserInformation.FirstName + " " + CommonClass.UserInformation.LastName;
                    }

                    _objectUnitType.UnitTypeName = TextBoxUnitType.Text;
                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        _objectUnitType.CreatedBy = "Super Admin";
                    }
                    else
                    {
                        _objectUnitType.CreatedBy = userName;
                    }

                    _objectUnitType.CreatedDate = DateTime.Now;

                    flag = _objectBSUnitType.InsertUnitType(_objectUnitType);

                    if (flag)
                    {
                        if (RequestID > 0)
                        {
                            _objectBSRequestForTypes = new RMC.BussinessService.BSRequestForTypes();
                            _objectBSRequestForTypes.DeleteRequestForTypes(RequestID);
                            Response.Redirect("~/Administrator/AdminHomePage.aspx", false);
                        }
                        CommonClass.Show("Unit Type Save Successfully.");
                        //DisplayMessage("Unit Type Save Successfully.", System.Drawing.Color.Green);
                    }
                    else
                    {
                        CommonClass.Show("Fail to Save Unit Type.");
                        //DisplayMessage("Fail to Save Unit Type.", System.Drawing.Color.Red);
                    }

                    TextBoxUnitType.Text = string.Empty;
                    ListBoxUnitTypes.DataBind();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonSave_Click");
                ex.Data.Add("Page", "UnitType.aspx");
                LogManager._stringObject = "UnitType.aspx ---- ButtonSave_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                TextBoxUnitType.Text = string.Empty;
                PanelErrorMsg.Visible = false;
                LabelErrorMsg.Text = string.Empty;
                ListBoxUnitTypes.DataBind();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonReset_Click");
                ex.Data.Add("Page", "UnitType.aspx");
                LogManager._stringObject = "UnitType.aspx ---- ButtonReset_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

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
                ex.Data.Add("Class", "EditOrViewDemographicDetail");
                throw ex;
            }
        }

        #endregion
               
    }
}