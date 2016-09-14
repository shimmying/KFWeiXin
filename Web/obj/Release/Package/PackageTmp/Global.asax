<%@ Application Language="C#" %>
<%@ Import Namespace="KFWeiXin.PublicAccount" %>
<%@ Import Namespace="KFWeiXin.PublicLibrary" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // 在应用程序启动时运行的代码
        //填写默认的SQL SERVER数据库连接字符串
        DataAccess.connectionString = "Data Source=mssql.sql126.cdncenter.net;Initial Catalog=sq_kefengwlb;Persist Security Info=True;User ID=sq_kefengwlb;Password=kefeng2016";
        //加入测试公众号
        //加入xrwang公众号
        AccountInfoCollection.SetAccountInfo(new AccountInfo("gh_ee1453182f2c", "wx975e0bcb435408a1", "df38f965d301d904ef27705c7f9c33f1", "MxQjlcmeBfZT0ArvVF9Awfg2zl6w8mtU", "EncodingAesKey", "测试公众号"));
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  在应用程序关闭时运行的代码

    }

    void Application_Error(object sender, EventArgs e)
    {
        if (Server.GetLastError() == null)
            return;
        Exception exp = Server.GetLastError().GetBaseException();
        HandleException(exp);
        if (exp.InnerException != null)
            HandleException(exp.InnerException);
    }

    /// <summary>
    /// 处理异常
    /// </summary>
    /// <param name="e"></param>
    private void HandleException(Exception e)
    {
        string err = string.Format("捕获未处理的异常。\r\n出错页面：{0}\r\n出错方法：{1}\r\n错误源：{2}\r\n错误提示：{3}\r\n堆栈：{4}",
            Request != null ? Request.RawUrl.ToString() : "", e.TargetSite, e.Source, e.Message, e.StackTrace);
        Message message = new Message(MessageType.Exception, err);
        Message.Insert(message);
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // 在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e) 
    {
        // 在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer
        // 或 SQLServer，则不引发该事件。

    }
       
</script>
