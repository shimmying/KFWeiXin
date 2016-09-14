<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerService.aspx.cs" Inherits="Example_CustomerService" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>多客服接口</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>多客服接口</h3>
        <h4>提示：<asp:Literal runat="server" ID="ltrMessage" Text="多客服接口未经充分测试。（我的个人订阅号没有多客服功能；测试账号没有对应的公众微信号，不能添加客服。）" /></h4>
        <p>
            请选择公众号：
            <asp:ListBox runat="server" ID="lbPublicAccount" Rows="1"></asp:ListBox>
        </p>
        <p>
            <b>添加客服账号</b><br />
            账号：<asp:TextBox runat="server" ID="txtAccount" Text="" /><br />
            昵称：<asp:TextBox runat="server" ID="txtNickname" Text="" /><br />
            密码：<asp:TextBox runat="server" ID="txtPassword" Text="" TextMode="Password" /><br />
            <asp:Button runat="server" ID="btnAdd" Text="添加客服账号" OnClick="btnAdd_Click" />
        </p>
        <p>
            <b>客服列表</b><br />
            <asp:ListBox runat="server" ID="lbKfList" /><br />
            <asp:Button runat="server" ID="btnGetKfList" Text="获取客服列表" OnClick="btnGetKfList_Click" /><br />
            <asp:Button runat="server" ID="btnUpdate" Text="修改客服信息" OnClick="btnUpdate_Click" />
            昵称：<asp:TextBox runat="server" ID="txtNickname2" Text="" />
            密码：<asp:TextBox runat="server" ID="txtPassword2" Text="" TextMode="Password" /><br />
            <asp:Button runat="server" ID="btnDelete" Text="删除选中的客服" OnClick="btnDelete_Click" /><br />
            <asp:Button runat="server" ID="btnUpload" Text="上传客服头像" OnClick="btnUpload_Click" />
            <asp:FileUpload runat="server" ID="fileUpload" /><br />
            <asp:Button runat="server" ID="btnGetSessionList" Text="获取客服的会话列表" OnClick="btnGetSessionList_Click" />
        </p>
        <p>
            <b>客服的会话列表</b><br />
            <asp:ListBox runat="server" ID="lbSession" /><br />
            <asp:Button runat="server" ID="btnGetSession" Text="获取客户的会话状态" OnClick="btnGetSession_Click" /><br />
            <asp:Button runat="server" ID="btnCloseSession" Text="关闭会话" OnClick="btnCloseSession_Click" />
        </p>
        <p>
            <b>未接入的客户列表</b><br />
            <asp:ListBox runat="server" ID="lbWaitCase" /><br />
            <asp:Button runat="server" ID="btnGetWaitCase" Text="获取未接入的客服列表" OnClick="btnGetWaitCase_Click" /><br />
            <asp:Button runat="server" ID="lbCreateSession" Text="接入会话" OnClick="lbCreateSession_Click" /><br />
        </p>
        <p>
            <b>获取在线客服接待信息</b><br />
            <asp:ListBox runat="server" ID="lbOnlineKfList" /><br />
            <asp:Button runat="server" ID="btnGetOnlineKfList" Text="获取在线客服接待信息" OnClick="btnGetOnlineKfList_Click" />
        </p>
    </div>
    </form>
</body>
</html>
