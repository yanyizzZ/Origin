using System.Collections.Generic;
using System.Linq;
using Origin.Common;
using Origin.Model;
using Origin.IDAL;
using Origin.IBLL;
namespace Origin.BLL
{
    public class SysExceptionBLL : ISysExceptionBLL
    {
        public ISysExceptionDAL exceptionDAL { get; set; }
        public List<SysException> GetList(ref GridPager pager, string queryStr)
        {
            OriginEntities db = new OriginEntities();
            List<SysException> query = null;
            IQueryable<SysException> list = exceptionDAL.GetList(db);
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.Message.Contains(queryStr));
                pager.totalRows = list.Count();
            }
            else
            {
                pager.totalRows = list.Count();
            }
            query = LinqHelper.DataSorting(list, pager.sort, pager.order).ToList();
            return query;
        }

        public SysException GetById(string id)
        {
            return exceptionDAL.GetById(id);
        }
    }
}
