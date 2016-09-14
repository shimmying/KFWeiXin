﻿namespace KFWeiXin.PublicAccount.Semantic.Reply
{
    /// <summary>
    /// 车载指令的操作值：OPEN(打开)，CLOSE(关闭)，MIN(最小)，MAX(最大)，UP(变大)，DOWN(变小)
    /// </summary>
    public enum CarInstructionOperatorEnum
    {
        /// <summary>
        /// 打开
        /// </summary>
        OPEN,
        /// <summary>
        /// 关闭
        /// </summary>
        CLOSE,
        /// <summary>
        /// 最小
        /// </summary>
        MIN,
        /// <summary>
        /// 最大
        /// </summary>
        MAX,
        /// <summary>
        /// 变大
        /// </summary>
        UP,
        /// <summary>
        /// 变小
        /// </summary>
        DOWN
    }
}