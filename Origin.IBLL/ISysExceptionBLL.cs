using System.Collections.Generic;
using Origin.Model;
using Origin.Common;
namespace Origin.IBLL
{
  public interface ISysExceptionBLL
    {
      List<SysException> GetList(ref GridPager pager, string queryStr);
      SysException GetById(string id);
    }
}
