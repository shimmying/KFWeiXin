<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataStatistics.aspx.cs" Inherits="KFWeiXinWeb.Example.DataStatistics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>数据统计接口</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>数据统计接口</h3>
        <h4>提示：<asp:Literal runat="server" ID="ltrMessage" Text="本示例演示数据统计接口的使用。" /></h4>
        <p>
            请选择公众号：
            <asp:ListBox runat="server" ID="lbPublicAccount" Rows="1" />
        </p>
        <p>
            请选择统计方式：
            <asp:ListBox runat="server" ID="lbType" Rows="1">
                <asp:ListItem Text="用户增减数据" Value="UserSummary" />
                <asp:ListItem Text="总用户量" Value="UserCumulate" />
                <asp:ListItem Text="图文群发每日数据" Value="ArticleSummary" />
                <asp:ListItem Text="图文群发总数据" Value="ArticleTotal" />
                <asp:ListItem Text="图文统计数据" Value="UserRead" />
                <asp:ListItem Text="图文统计分时数据" Value="UserReadHour" />
                <asp:ListItem Text="图文分享转发数据" Value="UserShare" />
                <asp:ListItem Text="图文分享转发分时数据" Value="UserShareHour" />
                <asp:ListItem Text="消息发送概况数据" Value="UpstreamMsg" />
                <asp:ListItem Text="消息发送分时数据" Value="UpstreamMsgHour" />
                <asp:ListItem Text="消息发送周数据" Value="UpstreamMsgWeek" />
                <asp:ListItem Text="消息发送月数据" Value="UpstreamMsgMonth" />
                <asp:ListItem Text="消息发送分布数据" Value="UpstreamMsgDist" />
                <asp:ListItem Text="消息发送分布周数据" Value="UpstreamMsgDistWeek" />
                <asp:ListItem Text="消息发送分布月数据" Value="UpstreamMsgDistMonth" />
                <asp:ListItem Text="接口分析数据" Value="InterfaceSummary" />
                <asp:ListItem Text="接口分析分时数据" Value="InterfaceSummaryHour" />
            </asp:ListBox>
            <br />
            请输入起止日期：
            <asp:TextBox runat="server" ID="txtBeginDate" Text="" />
            <asp:TextBox runat="server" ID="txtEndDate" Text="" /><br />
            <asp:TextBox runat="server" ID="txtData" TextMode="MultiLine" Text="" Width="600" Height="400" /><br />
            <asp:Button runat="server" ID="btnGet" Text="获取统计数据" OnClick="btnGet_Click" />
        </p>
    </div>
    </form>
</body>
</html>