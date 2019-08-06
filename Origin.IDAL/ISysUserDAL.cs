using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Origin.Model;
namespace Origin.IDAL
{
    public interface ISysUserDAL
    {
        SysUser Login(string username, string pwd);
        int Create(SysUser entity);
        IQueryable<SysUser> GetList(OriginEntities db);
        SysUser GetById(int id);
        bool IsExist(int id);
        int Edit(SysUser entity);
        int Delete(int id);
        void Delete(OriginEntities db, string[] deleteCollection);
    }
}
