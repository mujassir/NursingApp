using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogExceptions;

namespace RMC.Web.Administrator
{
    public partial class Type : System.Web.UI.Page
    {

        #region Variables
        
        //Bussiness Service Objects.
        RMC.BussinessService.BSRequestForTypes _objectBSRequestForTypes = null;

        //Bussiness Entity Objects.
        RMC.BusinessEntities.BERequestForTypes _objectBERequestForTypes = null;

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Control ctl = null;
                _objectBSRequestForTypes = new RMC.BussinessService.BSRequestForTypes();

                _objectBERequestForTypes = _objectBSRequestForTypes.GetRequestByRequestID(Convert.ToInt32(Request.QueryString["RequestID"]));
                if (_objectBERequestForTypes != null)
                {
                    if (_objectBERequestForTypes.Type.ToLower().Trim() == "unit type")
                    {
                        ctl = LoadControl("~/UserControls/UnitType.ascx");
                        ((RMC.Web.UserControls.UnitType)ctl).RequestID = _objectBERequestForTypes.RequestID;
                        ((RMC.Web.UserControls.UnitType)ctl).TypeName = _objectBERequestForTypes.Value;
                    }
                    else if (_objectBERequestForTypes.Type.ToLower().Trim() == "pharmacy type")
                    {
                        ctl = LoadControl("~/UserControls/PharmacyType.ascx");
                        ((RMC.Web.UserControls.PharmacyType)ctl).RequestID = _objectBERequestForTypes.RequestID;
                        ((RMC.Web.UserControls.PharmacyType)ctl).TypeName = _objectBERequestForTypes.Value;
                    }
                    else if (_objectBERequestForTypes.Type.ToLower().Trim() == "ownership type")
                    {
                        ctl = LoadControl("~/UserControls/OwnershipType.ascx");
                        ((RMC.Web.UserControls.OwnerShipType)ctl).RequestID = _objectBERequestForTypes.RequestID;
                        ((RMC.Web.UserControls.OwnerShipType)ctl).TypeName = _objectBERequestForTypes.Value;
                    }
                    else if (_objectBERequestForTypes.Type.ToLower().Trim() == "hospital type")
                    {
                        ctl = LoadControl("~/UserControls/HospitalType.ascx");
                        ((RMC.Web.UserControls.HospitalType)ctl).RequestID = _objectBERequestForTypes.RequestID;
                        ((RMC.Web.UserControls.HospitalType)ctl).TypeName = _objectBERequestForTypes.Value;
                    }

                    if (ctl != null)
                    {
                        PlaceHolder1.Controls.Add(ctl);
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
                //DisplayMessage(LogManager.ShowErrorDetail(ex), System.Drawing.Color.Red);
            }
        } 

        #endregion

    }
}
