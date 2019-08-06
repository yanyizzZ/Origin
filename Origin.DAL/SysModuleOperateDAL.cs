using System;
using System.Linq;
using Origin.IDAL;
using Origin.Model;
namespace Origin.DAL
{
    public class SysModuleOperateDAL : ISysModuleOperateDAL, IDisposable
    {
        public IQueryable<SysModuleOperate> GetList(OriginEntities db)
        {
            return db.SysModuleOperate.AsQueryable();
        }

        public int Create(SysModuleOperate entity)
        {
            using (OriginEntities db = new OriginEntities())
            {
                db.SysModuleOperate.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (OriginEntities db = new OriginEntities())
            {
                SysModuleOperate entity = db.SysModuleOperate.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {
                    db.SysModuleOperate.Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public SysModuleOperate GetById(string id)
        {
            using (OriginEntities db = new OriginEntities())
            {
                return db.SysModuleOperate.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (OriginEntities db = new OriginEntities())
            {
                SysModuleOperate entity = new SysModuleOperate();
                if (entity != null)
                {
                    return true;
                }
                return false;
            }
        }
        public void Dispose()
        {
            
        }
    }
}
