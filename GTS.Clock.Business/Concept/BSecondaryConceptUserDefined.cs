using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model.Security;
using Stimulsoft.Report.Components;
using GTS.Clock.Infrastructure.NHibernateFramework;
using NHibernate;
using NHibernate.Criterion;

namespace GTS.Clock.Business.RuleDesigner
{
    /// <summary>
    /// مفاهیم ثانوی ایجاد شده توسط کاربر
    /// </summary>
    public class BSecondaryConceptUserDefined : BaseBusiness<SecondaryConcept>
    {
        readonly EntityRepository<SecondaryConcept> _cnpRep = new EntityRepository<SecondaryConcept>();
        const string ExceptionSrc = "GTS.Clock.Business.RuleDesigner";
        ISession NHSession = NHibernateSessionManager.Instance.GetSession();

        /// <summary>
        /// تعداد مفاهیم ثانوی جستجو شده را بر می گرداند
        /// </summary>
        /// <param name="searchTerm">عبارت جستجو</param>
        /// <returns>تعداد</returns>
        public int GetAllByPageBySearchCount(string searchTerm)
        {
            var count = 0;

            IEnumerable<SecondaryConcept> queryOnSecondaryConcept = null;

            try
            {
                if (string.IsNullOrEmpty(searchTerm.Trim()))
                {
                    queryOnSecondaryConcept =
                        _cnpRep.Find(concept =>
                        concept.UserDefined);
                }
                else
                {
                    var allSecondaryConcept =
                          _cnpRep.GetAll();
                    queryOnSecondaryConcept = allSecondaryConcept.Where(
                        concept =>
                              concept.UserDefined &&
                              (
                                concept.Name.Contains(searchTerm) ||
                                concept.IdentifierCode.ToString(CultureInfo.InvariantCulture).Contains(searchTerm)
                              ));
                }

                count = 0;
                if (queryOnSecondaryConcept.FirstOrDefault() != null)
                    count = queryOnSecondaryConcept.Count();
            }
            catch (Exception ex)
            {
                LogException(ex, "GTS.Clock.Business.RuleDesigner.MConceptTemplate", "GetAllByPageBySearchCount(ConceptSearchKeys searchKey, string searchTerm)");
                throw ex;
            }
            return count;
        }

        /// <summary>
        /// مفاهیم ثانوی جستجو شده را به صورت صفحه بندی شده بر می گرداند
        /// </summary>
        /// <param name="pageIndex">تعداد رکورد در هر صفحه</param>
        /// <param name="pageSize">شماره صفحه</param>
        /// <param name="searchTerm">عبارت جستجو</param>
        /// <returns>لیست مفاهیم ثانوی</returns>
        public IList<SecondaryConcept> GetAllByPageBySearch(int pageIndex, int pageSize, string searchTerm)
        {
            IEnumerable<SecondaryConcept> queryOnSecondaryConcept = null;
            try
            {
                if (string.IsNullOrEmpty(searchTerm.Trim()))
                {
                    queryOnSecondaryConcept =
                        _cnpRep.Find(concept =>
                        concept.UserDefined &&
                        concept.Type == ScndCnpPairableType.NPSC);
                }
                else
                {
                    var allSecondaryConcept =
                          _cnpRep.GetAll();

                    queryOnSecondaryConcept = allSecondaryConcept.Where(
                        concept =>
                              concept.UserDefined &&
                              concept.Type == ScndCnpPairableType.NPSC &&
                              (
                                concept.Name.Contains(searchTerm) ||
                                concept.IdentifierCode.ToString().Contains(searchTerm)
                              ));
                }

                if (queryOnSecondaryConcept.FirstOrDefault() != null
                    )
                {
                    queryOnSecondaryConcept =
                        queryOnSecondaryConcept
                        .Skip(pageIndex * pageSize)
                        .Take(pageSize);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "GTS.Clock.Business.RuleDesigner.MConceptTemplate", "GetAllByPageBySearch");
                throw ex;
            }
            return queryOnSecondaryConcept.ToList();
        }

