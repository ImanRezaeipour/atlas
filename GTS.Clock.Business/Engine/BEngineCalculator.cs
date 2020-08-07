using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Temp;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Validation.Configuration;
using GTS.Clock.Infrastructure.Exceptions.UI;

namespace GTS.Clock.Business.Engine
{
    /// <summary>
    /// انجام محاسبات
    /// </summary>
    public class BEngineCalculator : MarshalByRefObject
    {
        #region Properties
        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>

        public virtual decimal PersonID { get; set; }
        public virtual DateTime FromDate { get; set; }
        public virtual DateTime ToDate { get; set; }

        #endregion
        const string ExceptionSrc = "GTS.Clock.Business.Engine.BEngineCalculator";


        private GTSEngineWS.TotalWebServiceClient gtsEngineWS = new GTS.Clock.Business.GTSEngineWS.TotalWebServiceClient();
        UIValidationGroupingRepository uivalidationGroupingRepository = new UIValidationGroupingRepository();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        private void CallUIValidator(Object obj)
        {
            ILockCalculationUIValidation validator = UIValidationFactory.GetRepository<ILockCalculationUIValidation>();
            //  IRequestUIValidation validator = UIValidationFactory.GetRepository<IRequestUIValidation>();
            if (validator != null)
            {
                validator.DoValidate(obj);
            }
        }

        /// <summary>
        /// انجام محاسبات یک پرسنل  در بازه زمانی مشخص
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="fromDate">تاریخ شروع</param>
        /// <param name="toDate">تاریخ پایان</param>
        /// <param name="forceCalculate">محاسبات اجباری</param>
        /// <returns>انجام شد یا نشد</returns>
        public bool Calculate(decimal personId, string fromDate, string toDate, bool forceCalculate)
        {
            try
            {
                DateTime from, to;
                object obj = new object();
                UIValidationExceptions exception = new UIValidationExceptions();
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    from = Utility.ToMildiDate(fromDate);
                    to = Utility.ToMildiDate(toDate);
                }
                else
                {
                    from = Utility.ToMildiDateTime(fromDate);
                    to = Utility.ToMildiDateTime(toDate);
                }
                if (to != Utility.GTSMinStandardDateTime && from > to)
                    exception.Add(new ValidationException(ExceptionResourceKeys.CalculationStartDateIsGreaterThanCalculationEndDate, "تاریخ ابتدا از تاریخ انتها بزرگتر است", ExceptionSrc));
                if(personId==0)
                    exception.Add(new ValidationException(ExceptionResourceKeys.CalculationPersonNotSelected, "پرسنلی انتخاب نشده است.", ExceptionSrc));
                if (exception.Count > 0)
                    throw exception;

                decimal[] ids = new decimal[1];
                ids[0] = personId;
                PersonID = ids[0];
                FromDate = from;
                ToDate = to;
                if (forceCalculate)
                {
                    BusinessEntity entity = new BusinessEntity();
                    entity.UpdateCFP(new List<Person>() { new Person() { ID = personId } }, from, true);
                }
                BTemp bTemp = new BTemp();
                string operationGUID = bTemp.InsertTempList(ids.ToList());
                CallUIValidator(this);
                gtsEngineWS.GTS_ExecutePersonsByToDateGUID(BUser.CurrentUser.UserName, operationGUID, to);
                bTemp.DeleteTempList(operationGUID);
                BaseBusiness<Entity>.LogUserAction(String.Format("CalculateAll -> personId: {0} -->Calculate(personId,toDate)", personId));
                return true;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BEngineCalculator", "Calculate(personId,toDate)");
                throw ex;
            }
        }

