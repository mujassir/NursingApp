<%@ Page Language="C#" MasterPageFile="~/Users/UserMasterPage.Master" AutoEventWireup="true"
    CodeBehind="FileUploader.aspx.cs" Inherits="RMC.Web.Users.FileUploader" Title="RMC :: Upload Files" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="99%">
        <tr>
            <th style="font-size: 14px; color: #06569d; padding-left: 10px; padding-top: 10px;"
                align="left">
                <%--<h3>--%>
                <u>Upload Files</u><%--</h3>--%>
            </th>
            <th style="padding-left: 10px; padding-top: 10px;" align="right">
                <asp:ImageButton ID="ImageButtonBack" ToolTip="Back" runat="server" ImageUrl="~/Images/back.gif"
                    OnClick="ImageButtonBack_Click" TabIndex="3" />
            </th>
        </tr>
        <tr>
            <td colspan="4" style="height: 10px;">
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <div style="padding-left: 15px;">
                    <asp:LinkButton ID="LinkButtonHospitalIndex" Font-Bold="true" Font-Size="11px" runat="server"
                        OnClick="LinkButtonHospitalIndex_Click">Hospital Index</asp:LinkButton>
                </div>
                <div style="padding-left: 30px; padding-top: 3px; width: 100%; height: 14px;">
                    <div style="float: left; width: 87%;">
                        <asp:LinkButton ID="LinkButtonHospitalInformation" Font-Bold="true" Font-Size="11px"
                            runat="server" OnClick="LinkButtonHospitalInformation_Click">LinkButton</asp:LinkButton>
                    </div>
                    <div style="float: left; width: 12%;" id="divEditHospital" runat="server">
                    </div>
                </div>
                <div style="padding-left: 45px; padding-top: 3px; width: 100%; height: 14px;">
                    <div style="float: left; width: 84%;">
                        <asp:LinkButton ID="LinkButtonHospitalUnitInformation" Font-Bold="true" Font-Size="11px"
                            runat="server" OnClick="LinkButtonHospitalUnitInformation_Click">LinkButton</asp:LinkButton>
                    </div>
                    <div style="float: left; width: 15%;" id="divEditHospitalUnit" runat="server">
                    </div>
                </div>
                <div style="padding-left: 60px; padding-top: 2px;">
                    <asp:LinkButton ID="LinkButtonYear" Font-Bold="true" Font-Size="11px" runat="server"
                        OnClick="LinkButtonYear_Click">LinkButton</asp:LinkButton>
                </div>
                <div style="padding-left: 75px; padding-top: 2px;">
                    <asp:LinkButton ID="LinkButtonMonth" Font-Bold="true" Font-Size="11px" runat="server"
                        OnClick="LinkButtonMonth_Click">LinkButton</asp:LinkButton>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr width="100%" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table>
                    <tr>
                        <td colspan="4" align="left">
                            <div style="width: 320px; float: left; background-color: Transparent; z-index: 0;">
                                <div id="divErrorMsgInner" style="width: 318px; float: left; background-color: #E8E9EA;
                                    z-index: 10;">
                                    <div style="padding-left: 90px;">
                                        <asp:ValidationSummary ID="ValidationSummaryUserRegistration" ValidationGroup="UploadFile"
                                            runat="server" DisplayMode="BulletList" Font-Size="11px" Font-Bold="true" Style="padding-top: 1px;" />
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
                </table>
                <div id="ParentDiv" class="Background" style="visibility: hidden;">
                    <div id="IMGDIV" align="center" valign="middle" style="position: absolute; left: 45%;
                        top: 50%; visibility: hidden; vertical-align: middle; border-style: none; border-color: black;
                        z-index: 40;">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Loading.gif" BorderWidth="0px"
                            AlternateText="Loading"></asp:Image>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <div id="EAFlashUpload_placeholder">
    </div>
    <script type="text/javascript" src="../JavaScript/swfobject.js"></script>
    <script type="text/javascript">

        //        function EAFlashUpload_onUploadEnd() {
        //            //            alert("The upload have been completed!");
        //            window.location = "DataManagement.aspx"
        //        }

        // Handles EAFlashUpload's onMovieLoad event and displays existing loading errors.
        function FlashUploader_onMovieLoad(errors) {
            if (errors != "")

                (errors);
        }

        function EAFlashUpload_onFileLoadEnd(fileObj) {

            if (fileObj.serverResponse.substring(0, 5) == 'False') {

                var url;
                var arr = new Array();
                EAFlashUpload.cancelUpload();
                arr = fileObj.serverResponse.split('^');
                url = 'StandardFormat.aspx?filename=' + arr[1];
                window.open('StandardFormat.aspx', null, 'left=400, top=100, height=150, width= 300, status=no, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no, titlebar= no');
            }
            else {

                EAFlashUpload.removeFiles(0);
            }
        }


        function getProp() {

            EAFlashUpload.uploadFiles();
        }

        function setProp() {

            var propertyObject = new Object();
            propertyObject.name = "Description";
            propertyObject.value = "Personal information";
            propertyObject.isPublic = false;
            // Here we omitted fileId property and custom property will apply to all files. 
            // propertyObject.fileId
            EAFlashUpload.setCustomProperty(propertyObject);
        }

        var params = {
            wmode: "window"
        };

        var attributes = {
            id: "EAFlashUpload",
            name: "EAFlashUpload"
        };

        var flashvars = new Object();
        var url = "FileUploader.aspx";
        flashvars["uploader.uploadUrl"] = url;
        flashvars["viewFile"] = "TableView.swf";
        flashvars["view.progressFile.width"] = "300";
        flashvars["view.progressAll.width"] = "300";
        flashvars["view.filesList.nameColumn.width"] = "300";
        flashvars["queue.allowedExtentions"] = "SDA Files:*.SDA; *.sda; *.Sda; *.SDa; *.sDa; *.sDA; *.sdA";
        flashvars["queue.fileTypeRestrictionMsg"] = "There are files with unallowed type! #REJ_FILES_COUNT# files has been rejected.";
        flashvars["uploader.redirectUrl"] = "DataManagementFileList.aspx";
        flashvars["uploader.redirectTarget"] = "_self";
        flashvars["uploader.redirectMethod"] = "POST";

        swfobject.embedSWF("EAFUpload.swf", "EAFlashUpload_placeholder", "575", "350", "9.0.0", "expressInstall.swf", flashvars, params, attributes);

    </script>
    <script type="text/javascript" src="../JavaScript/swfobject.js"></script>
</asp:Content>
