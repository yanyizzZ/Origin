using System.Linq;
using Origin.Model;
namespace Origin.IDAL
{
   public interface ISysModuleOperateDAL
    {
       IQueryable<SysModuleOperate> GetList(OriginEntities db);
       int Create(SysModuleOperate entity);
       int Delete(string id);
       SysModuleOperate GetById(string id);
       bool IsExist(string id);
    }
}
