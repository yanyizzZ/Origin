using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity.Attributes;
using Origin.IBLL;
using Origin.Model;
using Origin.Model.Sys;
using Origin.Common;
namespace OriginAdminWeb.Controllers
{
    public class SysLogController : Controller
    {
        [Dependency]
        public ISysLogBLL logBLL { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetList(GridPager pager, string querStr)
        {
            List<SysLog> list = logBLL.GetList(ref pager, querStr);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new SysLogModel()
                        {
                            Id = r.Id,
                            Operator = r.Operator,
                            Message = r.Message,
                            Result = r.Result,
                            Type = r.Type,
                            Module = r.Module,
                            CreateTime = r.CreateTime
                        }).ToArray()
            };
            return Json(json);
        }
        public ActionResult Detail(int id)
        {
            SysLog entity = logBLL.GetById(id);
            SysLogModel info = new SysLogModel()
            {
                Id = entity.Id,
                Operator = entity.Operator,
                Message = entity.Message,
                Result = entity.Result,
                Type = entity.Type,
                Module = entity.Module,
                CreateTime = entity.CreateTime,
            };
            return View(info);
        }
    }
}