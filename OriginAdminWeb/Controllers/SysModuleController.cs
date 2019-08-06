using Newtonsoft.Json;
using Origin.Common;
using Origin.IBLL;
using Origin.Model;
using OriginAdminWeb.Common;
using OriginAdminWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Unity.Attributes;

namespace OriginAdminWeb.Controllers
{
    public class SysModuleController : BaseController
    {
        [Dependency]
        public ISysModuleBLL m_BLL { get; set; }
        [Dependency]
        public ISysModuleOperateBLL operateBLL { get; set; }

        #region 模块列表
        /// <summary>
        /// 模块列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Perm = GetPermission();
            return MyView();
        }
        #endregion



        #region 获取资源模块数据树
        /// <summary>
        /// 获取资源模块数据树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[AuthorizeFilter(ActionName="Query")]
        public string GetTreeData()
        {
            if (Session["Account"] != null)
            {
                var modules = db.SysModule.Where(c => c.IsDel == 0).OrderBy(c => c.Sort).ToList();
                var rootList = modules.Where(c => c.ParentId == "0" && c.IsDel == 0).OrderBy(c => c.Sort).ToList();
                IList<NodeInfo> nodeList = new List<NodeInfo>();
                NodeInfo rootNode = new NodeInfo()
                {
                    id = "0",
                    text = "模块资源",
                    icon = "fa fa-bars",
                    level = 0
                };
                foreach (SysModule module in rootList)
                {
                    NodeInfo mainNode = new NodeInfo
                    {
                        id = module.ModuleId.ToString(),
                        text = module.ModuleName,
                        icon = module.Icon,
                        level = module.ModuleLevel
                    };
                    var children = modules.FirstOrDefault(c => c.ParentId == module.ModuleId && c.IsDel == 0 && c.ModuleLevel != 3);
                    if (children != null)
                    {
                        GetChildren(mainNode, modules);
                    }
                    else
                    {
                        mainNode.nodes = null;
                    }
                    rootNode.nodes.Add(mainNode);
                    //nodeList.Add(mainNode);
                }
                nodeList.Add(rootNode);
                return JsonConvert.SerializeObject(nodeList);
            }
            else
            {
                return JsonConvert.SerializeObject("0");
            }
        }

        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="parentModule"></param>
        /// <param name="modules"></param>
        public void GetChildren(NodeInfo parentModule, IList<SysModule> modules)
        {
            var childrenModule = modules.Where(c => c.ParentId == parentModule.id && c.IsDel == 0 && c.ModuleLevel != 3).OrderBy(c => c.Sort);
            NodeInfo childrenNode = null;
            foreach (SysModule module in childrenModule)
            {
                childrenNode = new NodeInfo()
                {
                    id = module.ModuleId.ToString(),
                    icon = module.Icon,
                    text = module.ModuleName,
                    level = module.ModuleLevel
                };
                var children = modules.FirstOrDefault(c => c.ParentId == module.ModuleId && c.IsDel == 0 && c.ModuleLevel != 3);
                if (children != null)
                {
                    GetChildren(childrenNode, modules);
                }
                else
                {
                    childrenNode.nodes = null;
                }
                parentModule.nodes.Add(childrenNode);
            }
        }
        #endregion

        #region 获取资源模块数据
        /// <summary>
        /// 获取资源模块数据
        /// </summary>
        /// <returns>JsonResult数据</returns>
        [HttpGet]
        //[AuthorizeFilter(ActionName ="Detail")]
        public string GetData()
        {
            string key = Request.QueryString["key"];
            IList<SysModule> listData = new List<SysModule>();
            if (!string.IsNullOrWhiteSpace(key))
            {
                listData = db.SysModule.Where(c => c.IsDel == 0 && c.ParentId == key).OrderBy(c => c.Sort).ToList();
            }
            else
            {
                listData = db.SysModule.Where(c => c.IsDel == 0 && c.ParentId == "0").OrderBy(c => c.Sort).ToList();
            }
            return listData.ToJson();
        }
        #endregion