        /// <summary>
        /// مفاهیم ثانوی غیر دوره ای جستجو شده را به صورت صفحه بندی شده بر می گرداند 
        /// </summary>
        /// <param name="pageIndex">تعداد رکورد در هر صفحه</param>
        /// <param name="pageSize">شماره صفحه</param>
        /// <param name="searchTerm">عبارت جستجو</param>
        /// <returns>لیست مفاهیم ثانوی</returns>
        public IList<SecondaryConcept> GetAllNonPeriodicByPageBySearch(int pageIndex, int pageSize, string searchTerm)
        {
            IEnumerable<SecondaryConcept> queryOnSecondaryConcept = null;
            try
            {
                if (string.IsNullOrEmpty(searchTerm.Trim()))
                {
                    queryOnSecondaryConcept =
                        _cnpRep.Find(concept =>
                        concept.UserDefined);
                }
                else
                {
                    var allSecondaryConcept =
                          _cnpRep.GetAll();

                    queryOnSecondaryConcept = allSecondaryConcept.Where(
                        concept =>
                              concept.UserDefined &&
                              (
                                concept.Name.Contains(searchTerm) ||
                                concept.IdentifierCode.ToString().Contains(searchTerm)
                              ));
                }

                if (queryOnSecondaryConcept.FirstOrDefault() != null
                    )
                {
                    queryOnSecondaryConcept =
                        queryOnSecondaryConcept
                        .Skip(pageIndex * pageSize)
                        .Take(pageSize);
                }

            }
            catch (Exception ex)
            {
                LogException(ex, "GTS.Clock.Business.RuleDesigner.MConceptTemplate", "GetAllByPageBySearch");
                throw ex;
            }
            return queryOnSecondaryConcept.ToList();
        }

        /// <summary>
        /// مفاهیم ثانوی دوره ای جستجو شده را به صورت صفحه بندی شده بر می گرداند  
        /// </summary>
        /// <param name="pageIndex">تعداد رکورد در هر صفحه</param>
        /// <param name="pageSize">شماره صفحه</param>
        /// <param name="searchTerm">عبارت جستجو</param>
        /// <returns>لیست مفاهیم ثانوی</returns>
        public IList<SecondaryConcept> GetAllPeriodicByPageBySearch(int pageIndex, int pageSize, string searchTerm)
        {
            IEnumerable<SecondaryConcept> queryOnSecondaryConcept = null;
            try
            {
                if (string.IsNullOrEmpty(searchTerm.Trim()))
                {
                    queryOnSecondaryConcept =
                        _cnpRep.Find(concept =>
                        concept.UserDefined &&
                        concept.PeriodicType == ScndCnpPeriodicType.Periodic);
                }
                else
                {
                    var allSecondaryConcept =
                          _cnpRep.GetAll();

                    queryOnSecondaryConcept = allSecondaryConcept.Where(
                        concept =>
                              concept.UserDefined &&
                              concept.PeriodicType == ScndCnpPeriodicType.Periodic &&
                              (
                                concept.Name.Contains(searchTerm) ||
                                concept.IdentifierCode.ToString().Contains(searchTerm)
                              ));
                }

                if (queryOnSecondaryConcept.FirstOrDefault() != null
                    )
                {
                    queryOnSecondaryConcept =
                        queryOnSecondaryConcept
                        .Skip(pageIndex * pageSize)
                        .Take(pageSize);
                }

            }
            catch (Exception ex)
            {
                LogException(ex, "GTS.Clock.Business.RuleDesigner.MConceptTemplate", "GetAllByPageBySearch");
                throw ex;
            }
            return queryOnSecondaryConcept.ToList();
        }

        #region Overrided Methods

