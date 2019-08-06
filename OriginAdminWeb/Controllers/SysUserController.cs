using log4net;
using Origin.Common;
using Origin.IBLL;
using Origin.Model;
using Origin.Model.Sys;
using System;
using System.Linq;
using System.Web.Mvc;
using Unity.Attributes;
namespace OriginAdminWeb.Controllers
{
    public class SysUserController : BaseController
    {
        [Dependency]
        public ISysUserBLL accountBLL { get; set; }
        public ActionResult Index()
        {
            return MyView();
        }
        public ActionResult Page()
        {
            try
            {
                IQueryable<SysUser> data = db.SysUser;
                if (!string.IsNullOrEmpty(Request["UserName"]))
                {
                    data = data.Where(t => t.UserName == Request["UserName"]);
                }
                if (!string.IsNullOrEmpty(Request["MobileNumber"]))
                {
                    data = data.Where(t => t.MobileNumber == Request["MobileNumber"]);
                }
                int total = data.Count();
                //data = data.OrderByDescending(t => t.ID).Skip(offset).Take(limit);
                if (Request["limit"] != null)
                {
                    data = data.SortBy(Request["sort"] ?? "ID", string.IsNullOrEmpty(Request["sort"]) ? "desc" : Request["order"]).Skip(int.Parse(Request["offset"])).Take(int.Parse(Request["limit"]));
                }
                var rows = data.Select(t => new
                {
                    t.Id,
                    t.UserName,
                    t.TrueName,
                    t.Card,
                    t.MobileNumber,
                    t.PhoneNumber,
                    t.QQ,
                    t.EmailAddress,
                    t.OtherContact
                }).ToList();
                var jsonResult = new { total = total, rows = rows };
                return Json(jsonResult);
            }
            catch (Exception err)
            {
                LogManager.GetLogger("loginfo").Error("Page", err);
                return new EmptyResult();
            }
        }
        public JsonResult GetList(string queryStr)
        {
            GridPager pager = GetPageInfo();
            var list = accountBLL.GetList(ref pager, queryStr).Select(t => new
            {
                t.Id,
                t.UserName,
                t.TrueName,
                t.Card,
                t.MobileNumber,
                t.PhoneNumber,
                t.QQ,
                t.EmailAddress,
                t.OtherContact
            }).ToList();
            var json = new { total = pager.totalRows, rows = list };
            return Json(json);
        }
        public override GridPager GetPageInfo()
        {
            GridPager page = new GridPager();
            if (Request["limit"] != null)
            {
                page.sort = Request["sort"] ?? "Id";
                page.order = string.IsNullOrEmpty(Request["order"]) ? "desc" : Request["order"];
                page.page = string.IsNullOrEmpty(Request["page"]) ? 1 : int.Parse(Request["page"]);
                page.rows = string.IsNullOrEmpty(Request["rows"]) ? 10 : int.Parse(Request["rows"]);
            }
            //data = data.SortBy(Request["sort"] ?? "ID", string.IsNullOrEmpty(Request["sort"]) ? "desc" : Request["order"]).Skip(int.Parse(Request["offset"])).Take(int.Parse(Request["limit"]));
            return page;
        }

        [AuthorizeFilterAttribute(IsCheck = false)]
        public ActionResult Login()
        {
            return View();
        }
        #region 从缓存中获取最近发送的验证码
        //从缓存中获取最近发送的验证码
        //ObjCache oChache = AppCache.GetInstance().GetCache(CacheType.VerificationCode, "RegisterCode_" + mobile);

        //if (oChache == null)
        //{
        //    return Json(new { status = "error", message = "尚未发送验证码，请先发送验证码！" }, JsonRequestBehavior.AllowGet);
        //}
        //else if (oChache.CacheTime.AddSeconds(600) < DateTime.Now)
        //{
        //    return Json(new { status = "error", message = "验证码已经过期，请重新发送！" }, JsonRequestBehavior.AllowGet);
        //}
        //else if (code != oChache.Content.ToString())
        //{
        //    return Json(new { status = "error", message = "验证码输入错误，请重新输入！" }, JsonRequestBehavior.AllowGet);
        //}
        //AppCache.GetInstance().SetCache(CacheType.VerificationCode, "RegisterCode_" + mobile, RegisterCode);
        #endregion

        [AuthorizeFilterAttribute(IsCheck = false)]
        public JsonResult CheckLogin(string UserName, string Password, string Code)
        {
            //if (Session["Code"] == null)
            //{
            //    return Json(JsonHandler.CreateMessage(0, "请重新刷新验证码！"), JsonRequestBehavior.AllowGet);
            //}
            //if (Session["Code"].ToString().ToLower() != Code.ToLower())
            //{
            //    return Json(JsonHandler.CreateMessage(0, "验证码错误！"), JsonRequestBehavior.AllowGet);
            //}
            SysUser user = accountBLL.Login(UserName, ValueConvert.MD5(Password));
            if (user == null)
            {
                return Json(JsonHandler.CreateMessage(0, "用户名或密码错误！"), JsonRequestBehavior.AllowGet);
            }
            else if (!Convert.ToBoolean(user.State))
            {
                return Json(JsonHandler.CreateMessage(0, "账户被系统禁用！"), JsonRequestBehavior.AllowGet);
            }
            SysUserModel account = new SysUserModel();
            account.Id = user.Id;
            account.TrueName = user.TrueName;
            Session["Account"] = account;
            return Json(JsonHandler.CreateMessage(1, "登陆成功！"), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Create(string id)
        {
            ViewBag.Perm = GetPermission();
            SysUser entity = accountBLL.GetById(Convert.ToInt32(id));
            return View(entity);
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            ViewBag.Perm = GetPermission();
            SysUser entity = accountBLL.GetById(Convert.ToInt32(id));
            return View(entity);
        }
    }
}