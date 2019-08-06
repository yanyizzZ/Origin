using Origin.Model;
namespace OriginAdminWeb.Models
{
    /// <summary>
    /// 资源模块页面模型
    /// </summary>
    public class ModuleViewMoudle
    {
        private SysModule _sysModule = null;
        /// <summary>
        /// 资源模块
        /// </summary>
        public SysModule SysModule
        {
            get { return _sysModule ?? new SysModule(); }
            set { _sysModule = value; }
        }
        /// <summary>
        /// 父级模块名称
        /// </summary>
        public string ParentModuleName { get; set; }
        /// <summary>
        /// 按钮大小
        /// </summary>
        public string BtnSize { get; set; }
        public string BtnClass { get; set; }
    }
}