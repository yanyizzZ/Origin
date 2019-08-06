using System;
using System.Collections.Generic;
using System.Linq;
using Origin.IDAL;
using Origin.Model;
using Origin.Model.Sys;

namespace Origin.DAL
{
    public class SysModuleDAL : ISysModuleDAL, IDisposable
    {
        public List<SysModule> GetMenuByPersonId(int personId, string moduleId)
        {
            using (OriginEntities db = new OriginEntities())
            {

                //得到角色
                var roles = from r in db.SysRoleSysUser
                            from u in db.SysUser
                            where u.Id == personId
                            select r;

                    var menus = (from m in db.SysModule
                                 from ri in db.SysRight
                                 where ri.ModuleId == m.ModuleId
                                 where roles.Any(r => r.SysRoleId == ri.RoleId)
                                 where ri.Rightflag == true
                                 where m.ParentId == moduleId
                                 where m.ModuleId != "0"
                                 where m.IsDisplay==true
                                 select m)
                 .Distinct().OrderBy(a => a.Sort);
                return menus.ToList();
            }
        }

        public IQueryable<SysModule> GetList(OriginEntities db)
        {
            return db.SysModule.AsQueryable();
        }

        public IQueryable<SysModule> GetModuleBySystem(OriginEntities db, string parentId)
        {
            return db.SysModule.Where(a => a.ParentId == parentId).AsQueryable();
        }

        public int Create(SysModule entity)
        {
            using (OriginEntities db = new OriginEntities())
            {
                db.SysModule.Add(entity);
                return db.SaveChanges();
            }
        }

        public void Delete(OriginEntities db, string id)
        {
            SysModule entity = db.SysModule.SingleOrDefault(a => a.ModuleId == id);
            if (entity != null)
            {
                //删除SysRight表数据
                var sr = db.SysRight.AsQueryable().Where(a => a.ModuleId == id);
                foreach (var o in sr)
                {
                    var sro = db.SysRightOperate.AsQueryable().Where(a => a.RightId == o.Id);
                    foreach (var o2 in sro)
                    {
                        db.SysRightOperate.Remove(o2);
                    }
                }
                var smo = db.SysModuleOperate.AsQueryable().Where(a => a.ModuleId == id);
                foreach (var o3 in smo)
                {
                    db.SysModuleOperate.Remove(o3);
                }
                db.SysModule.Remove(entity);
            }
        }

        public int Edit(SysModule entity)
        {
            using (OriginEntities db = new OriginEntities())
            {
                db.SysModule.Add(entity);
                return db.SaveChanges();
            }
        }

        public SysModule GetById(string id)
        {
            using (OriginEntities db = new OriginEntities())
            {
                return db.SysModule.SingleOrDefault(a => a.ModuleId == id);
            }
        }

        public bool IsExist(string id)
        {
            using (OriginEntities db = new OriginEntities())
            {
                SysModule entity = GetById(id);
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
