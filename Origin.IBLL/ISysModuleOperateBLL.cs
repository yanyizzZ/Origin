using System.Collections.Generic;
using Origin.Common;
using Origin.Model.Sys;
namespace Origin.IBLL
{
    public interface ISysModuleOperateBLL
    {
        List<SysModuleOperateModel> GetList(ref GridPager pager, string queryStr);
        bool Create(ref ValidationErrors errors, SysModuleOperateModel model);
        bool Delete(ref ValidationErrors errors, string id);
        SysModuleOperateModel GetById(string id);
        bool IsExist(string id);
    }
}
