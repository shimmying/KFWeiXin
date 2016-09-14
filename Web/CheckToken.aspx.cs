using System;
using System.Web.Security;

namespace KFWeiXinWeb
{
    public partial class CheckToken : System.Web.UI.Page
    {
        public string Token = "MxQjlcmeBfZT0ArvVF9Awfg2zl6w8mtU";

        protected void Page_Load(object sender, EventArgs e)
        {
            string echoStr = Request.QueryString["echoStr"];
            //Log.Debug("Token", "测试输出: echoStr = " + echoStr);
            if (CheckSignature() && !string.IsNullOrEmpty(echoStr))
            {
                Response.Write(echoStr);
                Response.End();
            }
        }

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        private bool CheckSignature()
        {
            string signature = Request.QueryString["signature"];
            string timestamp = Request.QueryString["timestamp"];
            string nonce = Request.QueryString["nonce"];
            //Log.Debug("Token", "测试输出: signature = " + signature);
            //Log.Debug("Token", "测试输出: timestamp = " + timestamp);
            //Log.Debug("Token", "测试输出: nonce = " + nonce);
            string[] arrTmp = { Token, timestamp, nonce };
            Array.Sort(arrTmp);
            string tmpStr = string.Join("", arrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            if (tmpStr != null)
            {
                tmpStr = tmpStr.ToLower();
                return tmpStr == signature;
            }
            return false;
        }
    }
}
