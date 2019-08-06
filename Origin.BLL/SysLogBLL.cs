using System;
using System.Collections.Generic;
using System.Linq;
using Origin.Common;
using Origin.IBLL;
using Origin.IDAL;
using Origin.Model;
using Unity.Attributes;
namespace Origin.BLL
{
    public class SysLogBLL : ISysLogBLL
    {
        [Dependency]
        public ISysLogDAL logDAL { get; set; }
        public List<SysLog> GetList(ref GridPager pager, string queryStr)
        {
            OriginEntities db = new OriginEntities();
            List<SysLog> query = null;
            IQueryable<SysLog> list = logDAL.GetList(db);
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.Message.Contains(queryStr) || a.Module.Contains(queryStr));
                pager.totalRows = list.Count();
            }
            else
            {
                pager.totalRows = list.Count();
            }
            query = LinqHelper.SortingAndPaging(list, pager.sort, pager.order, pager.page, pager.rows).ToList();
            return query;
        }

        public SysLog GetById(int id)
        {
            return logDAL.GetById(id);
        }
        /// 创建一个实体
        /// </summary>
        /// <param name="errors">持久的错误信息</param>
        /// <param name="model">模型</param>
        /// <returns>是否成功</returns>
        public bool Create(SysLog model, ref ValidationErrors errors)
        {
            try
            {
                SysLog entity = logDAL.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add("主键重复");
                    return false;
                }
                if (logDAL.Create(entity) == 1)
                {
                    return true;
                }
                else
                {
                    errors.Add("插入失败");
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

        public void Delete(OriginEntities db, string[] deleteCollection)
        {
            logDAL.Delete(db, deleteCollection);
        }
    }
}
