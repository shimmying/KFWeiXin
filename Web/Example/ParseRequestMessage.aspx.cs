using System;
using KFWeiXin.PublicAccount.RequestMessage;

namespace KFWeiXinWeb.Example
{
    public partial class ParseRequestMessage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnParse_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text;
            if (string.IsNullOrWhiteSpace(message))
                txtResult.Text = "消息为空。";
            try
            {
                RequestBaseMessage msg = RequestMessageHelper.Parse(message);
                txtResult.Text = msg != null ? msg.ToString() : "解析消息失败。";
            }
            catch (Exception ex)
            {
                txtResult.Text = string.Format("解析消息发生异常。\r\n源：{0}\r\n描述：{1}\r\n堆栈：{2}",
                    ex.Source, ex.Message, ex.StackTrace);
            }
        }
    }
}