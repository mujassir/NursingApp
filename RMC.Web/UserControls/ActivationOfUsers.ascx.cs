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
using System.Collections.Generic;

namespace RMC.Web.UserControls
{
    public partial class ActivationOfUsers : System.Web.UI.UserControl
    {

        #region Variables

        RMC.BussinessService.BSUsers objectBSUserInfo = null;
        RMC.DataService.UserInfo objectUserInfo = null;
        List<RMC.DataService.UserInfo> objectUserInfoList = null;

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ImageButtonView_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow grdRow = (GridViewRow)((ImageButton)sender).NamingContainer;
                Response.Redirect("ActivateUser.aspx?UserId=" + GridViewActivationOfUsers.DataKeys[grdRow.RowIndex].Value.ToString(), false);
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ImageButtonView_Click");
                LogManager._stringObject = "ActivationOfUsers.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void ButtonActivate_Click(object sender, EventArgs e)
        {
            bool flag = false;
            bool IsActive = false;
            string UserRequest = "";
            try
            {
                objectUserInfoList = new List<RMC.DataService.UserInfo>();
                foreach (GridViewRow grdRow in GridViewActivationOfUsers.Rows)
                {
                    objectBSUserInfo = new RMC.BussinessService.BSUsers();
                    objectUserInfo = new RMC.DataService.UserInfo();
                    RadioButton rdButtonActive = (RadioButton)grdRow.FindControl("RadioButtonActive");
                    RadioButton rdButtonNotActive = (RadioButton)grdRow.FindControl("RadioButtonNotActive");
                    Label AccessReq = (Label)grdRow.FindControl("LabelAccessRequest");
                    Label toUserID = (Label)grdRow.FindControl("LabelUserID");

                    if (rdButtonActive.Checked || rdButtonNotActive.Checked)
                    {
                        int useridID = Convert.ToInt32(GridViewActivationOfUsers.DataKeys[grdRow.RowIndex].Value);
                        //objectUserInfo.IsActive = rdButtonActive.Checked;
                        // Added by Raman 
                        // This is used to store UserId and passed to GetUserEmailByUserID() function
                        ViewState["UserID"] = Convert.ToString(useridID);
                        if (rdButtonActive.Checked == true)
                        {
                            objectUserInfo.IsActive = true;
                            IsActive = objectUserInfo.IsActive;

                            // Added by Raman
                            // Added for Email Functionality, 21/12/2010 
                            // This code is added to send an email 

                            bool _emailFlag;
                            string _bodyText, _fromAddress, _toAddress, _subjectText;

                            RMC.BussinessService.BSCommon _objectBSCommon = new RMC.BussinessService.BSCommon();
                            RMC.BussinessService.BSUsers _objectBSUser = new RMC.BussinessService.BSUsers();
                            try
                            {
                                string email = string.Empty;
                                RMC.BussinessService.BSEmail _objectBSEmail;
                                _toAddress = _objectBSUser.GetUserEmailByUserID(Convert.ToInt32(ViewState["UserID"]));
                                if (AccessReq.Text == "Owner")
                                {
                                    _bodyText = "Your request to create a hospital unit record and reporting access in the Time Study RN National Benchmarking Database has been approved.  You can now login to the database using your email address and password.<br><br> When you login to the database the first time, you will need to create your hospital record and also your unit record.  To do this, you will select “Data Management” and then “Add Hospital”.  Please be sure to complete all fields before leaving the form.<br><br> Once the hospital is created, click on data management again and select the new hospital from the list.  You will see a new link to “Add Unit”.  This window will allow you to add your hospital unit to the database.  Please be sure to complete all fields before leaving the form. <br><br>Once you have created the unit record, then you can begin to upload data.  The data is organized in an indented index under the data management tab.  The index hierarchy is hospital, unit, year, month, then the data.  To add data, first you will navigate to the hospital and unit and year in the index, then you will “Add Month” and then select the “Add Data” option.  The “Add Data” form is relatively self explanatory, but if you feel that you need help using the form then please contact our tech support hot line at (513) 624-6629.<br><br>If you have any questions, then please do not hesitate to contact the system administrator at (513) 624-6629 or by email at nlee@rapidmodeling.com.<br><br> Thanks for participating in the Time Study RN National Benchmarking Database.<br><br>Sincerely,<br>System Administrator<br>Time Study RN National Benchmarking Database Project";
                                }
                                else
                                {
                                    _bodyText = "Your request for hospital unit data viewing and reporting access in the Time Study RN National Benchmarking Database has been approved.  You can now login to the database using your email address and password.<br><br>  When you login to the database the first time, you need to select “Req. Hospital Unit Access”.  This window will allow you to request access to a specific hospital unit dataset and this request must be approved by the Owner of record.  Once the approval is given, then you will be given access to the data for that unit and you will be able to generate and view reports for that unit.<br><br> If you have any questions, then please do not hesitate to contact the system administrator at (513) 624-6629 or by email at nlee@rapidmodeling.com. <br><br> Thanks for participating in the Time Study RN National Benchmarking Database.<br><br> Sincerely,<br> System Administrator<br>Time Study RN National Benchmarking Database Project";
                                }
                                _subjectText = "Time Study RN National Benchmarking Database Approval Status";
                                _fromAddress = ConfigurationManager.AppSettings["superAdminAddress"].ToString();
                                _objectBSEmail = new RMC.BussinessService.BSEmail(_fromAddress, _toAddress, _subjectText, _bodyText, true);
                                
                                _objectBSEmail.SendMail(true, out _emailFlag);
                                
                            }
                            catch (Exception ex)
                            {
                                LogManager._stringObject = "SendMessage.ascx.cs ---- ";
                                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                                //CommonClass.Show("Not able to send notification mail, please ensure that user mail id is correct");
                                //CommonClass.Show(LogManager.ShowErrorDetail(ex));
                            }
                            // Added for Email Functionality, 21/12/2010



                        }

                        if (rdButtonNotActive.Checked == true)
                        {
                            objectUserInfo.UserActivationRequest = "";
                        }
                        else
                        {
                            objectUserInfo.UserActivationRequest = "Activation Request";
                        }

                        UserRequest = objectUserInfo.UserActivationRequest;
                        objectUserInfo.UserID = useridID;
                        objectUserInfoList.Add(objectUserInfo);

                    }
                }
                flag = objectBSUserInfo.UpdateUserActivation(objectUserInfoList, IsActive, UserRequest);
                
                GridViewActivationOfUsers.DataBind();
                
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ButtonApproved_Click");
                ex.Data.Add("Page", "RequestApproval.ascx");
                LogManager._stringObject = "RequestApproval.ascx ---- ButtonApproved_Click";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
            }


        }

        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
        {
            bool flag = false;
            bool IsActive = false;
            string UserRequest = "";
            try
            {
                objectBSUserInfo = new RMC.BussinessService.BSUsers();
                GridViewRow grdRow = (GridViewRow)((ImageButton)sender).NamingContainer;
                flag = objectBSUserInfo.DeleteUserInformation(Convert.ToInt32(GridViewActivationOfUsers.DataKeys[grdRow.RowIndex].Value), "true");
                if (flag)
                {
                    GridViewActivationOfUsers.DataBind();
                }
                RadioButton rdButtonActive = (RadioButton)grdRow.FindControl("RadioButtonActive");
                RadioButton rdButtonNotActive = (RadioButton)grdRow.FindControl("RadioButtonNotActive");
                if (rdButtonActive.Checked || rdButtonNotActive.Checked)
                {
                    if (rdButtonActive.Checked == true)
                    {
                        objectUserInfo.IsActive = true;
                        IsActive = objectUserInfo.IsActive;
                    }
                    if (rdButtonNotActive.Checked == true)
                    {
                        objectUserInfo.UserActivationRequest = "";
                    }
                    else
                    {
                        objectUserInfo.UserActivationRequest = "Activation Request";
                    }
                    int useridID = Convert.ToInt32(GridViewActivationOfUsers.DataKeys[grdRow.RowIndex].Value);
                    UserRequest = objectUserInfo.UserActivationRequest;
                    objectUserInfo.UserID = useridID;
                    objectUserInfoList.Add(objectUserInfo);
                }
                flag = objectBSUserInfo.UpdateUserActivation(objectUserInfoList, IsActive, UserRequest);
                GridViewActivationOfUsers.DataBind();

            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "ImageButtonDelete_Click");
                LogManager._stringObject = "ActivationOfUsers.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        /// <summary>
        /// Page PreRender.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (GridViewActivationOfUsers.Rows.Count == 0)
                {
                    ButtonActivate.Visible = false;
                }
                else
                {
                    ButtonActivate.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "Page_Load");
                ex.Data.Add("Page", "RequestApproval.ascx");
                LogManager._stringObject = "RequestApproval.ascx ---- Page_Load";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        protected void LinkButtonSave_Click(object sender, EventArgs e)
        {
            bool flag = false;
            bool IsActive = false;
            string UserRequest = "";
            try
            {
                objectBSUserInfo = new RMC.BussinessService.BSUsers();
                objectUserInfoList = new List<RMC.DataService.UserInfo>();
                GridViewRow grdRow = (GridViewRow)((LinkButton)sender).NamingContainer;
                //flag = objectBSUserInfo.DeleteUserInformation(Convert.ToInt32(GridViewActivationOfUsers.DataKeys[grdRow.RowIndex].Value), "true");
                //if (flag)
                //{
                //    GridViewActivationOfUsers.DataBind();
                //}
                objectUserInfo = new RMC.DataService.UserInfo();
                RadioButton rdButtonActive = (RadioButton)grdRow.FindControl("RadioButtonActive");
                RadioButton rdButtonNotActive = (RadioButton)grdRow.FindControl("RadioButtonNotActive");
                if (rdButtonActive.Checked || rdButtonNotActive.Checked)
                {
                    if (rdButtonActive.Checked == true)
                    {
                        objectUserInfo.IsActive = true;
                        IsActive = objectUserInfo.IsActive;
                    }
                    if (rdButtonNotActive.Checked == true)
                    {
                        objectUserInfo.UserActivationRequest = "";
                    }
                    else
                    {
                        objectUserInfo.UserActivationRequest = "Activation Request";
                    }
                    int useridID = Convert.ToInt32(GridViewActivationOfUsers.DataKeys[grdRow.RowIndex].Value);
                    UserRequest = objectUserInfo.UserActivationRequest;
                    objectUserInfo.UserID = useridID;
                    objectUserInfoList.Add(objectUserInfo);
                }
                flag = objectBSUserInfo.UpdateUserActivation(objectUserInfoList, IsActive, UserRequest);
                GridViewActivationOfUsers.DataBind();

            }
            catch (Exception ex)
            {
                ex.Data.Add("Events", "LinkButtonSave_Click");
                LogManager._stringObject = "ActivationOfUsers.ascx.cs ---- ";
                LogManager.SetExceptionDetails(ex, LogManager._stringObject, null);
                LogManager.LogException(ex, LogManager.LoggingCategory.General, LogManager.LoggingLevel.Error);
                CommonClass.Show(LogManager.ShowErrorDetail(ex));
            }
        }

        #endregion
    }
}