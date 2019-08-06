using Origin.Common;
using Origin.IBLL;
using Origin.Model;
using Origin.Model.Sys;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Unity.Attributes;

namespace OriginAdminWeb.Controllers
{
    public class SysRightController : BaseController
    {
        //
        // GET: /SysRight/
        [Dependency]
        public ISysRightBLL SysRightBLL { get; set; }
        [Dependency]
        public ISysRoleBLL SysRoleBLL { get; set; }
        [Dependency]
        public ISysModuleBLL SysModuleBLL { get; set; }
        [AuthorizeFilter]
        public ActionResult Index()
        {
            ViewBag.Perm = GetPermission();
            return View();
        }
        //获取角色列表
        [AuthorizeFilter(ActionName = "Index")]
        [HttpPost]
        public JsonResult GetRoleList(GridPager pager)
        {
            List<SysRoleModel> list = SysRoleBLL.GetList(ref pager, "");
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new SysRoleModel()
                        {

                            Id = r.Id,
                            Name = r.Name,
                            Description = r.Description,
                            CreateTime = r.CreateTime,
                            CreatePerson = r.CreatePerson

                        }).ToArray()

            };

            return Json(json);
        }
        //获取模组列表
        [AuthorizeFilter(ActionName = "Index")]
        [HttpPost]
        public JsonResult GetModelList(string id)
        {
            if (id == null)
                id = "0";
            List<SysModule> list = SysModuleBLL.GetList(id);
            //var json = from r in list
            //           select new SysModule()
            //           {
            //               r.ActionName
            //           };
            return Json(list);
        }

        //根据角色与模块得出权限
        [AuthorizeFilter(ActionName = "Index")]
        [HttpPost]
        public JsonResult GetRightByRoleAndModule(GridPager pager, string roleId, string moduleId)
        {
            pager.rows = 100000;
            var right = SysRightBLL.GetRightByRoleAndModule(roleId, moduleId);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in right
                        select new SysRightModelByRoleAndModuleModel()
                        {
                            Ids = r.RightId + r.KeyCode,
                            Name = r.Name,
                            KeyCode = r.KeyCode,
                            IsValid = r.isvalid,
                            RightId = r.RightId
                        }).ToArray()

            };

            return Json(json);
        }
        //保存
        [HttpPost]
        [AuthorizeFilter(ActionName = "Save")]
        public int UpdateRight(SysRightOperate model)
        {
            return SysRightBLL.UpdateRight(model);
        }
    }
}