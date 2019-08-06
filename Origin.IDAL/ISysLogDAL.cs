using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Origin.Model;
namespace Origin.IDAL
{
    public interface ISysLogDAL
    {
        int Create(SysLog entity);
        void Delete(OriginEntities db, string[] deleteCollection);
        IQueryable<SysLog> GetList(OriginEntities db);
        SysLog GetById(int id);
    }
}
