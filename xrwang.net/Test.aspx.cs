using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Text;
using xrwang.weixin.PublicAccount;
using System.Dynamic;

public partial class Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string menus="{\"is_menu_open\": 1, \"selfmenu_info\": { \"button\": [ { \"type\": \"news\",  \"name\": \"news\", \"news_info\": {  \"list\": [ {\"title\": \"MULTI_NEWS\", \"author\": \"JIMZHENG\", \"digest\": \"text\", \"show_cover\": 0, \"cover_url\": \"http://mmbiz.qpic.cn/mmbiz/GE7et87vE9vicuCibqXsX9GPPLuEtBfXfK0HKuBIa1A1cypS0uY1wickv70iaY1gf3I1DTszuJoS3lAVLvhTcm9sDA/0\", \"content_url\": \"http://mp.weixin.qq.com/s?biz=MjM5ODUwNTM3Ng==&mid=204013432&idx=1&sn=80ce6d9abcb832237bf86c87e50fda15#rd\", \"source_url\": \"\"}, { \"title\": \"MULTI_NEWS1\", \"author\": \"JIMZHENG\", \"digest\": \"MULTI_NEWS1\",  \"show_cover\": 1, \"cover_url\": \"http://mmbiz.qpic.cn/mmbiz/GE7et87vE9vicuCibqXsX9GPPLuEtBfXfKnmnpXYgWmQD5gXUrEApIYBCgvh2yHsu3ic3anDUGtUCHwjiaEC5bicd7A/0\", \"source_url\": \"\"}]}}]}}";
        //BaseMenu[] menuss = MenuHelper.Parse(menus);
        SendTemplateMessage();
    }

    private void SendTemplateMessage()
    {
        //string user_openid = "o7pNts9uwvBYCgZZ79AiJig6BMUs";
        string user_openid = "ojyxtuImDUYDw1gtvsMHt7ZpxKTI";
        dynamic postData = new ExpandoObject();
        postData.touser = user_openid;
        postData.template_id = "rjzbNBpcZDbxXfS1H1TfavoQk8qkpkFb9MGTEOnE5ng";
        postData.url = string.Empty;
        postData.topcolor = "#FF0000";
        postData.data = new ExpandoObject();
        var data = new[]
        {
        new Tuple<string, string, string>("first", "职位审核通知", "#FF0000"),
        new Tuple<string, string, string>("keyword1", "10分钟", "#FF0000"),
        new Tuple<string, string, string>("keyword2", "上海-北京", "#FF0000"),
        new Tuple<string, string, string>("remark", "2015/7/31 14:36:32", "#FF0000"),
        };
        var dataDict = (IDictionary<string, object>)postData.data;
        foreach (var item in data)
        {
            dataDict.Add(item.Item1, new { value = item.Item2, color = item.Item3 });
        }

        string json = JsonConvert.SerializeObject(postData);
        //string access_token = "sM4AOVdWfPE4DxkXGEs8VF3-q3cmyFx--dxRR1JWqeMB7xj1nKTKXfDUvHD-Z-KcY4KSBi00NysiCtfveBkrfw";
        string access_token = "T-J_SSIcUj1cFGDDs7FgG6sAul_U45UPiA5MJHGdaK6j2YVYjbzH73PDlg53K_-Wv2C2LevbT9isArqAsRce2zc90HB5VggQZlwtyaI9lYUHCQiAAAUPD";
        //string url = "https://api.weixin.qq.com/cgi-bin/template/api_add_template?access_token=" + access_token;
        string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + access_token;
        string responseContent;
        if (!HttpHelper.Request(url, out responseContent, "POST", json))
            ltrMsg.Text = "提交到服务器错误。";
        else
            ltrMsg.Text = /*GetResponseData(json, url);*/responseContent;
    }

    /// <summary>
    /// 发送模板消息
    /// </summary>
    /// <param name="token">access token</param>
    /// <param name="userOpenId">用户的openid</param>
    /// <param name="templateId">模板id</param>
    /// <param name="data">模板数据</param>
    /// <param name="url">模板详细情况地址</param>
    /// <param name="topColor">模板顶部的颜色</param>
    /// <returns>返回发送模板消息的结果</returns>
    private string SendTemplateMessage(string token, string userOpenId, string templateId, Tuple<string, string, string>[] data,
        string url = null, string topColor = "#FF0000")
    {
        //检查参数
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentNullException("token", "token不能为空。");
        if (string.IsNullOrWhiteSpace(userOpenId))
            throw new ArgumentNullException("userOpenId", "data不能为空。");
        if (data == null || data.Length == 0)
            throw new ArgumentNullException("data", "data不能为空。");
        //构造json数据
        dynamic postData = new ExpandoObject();
        postData.touser = userOpenId;
        postData.template_id = templateId;
        postData.url = url ?? "";
        postData.topcolor = topColor;
        postData.data = new ExpandoObject();
        var dataDict = (IDictionary<string, object>)postData.data;
        foreach (var item in data)
        {
            dataDict.Add(item.Item1, new { value = item.Item2, color = item.Item3 });
        }
        string json = JsonConvert.SerializeObject(postData);
        //发送并返回结果
        string urlServer = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + token;
        string responseContent;
        if (!HttpHelper.Request(url, out responseContent, "POST", json))
        {
            //提交到服务器错误。可以在此加入错误处理。
        }
        else
        {
            //提交到服务器成功。可以在此加入成功处理。
        }
        return responseContent;
    }

    /*public string GetResponseData(string JSONData, string Url)
    {
        Response.Write(JSONData + "<br>");
        Response.Write(Url + "<br>");
        byte[] bytes = Encoding.UTF8.GetBytes(JSONData);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
        request.Method = "POST";
        request.ContentLength = bytes.Length;
        request.ContentType = "text/xml";
        Stream reqstream = request.GetRequestStream();
        reqstream.Write(bytes, 0, bytes.Length);

        //声明一个HttpWebRequest请求  
        request.Timeout = 90000;
        //设置连接超时时间  
        request.Headers.Set("Pragma", "no-cache");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream streamReceive = response.GetResponseStream();
        Encoding encoding = Encoding.UTF8;

        StreamReader streamReader = new StreamReader(streamReceive, encoding);
        string strResult = streamReader.ReadToEnd();
        streamReceive.Dispose();
        streamReader.Dispose();

        return strResult;
    }*/
}