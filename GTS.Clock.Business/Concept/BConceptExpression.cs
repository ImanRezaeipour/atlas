using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using NHibernate.Hql.Ast.ANTLR;
using NHibernate.Linq;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.RuleDesigner
{
    /// <summary>
    /// عبارت منطقی ساخته شده مفاهیم
    /// </summary>
    public class BConceptExpression : BaseBusiness<ConceptExpression>
    {
        readonly EntityRepository<ConceptExpression> _cnpRep = new EntityRepository<ConceptExpression>();
        const string ExceptionSrc = "GTS.Clock.Business.RuleDesigner";

        /// <summary>
        /// ریشه را بر می گرداند
        /// </summary>
        /// <returns>عبارت منطقی</returns>
        public ConceptExpression GetRoot()
        {
            var rootNode = Infrastructure.NHibernateFramework.NHibernateSessionManager.Instance.GetSession()
                .Query<ConceptExpression>().Single(x => x.Parent_ID == null && x.ScriptBeginFa == "...");
             
            return rootNode;
        }

        /// <summary>
        /// عبارت های منطقی را به وسیله کلید اصلی والد آن بر می گرداند
        /// </summary>
        /// <param name="parentId">کلید اصلی والد</param>
        /// <returns>لیست عبارت منطقی</returns>
        public List<ConceptExpression> GetByParentId(decimal? parentId)
        {
            var items = _cnpRep.GetAll().Where(x => x.Visible == true);
            items = items.Where(x => x.Visible && parentId == null ? x.Parent_ID == null : x.Parent_ID == parentId)
                .OrderBy(x => BLanguage.CurrentLocalLanguage == LanguagesName.Parsi ? x.ScriptBeginFa : x.ScriptBeginEn)
                .ToList();

            return (List<ConceptExpression>)items;

            //if (parentId == null)
            //    return _cnpRep.Find(x => !x.Parent_ID.HasValue).ToList();
            //return _cnpRep.Find(x => x.Parent_ID == parentId).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId">کلید اصلی والد</param>
        /// <returns>لیست عبارت منطقی</returns>
        public List<ConceptExpression> GetChildrenOnCreation(decimal parentId)
        {
            var items = _cnpRep.GetAll().Where(x => x.Visible == true);
            items = items.Where(x => x.Visible && x.Parent_ID == parentId && x.CanAddToFinal && x.AddOnParentCreation)
                .OrderBy(x => BLanguage.CurrentLocalLanguage == LanguagesName.Parsi ? x.ScriptBeginFa : x.ScriptBeginEn)
                .ToList();

            return (List<ConceptExpression>)items;

            //if (parentId == null)
            //    return _cnpRep.Find(x => !x.Parent_ID.HasValue).ToList();
            //return _cnpRep.Find(x => x.Parent_ID == parentId).ToList();
        }

        #region Overrides of BaseBusiness<ConceptExpression>

        /// <summary>
        /// اعتبارسنجی عملیات درج
        /// </summary>
        /// <param name="obj">عبارت منطقی</param>
        protected override void InsertValidate(ConceptExpression obj)
        {

        }

        /// <summary>
        /// اعتبار سنجی عملیات ویرایش
        /// </summary>
        /// <param name="obj">عبارت منطقی</param>
        protected override void UpdateValidate(ConceptExpression obj)
        {

        }

        /// <summary>
        /// اعتبار سنجی عملیات حذف
        /// </summary>
        /// <param name="obj">عبارت منطقی</param>
        protected override void DeleteValidate(ConceptExpression obj)
        {

        }

        #endregion

        /// <summary>
        /// علمیات درج عبارت منطقی در دیتابیس
        /// </summary>
        /// <param name="conceptExpression">عبارت منطقی</param>
        /// <returns>کلید اصلی عبارت منطقی</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertConceptExpression(ConceptExpression conceptExpression)
        {
            try
            {
                return this.SaveChanges(conceptExpression, UIActionType.ADD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// علمیات ویرایش عبارت منطقی در دیتابیس
        /// </summary>
        /// <param name="conceptExpression">عبارت منطقی</param>
        /// <returns>کلید اصلی عبارت منطقی</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateConceptExpression(ConceptExpression conceptExpression)
        {
            try
            {
                return this.SaveChanges(conceptExpression, UIActionType.EDIT);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// عملیات حذف عبارت منطقی در دیتابیس
        /// </summary>
        /// <param name="conceptExpression">عبارت منطقی</param>
        /// <returns>کلید اصلی عبارت منطقی</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteConceptExpression(ConceptExpression conceptExpression)
        {
            try
            {
                return this.SaveChanges(conceptExpression, UIActionType.DELETE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// تعداد عبارت های منطقی جستجو شده را بر می گرداند
        /// </summary>
        /// <param name="searchTerm">عبارت جستجو</param>
        /// <returns>تعداد</returns>
        public int GetAllByPageBySearchCount(string searchTerm)
        {
            IEnumerable<ConceptExpression> nn = _cnpRep.GetAll();

            if (nn.FirstOrDefault() == null)
                return 0;

            nn = nn.Where(
               concept =>
                   concept.ScriptBeginFa.Contains(searchTerm)
                //|| concept.ScriptBeginEn.Contains(searchTerm)
                   );

            if (nn.FirstOrDefault() == null)
                return 0;

            return nn.Count();
        }

        /// <summary>
        /// عبارت های منطقی جستجو شده را به صورت صفحه بندی شده بر می گرداند
        /// </summary>
        /// <param name="pageIndex">تعداد رکوردها در هر صفحه</param>
        /// <param name="pageSize">شماره صفحه</param>
        /// <param name="searchTerm">عبارت جستجو</param>
        /// <returns>لیست عبارت منطقی</returns>
        public IList<ConceptExpression> GetAllByPageBySearch(int pageIndex, int pageSize, string searchTerm)
        {
            IEnumerable<ConceptExpression> queryOnConceptExression = null;
            try
            {
                if (string.IsNullOrEmpty(searchTerm.Trim()))
                {
                    queryOnConceptExression = _cnpRep.GetAll();
                }
                else
                {
                    queryOnConceptExression = _cnpRep.GetAll().Where(
                        concept =>
                        concept.ScriptBeginFa.Contains(searchTerm)
                        //|| concept.ScriptBeginEn.Contains(searchTerm)
                        );
                }

                if (queryOnConceptExression.FirstOrDefault() != null
                    //&& queryOnSecondaryConcept.Skip(pageIndex * pageSize).FirstOrDefault() != null
                    )
                {
                    queryOnConceptExression =
                        queryOnConceptExression
                        .Skip(pageIndex * pageSize)
                        .Take(pageSize);
                }

            }
            catch (Exception ex)
            {
                LogException(ex, "GTS.Clock.Business.RuleDesigner.BConceptExpression", "GetAllByPageBySearch");
                throw ex;
            }
            return queryOnConceptExression.ToList();
        }

        /// <summary>
        /// بررسی فيلد شروع اسكريپت فارسي
        /// فيلد شروع اسكريپت فارسي نباید خالي باشد
        /// </summary>
        /// <param name="ScriptBeginFa">شروع اسكريپت فارسي</param>
        /// <returns>خطای واسط کاربری</returns>
        public UIValidationExceptions Validation(string ScriptBeginFa)
        {
            UIValidationExceptions uiValidationExceptions = new UIValidationExceptions();

            if (string.IsNullOrEmpty(ScriptBeginFa))
                uiValidationExceptions.ExceptionList.Add(
                    new ValidationException(ExceptionResourceKeys.BExpressionRequiedScriptBeginFa, "فيلد شروع اسكريپت فارسي نميتواند خالي باشد", ExceptionSrc));
            return uiValidationExceptions;
        }

        /// <summary>
        /// ایجاد رونوشت از عبارت منطقی در دیتابیس
        /// </summary>
        /// <param name="conceptExpressionRecived">عبارت منطقی دریافتی</param>
        /// <param name="conceptFromDb">عبارت منطقی در دیتابیس</param>
        public void Copy(ConceptExpression conceptExpressionRecived, ref ConceptExpression conceptFromDb)
        {
            conceptFromDb.Parent_ID = conceptExpressionRecived.Parent_ID;
            conceptFromDb.ScriptBeginFa = conceptExpressionRecived.ScriptBeginFa;
            conceptFromDb.ScriptEndFa = conceptExpressionRecived.ScriptEndFa;
            conceptFromDb.ScriptBeginEn = conceptExpressionRecived.ScriptBeginEn;
            conceptFromDb.ScriptEndEn = conceptExpressionRecived.ScriptEndEn;
            conceptFromDb.AddOnParentCreation = conceptExpressionRecived.AddOnParentCreation;
            conceptFromDb.CanAddToFinal = conceptExpressionRecived.CanAddToFinal;
            conceptFromDb.CanEditInFinal = conceptExpressionRecived.CanEditInFinal;
            conceptFromDb.SortOrder = conceptExpressionRecived.SortOrder;
            conceptFromDb.Visible = conceptExpressionRecived.Visible;
        }

        /// <summary>
        /// بررسی دسترسی عبارت منطقی 
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckConceptRuleMasterOperationLoadAccess()
        {
        }

        /// <summary>
        /// بررسی دسترسی ویرایش عبارت منطقی در مفاهیم 
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckConceptRuleEditorLoadAccess_OnConcepts()
        {
        }

        /// <summary>
        /// بررسی دسترسی ویرایش عبارت منطقی در قوانین
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckConceptRuleEditorLoadAccess_OnRules()
        {
        }

        /// <summary>
        /// بررسی دسترسی مدیریت عبارت منطقی
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckExpressionsManagementLoadAccess()
        {
        }
         
    }
}
