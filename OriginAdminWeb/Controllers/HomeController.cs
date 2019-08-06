using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity.Attributes;
using Origin.Model;
using Origin.IBLL;
using Origin.Model.Sys;
namespace OriginAdminWeb.Controllers
{
    public class HomeController : BaseController
    {
        [Dependency]
        public ISysModuleBLL SysModuleBLL { get; set; }
        public ActionResult Index()
        {
            return MyView();
        }
        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>树</returns>
        public JsonResult GetTree(string id)
        {
            if (Session["Account"] != null)
            {
                SysUserModel account = (SysUserModel)Session["Account"];
                List<SysModule> menus = SysModuleBLL.GetMenuByPersonId(account.Id, id);
                var jsonData = (from m in menus
                               select m).ToArray();
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }

        }
    }
}