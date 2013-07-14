<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NonValidatedData.ascx.cs" Inherits="RMC.Web.UserControls.NonValidatedData" %>
<script language ="javascript" type ="text/javascript" >
      $(document).ready(function() {
          $("#ulHospitalTree").treeview({
              collapsed: true
              //toggle: TestToggle(this)
          });
      });
      function TestToggle(vartest)
      {
        alert(vartest + " has been toggled");
    }

    function OpenWindow() {
        window.open('MonthDetail.aspx', 'windowname1', 'width=800, height=650');
    }
    function OpenErrorWindow() {
        window.open('MonthlyErrorReport.aspx', 'windowname1', 'width=800, height=650');
    }
  </script>
 
  <table width="100%" >
  <tr>
            <th align ="center" >
                <h3 style="font-size: 13px;">
                    Non Validated Data
                </h3>
            </th>
     </tr>
    <tr>
        <td>
            <div id="divTreeView" runat="server" >
            </div>
        </td>
    </tr>
  </table> 
  