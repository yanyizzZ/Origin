using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Origin.Model.Sys;
using Origin.IBLL;
using Origin.IDAL;
using Origin.Common;
using Unity.Attributes;
using Origin.Model;
namespace Origin.BLL
{
    public class SysRoleBLL : BaseBLL, ISysRoleBLL
    {
        [Dependency]
        public ISysRoleDAL dal { get; set; }
        public List<SysRoleModel> GetList(ref GridPager pager, string queryStr)
        {
            IQueryable<SysRole> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = dal.GetList(db).Where(a => a.Name.Contains(queryStr));
            }
            else
            {
                queryData = dal.GetList(db);
            }
            pager.totalRows = queryData.Count();
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }
        private List<SysRoleModel> CreateModelList(ref IQueryable<SysRole> queryData)
        {
            List<SysRoleModel> modelList = new List<SysRoleModel>();
            foreach (var i in queryData)
            {
                modelList.Add(new SysRoleModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    CreateTime = i.CreateTime,
                    CreatePerson = i.CreatePerson,
                    UserName = ""
                });
            }
            return modelList;
        }
        public bool Create(ref ValidationErrors errors, SysRoleModel model)
        {
            try
            {
                SysRole entity = dal.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new SysRole();
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.CreateTime = model.CreateTime;
                entity.CreatePerson = model.CreatePerson;
                if (dal.Create(entity) == 1)
                {
                    //分配给角色
                    db.P_Sys_InsertSysRight();
                    //清理无用的项
                    db.P_Sys_ClearUnusedRightOperate();
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
            throw new NotImplementedException();
        }

        public bool Edit(ref ValidationErrors errors, SysRoleModel model)
        {
            throw new NotImplementedException();
        }

        public SysRoleModel GetById(string id)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(string id)
        {
            throw new NotImplementedException();
        }
    }
}
