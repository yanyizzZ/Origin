using System.Collections.Generic;
using System.Linq;
using Origin.Model;
using Origin.Model.Sys;
namespace Origin.IDAL
{
    public interface ISysModuleDAL
    {
        List<SysModule> GetMenuByPersonId(int personId, string moduleId);
        IQueryable<SysModule> GetList(OriginEntities db);
        IQueryable<SysModule> GetModuleBySystem(OriginEntities db, string parentId);
        int Create(SysModule entity);
        void Delete(OriginEntities db, string id);
        int Edit(SysModule entity);
        SysModule GetById(string id);
        bool IsExist(string id);
    }
}
