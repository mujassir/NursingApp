<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileTreeView.ascx.cs"
    Inherits="RMC.Web.UserControls.ProfileTreeView" %>

<script language="javascript" type="text/javascript">
    function ConfirmMessage(profileTypeID) {

        var where_to = confirm("Do you really want to delete this profile?");
        if (where_to == true) {
            window.location.href = "../Users/DeleteCategoryProfile.aspx?ProfileTypeID=" + profileTypeID;
        }
    }

    function ConfirmMessageForAdministrator(profileTypeID) {

        var where_to = confirm("Do you really want to delete this profile?");
        if (where_to == true) {
            window.location.href = "../Administrator/DeleteCategoryProfile.aspx?ProfileTypeID=" + profileTypeID;
        }
    }

    function mypopup(senderID) {

        mywindow = window.open("../Users/SendUserNotification.aspx?SenderID=" + senderID, "", "location=0,status=0,scrollbars=1, width=500,height=300,resizable=no ");
        return false;
    }

    function mypopupAdministrator(senderID) {

        mywindow = window.open("../Administrator/SendUserNotification.aspx?SenderID=" + senderID, "", "location=0,status=0,scrollbars=1, width=500,height=300,resizable=no ");
        return false;
    } 
</script>

<table width="100%">
    <tr>
        <td>
            <asp:TreeView ID="TreeViewProfile" runat="server" ImageSet="Arrows" Font-Size="10px"
                TabIndex="1">
                <Nodes>
                    <asp:TreeNode Text="Value Added Category Profiles " Value="Value Added Category Profiles">
                    </asp:TreeNode>
                    <asp:TreeNode Text="Other Category Profiles" Value="Other Category Profiles"></asp:TreeNode>
                    <asp:TreeNode Text="Location Category Profiles" Value="Location Category Profiles">
                    </asp:TreeNode>
                    <asp:TreeNode Text="Activities Category Profiles" Value="Activities Category Profiles"></asp:TreeNode>
                </Nodes>
            </asp:TreeView>
        </td>
    </tr>
</table>
