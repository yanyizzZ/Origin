using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OriginAdminWeb.Common
{
    /// <summary>
    /// 树类   用于Bootstrap-Treeview
    /// </summary>
    public class NodeInfo
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
        /// 图标
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public int? level { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public IList<NodeInfo> nodes = new List<NodeInfo>();
    }
}