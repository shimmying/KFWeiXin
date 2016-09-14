<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="KFWeiXinWeb.Example.UserManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户管理</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>用户管理</h3>
        <h4>提示：<asp:Literal runat="server" ID="ltrMessage" Text="本示例演示用户管理的使用。" /></h4>
        <p>
            请选择公众号：
            <asp:ListBox runat="server" ID="lbPublicAccount" Rows="1"></asp:ListBox>
        </p>
        <p>
            <h5>用户分组</h5>
            <asp:ListBox runat="server" ID="lbGroup" /><br />
            <asp:Button runat="server" ID="btnGetGroup" Text="查询所有分组" OnClick="btnGetGroup_Click" /><br />
            <asp:Button runat="server" ID="btnCreateGroup" Text="创建分组" OnClick="btnCreateGroup_Click" />
            <asp:Button runat="server" ID="btnUpdateGroup" Text="修改分组名" OnClick="btnUpdateGroup_Click" />
            分组名：<asp:TextBox runat="server" ID="txtGroupName" Text="" />
        </p>
        <p>
            <h5>用户</h5>
            <asp:ListBox runat="server" ID="lbUser" /><br />
            <asp:Button runat="server" ID="btnGetUser" Text="查询所有用户" OnClick="btnGetUser_Click" /><br />
            <asp:Button runat="server" ID="btnGetGroupId" Text="查询用户所在分组" OnClick="btnGetGroupId_Click" /><br />
            <asp:Button runat="server" ID="btnMoveUser" Text="移动用户分组" OnClick="btnMoveUser_Click" />
            请在上面的用户分组中选择目的分组。<br />
            <asp:Button runat="server" ID="btnGetUserInfo" Text="获取用户基本信息" OnClick="btnGetUserInfo_Click" /><br />
            <asp:Button runat="server" ID="btnSetRemark" Text="设置用户备注名" OnClick="btnSetRemark_Click" />
            备注名：<asp:TextBox runat="server" ID="txtRemark" Text="" /><br />
        </p>
    </div>
    </form>
</body>
</html>
