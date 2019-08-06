using System.Collections.Generic;
using Origin.Model.Sys;
using Origin.Model;
namespace Origin.IDAL
{
   public interface ISysRightDAL
    {
       List<PermModel> GetPermission(int accountid, string controller);
        int UpdateRight(SysRightOperate model);
        //按选择的角色及模块加载模块的权限项
        List<P_Sys_GetRightByRoleAndModule_Result> GetRightByRoleAndModule(string roleId, string moduleId);
    }
}
