using System.Collections.Generic;
using Origin.Common;
using Origin.Model.Sys;
namespace Origin.IBLL
{
    public interface ISysRoleBLL
    {
        List<SysRoleModel> GetList(ref GridPager pager, string queryStr);
        bool Create(ref ValidationErrors errors, SysRoleModel model);
        bool Delete(ref ValidationErrors errors, string id);
        bool Edit(ref ValidationErrors errors, SysRoleModel model);
        SysRoleModel GetById(string id);
        bool IsExist(string id);
    }
}
