using System;
using System.Collections.Generic;
using GTS.Clock.Infrastructure.NHibernateFramework;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using GTS.Clock.Infrastructure.Exceptions;
using System.Linq;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Infrastructure.RepositoryFramework
{
    /// <summary>
    /// .کلاس پايه براي تمامي کلاس هاي انباره که رابط آنها در مدل و پياده سازي آنها در فضاي نامي انباره انجام مي شود 
    /// </summary>
    /// <typeparam name="T">.نوع موجوديتي که انباره با آن کار مي کند</typeparam>
    public abstract class RepositoryBase<T> : IRepository<T>
    {
        protected Type persistanceType = typeof(T);
        protected bool disconnectedly;

        public RepositoryBase()
            : this(false)
        { }

        public RepositoryBase(bool Disconnectedly)
        {
            this.disconnectedly = Disconnectedly;
        }

        #region IRepository<T> Members

        /// <summary>
        /// یک نمونه از شی مشخص شده را براساس شناسه آن برمی گرداند
        /// </summary>
        /// <param name="id">شناسه موجودیت مورد نظر</param>
        /// <param name="shouldLock"></param>
        /// <returns></returns>
        public virtual T GetById(decimal id, bool shouldLock)
        {
            T entity = default(T);
            try
            {
                entity = shouldLock ?
                           (T)this.NHibernateSession.Get(this.persistanceType, id, LockMode.Upgrade)
                              : (T)this.NHibernateSession.Get(this.persistanceType, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.disconnectedly)
                    this.NHibernateSession.Disconnect();
            }
            return entity;
        }

        public virtual T CheckById(decimal id)
        {
            T t = GetById(id, false);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new ResourceNotFoundException("An Element by requested id can't be found in the database,Requested ID:" + id.ToString(), t.GetType().Name, ExceptionType.ALARM, " GTS.Clock.Infrastructure.RepositoryFramework.CheckById");
            }

        }

        public virtual IList<T> GetAll()
        {
            DetachedCriteria criteria = DetachedCriteria.For(this.persistanceType);
           
            IList<T> entities = null;
            try
            {
                entities = criteria.GetExecutableCriteria(NHibernateSession).List<T>() as IList<T>;
            }
            finally
            {
                if (this.disconnectedly)
                    this.NHibernateSession.Disconnect();
            }
            return entities;
        }

        public virtual IList<T> GetAllByPage(int pageIndex, int pageSize)
        {           
            DetachedCriteria criteria = DetachedCriteria.For(this.persistanceType);

            IList<T> entities = null;
            try
            {
                entities = criteria.GetExecutableCriteria(NHibernateSession)
                    .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                    .List<T>() as IList<T>;
            }
            finally
            {
                if (this.disconnectedly)
                    this.NHibernateSession.Disconnect();
            }
            return entities;
        }               

        public virtual T Save(T entity)
        {
            try
            {
                Transact(() => NHibernateSession.Save(entity));
            }
            finally
            {
                if (this.disconnectedly)
                    this.NHibernateSession.Disconnect();
            }
            return entity;
        }

        public virtual T Update(T entity)
        {
            try
            {
                Transact(() => NHibernateSession.Update(entity));
            }
            finally
            {
                if (this.disconnectedly)
                    this.NHibernateSession.Disconnect();
            }
            return entity;
        }

        public virtual T SaveOrUpdate(T entity)
        {
            try
            {
                Transact(() => NHibernateSession.SaveOrUpdate(entity));
            }
            finally
            {
                if (this.disconnectedly)
                    this.NHibernateSession.Disconnect();
            }
            return entity;
        }

        public virtual void Delete(T entity)
        {
            try
            {
                this.Transact(() => NHibernateSession.Delete(entity));
            }
            finally
            {
                if (this.disconnectedly)
                    this.NHibernateSession.Disconnect();
            }
        }

        public virtual T Merge(T entity)
        {
            try
            {
                return this.Transact<T>(() => (T)NHibernateSession.Merge(entity));
            }
            finally
            {
                if (this.disconnectedly)
                    this.NHibernateSession.Disconnect();
            }
        }

        public virtual T WithoutTransactSave(T entity)
        {
            try
            {
                NHibernateSession.Save(entity);
            }
            finally
            {
                if (this.disconnectedly)
                    this.NHibernateSession.Disconnect();
            }
            return entity;
        }

        public virtual T WithoutTransactUpdate(T entity)
        {
            try
            {
                NHibernateSession.Update(entity);
            }
            finally
            {
                if (this.disconnectedly)
                    this.NHibernateSession.Disconnect();
            }
            return entity;
        }

        public virtual T WithoutTransactSaveOrUpdate(T entity)
        {
            try
            {
                NHibernateSession.SaveOrUpdate(entity);
            }
            finally
            {
                if (this.disconnectedly)
                    this.NHibernateSession.Disconnect();
            }
            return entity;
        }

        public virtual void WithoutTransactDelete(T entity)
        {
            try
            {
                NHibernateSession.Delete(entity);
            }
            finally
            {
                if (this.disconnectedly)
                    this.NHibernateSession.Disconnect();
            }
        }

        public virtual T WithoutTransactMerge(T entity)
        {
            try
            {
                return (T)NHibernateSession.Merge(entity);
            }
            finally
            {
                if (this.disconnectedly)
                    this.NHibernateSession.Disconnect();
            }
        }

        /// <summary>
        /// جستجوی ارسال شده را اجرا می نماید
        /// </summary>
        /// <param name="Query">جستجوی مورد نظر</param>
        public virtual void RunHQL(string Query)
        {
            try
            {
                NHibernateSession.CreateQuery(Query)
                                    .ExecuteUpdate();
            }
            finally
            {
                if (this.disconnectedly)
                    this.NHibernateSession.Disconnect();
            }
        }

        public void RunSQL(string Query)
        {
            try
            {
                if (!String.IsNullOrEmpty(Query))
                {
                    NHibernateSession.CreateSQLQuery(Query).ExecuteUpdate();
                }
            }
            finally
            {
                if (this.disconnectedly)
                    this.NHibernateSession.Disconnect();
            }
        }

        public virtual void CommitChanges()
        {
            try
            {
                if (!NHibernateSessionManager.Instance.CommitTransactionOn())
                {
                    this.NHibernateSession.Flush();
                }
            }
            catch (NHibernateException)
            {
                NHibernateSessionManager.Instance.RollbackTransactionOn();
            }
        }

        public virtual ITransaction BeginTransaction()
        {
            return NHibernateSessionManager.Instance.BeginTransactionOn();
        }

        public virtual void RollebackTransaction()
        {
            NHibernateSessionManager.Instance.RollbackTransactionOn();
        }

        public IQuery GetNamedQuery(string QueryName)
        {
            return NHibernateSession.GetNamedQuery(QueryName);
        }

        public virtual IList<T> QuickSearch(string searchValue, int startIndex, int pageSize)
        {
            string query = String.Format(" EXEC sp_FindStringInTable '%{0}%', '{1}',{2},{3} ", searchValue, TableName, startIndex, pageSize);

            IList<T> resultList = this.NHibernateSession.CreateSQLQuery(query)
                                .List<T>();


            return resultList;
        }

        public int QuickSearchCount(string searchValue)
        {
            string query = String.Format(" EXEC sp_FindStringInTableCount '%{0}%', '{1}'", searchValue, TableName);

            int count = this.NHibernateSession.CreateSQLQuery(query)
                                 .List<int>().First();
            return count;
        }

        public int RowCount() 
        {
            string SQLQuery = String.Format(" SELECT Count(*) FROM {0} ",TableName);

            int count = this.NHibernateSession.CreateSQLQuery(SQLQuery)
                                           .List<int>().First();
            return count;
        }

        public abstract string TableName
        {
            get;
        }
       
        #region GetByCriteria

        /// <summary>
        /// تعداد رکوردها را پس از اعمال شروط بازمیگرداند
        /// توجه شود که تنها عملگر 'و' را جهت اتصال شروط استفاده میکند
        /// </summary>
        /// <param name="cretriaParam"></param>
        /// <returns></returns>
        public virtual IList<T> GetByCriteria(params CriteriaStruct[] cretriaParam)
        {
            ICriteria crit = this.NHibernateSession.CreateCriteria(typeof(T));
            for (int i = 0; i < cretriaParam.Length; i++)
            {
                CriteriaStruct c = cretriaParam[i];
                this.MakeCriteria(c, ref crit);
            }
            IList<T> list = crit.List<T>();
            return list;
        }

        /// <summary>
        /// تعداد رکوردها را پس از اعمال شروط بازمیگرداند
        /// توجه شود که تنها عملگر 'و' را جهت اتصال شروط استفاده میکند
        /// </summary>
        /// <param name="cretriaParam"></param>
        /// <returns></returns>
        public virtual IList<T> GetByCriteriaByPage(int pageIndex, int pageSize, params CriteriaStruct[] cretriaParam)
        {

            ICriteria crit = this.NHibernateSession.CreateCriteria(typeof(T));
            for (int i = 0; i < cretriaParam.Length; i++)
            {
                CriteriaStruct c = cretriaParam[i];
                this.MakeCriteria(c, ref crit);
            }
            //if (category == PersonCategory.Public) 
            //{

            //}
            IList<T> list = crit.SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize).List<T>();
            return list;

        }

        /// <summary>
        /// تعداد رکوردها را پس از اعمال شروط بازمیگرداند
        /// </summary>
        /// <param name="cretriaParam"></param>
        /// <returns></returns>
        public virtual IList<T> GetByCriteria(ConditionOperations conOp, params CriteriaStruct[] cretriaParam)
        {

            ICriteria crit = this.NHibernateSession.CreateCriteria(typeof(T));
            Junction disjunction = Restrictions.Disjunction();

            for (int i = 0; i < cretriaParam.Length; i++)
            {
                CriteriaStruct c = cretriaParam[i];
                this.MakeCriteria(c, ref crit, ref disjunction, conOp);
            }
            crit.Add(disjunction);
            IList<T> list = crit.List<T>();
            return list;

        }

        /// <summary>
        /// تعداد رکوردها را پس از اعمال شروط بازمیگرداند
        /// </summary>
        /// <param name="cretriaParam"></param>
        /// <returns></returns>
        public virtual IList<T> GetByCriteriaByPage(int pageIndex, int pageSize, ConditionOperations conOp, params CriteriaStruct[] cretriaParam)
        {

            ICriteria crit = this.NHibernateSession.CreateCriteria(typeof(T));
            Junction disjunction = Restrictions.Disjunction();

            for (int i = 0; i < cretriaParam.Length; i++)
            {
                CriteriaStruct c = cretriaParam[i];
                this.MakeCriteria(c, ref crit, ref disjunction, conOp);
            }
            crit.Add(disjunction);
            IList<T> list = crit.SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize).List<T>();
            return list;

        }

        /// <summary>
        /// تعداد رکوردها را پس از اعمال شروط بازمیگرداند
        /// توجه شود که تنها عملگر 'و' را جهت اتصال شروط استفاده میکند
        /// </summary>
        /// <param name="cretriaParam"></param>
        /// <returns></returns>
        public virtual int GetCountByCriteria(params CriteriaStruct[] cretriaParam)
        {

            ICriteria crit = this.NHibernateSession.CreateCriteria(typeof(T));
            for (int i = 0; i < cretriaParam.Length; i++)
            {
                CriteriaStruct c = cretriaParam[i];
                this.MakeCriteria(c, ref crit);
            }
            int count = (int)crit.SetProjection(Projections.Count("ID")).UniqueResult();
            return count;

        }

        /// <summary>
        /// تعداد رکوردها را پس از اعمال شروط بازمیگرداند
        /// </summary>
        /// <param name="cretriaParam"></param>
        /// <returns></returns>
        public virtual int GetCountByCriteria(ConditionOperations conOp, params CriteriaStruct[] cretriaParam)
        {

            ICriteria crit = this.NHibernateSession.CreateCriteria(typeof(T));
            Junction disjunction = Restrictions.Disjunction();

            for (int i = 0; i < cretriaParam.Length; i++)
            {
                CriteriaStruct c = cretriaParam[i];
                this.MakeCriteria(c, ref crit, ref disjunction, conOp);
            }
            crit.Add(disjunction);
            int count = (int)crit.SetProjection(Projections.Count("ID")).UniqueResult();
            return count;

        }

        /// <summary>
        /// شرط را اعمال میکند
        /// و خروجی را در پارامتر دوم میریزد
        /// </summary>
        /// <param name="criteriaStruct"></param>
        /// <param name="criteria"></param>
        private void MakeCriteria(CriteriaStruct criteriaStruct  ,ref ICriteria criteria) 
        {
            CriteriaStruct c = criteriaStruct;
            switch (criteriaStruct.Operation)
            {
                case CriteriaOperation.Equal:
                    criteria.Add(Restrictions.Eq(c.PropertyName, c.Value));
                    break;
                case CriteriaOperation.IS:
                    criteria.Add(Restrictions.IsNull(c.PropertyName));
                    break;
                case CriteriaOperation.NotEqual:
                    criteria.Add(Restrictions.Not(Restrictions.Eq(c.PropertyName, c.Value)));
                    break;
                case CriteriaOperation.GreaterThan:
                    criteria.Add(Restrictions.Gt(c.PropertyName, c.Value));
                    break;
                case CriteriaOperation.LessThan:
                    criteria.Add(Restrictions.Lt(c.PropertyName, c.Value));
                    break;
                case CriteriaOperation.GreaterEqThan:
                    criteria.Add(Restrictions.Ge(c.PropertyName, c.Value));
                    break;
                case CriteriaOperation.LessEqThan:
                    criteria.Add(Restrictions.Le(c.PropertyName, c.Value));
                    break;
                case CriteriaOperation.IsNotNull:
                    criteria.Add(Restrictions.IsNotNull(c.PropertyName));
                    break;
                case CriteriaOperation.IsNull:
                    criteria.Add(Restrictions.IsNull(c.PropertyName));
                    break;
                case CriteriaOperation.IN:
                    criteria.Add(Restrictions.In(c.PropertyName, (decimal[])c.Value));
                    break;
                case CriteriaOperation.Like:
                    if (c.Value is string)
                        criteria.Add(Restrictions.Like(c.PropertyName, c.Value.ToString(), MatchMode.Anywhere));
                    else
                        criteria.Add(Restrictions.Like(c.PropertyName, c.Value));
                    break;
            }
        }
       
        /// <summary>
        /// شرط را اعمال میکند
        /// و خروجی را در پارامتر دوم و سوم میریزد
        /// </summary>
        /// <param name="criteriaStruct"></param>
        /// <param name="criteria"></param>
        private void MakeCriteria(CriteriaStruct criteriaStruct, ref ICriteria criteria, ref Junction disjunction, ConditionOperations conOp)
        {
            CriteriaStruct c = criteriaStruct;
            switch (c.Operation)
            {
                case CriteriaOperation.Equal:
                    if (conOp == ConditionOperations.AND)
                        criteria.Add(Restrictions.Eq(c.PropertyName, c.Value));
                    else if (conOp == ConditionOperations.OR)
                        disjunction.Add(Restrictions.Eq(c.PropertyName, c.Value));
                    break;
                case CriteriaOperation.NotEqual:
                    if (conOp == ConditionOperations.AND)
                        criteria.Add(Restrictions.Not(Restrictions.Eq(c.PropertyName, c.Value)));
                    else if (conOp == ConditionOperations.OR)
                        disjunction.Add(Restrictions.Not(Restrictions.Eq(c.PropertyName, c.Value)));
                    break;
                case CriteriaOperation.GreaterThan:
                    if (conOp == ConditionOperations.AND)
                        criteria.Add(Restrictions.Gt(c.PropertyName, c.Value));
                    else if (conOp == ConditionOperations.OR)
                        disjunction.Add(Restrictions.Gt(c.PropertyName, c.Value));
                    break;
                case CriteriaOperation.LessThan:
                    if (conOp == ConditionOperations.AND)
                        criteria.Add(Restrictions.Lt(c.PropertyName, c.Value));
                    else if (conOp == ConditionOperations.OR)
                        disjunction.Add(Restrictions.Lt(c.PropertyName, c.Value));
                    break;
                case CriteriaOperation.GreaterEqThan:
                    if (conOp == ConditionOperations.AND)
                        criteria.Add(Restrictions.Ge(c.PropertyName, c.Value));
                    else if (conOp == ConditionOperations.OR)
                        disjunction.Add(Restrictions.Ge(c.PropertyName, c.Value));
                    break;
                case CriteriaOperation.LessEqThan:
                    if (conOp == ConditionOperations.AND)
                        criteria.Add(Restrictions.Le(c.PropertyName, c.Value));
                    else if (conOp == ConditionOperations.OR)
                        disjunction.Add(Restrictions.Le(c.PropertyName, c.Value));
                    break;
                case CriteriaOperation.IsNotNull:
                    criteria.Add(Restrictions.IsNotNull(c.PropertyName));
                    break;
                case CriteriaOperation.IsNull:
                    criteria.Add(Restrictions.IsNull(c.PropertyName));
                    break;
                case CriteriaOperation.IN:
                    if (conOp == ConditionOperations.AND)
                        criteria.Add(Restrictions.In(c.PropertyName, (object[])c.Value));
                    else if (conOp == ConditionOperations.OR)
                        disjunction.Add(Restrictions.In(c.PropertyName, (object[])c.Value));
                    break;
                case CriteriaOperation.Like:
                    if (conOp == ConditionOperations.AND)
                    {
                        if (c.Value is string)
                        {
                            criteria.Add(Restrictions.Like(c.PropertyName, c.Value.ToString(), MatchMode.Anywhere));
                        }
                        else
                        {
                            criteria.Add(Restrictions.Like(c.PropertyName, c.Value));
                        }
                    }
                    else if (conOp == ConditionOperations.OR)
                    {
                        if (c.Value is string)
                        {
                            disjunction.Add(Restrictions.Like(c.PropertyName, c.Value.ToString(), MatchMode.Anywhere));
                        }
                        else
                        {
                            disjunction.Add(Restrictions.Like(c.PropertyName, c.Value));
                        }
                    }
                    break;
            }
        }
 
        #endregion

        #region Linq Filter
       
        public IQueryable<T> Find()
		{
            return NHibernateSession.Query<T>();
		}

		public IQueryable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
		{			
			return Find().Where(predicate);
		}
        
        #endregion

        /// <summary>
        /// رشته ارتباط با پایگاه داده را برمی گرداند
        /// </summary>
        /// <returns></returns>
        public virtual string GetConnectionString()
        {
            //return this.NHibernateSession.Connection.ConnectionString;
            return Utility.Utility.ReadAppSetting("ReportConnectionString");
        }



        public decimal GetMaxId() 
        {
            string HQLQuery = String.Format(" SELECT MAX(ID) FROM {0} ", typeof(T).Name);

            decimal maxId = this.NHibernateSession.CreateQuery(HQLQuery)
                                           .List<decimal>().First();
            return maxId;
        }
        #endregion

        public ISession NHibernateSession
        {
            get
            {
                return NHibernateSessionManager.Instance.GetSession();
            }
        }

        public NhibernateFilters NhibernateFilters
        {
            get
            {
                return NHibernateSessionManager.Instance.GetFilters();
            }
        }

        public virtual void Refresh(T Entity)
        {
            this.NHibernateSession.Refresh(Entity);
        }

        protected TResult Transact<TResult>(Func<TResult> func)
        {
            TResult result = default(TResult);
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {

                    result = func.Invoke();
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }
                catch
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    throw;
                }

            }
            return result;
        }

        protected void Transact(Action action)
        { 
            this.Transact<bool>(() => {action.Invoke(); return false;});
        }

        /// <summary>
        /// موارد زیر را یررسی میکند:
        /// حداکثر تعداد پارامتر
        /// بررسی خالی نبودن لیست
        /// </summary>
        /// <param name="listParam"></param>
        /// <returns></returns>
        protected decimal[] CheckListParameter(IList<decimal> listParam)
        {
            if (listParam == null)
                return new decimal[0];
			
            return listParam.ToArray<decimal>();
        }
    }
}

