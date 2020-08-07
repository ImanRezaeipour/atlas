using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Business.Security;
using GTS.Clock.Model;
using NHibernate.Linq;
using NHibernate.Transaction;
using NHibernate.Criterion;
using NHibernate;
using GTS.Clock.Business.Temp;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Presentaion_Helper.Proxy;

namespace GTS.Clock.Business.Charts
{
    /// <summary>
    ///کلاس منطق چارت سازمانی 
    /// </summary>
    public class BDepartment : BaseBusiness<Department>
    {
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        private IDataAccess accessPort = new BUser();
        const string ExceptionSrc = "GTS.Clock.Business.Shifts.Business.Department";
        private DepartmentRepository departmentRepository = new DepartmentRepository(false);
        private BTemp bTemp = new BTemp();
        GTS.Clock.Business.RequestFlow.BUnderManagment bUnderManagment = new GTS.Clock.Business.RequestFlow.BUnderManagment();
        /// <summary>
        /// تعداد دسته بندی در بروز رسانی عملیات در دیتابیس
        /// </summary>
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);

        /// <summary>
        /// واکشی بخش ها بر اساس دسترسی کاربر جاری به بخش
        /// </summary>
        /// <returns></returns>
        public override IList<Department> GetAll()
        {
            try
            {
                IList<decimal> accessableIDs = accessPort.GetAccessibleDeparments();
                IList<Department> list = new List<Department>();
                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {

                    list = NHSession.QueryOver<Department>().Where(d => d.ID.IsIn(accessableIDs.ToList())).List<Department>();
                }
                else
                {
                    Department departmentAlias = null;
                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                    string operationGUID = this.bTemp.InsertTempList(accessableIDs);
                    list = NHSession.QueryOver(() => departmentAlias)
                                    .JoinAlias(() => departmentAlias.TempList, () => tempAlias)
                                    .Where(() => tempAlias.OperationGUID == operationGUID)
                                    .List<Department>();
                    this.bTemp.DeleteTempList(operationGUID);
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// واکشی کلیه بخش ها  بدون احتساب دسترسی کاربر جاری به بخش
        /// </summary>
        /// <returns>لیست بخش</returns>
        public IList<Department> GetAllWithoutDataAccess()
        {
            IList<Department> departmentsList = new List<Department>();
            departmentsList = this.NHSession.QueryOver<Department>()
                                            .List<Department>();
            return departmentsList;
        }

        /// <summary>
        /// ریشه درخت را برمیگرداند که با پیمایش گره های آن درخت استخراج میشود
        /// </summary>
        /// <returns>بخش</returns>
        public Department GetDepartmentsTree()
        {
            try
            {
                IList<Department> departmentsList = departmentRepository.GetDepartmentTree();

                if (departmentsList.Count == 1)
                {
                    return departmentsList.First();
                }
                else
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.DepartmentRootMoreThanOne, "تعداد ریشه بخشها در دیتابیس نامعتبر است", ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BDepartment", "GetDepartmentsTree");
                throw ex;
            }
        }

        /// <summary>
        /// زیر بخش های یک بخش را بر اساس دسترسی کاربر جاری به بخش ها برمی گرداند
        /// </summary>
        /// <param name="departmentId">کلید اصلی بخش</param>
        /// <returns>لیست بخش</returns>
        public IList<Department> GetDepartmentChilds(decimal departmentId)
        {
            try
            {
                IList<decimal> accessableIDs = accessPort.GetAccessibleDeparments();
                IList<Department> depList = new List<Department>();

                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {


                    depList = departmentRepository
                        .GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Department().Parent), new Department() { ID = departmentId }),
                                       new CriteriaStruct(Utility.GetPropertyName(() => new Department().ID), accessableIDs.ToArray(), CriteriaOperation.IN));
                }
                else
                {
                    Department departmentAlias = null;
                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                    string operationGUID = this.bTemp.InsertTempList(accessableIDs);
                    depList = NHSession.QueryOver(() => departmentAlias)
                        //.JoinAlias(() => departmentAlias.Parent, () => departmentAlias)
                                                      .JoinAlias(() => departmentAlias.TempList, () => tempAlias)
                                                      .Where(() => tempAlias.OperationGUID == operationGUID && departmentAlias.Parent.ID == departmentId)
                                                      .List<Department>();
                    this.bTemp.DeleteTempList(operationGUID);

                }
                if (depList != null && depList.Count > 0)
                {
                    depList = depList.OrderBy(x => x.CustomCode).ToList();
                }
                return depList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDepartment", "GetDepartmentChilds");
                throw ex;
            }
        }

