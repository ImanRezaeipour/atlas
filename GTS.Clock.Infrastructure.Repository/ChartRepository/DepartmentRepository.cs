using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.NHibernateFramework;
using NHibernate.Transform;

namespace GTS.Clock.Infrastructure.Repository
{
    public class DepartmentRepository : RepositoryBase<GTS.Clock.Model.Charts.Department>, IDepartmentRepository
    {
        TempRepository tempRepository = new TempRepository(false);
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);

        public override string TableName
        {
            get { return "TA_Department"; }
        }

        public DepartmentRepository(bool Disconnectedly)
            : base(Disconnectedly)
        { }

        #region Model Interface

        public IList<Department> GetDepartmentTree()
        {
            ICriteria crit = this.NHibernateSession.CreateCriteria(typeof(Department));
            crit.Add(Expression.Or(
                Expression.IsNull("Parent"),
                Expression.Eq("Parent.ID", Convert.ToDecimal(0))));

            IList<Department> parents = crit.List<Department>();

            return parents;
        }

        public decimal GetParentID(decimal departmentID)
        {
            string SQLCommand = String.Format("SELECT dep_ParentID FROM TA_Department " +
                                                "WHERE dep_ID = {0} ", departmentID);

            IQuery query = base.NHibernateSession.CreateSQLQuery(SQLCommand);

            object parentID = query.List<object>().FirstOrDefault();
            if (parentID != null)
                return (decimal)parentID;
            return 0;
        }

        public bool IsRoot(decimal departmentID)
        {
            if (GetParentID(departmentID) == 0)
                return true;
            return false;
        }

        /// <summary>
        /// لیست بچهای یک گره را با توجه به آدرس والدین برمیگرداند
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        //public IList<Department> GetChilds(decimal parentId) 
        //{
        //    IList<Department> depList = base.GetByCriteria(new CriteriaStruct(Utility.Utility.GetPropertyName(() => new Department().ParentPath), String.Format(",{0},", parentId), CriteriaOperation.Like));
        //    return depList;
        //}

        public IList<Department> GetById(IList<decimal> idList)
        {
            string HQLCommand = string.Empty;
            IQuery query = null;
            IList<Department> list = null;
            if (idList.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
            {
                HQLCommand = "FROM Department " +
                             "WHERE ID in (:idList) ";
                query = base.NHibernateSession.CreateQuery(HQLCommand)
                                              .SetParameterList("idList", base.CheckListParameter(idList));
                list = query.List<Department>();
            }
            else
            {
                string operationGUID = this.tempRepository.InsertTempList(idList);
                HQLCommand = "select department from Department department" +
                             "join department.TempList temp" +
                             "WHERE temp.OperationGUID = :operationGUID";
                query = base.NHibernateSession.CreateQuery(HQLCommand)
                                              .SetParameterList("operationGUID", operationGUID);
                list = query.List<Department>();
                this.tempRepository.DeleteTempList(operationGUID);
            }
            return list;
        }

        /// <summary>
        /// بازیابی دپارتمان یک پرسنل
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <returns>دپارتمان</returns>
        public Department GetByPersonId(decimal personId)
        {
            IQuery Query = null;
            Department item = null;

            string SQLCommand = @"DECLARE @PeronId decimal
                                SET @PeronId=:personId
                                SELECT TOP 1 
                                TA_Department.dep_ID AS ID
                                ,TA_Department.dep_Name AS Name
                                ,TA_Department.dep_CustomCode AS CustomCode
                                ,TA_Department.dep_ParentID AS ParentID
                                ,TA_Department.dep_ParentPath AS ParentPath
                                ,TA_Department.dep_DepartmentType AS DepartmentType
                                FROM TA_Department
                                JOIN TA_Person on TA_Person.Prs_DepartmentId=TA_Department.dep_ID
                                WHERE TA_Person.Prs_ID=@PeronId";

            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand).SetParameter("personId", personId);

            Query = Query.SetResultTransformer(Transformers.AliasToBean(typeof(Department)));
            item = Query.List<Department>().FirstOrDefault();

            return item;
        }


