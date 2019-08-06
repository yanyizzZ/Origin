using Origin.BLL;
using Origin.DAL;
using Origin.IBLL;
using Origin.IDAL;
using Unity;

namespace Origin.Core
{
    /// <summary>
    /// 系统注入
    /// </summary>
    public class DependencyRegisterType
    {
        public static void Container_Sys(ref UnityContainer container)
        {
            //用户
            container.RegisterType<ISysUserBLL, SysUserBLL>();
            container.RegisterType<ISysUserDAL, SysUserDAL>();
            //菜单
            container.RegisterType<ISysModuleBLL, SysModuleBLL>();
            container.RegisterType<ISysModuleDAL, SysModuleDAL>();
            //模块操作码
            container.RegisterType<ISysModuleOperateBLL, SysModuleOperateBLL>();
            container.RegisterType<ISysModuleOperateDAL, SysModuleOperateDAL>();
            //角色权限
            container.RegisterType<ISysRightBLL, SysRightBLL>();
            container.RegisterType<ISysRightDAL, SysRightDAL>();
            //日志
            container.RegisterType<ISysLogBLL, SysLogBLL>();
            container.RegisterType<ISysLogDAL, SysLogDAL>();
            //异常
            container.RegisterType<ISysExceptionBLL, SysExceptionBLL>();
            container.RegisterType<ISysExceptionDAL, SysExceptionDAL>();

        }
    }
}
