using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMC.BussinessService
{
    public class BSEmailNotificationBody
    {

        #region Public Methods

        /// <summary>
        /// Create Email Body for Hospital Registration.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 22, 2009.
        /// </summary>
        /// <param name="hospitalInfo"></param>
        /// <returns></returns>
        public StringBuilder GetEmailBodyOfHospitalRegistration(RMC.DataService.HospitalInfo hospitalInfo)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                //start html Tag.
                builder.Append("<html><head><title>Hospital Registration</title></head><body><table width=\"370px\">");
                //start row tag of table.
                builder.Append("<tr><td align=\"center\" style=\"color: #06569d; font-weight: bold;\" colspan=\"2\">The New Hospital Register by following User.</td></tr>");
                builder.Append("<tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\">User Name &nbsp;&nbsp;</td><td align=\"left\">");
                //Show User Name.
                builder.Append(hospitalInfo.UserInfo.FirstName + " " + hospitalInfo.UserInfo.LastName);
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\">Hospital Name &nbsp;&nbsp;</td><td align=\"left\">");
                //Show Hospital Name.
                builder.Append(hospitalInfo.HospitalName);
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\">C.N.O. First Name &nbsp;&nbsp;</td><td align=\"left\">");
                //Show C.N.O. First Name.
                builder.Append(hospitalInfo.ChiefNursingOfficerFirstName);
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\">C.N.O. Last Name &nbsp;&nbsp;</td><td align=\"left\">");
                //Show C.N.O. Last Name.
                builder.Append(hospitalInfo.ChiefNursingOfficerLastName);
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\">C.N.O. Phone &nbsp;&nbsp;</td><td align=\"left\">");
                //Show C.N.O Phone.
                builder.Append(hospitalInfo.ChiefNursingOfficerPhone);
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\">Address&nbsp;&nbsp;</td><td align=\"left\">");
                //Show Address.
                builder.Append(hospitalInfo.Address);
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\">City&nbsp;&nbsp;</td><td align=\"left\">");
                //Show City.
                builder.Append(hospitalInfo.City);
                if (hospitalInfo.StateID != null && hospitalInfo.StateID != 0)
                {
                    builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\">Country&nbsp;&nbsp;</td><td align=\"left\">");
                    //Show Country.
                    builder.Append(hospitalInfo.State.Country.CountryName);
                    builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\">State&nbsp;&nbsp;</td><td align=\"left\">");
                    //Show State.
                    builder.Append(hospitalInfo.State.StateName);
                }
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\">Zip&nbsp;&nbsp;</td><td align=\"left\">");
                //Show Zip Code.
                builder.Append(hospitalInfo.Zip);
                builder.Append("</td></tr></table></body></html>");

                return builder;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetEmailBodyOfHospitalInfo");
                ex.Data.Add("Class", "BSEmailNotificationBody");
                throw ex;
            }
        }

        /// <summary>
        /// Create Email Body for Hospital Demographic Detail.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 23, 2009.
        /// </summary>
        /// <param name="hospitalDemographicInfo"></param>
        /// <returns></returns>
        public StringBuilder GetEmailBodyOfHospitalDemographicDetail(RMC.DataService.HospitalDemographicInfo hospitalDemographicInfo)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                //Start Html Tag.
                builder.Append("<html><head><title>Demographic Detail</title></head><body><table>");
                builder.Append("<tr><td align=\"center\" style=\"color: #06569d; font-weight: bold;\" colspan=\"6\">New Demographic Detail Enter by &nbsp");
                builder.Append(hospitalDemographicInfo.HospitalInfo.UserInfo.FirstName + " " + hospitalDemographicInfo.HospitalInfo.UserInfo.LastName);
                //Show Hospital Name.
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\">Hospital Name &nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(hospitalDemographicInfo.HospitalInfo.HospitalName);
                //Show Unit Name.
                builder.Append("</td><td align=\"left\">&nbsp;</td><td align=\"right\"></td><td align=\"right\" style=\"color: #06569d; font-weight: bold;\">Unit Name &nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(hospitalDemographicInfo.HospitalUnitName);
                //Show TCABUnit.
                builder.Append("</td></tr><tr><td>TCAB Unit</td><td align=\"left\" colspan=\"5\" style=\"color: #06569d; font-weight: bold;\">");
                if (hospitalDemographicInfo.TCABUnit)
                {
                    builder.Append("Checked");
                }
                else
                {
                    builder.Append("Unchecked");
                }
                builder.Append("</td></tr><tr><td colspan=\"6\"><div style=\"width: 100%; padding-top: 5px; padding-bottom: 5px;\">");
                //Show Unit Type.
                builder.Append("<hr style=\"height: 1px; color: #d6d6d6;\" /></div></td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\">Unit Type &nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(hospitalDemographicInfo.UnitType);
                //Show Demographic.
                builder.Append("</td><td align=\"right\"></td><td align=\"left\"></td><td align=\"right\" style=\"color: #06569d; font-weight: bold;\">Demographic &nbsp;&nbsp;</td><td align=\"left\">");
                //builder.Append(hospitalDemographicInfo.Demographic);
                //Show Beds In Unit
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\">Beds In Unit &nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(hospitalDemographicInfo.BedsInUnit);
                //Show Beds In Hospital
                builder.Append("</td><td align=\"right\"></td><td align=\"left\"></td><td align=\"right\" style=\"color: #06569d; font-weight: bold;\">Beds In Hospital &nbsp;&nbsp;</td><td align=\"left\">");
                //builder.Append(hospitalDemographicInfo.BedsInHospital);
                //Show BudgetedPatientsPerNurse
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\">Budgeted Patients Per Nurse &nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(hospitalDemographicInfo.BudgetedPatientsPerNurse);
                //Show Pharmacy Type.
                builder.Append("</td><td align=\"right\"></td><td align=\"left\"></td><td align=\"right\" style=\"color: #06569d; font-weight: bold;\">Pharmacy Type &nbsp;&nbsp;</td><td align=\"left\" rowspan=\"2\">");
                builder.Append(hospitalDemographicInfo.PharmacyType);
                //Electronic Documentation.
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\">Electronic Documentation(%) &nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(hospitalDemographicInfo.ElectronicDocumentation);
                //Show Start Date.
                builder.Append("</td><td align=\"right\"></td><td align=\"left\"></td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\">Start Date &nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(hospitalDemographicInfo.CreatedDate);
                //Show End Date.
                builder.Append("</td><td align=\"right\"></td><td align=\"left\"></td><td align=\"right\" style=\"color: #06569d; font-weight: bold;\">End Date &nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(hospitalDemographicInfo.EndedDate);
                //Show Doc By Exception.
                builder.Append("</td></tr><tr><td align=\"right\">Doc By Exception</td><td align=\"left\" style=\"color: #06569d; font-weight: bold;\">");
                if (hospitalDemographicInfo.DocByException)
                {
                    builder.Append("Checked");
                }
                else
                {
                    builder.Append("Unchecked");
                }
                builder.Append("</td><td align=\"right\"></td><td align=\"left\"></td><td align=\"right\"></td><td align=\"left\"></td></tr><tr><td style=\"padding-top: 5px;\"></td>");
                builder.Append("<td align=\"left\" colspan=\"5\" style=\"padding-top: 5px;\"></td></tr></table></body></html>");

                return builder;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetEmailBodyOfHospitalDemographicDetail");
                ex.Data.Add("Class", "BSEmailNotificationBody");
                throw ex;
            }
        }

        /// <summary>
        /// Create Email Body for User Registration.
        /// Created By : Davinder Kumar.
        /// Creation Date : July 23, 2009.
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public StringBuilder GetEmailBodyOfUserRegistration(RMC.DataService.UserInfo userInfo)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                //Start Html Tag.
                builder.Append("<html><head><title>User Registration</title></head><body><table width=\"430px\"><tr><td align=\"center\" style=\"color: #06569d; font-weight: bold;\" colspan=\"2\">");
                builder.Append("The following User apply for Registration.</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\" width=\"190px\">Company Name &nbsp;&nbsp;</td>");
                //Show Company Name.
                builder.Append("<td align=\"left\">");
                builder.Append(userInfo.CompanyName);
                //Show First Name.
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\" width=\"190px\">First Name &nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(userInfo.FirstName);
                //Show Last Name.
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\" width=\"190px\">Last Name &nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(userInfo.LastName);
                //Show Phone.
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\" width=\"190px\">Phone &nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(userInfo.Phone);
                //Show Fax.
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\" width=\"190px\">Fax&nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(userInfo.Fax);
                //Show Email.
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\" width=\"190px\">Email &nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(userInfo.Email);
                //Show Security Question
                //builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\" width=\"190px\">Security Question &nbsp;&nbsp;</td><td align=\"left\">");
                //builder.Append(userInfo.SecurityQuestion);
                //Show Security Answer.
                //builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\" width=\"190px\">Security Answer &nbsp;&nbsp;</td><td align=\"left\">");
                //builder.Append(userInfo.SecurityAnswer);
                //builder.Append("</td></tr></table></body></html>");
                // Show Permission Requested
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\" width=\"190px\">Permission Requested &nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(userInfo.AccessRequest);

                return builder;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetEmailBodyOfHospitalDemographicDetail");
                ex.Data.Add("Class", "BSEmailNotificationBody");
                throw ex;
            }
        }

        // Added by Raman 
        // this function is used to get email details
        // added on 06 Jan 2010

        public StringBuilder GetEmailBodyOfRequestHospitalUnitAccess(RMC.DataService.UserInfo userInfo, RMC.DataService.HospitalDemographicInfo _objectHospitaDemoInfo)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                //Start Html Tag.
                builder.Append("<html><head><title>User Registration</title></head><body><table width=\"430px\"><tr><td align=\"center\" style=\"color: #06569d; font-weight: bold;\" colspan=\"2\">");
                builder.Append("The following User Apply for Hospital Unit Access.</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\" width=\"190px\">Company Name &nbsp;&nbsp;</td>");
                //Show Company Name.
                builder.Append("<td align=\"left\">");
                builder.Append(userInfo.CompanyName);
                //Show First Name.
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\" width=\"190px\">First Name &nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(userInfo.FirstName);
                //Show Last Name.
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\" width=\"190px\">Last Name &nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(userInfo.LastName);
                //Show Phone.
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\" width=\"190px\">Phone &nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(userInfo.Phone);
                //Show Fax.
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\" width=\"190px\">Fax&nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(userInfo.Fax);
                //Show Email.
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\" width=\"190px\">Email &nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(userInfo.Email);


                //Show Hospital Unit Name
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\" width=\"190px\">Hospital Unit Name&nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(_objectHospitaDemoInfo.HospitalUnitName);

                //Show Owner.
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\" width=\"190px\">Hospital Owner &nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(_objectHospitaDemoInfo.CreatedBy);
                builder.Append("</td></tr></table></body></html>");

                // Show Hospital Name
                builder.Append("</td></tr><tr><td align=\"right\" style=\"color: #06569d; font-weight: bold;\" width=\"190px\">Hospital Name &nbsp;&nbsp;</td><td align=\"left\">");
                builder.Append(_objectHospitaDemoInfo.HospitalInfo.HospitalName);

                return builder;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Function", "GetEmailBodyOfHospitalDemographicDetail");
                ex.Data.Add("Class", "BSEmailNotificationBody");
                throw ex;
            }
        }

        #endregion

    }
    //End Of BSEmailNotificationBody Class
}
//End Of NameSpace
