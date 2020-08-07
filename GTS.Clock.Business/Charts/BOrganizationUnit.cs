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
using NHibernate.Criterion;
using GTS.Clock.Business.Temp;
using NHibernate;
using System.Web.Configuration;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.RequestFlow;

namespace GTS.Clock.Business.Charts
{
    /// <summary>
    /// پست سازمانی
    /// </summary>
    public class BOrganizationUnit : BaseBusiness<OrganizationUnit>
    {
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        private IDataAccess accessPort = new BUser();
        const string ExceptionSrc = "GTS.Clock.Business.Charts.BOrganizationUnit";
        private OrganizationUnitRepository organizationUnitRepository = new OrganizationUnitRepository(false);
        private int OperationBatchSizeValue = int.Parse(WebConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        private BTemp bTemp = new BTemp();
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        /// <summary>
        /// ریشه درخت را برمیگرداند که با پیمایش بچههای آن درخت استخراج میشود
        /// حتما باید ریشه در دیتابیس موجود باشد
        /// </summary>
        /// <returns>پست سازمانی</returns>
        public OrganizationUnit GetOrganizationUnitTree()
        {
            try
            {
                IList<OrganizationUnit> organizationUnitList = organizationUnitRepository.GetOrganizationUnitTree();

                if (organizationUnitList.Count == 1)
                {
                    return organizationUnitList.First();
                }
                else
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.OrganizationUnitRootMoreThanOne, "تعداد ریشه چارت سازمانی در دیتابیس نامعتبر است", ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BOrganizationUnit", "GetOrganizationUnitTree");
                throw ex;
            }
        }

        /// <summary>
        /// برای بایند شدن درخت در هنگام باز کردن گره این تابع استفاده میشود
        /// </summary>
        /// <param name="parentId">کلید ریشه</param> 
        /// <returns>لیست پست سازمانی</returns>
        public IList<OrganizationUnit> GetChilds(decimal parentId)
        {
            try
            {
                IList<OrganizationUnit> list = null;
                IList<decimal> accessableIDs = accessPort.GetAccessibleOrgans();
                if (accessableIDs.Count < this.OperationBatchSizeValue && this.OperationBatchSizeValue < 2100)
                {
                    list = NHSession.QueryOver<OrganizationUnit>()
                                    .Where(x => x.ID.IsIn(accessableIDs.ToArray()) &&
                                                x.Parent.ID == parentId
                                          )
                                    .List<OrganizationUnit>();
                }
                else
                {
                    OrganizationUnit organizationAlias = null;
                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                    string operationGUID = this.bTemp.InsertTempList(accessableIDs);
                    list = NHSession.QueryOver(() => organizationAlias)
                                    .JoinAlias(() => organizationAlias.TempList, () => tempAlias)
                                    .Where(() => tempAlias.OperationGUID == operationGUID && organizationAlias.Parent.ID == parentId)
                                    .List<OrganizationUnit>();
                    this.bTemp.DeleteTempList(operationGUID);
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BOrganizationUnit", "GetChilds");
                throw ex;
            }
        }

        /// <summary>
        /// زیر گره های یک گره را برمیگرداند
        /// </summary>
        /// <param name="organId">کلید اصلی پست</param>
        /// <returns>لیست پست سازمانی</returns>
        public IList<OrganizationUnit> GetOrganizationChildsWithoutDA(decimal organId)
        {
            try
            {
                IList<OrganizationUnit> organList = new List<OrganizationUnit>();

                organList = organizationUnitRepository
                    .GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new OrganizationUnit().Parent), new OrganizationUnit() { ID = organId }));


                return organList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BOrganizationUnit", "GetDepartmentChildsWithoutDA");
                throw ex;
            }
        }
        public IList<OrganizationUnit> GetSearchedOrganizationWithoutDA(string searchItem)
        {
            try
            {
                IList<OrganizationUnit> OrgList = new List<OrganizationUnit>();
                IList<OrganizationUnit> organList = new List<OrganizationUnit>();
                OrganizationUnit orgAlias = null;
                organList = NHSession.QueryOver(() => orgAlias)
                                     .Where(() => orgAlias.Name.IsInsensitiveLike(searchItem, MatchMode.Anywhere) &&
                                                  orgAlias.Parent.ID != null
                                           )
                                     .List<OrganizationUnit>();
                foreach (OrganizationUnit o1 in organList)
                {
                    foreach (OrganizationUnit o2 in organList)
                    {
                        if (o2.ParentPathList.Contains(o1.ID))
                        {
                            OrgList.Add(o2);
                        }
                    }
                }
                organList = organList.Except(OrgList).ToList<OrganizationUnit>();
                return organList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BOrganizationUnit", "GetDepartmentChildsWithoutDA");
                throw ex;
            }
        }

