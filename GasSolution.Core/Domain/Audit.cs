using System.ComponentModel;

namespace GasSolution.Domain
{
    /// <summary>
    /// 审核状态
    /// </summary>
    public enum Audit : int
    {
        /// <summary>
        /// 未处理
        /// </summary>
        [Description("未处理")]
        None = 0,
        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("审核通过")]
        Passed = 1,
        /// <summary>
        /// 审核失败
        /// </summary>
        [Description("审核失败")]
        NotPassed = 2,

    }
}
