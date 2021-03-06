﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RMC.Web.Users
{
    public partial class AjaxClassicCallBack1 : System.Web.UI.Page
    {

        #region Variables

        int index = 0;

        #endregion
               
        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Buffer = false;

            if (Request.QueryString["Year"] != null)
            {
                RMC.BussinessService.BSMaintainControlState objectBSMaintainControlState = new RMC.BussinessService.BSMaintainControlState();

                Session["Year"] = Convert.ToString(Request.QueryString["Year"]);
                if (Session["Month"] != null)
                {
                    objectBSMaintainControlState.UpdateMaintainControlStateForYearMonth(CommonClass.UserInformation.UserID, Convert.ToString(Session["Year"]), Convert.ToString(Session["Month"]));
                }
                else
                {
                    objectBSMaintainControlState.UpdateMaintainControlStateForYearMonth(CommonClass.UserInformation.UserID, Convert.ToString(Session["Year"]), null);
                }
                Response.Write(Convert.ToString(Request.QueryString["Year"]));
            }

            if (Request.QueryString["Month"] != null)
            {
                RMC.BussinessService.BSMaintainControlState objectBSMaintainControlState = new RMC.BussinessService.BSMaintainControlState();

                Session["Month"] = Convert.ToString(Request.QueryString["Month"]);
                if (Session["Year"] != null)
                {
                    objectBSMaintainControlState.UpdateMaintainControlStateForYearMonth(CommonClass.UserInformation.UserID, Convert.ToString(Session["Year"]), Convert.ToString(Session["Month"]));
                }
                else
                {
                    objectBSMaintainControlState.UpdateMaintainControlStateForYearMonth(CommonClass.UserInformation.UserID, null, Convert.ToString(Session["Month"]));
                }
                Response.Write(Convert.ToString(Request.QueryString["Month"]));
            }

            Response.End();
        }

        #endregion

    }
}
