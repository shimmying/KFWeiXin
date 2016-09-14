using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using xrwang.PublicLibrary;
using xrwang.weixin.PublicAccount;

public partial class OAuth : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        xrwang.net.Message.Insert(new xrwang.net.Message(xrwang.net.MessageType.Request, Request.RawUrl));
        string code = RequestEx.TryGetQueryString("code");
        string state = RequestEx.TryGetQueryString("state");
        if(!string.IsNullOrWhiteSpace(code))
        {
            ltrResult.Text += "OAuth code:" + code + ",State:" + state;
            string userName = "gh_5dbae931ec49";
            ErrorMessage errorMessage;
            OAuthAccessToken token = OAuthAccessToken.Get(userName, code, out errorMessage);
            if (errorMessage.IsSuccess)
            {
                ltrResult.Text += "<br/>获取网页授权成功。" + token.ToString();
                token = OAuthAccessToken.Refresh(userName, token.refresh_token, out errorMessage);
                if (errorMessage.IsSuccess)
                {
                    ltrResult.Text += "<br/>刷新网页授权成功。" + token.ToString();
                    errorMessage = OAuthAccessToken.CheckValidate(token.access_token, token.openid);
                    if (errorMessage.IsSuccess)
                    {
                        ltrResult.Text += "<br/>校验网页授权成功。";
                        UserInfo user = OAuthAccessToken.GetUserInfo(token.access_token, token.openid, out errorMessage);
                        ltrResult.Text += "<br/>获取用户信息：" + (errorMessage.IsSuccess ? user.nickname + user.sex.Value.ToString("g") + user.headimgurl : errorMessage.ToString());
                    }
                    else
                        ltrResult.Text += "<br/>校验网页授权失败。" + errorMessage.ToString();
                }
                else
                    ltrResult.Text += "<br/>刷新网页授权失败。" + errorMessage.ToString();
            }
            else
                ltrResult.Text += "获取网页授权失败。"+errorMessage.ToString();
        }
    }
}