        /// <summary>
        /// جستجوی پست سازمانی با نام 
        /// </summary>
        /// <param name="organName">نام پست سازمانی</param>
        /// <returns>لیست پست سازمانی</returns>
        public IList<OrganizationUnit> SearchByUnitName(string organName)
        {
            try
            {
                IList<OrganizationUnit> list = null;
                IList<decimal> accessableIDs = accessPort.GetAccessibleOrgans();
                if (accessableIDs.Count < this.OperationBatchSizeValue && this.OperationBatchSizeValue < 2100)
                {
                    list = NHSession.QueryOver<OrganizationUnit>()
                                    .Where(x => x.ID.IsIn(accessableIDs.ToArray()) &&
                                                x.Name.IsInsensitiveLike(organName, MatchMode.Anywhere) &&
                                                x.Parent != null
                                          )
                                    .List<OrganizationUnit>();
                }
                else
                {
                    OrganizationUnit organizationAlias = null;
                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                    string operationGUID = this.bTemp.InsertTempList(accessableIDs);
                    list = NHSession.QueryOver(() => organizationAlias)
                                    .JoinAlias(() => organizationAlias.TempList, () => tempAlias)
                                    .Where(() => tempAlias.OperationGUID == operationGUID && organizationAlias.Name.IsInsensitiveLike(organName, MatchMode.Anywhere) && organizationAlias.Parent != null)
                                    .List<OrganizationUnit>();
                    this.bTemp.DeleteTempList(operationGUID);
                }

                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BOrganizationUnit", "SearchByUnitName");
                throw ex;
            }
        }

