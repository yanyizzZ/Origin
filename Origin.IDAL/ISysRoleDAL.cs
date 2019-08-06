using Origin.Model;
using System.Linq;
namespace Origin.IDAL
{
    public interface ISysRoleDAL
    {
        IQueryable<SysRole> GetList(OriginEntities db);
        int Create(SysRole entity);
        int Delete(string id);
        int Edit(SysRole entity);
        SysRole GetById(string id);
        bool IsExist(string id);
    }
}
