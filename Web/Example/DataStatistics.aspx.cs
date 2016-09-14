using System;
using System.Text;
using System.Web.UI.WebControls;
using KFWeiXin.PublicAccount;

namespace KFWeiXinWeb.Example
{
    public partial class DataStatistics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitControls();
            }
        }

        /// <summary>
        /// 初始化控件，包括：公众号列表、起止日期
        /// </summary>
        private void InitControls()
        {
            foreach (AccountInfo account in AccountInfoCollection.AccountInfos)
                lbPublicAccount.Items.Add(new ListItem(account.Caption, account.UserName));
            DateTime yesterday = DateTime.Today.AddDays(-1);
            txtBeginDate.Text = yesterday.ToString("yyyy-MM-dd");
            txtEndDate.Text = txtBeginDate.Text;
        }

        /// <summary>
        /// 获取统计数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGet_Click(object sender, EventArgs e)
        {
            object[] result = null;
            ErrorMessage errorMessage = new ErrorMessage(ErrorMessage.ExceptionCode, "未知的统计类型。");
            string userName = lbPublicAccount.SelectedValue;
            string type = lbType.SelectedValue;
            string typeName = lbType.SelectedItem.Text;
            DateTime beginDate = DateTime.Parse(txtBeginDate.Text);
            DateTime endDate = DateTime.Parse(txtEndDate.Text);
            if (type == "UserSummary")
                result = KFWeiXin.PublicAccount.DataStatistics.DataStatistics.GetUserSummary(userName, beginDate, endDate, out errorMessage);
            else if (type == "UserCumulate")
                result = KFWeiXin.PublicAccount.DataStatistics.DataStatistics.GetUserCumulate(userName, beginDate, endDate, out errorMessage);
            else if (type == "ArticleSummary")
                result = KFWeiXin.PublicAccount.DataStatistics.DataStatistics.GetArticleSummary(userName, beginDate, endDate, out errorMessage);
            else if (type == "ArticleTotal")
                result = KFWeiXin.PublicAccount.DataStatistics.DataStatistics.GetArticleTotal(userName, beginDate, endDate, out errorMessage);
            else if (type == "UserRead")
                result = KFWeiXin.PublicAccount.DataStatistics.DataStatistics.GetUserRead(userName, beginDate, endDate, out errorMessage);
            else if (type == "UserReadHour")
                result = KFWeiXin.PublicAccount.DataStatistics.DataStatistics.GetUserReadHour(userName, beginDate, endDate, out errorMessage);
            else if (type == "UserShare")
                result = KFWeiXin.PublicAccount.DataStatistics.DataStatistics.GetUserShare(userName, beginDate, endDate, out errorMessage);
            else if (type == "UserShareHour")
                result = KFWeiXin.PublicAccount.DataStatistics.DataStatistics.GetUserShareHour(userName, beginDate, endDate, out errorMessage);
            else if (type == "UpstreamMsg")
                result = KFWeiXin.PublicAccount.DataStatistics.DataStatistics.GetUpstreamMsg(userName, beginDate, endDate, out errorMessage);
            else if (type == "UpstreamMsgHour")
                result = KFWeiXin.PublicAccount.DataStatistics.DataStatistics.GetUpstreamMsgHour(userName, beginDate, endDate, out errorMessage);
            else if (type == "UpstreamMsgWeek")
                result = KFWeiXin.PublicAccount.DataStatistics.DataStatistics.GetUpstreamMsgWeek(userName, beginDate, endDate, out errorMessage);
            else if (type == "UpstreamMsgMonth")
                result = KFWeiXin.PublicAccount.DataStatistics.DataStatistics.GetUpstreamMsgMonth(userName, beginDate, endDate, out errorMessage);
            else if (type == "UpstreamMsgDist")
                result = KFWeiXin.PublicAccount.DataStatistics.DataStatistics.GetUpstreamMsgDist(userName, beginDate, endDate, out errorMessage);
            else if (type == "UpstreamMsgDistWeek")
                result = KFWeiXin.PublicAccount.DataStatistics.DataStatistics.GetUpstreamMsgDistWeek(userName, beginDate, endDate, out errorMessage);
            else if (type == "UpstreamMsgDistMonth")
                result = KFWeiXin.PublicAccount.DataStatistics.DataStatistics.GetUpstreamMsgDistMonth(userName, beginDate, endDate, out errorMessage);
            else if (type == "InterfaceSummary")
                result = KFWeiXin.PublicAccount.DataStatistics.DataStatistics.GetInterfaceSummary(userName, beginDate, endDate, out errorMessage);
            else if (type == "InterfaceSummaryHour")
                result = KFWeiXin.PublicAccount.DataStatistics.DataStatistics.GetInterfaceSummaryHour(userName, beginDate, endDate, out errorMessage);
            if (errorMessage.IsSuccess)
            {
                StringBuilder sb = new StringBuilder();
                if (result != null && result.Length > 0)
                {
                    foreach (object item in result)
                    {
                        sb.AppendLine(item.ToString());
                        sb.AppendLine("----------------------");
                    }
                }
                txtData.Text = sb.ToString();
                ltrMessage.Text = string.Format("获取{0}成功。", typeName);
            }
            else
            {
                txtData.Text = "";
                ltrMessage.Text = string.Format("获取{0}失败。{1}", typeName, errorMessage);
            }

        }
    }
}