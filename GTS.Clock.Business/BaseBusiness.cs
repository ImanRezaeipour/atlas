using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.Security;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Model;
using GTS.Clock.Business.AppSettings;
using System.Reflection;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.Validation.Configuration;

namespace GTS.Clock.Business
{
    public abstract class BaseBusiness<T> : GTS.Business.BaseGTSBusiness<T>
        where T : GTS.Clock.Model.IEntity, new()
    {
        //protected User currentUser=null;
        //public User CurrentUser
        //{
        //    get
        //    {
        //        if (currentUser == null) 
        //        {
        //            currentUser = BUser.CurrentUser;
        //        }
        //        return currentUser;
        //    }
        //}
        CFPRepository cfpRepository = new CFPRepository();
        UIValidationGroupingRepository uivalidationGroupingRepository = new UIValidationGroupingRepository();
        public BaseBusiness()
            : base(BUser.CurrentUser.UserName, BUser.CurrentUser.ID, new EntityRepository<T>(false), IsBusinessLogEnable)
        {
            try
            {

            }
            catch (AthorizeServiceException ex)
            {
                LogException(ex, typeof(T).Name, "Cunstructor");
                throw ex;
            }
        }

        #region base override
        protected override void BaseGetReadyBeforeSave(T obj, GTS.Business.UIActionType action)
        {
            this.GetReadyBeforeSave(obj, ConvertAction(action));
        }
        protected override void BaseOnSaveChangesSuccess(T obj, GTS.Business.UIActionType action)
        {
            this.OnSaveChangesSuccess(obj, ConvertAction(action));
        }
        protected override void BaseUpdateCFP(T obj, GTS.Business.UIActionType action)
        {
            this.UpdateCFP(obj, ConvertAction(action));
        }

        protected override void BaseUIValidate(T obj, GTS.Business.UIActionType action)
        {
            this.UIValidate(obj, ConvertAction(action));
        }

        
        #endregion
        public virtual decimal SaveChanges(T obj, UIActionType action)
        {
            try
            {
                return base.BaseSaveChanges(obj, ConvertAction(action));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// اگر بروزرسانی و درج و حذف باموفقیت انجام گیرد این تابع صدا زده میشود
        /// </summary>
        /// <param name="action"></param>
        protected virtual void OnSaveChangesSuccess(T obj, UIActionType action)
        {
            try
            {
                base.BaseOnSaveChangesSuccess(obj, ConvertAction(action));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// اگر شی نیاز به مقداردهی قبل از ذخیره دارد این تابع پیاده سازی دوباره شود
        /// </summary>
        /// <param name="obj"></param>
        protected virtual void GetReadyBeforeSave(T obj, UIActionType action)
        {
            try
            {
                base.BaseGetReadyBeforeSave(obj, ConvertAction(action));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// بروزرسانی نشانه تغییرات
        /// </summary>
        /// <param name="obj"></param>
        protected virtual void UpdateCFP(T obj, UIActionType action)
        {
            try
            {
                base.BaseUpdateCFP(obj, ConvertAction(action));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        protected virtual void UIValidate(T obj, UIActionType action)
        {
            try
            {
                base.BaseUIValidate(obj, ConvertAction(action));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }
     

        #region Log

        protected static bool IsBusinessErrorLogEnable
        {
            get
            {
                GTS.Clock.Model.AppSetting.ApplicationSettings appSettings = BApplicationSettings.CurrentApplicationSettings;
                return appSettings.BusinessErrorLogEnable;

            }
        }

        protected static bool IsBusinessLogEnable
        {
            get
            {
                GTS.Clock.Model.AppSetting.ApplicationSettings appSettings = BApplicationSettings.CurrentApplicationSettings;
                return appSettings.BusinessLogEnable;
            }
        }

        protected static bool IsDataCollectorLogEnable
        {
            get
            {
                GTS.Clock.Model.AppSetting.ApplicationSettings appSettings = BApplicationSettings.CurrentApplicationSettings;
                return appSettings.DataCollectorLogEnable;
            }

        }

        private class EntityBusiness : BaseBusiness<T>
        {

            protected override void InsertValidate(T obj)
            {
                throw new NotImplementedException();
            }

            protected override void UpdateValidate(T obj)
            {
                throw new NotImplementedException();
            }
            protected override void DeleteValidate(T obj)
            {
                throw new NotImplementedException(); 
            }
            protected override void InsertCustomValidate(T obj)
            {
                throw new NotImplementedException();
            }
             protected override void UpdateCustomValidate(T obj)
            {
                throw new NotImplementedException();
            }
             protected override void DeleteCustomValidate(T obj)
             {
                 throw new NotImplementedException();
             } 
        }

        public static void LogException(UIValidationExceptions ex, string className, string methodName)
        {
            new EntityBusiness().LogException(ex, className, methodName, BUser.CurrentUser.UserName, IsBusinessLogEnable);
        }

        public static void LogException(BaseException ex, string className, string methodName)
        {
            new EntityBusiness().LogException(ex, className, methodName, BUser.CurrentUser.UserName, IsBusinessLogEnable);
        }

        public static void LogException(Exception ex, string className, string methodName)
        {
            new EntityBusiness().LogException(ex, className, methodName, BUser.CurrentUser.UserName, IsBusinessLogEnable);
        }

        public static void LogException(Exception ex)
        {
            new EntityBusiness().LogException(ex, BUser.CurrentUser.UserName, IsBusinessLogEnable);
        }

        public static void LogException(Exception ex, string execptionSrcDescription)
        {
            new EntityBusiness().LogException(ex, execptionSrcDescription, BUser.CurrentUser.UserName, IsBusinessLogEnable);
        }

        public static void LogUserAction(string action)
        {
            new EntityBusiness().LogUserAction(action, BUser.CurrentUser.UserName, IsBusinessLogEnable);
        }

        public static void LogUserAction(T obj, string action)
        {
            new EntityBusiness().LogUserAction(obj, action, BUser.CurrentUser.UserName, IsBusinessLogEnable);
        }

        public static void LogDataCollectorException(string personBarcode, DateTime trafficDateTime, DateTime recieveDateTime, string deviceID, string status, Exception exception)
        {
          
            new EntityBusiness().LogDataCollectorException(personBarcode, trafficDateTime, recieveDateTime, deviceID, status, exception, IsDataCollectorLogEnable);
         
        }

        public static void LogDataCollectorInfo(string personBarcode, DateTime trafficDateTime, DateTime recieveDateTime, string deviceID, string status, Exception exception)
        {
            new EntityBusiness().LogDataCollectorInfo(personBarcode, trafficDateTime, recieveDateTime, deviceID, status, exception, IsDataCollectorLogEnable);
        }


        #endregion

        private GTS.Business.UIActionType ConvertAction(UIActionType type)
        {
            switch (type)
            {
                case UIActionType.ADD:
                    return GTS.Business.UIActionType.ADD;
                case UIActionType.DELETE:
                    return GTS.Business.UIActionType.DELETE;
                case UIActionType.EDIT:
                    return GTS.Business.UIActionType.EDIT;
            }
            return GTS.Business.UIActionType.EDIT;
        }

        private UIActionType ConvertAction(GTS.Business.UIActionType type)
        {
            switch (type)
            {
                case GTS.Business.UIActionType.ADD:
                    return UIActionType.ADD;
                case GTS.Business.UIActionType.DELETE:
                    return UIActionType.DELETE;
                case GTS.Business.UIActionType.EDIT:
                    return UIActionType.EDIT;
            }
            return UIActionType.EDIT;
        }

        #region CFP

        /// <summary>
        /// بروزرسانی نشانه تغییرات
        /// </summary>
        /// <param name="obj"></param>
        private void InsertCFP(decimal personId, DateTime date)
        {
            CFP cfp = new CFP();
            cfp.PrsId = personId;
            cfp.Date = date;
            cfp.CalculationIsValid = false;
            cfpRepository.Save(cfp);
        }

        /// <summary>
        /// بروزرسانی نشانه محاسبات
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="newCfpDate"></param>
        protected void UpdateCFP(decimal personId, DateTime newCfpDate, bool isForce = false)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    IList<decimal> prsIds = new List<decimal>();
                    prsIds.Add(personId);
                    IList<decimal> UiValidationGroupIdList = uivalidationGroupingRepository.GetUivalidationIdList(prsIds);
                    DateTime calculationLockDate = Utility.GTSMinStandardDateTime;
                    if(UiValidationGroupIdList.Count>0)
                    {
                        calculationLockDate = base.UIValidator.GetCalculationLockDateByGroup(UiValidationGroupIdList.FirstOrDefault());
                    }

                    string className = Utility.CallerCalassName;



                    CFP cfp = cfpRepository.GetByPersonID(personId);
                    DateTime finalCfpDate = Utility.GTSMinStandardDateTime;
                    if (cfp != null && cfp.ID > 0)
                    {
                        if (UiValidationGroupIdList.Count > 0)
                        {
                            if (cfp.Date <= newCfpDate || cfp.Date < calculationLockDate)
                            {
                                finalCfpDate = cfp.Date;
                            }
                            else if(calculationLockDate<=cfp.Date && calculationLockDate>=newCfpDate)
                            {
                                if (!isForce)
                                    finalCfpDate = calculationLockDate.AddDays(1);
                                else
                                    finalCfpDate = newCfpDate;
                            }
                            else if(newCfpDate> calculationLockDate && newCfpDate <cfp.Date)
                            {
                                finalCfpDate = newCfpDate;
                            }

                        }
                        else
                        {
                            finalCfpDate = newCfpDate;
                        }
                        //if (UiValidationGroupIdList.Count > 0)
                        //{
                        //    if(calculationLockDate >= newCfpDate || calculationLockDate >= cfp.Date)
                        //    {
                        //        finalCfpDate = calculationLockDate.AddDays(1);
                        //    }
                        //    else if (cfp.Date > calculationLockDate && cfp.Date <= newCfpDate)
                        //    {
                        //        finalCfpDate = cfp.Date;
                        //    }
                        //    else if (newCfpDate > calculationLockDate && newCfpDate <= cfp.Date)
                        //    {
                        //        finalCfpDate = newCfpDate;
                        //    }
                            
                        //}
                        //else
                        //{
                        //    finalCfpDate = newCfpDate;
                        //}
                        
                        LogUserAction(String.Format("CFP {0} Prs:{1} cls:{2} ", Utility.ToPersianDate(finalCfpDate), personId, className));
                        cfp.Date = finalCfpDate.Date;
                        cfp.CalculationIsValid = false;
                        cfpRepository.WithoutTransactUpdate(cfp);
                        
                    }
                    else
                    {
                        cfpRepository.InsertCFP(personId, finalCfpDate);
                    }

                    //LogUserAction(String.Format("Before InvalidateTrafficCalculation CFP {0} Prs:{1} cls:{2} ", Utility.ToPersianDate(cfpDate), personId, className));
                    //PermitRepository permitRep = new PermitRepository();
                    //permitRep.InvalidateTrafficCalculation(personId, cfpDate);
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    //LogUserAction(String.Format("After InvalidateTrafficCalculation CFP {0} Prs:{1} cls:{2} ", Utility.ToPersianDate(cfpDate), personId, className));
                }
                catch (Exception ex) 
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex);
                    throw ex;
                }
            }
        }

        /// <summary>
        /// بروزرسانی نشانه محاسبات
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="cfpDate"></param>
        protected void UpdateCFP(CFP cfp, decimal personId, DateTime cfpDate, bool invalidateTraffic)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    //string className = Utility.CallerCalassName;

                    //LogUserAction(String.Format("CFP {0} Prs:{1} cls:{2} ", Utility.ToPersianDate(cfpDate), personId, className));

                    if (cfp != null && cfp.ID > 0)
                    {
                        if (cfp.Date.Date >= cfpDate.Date)
                        {
                            cfp.Date = cfpDate.Date;
                            cfp.CalculationIsValid = false;
                            cfpRepository.WithoutTransactUpdate(cfp);
                        }
                    }
                    else
                    {
                        cfpRepository.InsertCFP(personId, cfpDate.Date);
                    }
                    //PermitRepository permitRep = new PermitRepository();
                    //permitRep.InvalidateTrafficCalculation(personId, cfpDate);
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// بروزرسانی نشانه محاسبات
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="cfpDate"></param>
        protected void UpdateCFP(IList<CFP> cfpList, bool invalidateTraffic)
        {
            LogUserAction(String.Format("CFP Update Count:{0} Started", cfpList.Count));

            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    foreach (CFP cfp in cfpList)
                    {
                        if (cfp != null && cfp.ID > 0)
                        {
                            cfp.CalculationIsValid = false;
                            cfpRepository.WithoutTransactUpdate(cfp);
                        }
                        else
                        {
                            cfpRepository.InsertCFP(cfp.PrsId, cfp.Date);
                        }
                        //PermitRepository permitRep = new PermitRepository();
                        //permitRep.InvalidateTrafficCalculation(cfp.PrsId, cfp.Date);
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    LogUserAction(String.Format("CFP Update Count:{0} Finished", cfpList.Count));
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    throw ex;
                }
            }
        }

        protected CFP GetCFP(decimal personId) 
        {
             CFP cfp = cfpRepository.GetByPersonID(personId);
             if (cfp != null && cfp.ID > 0)
             {
                 return cfp;
             }
             return new CFP();
        }

        protected IList<CFP> GetCFPPersons(IList<decimal> personIdList)
        {
            IList<CFP> cfpList = cfpRepository.GetByPersonIDList(personIdList);
            //if (cfp != null && cfp.ID > 0)
            //{
            //    return cfp;
            //}
            return cfpList;
        }

        protected void UpdateCfpByWrokGroup(decimal workGroupId, Dictionary<decimal, DateTime> uivalidationGroupIdDic)
        {
            try
            {
                cfpRepository.UpdateCfpByWrokGroup( workGroupId , uivalidationGroupIdDic);
                //LogUserAction(String.Format("CFP Update Count:{0} Finished", personIDList.Count));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
         
        }
        protected void InsertCfpByWrokGroup(IList<decimal> personIDList, decimal workGroupId, Dictionary<decimal, DateTime> uivalidationGroupIdDic)
        {
            try
            {


                cfpRepository.InsertCfpByWrokGroup(personIDList, workGroupId ,uivalidationGroupIdDic);
                LogUserAction(String.Format("CFP Insert Count:{0} Finished", personIDList.Count));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }
        protected void UpdateCfpByRuleCategory(decimal RuleCateId, Dictionary<decimal, DateTime> uivalidationGroupIdDic)
        {
            try
            {
                cfpRepository.UpdateCfpByRuleCategory(RuleCateId, uivalidationGroupIdDic);
                //LogUserAction(String.Format("CFP Update Count:{0} Finished", personIDList.Count));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }

        }
        protected void InsertCfpByRuleCategory(IList<decimal> personIDList, decimal ruleCateId, Dictionary<decimal, DateTime> uivalidationGroupIdDic)
        {
            try
            {


                cfpRepository.InsertCfpByRuleCategory(personIDList, ruleCateId, uivalidationGroupIdDic);
                LogUserAction(String.Format("CFP Insert Count:{0} Finished", personIDList.Count));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }
        protected void UpdateCfpByPermitList(IList<decimal> permitIdList, Dictionary<decimal, DateTime> uivalidationGroupIdDic)
        {
            try
            {
                cfpRepository.UpdateCfpByPermitList(permitIdList, uivalidationGroupIdDic);
                //LogUserAction(String.Format("CFP Update Count:{0} Finished", personIDList.Count));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }

        }
        protected void InsertCfpByPermitList(IList<decimal> permitIdList, Dictionary<decimal, DateTime> uivalidationGroupIdDic)
        {
            try
            {


                cfpRepository.InsertCfpByPermitList(permitIdList, uivalidationGroupIdDic);
                LogUserAction(String.Format("CFP Insert Count:{0} Finished", permitIdList.Count));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }
        protected void UpdateCfpByPersonList(IList<decimal> personIdList, DateTime newCfpDate, Dictionary<decimal, DateTime> uivalidationGroupIdDic)
        {
            try
            {
                cfpRepository.UpdateCfpByPersonList(personIdList,newCfpDate, uivalidationGroupIdDic);
                //LogUserAction(String.Format("CFP Update Count:{0} Finished", personIDList.Count));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }

        }
        protected void InsertCfpByPersonList(IList<decimal> personIdList, DateTime newCfpDate, Dictionary<decimal, DateTime> uivalidationGroupIdDic)
        {
            try
            {


                cfpRepository.InsertCfpByPersonList(personIdList,newCfpDate, uivalidationGroupIdDic);
                LogUserAction(String.Format("CFP Insert Count:{0} Finished", personIdList.Count));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }
        protected void UpdateCfpByPersonList(IList<decimal> personIdList, DateTime newCfpDate)
        {
            try
            {
                cfpRepository.UpdateCfpByPersonList(personIdList, newCfpDate);
                //LogUserAction(String.Format("CFP Update Count:{0} Finished", personIDList.Count));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }

        }
        protected void InsertCfpByPersonList(IList<decimal> personIdList, DateTime newCfpDate)
        {
            try
            {


                cfpRepository.InsertCfpByPersonList(personIdList, newCfpDate);
                LogUserAction(String.Format("CFP Insert Count:{0} Finished", personIdList.Count));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }
        protected void UpdateCfpByDateRangeGroup(decimal dateRangeGroupId, Dictionary<decimal, DateTime> uivalidationGroupIdDic)
        {
            try
            {
                cfpRepository.UpdateCfpByDateRangeGroup(dateRangeGroupId, uivalidationGroupIdDic);
                //LogUserAction(String.Format("CFP Update Count:{0} Finished", personIDList.Count));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }

        }
        protected void InsertCfpByDateRangeGroup(IList<decimal> personIDList, decimal dateRangeGroupId, Dictionary<decimal, DateTime> uivalidationGroupIdDic)
        {
            try
            {


                cfpRepository.InsertCfpByDateRangeGroup(personIDList, dateRangeGroupId, uivalidationGroupIdDic);
                LogUserAction(String.Format("CFP Insert Count:{0} Finished", personIDList.Count));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }
        #endregion

   
    }
}
