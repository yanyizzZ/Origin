using Origin.IDAL;
using Origin.Model;
using Origin.Model.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Origin.DAL
{
    public class SysRightDAL : ISysRightDAL, IDisposable
    {
        /// <summary>
        /// 取角色模块的操作权限，用于权限控制
        /// </summary>
        /// <param name="accountid">acount Id</param>
        /// <param name="controller">url</param>
        /// <returns></returns>
        public List<PermModel> GetPermission(int accountid, string controller)
        {
            using (OriginEntities db = new OriginEntities())
            {
                List<PermModel> rights = (from r in db.P_Sys_GetRightOperate(accountid, controller)
                                          select new PermModel
                                          {
                                              KeyCode = r.KeyCode,
                                              IsValid = r.IsValid
                                          }).ToList();
                return rights;
            }
        }

        public void Dispose()
        {

        }

        public int UpdateRight(SysRightOperate model)
        {
            using (OriginEntities db = new OriginEntities())
            {
                SysRightOperate right = db.SysRightOperate.Where(a => a.Id == model.Id).FirstOrDefault();
                if (right != null)
                {
                    right.IsValid = model.IsValid;
                }
                else
                {
                    db.SysRightOperate.Add(model);
                }
                if (db.SaveChanges() > 0)
                {
                    var sysRight = (from r in db.SysRight
                                    where r.Id == model.RightId
                                    select r).First();
                    db.P_Sys_UpdateSysRightRightFlag(sysRight.ModuleId, sysRight.RoleId);
                    return 1;
                }
                return 0;
            }
        }

        public List<P_Sys_GetRightByRoleAndModule_Result> GetRightByRoleAndModule(string roleId, string moduleId)
        {
            List<P_Sys_GetRightByRoleAndModule_Result> results = null;
            using (OriginEntities db = new OriginEntities())
            {
                results = db.P_Sys_GetRightByRoleAndModule(roleId, moduleId).ToList();
            }
            return results;
        }
    }
}