        /// <summary>
        /// جستجوی پست سازمانی
        /// </summary>
        /// <param name="field">فیلد جستجو شونده</param>
        /// <param name="searchVal">مقدار جستجو</param>
        /// <returns>لیست پست سازمانی</returns>
        public IList<OrganizationUnit> SearchOrganization(OrganizationSearchFields field, string searchVal)
        {
            try
            {
                IList<OrganizationUnit> list = null;
                IList<decimal> accessableIDs = accessPort.GetAccessibleOrgans();
                if (accessableIDs.Count < this.OperationBatchSizeValue && this.OperationBatchSizeValue < 2100)
                {
                    switch (field)
                    {
                        case OrganizationSearchFields.NotSpec:
                            list = NHSession.QueryOver<OrganizationUnit>()
                                            .Where(x => x.ID.IsIn(accessableIDs.ToArray()) &&
                                                       (x.Name.IsInsensitiveLike(searchVal, MatchMode.Anywhere) || x.CustomCode.IsInsensitiveLike(searchVal, MatchMode.Anywhere))
                                                  )
                                            .List<OrganizationUnit>();
                            break;
                        case OrganizationSearchFields.OrganizationName:
                            break;
                    }
                }
                else
                {
                    OrganizationUnit organizationAlias = null;
                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                    switch (field)
                    {
                        case OrganizationSearchFields.OrganizationName:
                            break;
                        case OrganizationSearchFields.NotSpec:
                            string operationGUID = this.bTemp.InsertTempList(accessableIDs);
                            list = NHSession.QueryOver(() => organizationAlias)
                                                      .JoinAlias(() => organizationAlias.TempList, () => tempAlias)
                                                      .Where(() => tempAlias.OperationGUID == operationGUID && (organizationAlias.Name.IsInsensitiveLike(searchVal, MatchMode.Anywhere) || organizationAlias.CustomCode.IsInsensitiveLike(searchVal, MatchMode.Anywhere)))
                                                      .List<OrganizationUnit>();
                            this.bTemp.DeleteTempList(operationGUID);
                            break;
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BOrganizationUnit", "SearchOrganization");
                throw ex;
            }
        }

        /// <summary>
        /// جستجوی پست سازمانی
        /// </summary>
        /// <param name="searchVal">عبارت جستجو</param>
        /// <returns>لیست پست سازمانی</returns>
        public IList<OrganizationUnit> SearchOrganizationUnit(string searchVal)
        {
            try
            {
                IList<OrganizationUnit> list = null;
                IList<decimal> accessableIDs = accessPort.GetAccessibleOrgans();
                if (accessableIDs.Count < this.OperationBatchSizeValue && this.OperationBatchSizeValue < 2100)
                {
                    list = NHSession.QueryOver<OrganizationUnit>()
                                    .Where(x => x.ID.IsIn(accessableIDs.ToArray()) &&
                                               (x.Name.IsInsensitiveLike(searchVal, MatchMode.Anywhere) || x.CustomCode.IsInsensitiveLike(searchVal, MatchMode.Anywhere))
                                          )
                                    .List<OrganizationUnit>();
                }
                else
                {
                    OrganizationUnit organizationAlias = null;
                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                    string operationGUID = this.bTemp.InsertTempList(accessableIDs);
                    list = NHSession.QueryOver(() => organizationAlias)
                                    .JoinAlias(() => organizationAlias.TempList, () => tempAlias)
                                    .Where(() => tempAlias.OperationGUID == operationGUID && (organizationAlias.Name.IsInsensitiveLike(searchVal, MatchMode.Anywhere) || organizationAlias.CustomCode.IsInsensitiveLike(searchVal, MatchMode.Anywhere)))
                                    .List<OrganizationUnit>();
                    this.bTemp.DeleteTempList(operationGUID);
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BOrganizationUnit", "SearchOrganizationUnit");
                throw ex;
            }
        }

        /// <summary>
        /// زیر گره های یک گره را با استفاده از آدرس پدران آن برمیگرداند
        /// </summary>
        /// <param name="departmentId">کلید اصلی گره</param>
        /// <returns>لیست پست سازمانی</returns>
        public IList<OrganizationUnit> GetOrganiztionChildsByParentPath(decimal parentId)
        {
            IList<OrganizationUnit> depList = organizationUnitRepository.GetByCriteria(
                new CriteriaStruct(
                    Utility.GetPropertyName(() => new Department().ParentPath)
                    , String.Format(",{0},", parentId)
                    , CriteriaOperation.Like));
            return depList;
        }

        /// <summary>
        /// اعتبارسنجی عملیات درج
        /// نام خالی نباشد
        /// شناسه والد معتبر باشد
        /// اعتبار سنجی آیتمی که قرار است درج شود
        /// شناسه والد خالی نباشد
        /// نام  تکراری نباشد
        /// کد تعریف شده نباید تکراری باشد
        /// </summary>
        /// <param name="organizationUnit">پست سازمانی</param> 
        protected override void InsertValidate(OrganizationUnit organizationUnit)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(organizationUnit.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.OrganizationUnitNameRequierd, "نام چارت باید مشخص شود", ExceptionSrc));
            }

            if (Utility.IsEmpty(organizationUnit.ParentID))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.OrganizationUnitParentIDRequierd, "نام والد چارت باید مشخص شود", ExceptionSrc));
            }

            else if (organizationUnitRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => organizationUnit.ID), organizationUnit.ParentID)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.OrganizationUnitParentNotExists, "چارتی با این شناسه موجود نمیباشد", ExceptionSrc));
            }

            else if (organizationUnitRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => organizationUnit.Name), organizationUnit.Name),
                                                                        new CriteriaStruct(Utility.GetPropertyName(() => organizationUnit.Parent), organizationUnit.Parent)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.OrganizationUnitNameRepeated, "نام در یک سطح چارت نباید تکراری باشد", ExceptionSrc));
            }

            if (!Utility.IsEmpty(organizationUnit.CustomCode))
            {
                if (organizationUnitRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => organizationUnit.CustomCode), organizationUnit.CustomCode)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.OrganizationUnitCustomCodeRepeated, "درج - کد تعریف شده در چارت نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبارسنجی عملیات ویرایش
        /// نام خالی نباشد
        /// شناسه والد معتبر باشد
        /// اعتبار سنجی آیتمی که قرار است بروزرسانی شود
        /// نام در گرهای همسطح تکراری نباشد
        /// کد تعریف شده نباید تکراری باشد
        /// </summary>
        /// <param name="organizationUnit">پست سازمانی</param>
        protected override void UpdateValidate(OrganizationUnit organizationUnit)
        {
            // والد یک گره بروزرسانی نمیشود .همچنینبنا به محدودیتهای کلاینت هنگام بروزرسانی والد مقداردهی نمیشود
            organizationUnit.ParentID = organizationUnitRepository.GetParentID(organizationUnit.ID);

            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(organizationUnit.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.OrganizationUnitNameRequierd, "نام چارت باید مشخص شود", ExceptionSrc));
            }

            else if (organizationUnitRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => organizationUnit.Name), organizationUnit.Name),
                                                                        new CriteriaStruct(Utility.GetPropertyName(() => organizationUnit.Parent), organizationUnit.Parent),
                                                                        new CriteriaStruct(Utility.GetPropertyName(() => organizationUnit.ID), organizationUnit.ID, CriteriaOperation.NotEqual)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.OrganizationUnitNameRepeated, "نام چارت در یک سطح نباید تکراری باشد", ExceptionSrc));
            }

            if (organizationUnit.ParentID != 0 &&
                organizationUnitRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => organizationUnit.Parent), organizationUnit.Parent)) == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.OrganizationUnitParentNotExists, "چارت والدی با این شناسه موجود نمیباشد", ExceptionSrc));
            }


            if (!Utility.IsEmpty(organizationUnit.CustomCode))
            {
                if (organizationUnitRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => organizationUnit.CustomCode), organizationUnit.CustomCode),
                                                                       new CriteriaStruct(Utility.GetPropertyName(() => organizationUnit.ID), organizationUnit.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.OrganizationUnitCustomCodeRepeated, "درج - کد تعریف شده در چارت نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبارسنجی عملیات حذف
        /// </summary>
        /// <param name="organizationUnit">پست سازمانی</param>
        protected override void DeleteValidate(OrganizationUnit organizationUnit)
        {
            if (organizationUnitRepository.IsRoot(organizationUnit.ID))
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                exception.Add(ExceptionResourceKeys.OrganizationUnitRootDeleteIllegal, "ریشه چارت سازمانی نباید حذف شود", ExceptionSrc);
                throw exception;
            }
            if (organizationUnitRepository.HasPerson(organizationUnit.ID))
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                exception.Add(ExceptionResourceKeys.OrganizationUnitUsedByPerson, "این چارت به اشخاص انتساب داده شده است", ExceptionSrc);
                throw exception;
            }
            string id = organizationUnit.ID.ToString();
            IList<OrganizationUnit> list = NHibernateSessionManager.Instance.GetSession().QueryOver<OrganizationUnit>()
                                                                          .Where(x => x.ParentPath.IsInsensitiveLike("," + id + ",", MatchMode.Anywhere))

                                                                          .List<OrganizationUnit>();
            foreach (OrganizationUnit item in list)
            {
                if (organizationUnitRepository.HasPerson(item.ID))
                {
                    UIValidationExceptions exception = new UIValidationExceptions();
                    exception.Add(ExceptionResourceKeys.ChildOrganizationUnitUsedByPerson, "زیر مجموعه های این چارت به اشخاص انتساب داده شده است", ExceptionSrc);
                    throw exception;
                }
            }

            BManager bManager = new BManager();
            if (bManager.CheckOrganizationUnitUsedInFlowAsManager(organizationUnit, true))
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                exception.Add(new ValidationException(ExceptionResourceKeys.OrganizationUnitUsedInFlowAsManager, "پست سازمانی در جریان کاری بعنوان مدیر تعریف شده است", ExceptionSrc));
                throw exception;
            }


            this.organizationUnitRepository.DeleteHierarchicalByParentId(organizationUnit.ID);

        }

        /// <summary>
        /// اعمال دسترسی بعد از ذخیره پست سازمانی به کاربر جاری
        /// </summary>
        /// <param name="obj">پست سازمانی</param>
        /// <param name="action">نوع عملیات</param>
        protected override void OnSaveChangesSuccess(OrganizationUnit obj, UIActionType action)
        {
            if (action == UIActionType.ADD)
            {
                new BDataAccess().InsertDataAccess(Infrastructure.DataAccessLevelOperationType.Single, Infrastructure.DataAccessParts.OrganizationUnit, obj.ID, BUser.CurrentUser.ID, null, "");
            }
            this.UpdateParentOrganizationPostChildCount(obj, action);
        }

        /// <summary>
        /// به روز رسانی تعداد زیرگره های یک پست سازمانی
        /// </summary>
        /// <param name="obj">پست سازمانی</param>
        /// <param name="action">نوع عملیات</param>
        private void UpdateParentOrganizationPostChildCount(OrganizationUnit obj, UIActionType action)
        {
            if (obj.ParentID != 0)
            {
                OrganizationUnit parentOrganizationUnit = NHSession.QueryOver<OrganizationUnit>()
                                                                   .Where(x => x.ID == obj.ParentID)
                                                                   .SingleOrDefault<OrganizationUnit>();
                if (parentOrganizationUnit != null)
                {
                    switch (action)
                    {
                        case UIActionType.ADD:
                            parentOrganizationUnit.ChildCount = parentOrganizationUnit.ChildCount + 1;
                            this.SaveChanges(parentOrganizationUnit, UIActionType.EDIT);
                            break;
                        case UIActionType.EDIT:
                            break;
                        case UIActionType.DELETE:
                            if (parentOrganizationUnit.ChildCount > 0)
                            {
                                parentOrganizationUnit.ChildCount = parentOrganizationUnit.ChildCount - 1;
                                this.SaveChanges(parentOrganizationUnit, UIActionType.EDIT);
                            }
                            break;
                    }
                }
            }
        }
        public IList<OrganizationUnit> GetParentOrganizationPosts(int postId)
        {
            OrganizationUnit organizationUnitAlias = null;
            IList<OrganizationUnit> ParentOrganizationUnitList = new List<OrganizationUnit>();
            OrganizationUnit PostList = NHSession.QueryOver<OrganizationUnit>()
                                                 .Where(x => x.ID == postId)
                                                 .SingleOrDefault();
            if (PostList != null)
            {
                if (PostList.ParentPathList.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {
                    ParentOrganizationUnitList = NHSession.QueryOver(() => organizationUnitAlias)
                                                          .Where(() => organizationUnitAlias.ID.IsIn(PostList.ParentPathList.ToArray()))
                                                          .List<OrganizationUnit>();
                }
                else
                {
                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                    string operationGUID = this.bTemp.InsertTempList(PostList.ParentPathList);
                    ParentOrganizationUnitList = NHSession.QueryOver(() => organizationUnitAlias)
                                                          .JoinAlias(() => organizationUnitAlias.TempList, () => tempAlias)
                                                          .Where(() => tempAlias.OperationGUID == operationGUID)
                                                          .List<OrganizationUnit>();
                    this.bTemp.DeleteTempList(operationGUID);
                }
            }
            return ParentOrganizationUnitList;
        }

        /// <summary>
        /// بررسی دسترسی به پست سازمانی
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckOrganizationPostsLoadAccess()
        {
        }

        /// <summary>
        /// عملیات درج پست سازمانی در دیتابیس
        /// </summary>
        /// <param name="organizationPost">پست سازمانی</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی پست سازمانی</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertOrganizationPost(OrganizationUnit organizationPost, UIActionType UAT)
        {
            return base.SaveChanges(organizationPost, UAT);
        }

        /// <summary>
        /// عملیات ویرایش پست سازمانی در دیتابیس
        /// </summary>
        /// <param name="organizationPost">پست سازمانی</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی پست سازمانی</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateOrganizationPost(OrganizationUnit organizationPost, UIActionType UAT)
        {
            return base.SaveChanges(organizationPost, UAT);
        }

        /// <summary>
        /// عملیات حذف پست سازمانی در دیتابیس
        /// </summary>
        /// <param name="organizationPost">پست سازمانی</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی پست سازمانی</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteOrganizationPost(OrganizationUnit organizationPost, UIActionType UAT)
        {
            return base.SaveChanges(organizationPost, UAT);
        }

    }
}

