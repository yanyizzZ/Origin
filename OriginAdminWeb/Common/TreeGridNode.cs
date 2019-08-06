using System.Collections.Generic;

namespace OriginAdminWeb.Common
{
    /// <summary>
    /// 树表格
    /// </summary>
    public class TreeGridNode
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string DbKey { get; set; }
        /// <summary>
        ///  名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        ///  操作
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        ///  控制器
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        ///  描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///  图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///  按钮样式
        /// </summary>
        public string BtnClass { get; set; }

        /// <summary>
        ///  按钮主键
        /// </summary>
        public string BtnId { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int ModuleLevel { get; set; }

        /// <summary>
        /// 展开收缩状态，closed 收缩
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// 选中状态，checked 选中
        /// </summary>
        public bool CheckState { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public IList<TreeGridNode> children = new List<TreeGridNode>();
    }
}
