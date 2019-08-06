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
    public class SysUserDAL : ISysUserDAL, IDisposable
    {
        public SysUser Login(string username, string pwd)
        {
            using (OriginEntities db = new OriginEntities())
            {
                SysUser user = db.SysUser.SingleOrDefault(a => a.UserName == username && a.Password == pwd);
                return user;
            }
        }

        public int Create(SysUser entity)
        {
            using (OriginEntities db = new OriginEntities())
            {
                db.SysUser.Add(entity);
                return db.SaveChanges();
            }
        }

        public IQueryable<SysUser> GetList(OriginEntities db)
        {
            IQueryable<SysUser> list = db.SysUser.AsQueryable();
            return list;
        }

        public SysUser GetById(int id)
        {
            using (OriginEntities db = new OriginEntities())
            {
                return db.SysUser.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(int id)
        {
            using (OriginEntities db = new OriginEntities())
            {
                SysUser user = GetById(id);
                if (user != null)
                    return true;
                return false;
            }
        }

        public int Edit(SysUser entity)
        {
            using (OriginEntities db = new OriginEntities())
            {
                db.Set<SysUser>().Attach(entity);
                db.Entry<SysUser>(entity).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }

        public int Delete(int id)
        {
            using (OriginEntities db = new OriginEntities())
            {
                SysUser entity = db.SysUser.SingleOrDefault(a => a.Id == id);
                db.Set<SysUser>().Remove(entity);
                return db.SaveChanges();
            }
        }

        public void Delete(OriginEntities db, string[] deleteCollection)
        {
            IQueryable<SysUser> collection = from u in db.SysUser
                                             where
                                                 deleteCollection.Contains(u.Id.ToString())
                                             select u;
            foreach (var item in collection)
            {
                db.SysUser.Remove(item);
            }
            db.SaveChanges();
        }
        public void Dispose()
        {
        }
    }
}
