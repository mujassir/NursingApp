<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDemographicMembersTreeView.ascx.cs" Inherits="RMC.Web.UserControls.ucDemographicMembersTreeView" %>
<link rel="stylesheet" href="../CSS/screen.css" type="text/css" />
  <link rel="stylesheet" href="../CSS/treeview.css"type="text/css" />
  <script language ="javascript" type ="text/javascript" >
      $(document).ready(function() {
      $("#ulHospitalDemographicMemberTree").treeview({
              collapsed: true
              //toggle: TestToggle(this)
          });
      });
  </script>
  <table width="100%" >
    <tr>
            <th align ="center" >
                <h3 style="font-size: 13px;">
                   Hospital Unit Approved Users
                </h3>
            </th>
     </tr>
    <tr>
        <td>
            <div id="divmembersTreeView" runat="server">
            </div>
        </td>
    </tr>
  </table>