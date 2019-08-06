using Origin.Common;
using Origin.IBLL;
using Origin.IDAL;
using Origin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Attributes;
namespace Origin.BLL
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class SysModuleBLL : BaseBLL, ISysModuleBLL
    {
        [Dependency]
        public ISysModuleDAL SysModuleDAL { get; set; }

        public List<SysModule> GetMenuByPersonId(int personId, string moduleId)
        {
            List<SysModule> modelList = SysModuleDAL.GetMenuByPersonId(personId, moduleId);

            return modelList;
        }

        public List<SysModule> GetList(string parentId)
        {
            IQueryable<SysModule> queryData = null;
            queryData = SysModuleDAL.GetList(db).Where(a => a.ParentId == parentId).OrderBy(a => a.Sort);
            return CreateModelList(ref queryData);
        }
        private List<SysModule> CreateModelList(ref IQueryable<SysModule> queryData)
        {
            List<SysModule> modelList = (from r in queryData
                                         select r).ToList();
            return modelList;
        }

        public List<SysModule> GetModuleBySystem(string parentId)
        {
            return SysModuleDAL.GetModuleBySystem(db, parentId).ToList();
        }

        public bool Create(ref ValidationErrors errors, SysModule model)
        {
            try
            {
                SysModule entity = SysModuleDAL.GetById(model.ModuleId);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = model;
                if (SysModuleDAL.Create(entity) == 1)
                {
                    //分配给角色
                    db.P_Sys_InsertSysRight();
                    return true;
                }
                else
                {
                    errors.Add(Suggestion.InsertFail);
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

        public bool Delete(ref ValidationErrors errors, string id)
        {
            try
            {
                //检查是否有下级
                if (db.SysModule.AsQueryable().Where(a => a.ParentId == id).Count() > 0)
                {
                    errors.Add("有下属关联，请先删除下属！");
                    return false;
                }
                SysModuleDAL.Delete(db, id);
                if (db.SaveChanges() > 0)
                {
                    //清理无用的项
                    db.P_Sys_ClearUnusedRightOperate();
                    return true;
                }
                else
                {
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

        public bool Edit(ref ValidationErrors errors, SysModule model)
        {
            try
            {
                SysModule entity = SysModuleDAL.GetById(model.ModuleId);
                if (entity == null)
                {
                    errors.Add(Suggestion.Disable);
                    return false;
                }
                entity = model;
                if (SysModuleDAL.Edit(entity) == 1)
                {
                    return true;
                }
                else
                {
                    errors.Add(Suggestion.EditFail);
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

        public SysModule GetById(string id)
        {
            if (IsExist(id))
            {
                SysModule entity = SysModuleDAL.GetById(id);
                return entity;
            }
            else
            {
                return null;
            }
        }

        public bool IsExist(string id)
        {
            return SysModuleDAL.IsExist(id);
        }


        public IQueryable<SysModule> GetList(OriginEntities db)
        {
            return SysModuleDAL.GetList(db);
        }
    }
}
