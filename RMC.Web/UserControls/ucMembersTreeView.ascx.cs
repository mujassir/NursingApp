using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using RMC.BussinessService;

namespace RMC.Web.UserControls
{
    public partial class ucMembersTreeView : System.Web.UI.UserControl
    {
        #region Properties
        /// <summary>
        /// Return passed HospitalInfoId
        /// <Author>Raman</Author>
        /// <createdOn>Aug 5, 2009</createdOn>
        /// </summary>
        private int HospitalInfoId
        {
            get
            {
                return Request.QueryString["HospitalInfoId"] != null ? Convert.ToInt32(Request.QueryString["HospitalInfoId"].ToString()) : 0;
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            BSTreeView objectTreeView = new BSTreeView();
            List<RMC.BusinessEntities.BEHospitalMembers> objectTreeStructure = objectTreeView.GetAllMembersOfHospital(HospitalInfoId);
            StringBuilder objectSeringButilder = new StringBuilder();
            objectSeringButilder.Append("<ul id='ulHospitalMemberTree' class='treeview-gray' style='color:#06569D'><b>List of Approved Users</b>");
            if (objectTreeStructure != null)
            {
                if (objectTreeStructure.Count > 0)
                {
                    foreach (RMC.BusinessEntities.BEHospitalMembers objectHospitalMembers in objectTreeStructure)
                    {
                        objectSeringButilder.Append("<li>");
                        objectSeringButilder.Append("<span><a href='UserProfile.aspx?UserId=" + objectHospitalMembers.UserID + "'>" + objectHospitalMembers.UserName + "</a></span>");
                        if (objectHospitalMembers.Owner)
                        {
                            objectSeringButilder.Append("<span> (Owner)</span>");
                        }
                        if (objectHospitalMembers.UnitList != null)
                        {
                            if (objectHospitalMembers.UnitList.Count > 0)
                            {
                                objectSeringButilder.Append("<ul>");
                                foreach (RMC.BusinessEntities.BETreeHospitalUnits objectHospitalUnits in objectHospitalMembers.UnitList)
                                {
                                    objectSeringButilder.Append("<li>");
                                    objectSeringButilder.Append("<span>" + objectHospitalUnits.HospitalDemographicID + "#" + " " + "-" + objectHospitalUnits.HospitalUnitName + "," + objectHospitalUnits.CreatedDate.ToShortDateString() + (objectHospitalUnits.ModifiedDate.HasValue == true ? "," + objectHospitalUnits.ModifiedDate.Value.ToShortDateString() : "") + "</span>");
                                    objectSeringButilder.Append("</li>");
                                }
                                objectSeringButilder.Append("</ul>");
                            }
                        }
                        objectSeringButilder.Append("</li>");
                    }
                }
                else
                {
                    objectSeringButilder.Append("<li>");
                    objectSeringButilder.Append("<span style ='color:Red'>No Record(s) to Display</span>");
                    objectSeringButilder.Append("</li>");
                }
            }
            objectSeringButilder.Append("</ul>");
            divmembersTreeView.InnerHtml = objectSeringButilder.ToString();

        }
    }
}