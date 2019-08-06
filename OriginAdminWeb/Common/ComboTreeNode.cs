using System.Collections.Generic;

namespace OriginAdminWeb.Common
{
    /// <summary>
    /// 下拉树，combotree
    /// </summary>
    public class ComboTreeNode
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 页面内容展示字段
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public int? level { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public IList<ComboTreeNode> children = new List<ComboTreeNode>();
    }
}
