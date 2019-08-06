using Origin.BLL;
using Origin.DAL;
using Origin.Model.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OriginAdminWeb
{
    public class AuthorizeFilterAttribute : ActionFilterAttribute
    {
        public string ActionName { get; set; }
        private string Area;
        /// <summary>
        /// 是否需要验证
        /// </summary>
        public bool IsCheck { get; set; }
        /// <summary>
        /// 执行actin之前执行以下代码，通过[AuthorizeFilter(ActionName="Index")]指定参数
        /// </summary>
        /// <param name="filterContext">页面传过来的上下文</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!IsCheck)
            {
                return;
            }
            //判断Action方法的Control和Action是否跳过登录验证
            //if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), false)|| filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), false))
            //{
            //    return;
            //}
            if (filterContext.HttpContext.Session["Account"] == null)
            {
                //跳转方法1：
                filterContext.HttpContext.Response.Redirect("/SysUser/Login");
                //跳转方法2：
                // ViewResult view = new ViewResult();
                //指定要返回的完整视图名称
                // view.ViewName = "~/View/Login/Login.cshtml";
                return;
            }
            #region 验证用户是否有权限操作
            ////读取请求上下文中的Controller,Action,Id
            //var routes = new RouteCollection();
            //RouteConfig.RegisterRoutes(routes);
            //RouteData routeData = routes.GetRouteData(filterContext.HttpContext);
            ////取出区域的控制器Action,id
            //string ctlName = filterContext.Controller.ToString();
            //string[] routeInfo = ctlName.Split('.');
            //string controller = null;
            //string action = null;
            //string id = null;

            //int iAreas = Array.IndexOf(routeInfo, "Areas");
            //if (iAreas > 0)
            //{
            //    //取区域及控制器
            //    Area = routeInfo[iAreas + 1];
            //}
            //int ctlIndex = Array.IndexOf(routeInfo, "Controllers");
            //ctlIndex++;
            //controller = routeInfo[ctlIndex].Replace("Controller", "").ToLower();

            //string url = HttpContext.Current.Request.Url.ToString().ToLower();
            //string[] urlArray = url.Split('/');
            //int urlCtlIndex = Array.IndexOf(urlArray, controller);
            //urlCtlIndex++;
            //if (urlArray.Count() > urlCtlIndex)
            //{
            //    action = urlArray[urlCtlIndex];
            //}
            //urlCtlIndex++;
            //if (urlArray.Count() > urlCtlIndex)
            //{
            //    id = urlArray[urlCtlIndex];
            //}
            ////url
            //action = string.IsNullOrEmpty(action) ? "Index" : action;
            //int actionIndex = action.IndexOf("?", 0);
            //if (actionIndex > 1)
            //{
            //    action = action.Substring(0, actionIndex);
            //}
            //id = string.IsNullOrEmpty(id) ? "" : id;

            ////URL路径
            //string filePath = HttpContext.Current.Request.FilePath;
            //SysUserModel account = filterContext.HttpContext.Session["Account"] as SysUserModel;
            //if (ValiddatePermission(account, controller, action, filePath))
            //{
            //    return;
            //}
            //else
            //{
            //    filterContext.Result = new EmptyResult();
            //    // filterContext.Result = new RedirectResult("/SysUser/Login");
            //    return;
            //}
            #endregion
        }
        /// <summary>
        /// 执行actin之后执行以下代码
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
        public bool ValiddatePermission(SysUserModel account, string controller, string action, string filePath)
        {
            bool bResult = false;
            string actionName = string.IsNullOrEmpty(ActionName) ? action : ActionName;
            if (account != null)
            {
                List<PermModel> perm = null;
                //测试当前controller是否已赋权限值，如果没有从
                //如果存在区域,Seesion保存（区域+控制器）
                if (!string.IsNullOrEmpty(Area))
                {
                    controller = Area + "/" + controller;
                }
                perm = (List<PermModel>)HttpContext.Current.Session[filePath];
                if (perm == null || perm.Count < 1)
                {
                    using (SysUserBLL userBLL = new SysUserBLL()
                    {
                        sysRightDAL = new SysRightDAL()
                    })
                    {
                        perm = userBLL.GetPermission(account.Id, controller);//获取当前用户的权限列表
                        HttpContext.Current.Session[filePath] = perm;//获取的权限放入会话由Controller调用
                    }
                }
                //当用户访问index时，只要权限>0就可以访问
                if (actionName.ToLower() == "index")
                {
                    if (perm.Count > 0)
                    {
                        return true;
                    }
                }
                //查询当前Action 是否有操作权限，大于0表示有，否则没有
                int count = perm.Where(a => a.KeyCode.ToLower() == actionName.ToLower()).Count();
                if (count > 0)
                {
                    bResult = true;
                }
                else
                {
                    bResult = false;
                    HttpContext.Current.Response.Write("你没有操作权限，请联系管理员！");
                }

            }
            return bResult;
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
    }
}