        #region 获取子节点
        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="parentModule"></param>
        /// <param name="modules"></param>
        //public void GetChildren(NodeInfo parentModule, IList<Sys_Module> modules)
        //{
        //    int key = Int32.Parse(parentModule.id);
        //    var childrenModule = modules.Where(c => c.ParentId == key && c.IsDel == 0 && c.ModuleLevel != 3).OrderBy(c => c.Sort);
        //    NodeInfo childrenNode = null;
        //    foreach (Sys_Module module in childrenModule)
        //    {
        //        childrenNode = new NodeInfo()
        //        {
        //            id = module.ModuleId.ToString(),
        //            icon = module.Icon,
        //            text = module.ModuleName,
        //            level = module.ModuleLevel
        //        };
        //        var children = modules.FirstOrDefault(c => c.ParentId == module.ModuleId && c.IsDel == 0 && c.ModuleLevel != 3);
        //        if (children != null)
        //        {
        //            GetChildren(childrenNode, modules);
        //        }
        //        else
        //        {
        //            childrenNode.nodes = null;
        //        }
        //        parentModule.nodes.Add(childrenNode);
        //    }
        //}
        #endregion

        #region 新增，编辑模块页面
        /// <summary>
        /// 新增，编辑模块页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ModuleInfo()
        {
            var module = new SysModule();
            ModuleViewMoudle viewModel = new ModuleViewMoudle();
            string action = Request.QueryString["method"];
            string moduleKey = Request.QueryString["moduleKey"];
            if (moduleKey.IsNullOrEmpty())
            {
                moduleKey = "0";
            }
            ViewData["BtnIdList"] = ModuleServer.GetBtnSelectList(SysConfigEnum.BtnFunction);
            ViewData["BtnClassList"] = ModuleServer.GetBtnSelectList(SysConfigEnum.BtnClass);
            ViewData["BtnSizeList"] = ModuleServer.GetBtnSelectList(SysConfigEnum.BtnSize);
            ViewData["IsAuthorize"] = ModuleServer.GetBtnSelectList(SysConfigEnum.IsAuthorize);
            switch (action)
            {
                case "add":
                    ViewBag.Title = "新增模块资源";
                    module.ParentId = moduleKey;
                    module.IsDisplay = true;
                    if (module.ParentId == "0")
                    {
                        module.ModuleLevel = 1;
                    }
                    else
                    {
                        var parentModule = db.SysModule.SingleOrDefault(a => a.ModuleId == module.ParentId);
                        if (parentModule != null) module.ModuleLevel = ++parentModule.ModuleLevel;
                    }
                    break;
                case "edit":
                    ViewBag.Title = "编辑模块";
                    module = db.SysModule.SingleOrDefault(a => a.ModuleId == moduleKey);
                    if (!module.BtnClass.IsNullOrEmpty())
                    {
                        var c = module.BtnClass.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        viewModel.BtnClass = String.Format("{0} {1}", c[0], c[1]);
                        viewModel.BtnSize = c[2];
                    }
                    break;
            }
            var m = db.SysModule.FirstOrDefault(c => c.ModuleId == module.ParentId);
            if (m != null)
            {
                viewModel.ParentModuleName = m.ModuleName;
            }
            else
            {
                viewModel.ParentModuleName = "模块资源";
            }
            viewModel.SysModule = module;
            return View(viewModel);
        }
        #endregion

