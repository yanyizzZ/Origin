using System;
using System.Linq;
using Origin.Model;
using Origin.IDAL;
namespace Origin.DAL
{
    public class SysLogDAL : ISysLogDAL, IDisposable
    {
        /// <summary>
        /// 创建一个对象
        /// </summary>
        /// <param name="db">数据库</param>
        /// <param name="entity">实体</param>
        public int Create(SysLog entity)
        {
            using (OriginEntities db = new OriginEntities())
            {
                db.SysLog.Add(entity);
                return db.SaveChanges();
            }
        }
        /// <summary>
        /// 删除对象集合
        /// </summary>
        /// <param name="db">数据库</param>
        /// <param name="deleteCollection">集合</param>
        public void Delete(OriginEntities db, string[] deleteCollection)
        {
            IQueryable<SysLog> collection = from f in db.SysLog
                                            where deleteCollection.Contains(f.Id.ToString())
                                            select f;
            foreach (var deleteItem in collection)
            {
                db.SysLog.Remove(deleteItem);
            }
            db.SaveChanges();
        }
        /// 获取集合
        /// </summary>
        /// <param name="db">数据库</param>
        /// <returns>集合</returns>
        public IQueryable<SysLog> GetList(OriginEntities db)
        {
            IQueryable<SysLog> list = db.SysLog.AsQueryable();
            return list;
        }

        public SysLog GetById(int id)
        {
            using (OriginEntities db = new OriginEntities())
            {
                return db.SysLog.SingleOrDefault(a => a.Id == id);

            }
        }

        public void Dispose()
        {
        }
    }
}
