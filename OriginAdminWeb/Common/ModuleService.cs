using Origin.Common;
using Origin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace OriginAdminWeb.Common
{
    /// <summary>
    /// 资源模块服务类
    /// </summary>
    public class ModuleServer
    {
        private readonly OriginEntities _model = new OriginEntities();

        #region 获取全部模块资源，树表格数据
        /// <summary>
        /// 获取全部模块资源，树表格数据
        /// </summary>
        /// <returns></returns>
        public string GetModuleToTreeGrid()
        {
            var rootList = _model.SysModule.Where(c => c.ParentId =="0" && c.IsDel == 0).OrderBy(c => c.Sort).ToList();
            IList<TreeGridNode> nodeList = new List<TreeGridNode>();
            foreach (SysModule module in rootList)
            {
                TreeGridNode rootNode = new TreeGridNode
                {
                    DbKey = module.ModuleId.ToString(),
                    ModuleName = module.ModuleName,
                    ControllerName = module.ControllerName,
                    ActionName = module.ActionName,
                    Icon = module.Icon,
                    BtnClass = module.BtnClass,
                    BtnId = module.BtnId,
                    state = "closed"
                };
                int num = _model.SysModule.Count(c => c.ParentId == module.ModuleId);
                if (num > 0)
                {
                    GetChildren(rootNode);
                }
                else
                {
                    rootNode.children = null;
                }
                nodeList.Add(rootNode);
            }
            return nodeList.ToJson();
        }

        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="parentModule"></param>
        public void GetChildren(TreeGridNode parentModule)
        {
            //int key = Int32.Parse(parentModule.DbKey);
            string key = parentModule.DbKey;
            var childrenModule = _model.SysModule.Where(c => c.ParentId == key).OrderBy(c => c.Sort).ToList();
            TreeGridNode childrenNode = null;
            foreach (SysModule module in childrenModule)
            {
                childrenNode = new TreeGridNode()
                {
                    DbKey = module.ModuleId.ToString(),
                    ModuleName = module.ModuleName,
                    ControllerName = module.ControllerName,
                    ActionName = module.ActionName,
                    Icon = module.Icon,
                    BtnClass = module.BtnClass,
                    BtnId = module.BtnId,
                    state = "closed"
                };
                int num = _model.SysModule.Count(c => c.ParentId == module.ModuleId);
                if (num > 0)
                {
                    GetChildren(childrenNode);
                }
                else
                {
                    childrenNode.children = null;
                }
                parentModule.children.Add(childrenNode);
            }
        }
        #endregion

        #region 获取根模块资源，树表格数据（列表页面）
        /// <summary>
        /// 获取根模块资源，树表格数据
        /// </summary>
        /// <returns></returns>
        public string GetRootModuleToTreeGrid()
        {
            var rootList = _model.SysModule.Where(c => c.ParentId == "0" && c.IsDel == 0).OrderBy(c => c.Sort).ToList();
            IList<TreeGridNode> nodeList = new List<TreeGridNode>();
            rootList.Each(c =>
            {
                TreeGridNode rootNode = new TreeGridNode();
                rootNode.DbKey = c.ModuleId.ToString();
                rootNode.ModuleName = c.ModuleName;
                rootNode.ControllerName = c.ControllerName;
                rootNode.ActionName = c.ActionName;
                rootNode.Icon = c.Icon;
                rootNode.BtnClass = c.BtnClass;
                rootNode.BtnId = c.BtnId;
                if (HasChildren(c.ModuleId))
                {
                    rootNode.state = "closed";
                }
                nodeList.Add(rootNode);
            });
            return nodeList.ToJson();
        }

        /// <summary>
        /// 获取下级模块资源，树表格数据
        /// </summary>
        /// <param name="id">下级模块资源主键</param>
        /// <returns></returns>
        public string GetNodeToTreeGrid(string id)
        {
            //int parentId = Int32.Parse(id);
            var moduleList = _model.SysModule.Where(c => c.ParentId == id && c.IsDel == 0).OrderBy(c => c.Sort).ToList();
            IList<TreeGridNode> nodeList = new List<TreeGridNode>();
            moduleList.Each(c =>
            {
                TreeGridNode Node = new TreeGridNode();
                Node.DbKey = c.ModuleId.ToString();
                Node.ModuleName = c.ModuleName;
                Node.ControllerName = c.ControllerName;
                Node.ActionName = c.ActionName;
                Node.Icon = c.Icon;
                Node.BtnClass = c.BtnClass;
                Node.BtnId = c.BtnId;
                if (HasChildren(c.ModuleId))
                {
                    Node.state = "closed";
                }
                nodeList.Add(Node);
            });
            return nodeList.ToJson();
        }
        #endregion

        #region 根据角色获取根模块资源，树表格数据
        private static IList<SysModule> _rModuleList = null;
        /// <summary>
        /// 根据角色获取根模块资源，树表格数据
        /// </summary>
        /// <returns></returns>
        public string GetModuleToTreeGridByRoleKey(string roleId)
        {
            _rModuleList = new List<SysModule>();
            _rModuleList = GetModulesByRole(roleId);
            if (!_rModuleList.HasElement())
            {
                return "";
            }
            var rootList = _rModuleList.Where(c => c.ParentId == "0").OrderBy(c => c.Sort).ToList();
            IList<TreeGridNode> nodeList = new List<TreeGridNode>();
            rootList.Each(c =>
            {
                TreeGridNode rootNode = new TreeGridNode();
                rootNode.DbKey = c.ModuleId.ToString();
                rootNode.ModuleName = c.ModuleName;
                rootNode.ControllerName = c.ControllerName;
                rootNode.ActionName = c.ActionName;
                rootNode.Icon = c.Icon;
                rootNode.BtnClass = c.BtnClass;
                rootNode.BtnId = c.BtnId;
                int num = _rModuleList.Count(m => m.ParentId == c.ModuleId);
                if (num > 0)
                {
                    rootNode.state = "closed";
                }
                nodeList.Add(rootNode);
            });
            return nodeList.ToJson();
        }

        /// <summary>
        /// 获取下级模块资源，树表格数据
        /// </summary>
        /// <param name="id">下级模块资源主键</param>
        /// <returns></returns>
        public string GetRoleNodeToTreeGrid(string id)
        {
            if (_rModuleList == null) return "";
            //int parentId = Int32.Parse(id);
            var moduleList = _rModuleList.Where(c => c.ParentId == id && c.IsDel == 0).OrderBy(c => c.Sort).ToList();
            IList<TreeGridNode> nodeList = new List<TreeGridNode>();
            moduleList.Each(c =>
            {
                TreeGridNode Node = new TreeGridNode();
                Node.DbKey = c.ModuleId.ToString();
                Node.ModuleName = c.ModuleName;
                Node.ControllerName = c.ControllerName;
                Node.ActionName = c.ActionName;
                Node.Icon = c.Icon;
                Node.BtnClass = c.BtnClass;
                Node.BtnId = c.BtnId;
                int num = _rModuleList.Count(m => m.ParentId == c.ModuleId);
                if (num > 0)
                {
                    Node.state = "closed";
                }
                nodeList.Add(Node);
            });
            return nodeList.ToJson();
        }
        #endregion

        #region 获取根模块资源，树表格数据（分配权限页面）
        private static IList<SysModule> _rModuleInfoList = null;
        /// <summary>
        /// 获取根模块资源，树表格数据
        /// </summary>
        /// <param name="roleId">角色主键</param>
        /// <returns></returns>
        public string GetRootModuleAndIsChecked(string roleId)
        {
            _rModuleInfoList = GetModulesByRole(roleId);
            IList<SysModule> hasRoots = new List<SysModule>();
            if (_rModuleInfoList.HasElement())
            {
                hasRoots = _rModuleInfoList.Where(c => c.ParentId == "0").OrderBy(c => c.Sort).ToList();
            }
            var rootList = _model.SysModule.Where(c => c.ParentId == "0" && c.IsDel == 0).OrderBy(c => c.Sort).ToList();
            IList<TreeGridNode> nodeList = new List<TreeGridNode>();
            rootList.Each(c =>
            {
                TreeGridNode rootNode = new TreeGridNode();
                rootNode.DbKey = c.ModuleId.ToString();
                rootNode.ModuleName = c.ModuleName;
                rootNode.ControllerName = c.ControllerName;
                rootNode.ActionName = c.ActionName;
                rootNode.Icon = c.Icon;
                rootNode.BtnClass = c.BtnClass;
                rootNode.BtnId = c.BtnId;
                if (HasChildren(c.ModuleId))
                {
                    rootNode.state = "closed";
                }
                if (hasRoots.HasElement())
                {
                    hasRoots.Each(r =>
                    {
                        if (c.ModuleId == r.ModuleId)
                        {
                            rootNode.CheckState = true;
                        }
                    });
                }
                nodeList.Add(rootNode);
            });
            return nodeList.ToJson();
        }

        /// <summary>
        /// 获取下级模块资源，树表格数据
        /// </summary>
        /// <param name="id">下级模块资源主键</param>
        /// <returns></returns>
        public string GetNodeAndIsChecked(string id)
        {
            if (id.IsNullOrEmpty()) return "";
            //int parentId = Int32.Parse(id);
            var moduleList = _model.SysModule.Where(c => c.ParentId == id && c.IsDel == 0).OrderBy(c => c.Sort).ToList();
            IList<TreeGridNode> nodeList = new List<TreeGridNode>();
            moduleList.Each(c =>
            {
                TreeGridNode Node = new TreeGridNode();
                Node.DbKey = c.ModuleId.ToString();
                Node.ModuleName = c.ModuleName;
                Node.ControllerName = c.ControllerName;
                Node.ActionName = c.ActionName;
                Node.Icon = c.Icon;
                Node.BtnClass = c.BtnClass;
                Node.BtnId = c.BtnId;
                if (HasChildren(c.ModuleId))
                {
                    Node.state = "closed";
                }
                if (_rModuleInfoList.HasElement())
                {
                    _rModuleInfoList.Each(r =>
                    {
                        if (c.ModuleId == r.ModuleId)
                        {
                            Node.CheckState = true;
                        }
                    });
                }
                nodeList.Add(Node);
            });
            return nodeList.ToJson();
        }
        #endregion

        #region 获取模块资源下拉树
        /// <summary>
        /// 获取模块资源树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetRootTree()
        {
            IList<ComboTreeNode> nodeList = new List<ComboTreeNode>();
            var rootModuleList = _model.SysModule.Where(c => c.IsDel == 0 && c.ParentId == "0").OrderBy(c => c.Sort).ToList();
            ComboTreeNode mainNode = null;
            ComboTreeNode rootNode = new ComboTreeNode();
            rootNode.level = 0;
            rootNode.id = "0";
            rootNode.state = "closed";
            rootNode.text = "模块资源";
            rootModuleList.Each(c =>
            {
                mainNode = new ComboTreeNode();
                mainNode.id = c.ModuleId.ToString();
                mainNode.text = c.ModuleName;
                mainNode.level = c.ModuleLevel;
                if (HasChildren(c.ModuleId))
                {
                    mainNode.state = "closed";
                }
                rootNode.children.Add(mainNode);
            });
            nodeList.Add(rootNode);
            return nodeList.ToJson();
        }
        #endregion

        #region 获取下级模块资源
        /// <summary>
        /// 获取下级模块资源
        /// </summary>
        /// <param name="id">下级模块资源主键</param>
        /// <returns></returns>
        public string GetNode(string id)
        {
            //int parentId = Int32.Parse(id);
            var moduleList = _model.SysModule.Where(c => c.ParentId == id && c.IsDel == 0 && c.ModuleLevel != 3).OrderBy(c => c.Sort).ToList();
            IList<ComboTreeNode> nodeList = new List<ComboTreeNode>();
            moduleList.Each(c =>
            {
                ComboTreeNode mainNode = new ComboTreeNode();
                mainNode.id = c.ModuleId.ToString();
                mainNode.text = c.ModuleName;
                mainNode.level = c.ModuleLevel;
                if (HasChildrenNoBtn(c.ModuleId))
                {
                    mainNode.state = "closed";
                }
                nodeList.Add(mainNode);
            });
            return nodeList.ToJson();
        }
        #endregion

        #region 是否有下级模块资源
        /// <summary>
        /// 是否有下级模块资源
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool HasChildren(string key)
        {
            return _model.SysModule.Count(c => c.ParentId == key && c.IsDel == 0) > 0;
        }
        /// <summary>
        /// 是否有下级模块资源，按钮除外
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool HasChildrenNoBtn(string key)
        {
            return _model.SysModule.Count(c => c.ParentId == key && c.IsDel == 0 && c.ModuleLevel != 3) > 0;
        }
        #endregion

        #region 获取资源模块
        /// <summary>
        /// 获取资源模块
        /// </summary>
        /// <param name="keyList">资源模块主键</param>
        /// <returns></returns>
        public static IList<SysModule> GetModuleList(IList<int> keyList)
        {
            OriginEntities model = new OriginEntities();
            IList<SysModule> modules = new List<SysModule>();
            //foreach (var item in keyList)
            //{
            //    var module = model.SysModule.FirstOrDefault(m => m.IsDel == 0 && m.ModuleId == item.GetInt().t);
            //}
            keyList.Each(c =>
            {
                var module = model.SysModule.FirstOrDefault(m => m.IsDel == 0 && m.ModuleId == c.ToString());
                if (module != null)
                {
                    modules.Add(module);
                }
            });
            return modules.OrderBy(c => c.Sort).ToList();
        }
        /// <summary>
        /// 获取模块资源，按钮除外
        /// </summary>
        /// <param name="keyList"></param>
        /// <returns></returns>
        public static IList<SysModule> GetModuleListOutBtn(IList<int?> keyList)
        {
            OriginEntities model = new OriginEntities();
            IList<SysModule> modules = new List<SysModule>();
            keyList.Each(c =>
            {
                var module = model.SysModule.FirstOrDefault(m => m.IsDel == 0 && m.ModuleId == c.ToString() && m.ModuleLevel != 3);
                if (module != null)
                {
                    modules.Add(module);
                }
            });
            return modules.OrderBy(c => c.Sort).ToList();
        }
        #endregion

        #region 获取角色所配资源模块
        /// <summary>
        /// 获取角色所配资源模块
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<SysModule> GetModulesByRole(string roleId)
        {
            if (roleId.IsNullOrEmpty()) return null;
            //int key = Int32.Parse(roleId);

            var moduleList = _model.GetModulesByRole(roleId).ToList();
            if (moduleList.HasElement())
            {
                SysModule m = null;
                _rModuleList = new List<SysModule>();
                foreach (var item in moduleList)
                {
                    m = new SysModule();
                    m.ActionName = item.ActionName;
                    m.BtnClass = item.BtnClass;
                    m.BtnId = item.BtnId;
                    m.ControllerName = item.ControllerName;
                    m.Description = item.Description;
                    m.Icon = item.Icon;
                    m.IsDel = item.IsDel;
                    m.ModuleId = item.ModuleId;
                    m.ModuleLevel = item.ModuleLevel;
                    m.ParentId = item.ParentId;
                    m.Sort = item.Sort;
                    m.ModuleName = item.ModuleName;
                    _rModuleList.Add(m);
                }
            }
            return _rModuleList;
        }
        #endregion

        #region 获取按钮SelectList集合
        /// <summary>
        /// 获取按钮SelectList集合
        /// </summary>
        /// <param name="configGroup">分组名称</param>
        /// <returns>SelectList集合</returns>
        public static SelectList GetBtnSelectList(string configGroup)
        {
            List<SelectListItem> item = null;
            if (configGroup == SysConfigEnum.BtnSize)
            {
                item = new List<SelectListItem>()
                {
                    new SelectListItem() {Text = "特小号", Value = "btn-xs"},
                    new SelectListItem() {Text = "小号", Value = "btn-sm"},
                    new SelectListItem() {Text = "中号", Value = "btn-md"},
                    new SelectListItem() {Text = "图标按钮", Value = "btn-icon"}
                };
            }
            else if (configGroup == SysConfigEnum.BtnClass)
            {
                item = new List<SelectListItem>()
                {
                    new SelectListItem() {Text = "灰色按钮，默认", Value = "btn btn-default"},
                    new SelectListItem() {Text = "蓝色按钮，经典", Value = "btn btn-primary"},
                    new SelectListItem() {Text = "绿色按钮，成功", Value = "btn btn-success"},
                    new SelectListItem() {Text = "红色按钮，危险", Value = "btn btn-danger"},
                    new SelectListItem() {Text = "浅蓝色按钮，信息", Value = "btn btn-info"},
                    new SelectListItem() {Text = "橘黄色按钮，警告", Value = "btn btn-warning"}
                };
            }
            else if (configGroup == SysConfigEnum.BtnFunction)
            {
                item = new List<SelectListItem>()
                {
                    new SelectListItem() {Text = "新增", Value = "btnAdd"},
                    new SelectListItem() {Text = "导入", Value = "btnImport"},
                    new SelectListItem() {Text = "高级查询", Value = "btnAdvancedSearch"},
                    new SelectListItem() {Text = "批量删除", Value = "btnDeleteAll"},
                    new SelectListItem() {Text = "批量分配", Value = "btnAssign"},
                    new SelectListItem() {Text = "编辑", Value = "btnEdit"},
                    new SelectListItem() {Text = "删除", Value = "btnDelete"},
                    new SelectListItem() {Text = "搜索", Value = "btnSearch"},
                    new SelectListItem() {Text = "确定", Value = "btnSure"},
                    new SelectListItem() {Text = "禁用", Value = "btnFrozen"},
                    new SelectListItem() {Text = "启用", Value = "btnUnFreeze"},
                    new SelectListItem() {Text = "密码初始化", Value = "btnInitPassword"},
                    new SelectListItem() {Text = "授权", Value = "btnPower"},
                    new SelectListItem() {Text = "批次授权", Value = "btnPCPower"},
                    new SelectListItem() {Text = "下载", Value = "btnDownload"},
                    new SelectListItem() {Text = "重置", Value = "btnClear"},
                       new SelectListItem() {Text = "快速取消", Value = "btnQuickCancel"},
                        new SelectListItem() {Text = "快速确认", Value = "btnQuickConfirmation"},
                };
            }
            else if (configGroup == SysConfigEnum.IsAuthorize)
            {
                item = new List<SelectListItem>()
                {
                    new SelectListItem() {Text = "需要", Value = "true"},
                    new SelectListItem() {Text = "不需要", Value = "false"}
                };
            }
            else
            {
                item = new List<SelectListItem>();
            }
            if (item.HasElement())
            {
                item.Insert(0, new SelectListItem() { Text = "---请选择---" });
            }
            return new SelectList(item, "Value", "Text");
        }
        #endregion
    }
}
