<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TemplateMessage.aspx.cs" Inherits="Example_TemplateMessage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>模板消息</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>模板消息</h3>
        <h4>提示：<asp:Literal runat="server" ID="ltrMessage" Text="本示例演示模板消息的使用。" /></h4>
        <p>
            请选择公众号：
            <asp:ListBox runat="server" ID="lbPublicAccount" Rows="1"></asp:ListBox>
        </p>
        <p>
            <h5>设置所属行业</h5>
            <asp:CheckBoxList runat="server" ID="cblIndustry" RepeatDirection="Vertical" RepeatColumns="5" /><br />
            <asp:Button runat="server" ID="btnSetIndustry" Text="设置所属行业" OnClick="btnSetIndustry_Click" />
        </p>
        <p>
            <h5>添加并获得模板id</h5>
            模板编号：<asp:TextBox runat="server" ID="txtTemplateIdShort" Text="TM00015" /><br />
            <asp:Button runat="server" ID="btnGetTemplateId" Text="添加并获得模板id" OnClick="btnGetTemplateId_Click" />
        </p>
        <p>
            <h5>获取已添加的模板</h5>
            模板列表：<br />
            <asp:RadioButtonList runat="server" ID="rblTemplates" RepeatDirection="Vertical" RepeatColumns="1" /><br />
            <asp:Button runat="server" ID="btnGetTemplates" Text="获取已添加的模板" OnClick="btnGetTemplates_Click" />
            <asp:Button runat="server" ID="btnDelete" Text="删除模板" OnClick="btnDelete_Click" />
        </p>
        <p>
            <h5>发送模板消息</h5>
            请选择用户：<br />
            <asp:RadioButtonList runat="server" ID="rblUser" RepeatDirection="Vertical" RepeatColumns="5" /><br />
            <asp:Button runat="server" ID="btnGetUser" Text="获取用户列表" OnClick="btnGetUser_Click" /><br />
            标题：<asp:TextBox runat="server" ID="txtTitle" Text="" /><br />
            称呼：<asp:TextBox runat="server" ID="txtUserName" Text="" /><br />
            <asp:Button runat="server" ID="btnSend" Text="发送模板消息" OnClick="btnSend_Click" />
        </p>
    </div>
    </form>
</body>
</html>
