using System;
using System.Web.UI.WebControls;
using KFWeiXin.PublicAccount;
using KFWeiXin.PublicAccount.Meterial;
using KFWeiXin.PublicAccount.MultiMedia;

namespace KFWeiXinWeb.Example
{
    public partial class Meterial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitControls();
            }
        }

        /// <summary>
        /// 初始化控件，包括：公众号列表、多媒体类型列表
        /// </summary>
        private void InitControls()
        {
            foreach (AccountInfo account in AccountInfoCollection.AccountInfos)
                lbPublicAccount.Items.Add(new ListItem(account.Caption, account.UserName));
            foreach (string name in Enum.GetNames(typeof(MultiMediaTypeEnum)))
                lbMultiMediaType.Items.Add(name);
        }

        /// <summary>
        /// 新增素材
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string userName = lbPublicAccount.SelectedValue;
            MultiMediaTypeEnum type = (MultiMediaTypeEnum)Enum.Parse(typeof(MultiMediaTypeEnum), lbMultiMediaType.SelectedValue);
            ErrorMessage errorMessage;
            string mediaId = KFWeiXin.PublicAccount.Meterial.Meterial.Add(userName, type, fileUpload.FileName, fileUpload.FileBytes, out errorMessage);
            if (errorMessage.IsSuccess)
                ltrMessage.Text = string.Format("新增永久{0:g}素材成功。素材媒体id：{1}", type, mediaId);
            else
                ltrMessage.Text = string.Format("新增永久{0:g}素材失败。{1}", type, errorMessage);
        }

        /// <summary>
        /// 获取素材总数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGetCount_Click(object sender, EventArgs e)
        {
            string userName = lbPublicAccount.SelectedValue;
            ErrorMessage errorMessage;
            MeterialCount count = KFWeiXin.PublicAccount.Meterial.Meterial.GetMeterialCount(userName, out errorMessage);
            ltrMessage.Text = string.Format("获取素材总数{0}。{1}",
                errorMessage.IsSuccess ? "成功" : "失败",
                errorMessage.IsSuccess ? count.ToString() : errorMessage.ToString());
        }

        /// <summary>
        /// 批量获取素材
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBatchGet_Click(object sender, EventArgs e)
        {
            ltrMessage.Text = "";
            lbMeterial.Items.Clear();
            string userName = lbPublicAccount.SelectedValue;
            ErrorMessage errorMessage;
            int count = 20;
            foreach (MultiMediaTypeEnum type in Enum.GetValues(typeof(MultiMediaTypeEnum)))
            {
                int offset = 0;
                BatchMeterial bm = null;
                while (true)
                {
                    bm = KFWeiXin.PublicAccount.Meterial.Meterial.BatchGet(userName, type, offset, count, out errorMessage);
                    if (bm == null || !errorMessage.IsSuccess)
                    {
                        ltrMessage.Text = string.Format("批量获取素材错误。{0}", errorMessage);
                        break;
                    }
                    offset += count;
                    if (bm.Item != null && bm.Item.Length > 0)
                    {
                        foreach (MeterialItem mi in bm.Item)
                            lbMeterial.Items.Add(new ListItem(mi.ToString(), mi.MediaId));
                    }
                }
            }
            ltrMessage.Text += string.Format("共获取到{0}个素材。", lbMeterial.Items.Count);
        }

        /// <summary>
        /// 删除永久素材
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string userName = lbPublicAccount.SelectedValue;
            string mediaId = lbMeterial.SelectedValue;
            ErrorMessage errorMessage = KFWeiXin.PublicAccount.Meterial.Meterial.Delete(userName, mediaId);
            ltrMessage.Text = string.Format("删除永久素材（媒体id：{0}）{1}。", mediaId, errorMessage.IsSuccess ? "成功" : "失败");
        }

        /// <summary>
        /// 获取素材
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGet_Click(object sender, EventArgs e)
        {
            string userName = lbPublicAccount.SelectedValue;
            string mediaId = lbMeterial.SelectedValue;
            ErrorMessage errorMessage;
            byte[] bytes = KFWeiXin.PublicAccount.Meterial.Meterial.Get(userName, mediaId, out errorMessage);
            if (bytes != null && bytes.Length > 0 && errorMessage.IsSuccess)
            {
                Response.Clear();
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            else
                ltrMessage.Text = string.Format("获取素材失败。{0}", errorMessage);
        }
    }
}