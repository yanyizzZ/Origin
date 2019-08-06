using System.Linq;
using Origin.Model;
using Origin.Common;
using System.Collections.Generic;
using Origin.Model.Sys;
namespace Origin.IBLL
{
    public interface ISysUserBLL
    {
        SysUser Login(string username, string pwd);
        int Create(SysUser entity);
        List<SysUser> GetList(ref GridPager pager, string queryStr);
        SysUser GetById(int id);
        bool IsExist(int id);
        int Edit(SysUser entity);
        int Delete(int id);
        void Delete(string[] deleteCollection);
        List<PermModel> GetPermission(int accountid, string controller);
    }
}
