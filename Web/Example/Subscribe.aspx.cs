using System;
using System.IO;
using System.Text;
using System.Xml;

namespace KFWeiXinWeb.Example
{



    public partial class subscribe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
                string weixin = "23";
              //  weixin = PostInput();//获取xml数据
                if (!string.IsNullOrEmpty(weixin))
                {
                    ResponseMsg(weixin);//调用消息适配器
                }
            
        }

        #region 获取post请求数据
        /// <summary>
        /// 获取post请求数据
        /// </summary>
        /// <returns></returns>
        private string PostInput()
        {
            Stream s = System.Web.HttpContext.Current.Request.InputStream;
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            return Encoding.UTF8.GetString(b);
        }
        #endregion

        #region 消息类型适配器
        private void ResponseMsg(string weixin)// 服务器响应微信请求
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(weixin);//读取xml字符串
            XmlElement root = doc.DocumentElement;
            ExmlMsg xmlMsg = GetExmlMsg(root);
            //XmlNode MsgType = root.SelectSingleNode("MsgType");
            //string messageType = MsgType.InnerText;
            string messageType = xmlMsg.MsgType;//获取收到的消息类型。文本(text)，图片(image)，语音等。


            try
            {

                switch (messageType)
                {
                    //当消息为文本时
                    case "text":
                        textCase(xmlMsg);
                        break;
                    case "event":
                        if (!string.IsNullOrEmpty(xmlMsg.EventName) && xmlMsg.EventName.Trim() == "subscribe")
                        {
                            //刚关注时的时间，用于欢迎词  
                            int nowtime = ConvertDateTimeInt(DateTime.Now);
                            string msg = "你要关注我，我有什么办法。随便发点什么试试吧~~~";
                            string resxml = "<xml><ToUserName><![CDATA[" + xmlMsg.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + xmlMsg.ToUserName + "]]></FromUserName><CreateTime>" + nowtime + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + msg + "]]></Content><FuncFlag>0</FuncFlag></xml>";
                            Response.Write(resxml);
                        }
                        break;
                    case "image":
                        break;
                    case "voice":
                        break;
                    case "vedio":
                        break;
                    case "location":
                        break;
                    case "link":
                        break;
                    default:
                        break;
                }
                Response.End();
            }
            catch (Exception)
            {

            }
        }
        #endregion

        private string getText(ExmlMsg xmlMsg)
        {
            string con = xmlMsg.Content.Trim();

            System.Text.StringBuilder retsb = new StringBuilder(200);
            retsb.Append("这里放你的业务逻辑");
            retsb.Append("接收到的消息：" + xmlMsg.Content);
            retsb.Append("用户的OPEANID：" + xmlMsg.FromUserName);

            return retsb.ToString();
        }


        #region 操作文本消息 + void textCase(XmlElement root)
        private void textCase(ExmlMsg xmlMsg)
        {
            int nowtime = ConvertDateTimeInt(DateTime.Now);
            string msg = "";
            msg = getText(xmlMsg);
            string resxml = "<xml><ToUserName><![CDATA[" + xmlMsg.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + xmlMsg.ToUserName + "]]></FromUserName><CreateTime>" + nowtime + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + msg + "]]></Content><FuncFlag>0</FuncFlag></xml>";
            Response.Write(resxml);

        }
        #endregion

        #region 将datetime.now 转换为 int类型的秒
        /// <summary>
        /// datetime转换为unixtime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        private int converDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// unix时间转换为datetime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        private DateTime UnixTimeToTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        #endregion

        #region 接收的消息实体类 以及 填充方法
        private class ExmlMsg
        {
            /// <summary>
            /// 本公众账号
            /// </summary>
            public string ToUserName { get; set; }
            /// <summary>
            /// 用户账号
            /// </summary>
            public string FromUserName { get; set; }
            /// <summary>
            /// 发送时间戳
            /// </summary>
            public string CreateTime { get; set; }
            /// <summary>
            /// 发送的文本内容
            /// </summary>
            public string Content { get; set; }
            /// <summary>
            /// 消息的类型
            /// </summary>
            public string MsgType { get; set; }
            /// <summary>
            /// 事件名称
            /// </summary>
            public string EventName { get; set; }

        }

        private ExmlMsg GetExmlMsg(XmlElement root)
        {
            ExmlMsg xmlMsg = new ExmlMsg()
            {
                FromUserName = root.SelectSingleNode("FromUserName").InnerText,
                ToUserName = root.SelectSingleNode("ToUserName").InnerText,
                CreateTime = root.SelectSingleNode("CreateTime").InnerText,
                MsgType = root.SelectSingleNode("MsgType").InnerText,
            };
            if (xmlMsg.MsgType.Trim().ToLower() == "text")
            {
                xmlMsg.Content = root.SelectSingleNode("Content").InnerText;
            }
            else if (xmlMsg.MsgType.Trim().ToLower() == "event")
            {
                xmlMsg.EventName = root.SelectSingleNode("Event").InnerText;
            }
            return xmlMsg;
        }
        #endregion
    }
}