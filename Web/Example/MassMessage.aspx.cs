using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using KFWeiXin.PublicAccount;
using KFWeiXin.PublicAccount.MassMessage;
using KFWeiXin.PublicAccount.UserManagement;

namespace KFWeiXinWeb.Example
{
    public partial class MassMessage : System.Web.UI.Page
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
        /// 获取用户分组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGetGroup_Click(object sender, EventArgs e)
        {
            string userName = lbPublicAccount.SelectedValue;
            ErrorMessage errorMessage;
            rblGroup.Items.Clear();
            rblGroup.Items.Add(new ListItem("所有分组", ""));
            UserGroup[] groups = KFWeiXin.PublicAccount.UserManagement.UserManagement.GetGroup(userName, out errorMessage);
            if (errorMessage.IsSuccess && groups != null && groups.Length > 0)
            {
                foreach (UserGroup group in groups)
                    rblGroup.Items.Add(new ListItem(string.Format("{0}({1})", group.name, group.count), group.id.ToString()));
                ltrMessage.Text = "获取用户分组成功。";
            }
            else
                ltrMessage.Text = string.Format("获取用户分组失败。{0}", errorMessage);
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGetUser_Click(object sender, EventArgs e)
        {
            string userName = lbPublicAccount.SelectedValue;
            ErrorMessage errorMessage;
            cblUser.Items.Clear();
            string[] openIds = KFWeiXin.PublicAccount.UserManagement.UserManagement.GetUserList(userName);
            if (openIds != null && openIds.Length > 0)
            {
                foreach (string openId in openIds)
                {
                    UserInfo user = KFWeiXin.PublicAccount.UserManagement.UserManagement.GetUserInfo(userName, openId, out errorMessage);
                    if (errorMessage.IsSuccess && user != null)
                        cblUser.Items.Add(new ListItem(user.nickname, openId));
                }
                ltrMessage.Text = "获取用户成功。";
            }
            else
                ltrMessage.Text = "获取用户失败。";
        }

        /// <summary>
        /// 按分组群发消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSendToGroup_Click(object sender, EventArgs e)
        {
            if (rblGroup.SelectedIndex >= 0)
            {
                string userName = lbPublicAccount.SelectedValue;
                ErrorMessage errorMessage;
                bool isToAll = string.IsNullOrWhiteSpace(rblGroup.SelectedValue);
                string groupId = isToAll ? "" : rblGroup.SelectedValue;
                string content = txtContent.Text;
                long msgId = KFWeiXin.PublicAccount.MassMessage.MassMessage.Send(userName, isToAll, groupId, MassMessageTypeEnum.text, content, out errorMessage);
                if (errorMessage.IsSuccess)
                {
                    ltrMessage.Text = "群发消息成功。";
                    rblMassMessage.Items.Add(new ListItem(string.Format("id:{0},text:{1}", msgId, content), msgId.ToString()));
                }
                else
                    ltrMessage.Text = string.Format("群发消息失败。{0}", errorMessage);
            }
        }

        /// <summary>
        /// 按用户群发消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSendToUsers_Click(object sender, EventArgs e)
        {
            if (cblUser.SelectedIndex >= 0)
            {
                string userName = lbPublicAccount.SelectedValue;
                ErrorMessage errorMessage;
                List<string> openIds = new List<string>();
                foreach (ListItem item in cblUser.Items)
                {
                    if (item.Selected)
                        openIds.Add(item.Value);
                }
                string content = txtContent.Text;
                long msgId = KFWeiXin.PublicAccount.MassMessage.MassMessage.Send(userName, openIds, MassMessageTypeEnum.text, content, out errorMessage);
                if (errorMessage.IsSuccess)
                {
                    ltrMessage.Text = "群发消息成功。";
                    rblMassMessage.Items.Add(new ListItem(string.Format("id:{0},text:{1}", msgId, content), msgId.ToString()));
                }
                else
                    ltrMessage.Text = string.Format("群发消息失败。{0}", errorMessage);
            }
        }

        /// <summary>
        /// 预览群发消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            if (cblUser.SelectedIndex >= 0)
            {
                string userName = lbPublicAccount.SelectedValue;
                ErrorMessage errorMessage;
                string openId = cblUser.SelectedValue;
                string content = txtContent.Text;
                long msgId = KFWeiXin.PublicAccount.MassMessage.MassMessage.Preview(userName, openId, MassMessageTypeEnum.text, content, out errorMessage);
                if (errorMessage.IsSuccess)
                {
                    ltrMessage.Text = "预览消息成功。";
                    rblMassMessage.Items.Add(new ListItem(string.Format("id:{0},text:{1}", msgId, content), msgId.ToString()));
                }
                else
                    ltrMessage.Text = string.Format("预览消息失败。{0}", errorMessage);
            }
        }

        /// <summary>
        /// 删除群发消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (rblMassMessage.SelectedIndex >= 0)
            {
                string userName = lbPublicAccount.SelectedValue;
                long msgId = long.Parse(rblMassMessage.SelectedValue);
                ErrorMessage errorMessage = KFWeiXin.PublicAccount.MassMessage.MassMessage.Delete(userName, msgId);
                if (errorMessage.IsSuccess)
                {
                    ltrMessage.Text = "删除消息成功。";
                    rblMassMessage.Items.Remove(rblMassMessage.SelectedItem);
                }
                else
                    ltrMessage.Text = string.Format("删除消息失败。{0}", errorMessage);
            }
        }

        /// <summary>
        /// 查询群发消息状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGetStatus_Click(object sender, EventArgs e)
        {
            if (rblMassMessage.SelectedIndex >= 0)
            {
                string userName = lbPublicAccount.SelectedValue;
                ErrorMessage errorMessage;
                long msgId = long.Parse(rblMassMessage.SelectedValue);
                bool success = KFWeiXin.PublicAccount.MassMessage.MassMessage.GetStatus(userName, msgId, out errorMessage);
                if (errorMessage.IsSuccess)
                    ltrMessage.Text = string.Format("消息群发{0}。", success ? "成功" : "失败");
                else
                    ltrMessage.Text = string.Format("获取消息群发状态失败。{0}", errorMessage);
            }
        }
    }
}