        /// <summary>
        /// بازیابی سازمان یک پرسنل
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <returns>سازمان</returns>
        public Department GetOraganizationByPersonId(decimal personId)
        {
            IQuery Query = null;
            Department item = null;

            string SQLCommand = @"DECLARE @PeronId decimal
                                SET @PeronId=:personId
                                SELECT TOP 1 
                                 Org.dep_ID AS ID
                                ,Org.dep_Name AS Name
                                ,Org.dep_CustomCode AS CustomCode
                                ,Org.dep_ParentID AS ParentID
                                ,Org.dep_ParentPath AS ParentPath
                                ,Org.dep_DepartmentType AS DepartmentType

                                FROM TA_Department
                                JOIN TA_Person on TA_Person.Prs_DepartmentId=TA_Department.dep_ID
                                JOIN TA_Department as Org on  TA_Department.dep_ParentPath like '%'+convert(varchar(50),Org.dep_ID)+'%' and Org.dep_DepartmentType=4
                                WHERE TA_Person.Prs_ID=@PeronId";

            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand).SetParameter("personId", personId);

            Query = Query.SetResultTransformer(Transformers.AliasToBean(typeof(Department)));
            item = Query.List<Department>().FirstOrDefault();

            return item;
        }

        /// <summary>
        /// بصورت سلسله مراتبی حذف میکند
        /// </summary>
        /// <param name="parentId"></param>
        public void DelateHierarchicalByParentId(decimal parentId)
        {
            string SQLCommand = String.Format("DELETE FROM TA_Department where dep_ParentPath like('%,{0},%')", parentId);
            base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .ExecuteUpdate();
        }

        #endregion


        public IList<Department> GetAllDepartmentParents(decimal departmentID)
        {
            IList<Department> DepartmentsList = new List<Department>();

            Department childDepartment = NHibernateSession.QueryOver<Department>()
                            .Where(department => department.ID == departmentID)
                            .List<Department>()
                            .FirstOrDefault();

            if (childDepartment != null)
                DepartmentsList.Add(childDepartment);
            else
                return DepartmentsList;

            this.GetAllDepartmentParents(ref DepartmentsList, childDepartment);
            return DepartmentsList.Reverse().ToList<Department>();
        }

        private void GetAllDepartmentParents(ref IList<Department> DepartmentsList, Department childDepartment)
        {
            Department parentDepartment = NHibernateSession.QueryOver<Department>()
                                          .Where(department => department.ID == childDepartment.ParentID)
                                          .List<Department>()
                                          .FirstOrDefault();
            if (parentDepartment != null)
            {
                DepartmentsList.Add(parentDepartment);
                this.GetAllDepartmentParents(ref DepartmentsList, parentDepartment);
            }
        }
        public IList<Department> GetSearchDepartmentsOfUser(decimal userId, string SearchItem)
        {           
            string SqlCommand = @"  select * from  TA_Department
							        INNER JOIN TA_DataAccessDepartment 
                                  on dep_ID=DataAccessDep_DepID OR 
							        dep_ParentPath like '%,' + CONVERT(varchar(10),DataAccessDep_DepID) + ',%'
							        WHERE DataAccessDep_UserID =:userId AND (dep_Name LIKE :SearchItem or dep_CustomCode LIKE :SearchItem)";


            IList<Department> DepartmentList = NHibernateSessionManager.Instance.GetSession().CreateSQLQuery(SqlCommand)
                                                                                .AddEntity(typeof(Department))
                                                                                .SetParameter("SearchItem", String.Format("%{0}%", SearchItem))
                                                                                .SetParameter("userId", userId)
                                                                                .List<Department>();
            return DepartmentList;
        }

    }
}
