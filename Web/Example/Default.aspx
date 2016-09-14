<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KFWeiXinWeb.Example.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>微信示例</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>微信示例</h3>
        <ul>
            <li><a href="BasicInterface.aspx" target="_blank">基础接口</a></li>
            <li><a href="Subscribe.aspx" target="_blank">关注推送</a></li>
            <li><a href="ParseRequestMessage.aspx" target="_blank">解析请求消息（事件）</a></li>
            <li><a href="CustomerService.aspx" target="_blank">多客服接口</a></li>
            <li><a href="TemplateMessage.aspx" target="_blank">模板消息</a></li>
            <li><a href="MassMessage.aspx" target="_blank">群发消息</a></li>
            <li><a href="Menu.aspx" target="_blank">自定义菜单</a></li>
            <li><a href="UserManagement.aspx" target="_blank">用户管理</a></li>
            <li><a href="DataStatistics.aspx" target="_blank">数据统计接口</a></li>
        </ul>
    </div>
    </form>
</body>
</html>
