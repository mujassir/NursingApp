<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequestForTypes.aspx.cs" Title="RMC :: Type's Request"
    Inherits="RMC.Web.Users.RequestForTypes" %>

<%@ Register Src="../UserControls/RequestForTypes.ascx" TagName="RequestForTypes"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script type="text/javascript" src="../jquery/jquery-latest.js"></script>

    <script type="text/javascript" src="../jquery/jquery.treeview.js"></script>

    <script type="text/javascript" language="javascript" src="../jquery/jquery.maskedinput-1.2.2.js"></script>

    <script type="text/javascript" language="javascript" src="../jquery/jquery.alphanumeric.pack.js"></script>

    <script type="text/javascript" language="javascript" src="../jquery/jquery.corner.js"></script>

    <script type="text/javascript" language="javascript" src="../JavaScript/Common.js"></script>

    <script src="../jquery/ui.core.js" type="text/javascript"></script>

    <script src="../jquery/ui.accordion.js" type="text/javascript"></script>

    <link href="../CSS/treeview.css" rel="stylesheet" type="text/css" />
    <title>Request Form</title>
    <link rel="stylesheet" href="../CSS/template.css" type="text/css">
    <link href="../CSS/ControlStyles.css" rel="stylesheet" type="text/css" />
    <link href="/favicon.ico" rel="shortcut icon" type="image/x-icon" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:RequestForTypes ID="RequestForTypes1" runat="server" />
    </div>
    </form>
</body>
</html>
