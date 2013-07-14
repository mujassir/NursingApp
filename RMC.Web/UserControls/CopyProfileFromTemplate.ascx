<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CopyProfileFromTemplate.ascx.cs" Inherits="RMC.Web.UserControls.CopyProfileFromTemplate" %>

<style type="text/css">
    .style1
    {
        text-align: center;
    }
</style>

<script type="text/javascript">

    function ClosePopup(buttonClicked) {
        if(buttonClicked==1)
            window.opener.document.forms(0).submit();
        window.close();
    }
</script>
<form runat="server">
<table width="100%" style="height: 118px">
    <tr>
        <td class="style1">
         <asp:DropDownList ID="DropDownListProfileType" CssClass="aspDropDownList" AutoPostBack="true"
                                    runat="server" ForeColor="#06569D" TabIndex="4" 
            onselectedindexchanged="DropDownListProfileType_SelectedIndexChanged" 
                Height="22px" Width="166px" >
         </asp:DropDownList>
         </td>
    </tr>
    <tr>
    <td style="margin-left: 40px" class="style1">
        <asp:Button ID="ButtonOk" Text="Ok" OnClientClick="ClosePopup(1);" 
            runat="server" Width="79px"/>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ButtonCancel" Text="Cancel" OnClientClick=" ClosePopup(0);" 
            runat="server" onclick="ButtonCancel_Click"/>
    </td>
    </tr>
</table>
</form>


 
 