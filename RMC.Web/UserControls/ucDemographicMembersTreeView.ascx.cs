using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using RMC.BussinessService;

namespace RMC.Web.UserControls
{
    public partial class ucDemographicMembersTreeView : System.Web.UI.UserControl
    {
        #region Properties

        /// <summary>
        /// Return passed HospitalInfoId
        /// <Author>Raman</Author>
        /// <createdOn>Aug 5, 2009</createdOn>
        /// </summary>
        private int HospitalDemographicId
        {
            get
            {
                return Request.QueryString["HospitalDemographicId"] != null ? Convert.ToInt32(Request.QueryString["HospitalDemographicId"].ToString()) : 0;
            }
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            BuildDemographicMembersTree();
        }
        public void BuildDemographicMembersTree()
        {
            BSTreeView objectTreeView = new BSTreeView();
            List<RMC.BusinessEntities.BEHospitalMembers> objectTreeStructure = objectTreeView.GetAllMembersOfHospitalUnit(HospitalDemographicId);
            StringBuilder objectSeringButilder = new StringBuilder();
            objectSeringButilder.Append("<ul id='ulHospitalDemographicMemberTree' class='treeview-gray' style='color:#06569D'><b>List of Approved Users</b>");
            if (objectTreeStructure != null)
            {
                if (objectTreeStructure.Count > 0)
                {

                    foreach (RMC.BusinessEntities.BEHospitalMembers objectHospitalMembers in objectTreeStructure)
                    {
                        objectSeringButilder.Append("<li>");
                        objectSeringButilder.Append("<span><a href='UserProfile.aspx?UserId=" + objectHospitalMembers.UserID + "'>" + objectHospitalMembers.UserName + "</a></span>");
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