        /// <summary>
        /// اعتبارسنجی عملیات درج
        /// </summary>
        /// <param name="obj">مفاهیم ثانوی</param>
        protected override void InsertValidate(SecondaryConcept obj)
        {
            GeneralValidation(obj);

            UIValidationExceptions exception = new UIValidationExceptions();

            if (_cnpRep.GetAll().Any(x => x.Name.ToUpper().Equals(obj.Name.ToUpper())))
                exception.Add(ExceptionResourceKeys.BSecondaryConceptCodeRepeated, "نام تكراري است", ExceptionSrc);

            if (_cnpRep.GetAll().Any(x => x.IdentifierCode.Equals(obj.IdentifierCode)))
                exception.Add(ExceptionResourceKeys.BSecondaryConceptCodeRepeated, "كد تكراري است", ExceptionSrc);


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبارسنجی عملیات ویرایش
        /// </summary>
        /// <param name="obj">مفاهیم ثانوی</param>
        protected override void UpdateValidate(SecondaryConcept obj)
        {
            GeneralValidation(obj);

            UIValidationExceptions exception = new UIValidationExceptions();

            if (_cnpRep.GetAll().Any(x => x.ID != obj.ID && x.Name.ToUpper().Equals(obj.Name.ToUpper())))
                exception.Add(ExceptionResourceKeys.BSecondaryConceptCodeRepeated, "نام تكراري است", ExceptionSrc);

            if (_cnpRep.GetAll().Any(x => x.ID != obj.ID && x.IdentifierCode.Equals(obj.IdentifierCode)))
                exception.Add(ExceptionResourceKeys.BSecondaryConceptCodeRepeated, "كد تكراري است", ExceptionSrc);


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبارسنجی عملیات حذف
        /// </summary>
        /// <param name="obj">مفاهیم ثانوی</param>
        protected override void DeleteValidate(SecondaryConcept obj)
        {

        }

        /// <summary>
        /// اعتبارسنجی عمومی
        /// </summary>
        /// <param name="obj">مفاهیم ثانوی</param>
        private void GeneralValidation(SecondaryConcept obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (string.IsNullOrEmpty(obj.Name))
                exception.Add(ExceptionResourceKeys.BSecondaryConceptNameRepeated, "نام اجباري است", ExceptionSrc);

            if (obj.IdentifierCode < 1)
                exception.Add(ExceptionResourceKeys.BSecondaryConceptCodeRequierd, "كد اجباري است", ExceptionSrc);

            if (string.IsNullOrEmpty(obj.Color))
                exception.Add(ExceptionResourceKeys.BSecondaryConceptNameRepeated, "رنگ اجباري است", ExceptionSrc);

            if (IsPeriodicTypeEmpty(obj))
                exception.Add(ExceptionResourceKeys.BSecondaryConceptPeriodicTypeRequierd, "نوع مفهوم اجباري است", ExceptionSrc);

            if (IsTypeEmpty(obj))
                exception.Add(ExceptionResourceKeys.BSecondaryConceptPeriodicTypeRequierd, "جنس مفهوم اجباري است", ExceptionSrc);

            if (IsCalcSituationTypeEmpty(obj))
                exception.Add(ExceptionResourceKeys.BSecondaryConceptPeriodicTypeRequierd, "زمان اجرا اجباري است", ExceptionSrc);

            if (IsPersistSituationTypeEmpty(obj))
                exception.Add(ExceptionResourceKeys.BSecondaryConceptPeriodicTypeRequierd, "نوع ذخيره‌سازي اجباري است", ExceptionSrc);

            if (IsPersistSituationTypeEmpty(obj))
                exception.Add(ExceptionResourceKeys.BSecondaryConceptCustomeCategoryCodeRequierd, "دسته بندی اجباري است", ExceptionSrc);


            if (exception.Count > 0)
            {
                throw exception;
            }

        }

        #endregion

        #region Validation Methods

        /// <summary>
        /// اعتبار سنجی مفاهیم ثانوی در قالب جسون
        /// فيلد  نوع نميتواند خالي باشد
        /// فيلد  جنس نميتواند خالي باشد
        /// فيلد  زمان اجرا نميتواند خالي باشد
        /// فيلد  نحوه ذخيره سازي نميتواند خالي باشد
        /// </summary>
        /// <param name="PeriodicTypeString">فیلد نوع بازه ای یا عددی</param>
        /// <param name="TypeString">فیلد جنس</param>
        /// <param name="CalcSituationTypeString">فیلد زمان اجرا</param>
        /// <param name="PersistSituationTypeString">فیلد نحوه ذخيره سازي</param>
        /// <returns></returns>
        public UIValidationExceptions SecondaryConceptEnumJsonObjectsValidation(
            string PeriodicTypeString,
            string TypeString,
            string CalcSituationTypeString,
            string PersistSituationTypeString
            )
        {

            UIValidationExceptions uiValidationExceptions = new UIValidationExceptions();

            if (string.IsNullOrEmpty(PeriodicTypeString) || PeriodicTypeString == "-1")
                uiValidationExceptions.ExceptionList.Add(
                    new ValidationException(ExceptionResourceKeys.BSecondaryConceptPeriodicTypeRequierd, "فيلد  نوع نميتواند خالي باشد", ExceptionSrc));

            if (string.IsNullOrEmpty(TypeString) || TypeString == "-1")
                uiValidationExceptions.ExceptionList.Add(
                    new ValidationException(ExceptionResourceKeys.BSecondaryConceptTypeRequierd, "فيلد  جنس نميتواند خالي باشد", ExceptionSrc));

            if (string.IsNullOrEmpty(CalcSituationTypeString) || CalcSituationTypeString == "-1")
                uiValidationExceptions.ExceptionList.Add(
                    new ValidationException(ExceptionResourceKeys.BSecondaryConceptCalcSituationTypeRequierd, "فيلد  زمان اجرا نميتواند خالي باشد", ExceptionSrc));

            if (string.IsNullOrEmpty(PersistSituationTypeString) || PersistSituationTypeString == "-1")
                uiValidationExceptions.ExceptionList.Add(
                    new ValidationException(ExceptionResourceKeys.BSecondaryConceptPersistSituationTypeRequierd, "فيلد  نحوه ذخيره سازي نميتواند خالي باشد", ExceptionSrc));

            return uiValidationExceptions;
        }

        /// <summary>
        /// اعتبار سنجی مفاهیم ثانوی در قالب جسون
        /// مفهومي انتخابي بدرستي باید پر شده باشد
        /// </summary>
        /// <param name="ConceptString">جسون مفهوم</param>
        /// <returns>خطای واسط کاربری</returns>
        public UIValidationExceptions SecondaryConceptJsonObjectValidation(string ConceptString)
        {
            UIValidationExceptions uiValidationExceptions = new UIValidationExceptions();

            if (string.IsNullOrEmpty(ConceptString))
                uiValidationExceptions.ExceptionList.Add(
                    new ValidationException(ExceptionResourceKeys.BSecondaryConceptRequierd, "مفهومي انتخابي بدرستي پر نشده است", ExceptionSrc));

            try
            {
                Newtonsoft.Json.JsonConvert.DeserializeObject<SecondaryConcept>(ConceptString);
            }
            catch (Exception ex)
            {
                uiValidationExceptions.ExceptionList.Add(
                    new ValidationException(ExceptionResourceKeys.BSecondaryConceptRequierd, "مفهومي انتخابي بدرستي پر نشده است", ExceptionSrc));
            }

            return uiValidationExceptions;
        }

        /// <summary>
        /// بررسی خالی بودن نوع دوره ای یا غیر دوره ای مفهوم ثانویه
        /// </summary>
        /// <param name="secondaryConcept">مفهوم ثانویه</param>
        /// <returns>بلی/خیر</returns>
        public bool IsPeriodicTypeEmpty(SecondaryConcept secondaryConcept)
        {
            return secondaryConcept.PeriodicType == null;
        }

        /// <summary>
        ///  بررسی خالی بودن نوع بازه ای یا عددی مفهوم ثانویه 
        /// </summary>
        /// <param name="secondaryConcept">مفهوم ثانویه</param>
        /// <returns>بلی/خیر</returns>
        public bool IsTypeEmpty(SecondaryConcept secondaryConcept)
        {
            return secondaryConcept.Type == null;
        }

        /// <summary>
        /// بررسی خالی بودن وضعیت  زمان اجرای مفهوم
        /// </summary>
        /// <param name="secondaryConcept">مفهوم ثانوی</param>
        /// <returns>بلی/خیر</returns>
        public bool IsCalcSituationTypeEmpty(SecondaryConcept secondaryConcept)
        {
            return secondaryConcept.CalcSituationType == null;
        }

        /// <summary>
        /// بررسی خالی بودن وضعیت ذخیره سازی مقدار مفهوم در دیتابیس
        /// </summary>
        /// <param name="secondaryConcept">مفهوم ثانوی</param>
        /// <returns>بلی/خیر</returns>
        public bool IsPersistSituationTypeEmpty(SecondaryConcept secondaryConcept)
        {
            return secondaryConcept.PersistSituationType == null;
        }

        #endregion

        /// <summary>
        /// بررسی دسترسی مدیریت مفاهیم
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckConceptManagementLoadAccess()
        {
        }

        /// <summary>
        /// بررسی دسترسی مدیریت قوانین
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckRuleManagementLoadAccess()
        { 
        }

        /// <summary>
        /// عملیات درج مفاهیم ثانوی در دیتابیس
        /// </summary>
        /// <param name="secondaryConcept">مفاهیم ثانوی</param>
        /// <returns>کلید اصلی مفاهیم ثانوی</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertConcept(SecondaryConcept secondaryConcept)
        {
            try
            {
                AddUpdateRelatedExpression(secondaryConcept);

                return this.SaveChanges(secondaryConcept, UIActionType.ADD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// عملیات ویرایش مفاهیم ثانوی در دیتابیس
        /// </summary>
        /// <param name="secondaryConcept">مفاهیم ثانوی</param>
        /// <returns>کلید اصلی مفاهیم ثانوی</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateConcept(SecondaryConcept secondaryConcept)
        {
            try
            {
                return this.SaveChanges(secondaryConcept, UIActionType.EDIT);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// عملیات حذف مفاهیم ثانوی در دیتابیس
        /// </summary>
        /// <param name="secondaryConcept">مفاهیم ثانوی</param>
        /// <returns>کلید اصلی مفاهیم ثانوی</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteConcept(SecondaryConcept secondaryConcept)
        {
            try
            {
                return this.SaveChanges(secondaryConcept, UIActionType.DELETE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// مقدار کلید اصلی مفهوم ثانوی را از آبجکت جسون بر می گرداند
        /// در صورت عدم وجود کلید اصلی در آبجکت جسون صفر بر می گرداند 
        /// </summary>
        /// <param name="ConceptString"> آبجکت جسون مفهوم ثانوی</param>
        /// <returns>مقدار کلید اصلی</returns>
        public decimal GetIdFromConceptJsonObject(string ConceptString)
        {
            Newtonsoft.Json.Linq.JObject cnpJObject =
                Newtonsoft.Json.JsonConvert
                .DeserializeObject<Newtonsoft.Json.Linq.JObject>(ConceptString);

            if (cnpJObject.HasValues && (decimal)cnpJObject.GetValue("ID") > 0)
            {
                return (decimal)cnpJObject.GetValue("ID");
            }

            return 0;
        }

        /// <summary>
        /// ایجاد رونوشت از مفاهیم ثانوی
        /// </summary>
        /// <param name="secondaryConceptFrom">مفاهیم ثانوی مبدا</param>
        /// <param name="secondaryConceptTo">مفاهیم ثانوی مقصد</param>
        public void Copy(SecondaryConcept secondaryConceptFrom, ref SecondaryConcept secondaryConceptTo)
        {
            secondaryConceptTo.IdentifierCode = secondaryConceptFrom.IdentifierCode;
            secondaryConceptTo.Name = secondaryConceptFrom.Name;
            secondaryConceptTo.Script = secondaryConceptFrom.Script;
            secondaryConceptTo.Color = secondaryConceptFrom.Color;
            secondaryConceptTo.KeyColumnName = secondaryConceptFrom.KeyColumnName;
            secondaryConceptTo.CSharpCode = secondaryConceptFrom.CSharpCode;
            secondaryConceptTo.Script = secondaryConceptFrom.Script;
            secondaryConceptTo.PeriodicType = secondaryConceptFrom.PeriodicType;
            secondaryConceptTo.Type = secondaryConceptFrom.Type;
            secondaryConceptTo.CalcSituationType = secondaryConceptFrom.CalcSituationType;
            secondaryConceptTo.PersistSituationType = secondaryConceptFrom.PersistSituationType;
            secondaryConceptTo.UserDefined = secondaryConceptFrom.UserDefined;
            secondaryConceptTo.CSharpCode = secondaryConceptFrom.CSharpCode;
            secondaryConceptTo.JsonObject = secondaryConceptFrom.JsonObject;
        }

        /// <summary>
        /// ارتباطات مفهوم ثانوی با بقیه مدل ها را دیتابیس ایجاد و بروزرسانی می کند
        /// </summary>
        /// <param name="secondaryConcept">مفهوم ثانوی</param>
        private void AddUpdateRelatedExpression(SecondaryConcept secondaryConcept)
        {

            try
            {
                var bCnpExpression = new BConceptExpression();

                var cnpExpressionRoot = bCnpExpression.GetRoot();
                if (cnpExpressionRoot == null)
                {
                    try
                    {
                        cnpExpressionRoot = new ConceptExpression
                        {
                            AddOnParentCreation = false,
                            CanAddToFinal = false,
                            CanEditInFinal = false,

                            CustomeCategoryCode = 0,
                            Parent_ID = null,
                            ScriptBeginEn = string.Empty,
                            ScriptEndEn = string.Empty,
                            ScriptBeginFa = "...",
                            ScriptEndFa = string.Empty,
                            SortOrder = 0,
                            Visible = true
                        };

                        bCnpExpression.InsertConceptExpression(cnpExpressionRoot);
                    }
                    catch (Exception)
                    {
                        var uiException = new UIValidationExceptions();
                        uiException.Add(ExceptionResourceKeys.BSecondaryConceptCustomeCategoryParentExpressionRequierd,
                            "مولفه ریشه (...) ساخته نشد", ExceptionSrc);
                        throw uiException;
                    }
                }


                #region اجزاي كاربردي

                var expressionParent1 =
                    bCnpExpression.GetByParentId(cnpExpressionRoot.ID)
                        .FirstOrDefault(x => x.ScriptBeginFa.Contains("اجزاي كاربردي"));
                if (expressionParent1 == null)
                {
                    try
                    {
                        expressionParent1 = new ConceptExpression
                        {
                            AddOnParentCreation = false,
                            CanAddToFinal = false,
                            CanEditInFinal = false,

                            CustomeCategoryCode = int.Parse(secondaryConcept.CustomCategoryCode),
                            Parent_ID = cnpExpressionRoot.ID,
                            ScriptBeginEn = string.Empty,
                            ScriptEndEn = string.Empty,
                            ScriptBeginFa = "اجزاي كاربردي",
                            ScriptEndFa = string.Empty,
                            SortOrder = 0,
                            Visible = true
                        };

                        bCnpExpression.InsertConceptExpression(expressionParent1);
                    }
                    catch (Exception)
                    {
                        var uiException = new UIValidationExceptions();
                        uiException.Add(ExceptionResourceKeys.BSecondaryConceptCustomeCategoryParentExpressionRequierd,
                            "مولفه اجزاي كاربردي ساخته نشد", ExceptionSrc);
                        throw uiException;
                    }
                }


                #endregion

                #region مفهوم

                var expressionParent2 =
                    bCnpExpression.GetByParentId(expressionParent1.ID)
                        .FirstOrDefault(x => x.ScriptBeginFa.Contains("مفهوم"));
                if (expressionParent2 == null)
                {
                    try
                    {
                        expressionParent2 = new ConceptExpression
                        {
                            AddOnParentCreation = false,
                            CanAddToFinal = false,
                            CanEditInFinal = false,

                            CustomeCategoryCode = int.Parse(secondaryConcept.CustomCategoryCode),
                            Parent_ID = expressionParent1.ID,
                            ScriptBeginEn = string.Empty,
                            ScriptEndEn = string.Empty,
                            ScriptBeginFa = "مفهوم",
                            ScriptEndFa = string.Empty,
                            SortOrder = 1,
                            Visible = true
                        };

                        bCnpExpression.InsertConceptExpression(expressionParent2);
                    }
                    catch (Exception)
                    {
                        var uiException = new UIValidationExceptions();
                        uiException.Add(ExceptionResourceKeys.BSecondaryConceptCustomeCategoryParentExpressionRequierd,
                            "مولفه مفهوم ساخته نشد", ExceptionSrc);
                        throw uiException;
                    }
                }

                #endregion

                #region بازه ای مقداری عملگر

                string periodicTypeTitle;
                switch (secondaryConcept.Type)
                {
                    case ScndCnpPairableType.PSC:
                        periodicTypeTitle = "بازه ای";
                        break;
                    case ScndCnpPairableType.NPSC:
                        periodicTypeTitle = "مقداری";
                        break;
                    default:
                        periodicTypeTitle = "عملگر";
                        break;
                }

                var expressionParent3 =
                    bCnpExpression.GetByParentId(expressionParent2.ID)
                        .FirstOrDefault(x => x.ScriptBeginFa.Contains(periodicTypeTitle));
                if (expressionParent3 == null)
                {
                    try
                    {
                        expressionParent3 = new ConceptExpression
                        {
                            AddOnParentCreation = false,
                            CanAddToFinal = false,
                            CanEditInFinal = false,

                            CustomeCategoryCode = int.Parse(secondaryConcept.CustomCategoryCode),
                            Parent_ID = expressionParent2.ID,
                            ScriptBeginEn = string.Empty,
                            ScriptEndEn = string.Empty,
                            ScriptBeginFa = periodicTypeTitle,
                            ScriptEndFa = string.Empty,
                            SortOrder = 1,
                            Visible = true
                        };

                        bCnpExpression.InsertConceptExpression(expressionParent3);
                    }
                    catch (Exception)
                    {
                        var uiException = new UIValidationExceptions();
                        uiException.Add(ExceptionResourceKeys.BSecondaryConceptCustomeCategoryParentExpressionRequierd,
                            "مولفه" + periodicTypeTitle + "موجود نیست", ExceptionSrc);
                        throw uiException;
                    }
                }

                #endregion

                #region کاری مرخصی ماموریت غیبت اضافه کار

                var expressionParent4 =
                    bCnpExpression.GetByParentId(expressionParent3.ID)
                        .FirstOrDefault(x => x.CustomeCategoryCode == int.Parse(secondaryConcept.CustomCategoryCode));
                if (expressionParent4 == null)
                {
                    var customCategoryTitle = string.Empty;

                    try
                    {
                        switch (int.Parse(secondaryConcept.CustomCategoryCode))
                        {
                            case 1:
                                {
                                    customCategoryTitle = "کاری";
                                    break;
                                }
                            case 2:
                                {
                                    customCategoryTitle = "مرخصی";
                                    break;
                                }
                            case 3:
                                {
                                    customCategoryTitle = "ماموریت";
                                    break;
                                }
                            case 4:
                                {
                                    customCategoryTitle = "غیبت";
                                    break;
                                }
                            case 5:
                                {
                                    customCategoryTitle = "اضافه کار";
                                    break;
                                }
                            default:
                                {
                                    customCategoryTitle = "کاری";
                                    break;
                                }
                        }

                        expressionParent4 = new ConceptExpression
                        {
                            AddOnParentCreation = false,
                            CanAddToFinal = false,
                            CanEditInFinal = false,

                            CustomeCategoryCode = int.Parse(secondaryConcept.CustomCategoryCode),
                            Parent_ID = expressionParent3.ID,
                            ScriptBeginEn = string.Empty,
                            ScriptEndEn = string.Empty,
                            ScriptBeginFa = customCategoryTitle,
                            ScriptEndFa = string.Empty,
                            SortOrder = 1,
                            Visible = true
                        };

                        bCnpExpression.InsertConceptExpression(expressionParent4);
                    }
                    catch (Exception)
                    {
                        var uiException = new UIValidationExceptions();
                        uiException.Add(ExceptionResourceKeys.BSecondaryConceptCustomeCategoryParentExpressionRequierd,
                            "مولفه" + customCategoryTitle + "موجود نیست", ExceptionSrc);
                        throw uiException;
                    }
                }

                #endregion

                var conceptExpressionObj = new ConceptExpression
                {
                    AddOnParentCreation = false,
                    CanAddToFinal = true,
                    CanEditInFinal = false,
                    CustomeCategoryCode = int.Parse(secondaryConcept.CustomCategoryCode),
                    Parent_ID = expressionParent4.ID,
                    ScriptBeginEn = string.Format("this.DoConcept({0}).Value", secondaryConcept.IdentifierCode),
                    ScriptEndEn = string.Empty,
                    ScriptBeginFa = secondaryConcept.Name,
                    ScriptEndFa = string.Empty,
                    SortOrder = (int)secondaryConcept.ID,
                    Visible = true
                };

                bCnpExpression.InsertConceptExpression(conceptExpressionObj);
            }
            catch (Exception ex)
            {
                var uiException = new UIValidationExceptions();
                uiException.Add(ExceptionResourceKeys.BSecondaryConceptCustomeCategoryParentExpressionRequierd,
                    ex.Message, ExceptionSrc);
                throw uiException;
            }
        }
    }
}