using System.Linq;
using Origin.Model;
namespace Origin.IDAL
{
    public interface ISysExceptionDAL
    {
        int Create(SysException entity);
        IQueryable<SysException> GetList(OriginEntities db);
        SysException GetById(string id);
        void Delete(OriginEntities db, string[] deleteCollection);
    }
}
