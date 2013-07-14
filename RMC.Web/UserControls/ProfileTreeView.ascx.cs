using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogExceptions;

namespace RMC.Web.UserControls
{
    public partial class ProfileTreeView : System.Web.UI.UserControl
    {

        #region Variables

        //Bussiness Service objects.
        RMC.BussinessService.BSProfileType _objectBSProfileType = null;
        RMC.BussinessService.BSProfileType _objectBSDeleteProfileType = null;


        //Generic List Of Data Service objects.
        List<RMC.DataService.ProfileType> _objectGenericProfileType = null;
        List<RMC.DataService.ProfileUser> _objectGenericDeleteProfileType = null;

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (HttpContext.Current.User.IsInRole("superadmin"))
                    {
                        TreeViewProfile.Nodes[0].Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'>Value Added Category Profiles</td><td><span style='padding-left:5px; color:#06569d; font-weight:bold;'>( <a href='CreateNewProfile.aspx?valuetype=0' style='font-weight:bold;'>Create New Profile</a> )</span><span style='padding-left:5px; color:#06569d; font-weight:bold;'> (" + "<a href='ProfileType.aspx?valuetype=0' style='font-weight:bold;'>Add New Category</a> )</span></td></tr></table>";
                        TreeViewProfile.Nodes[1].Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'>Other Category Profiles</td><td><span style='padding-left:5px; color:#06569d; font-weight:bold;'>( <a href='CreateNewProfile.aspx?valuetype=1' style='font-weight:bold;'>Create New Profile</a> )</span><span style='padding-left:5px; color:#06569d; font-weight:bold;'> (" + "<a href='ProfileType.aspx?valuetype=1' style='font-weight:bold;'>Add New Category</a> )</span></td></tr></table>";
                        TreeViewProfile.Nodes[2].Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'>Location Category Profiles</td><td><span style='padding-left:5px; color:#06569d; font-weight:bold;'>( <a href='CreateNewProfile.aspx?valuetype=2' style='font-weight:bold;'>Create New Profile</a> )</span><span style='padding-left:5px; color:#06569d; font-weight:bold;'> (" + "<a href='ProfileType.aspx?valuetype=2' style='font-weight:bold;'>Add New Category</a> )</span></td></tr></table>";
                        TreeViewProfile.Nodes[3].Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'>Activities Category Profiles</td><td><span style='padding-left:5px; color:#06569d; font-weight:bold;'>( <a href='CreateNewProfile.aspx?valuetype=3' style='font-weight:bold;'>Create New Profile</a> )</span><span style='padding-left:5px; color:#06569d; font-weight:bold;'> (" + "<a href='ProfileType.aspx?valuetype=3' style='font-weight:bold;'>Add New Category</a> )</span></td></tr></table>";
                    }
                    else
                    {
                        TreeViewProfile.Nodes[0].Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'>Value Added Category Profiles</td><td><span style='padding-left:5px; color:#06569d; font-weight:bold;'>( <a href='CreateNewProfile.aspx?valuetype=0' style='font-weight:bold;'>Create New Profile</a> )</span></td></tr></table>";
                        TreeViewProfile.Nodes[1].Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'>Other Category Profiles</td><td><span style='padding-left:5px; color:#06569d; font-weight:bold;'>( <a href='CreateNewProfile.aspx?valuetype=1' style='font-weight:bold;'>Create New Profile</a> )</span></td></tr></table>";
                        TreeViewProfile.Nodes[2].Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'>Location Category Profiles</td><td><span style='padding-left:5px; color:#06569d; font-weight:bold;'>( <a href='CreateNewProfile.aspx?valuetype=2' style='font-weight:bold;'>Create New Profile</a> )</span></td></tr></table>";
                        TreeViewProfile.Nodes[2].Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'>Activities Category Profiles</td><td><span style='padding-left:5px; color:#06569d; font-weight:bold;'>( <a href='CreateNewProfile.aspx?valuetype=3' style='font-weight:bold;'>Create New Profile</a> )</span></td></tr></table>";
                    }
                    populateTreeviewParentNodes();
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "ProfileTreeView.ascx");
                LogManager._stringObject = "ProfileTreeView.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Populate Tree View by Profile Types.
        /// </summary>
        private void populateTreeviewParentNodes()
        {
            try
            {
                string navigationUrl = null;
                RMC.BussinessService.BSProfileUser objectBSProfileUser = new RMC.BussinessService.BSProfileUser();
                _objectBSProfileType = new RMC.BussinessService.BSProfileType();
                _objectBSDeleteProfileType = new RMC.BussinessService.BSProfileType();

                _objectGenericProfileType = _objectBSProfileType.GetProfileTypes(CommonClass.UserInformation.UserID);
                foreach (RMC.DataService.ProfileType objectProfileType in _objectGenericProfileType)
                {
                    if (objectProfileType.Type.ToLower().Trim() == "value added")
                    {
                        RMC.DataService.ProfileUser objectProfileUser = new RMC.DataService.ProfileUser();
                        TreeNode objectTreeNodeChild = new TreeNode();
                        objectTreeNodeChild.Value = objectProfileType.ProfileTypeID.ToString();
                        Session["ProfileTypeID"] = objectTreeNodeChild.Value;

                        objectProfileUser = objectBSProfileUser.GetProfileUser(objectProfileType.ProfileTypeID);

                        if (HttpContext.Current.User.IsInRole("superadmin"))
                        {
                            navigationUrl = "../Administrator/ViewProfileDetail.aspx?ProfileTypeID=" + objectProfileType.ProfileTypeID.ToString();
                        }
                        else
                        {
                            navigationUrl = "../Users/ViewProfileDetail.aspx?ProfileTypeID=" + objectProfileType.ProfileTypeID.ToString();
                        }

                        if (HttpContext.Current.User.IsInRole("superadmin"))
                        {
                            

                            if (objectProfileUser != null)
                            {
                                if (objectProfileUser.UserID == 1)
                                {
                                    objectTreeNodeChild.Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold;' href='" + navigationUrl + "'>" + objectProfileType.ProfileName + "</td><td><span style='padding-left:5px; font-weight:bold; color:#06569d;'>( <a style=\" color:Red; cursor:pointer; font-weight:bold;\" title='Delete this profile.' onclick=ConfirmMessageForAdministrator(" + objectTreeNodeChild.Value + ");>Delete Profile</a> )</span></td>";
                                }
                                else
                                {
                                    //objectTreeNodeChild.Text = objectProfileType.ProfileName + "<span style='padding-left:5px; font-weight:bold; color:#06569d;'>(" + "<a style=\" color:Red; cursor:pointer;\" title='Delete this profile.' onclick=ConfirmMessageForAdministrator(" + objectTreeNodeChild.Value + ");>Delete Profile</a>)</span>" +
                                    //                            " <span style='padding-left:5px; font-weight:bold;'>(<a style='cursor:pointer; color:#06569d;' onclick='mypopupAdministrator(" + objectProfileUser.UserID.ToString() + ")'>" + objectProfileType.AuthorName + "</a>)</span>";
                                    objectTreeNodeChild.Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold;' href='" + navigationUrl + "'>" + objectProfileType.ProfileName + "</td><td><span style='padding-left:5px; font-weight:bold; color:#06569d;'>( <a style=\" color:Red; cursor:pointer; font-weight:bold;\" title='Delete this profile.' onclick=ConfirmMessageForAdministrator(" + objectTreeNodeChild.Value + ");>Delete Profile</a> )</span></td>" +
                                                                  "<td><span style='padding-left:5px; font-weight:bold;'>( <a style='cursor:pointer; color:#06569d;' onclick='mypopupAdministrator(" + objectProfileUser.UserID.ToString() + ")'>" + objectProfileType.AuthorName + "</a> )</span></td>";//+ " <span style='padding-left:5px; font-weight:bold; color:#06569d;'>(<a style=' color:#06569d; cursor:pointer;' onclick='#' title='Edit'>Edit</a>)</span>";

                                }
                            }
                            else
                            {
                                objectTreeNodeChild.Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold;' href='" + navigationUrl + "'>" + objectProfileType.ProfileName + "</td><td><span style='padding-left:5px; font-weight:bold; color:#06569d;'>( <a style=\" color:Red; cursor:pointer; font-weight:bold;\" title='Delete this profile.' onclick=ConfirmMessageForAdministrator(" + objectTreeNodeChild.Value + ");>Delete Profile</a> )</span></td>";
                            }

                        }
                        else
                        {
                            _objectGenericDeleteProfileType = _objectBSProfileType.GetIDBy_ProfileTypeID_UserID(CommonClass.UserInformation.UserID, objectProfileType.ProfileTypeID);
                            if (_objectGenericDeleteProfileType.Count > 0)
                            {
                                //objectTreeNodeChild.Text = objectProfileType.ProfileName + "<span style='padding-left:5px; font-weight:bold; color:#06569d;'>(" + "<a style=\" color:Red; cursor:pointer;\" title='Delete this profile.' onclick=ConfirmMessage(" + objectTreeNodeChild.Value + ");>Delete Profile</a>)" ;
                                objectTreeNodeChild.Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold;' href='" + navigationUrl + "'>" + objectProfileType.ProfileName + "</td><td><span style='padding-left:5px; font-weight:bold; color:#06569d;'>( <a style=\" color:Red; cursor:pointer; font-weight:bold;\" title='Delete this profile.' onclick=ConfirmMessage(" + objectTreeNodeChild.Value + ");>Delete Profile</a> )</span></td>";// +" <span style='padding-left:5px; font-weight:bold; color:#06569d;'>(<a style=' color:#06569d; cursor:pointer;' title='Edit'>Edit</a>)</span>";
                            }
                            else
                            {
                                objectTreeNodeChild.Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold;' href='" + navigationUrl + "'>" + objectProfileType.ProfileName + "</td>";
                            }

                            if (objectProfileUser != null)
                            {
                                if (objectProfileUser.UserID != CommonClass.UserInformation.UserID)
                                {
                                    objectTreeNodeChild.Text += "<td><span style='padding-left:5px; font-weight:bold; color:#06569d;'>( <a style='cursor:pointer; color:#06569d; font-weight:bold;' onclick='mypopup(" + objectProfileUser.UserID.ToString() + ")'>" + objectProfileType.AuthorName + "</a> )</span></td>";
                                }
                            }
                        }

                        objectTreeNodeChild.Text += "</tr></table>";
                        TreeViewProfile.Nodes[0].ChildNodes.Add(objectTreeNodeChild);
                    }
                    else if (objectProfileType.Type.ToLower().Trim() == "others")
                    {
                        TreeNode objectTreeNodeChild = new TreeNode();
                        RMC.DataService.ProfileUser objectProfileUser = new RMC.DataService.ProfileUser();
                        objectTreeNodeChild.Value = objectProfileType.ProfileTypeID.ToString();
                        Session["ProfileTypeID"] = objectTreeNodeChild.Value;
                        objectProfileUser = objectBSProfileUser.GetProfileUser(objectProfileType.ProfileTypeID);

                        if (HttpContext.Current.User.IsInRole("superadmin"))
                        {
                            navigationUrl = "../Administrator/ViewProfileDetail.aspx?ProfileTypeID=" + objectProfileType.ProfileTypeID.ToString();
                        }
                        else
                        {
                            navigationUrl = "../Users/ViewProfileDetail.aspx?ProfileTypeID=" + objectProfileType.ProfileTypeID.ToString();
                        }

                        if (HttpContext.Current.User.IsInRole("superadmin"))
                        {
                            if (objectProfileUser != null)
                            {
                                if (objectProfileUser.UserID == 1)
                                {
                                    //objectTreeNodeChild.Text = objectProfileType.ProfileName + "<span style='padding-left:5px; font-weight:bold; color:#06569d;'>(" + "<a style=\" color:Red; cursor:pointer;\" title='Delete this profile.' onclick=ConfirmMessageForAdministrator(" + objectTreeNodeChild.Value + ");>Delete Profile</a>)</span>";
                                    objectTreeNodeChild.Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold;' href='" + navigationUrl + "'>" + objectProfileType.ProfileName + "</a></td><td><span style='padding-left:5px; color:#06569d; font-weight:bold;'>( <a style=\" color:Red; cursor:pointer; font-weight:bold;\" title='Delete this profile.' onclick=ConfirmMessageForAdministrator(" + objectTreeNodeChild.Value + ");>Delete Profile</a> )</span></td>";//+ " <span style='padding-left:5px; font-weight:bold; color:#06569d;'>(<a style=' color:#06569d; cursor:pointer;' title='Edit'>Edit</a>)</span>";
                                }
                                else
                                {
                                    //objectTreeNodeChild.Text = objectProfileType.ProfileName + "<span style='padding-left:5px; font-weight:bold; color:#06569d;'>(" + "<a style=\" color:Red; cursor:pointer;\" title='Delete this profile.' onclick=ConfirmMessageForAdministrator(" + objectTreeNodeChild.Value + ");>Delete Profile</a>)</span>" +
                                    //                            "<span style='padding-left:5px; font-weight:bold;'>(<a style='cursor:pointer; color:#06569d;' onclick='mypopupAdministrator(" + objectProfileUser.UserID.ToString() + ")'>" + objectProfileType.AuthorName + "</a>)</span>";
                                    objectTreeNodeChild.Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold;' href='" + navigationUrl + "'>" + objectProfileType.ProfileName + "</a></td><td><span style='padding-left:5px; color:#06569d; font-weight:bold;'>( <a style=\" color:Red; cursor:pointer; font-weight:bold;\" title='Delete this profile.' onclick=ConfirmMessageForAdministrator(" + objectTreeNodeChild.Value + ");>Delete Profile</a> )</span></td>" +
                                                                  "<td><span style='padding-left:5px; font-weight:bold;'>(<a style='cursor:pointer; color:#06569d;' onclick='mypopupAdministrator(" + objectProfileUser.UserID.ToString() + ")'>" + objectProfileType.AuthorName + "</a>)</span></td>"; //+ " <span style='padding-left:5px; font-weight:bold; color:#06569d;'>(<a style=' color:#06569d; cursor:pointer;' title='Edit'>Edit</a>)</span>";
                                }
                            }
                            else
                            {
                                objectTreeNodeChild.Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold;' href='" + navigationUrl + "'>" + objectProfileType.ProfileName + "</a></td><td><span style='padding-left:5px; font-weight:bold; color:#06569d;'>( <a style=\" color:Red; cursor:pointer; font-weight:bold;\" title='Delete this profile.' onclick=ConfirmMessageForAdministrator(" + objectTreeNodeChild.Value + ");>Delete Profile</a> )</span></td>";
                            }
                        }
                        else
                        {
                            _objectGenericDeleteProfileType = _objectBSProfileType.GetIDBy_ProfileTypeID_UserID(CommonClass.UserInformation.UserID, objectProfileType.ProfileTypeID);
                            if (_objectGenericDeleteProfileType.Count > 0)
                            {
                                //objectTreeNodeChild.Text = objectProfileType.ProfileName + "<span style='padding-left:5px; font-weight:bold; color:#06569d;'>(" + "<a style=\" color:Red; cursor:pointer;\" title='Delete this profile.' onclick=ConfirmMessage(" + objectTreeNodeChild.Value + ");>Delete Profile</a>)</span>";
                                objectTreeNodeChild.Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold;' href='" + navigationUrl + "'>" + objectProfileType.ProfileName + "</a></td><td><span style='padding-left:5px; font-weight:bold; color:#06569d;'>( <a style=\" color:Red; cursor:pointer; font-weight:bold;\" title='Delete this profile.' onclick=ConfirmMessage(" + objectTreeNodeChild.Value + ");>Delete Profile</a> )</span></td>";// +" <span style='padding-left:5px; font-weight:bold; color:#06569d;'>(<a style=' color:#06569d; cursor:pointer;' title='Edit'>Edit</a>)</span>";
                            }
                            else
                            {
                                objectTreeNodeChild.Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold;' href='" + navigationUrl + "'>" + objectProfileType.ProfileName + "</a></td>";
                            }
                        }
                        if (objectProfileUser != null)
                        {
                            if (objectProfileUser.UserID != CommonClass.UserInformation.UserID)
                            {
                                objectTreeNodeChild.Text += "<td><span style='padding-left:5px; font-weight:bold; color:#06569d;'>( <a style='cursor:pointer; color:#06569d; font-weight:bold;' onclick='mypopup(" + objectProfileUser.UserID.ToString() + ")'>" + objectProfileType.AuthorName + "</a> )</span></td>";
                            }
                        }
                        
                        objectTreeNodeChild.Text += "</tr></table>";
                        TreeViewProfile.Nodes[1].ChildNodes.Add(objectTreeNodeChild);
                    }
                    else if (objectProfileType.Type.ToLower().Trim() == "activities")
                    {
                        TreeNode objectTreeNodeChild = new TreeNode();
                        RMC.DataService.ProfileUser objectProfileUser = new RMC.DataService.ProfileUser();
                        objectTreeNodeChild.Value = objectProfileType.ProfileTypeID.ToString();
                        Session["ProfileTypeID"] = objectTreeNodeChild.Value;
                        objectProfileUser = objectBSProfileUser.GetProfileUser(objectProfileType.ProfileTypeID);

                        if (HttpContext.Current.User.IsInRole("superadmin"))
                        {
                            navigationUrl = "../Administrator/ViewProfileDetail.aspx?ProfileTypeID=" + objectProfileType.ProfileTypeID.ToString();
                        }
                        else
                        {
                            navigationUrl = "../Users/ViewProfileDetail.aspx?ProfileTypeID=" + objectProfileType.ProfileTypeID.ToString();
                        }

                        if (HttpContext.Current.User.IsInRole("superadmin"))
                        {
                            if (objectProfileUser != null)
                            {
                                if (objectProfileUser.UserID == 1)
                                {
                                    //objectTreeNodeChild.Text = objectProfileType.ProfileName + "<span style='padding-left:5px; font-weight:bold; color:#06569d;'>(" + "<a style=\" color:Red; cursor:pointer;\" title='Delete this profile.' onclick=ConfirmMessageForAdministrator(" + objectTreeNodeChild.Value + ");>Delete Profile</a>)</span>";
                                    objectTreeNodeChild.Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold;' href='" + navigationUrl + "'>" + objectProfileType.ProfileName + "</a></td><td><span style='padding-left:5px; color:#06569d; font-weight:bold;'>( <a style=\" color:Red; cursor:pointer; font-weight:bold;\" title='Delete this profile.' onclick=ConfirmMessageForAdministrator(" + objectTreeNodeChild.Value + ");>Delete Profile</a> )</span></td>";//+ " <span style='padding-left:5px; font-weight:bold; color:#06569d;'>(<a style=' color:#06569d; cursor:pointer;' title='Edit'>Edit</a>)</span>";
                                }
                                else
                                {
                                    //objectTreeNodeChild.Text = objectProfileType.ProfileName + "<span style='padding-left:5px; font-weight:bold; color:#06569d;'>(" + "<a style=\" color:Red; cursor:pointer;\" title='Delete this profile.' onclick=ConfirmMessageForAdministrator(" + objectTreeNodeChild.Value + ");>Delete Profile</a>)</span>" +
                                    //                            "<span style='padding-left:5px; font-weight:bold;'>(<a style='cursor:pointer; color:#06569d;' onclick='mypopupAdministrator(" + objectProfileUser.UserID.ToString() + ")'>" + objectProfileType.AuthorName + "</a>)</span>";
                                    objectTreeNodeChild.Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold;' href='" + navigationUrl + "'>" + objectProfileType.ProfileName + "</a></td><td><span style='padding-left:5px; color:#06569d; font-weight:bold;'>( <a style=\" color:Red; cursor:pointer; font-weight:bold;\" title='Delete this profile.' onclick=ConfirmMessageForAdministrator(" + objectTreeNodeChild.Value + ");>Delete Profile</a> )</span></td>" +
                                                                  "<td><span style='padding-left:5px; font-weight:bold;'>(<a style='cursor:pointer; color:#06569d;' onclick='mypopupAdministrator(" + objectProfileUser.UserID.ToString() + ")'>" + objectProfileType.AuthorName + "</a>)</span></td>"; //+ " <span style='padding-left:5px; font-weight:bold; color:#06569d;'>(<a style=' color:#06569d; cursor:pointer;' title='Edit'>Edit</a>)</span>";
                                }
                            }
                            else
                            {
                                objectTreeNodeChild.Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold;' href='" + navigationUrl + "'>" + objectProfileType.ProfileName + "</a></td><td><span style='padding-left:5px; font-weight:bold; color:#06569d;'>( <a style=\" color:Red; cursor:pointer; font-weight:bold;\" title='Delete this profile.' onclick=ConfirmMessageForAdministrator(" + objectTreeNodeChild.Value + ");>Delete Profile</a> )</span></td>";
                            }
                        }
                        else
                        {
                            _objectGenericDeleteProfileType = _objectBSProfileType.GetIDBy_ProfileTypeID_UserID(CommonClass.UserInformation.UserID, objectProfileType.ProfileTypeID);
                            if (_objectGenericDeleteProfileType.Count > 0)
                            {
                                //objectTreeNodeChild.Text = objectProfileType.ProfileName + "<span style='padding-left:5px; font-weight:bold; color:#06569d;'>(" + "<a style=\" color:Red; cursor:pointer;\" title='Delete this profile.' onclick=ConfirmMessage(" + objectTreeNodeChild.Value + ");>Delete Profile</a>)</span>";
                                objectTreeNodeChild.Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold;' href='" + navigationUrl + "'>" + objectProfileType.ProfileName + "</a></td><td><span style='padding-left:5px; font-weight:bold; color:#06569d;'>( <a style=\" color:Red; cursor:pointer; font-weight:bold;\" title='Delete this profile.' onclick=ConfirmMessage(" + objectTreeNodeChild.Value + ");>Delete Profile</a> )</span></td>";// +" <span style='padding-left:5px; font-weight:bold; color:#06569d;'>(<a style=' color:#06569d; cursor:pointer;' title='Edit'>Edit</a>)</span>";
                            }
                            else
                            {
                                objectTreeNodeChild.Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold;' href='" + navigationUrl + "'>" + objectProfileType.ProfileName + "</a></td>";
                            }
                        }
                        if (objectProfileUser != null)
                        {
                            if (objectProfileUser.UserID != CommonClass.UserInformation.UserID)
                            {
                                objectTreeNodeChild.Text += "<td><span style='padding-left:5px; font-weight:bold; color:#06569d;'>( <a style='cursor:pointer; color:#06569d; font-weight:bold;' onclick='mypopup(" + objectProfileUser.UserID.ToString() + ")'>" + objectProfileType.AuthorName + "</a> )</span></td>";
                            }
                        }

                        objectTreeNodeChild.Text += "</tr></table>";
                        TreeViewProfile.Nodes[3].ChildNodes.Add(objectTreeNodeChild);
                    }
                    else
                    {
                        TreeNode objectTreeNodeChild = new TreeNode();
                        RMC.DataService.ProfileUser objectProfileUser = new RMC.DataService.ProfileUser();
                        objectTreeNodeChild.Value = objectProfileType.ProfileTypeID.ToString();
                        Session["ProfileTypeID"] = objectTreeNodeChild.Value;
                        objectProfileUser = objectBSProfileUser.GetProfileUser(objectProfileType.ProfileTypeID);

                        if (HttpContext.Current.User.IsInRole("superadmin"))
                        {
                            navigationUrl = "../Administrator/ViewProfileDetail.aspx?ProfileTypeID=" + objectProfileType.ProfileTypeID.ToString();
                        }
                        else
                        {
                            navigationUrl = "../Users/ViewProfileDetail.aspx?ProfileTypeID=" + objectProfileType.ProfileTypeID.ToString();
                        }

                        if (HttpContext.Current.User.IsInRole("superadmin"))
                        {
                            if (objectProfileUser != null)
                            {
                                if (objectProfileUser.UserID == 1)
                                {
                                    //objectTreeNodeChild.Text = objectProfileType.ProfileName + "<span style='padding-left:5px; font-weight:bold; color:#06569d;'>(" + "<a style=\" color:Red; cursor:pointer;\" title='Delete this profile.' onclick=ConfirmMessageForAdministrator(" + objectTreeNodeChild.Value + ");>Delete Profile</a>)</span>";
                                    objectTreeNodeChild.Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold;' href='" + navigationUrl + "'>" + objectProfileType.ProfileName + "</a></td><td><span style='padding-left:5px; font-weight:bold; color:#06569d;'>( <a style=\" color:Red; cursor:pointer; font-weight:bold;\" title='Delete this profile.' onclick=ConfirmMessageForAdministrator(" + objectTreeNodeChild.Value + ");>Delete Profile</a> )</span></td>";// +" <span style='padding-left:5px; font-weight:bold; color:#06569d;'>(<a style=' color:#06569d; cursor:pointer;' title='Edit'>Edit</a>)</span>";
                                }
                                else
                                {
                                    //objectTreeNodeChild.Text = objectProfileType.ProfileName + "<span style='padding-left:5px; font-weight:bold; color:#06569d;'>(" + "<a style=\" color:Red; cursor:pointer;\" title='Delete this profile.' onclick=ConfirmMessageForAdministrator(" + objectTreeNodeChild.Value + ");>Delete Profile</a>)</span>" +
                                    //                            "<span style='padding-left:5px; font-weight:bold;'>(<a style='cursor:pointer; color:#06569d;' onclick='mypopupAdministrator(" + objectProfileUser.UserID.ToString() + ")'>" + objectProfileType.AuthorName + "</a>)</span>";
                                    objectTreeNodeChild.Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold;' href='" + navigationUrl + "'>" + objectProfileType.ProfileName + "</a></td><td><span style='padding-left:5px; font-weight:bold; color:#06569d;'>( <a style=\" color:Red; cursor:pointer; font-weight:bold;\" title='Delete this profile.' onclick=ConfirmMessageForAdministrator(" + objectTreeNodeChild.Value + ");>Delete Profile</a> )</span></td>" +
                                                                  "<td><span style='padding-left:5px; font-weight:bold;'>( <a style='cursor:pointer; color:#06569d;' onclick='mypopupAdministrator(" + objectProfileUser.UserID.ToString() + ")'>" + objectProfileType.AuthorName + "</a> )</span></td>";//+ " <span style='padding-left:5px; font-weight:bold; color:#06569d;'>(<a style=' color:#06569d; cursor:pointer;' title='Edit'>Edit</a>)</span>";
                                }
                            }
                            else
                            {
                                objectTreeNodeChild.Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold;' href='" + navigationUrl + "'>" + objectProfileType.ProfileName + "</a></td><td><span style='padding-left:5px; font-weight:bold; color:#06569d;'>( <a style=\" color:Red; cursor:pointer; font-weight:bold;\" title='Delete this profile.' onclick=ConfirmMessageForAdministrator(" + objectTreeNodeChild.Value + ");>Delete Profile</a> )</span></td>";
                            }
                        }
                        else
                        {
                            _objectGenericDeleteProfileType = _objectBSProfileType.GetIDBy_ProfileTypeID_UserID(CommonClass.UserInformation.UserID, objectProfileType.ProfileTypeID);
                            if (_objectGenericDeleteProfileType.Count > 0)
                            {
                                //objectTreeNodeChild.Text = objectProfileType.ProfileName + "<span style='padding-left:5px; font-weight:bold; color:#06569d;'>(" + "<a style=\" color:Red; cursor:pointer;\" title='Delete this profile.' onclick=ConfirmMessage(" + objectTreeNodeChild.Value + ");>Delete Profile</a>)</span>";
                                objectTreeNodeChild.Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold;' href='" + navigationUrl + "'>" + objectProfileType.ProfileName + "</a></td><td><span style='padding-left:5px; font-weight:bold; color:#06569d;'>( <a style=\" color:Red; cursor:pointer; font-weight:bold;\" title='Delete this profile.' onclick=ConfirmMessage(" + objectTreeNodeChild.Value + ");>Delete Profile</a> )</span></td>";// +" <span style='padding-left:5px; font-weight:bold; color:#06569d;'>(<a style=' color:#06569d; cursor:pointer;' title='Edit'>Edit</a>)</span>";
                            }
                            else
                            {
                                objectTreeNodeChild.Text = "<table width='525px' style='text-decoration:none;'><tr><td style='width:400px; font-weight:bold; color:#06569d;'><a style='font-weight:bold;' href='" + navigationUrl + "'>" + objectProfileType.ProfileName + "</a></td>";
                            }
                        }
                        if (objectProfileUser != null)
                        {
                            if (objectProfileUser.UserID != CommonClass.UserInformation.UserID)
                            {
                                objectTreeNodeChild.Text += "<td><span style='padding-left:5px; font-weight:bold; color:#06569d;'>( <a style='cursor:pointer; color:#06569d; font-weight:bold;' onclick='mypopup(" + objectProfileUser.UserID.ToString() + ")'>" + objectProfileType.AuthorName + "</a> )</span></td>";
                            }
                        }

                        objectTreeNodeChild.Text += "</tr></table>";
                        TreeViewProfile.Nodes[2].ChildNodes.Add(objectTreeNodeChild);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Methods", "populateTreeviewParentNodes");
                ex.Data.Add("Page", "ProfileTreeView.ascx");
                LogManager._stringObject = "ProfileTreeView.ascx ---- populateTreeviewParentNodes";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
            }
        }

        #endregion

    }
}