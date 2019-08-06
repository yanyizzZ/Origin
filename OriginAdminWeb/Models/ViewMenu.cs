using System;
using System.Collections.Generic;

namespace OriginAdminWeb.Models
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class ViewMenu
    {
        public ViewMenu()
        {
            menuList = new List<ViewMenu>();
        }
        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleNameEn { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string BtnId { get; set; }
        public string BtnClass { get; set; }
        public Nullable<int> Sort { get; set; }
        public Nullable<int> ModuleLevel { get; set; }
        public short IsDel { get; set; }
        public string ParentId { get; set; }
        public Nullable<int> ModuleType { get; set; }
        public bool? IsDisplay { get; set; }
        public List<ViewMenu> menuList { get; set; }
    }
}