using System;
using System.Linq;
using Origin.Model;
using Origin.IDAL;
namespace Origin.DAL
{
    public class SysExceptionDAL : ISysExceptionDAL, IDisposable
    {
        /// <summary>
        /// 创建一个对象
        /// </summary>
        /// <param name="db">数据库</param>
        /// <param name="entity">实体</param>
        public int Create(SysException entity)
        {
            using (OriginEntities db = new OriginEntities())
            {
                db.SysException.Add(entity);
                return db.SaveChanges();
            }
        }
        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="db">数据库</param>
        /// <returns>集合</returns>
        public IQueryable<SysException> GetList(OriginEntities db)
        {
            IQueryable<SysException> list = db.SysException.AsQueryable();
            return db.SysException.AsQueryable();
        }
        /// <summary>
        /// 根据ID获取一个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysException GetById(string id)
        {
            using (OriginEntities db = new OriginEntities())
            {
                return db.SysException.SingleOrDefault(a => a.Id == id);
            }
        }
        public void Delete(OriginEntities db, string[] deleteCollection)
        {
            IQueryable<SysException> collection = from u in db.SysException
                                                  where
                                                      deleteCollection.Contains(u.Id)
                                                  select u;
            foreach (var item in collection)
            {
                db.SysException.Remove(item);
            }
            db.SaveChanges();
        }
        public void Dispose()
        {
        }
    }
}
