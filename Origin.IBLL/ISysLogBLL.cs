using System.Collections.Generic;
using Origin.Model;
using Origin.Common;
namespace Origin.IBLL
{
   public  interface ISysLogBLL
    {
       bool Create(SysLog model ,ref ValidationErrors errors);
       void Delete(OriginEntities db, string[] deleteCollection);
       List<SysLog> GetList(ref GridPager pager, string queryStr);
       SysLog GetById(int id);
    }
}