        /// <summary>
        /// زیر بخش های یک گره را بر اساس پارامتر ورودی بر می گرداند
        /// </summary>
        /// <param name="departmentId">کلید بخشی که زیر گره های آن را می خواهیم جستجو کنیم</param>
        /// <param name="allDepartments">لیست بخش هایی که قصد جستجو روی آن را داریم</param>
        /// <returns>لیست بخش هایی که پیدا شده</returns>
        public IList<Department> GetDepartmentChilds(decimal departmentId, IList<Department> allDepartments)
        {
            try
            {
                IList<Department> list = new List<Department>();
                if (allDepartments != null && allDepartments.Count > 0)
                {
                    list = allDepartments.Where(x => x.ParentID == departmentId).ToList();
                }
                if (list != null && list.Count > 0)
                {
                    list = list.OrderBy(x => x.CustomCode).ToList();
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// زیر بخش های یک بخش را برمیگرداند
        /// </summary>
        /// <param name="departmentId">کلید اصلی بخش مورد نظر</param>
        /// <returns>لیست بخش</returns>
        public IList<Department> GetDepartmentChildsWithoutDA(decimal departmentId)
        {
            try
            {
                IList<Department> depList = new List<Department>();

                depList = departmentRepository
                    .GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Department().Parent), new Department() { ID = departmentId }));


                return depList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDepartment", "GetDepartmentChildsWithoutDA");
                throw ex;
            }
        }
        public IList<Department> GetDepartmentChildsWithoutDA(string SearchItem)
        {
            try
            {
                Department depAlias = null;
                IList<Department> DepList = new List<Department>();
                IList<Department> depList = NHSession.QueryOver(() => depAlias)
                                                     .Where(() => depAlias.Name.IsInsensitiveLike(SearchItem, MatchMode.Anywhere) ||
                                                                  depAlias.CustomCode.IsInsensitiveLike(SearchItem, MatchMode.Anywhere)
                                                           )
                                                     .List<Department>();
                foreach (Department d1 in depList)
                {
                    foreach (Department d2 in depList)
                    {
                        if (d2.ParentPathList.Contains(d1.ID))
                        {
                            DepList.Add(d2);
                        }
                    }
                }
                depList = depList.Except(DepList).ToList<Department>();
                return depList;

            }
            catch (Exception ex)
            {
                LogException(ex, "BDepartment", "GetDepartmentChildsWithoutDA");
                throw ex;
            }
        }
        /// <summary>
        /// زیر بخش های یک بخش را با استفاده از آدرس ریشه های آن برمیگرداند
        /// </summary>
        /// <param name="departmentId">کلید اصلی بخش</param>
        /// <returns></returns>
        public IList<Department> GetDepartmentChildsByParentPath(decimal parentId)
        {
            IList<Department> depList = departmentRepository.GetByCriteria(
                new CriteriaStruct(
                    Utility.GetPropertyName(() => new Department().ParentPath)
                    , String.Format(",{0},", parentId)
                    , CriteriaOperation.Like));
            if (depList != null && depList.Count > 0)
            {
                depList = depList.OrderBy(x => x.CustomCode).ToList();
            }
            return depList;
        }

        /// <summary>
        /// بازیابی دپارتمان یک پرسنل
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <returns>دپارتمان</returns>
        public Department GetDepartmentByPersonId(decimal personId)
        {
            return departmentRepository.GetByPersonId(personId);
        }

        /// <summary>
        /// بازیابی سازمان یک پرسنل
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <returns>سازمان</returns>
        public Department GetOraganizationByPersonId(decimal personId)
        {
            return departmentRepository.GetOraganizationByPersonId(personId);
        }

        #region استخراج بخش بر اساس دسترسی های مختلف

