<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RMC.Web.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en-gb" lang="en-gb">
<head>
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta name="robots" content="index, follow" />
    <meta name="keywords" content="Rapid, Modeling" />
    <meta name="description" content="Rapid Modeling" />
    <meta name="generator" content="Joomla! 1.5 - Open Source Content Management" />
    <title>RMC :: Login Page</title>

    <script type="text/javascript" language="javascript" src="jquery/jquery-latest.js"></script>

    <script type="text/javascript" language="javascript" src="jquery/jquery.corner.js"></script>

    <script type="text/javascript" language="javascript" src="JavaScript/Common.js"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {

            $("#divLogin").corner("round 12px").parent().css('padding', '1px').corner("round 12px");
            setWindowHeight();
        });
        
        function setWindowHeight() {

            var value = screen.height - 300;
            var setHeight = value + "px";
            $("#divMain").css("height", setHeight);
        }


        //--------- This is the jquery code that would be used to set all the textboxes with class water as watermark boxes -------------//
        $(document).ready(function() {

            //--------------------enter key logic--------------------//
            $("[id$='ps_search_str']").keypress(function(e) {
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


            $(".WaterMarkTextBox").focus(function() {
                if ($(this).val() == this.defaultValue) {
                    $(this).val("");
                }
            });


            $(".WaterMarkTextBox").blur(function() {
                if ($.trim($(this).val()) == "") {
                    $(this).val(this.defaultValue);
                }
                if ($(this).attr('type') == "password") {
                    var $watermark = $("<span/>")
                    .addClass("updnWatermark")
                    .insertBefore(this)
                    .hide()
					.bind("show", function() {
					    $(this).children().show();
					})
                    .bind("hide", function() {
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
                    $(this).focus(function(ev) {
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
                                        <div id="maincolumn">
                                            <div class="nopad">
                                                <table class="blog" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td valign="top">
                                                            <div>
                                                                <table class="contentpaneopen">
                                                                    <tr>
                                                                        <td valign="top" colspan="2" style="font-size: 13px;">
                                                                            <p>
                                                                                <font color="#ff6600"><strong></strong></font>
                                                                            </p>
                                                                            <p align="justify">
                                                                                The Time Study RN National Benchmarking Database allows users of Time Study RN to
                                                                                compare their performance against other hospitals nationally. The database includes
                                                                                over 350 hospital units in the United States and Canada, and the database is growing
                                                                                monthly.
                                                                            </p>
                                                                            <p align="justify">
                                                                                Only qualified administrator participants and approved research principles are provided
                                                                                access to the database. If you are a user of Time Study RN and you would like access
                                                                                to the database, then complete the User Registration link on this page and the database
                                                                                will review your application. Once your application is approved, then you will be
                                                                                able to enter your unit demographics, upload your data, and begin comparing your
                                                                                performance against other hospitals through North America.
                                                                            </p>
                                                                            <p align="justify">
                                                                                If you are a healthcare researcher, architectural firm, or other organization interested
                                                                                in learning more about how the built environment impacts the Nurse workload, or
                                                                                if you would like to contact the database administrator for any reason, then please
                                                                                send an email to the <font color="blue"><u><a href="mailto:nlee@rapidmodeling.com">nlee@rapidmodeling.com</a></u></font>
                                                                                .
                                                                            </p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <span class="article_separator">&nbsp;</span>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div id="rightcolumn" style="float: left;">
                                            <form id="form1" runat="server">
                                            <div id="divMain" style="float: left; width: 350px;">
                                                <table width="330px">
                                                    <tr>
                                                        <td valign="middle" style="padding-top: 10px;">
                                                            <div id="divOutterLogin" style="background-color: White; z-index: 9;">
                                                                <div id="divLogin" style="background-color: White; z-index: 10;">
                                                                    <div style="padding-top: 0px; padding-bottom: 10px; background-image: url(images/bg_gradient.jpg);">
                                                                        <table width="300px">
                                                                            <tr>
                                                                                <%--<th align="center">
                                                                                    <center>
                                                                                        <h3 style="color: #06569d; font-size: 13px;">
                                                                                            Login
                                                                                        </h3>
                                                                                    </center>
                                                                                </th>--%>
                                                                                <th align="left" style="font-size: 14px; color: #06569D; padding-left: 10px; padding-top: 10px;">
                                                                                    <u>Login</u>
                                                                                </th>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 5px;">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 2px;">
                                                                                    <asp:Label ID="LabelErrorMsg" runat="server" Font-Size="12px" Visible="False"></asp:Label>
                                                                                    <asp:ValidationSummary ID="ValidationSummaryLogin" runat="server" DisplayMode="List"
                                                                                        Font-Size="12px" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Login" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table width="300px">
                                                                                        <tr>
                                                                                            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                                                                                Email <span style="color: Red;">*</span>
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:TextBox ID="TextBoxUserName" runat="server" MaxLength="200" Width="150px" TabIndex="1"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorUsername" runat="server" ControlToValidate="TextBoxUserName"
                                                                                                    Display="None" ErrorMessage="Required Username." SetFocusOnError="true" ValidationGroup="Login">*</asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right" style="color: #06569d; font-weight: bold; font-size: 11px;">
                                                                                                Password <span style="color: Red;">*</span>
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" MaxLength="50"
                                                                                                    Width="150px" TabIndex="2"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ControlToValidate="TextBoxPassword"
                                                                                                    Display="None" SetFocusOnError="true" ErrorMessage="Required Password." ValidationGroup="Login">*</asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left">
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" OnClick="ButtonSubmit_Click"
                                                                                                    ValidationGroup="Login" CssClass="aspButton" Width="70px" TabIndex="3" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left">
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:LinkButton ID="LinkButtonUserRegistration" runat="server" PostBackUrl="~/UserRegistration.aspx"
                                                                                                    Font-Bold="true" Font-Underline="false" Font-Size="11px" TabIndex="4">User Registration</asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="left">
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:LinkButton ID="LinkButtonForgetPassword" runat="server" Font-Bold="true" Font-Underline="false"
                                                                                                    PostBackUrl="~/ForgetPassword.aspx" Font-Size="11px" TabIndex="5">Forget Password</asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            </form>
                                        </div>
                                        <div class="clr">
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
