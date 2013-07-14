<%@ Page Language="C#" Title="RMC :: ForgetPassword" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs"
    Inherits="RMC.Web.ForgetPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta name="robots" content="index, follow" />
    <meta name="keywords" content="Rapid, Modeling" />
    <meta name="description" content="Rapid Modeling" />
    <meta name="generator" content="Joomla! 1.5 - Open Source Content Management" />
    <title>RMC:ForgetPassword</title>
    <script type="text/javascript" language="javascript" src="jquery/jquery-1.3.2.js"></script>
    <script type="text/javascript" language="javascript" src="jquery/jquery.maskedinput-1.2.2.js"></script>
    <script type="text/javascript" language="javascript" src="jquery/jquery.corner.js"></script>
    <script type="text/javascript" language="javascript" src="JavaScript/Common.js"></script>
    <script type="text/javascript" language="javascript">
        jQuery(function ($) {

            $("#divInner").corner("round 12px").parent().css('padding', '1px').corner("round 12px");
            $("#divErrorMsgInner").corner("round 6px").parent().css('padding', '1px').corner("round 6px");
        });


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
    <link rel="stylesheet" href="CSS/template.css" type="text/css">
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
                                        <form id="form1" runat="server">
                                        <asp:Panel ID="PanelForgetPassword" runat="server" DefaultButton="ButtonSubmit">
                                            <center style="width: 450px;">
                                                <div id="divOutter" style="background-color: white; z-index: 0;">
                                                    <div id="divInner" style="background-color: White; z-index: 10;">
                                                        <div style="padding-top: 0px; padding-bottom: 10px; background-image: url(images/bg_gradient.jpg);">
                                                            <table width="430px">
                                                                <tr>
                                                                    <th align="left" style="font-size: 14px; color: #06569D; padding-left: 10px; padding-top: 10px;">
                                                                        <u>Forget Password</u>
                                                                    </th>
                                                                    <th align="left" style="padding-left: 10px; padding-top: 10px;">
                                                                        <asp:ImageButton ID="ImageButton1" ToolTip="Back To Login" runat="server" ImageUrl="~/Images/back.gif"
                                                                            PostBackUrl="~/Login.aspx" TabIndex="7" />
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 2px;">
                                                                        <div style="width: 302px; float: left; background-color: Transparent; z-index: 0;
                                                                            margin-left: 80px">
                                                                            <div id="divErrorMsgInner" style="width: 300px; float: left; background-color: #E8E9EA;
                                                                                z-index: 10;">
                                                                                <div style="float: left;">
                                                                                    <asp:ValidationSummary ID="ValidationSummaryUserRegistration" ValidationGroup="ForgetPassword"
                                                                                        runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" Font-Size="11px"
                                                                                        Font-Bold="true" Style="padding-top: 1px;" />
                                                                                    <asp:Panel ID="PanelErrorMsg" runat="server" Style="padding-top: 1px;" Visible="false">
                                                                                        <ul>
                                                                                            <li style="color: Red">
                                                                                                <asp:Label ID="LabelErrorMsg" runat="server" Font-Bold="true" Font-Size="11px" ForeColor="Red"></asp:Label>
                                                                                            </li>
                                                                                        </ul>
                                                                                    </asp:Panel>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div style="float: left; margin-left: 30px">
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                                                                        Email <span style="color: Red;">*</span>
                                                                                    </td>
                                                                                    <td align="left">
                                                                                        <asp:TextBox ID="TextBoxUserName" runat="server" MaxLength="200" Width="250px" TabIndex="1"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorUsername" runat="server" ControlToValidate="TextBoxUserName"
                                                                                            SetFocusOnError="true" Display="None" ErrorMessage="Required Email." ValidationGroup="ForgetPassword">*</asp:RequiredFieldValidator>
                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorPrimaryEmail" runat="server"
                                                                                            SetFocusOnError="true" ErrorMessage="Please Enter Valid Email Address." ControlToValidate="TextBoxUserName"
                                                                                            Display="None" ValidationGroup="ForgetPassword" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                                                                    </td>
                                                                                </tr>
                                                                                <asp:Panel ID="PanelSecurityQuestion" runat="server" Visible="false">
                                                                                    <tr>
                                                                                        <td align="right" style="color: #06569d; font-weight: bold;">
                                                                                            Question &nbsp
                                                                                        </td>
                                                                                        <td align="left" style="color: #06569d;">
                                                                                            <asp:TextBox ID="TextBoxSecurityQuestion" runat="server" ReadOnly="true" MaxLength="500"
                                                                                                Width="250px" TabIndex="2"></asp:TextBox>
                                                                                            <%--  <asp:Label ID="LabelSecurityQuestion" runat="server"  Font-Size="11px"></asp:Label>--%>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right" style="color: #06569d; font-weight: bold;">
                                                                                            Answer <span style="color: Red;">*</span>
                                                                                        </td>
                                                                                        <td align="left" style="color: #06569d;">
                                                                                            <asp:TextBox ID="TextBoxAnswer" runat="server" MaxLength="500" Width="250px" TabIndex="3"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAnswer" runat="server" ControlToValidate="TextBoxAnswer"
                                                                                                SetFocusOnError="true" Display="None" ErrorMessage="Required Security Answer."
                                                                                                ValidationGroup="ForgetPassword">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                </asp:Panel>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                    </td>
                                                                                    <td align="left">
                                                                                        <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" ValidationGroup="ForgetPassword"
                                                                                            CssClass="aspButton" Width="70px" OnClick="ButtonSubmit_Click" TabIndex="4" />
                                                                                        <asp:Button ID="ButtonSubmitAnswer" runat="server" Visible="false" Text="Submit"
                                                                                            CssClass="aspButton" Width="70px" ValidationGroup="ForgetPassword" OnClick="ButtonSubmitAnswer_Click"
                                                                                            TabIndex="5" />
                                                                                        &nbsp;
                                                                                        <asp:Button ID="ButtonReset" runat="server" Visible="false" Text="Reset" CssClass="aspButton"
                                                                                            Width="70px" OnClick="ButtonReset_Click" TabIndex="6" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </center>
                                        </asp:Panel>
                                        </form>
                                        <div class="clr" style="height: 10px;">
                                        </div>
                                    </div>
                                    <table width="100%" border="0" cellspacing="5" cellpadding="5">
                                        <tr>
                                            <td width="33%" valign="top">
                                                <div class="moduletable">
                                                    <h3>
                                                        View Video Presentations</h3>
                                                    <span style="display: block; background-image: url('images/arrow.gif'); padding-left: 15px;
                                                        padding-top: 3px; padding-bottom: 3px; height: 16px; background-repeat: no-repeat;
                                                        background-position: 0px 5px">Title 1</span> <span style="display: block; background-image: url('images/arrow.gif');
                                                            padding-left: 15px; padding-top: 3px; padding-bottom: 3px; height: 16px; background-repeat: no-repeat;
                                                            background-position: 0px 5px">Title 2</span>
                                                </div>
                                                <div class="moduletable">
                                                    <h3>
                                                        View Case Studies</h3>
                                                    <span style="display: block; background-image: url('images/arrow.gif'); padding-left: 15px;
                                                        padding-top: 3px; padding-bottom: 3px; height: 16px; background-repeat: no-repeat;
                                                        background-position: 0px 5px">Rapid Modeling Resources</span><span style="display: block;
                                                            background-image: url('images/arrow.gif'); padding-left: 15px; padding-top: 3px;
                                                            padding-bottom: 3px; height: 16px; background-repeat: no-repeat; background-position: 0px 5px">Identifying
                                                            Interuption </span>
                                                </div>
                                            </td>
                                            <td width="33%" valign="top">
                                                <div class="moduletable_feed">
                                                    <h3>
                                                        Feed Display</h3>
                                                    <table cellpadding="0" cellspacing="0" class="moduletable_feed">
                                                        <tr>
                                                            <td>
                                                                <ul class="newsfeed_feed" style="padding-left: 15px;">
                                                                    <li><a href="" target="_blank">Demo Post (Please replace)</a>
                                                                        <div class="newsfeed_item_feed">
                                                                            This is your first blog post.
                                                                        </div>
                                                                    </li>
                                                                </ul>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                            <td width="34%" valign="top">
                                                <div class="moduletable">
                                                    <h3>
                                                        Contact and Suport</h3>
                                                    Contact Information Here
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
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
