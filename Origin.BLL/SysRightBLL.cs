using Origin.IBLL;
using Origin.IDAL;
using Origin.Model;
using System;
using System.Collections.Generic;
using Unity.Attributes;

namespace Origin.BLL
{
    public class SysRightBLL : ISysRightBLL, IDisposable
    {
        [Dependency]
        public ISysRightDAL SysRightDAL { get; set; }
        public List<P_Sys_GetRightByRoleAndModule_Result> GetRightByRoleAndModule(string roleId, string moduleId)
        {
            return SysRightDAL.GetRightByRoleAndModule(roleId, moduleId);
        }

        public int UpdateRight(SysRightOperate model)
        {
            return SysRightDAL.UpdateRight(model);
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
