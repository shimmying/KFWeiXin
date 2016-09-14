using System;
using System.Web.UI.WebControls;
using KFWeiXin.PublicAccount;
using KFWeiXin.PublicAccount.UserManagement;

namespace KFWeiXinWeb.Example
{
    public partial class UserManagement : System.Web.UI.Page
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
        /// 查询所有分组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGetGroup_Click(object sender, EventArgs e)
        {
            lbGroup.Items.Clear();
            string userName = lbPublicAccount.SelectedValue;
            ErrorMessage errorMessage;
            UserGroup[] groups = KFWeiXin.PublicAccount.UserManagement.UserManagement.GetGroup(userName, out errorMessage);
            if (errorMessage.IsSuccess)
            {
                if (groups != null && groups.Length > 0)
                {
                    foreach (UserGroup group in groups)
                        lbGroup.Items.Add(new ListItem(string.Format("{0}({1})", group.name, group.count), group.id.ToString()));
                }
                ltrMessage.Text = string.Format("查询所有分组成功。分组数目：{0}", groups != null ? groups.Length : 0);
            }
            else
                ltrMessage.Text = string.Format("查询所有分组失败。{0}", errorMessage);
        }

        /// <summary>
        /// 创建分组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateGroup_Click(object sender, EventArgs e)
        {
            string userName = lbPublicAccount.SelectedValue;
            ErrorMessage errorMessage;
            int groupId = KFWeiXin.PublicAccount.UserManagement.UserManagement.CreateGroup(userName, txtGroupName.Text, out errorMessage);
            ltrMessage.Text = string.Format("创建用户分组{0}。{1}",
                errorMessage.IsSuccess ? "成功" : "失败",
                errorMessage.IsSuccess ? string.Format("分组ID：{0}", groupId) : errorMessage.ToString());
        }

        /// <summary>
        /// 修改分组名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateGroup_Click(object sender, EventArgs e)
        {
            if (lbGroup.SelectedIndex >= 0)
            {
                string userName = lbPublicAccount.SelectedValue;
                int groupId = int.Parse(lbGroup.SelectedValue);
                ErrorMessage errorMessage = KFWeiXin.PublicAccount.UserManagement.UserManagement.ChangeGroupName(userName, groupId, txtGroupName.Text);
                ltrMessage.Text = string.Format("修改分组名{0}。{1}",
                    errorMessage.IsSuccess ? "成功" : "失败",
                    errorMessage.IsSuccess ? "" : errorMessage.ToString());
            }
        }

        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGetUser_Click(object sender, EventArgs e)
        {
            lbUser.Items.Clear();
            string userName = lbPublicAccount.SelectedValue;
            ErrorMessage errorMessage;
            string[] openIds = KFWeiXin.PublicAccount.UserManagement.UserManagement.GetUserList(userName);
            if (openIds != null && openIds.Length > 0)
            {
                foreach (string openId in openIds)
                {
                    UserInfo user = KFWeiXin.PublicAccount.UserManagement.UserManagement.GetUserInfo(userName, openId, out errorMessage);
                    if (errorMessage.IsSuccess && user != null)
                        lbUser.Items.Add(new ListItem(string.Format("{0}({1})", user.nickname, user.sex), user.openid));
                }
                ltrMessage.Text = string.Format("查询所有用户成功。用户数：{0}", openIds.Length);
            }
            else
                ltrMessage.Text = "查询所有分组失败。";
        }

        /// <summary>
        /// 查询用户所在分组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGetGroupId_Click(object sender, EventArgs e)
        {
            if (lbUser.SelectedIndex >= 0)
            {
                string userName = lbPublicAccount.SelectedValue;
                ErrorMessage errorMessage;
                string openId = lbUser.SelectedValue;
                int groupId = KFWeiXin.PublicAccount.UserManagement.UserManagement.GetGroupId(userName, openId, out errorMessage);
                ltrMessage.Text = string.Format("查询用户所在分组{0}。{1}",
                    errorMessage.IsSuccess ? "成功" : "失败",
                    errorMessage.IsSuccess ? string.Format("分组id：{0}", groupId) : errorMessage.ToString());
            }
        }

        /// <summary>
        /// 移动用户分组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMoveUser_Click(object sender, EventArgs e)
        {
            if (lbUser.SelectedIndex >= 0 && lbGroup.SelectedIndex >= 0)
            {
                string userName = lbPublicAccount.SelectedValue;
                string openId = lbUser.SelectedValue;
                int groupId = int.Parse(lbGroup.SelectedValue);
                ErrorMessage errorMessage = KFWeiXin.PublicAccount.UserManagement.UserManagement.MoveUser(userName, groupId, openId);
                ltrMessage.Text = string.Format("移动用户分组{0}。{1}",
                    errorMessage.IsSuccess ? "成功" : "失败",
                    errorMessage.IsSuccess ? "" : errorMessage.ToString());
            }
        }

        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGetUserInfo_Click(object sender, EventArgs e)
        {
            if (lbUser.SelectedIndex >= 0)
            {
                string userName = lbPublicAccount.SelectedValue;
                ErrorMessage errorMessage;
                string openId = lbUser.SelectedValue;
                UserInfo user = KFWeiXin.PublicAccount.UserManagement.UserManagement.GetUserInfo(userName, openId, out errorMessage);
                ltrMessage.Text = string.Format("获取用户基本信息{0}。{1}",
                    errorMessage.IsSuccess ? "成功" : "失败",
                    errorMessage.IsSuccess ? user.ToString() : errorMessage.ToString());
            }
        }

        /// <summary>
        /// 设置用户备注名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSetRemark_Click(object sender, EventArgs e)
        {
            if (lbUser.SelectedIndex >= 0)
            {
                string userName = lbPublicAccount.SelectedValue;
                string openId = lbUser.SelectedValue;
                ErrorMessage errorMessage = KFWeiXin.PublicAccount.UserManagement.UserManagement.ChangeUserRemark(userName, openId, txtRemark.Text);
                ltrMessage.Text = string.Format("设置用户备注名{0}。{1}",
                    errorMessage.IsSuccess ? "成功" : "失败",
                    errorMessage.IsSuccess ? "" : errorMessage.ToString());
            }
        }
    }
}