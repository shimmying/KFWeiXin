<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetIpAddress.aspx.cs" Inherits="GetIpAddress" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>获取已上报的客户端地址</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="ltrAddress" runat="server" />
    </div>
    </form>
</body>
</html>
