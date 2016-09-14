using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using xrwang.weixin.PublicAccount;

public partial class Example_DataStatistics : System.Web.UI.Page
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
            result = DataStatistics.GetUserSummary(userName, beginDate, endDate, out errorMessage);
        else if (type == "UserCumulate")
            result = DataStatistics.GetUserCumulate(userName, beginDate, endDate, out errorMessage);
        else if (type == "ArticleSummary")
            result = DataStatistics.GetArticleSummary(userName, beginDate, endDate, out errorMessage);
        else if (type == "ArticleTotal")
            result = DataStatistics.GetArticleTotal(userName, beginDate, endDate, out errorMessage);
        else if (type == "UserRead")
            result = DataStatistics.GetUserRead(userName, beginDate, endDate, out errorMessage);
        else if (type == "UserReadHour")
            result = DataStatistics.GetUserReadHour(userName, beginDate, endDate, out errorMessage);
        else if (type == "UserShare")
            result = DataStatistics.GetUserShare(userName, beginDate, endDate, out errorMessage);
        else if (type == "UserShareHour")
            result = DataStatistics.GetUserShareHour(userName, beginDate, endDate, out errorMessage);
        else if (type == "UpstreamMsg")
            result = DataStatistics.GetUpstreamMsg(userName, beginDate, endDate, out errorMessage);
        else if (type == "UpstreamMsgHour")
            result = DataStatistics.GetUpstreamMsgHour(userName, beginDate, endDate, out errorMessage);
        else if (type == "UpstreamMsgWeek")
            result = DataStatistics.GetUpstreamMsgWeek(userName, beginDate, endDate, out errorMessage);
        else if (type == "UpstreamMsgMonth")
            result = DataStatistics.GetUpstreamMsgMonth(userName, beginDate, endDate, out errorMessage);
        else if (type == "UpstreamMsgDist")
            result = DataStatistics.GetUpstreamMsgDist(userName, beginDate, endDate, out errorMessage);
        else if (type == "UpstreamMsgDistWeek")
            result = DataStatistics.GetUpstreamMsgDistWeek(userName, beginDate, endDate, out errorMessage);
        else if (type == "UpstreamMsgDistMonth")
            result = DataStatistics.GetUpstreamMsgDistMonth(userName, beginDate, endDate, out errorMessage);
        else if (type == "InterfaceSummary")
            result = DataStatistics.GetInterfaceSummary(userName, beginDate, endDate, out errorMessage);
        else if (type == "InterfaceSummaryHour")
            result = DataStatistics.GetInterfaceSummaryHour(userName, beginDate, endDate, out errorMessage);
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