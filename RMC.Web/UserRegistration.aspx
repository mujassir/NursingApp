<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRegistration.aspx.cs"
    Inherits="RMC.Web.UserRegistration" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc11" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en-gb" lang="en-gb">
<head>
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta name="robots" content="index, follow" />
    <meta name="keywords" content="Rapid, Modeling" />
    <meta name="description" content="Rapid Modeling" />
    <meta name="generator" content="Joomla! 1.5 - Open Source Content Management" />
    <title>RMC :: User Registration</title>
    <script type="text/javascript" language="javascript" src="jquery/jquery-1.3.2.js"></script>
    <script type="text/javascript" language="javascript" src="jquery/jquery.maskedinput-1.2.2.js"></script>
    <script type="text/javascript" language="javascript" src="jquery/jquery.corner.js"></script>
    <script type="text/javascript" language="javascript" src="JavaScript/Common.js"></script>
    <script type="text/javascript" language="javascript">

        function ValidatePassword() {

            var regEx = /^(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,50})$/;
            if (regEx.test(document.getElementById("TextBoxPrimaryPassword").value)) {
                document.getElementById("passVal").style.display = "none";
            }
            else {
                document.getElementById("passVal").style.display = "inline-block";
            }
           // alert(document.getElementById("TextBoxPrimaryPassword").value);
        }
        jQuery(function ($) {

            $("#TextBoxPrimaryPhone").mask("999-999-9999");
            $("#TextBoxPrimaryFax").mask("999-999-9999");

            $("#divInner").corner("round 12px").parent().css('padding', '1px').corner("round 12px");
            $("#divErrorMsgInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
            centerDiv();
        });

        function centerDiv() {

            var div = $('#divOutter');
            var divHalfWidth = parseInt(div.width(), 10) / 2;
            var documentHalfWidth = parseInt($("#PanelRegistration").width(), 10) / 2;
            var divPadding = documentHalfWidth - divHalfWidth;
            div.css("padding-left", divPadding);
        }

        //--------- This is the jquery code that would be used to set all the textboxes with class water as watermark boxes -------------//
        $(document).ready(function () {

            //--------------------enter key logic--------------------//
            $("[id$='ps_search_str']").keypress(function (e) {
                var key;
                key = e.which ? e.which : e.keyCode;
                if (key == 13) {
                    window.location.href = 'http://www.rapidmodeling.com/?searchword=' + $(this).val() + '&searchphrase=any&limit=&ordering=newest&view=search&Itemid=99999999&option=com_search#';
                    return false;
                }
                else {
                    return true;
                }
            });


            $(".WaterMarkTextBox").focus(function () {
                if ($(this).val() == this.defaultValue) {
                    $(this).val("");
                }
            });


            $(".WaterMarkTextBox").blur(function () {
                if ($.trim($(this).val()) == "") {
                    $(this).val(this.defaultValue);
                }
                if ($(this).attr('type') == "password") {
                    var $watermark = $("<span/>")
                    .addClass("updnWatermark")
                    .insertBefore(this)
                    .hide()
					.bind("show", function () {
					    $(this).children().show();
					})
                    .bind("hide", function () {
                        $(this).children().hide();
                    });
                    // Positions watermark label relative to positioning context
                    $("<label/>").appendTo($watermark)
                    .text("Password")
                    .attr("for", this.id);
                    // Associate input element with watermark plugin.
                    if (!$(this).val()) {
                        $watermark.trigger("show");
                    }
                    $(this).focus(function (ev) {
                        $watermark.trigger("hide");
                    });

                    if (!$(this).val()) {
                        $watermark.show();
                    }
                }
            });
        });
        
    </script>
    <script src="JavaScript/MenuDropdown.js" type="text/javascript"></script>
    <link href="CSS/MenuDropDown.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="CSS/template.css" type="text/css" />
    <link href="CSS/ControlStyles.css" rel="stylesheet" type="text/css" />
    <link href="/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <%-- <link rel="stylesheet" href="CSS/style.css" type="text/css" />--%>
</head>
<body>
    <a name="up" id="up"></a>
    <div id="container">
        <div style="margin: 0px auto; width: 800px;">
            <div id="head-content">
                <div id="header">
                    <div id="logo">
                        <a href="#">
                            <img src="Images/logoMain.png" alt="Rapid Modelling" /></a>
                    </div>
                    <div style="width: 530px; float: left;">
                        <div id="search">
                        </div>
                        <div class="topmenu" style="float: left; padding-left: 35px;">
                            <div id="pillmenu">
                                <ul class="menusan" id="jsddm">
                                    <li class="item28"><a href="http://www.rapidmodeling.com/"><span>Home</span></a>
                                    </li>
                                    <li class="item18"><a href="#"><span>Products</span></a>
                                        <ul>
                                            <li><a href="http://www.rapidmodeling.com/index.php?option=com_content&amp;view=article&amp;id=46&amp;Itemid=54"
                                                style='font-weight: bold;'>Time Study RN</a></li>
                                            <li><a href="http://www.rapidmodeling.com/index.php?option=com_content&amp;view=article&amp;id=50&amp;Itemid=63"
                                                style='font-weight: bold;'>Layout -IQ</a></li>
                                            <li><a href="http://www.rapidmodeling.com/index.php?option=com_content&amp;view=article&amp;id=51&amp;Itemid=61"
                                                style='font-weight: bold;'>Simulaton Modeling</a></li>
                                            <li><a href="http://www.rapidmodeling.com/index.php?option=com_content&amp;view=article&amp;id=52&amp;Itemid=64"
                                                style='font-weight: bold;'>Shifthound</a></li>
                                            <li><a href="http://www.rapidmodeling.com/index.php?option=com_content&amp;view=article&amp;id=47&amp;Itemid=55"
                                                style='font-weight: bold;'>Work Measurement</a></li>
                                            <li><a href="http://www.rapidmodeling.com/index.php?option=com_content&amp;view=article&amp;id=49&amp;Itemid=62"
                                                style='font-weight: bold;'>Auditing &amp; Data Collection</a></li>
                                        </ul>
                                    </li>
                                    <li class="parent item29"><a href="http://www.rapidmodeling.com/index.php?option=com_mad4joomla&amp;jid=4&amp;Itemid=73">
                                        <span>Support</span></a> </li>
                                    <li class="item30"><a href="http://www.rapidmodeling.com/Private/login.aspx"><span>Nurse
                                        Data</span></a> </li>
                                </ul>
                            </div>
                        </div>
                        <a style="font-size: 8.5px; margin-right: 11px; float: right;" href="#">For Government</a>
                        <div style="padding: 0pt 0pt 0pt 40px; float: left;">
                            <div class="pixsearch">
                                <input id="ps_search_str" name="searchword" value="Search..." autocomplete="off"
                                    type="text" style="width: 115px;" class="WaterMarkTextBox" />
                            </div>
                        </div>
                    </div>
                    <div style="clear: both;">
                    </div>
                </div>
                <!--end of header-->
            </div>
            <!--end of head content-->
            <div id="bottom-content">
                <div id="content">
                    <div class="content-inner">
                        <div class="top">
                            <div class="topleft">
                                <div class="topright">
                                </div>
                            </div>
                        </div>
                        <!--begin of wrap-arin-->
                        <div id="wrapper">
                            <div id="wrapper_r">
                                <div id="whitebox_m">
                                    <div id="area">
                                        <form id="form1" runat="server" defaultbutton="ButtonSave" style="text-align: center;">
                                        <asp:ScriptManager ID="ScriptManagerUserRegistration" runat="server">
                                        </asp:ScriptManager>
                                        <asp:Panel ID="PanelRegistration" runat="server" HorizontalAlign="Center" Width="100%">
                                            <%--<center style="width: 450px;">--%>
                                            <div id="divOutter" style="background-color: White; z-index: 0; text-align: center;
                                                width: 450px; float: none;">
                                                <div id="divInner" style="background-color: White; z-index: 10;">
                                                    <div style="padding-top: 0px; padding-bottom: 10px; background-image: url(images/bg_gradient.jpg);">
                                                        <table width="430px">
                                                            <tr>
                                                                <th align="left" style="font-size: 14px; color: #06569D; padding-left: 10px; padding-top: 10px;">
                                                                    <u>User Registration</u>
                                                                </th>
                                                                <th align="right" style="padding-left: 10px; padding-top: 10px;">
                                                                    <asp:ImageButton ID="ImageButton1" ToolTip="Back To Login" runat="server" ImageUrl="~/Images/back.gif"
                                                                        PostBackUrl="~/Login.aspx" TabIndex="14" />
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="left">
                                                                    <div style="width: 420px; float: left; background-color: Transparent; z-index: 0;">
                                                                        <div id="divErrorMsgInner" style="width: 418px; float: left; background-color: #E8E9EA;
                                                                            z-index: 10;">
                                                                            <div style="padding-left: 90px;">
                                                                                <asp:ValidationSummary ID="ValidationSummaryUserRegistration" ValidationGroup="UserRegistration"
                                                                                    runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" Font-Size="11px"
                                                                                    Font-Bold="true" Style="padding-top: 1px;" />
                                                                                <asp:Panel ID="PanelErrorMsg" runat="server" Style="padding-top: 1px;" Visible="false">
                                                                                    <ul>
                                                                                        <li>
                                                                                            <asp:Label ID="LabelErrorMsg" runat="server" Font-Bold="true" Font-Size="11px"></asp:Label>
                                                                                        </li>
                                                                                    </ul>
                                                                                </asp:Panel>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                                                                    Company Name <span style="color: Red;">*</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TextBoxCompanyName" MaxLength="100" runat="server" Width="160px"
                                                                        TabIndex="1"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorCompanyName" runat="server"
                                                                        ErrorMessage="Required Company Name." ControlToValidate="TextBoxCompanyName"
                                                                        Display="None" SetFocusOnError="true" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                                                                    First Name <span style="color: Red;">*</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TextBoxPrimaryFirstName" MaxLength="50" runat="server" Width="160px"
                                                                        TabIndex="2"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPrimaryFirstName" runat="server"
                                                                        ErrorMessage="Required Primary Person's First Name." ControlToValidate="TextBoxPrimaryFirstName"
                                                                        Display="None" SetFocusOnError="true" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                                                                    Last Name <span style="color: Red;">*</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TextBoxPrimaryLastName" MaxLength="50" runat="server" Width="160px"
                                                                        TabIndex="3"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPrimaryLastName" runat="server"
                                                                        SetFocusOnError="true" ErrorMessage="Required Primary Person's Last Name." ControlToValidate="TextBoxPrimaryLastName"
                                                                        Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                                                                    Phone <span style="color: Red;">*</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TextBoxPrimaryPhone" MaxLength="20" runat="server" Width="160px"
                                                                        TabIndex="4"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPrimaryPhone" runat="server"
                                                                        SetFocusOnError="true" ErrorMessage="Required Primary Phone Number." ControlToValidate="TextBoxPrimaryPhone"
                                                                        Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                                                                    Fax&nbsp;&nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TextBoxPrimaryFax" MaxLength="20" runat="server" Width="160px" TabIndex="5"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                                                                    Email <span style="color: Red;">*</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TextBoxPrimaryEmail" runat="server" AutoCompleteType="Email" TabIndex="6"
                                                                        Width="160px" MaxLength="200"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPrimaryEmail" runat="server"
                                                                        SetFocusOnError="false" ErrorMessage="Required Primary Email Address." ControlToValidate="TextBoxPrimaryEmail"
                                                                        Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorPrimaryEmail" runat="server"
                                                                        SetFocusOnError="false" ErrorMessage="Please Enter Valid Email Address." ControlToValidate="TextBoxPrimaryEmail"
                                                                        Display="None" ValidationGroup="UserRegistration" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                                                                    Password <span style="color: Red;">*</span>
                                                                </td>
                                                                <td align="left">
                                                                    <div id="passVal" style="display:none; height:auto; padding:5px; left:355px; top:233px; width:auto; border-radius:3px; box-shadow:2px grey; background-color:#C53F40; z-index:2800; position:absolute; color:White; font-weight:bold;">
                                                                        Must use at least one digit and 8 to 50 Characters in Password.
                                                                    </div>
                                                                    <asp:TextBox ID="TextBoxPrimaryPassword" onkeyup="ValidatePassword()" runat="server" MaxLength="50" TextMode="Password"
                                                                        TabIndex="7" Width="160px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPrimaryPassword" runat="server"
                                                                        SetFocusOnError="false" ControlToValidate="TextBoxPrimaryPassword" Display="None"
                                                                        ErrorMessage="Required Password for Primary Person." ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorPrimaryPassword" runat="server"
                                                                        SetFocusOnError="false" ControlToValidate="TextBoxPrimaryPassword" Display="Dynamic"
                                                                        ErrorMessage="Must use at least one digit and 8 to 50 Characters in Password."
                                                                        ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,50})$" ValidationGroup="UserRegistration">*</asp:RegularExpressionValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                                                                    Confirm Password <span style="color: Red;">*</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TextBoxPrimaryConfirmPwd" runat="server" MaxLength="50" TextMode="Password"
                                                                        TabIndex="8" Width="160px"></asp:TextBox>
                                                                    <asp:CompareValidator ID="CompareValidatorConfirmPwd" runat="server" ControlToCompare="TextBoxPrimaryConfirmPwd"
                                                                        SetFocusOnError="false" ControlToValidate="TextBoxPrimaryPassword" Display="None"
                                                                        ErrorMessage="Confirm Password of Primary Person does not match." ValidationGroup="UserRegistration">*</asp:CompareValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                                                                    Security Question <span style="color: Red;">*</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TextBoxPrimarySecurityQuestion" runat="server" MaxLength="500" TabIndex="9"
                                                                        Width="160px" AutoCompleteType="Disabled"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorSecurityQuestion" runat="server"
                                                                        SetFocusOnError="true" ErrorMessage="Required Security Question." ControlToValidate="TextBoxPrimarySecurityQuestion"
                                                                        Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                                                                    Security Answer <span style="color: Red;">*</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TextBoxPrimarySecurityAnswer" runat="server" MaxLength="500" TabIndex="10"
                                                                        Width="160px" AutoCompleteType="Disabled"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorSecurityAnswer" runat="server"
                                                                        SetFocusOnError="true" ErrorMessage="Required Security Answer." ControlToValidate="TextBoxPrimarySecurityAnswer"
                                                                        Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="color: #06569d; font-weight: bold;" width="190px">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <cc11:CaptchaControl ID="CaptchaControlImage" runat="server" CaptchaBackgroundNoise="High"
                                                                        CaptchaLength="5" CaptchaHeight="60" CaptchaWidth="190" CaptchaLineNoise="None"
                                                                        CaptchaMinTimeout="5" CaptchaMaxTimeout="240" FontColor="#db4342" LineColor="192, 192, 255"
                                                                        NoiseColor="192, 192, 255" CaptchaFontWarping="Low" Width="236px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;" width="190px">
                                                                    Type Code from Image <span style="color: Red;">*</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="TextBoxCaptchaText" runat="server" MaxLength="500" TabIndex="11"
                                                                        Width="160px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorCaptchaText" runat="server"
                                                                        SetFocusOnError="true" ErrorMessage="Must Enter the Captcha Text." ControlToValidate="TextBoxCaptchaText"
                                                                        Display="None" ValidationGroup="UserRegistration">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" colspan="2" style="color: #06569d; padding-left: 50px; font-weight: bold;
                                                                    font-size: 11px;" width="190px">
                                                                    <asp:RadioButton ID="rbtOwner" GroupName="OwnerRights" Checked="true" runat="server" />&nbsp;&nbsp;
                                                                    I want to create a new hospital unit record and upload data.
                                                                    <br />
                                                                    <asp:RadioButton ID="rbtNonOwner" GroupName="OwnerRights" runat="server" />&nbsp;&nbsp;I
                                                                    want to view reports for an existing hospital unit dataset.
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-top: 5px;" width="190px">
                                                                </td>
                                                                <td align="left" style="padding-top: 5px;">
                                                                    <asp:Button ID="ButtonSave" runat="server" Text="Save" ValidationGroup="UserRegistration"
                                                                        OnClick="ButtonSave_Click" CssClass="aspButton" Width="70px" TabIndex="12" />
                                                                    &nbsp;
                                                                    <asp:Button ID="ButtonReset" runat="server" Text="Reset" OnClick="ButtonReset_Click"
                                                                        CssClass="aspButton" Width="70px" Visible="false" TabIndex="13" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--</center>--%>
                                        </asp:Panel>
                                        </form>
                                        <div class="clr" style="height: 10px;">
                                        </div>
                                    </div>
                                    <label>
                                    </label>
                                </div>
                            </div>
                        </div>
                        <!--end of wrap-arin-->
                        <div class="bottom">
                            <div class="bottomleft">
                                <div class="bottomright">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
