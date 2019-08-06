using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Origin.IBLL;
using Origin.Model.Sys;
using Origin.Common;
using Origin.IDAL;
using Unity.Attributes;
using Origin.Model;
namespace Origin.BLL
{
    public class SysModuleOperateBLL : BaseBLL, ISysModuleOperateBLL
    {
        [Dependency]
        public ISysModuleOperateDAL dal { get; set; }
        public List<SysModuleOperateModel> GetList(ref GridPager pager, string mid)
        {
            IQueryable<SysModuleOperate> queryData = dal.GetList(db).Where(a => a.ModuleId == mid);
            pager.totalRows = queryData.Count();
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }
        private List<SysModuleOperateModel> CreateModelList(ref IQueryable<SysModuleOperate> queryData)
        {

            List<SysModuleOperateModel> modelList = (from r in queryData
                                                     select new SysModuleOperateModel
                                                     {
                                                         Id = r.Id,
                                                         Name = r.Name,
                                                         KeyCode = r.KeyCode,
                                                         ModuleId = r.ModuleId,
                                                         IsValid = r.IsValid,
                                                         Sort = r.Sort
                                                     }).ToList();
            return modelList;
        }
        public bool Create(ref ValidationErrors errors, SysModuleOperateModel model)
        {
            try
            {
                SysModuleOperate entity = dal.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new SysModuleOperate();
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.KeyCode = model.KeyCode;
                entity.ModuleId = model.ModuleId;
                entity.IsValid = model.IsValid;
                entity.Sort = model.Sort;
                if (dal.Create(entity) == 1)
                {
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
                if (dal.Delete(id) == 1)
                {
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

        public SysModuleOperateModel GetById(string id)
        {
            if (IsExist(id))
            {
                SysModuleOperate entity = dal.GetById(id);
                SysModuleOperateModel model = new SysModuleOperateModel();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.KeyCode = entity.KeyCode;
                model.ModuleId = entity.ModuleId;
                model.IsValid = entity.IsValid;
                model.Sort = entity.Sort;
                return model;
            }
            else
            {
                return null;
            }
        }

        public bool IsExist(string id)
        {
            if (db.SysModuleOperate.SingleOrDefault(a => a.Id == id) != null)
            {
                return true;
            }
            return false;
        }
    }
}