        /// <summary>
        /// انجام محاسبات پرسنل جستجو شده در بازه زمانی مشخص
        /// </summary>
        /// <param name="proxy">پروکسی جستجوی پیشرفته پرسنل</param>
        /// <param name="fromDate">تاریخ شروع</param>
        /// <param name="toDate">تاریخ پایان</param>
        /// <param name="forceCalculate">محاسبات اجباری</param>
        /// <returns>انجام شد یا نشد</returns>
        public bool Calculate(PersonAdvanceSearchProxy proxy, string fromDate ,string toDate, bool forceCalculate)
        {
            try
            {
                bool IsCalculationDependOnLockDate = true;
                string IsCalculationDependOnLockDateStr = System.Configuration.ConfigurationManager.AppSettings["IsCalculationDependOnLockDate"];
                
                bool resultGetAppSetting = Boolean.TryParse(IsCalculationDependOnLockDateStr,out IsCalculationDependOnLockDate);
                if (!resultGetAppSetting)
                    IsCalculationDependOnLockDate = true;
                UIValidationExceptions exception = new UIValidationExceptions();
                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
                IList<Person> personList = searchTool.GetPersonInAdvanceSearch(proxy, 0, count)
                    .Where(x => x.Active).ToList();
                var ids = from o in personList
                          select o.ID;
                IList<decimal> PersonIdCalculateList = new List<decimal>();
                DateTime from, to;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    from = Utility.ToMildiDate(fromDate);
                    to = Utility.ToMildiDate(toDate);
                }
                else
                {
                    from = Utility.ToMildiDateTime(fromDate);
                    to = Utility.ToMildiDateTime(toDate);
                }
                if (to != Utility.GTSMinStandardDateTime && from > to)
                    exception.Add(new ValidationException(ExceptionResourceKeys.CalculationStartDateIsGreaterThanCalculationEndDate, "تاریخ ابتدا از تاریخ انتها بزرگتر است", ExceptionSrc));
                if (exception.Count > 0)
                    throw exception;

                if (forceCalculate)
                {
                    BusinessEntity entity = new BusinessEntity();
                    
             
                    Dictionary<decimal, decimal> UiValidationGroupIdPersonList = uivalidationGroupingRepository.GetUivalidationPersonIdList(ids.ToList<decimal>());
                    Dictionary<decimal, DateTime> uivalidationGroupIdDic = new Dictionary<decimal, DateTime>();
                    foreach (KeyValuePair<decimal,decimal> uiValidateionGrpId in UiValidationGroupIdPersonList)
                    {

                        if (!uivalidationGroupIdDic.ContainsKey(uiValidateionGrpId.Value))
                        {
                            
                            DateTime calculationLockDate = entity.UIValidator.GetCalculationLockDateByGroup(uiValidateionGrpId.Value);
                            uivalidationGroupIdDic.Add(uiValidateionGrpId.Value, calculationLockDate);
                        }

                    }
                    int personFailedToCalculateCount = 0;
                    if (IsCalculationDependOnLockDate)
                    {
                        foreach (decimal item in ids)
                        {
                            UIValidationExceptions exceptionLockDate = new UIValidationExceptions();
                            if (UiValidationGroupIdPersonList.Keys.Contains(item))
                            {
                                decimal groupId = UiValidationGroupIdPersonList.FirstOrDefault(u => u.Key == item).Value;
                                DateTime calculationPersonLockDate = uivalidationGroupIdDic.FirstOrDefault(u => u.Key == groupId).Value;
                                if (calculationPersonLockDate >= from)
                                {
                                    personFailedToCalculateCount++;
                                    
                                    exceptionLockDate.Add(new ValidationException(ExceptionResourceKeys.UIValidation_R3_LockCalculationFromDate, String.Format("خطا در انجام محاسبات - محاسبات برای پرسنل ({0}) بسته شده است", personList.FirstOrDefault(p => p.ID == item).BarCode + "-" + personList.FirstOrDefault(p => p.ID == item).Name), ExceptionSrc));
                                    BaseBusiness<Entity>.LogException(exceptionLockDate, "BEngineCalculator", "Calculate");

                                }
                                else
                                {
                                    PersonIdCalculateList.Add(item);
                                }

                            }
                            else
                            {
                                exceptionLockDate.Add(new ValidationException(ExceptionResourceKeys.UIValidation_R3_LockCalculationFromDate, String.Format("خطا در انجام محاسبات - تاریخ بستن محاسبات برای پرسنل {0} یافت نشد", personList.FirstOrDefault(p => p.ID == item).BarCode + "-" + personList.FirstOrDefault(p => p.ID == item).Name), ExceptionSrc));
                                BaseBusiness<Entity>.LogException(exceptionLockDate, "BEngineCalculator", "Calculate");
                            }

                        }
                    }
                    else
                    {
                        PersonIdCalculateList = ids.ToList();
                    }
                    
                    IList<CFP> cfpPersonList = new List<CFP>();
                    if (PersonIdCalculateList.Count > 0)
                        cfpPersonList = entity.GetCFPPersons(PersonIdCalculateList.Select(a => a).ToList<decimal>());
                    IList<decimal> cfpPersonIdInsertList = new List<decimal>();
                    entity.UpdateCfpByPersonList(PersonIdCalculateList, from);
                    cfpPersonIdInsertList = PersonIdCalculateList.Where(p => cfpPersonList != null && !cfpPersonList.Select(c => c.PrsId).ToList().Contains(p)).Select(p => p).Distinct().ToList<decimal>();
                    if (cfpPersonIdInsertList.Count > 0)
                        entity.InsertCfpByPersonList(cfpPersonIdInsertList, from);
                    SessionHelper.SaveSessionValue(SessionHelper.PersonIsFailedForCalculate,personFailedToCalculateCount);
                }
                else
                    PersonIdCalculateList = ids.ToList();

                BTemp bTemp = new BTemp();
                string operationGUID = bTemp.InsertTempList(PersonIdCalculateList);
                gtsEngineWS.GTS_ExecutePersonsByToDateGUID(BUser.CurrentUser.UserName, operationGUID, to);
                bTemp.DeleteTempList(operationGUID);
                BaseBusiness<Entity>.LogUserAction(String.Format("CalculateAll -> Count: {0} -->Calculate(AdvanceSearch,toDate)", PersonIdCalculateList.Count));
                return true;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BEngineCalculator", "Calculate(AdvanceSearch,toDate)");
                throw ex;
            }
        }

        /// <summary>
        /// انجام محاسبات پرسنل جستجو شده در بازه زمانی مشخص
        /// </summary>
        /// <param name="searchKey">عبارت جستجو</param>
        /// <param name="fromDate">تاریخ شروع</param>
        /// <param name="toDate">تاریخ پایان</param>
        /// <param name="forceCalculate">محاسبات اجباری</param>
        /// <returns>انجام شد یا نشد</returns>
        public bool Calculate(string searchKey, string fromDate, string toDate, bool forceCalculate)
        {
            try
            {
                bool IsCalculationDependOnLockDate = true;
                string IsCalculationDependOnLockDateStr = System.Configuration.ConfigurationManager.AppSettings["IsCalculationDependOnLockDate"];

                bool resultGetAppSetting = Boolean.TryParse(IsCalculationDependOnLockDateStr, out IsCalculationDependOnLockDate);
                if (!resultGetAppSetting)
                    IsCalculationDependOnLockDate = true;
                UIValidationExceptions exception = new UIValidationExceptions();
                ISearchPerson searchTool = new BPerson();
                IList<Person> personList = searchTool.QuickSearch(searchKey, PersonCategory.Public)
                    .Where(x => x.Active).ToList();
                IList<decimal> PersonIdCalculateList = new List<decimal>();
                var ids = from o in personList
                          select o.ID;
                DateTime from, to;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    from = Utility.ToMildiDate(fromDate);
                    to = Utility.ToMildiDate(toDate);
                }
                else
                {
                    from = Utility.ToMildiDateTime(fromDate);
                    to = Utility.ToMildiDateTime(toDate);
                }
                if (to != Utility.GTSMinStandardDateTime && from > to)
                    exception.Add(new ValidationException(ExceptionResourceKeys.CalculationStartDateIsGreaterThanCalculationEndDate, "تاریخ ابتدا از تاریخ انتها بزرگتر است", ExceptionSrc));
                if (exception.Count > 0)
                    throw exception;
                if (forceCalculate)
                {
                    BusinessEntity entity = new BusinessEntity();


                    Dictionary<decimal, decimal> UiValidationGroupIdPersonList = uivalidationGroupingRepository.GetUivalidationPersonIdList(ids.ToList<decimal>());
                    Dictionary<decimal, DateTime> uivalidationGroupIdDic = new Dictionary<decimal, DateTime>();
                    foreach (KeyValuePair<decimal, decimal> uiValidateionGrpId in UiValidationGroupIdPersonList)
                    {

                        if (!uivalidationGroupIdDic.ContainsKey(uiValidateionGrpId.Value))
                        {

                            DateTime calculationLockDate = entity.UIValidator.GetCalculationLockDateByGroup(uiValidateionGrpId.Value);
                            uivalidationGroupIdDic.Add(uiValidateionGrpId.Value, calculationLockDate);
                        }

                    }
                    int personFailedToCalculateCount = 0;
                    if (IsCalculationDependOnLockDate)
                    {
                        foreach (decimal item in ids)
                        {
                             UIValidationExceptions exceptionLockDate = new UIValidationExceptions();
                            if (UiValidationGroupIdPersonList.Keys.Contains(item))
                            {
                                decimal groupId = UiValidationGroupIdPersonList.FirstOrDefault(u => u.Key == item).Value;
                                DateTime calculationPersonLockDate = uivalidationGroupIdDic.FirstOrDefault(u => u.Key == groupId).Value;
                                if (calculationPersonLockDate >= from)
                                {
                                    personFailedToCalculateCount++;

                                    exceptionLockDate.Add(new ValidationException(ExceptionResourceKeys.UIValidation_R3_LockCalculationFromDate, String.Format("خطا در انجام محاسبات - محاسبات برای پرسنل ({0}) بسته شده است", personList.FirstOrDefault(p => p.ID == item).BarCode + "-" + personList.FirstOrDefault(p => p.ID == item).Name), ExceptionSrc));
                                    BaseBusiness<Entity>.LogException(exceptionLockDate, "BEngineCalculator", "Calculate");

                                }
                                else
                                {
                                    PersonIdCalculateList.Add(item);
                                }
                            }
                            else
                            {
                                exceptionLockDate.Add(new ValidationException(ExceptionResourceKeys.UIValidation_R3_LockCalculationFromDate, String.Format("خطا در انجام محاسبات - تاریخ بستن محاسبات برای پرسنل {0} یافت نشد", personList.FirstOrDefault(p => p.ID == item).BarCode + "-" + personList.FirstOrDefault(p => p.ID == item).Name), ExceptionSrc));
                                BaseBusiness<Entity>.LogException(exceptionLockDate, "BEngineCalculator", "Calculate");
                            }
                        }
                    }
                    else
                    {
                        PersonIdCalculateList = ids.ToList();
                    }
                    IList<CFP> cfpPersonList = new List<CFP>();
                    if (PersonIdCalculateList.Count > 0)
                        cfpPersonList = entity.GetCFPPersons(PersonIdCalculateList.Select(a => a).ToList<decimal>());
                    IList<decimal> cfpPersonIdInsertList = new List<decimal>();
                    entity.UpdateCfpByPersonList(PersonIdCalculateList, from);
                    cfpPersonIdInsertList = PersonIdCalculateList.Where(p => cfpPersonList != null && !cfpPersonList.Select(c => c.PrsId).ToList().Contains(p)).Select(p => p).Distinct().ToList<decimal>();
                    if (cfpPersonIdInsertList.Count > 0)
                        entity.InsertCfpByPersonList(cfpPersonIdInsertList, from);
                    SessionHelper.SaveSessionValue(SessionHelper.PersonIsFailedForCalculate, personFailedToCalculateCount);
                }
                else
                    PersonIdCalculateList = ids.ToList();

                BTemp bTemp = new BTemp();
                string operationGUID = bTemp.InsertTempList(PersonIdCalculateList);
                gtsEngineWS.GTS_ExecutePersonsByToDateGUID(BUser.CurrentUser.UserName, operationGUID, to);
                bTemp.DeleteTempList(operationGUID);
                BaseBusiness<Entity>.LogUserAction(String.Format("CalculateAll -> searchKey: {0} And Count: {1} -->Calculate(searchKey, toDate)", searchKey, PersonIdCalculateList.Count));
                return true;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BEngineCalculator", "Calculate(searchKey, toDate)");
                throw ex;
            }
        }

        /// <summary>
        /// انجام محاسبات همه پرسنل در بازه زمانی مشخص 
        /// </summary>
        /// <param name="fromDate">تاریخ شروع</param>
        /// <param name="toDate">تاریخ پایان</param>
        /// <param name="forceCalculate">محاسبات اجباری</param>
        /// <returns>انجام شد یا نشد</returns>
        public bool Calculate(string fromDate,string toDate, bool forceCalculate)
        {
            try
            {
                return this.Calculate(String.Empty, fromDate, toDate, forceCalculate);
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BEngineCalculator", "Calculate(toDate)");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد افرادی که برای محاسبه فرستاده شده و نیاز به محاسبه داشتند
        /// </summary>
        /// <returns>تعداد</returns>
        public int GetTotalCountInCalculating()
        {
            try
            {
                int total = 0;
                if (SessionHelper.HasSessionValue(SessionHelper.BusinessTotalCalculateCount))
                {
                    total = Utility.ToInteger(SessionHelper.GetSessionValue(SessionHelper.BusinessTotalCalculateCount));
                }
                int newTotal = gtsEngineWS.GTS_GETTotalExecuting(BUser.CurrentUser.UserName);
                if (newTotal > total)
                {
                    SessionHelper.SaveSessionValue(SessionHelper.BusinessTotalCalculateCount, newTotal);
                    return newTotal;
                }
                if (SessionHelper.GetSessionValue(SessionHelper.PersonIsFailedForCalculate) != null)
                {
                    total += Convert.ToInt32(SessionHelper.GetSessionValue(SessionHelper.PersonIsFailedForCalculate));
                }
                return total;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BEngineCalculator", "GetTotalCountInCalculating");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد افرادی که در محاسبات به خطا خورده اند را بر می گرداند
        /// </summary>
        /// <returns>تعداد</returns>
        public int GetErrorCountInCalculating()
        {
            try
            {
                int errorCount = 0;
                errorCount = gtsEngineWS.GTS_GETTotalErrorExecuting(BUser.CurrentUser.UserName);
                if(SessionHelper.GetSessionValue(SessionHelper.PersonIsFailedForCalculate)!=null)
                {
                    errorCount += Convert.ToInt32(SessionHelper.GetSessionValue(SessionHelper.PersonIsFailedForCalculate));
                }
                return errorCount;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BEngineCalculator", "GetErrorCountInCalculating");
                throw ex;
            }
        }

        /// <summary>
        /// پاک کردن تعداد در محاسبات
        /// </summary>
        public void ClearTotalCountInCalculating()
        {
            SessionHelper.ClearSessionValue(SessionHelper.BusinessTotalCalculateCount);
        }

        /// <summary>
        /// تعداد افراد باقی مانده برای محاسبه
        /// </summary>
        /// <returns>تعداد</returns>
        public int GetRemainCountInCalculating()
        {
            try
            {
                return gtsEngineWS.GTS_GETRemainExecuting(BUser.CurrentUser.UserName);
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BEngineCalculator", "GetRemainCountInCalculating");
                throw ex;
            }
        }

        /// <summary>
        /// بررسی دسترسی انجام محاسبات
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckCalculationsLoadAccess()
        {

        }
    }
}