        /// <summary>
        /// درخت بخش های تحت مدیریت یک مدیر را برمیگرداند
        /// </summary>
        /// <param name="managerId">کلید اصلی مدیر</param>
        /// <returns>بخش</returns>
        public Department GetManagerDepartmentTree(decimal managerId)
        {
            try
            {
                GTS.Clock.Business.RequestFlow.BManager bmanager = new GTS.Clock.Business.RequestFlow.BManager();
                GTS.Clock.Model.RequestFlow.Manager mng = bmanager.GetByID(managerId);

                List<decimal> nodeParentChildsId = new List<decimal>();
                var flows = new List<GTS.Clock.Model.RequestFlow.Flow>();
                flows.AddRange(from n in mng.ManagerFlowList
                               where !n.Flow.IsDeleted && n.Active && n.Flow.ActiveFlow
                               select n.Flow);

                #region اگر این شخص جانشین هم باشد
                SubstituteRepository subRep = new SubstituteRepository(false);
                if (subRep.IsSubstitute(Security.BUser.CurrentUser.Person.ID))
                {
                    IList<GTS.Clock.Model.RequestFlow.Substitute> subList = subRep.GetSubstitute(Security.BUser.CurrentUser.Person.ID);
                    foreach (GTS.Clock.Model.RequestFlow.Substitute sub in subList)
                    {
                        flows.AddRange(from n in sub.Manager.ManagerFlowList
                                       where n.Active && !n.Flow.IsDeleted && n.Flow.ActiveFlow
                                       select n.Flow);
                    }
                }
                #endregion

                Department root = this.GetDepartmentsTree();
                IList<GTS.Clock.Model.RequestFlow.Flow> flowList = flows.ToList();
                IList<Department> departmentsList = new BDepartment().GetAllWithoutDataAccess();
                root.Visible = true;

                var bFlow = new GTS.Clock.Business.RequestFlow.BFlow();
                foreach (GTS.Clock.Model.RequestFlow.Flow flow in flowList.Distinct())
                {
                    SetVisibility(bFlow, root, flow, this.GetDepartmentChilds(flow, root.ID, departmentsList), departmentsList);
                }
                this.RemoveNotVisibleChilds(root);
                return root;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// بخشهای تحت مدیریت یک اپراتور را برمی گرداند
        /// </summary>
        /// <param name="operatorPersonId">کلید اصلی اپراتور</param>
        /// <returns>بخش</returns>
        public Department GetOperatorDepartmentTree(decimal operatorPersonId)
        {
            try
            {
                GTS.Clock.Business.RequestFlow.BOperator boperator = new GTS.Clock.Business.RequestFlow.BOperator();
                IList<GTS.Clock.Model.RequestFlow.Operator> opList = boperator.GetOperator(operatorPersonId);

                List<decimal> nodeParentChildsId = new List<decimal>();
                var flows = from n in opList
                            where n.Active && !n.Flow.IsDeleted && n.Flow.ActiveFlow
                            select n.Flow;
                Department root = this.GetDepartmentsTree();
                IList<GTS.Clock.Model.RequestFlow.Flow> flowList = flows.ToList();
                IList<Department> departmentsList = new BDepartment().GetAllWithoutDataAccess();
                root.Visible = true;
                var bFlow = new GTS.Clock.Business.RequestFlow.BFlow();
                foreach (GTS.Clock.Model.RequestFlow.Flow flow in flowList)
                {
                    SetVisibility(bFlow, root, flow, bFlow.GetDepartmentChilds(root.ID, flow.ID, departmentsList), departmentsList);
                }
                this.RemoveNotVisibleChilds(root);
                return root;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// لیست بخش های تحت مدیریت یک مدیر را برمیگرداند
        /// جهت نمایش بخش ها در جستجوی پیشرفته
        /// </summary>
        /// <param name="managerId">کلید اصلی مدیر</param>
        /// <returns>لیست بخش</returns>
        public IList<Department> GetAllManagerDepartmentTree(decimal managerId)
        {
            try
            {
                Department root = this.GetManagerDepartmentTree(managerId);
                IList<Department> nodes = new List<Department>();
                nodes.Add(root);
                this.GetInnerNodes(root.ChildList, nodes);
                return nodes;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// درخت بخش های تحت مدیریت یک مدیر را برمیگرداند
        /// سازمان بر میگردد
        /// جانشینی لحاظ نمیگردد
        /// فقط جریان اصلی لحاظ میگردد
        /// </summary>
        /// <param name="managerId">کلید اصلی مدیر</param>
        /// <returns>بخش</returns>
        public Department GetManagerDepartmentTree_JustOrgan(decimal managerId)
        {
            try
            {
                GTS.Clock.Business.RequestFlow.BManager bmanager = new GTS.Clock.Business.RequestFlow.BManager();
                GTS.Clock.Model.RequestFlow.Manager mng = bmanager.GetByID(managerId);

                List<decimal> nodeParentChildsId = new List<decimal>();
                var flows = new List<GTS.Clock.Model.RequestFlow.Flow>();
                flows.AddRange(from n in mng.ManagerFlowList
                               where !n.Flow.IsDeleted && n.Active && n.Flow.ActiveFlow && n.Flow.MainFlow
                               select n.Flow);

                Department root = this.GetDepartmentsTree();
                IList<GTS.Clock.Model.RequestFlow.Flow> flowList = flows.ToList();
                if (flowList.Count() > 0)
                {
                    IList<Department> departmentsList = new BDepartment().GetAllWithoutDataAccess();
                    root = departmentsList.Where(x =>
                                                    x.DepartmentType == DepartmentType.Organization &&
                                                    flowList.First().UnderManagmentList.Any(y => y.Department.ID == x.ID || y.Department.ParentPath.Contains("," + x.ID.ToString() + ","))
                                                )
                                            .FirstOrDefault();//ریشه سازمان
                    if (root == null)
                    {
                        root = new Department();
                    }
                    departmentsList = departmentsList.Where(x => x.DepartmentType == DepartmentType.Organization || x.DepartmentType == DepartmentType.Assistance || x.DepartmentType == DepartmentType.Management || x.DepartmentType == DepartmentType.Unit).ToList();
                    root.Visible = true;
                    var bFlow = new GTS.Clock.Business.RequestFlow.BFlow();
                    foreach (GTS.Clock.Model.RequestFlow.Flow flow in flowList.Distinct())
                    {
                        SetVisibility(bFlow, root, flow, bFlow.GetDepartmentChilds(root.ID, flow.ID, departmentsList), departmentsList);
                    }
                    this.RemoveNotVisibleChilds(root);
                    return root;
                }
                else
                {
                    return new Department();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// لیست بخش های تحت مدیریت یک مدیر را برمیگرداند
        /// جهت نمایش بخش ها در جستجوی پیشرفته
        /// تنها سازمان ، معاونت و اداره بر میگردد
        /// </summary>
        /// <param name="managerId">کلید اصلی مدیر</param>
        /// <returns>لیست بخش</returns>
        public IList<Department> GetAllManagerDepartmentTree_JustOrgan(decimal managerId)
        {
            try
            {
                Department root = this.GetManagerDepartmentTree_JustOrgan(managerId);
                IList<Department> nodes = new List<Department>();
                nodes.Add(root);
                this.GetInnerNodes(root.ChildList, nodes);
                return nodes;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// لیست بخش های تحت مدیریت اپراتور را بر میگرداند
        /// جهت نمایش بخش ها در جستجوی پیشرفته
        /// </summary>
        /// <param name="operatorPersonId">کلید اصلی اپراتور</param>
        /// <returns>لیست بخش ها</returns>
        public IList<Department> GetAllOperatorDepartmentTree(decimal operatorPersonId)
        {
            try
            {
                Department root = this.GetOperatorDepartmentTree(operatorPersonId);
                IList<Department> nodes = new List<Department>();
                nodes.Add(root);
                this.GetInnerNodes(root.ChildList, nodes);
                return nodes;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// درخت بخشهای تحت مدیریت یک مدیر را برمیگرداند
        /// </summary>
        /// <param name="managerId">کلید اصلی مدیر</param>
        /// <param name="parentId">؟؟؟</param>
        /// <returns>لیست بخش ها</returns>
        public IList<Department> GetManagerDepartmentTree(decimal managerId, decimal parentId)
        {
            try
            {
                GTS.Clock.Business.RequestFlow.BManager bmanager = new GTS.Clock.Business.RequestFlow.BManager();
                GTS.Clock.Model.RequestFlow.Manager mng = bmanager.GetByID(managerId);

                List<decimal> nodeParentChildsId = new List<decimal>();
                var flows = from n in mng.ManagerFlowList
                            where n.Active && !n.Flow.IsDeleted && n.Flow.ActiveFlow
                            select n.Flow;
                Department root = this.GetByID(parentId);
                IList<GTS.Clock.Model.RequestFlow.Flow> flowList = flows.ToList();
                IList<Department> departmentsList = new BDepartment().GetAll();
                var bFlow = new GTS.Clock.Business.RequestFlow.BFlow();
                foreach (GTS.Clock.Model.RequestFlow.Flow flow in flowList)
                {
                    SetVisibility(bFlow, root, flow, bFlow.GetDepartmentChilds(root.ID, flow.ID, departmentsList), departmentsList);
                }
                this.RemoveNotVisibleChilds(root);
                return root.ChildList;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// درخت بخشهای تحت مدیریت یک مدیر را برمیگرداند
        /// </summary>
        /// <param name="managerId">کلید اصلی مدیر</param>
        /// <returns>لیست بخش ها</returns>
        public IList<Department> GetOperatorDepartmentTree(decimal operatorPersonId, decimal parentId)
        {
            try
            {
                GTS.Clock.Business.RequestFlow.BOperator boperator = new GTS.Clock.Business.RequestFlow.BOperator();
                IList<GTS.Clock.Model.RequestFlow.Operator> opList = boperator.GetOperator(operatorPersonId);

                List<decimal> nodeParentChildsId = new List<decimal>();
                var flows = from n in opList
                            where n.Active && !n.Flow.IsDeleted && n.Flow.ActiveFlow
                            select n.Flow;
                Department root = this.GetByID(parentId);
                IList<GTS.Clock.Model.RequestFlow.Flow> flowList = flows.ToList();
                IList<Department> departmentsList = new BDepartment().GetAll();

                var bFlow = new GTS.Clock.Business.RequestFlow.BFlow();
                foreach (GTS.Clock.Model.RequestFlow.Flow flow in flowList)
                {
                    SetVisibility(bFlow, root, flow, bFlow.GetDepartmentChilds(root.ID, flow.ID, departmentsList), departmentsList);
                }
                this.RemoveNotVisibleChilds(root);
                return root.ChildList;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// زیر بخش های یک بخش را بر اساس بخش های تحت مدیریت برمیگرداند
        /// </summary>
        /// <param name="nodeID">کلید اصلی بخش مورد نظر</param>
        /// <param name="flowId">کلید اصلی جریان کاری</param>
        /// <param name="departmentsList">لیست بخش مورد نظر که در آن جستجو می کنیم</param>
        /// <returns>لیست بخش ها</returns>
        public IList<Department> GetDepartmentChilds(GTS.Clock.Model.RequestFlow.Flow flow, decimal nodeID, IList<Department> departmentsList)
        {
            try
            {
                // why?
                //DepartmentRepository depRep = new DepartmentRepository(false);
                //GTS.Clock.Model.RequestFlow.Flow flow = new GTS.Clock.Business.RequestFlow.BFlow().GetByID(flowId);
                List<Department> underManagmentTree = new List<Department>();
                IList<Department> containsNode = bUnderManagment.GetUnderManagmentDepartmentByFlow(flow, true, departmentsList);
                foreach (Department dep in containsNode)
                {
                    underManagmentTree.Add(dep);
                }
                IList<Department> childs = departmentsList.Where(x => x.ParentID == nodeID).ToList<Department>();
                IList<Department> result = new List<Department>();
                foreach (Department child in childs)
                {
                    if (underManagmentTree.Where(x => x.ID == child.ID).Any())
                    {
                        result.Add(child);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }


        #endregion

        /// <summary>
        /// بصورت بازگشتی درحت را پیمایش و شرط نمایش را بررسی میکند
        ///  اگر تشخیص داده شد که گره ای نباید نشان داده شود نیازی به پیمایش گره های فرزند نیست
        ///  زیرا این تشخیص شامل آنها نیز میشود
        /// </summary>
        /// <param name="department"></param>
        /// <param name="visibleIds"></param>
        //private void SetVisibility(Department department, GTS.Clock.Model.RequestFlow.Flow flow, IList<Department> containsChildList)
        //{
        //    GTS.Clock.Business.RequestFlow.BFlow bFlow = new GTS.Clock.Business.RequestFlow.BFlow();            
        //    if (department.ChildList != null)
        //    {
        //        foreach (Department child in department.ChildList)
        //        {
        //            if (!containsChildList.Contains(child))
        //            {
        //                child.Visible = child.Visible || false;//ممکن است در جریانهای قبلی مقدار یک گرفته باشد
        //            }
        //            else
        //            {
        //                child.Visible = true;
        //                this.SetVisibility(child, flow, bFlow.GetDepartmentChilds(child.ID, flow.ID));
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// بررسی امکان رویت بخش ها
        /// بصورت بازگشتی درحت را پیمایش و شرط نمایش را بررسی میکند
        ///  اگر تشخیص داده شد که بخش ای نباید نشان داده شود نیازی به پیمایش زیر بخش های فرزند نیست
        ///  زیرا این تشخیص شامل آنها نیز میشود
        /// </summary>
        /// <param name="department">بخش</param>
        /// <param name="flow">جریان کاری</param>
        /// <param name="containsChildList">لیست زیر بخش ها</param>
        /// <param name="departmentsList">لیست بخش های که روی آن باید پیمایش شود</param>
        private void SetVisibility(GTS.Clock.Business.RequestFlow.BFlow bFlow, Department department, GTS.Clock.Model.RequestFlow.Flow flow, IList<Department> containsChildList, IList<Department> departmentsList)
        {

            if (departmentsList.Where(x => x.ParentID == department.ID).Any())
            {
                foreach (Department child in departmentsList.Where(x => x.ParentID == department.ID).ToList<Department>())
                {
                    if (!containsChildList.Contains(child))
                    {
                        child.Visible = child.Visible || false;//ممکن است در جریانهای قبلی مقدار یک گرفته باشد
                    }
                    else
                    {
                        child.Visible = true;
                        this.SetVisibility(bFlow, child, flow, bFlow.GetDepartmentChilds(child.ID, flow.ID, departmentsList), departmentsList);
                    }
                }
            }
        }


        /// <summary>
        /// زیر بخش هایی را که قابلیت رویت ندارند را به صورت بازگشتی حذف می کند
        /// </summary>
        /// <param name="department">بخش مورد نظری که روی آن پیمایش انجام می شود</param>
        private void RemoveNotVisibleChilds(Department department)
        {
            if (department != null && department.ChildList != null)
            {
                department.ChildList = department.ChildList.Where(x => x.Visible).ToList();
                foreach (Department dep in department.ChildList)
                {
                    this.RemoveNotVisibleChilds(dep);
                }
            }
        }

        /// <summary>
        /// تبدیل درخت بخش ها به صورت لیستی تک سطحی از بخش ها
        /// </summary>
        /// <param name="deps">درخت بخش</param>
        /// <param name="allNodes">لیست تک سطحی بخش ها جهت خروجی به صورت رفرنس</param>
        private void GetInnerNodes(IList<Department> deps, IList<Department> allNodes)
        {
            if (deps != null && deps.Count > 0)
            {
                foreach (Department dep in deps)
                {
                    allNodes.Add(dep);
                    this.GetInnerNodes(dep.ChildList, allNodes);
                }
            }
        }

        /// <summary>
        /// جستجوی بخش ها
        /// </summary>
        /// <param name="field">فیلد مورد نظر جهت جستجو</param>
        /// <param name="searchVal">عبارت جستجو</param>
        /// <returns>لیست بخش ها</returns>
        public IList<Department> SearchDepartment(DepartmentSearchFields field, string searchVal)
        {
            try
            {

                IList<Department> list = new List<Department>();
                IList<decimal> accessableIDs = accessPort.GetAccessibleDeparments();
                switch (field)
                {
                    case DepartmentSearchFields.DepartmentName:
                    case DepartmentSearchFields.NotSpec:
                        if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                        {
                            list =
                                NHibernateSessionManager.Instance.GetSession().QueryOver<Department>()
                                                                              .Where(x => x.ID.IsIn(accessableIDs.ToList()) &&
                                                                                           (x.Name.IsInsensitiveLike(searchVal, MatchMode.Anywhere) || x.CustomCode.IsInsensitiveLike(searchVal, MatchMode.Anywhere))
                                                                                     )
                                                                              .List<Department>();
                        }
                        else
                        {
                            Department departmentAlias = null;
                            GTS.Clock.Model.Temp.Temp tempAlias = null;
                            string operationGUID = this.bTemp.InsertTempList(accessableIDs);
                            list = NHSession.QueryOver(() => departmentAlias)
                                                              .JoinAlias(() => departmentAlias.TempList, () => tempAlias)
                                                              .Where(() => tempAlias.OperationGUID == operationGUID
                                                                           && (departmentAlias.Name.IsInsensitiveLike(searchVal, MatchMode.Anywhere) || (departmentAlias.CustomCode.IsInsensitiveLike(searchVal, MatchMode.Anywhere))))
                                                              .List<Department>();
                            this.bTemp.DeleteTempList(operationGUID);
                        }
                        break;
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BDepartment", "SearchDepartment");
                throw ex;
            }
        }

        /// <summary>
        /// اعتبار سنجی عملیات درج بخش جدید
        /// نام خالی نباشد
        /// شناسه والد معتبر باشد
        /// اعتبار سنجی آیتمی که قرار است درج شود
        /// شناسه والد خالی نباشد
        /// نام در گرهای همسطح تکراری نباشد
        /// کد تعریف شده نباید تکراری باشد
        /// </summary>
        /// <param name="dep">بخش مورد نظر</param>
        /// <param name="exception">در صورت عدم معتبر بودن بخش خطا رخ می دهد</param>
        protected override void InsertValidate(Department dep)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(dep.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DepNameRequierd, "نام بخش باید مشخص شود", ExceptionSrc));
            }

            if (Utility.IsEmpty(dep.ParentID))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DepParentIDRequierd, "نام والد بخش باید مشخص شود", ExceptionSrc));
            }

            if (departmentRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => dep.ID), dep.ParentID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DepParentNotExists, "دیپارتمان والدی با این شناسه موجود نمیباشد", ExceptionSrc));
            }

            else if (departmentRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => dep.Name), dep.Name),
                                                                  new CriteriaStruct(Utility.GetPropertyName(() => dep.Parent), dep.Parent)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DepartmentRepeatedName, "نام بخش در یک سطح نباید تکراری باشد", ExceptionSrc));
            }

            if (!Utility.IsEmpty(dep.CustomCode))
            {
                if (departmentRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => dep.CustomCode), dep.CustomCode)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.DepartCustomCodeRepeated, "درج - کد تعریف شده در بخش نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبار سنجی عملیات ویرایش بخش
        /// نام خالی نباشد
        /// شناسه والد معتبر باشد
        /// اعتبار سنجی آیتمی که قرار است بروزرسانی شود
        /// نام در گرهای همسطح تکراری نباشد
        /// کد تعریف شده نباید تکراری باشد
        /// </summary>
        /// <param name="dep">بخش مورد نظر</param>
        /// <param name="exception">در صورت عدم معتبر بودن بخش خطا رخ می دهد</param>
        protected override void UpdateValidate(Department dep)
        {
            // والد یک گره بروزرسانی نمیشود .همچنین بنا به محدودیتهای کلاینت هنگام بروزرسانی والد مقداردهی نمیشود
            dep.ParentID = departmentRepository.GetParentID(dep.ID);

            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(dep.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DepNameRequierd, "نام بخش باید مشخص شود", ExceptionSrc));
            }

            else if (dep.ParentID != 0 &&
                departmentRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => dep.Name), dep.Name),
                                                                  new CriteriaStruct(Utility.GetPropertyName(() => dep.Parent), dep.Parent),
                                                                  new CriteriaStruct(Utility.GetPropertyName(() => dep.ID), dep.ID, CriteriaOperation.NotEqual)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DepartmentRepeatedName, "نام بخش در یک سطح نباید تکراری باشد", ExceptionSrc));
            }

            if (!Utility.IsEmpty(dep.CustomCode))
            {
                if (departmentRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => dep.CustomCode), dep.CustomCode),
                                                                 new CriteriaStruct(Utility.GetPropertyName(() => dep.ID), dep.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.DepartCustomCodeRepeated, "بروزرسانی - کد تعریف شده در بخش نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبار سنجی عملیات حذف بخش 
        /// </summary>
        /// <param name="dep">بخش مورد نظر</param>
        /// <param name="exception">در صورت عدم معتبر بودن بخش خطا رخ می دهد</param>
        protected override void DeleteValidate(Department dep)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            PersonRepository personRep = new PersonRepository(false);
            if (departmentRepository.IsRoot(dep.ID))
            {
                exception.Add(ExceptionResourceKeys.DepartmentRootDeleteIllegal, "ریشه قابل حذف نیست", ExceptionSrc);
                throw exception;
            }

            //int count = personRep.GetCountByCriteria(new CriteriaStruct(/*Utility.GetPropertyName(() => new Model.Person().Department)*/"department", dep));
            IList<Person> personList = personRep.GetByCriteria(new CriteriaStruct("department", new Department() { ID = dep.ID }));
            if (personList.Count(p => p.IsDeleted == false) > 0)
            {
                exception.Add(ExceptionResourceKeys.DepUsedByPersons, "این بخش به اشخاص انتساب داده شده است", ExceptionSrc);
                throw exception;

            }
            IList<Department> depList = this.GetDepartmentChildsByParentPath(dep.ID);
            foreach (Department depItem in depList)
            {
                if (depItem.PersonList != null && depItem.PersonList.Count(p => p.IsDeleted == false) > 0)
                {
                    exception.Add(ExceptionResourceKeys.ChildsDepUsedByPersons, "زیر بخش های این بخش به اشخاص انتساب داده شده است", ExceptionSrc);
                    throw exception;
                }
            }
            if (personList.Count > 0)
            {
                Department depRootObj = NHibernateSessionManager.Instance.GetSession().QueryOver<Department>()
                                                                                  .Where(x => x.ParentPath == null).SingleOrDefault();
                foreach (Person item in personList)
                {
                    BPerson personBussiness = new BPerson();
                    item.Department = depRootObj;
                    personBussiness.UpdatePerson(item, UIActionType.EDIT);
                }
            }
            departmentRepository.DelateHierarchicalByParentId(dep.ID);

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// مقدار دهی پیش فرض برای خصیصه های خالی
        /// مقداردهی مسیر والد
        /// </summary>
        /// <param name="dep">بخش مورد نظر</param>
        /// <param name="action">نوع عملیات</param>
        protected override void GetReadyBeforeSave(Department dep, UIActionType action)
        {
            if (action == UIActionType.ADD && dep.ParentID > 0)
            {
                Department parent = base.GetByID(dep.ParentID);
                dep.ParentPath = parent.ParentPath + String.Format(",{0},", dep.ParentID);
            }
            else if (action == UIActionType.EDIT)
            {
                Department node = base.GetByID(dep.ID);
                dep.ParentPath = node.ParentPath;
                NHibernateSessionManager.Instance.ClearSession();
            }
        }

        /// <summary>
        /// عملیات جانبی بعد از ذخیره بخش در دیتابیس
        /// </summary>
        /// <param name="obj">بخش مورد نظر</param>
        /// <param name="action">نوع عملیات</param>
        protected override void OnSaveChangesSuccess(Department obj, UIActionType action)
        {
            if (action == UIActionType.ADD)
            {
                new BDataAccess().InsertDataAccess(Infrastructure.DataAccessLevelOperationType.Single, Infrastructure.DataAccessParts.Department, obj.ID, BUser.CurrentUser.ID, null, "");
            }
        }
        public IList<Department> GetParentDepartments(int departmentId)
        {
            Department departmentAlias = null;
            IList<Department> ParentDepartmentsList = new List<Department>();
            Department DepList = NHSession.QueryOver<Department>()
                                                     .Where(x => x.ID == departmentId)
                                                     .SingleOrDefault();
            if (DepList != null)
            {
                if (DepList.ParentPathList.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {
                    ParentDepartmentsList = NHSession.QueryOver(() => departmentAlias)
                                                              .Where(() => departmentAlias.ID.IsIn(DepList.ParentPathList.ToArray()))
                                                              .List<Department>();
                }
                else
                {
                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                    string operationGUID = this.bTemp.InsertTempList(DepList.ParentPathList);
                    ParentDepartmentsList = NHSession.QueryOver(() => departmentAlias)
                                                    .JoinAlias(() => departmentAlias.TempList, () => tempAlias)
                                                    .Where(() => tempAlias.OperationGUID == operationGUID)
                                                    .List<Department>();
                    this.bTemp.DeleteTempList(operationGUID);
                }
            }
            return ParentDepartmentsList;
        }

        //string ParentPathList = NHSession.QueryOver<Department>()
        //                                         .Where(x => x.ID == departmentId)
        //                                         .Select(x => x.ParentPath)
        //                                         .ToString();
        //string[] ParentDepartmentAray = ParentPathList.Split(',');

        //foreach (string depId in ParentDepartmentAray)
        //{
        //    if (depId != null && depId != string.Empty && depId != ",")
        //        ParentDepIds.Add(Convert.ToInt32(depId));
        //}     
        //List<decimal> ParentDepartmentIds = NHSession.QueryOver(() => departmentAlias)
        //                                    .Where(() => departmentAlias.ID == departmentId)
        //                                    .Select(() => departmentAlias.ParentPath)
        //                                    .List<decimal>();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckDepartmentsLoadAccess()
        {
        }

        /// <summary>
        /// ذخیره بخش جدید
        /// </summary>
        /// <param name="department">بخش</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی بخش ذخیره شده</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertDepartment(Department department, UIActionType UAT)
        {
            return base.SaveChanges(department, UAT);
        }

        /// <summary>
        /// ویرایش بخش 
        /// </summary>
        /// <param name="department">بخش</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی بخش ذخیره شده</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateDepartment(Department department, UIActionType UAT)
        {
            return base.SaveChanges(department, UAT);
        }

        /// <summary>
        /// حذف بخش 
        /// </summary>
        /// <param name="department">بخش</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی بخش ذخیره شده</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteDepartment(Department department, UIActionType UAT)
        {
            return base.SaveChanges(department, UAT);
        }

        public IList<Department> GetAllDepartmentParents(decimal departmentID)
        {
            try
            {
                return this.departmentRepository.GetAllDepartmentParents(departmentID);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }


    }
}
