using Origin.Common;
using Origin.Model;
using System.Collections.Generic;
using System.Linq;
namespace Origin.IBLL
{
    public interface ISysModuleBLL
    {
        List<SysModule> GetMenuByPersonId(int personId, string moduleId);
        List<SysModule> GetList(string parentId);
        IQueryable<SysModule> GetList(OriginEntities db);
        List<SysModule> GetModuleBySystem(string parentId);
        bool Create(ref ValidationErrors errors, SysModule model);
        bool Delete(ref ValidationErrors errors, string id);
        bool Edit(ref ValidationErrors errors, SysModule model);
        SysModule GetById(string id);
        bool IsExist(string id);
    }
}
