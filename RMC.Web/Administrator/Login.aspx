<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RMC.Web.Administrator.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Admin Login Form</title>
    <script type="text/javascript" language="javascript" src="../jquery/jquery-1.3.2.js"></script>

  
    <script type="text/javascript" language="javascript" src="../jquery/jquery.corner.js"></script>

    <script type="text/javascript" language="javascript" src="../JavaScript/Common.js"></script>
    
    <script type="text/javascript" language="javascript">
        jQuery(function($) {

            $("#divLogin").corner("round 6px").parent().css('padding', '1px').corner("round 6px");            
        });
        
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td style="height: 120px;">
            </td>
        </tr>
        <tr>
            <td>
                <center>
                    <table>
                        <tr>
                            <th>
                                <center>
                                    <h3>
                                        <u>Admin User Login </u>
                                    </h3>
                                </center>
                            </th>
                        </tr>
                        <tr>
                            <td style="height: 2px;">
                                <asp:Label ID="LabelErrorMsg" runat="server" Font-Size="12px" Visible="False"></asp:Label>
                                <asp:ValidationSummary ID="ValidationSummaryLogin" runat="server" DisplayMode="List"
                                    Font-Size="12px" ValidationGroup="Login" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            <div id="divOutterLogin" style="background-color: Black; z-index: 0;">
                        <div id="divLogin" style="background-color: White; z-index: 10;">
                            <div style="padding-top: 10px; padding-bottom: 10px;">
                                <table width="300px">
                                    <tr>
                                        <td align="right">
                                            Username :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxUserName" runat="server" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUsername" runat="server" ControlToValidate="TextBoxUserName"
                                                Display="None" ErrorMessage="Required Username." ValidationGroup="Login">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Password :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ControlToValidate="TextBoxPassword"
                                                Display="None" ErrorMessage="Required Password." ValidationGroup="Login">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td align="left">
                                            <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" OnClick="ButtonSubmit_Click" />
                                        </td>
                                    </tr>
                                    
                                    
                                </table>
                             </div>
                        </div>
                    </div>
                            </td>
                        </tr>
                    </table>
                </center>
            </td>
        </tr>
        <tr>
            <td style="height: 200px;">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
