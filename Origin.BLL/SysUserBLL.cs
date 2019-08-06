using System.Linq;
using Origin.IBLL;
using Origin.IDAL;
using Origin.Model;
using Unity.Attributes;
using Origin.Common;
using System.Collections.Generic;
using Origin.Model.Sys;
namespace Origin.BLL
{
    public class SysUserBLL : BaseBLL, ISysUserBLL
    {
        [Dependency]
        public ISysUserDAL accountDAL { get; set; }
        [Dependency]
        public ISysRightDAL sysRightDAL { get; set; }
        public SysUser Login(string username, string pwd)
        {
            return accountDAL.Login(username, pwd);
        }

        public int Create(SysUser entity)
        {
            return accountDAL.Create(entity);
        }

        public List<SysUser> GetList(ref GridPager pager, string queryStr)
        {
            List<SysUser> query = null;
            IQueryable<SysUser> list = accountDAL.GetList(db);
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.UserName.Contains(queryStr) || a.TrueName.Contains(queryStr));
                pager.totalRows = list.Count();
            }
            else
            {
                pager.totalRows = list.Count();
            }
            query = LinqHelper.SortingAndPaging(list, pager.sort, pager.order, pager.page, pager.rows).ToList();
            return query;
        }

        public SysUser GetById(int id)
        {
            return accountDAL.GetById(id);
        }

        public bool IsExist(int id)
        {
            return accountDAL.IsExist(id);
        }

        public int Edit(SysUser entity)
        {
            return accountDAL.Edit(entity);
        }

        public int Delete(int id)
        {
            return accountDAL.Delete(id);
        }

        public void Delete(string[] deleteCollection)
        {
            accountDAL.Delete(db, deleteCollection);
        }


        public List<PermModel> GetPermission(int accountid, string controller)
        {
            return sysRightDAL.GetPermission(accountid, controller);
        }
    }
}