        #region 添加，修改模块
        /// <summary>
        ///  添加，修改模块
        /// </summary>
        /// <param name="viewModel">页面实体</param>
        /// <returns>Json数据</returns>
        //[HttpPost]
        //[AllowAnonymous]
        //public JsonResult SaveModule(ModuleViewModel viewModel)
        //{
        //    string opearType = string.Empty;
        //    try
        //    {
        //        Sys_Module module = viewModel.Module;
        //        opearType = "编辑";
        //        if (module.ModuleId == 0)
        //        {
        //            opearType = "新增";
        //            var model =
        //                DbEntity.First(
        //                    c => c.IsDel == 0 && c.ParentId == module.ParentId && c.ModuleName == module.ModuleName);
        //            if (model != null)
        //            {
        //                return JsonError("该模块已存在！");
        //            }
        //            module.IsDel = 0;
        //            module.CompanyId = 1;
        //        }
        //        if (!viewModel.BtnClass.IsNullOrEmpty() && !module.BtnId.IsNullOrEmpty())
        //        {
        //            module.ModuleLevel = 3; //按钮
        //        }
        //        else
        //        {
        //            if (!module.ControllerName.IsNullOrEmpty() && !module.ActionName.IsNullOrEmpty())
        //            {
        //                module.ModuleLevel = 2; //页面
        //            }
        //            else
        //            {
        //                module.ModuleLevel = 1; //文件夹
        //            }
        //        }
        //        module.BtnClass = String.Format("{0} {1}", viewModel.BtnClass, viewModel.BtnSize);
        //        DbEntity.AddOrUpdate(module);
        //        LogService.AddOperateLog(DataCollection.userinfo, RouteData.Values["action"].ToString(), opearType + "资源模块");
        //        return JsonSuccess("操作成功");
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.LogError(opearType + "资源模块异常");
        //        return JsonError("操作失败");
        //    }
        //}
        #endregion

        #region 删除操作
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //[AllowAnonymous]
        //public JsonResult DeleteModule()
        //{
        //    try
        //    {
        //        string moduleKeys = Request.Form["moduleKeys"];
        //        List<string> arrKeys = moduleKeys.FromJson<List<string>>();
        //        IList<int> removeKeys = new List<int>();
        //        if (!arrKeys.Any())
        //        {
        //            return JsonError("操作失败");
        //        }
        //        foreach (var arrKey in arrKeys)
        //        {
        //            int key = Int32.Parse(arrKey);
        //            removeKeys.Add(key);
        //            GetChildrenKey(key, ref removeKeys);
        //        }
        //        //同时删除角色配置的模块
        //        List<Sys_Permission> permissions = new List<Sys_Permission>();
        //        Repository<Sys_Permission> permissionRepository = new Repository<Sys_Permission>();
        //        StringBuilder sb = new StringBuilder();
        //        removeKeys.Each(c =>
        //        {
        //            var module = DbEntity.First(m => m.ModuleId == c);
        //            if (module != null)
        //            {
        //                sb.AppendFormat("{0}，", module.ModuleName);
        //            }
        //            var perList = permissionRepository.FindAll(p => p.ModuleId == c).ToList();
        //            if (perList.HasElement())
        //            {
        //                permissions.AddRange(perList);
        //            }
        //        });
        //        DbEntity.RemoveAll(removeKeys);
        //        permissionRepository.DeleteAll(permissions);
        //        LogService.AddOperateLog(DataCollection.userinfo, RouteData.Values["action"].ToString(), String.Format("删除模块资源【{0}】", sb.ToString().TrimEnd('，')));
        //        return JsonSuccess("操作成功");
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.LogError("删除模块资源异常");
        //        return JsonError("操作失败");
        //    }
        //}
        #endregion

        #region 获取子节点主键
        /// <summary>
        /// 获取子节点主键
        /// </summary>
        /// <param name="parentKey"></param>
        /// <param name="removeKeys"></param>
        //public void GetChildrenKey(int parentKey, ref IList<int> removeKeys)
        //{
        //    var moduleList = DbEntity.FindAll(c => c.ParentId == parentKey && c.IsDel == 0).ToList();
        //    if (!moduleList.Any()) return;
        //    foreach (var module in moduleList)
        //    {
        //        removeKeys.Add(module.ModuleId);
        //        GetChildrenKey(module.ModuleId, ref removeKeys);
        //    }
        //}
        #endregion

        #region 图标列表
        /// <summary>
        /// 图标列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Icon_List()
        {
            return View();
        }
        #endregion

        #region 获取模块资源树，用于下拉列表树
        /// <summary>
        /// 获取模块资源树，用于下拉列表树
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public string GetModuleTree()
        {
            ModuleServer server = new ModuleServer();
            string parentId = Request.QueryString["id"];
            if (parentId.IsNullOrEmpty())
            {
                return server.GetRootTree();
            }
            return server.GetNode(parentId);

        }
        #endregion
    }
}