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

namespace RMC.Web.Users
{
    public partial class ExcelUploaderResults : System.Web.UI.Page
    {

        #region Variables

        //Business Service Objects.
        RMC.BussinessService.BSUpload _objectBSUpload = null;
        RMC.BussinessService.BSDataValidation _objectBSDataValidation = null;

        //Generic Data Service Objects.
        List<RMC.DataService.HospitalUpload> _objectGenericHospitalUpload = null;

        //Bussiness Entity Objects.
        RMC.BusinessEntities.BESessionInfomation _objectBESessionInfomation = null;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bool flag = false, errorFlag = false, setErrorFlag = false;
                _objectBSDataValidation = new RMC.BussinessService.BSDataValidation();
                _objectBSUpload = new RMC.BussinessService.BSUpload();

                _objectGenericHospitalUpload = _objectBSUpload.GetLatestUploadFile(Convert.ToInt32(Session["DemographicID"]), Convert.ToDateTime(Session["Date"]));
                foreach (RMC.DataService.HospitalUpload objectHospitalUpload in _objectGenericHospitalUpload)
                {
                    flag = _objectBSDataValidation.SaveFileData(objectHospitalUpload.FilePath + objectHospitalUpload.UploadedFileName, objectHospitalUpload.OriginalFileName, Convert.ToInt32(Session["DemographicID"]), out errorFlag);

                    if (flag)
                    {
                        _objectBSUpload.UpdateFileInformation(objectHospitalUpload);
                    }

                    if (errorFlag)
                    {
                        _objectBESessionInfomation = CommonClass.SessionInfomation;

                        if (_objectBESessionInfomation == null)
                        {
                            _objectBESessionInfomation = new RMC.BusinessEntities.BESessionInfomation();
                            _objectBESessionInfomation.HospitalUploadIDs = new List<int>();
                        }

                        _objectBESessionInfomation.HospitalUploadIDs.Add(objectHospitalUpload.HospitalUploadID);
                        CommonClass.SessionInfomation = _objectBESessionInfomation;
                        setErrorFlag = true;
                    }
                }
                if (!setErrorFlag)
                {
                    BindUnSuccessFile();
                    BindSuccessFile();
                }
            }
        }

        /// <summary>
        /// <Description>Bind ListBox for successfully uploaded files</Description>
        /// </summary>
        /// <Author>Amit Chawla</Author>
        /// <Created On>10-July-2009</Created>
        /// <ModifiedBy></ModifiedBy>
        /// <ModifiedOn></ModifiedOn> 
        /// <Version No=1>Description of change</Version>
        /// <param name="context"></param>
        private void BindUnSuccessFile()
        {
            try
            {
                string[] SessionUnSuccess;
                string SessionValue = "";
                if (Session["UnSuccess"] != null)
                {
                    SessionValue = Session["UnSuccess"].ToString();
                    SessionUnSuccess = SessionValue.Split('\n');
                    ListBoxUnsuccess.DataSource = SessionUnSuccess;
                    ListBoxUnsuccess.DataBind();
                }
                else
                {
                    trUnsuccessfully.Visible = false;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Session["UnSuccess"] = null;
            }
        }

        /// <summary>
        /// <Description>Bind ListBox for Successfully uploaded files</Description>
        /// </summary>
        /// <Author>Amit Chawla</Author>
        /// <Created On>10-July-2009</Created>
        /// <ModifiedBy></ModifiedBy>
        /// <ModifiedOn></ModifiedOn> 
        /// <Version No=1>Description of change</Version>
        /// <param name="context"></param>
        private void BindSuccessFile()
        {
            try
            {
                string[] SessionSuccess;
                string SessionValue = "";
                if (Session["Success"] != null)
                {
                    SessionValue = Session["Success"].ToString();
                    SessionSuccess = SessionValue.Split('\n');
                    ListBoxSuccess.DataSource = SessionSuccess;
                    ListBoxSuccess.DataBind();
                }
                else
                {
                    trSuccessfully.Visible = false;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Session["Success"] = null;
            }
        }

        protected void ButtonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExcelUploader.aspx");
        }

    }
}
