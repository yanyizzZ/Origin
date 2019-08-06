using Origin.Common;
using Origin.IBLL;
using Origin.Model;
using Origin.Model.Sys;
using OriginAdminWeb.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Unity.Attributes;
namespace OriginAdminWeb.Controllers
{
    public class BaseController : Controller
    {
        protected OriginEntities db = new OriginEntities();
        ValidationErrors errors = new ValidationErrors();
        [Dependency]
        public ISysModuleBLL sysModuleBLL { get; set; }
        public static string User
        {
            get
            {
                return (string)System.Web.HttpContext.Current.Session["Account"];
            }
            set
            {
                System.Web.HttpContext.Current.Session["Account"] = value;
            }
        }
        /// <summary>
        /// 获取当前用户Id
        /// </summary>
        /// <returns>Id</returns>
        public int GetUserId()
        {
            if (Session["Account"] != null)
            {
                SysUserModel info = (SysUserModel)Session["Account"];
                return info.Id;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 获取当前用户Name
        /// </summary>
        /// <returns>Name</returns>
        public string GetUserTrueName()
        {
            if (Session["Account"] != null)
            {
                SysUserModel info = (SysUserModel)Session["Account"];
                return info.TrueName;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns>用户信息</returns>
        public SysUserModel GetAccount()
        {
            if (Session["Account"] != null)
            {
                return (SysUserModel)Session["Account"];
            }
            return null;
        }
        public virtual GridPager GetPageInfo()
        {
            GridPager page = new GridPager();
            if (Request["limit"] != null)
            {
                page.sort = Request["sort"] ?? "Id";
                page.order = string.IsNullOrEmpty(Request["order"]) ? "desc" : Request["order"];
                page.page = int.Parse(Request["page"]);
                page.rows = int.Parse(Request["rows"]);
            }
            return page;
        }

        //protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        //{
        //    return new ToJsonResult
        //    {
        //        Data = data,
        //        ContentEncoding = contentEncoding,
        //        ContentType = contentType,
        //        JsonRequestBehavior = behavior,
        //        FormateStr = "yyyy-MM-dd HH:mm:ss"
        //    };
        //}
        /// <summary>
        /// 返回JsonResult.24         /// </summary>
        /// <param name="data">数据</param>
        /// <param name="behavior">行为</param>
        /// <param name="format">json中dateTime类型的格式</param>
        /// <returns>Json</returns>
        protected JsonResult MyJson(object data, JsonRequestBehavior behavior, string format)
        {
            return new ToJsonResult
            {
                Data = data,
                JsonRequestBehavior = behavior,
                FormateStr = format
            };
        }
        /// <summary>
        /// 返回JsonResult42         /// </summary>
        /// <param name="data">数据</param>
        /// <param name="format">数据格式</param>
        /// <returns>Json</returns>
        protected JsonResult MyJson(object data, string format)
        {
            return new ToJsonResult
            {
                Data = data,
                FormateStr = format
            };
        }
        /// <summary>
        /// 检查SQL语句合法性
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool ValidateSQL(string sql, ref string msg)
        {
            if (sql.ToLower().IndexOf("delete") > 0)
            {
                msg = "查询参数中含有非法语句DELETE";
                return false;
            }
            if (sql.ToLower().IndexOf("update") > 0)
            {
                msg = "查询参数中含有非法语句UPDATE";
                return false;
            }

            if (sql.ToLower().IndexOf("insert") > 0)
            {
                msg = "查询参数中含有非法语句INSERT";
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获取当前页或操作访问权限
        /// </summary>
        /// <returns>权限列表</returns>
        public List<PermModel> GetPermission()
        {
            string filePath = HttpContext.Request.FilePath;
            List<PermModel> perm = (List<PermModel>)Session[filePath];
            return perm;
        }
        protected ActionResult MyView()
        {
            LayoutModel view = new LayoutModel
            {
                Menu = GetMenus(GetCurrentMenu())
            };
            return View(view);
        }
        public List<ViewMenu> GetMenus(string modulId = "0")
        {
            List<SysModule> menus = sysModuleBLL.GetMenuByPersonId(GetUserId(), modulId);
            List<ViewMenu> temps = menus.Select(t => new ViewMenu
            {
                ModuleId = t.ModuleId,
                ModuleName = t.ModuleName,
                ModuleNameEn = t.ModuleNameEn,
                ActionName = t.ActionName,
                ControllerName = t.ControllerName,
                Description = t.Description,
                Icon = t.Icon,
                BtnId = t.BtnId,
                BtnClass = t.BtnClass,
                Sort = t.Sort,
                ModuleLevel = t.ModuleLevel,
                IsDel = t.IsDel,
                ParentId = t.ParentId,
                ModuleType = t.ModuleType,
                IsDisplay = t.IsDisplay
                //menuList = GetMenus(modulId)
            }).ToList();
            //foreach (var item in temps)
            //{
            //    if (item.ModuleId == modulId)
            //    {
            //        item.IsDisplay = "active";
            //    }
            //}
            var MenuList = temps.Where(p => p.ParentId == "0").OrderBy(p => p.Sort).ToList();
            foreach (var item in MenuList)
            {

                //1、查询所有的 2、再查一遍
                // item.menuList = temps.Where(p => p.ParentId == item.ModuleId).OrderBy(p => p.Sort).ToList();
                item.menuList = sysModuleBLL.GetMenuByPersonId(GetUserId(), item.ModuleId).Select(t => new ViewMenu
                {
                    ModuleId = t.ModuleId,
                    ModuleName = t.ModuleName,
                    ModuleNameEn = t.ModuleNameEn,
                    ActionName = t.ActionName,
                    ControllerName = t.ControllerName,
                    Description = t.Description,
                    Icon = t.Icon,
                    BtnId = t.BtnId,
                    BtnClass = t.BtnClass,
                    Sort = t.Sort,
                    ModuleLevel = t.ModuleLevel,
                    IsDel = t.IsDel,
                    ParentId = t.ParentId,
                    ModuleType = t.ModuleType,
                    IsDisplay = t.IsDisplay
                    //menuList = GetMenus(modulId)
                }).ToList();
                //if (item.menuList.Count(p => !string.IsNullOrEmpty(p.IsDisplay)) > 0)
                //{
                //    item.IsDisplay = "active";
                //}
            }
            return MenuList;
        }

        private string GetCurrentMenu()
        {
            var strid = RouteData.Values["id"] ?? -1;
            int id = -1;
            int.TryParse(strid.ToString(), out id);
            if (id > 0)
            {
                //var page = client.Queryable<Permission_Pages>().Single(p => p.ID == id);
                //return page;
            }
            var action = RouteData.Values["action"];
            var controller = RouteData.Values["controller"];
            id = 0;
            return id.ToString();
        }
    }
}