using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Origin.IDAL;
using Origin.Model;
namespace Origin.DAL
{
    public class SysRoleDAL : ISysRoleDAL, IDisposable
    {
        public IQueryable<SysRole> GetList(OriginEntities db)
        {
            IQueryable<SysRole> list = db.SysRole.AsQueryable();
            return list;
        }

        public int Create(SysRole entity)
        {
            using(OriginEntities db=new OriginEntities()){
                db.SysRole.Add(entity);
              return  db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
           using(OriginEntities db=new OriginEntities()){
               SysRole entity=db.SysRole.SingleOrDefault(a=>a.Id==id);
               if(entity!=null){
                   db.SysRole.Remove(entity);
               }
               return db.SaveChanges();
           }
        }

        public int Edit(SysRole entity)
        {
            using (OriginEntities db = new OriginEntities())
            {
                db.SysRole.Attach(entity);
                return db.SaveChanges();
            }
        }

        public SysRole GetById(string id)
        {
            using (OriginEntities db = new OriginEntities())
            {
                return db.SysRole.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (OriginEntities db = new OriginEntities())
            {
                SysRole entity = db.SysRole.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                    return true;
                return false;
            }
        }

        public void Dispose()
        {
        }
    }
}
