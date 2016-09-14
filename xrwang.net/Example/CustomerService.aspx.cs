using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using xrwang.weixin.PublicAccount;

public partial class Example_CustomerService : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitControls();
        }
    }

    /// <summary>
    /// 初始化控件，包括：公众号列表
    /// </summary>
    private void InitControls()
    {
        foreach (AccountInfo account in AccountInfoCollection.AccountInfos)
            lbPublicAccount.Items.Add(new ListItem(account.Caption, account.UserName));
    }

    /// <summary>
    /// 添加客服账号
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string userName = lbPublicAccount.SelectedValue;
        ErrorMessage errorMessage = CustomerService.Add(userName, txtAccount.Text, txtNickname.Text, txtPassword.Text);
        ltrMessage.Text = errorMessage.IsSuccess ? "添加客服账号成功。" : string.Format("添加客服账号失败。{0}", errorMessage);
    }

    /// <summary>
    /// 获取客服列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGetKfList_Click(object sender, EventArgs e)
    {
        lbKfList.Items.Clear();
        string userName = lbPublicAccount.SelectedValue;
        ErrorMessage errorMessage;
        List<CustomerServiceAccount> kfList = CustomerService.GetKfList(userName, out errorMessage);
        if (errorMessage.IsSuccess)
        {
            if (kfList != null && kfList.Count > 0)
            {
                foreach (CustomerServiceAccount kf in kfList)
                    lbKfList.Items.Add(new ListItem(string.Format("工号：{0}，账号：{1}，昵称：{2}", kf.kf_id, kf.kf_account, kf.kf_nick), kf.kf_account));
            }
            ltrMessage.Text = string.Format("获取客服列表成功。客服人数：{0}", kfList != null ? kfList.Count : 0);
        }
        else
            ltrMessage.Text = string.Format("获取客服列表失败。{0}", errorMessage);
    }

    /// <summary>
    /// 修改客服信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (lbKfList.Items != null && lbKfList.SelectedIndex >= 0)
        {
            string userName = lbPublicAccount.SelectedValue;
            string kfAccount = lbKfList.SelectedValue;
            ErrorMessage errorMessage = CustomerService.Update(userName, kfAccount, txtNickname2.Text, txtPassword2.Text);
            ltrMessage.Text = errorMessage.IsSuccess ? "修改客服信息成功。" : string.Format("修改客服信息失败。{0}", errorMessage);
        }
    }

    /// <summary>
    /// 删除客服账号
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (lbKfList.Items != null && lbKfList.SelectedIndex >= 0)
        {
            string userName = lbPublicAccount.SelectedValue;
            string kfAccount = lbKfList.SelectedValue;
            ErrorMessage errorMessage = CustomerService.Delete(userName, kfAccount);
            ltrMessage.Text = errorMessage.IsSuccess ? "删除客服账号成功。" : string.Format("删除客服账号失败。{0}", errorMessage);
        }
    }

    /// <summary>
    /// 上传客服头像
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (lbKfList.Items != null && lbKfList.SelectedIndex >= 0)
        {
            string userName = lbPublicAccount.SelectedValue;
            string kfAccount = lbKfList.SelectedValue;
            ErrorMessage errorMessage = CustomerService.UploadHeadImage(userName, kfAccount, fileUpload.FileBytes);
            ltrMessage.Text = errorMessage.IsSuccess ? "上传客服头像成功。" : string.Format("上传客服头像失败。{0}", errorMessage);
        }
    }

    /// <summary>
    /// 获取在线客服接待信息列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGetOnlineKfList_Click(object sender, EventArgs e)
    {
        lbOnlineKfList.Items.Clear();
        string userName = lbPublicAccount.SelectedValue;
        ErrorMessage errorMessage;
        List<CustomerServiceOnlineInfo> onlineList = CustomerService.GetOnlineKfList(userName, out errorMessage);
        if (errorMessage.IsSuccess)
        {
            if (onlineList != null && onlineList.Count > 0)
            {
                foreach (CustomerServiceOnlineInfo online in onlineList)
                    lbOnlineKfList.Items.Add(
                        new ListItem(string.Format("工号：{0}，账号：{1}，在线状态：{2}，最大自动接入数：{3}，当前接待的会话数：{4}",
                            online.kf_id, online.kf_account, online.GetOnlineStatus(), online.auto_accept, online.accepted_case),
                        online.kf_account));
            }
            ltrMessage.Text = string.Format("获取在线客服接待信息成功。在线客服人数：{0}", onlineList != null ? onlineList.Count : 0);
        }
        else
            ltrMessage.Text = string.Format("获取在线客服接待信息失败。{0}", errorMessage);
    }

    /// <summary>
    /// 获取未接入的客户列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGetWaitCase_Click(object sender, EventArgs e)
    {
        lbWaitCase.Items.Clear();
        ErrorMessage errorMessage;
        string userName = lbPublicAccount.SelectedValue;
        WaitCaseSession sessions = CustomerService.GetWaitCase(userName, out errorMessage);
        if (errorMessage.IsSuccess)
        {
            if (sessions != null)
            {
                ltrMessage.Text = string.Format("获取未接入的客户列表成功。未接入客户总数：{0}", sessions.count);
                if (sessions.waitcaselist != null && sessions.waitcaselist.Length > 0)
                {
                    foreach (WaitCase waitcase in sessions.waitcaselist)
                        lbWaitCase.Items.Add(new ListItem(waitcase.ToString(), waitcase.openid));
                }
            }
            else
                ltrMessage.Text = "获取未结果的客户列表成功，没有未接入的客户。";
        }
        else
            ltrMessage.Text = string.Format("获取未接入的客户列表失败。{0}", errorMessage);
    }

    /// <summary>
    /// 创建会话
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbCreateSession_Click(object sender, EventArgs e)
    {
        string userName = lbPublicAccount.SelectedValue;
        string openId = lbWaitCase.SelectedIndex >= 0 ? lbWaitCase.SelectedValue : "";
        string kfAccount = lbOnlineKfList.SelectedIndex >= 0 ? lbOnlineKfList.SelectedValue : "";
        ErrorMessage errorMessage = CustomerService.Create(userName, openId, kfAccount);
        ltrMessage.Text = string.Format("创建会话{0}。{1}",
            errorMessage.IsSuccess ? "成功" : "失败",
            errorMessage.IsSuccess ? "" : errorMessage.ToString());
    }

    /// <summary>
    /// 获取客服接待的会话列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGetSessionList_Click(object sender, EventArgs e)
    {
        lbSession.Items.Clear();
        string userName = lbPublicAccount.SelectedValue;
        string kfAccount = lbOnlineKfList.SelectedIndex >= 0 ? lbOnlineKfList.SelectedValue : "";
        ErrorMessage errorMessage;
        CustomerServiceSession[] sessions = CustomerService.GetSessionList(userName, kfAccount, out errorMessage);
        if (errorMessage.IsSuccess)
        {
            ltrMessage.Text = string.Format("获取客服接待的会话列表成功。会话数：{0}", sessions != null ? sessions.Length : 0);
            if (sessions != null && sessions.Length > 0)
            {
                foreach (CustomerServiceSession session in sessions)
                    lbSession.Items.Add(new ListItem(session.ToString(), session.openid));
            }
        }
        else
            ltrMessage.Text = string.Format("获取客服接待的会话列表失败。{0}", errorMessage);
    }

    /// <summary>
    /// 获取客户的会话状态
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGetSession_Click(object sender, EventArgs e)
    {
        string userName = lbPublicAccount.SelectedValue;
        string openId = lbSession.SelectedIndex >= 0 ? lbSession.SelectedValue : "";
        ErrorMessage errorMessage;
        CustomerSession session = CustomerService.GetSession(userName, openId, out errorMessage);
        if (errorMessage.IsSuccess)
            ltrMessage.Text = string.Format("获取客户的会话状态成功。{0}", session != null ? session.ToString() : "");
        else
            ltrMessage.Text = string.Format("获取客户的会话状态失败。{0}", errorMessage);
    }

    /// <summary>
    /// 关闭会话
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCloseSession_Click(object sender, EventArgs e)
    {
        string userName = lbPublicAccount.SelectedValue;
        string openId = lbSession.SelectedIndex >= 0 ? lbSession.SelectedValue : "";
        string kfAccount = lbOnlineKfList.SelectedIndex >= 0 ? lbOnlineKfList.SelectedValue : "";
        ErrorMessage errorMessage = CustomerService.Close(userName, openId, kfAccount);
        ltrMessage.Text = string.Format("关闭会话{0}。{1}",
            errorMessage.IsSuccess ? "成功" : "失败",
            errorMessage.IsSuccess ? "" : errorMessage.ToString());
    }
}