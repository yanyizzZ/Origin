using Origin.Model;
using System.Collections.Generic;

namespace Origin.IBLL
{
    public interface ISysRightBLL
    {
        //更新
        int UpdateRight(SysRightOperate model);
        //按选择的角色及模块加载模块的权限项
        List<P_Sys_GetRightByRoleAndModule_Result> GetRightByRoleAndModule(string roleId, string moduleId);
    }
}
