using Origin.Common;
using Origin.IBLL;
using Origin.Model;
using Origin.Model.Sys;
using OriginAdminWeb.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Unity.Attributes;
namespace OriginAdminWeb.Controllers
{
    [AuthorizeFilterAttribute(IsCheck = false)]
    public class SysExceptionController : BaseController
    {
        [Dependency]
        public ISysExceptionBLL exceptionBLL { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<SysException> list = exceptionBLL.GetList(ref pager, queryStr);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new SysException()
                        {
                            Id = r.Id,
                            HelpLink = r.HelpLink,
                            Message = r.Message,
                            Source = r.Source,
                            StackTrace = r.StackTrace,
                            TargetSite = r.TargetSite,
                            Data = r.Data,
                            CreateTime = r.CreateTime
                        }).ToArray()
            };
            return Json(json);
        }
        public ActionResult Details(string id)
        {
            SysException entity = exceptionBLL.GetById(id);
            SysExceptionModel info = new SysExceptionModel()
            {
                Id = entity.Id,
                HelpLink = entity.HelpLink,
                Message = entity.Message,
                Source = entity.Source,
                StackTrace = entity.StackTrace,
                TargetSite = entity.TargetSite,
                Data = entity.Data,
                CreateTime = entity.CreateTime,
            };
            return View(info);
        }
        /// <summary>
        /// 错误页
        /// </summary>
        /// <returns></returns>
        public ActionResult Error()
        {

            BaseException ex = new BaseException();
            return View(ex);
        }
    }
}