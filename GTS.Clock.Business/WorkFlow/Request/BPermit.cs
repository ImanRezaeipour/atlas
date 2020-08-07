﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Infrastructure.Validation.Configuration;

namespace GTS.Clock.Business.WorkFlow
{
    /// <summary>
    /// created at: 6/11/2012 12:59:21 PM
    /// by        : Farhad Salavati
    /// write your name here
    /// </summary>
    public class BPermit : BaseBusiness<Permit>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.WorkFlow.BPermit";
        private PermitRepository permitRep = new PermitRepository();
        UIValidationGroupingRepository uivalidationGroupingRepository = new UIValidationGroupingRepository();
     
        public void InsertPermit(Request request) 
        {
            Permit permit = new Permit();
            PermitPair permitPair = new PermitPair();
            string name = request.Precard.PrecardGroup.LookupKey;
            PrecardGroupsName groupName = (PrecardGroupsName)Enum.Parse(typeof(PrecardGroupsName), name);
            IList<Permit> list = permitRep.GetExistingPermit(request.Person.ID, request.Precard.ID, request.FromDate, request.ToDate);
            if (list.Count > 0 && (groupName == PrecardGroupsName.overwork || request.Precard.IsHourly))
            {
                permit = list.First();
                if (permit.Pairs == null)
                    permit.Pairs = new List<PermitPair>();
                permitPair.Permit = permit;
                permitPair.RequestID = request.ID;
                permitPair.From = request.FromTime;
                permitPair.To = request.ToTime;
                permitPair.Value = request.TimeDuration;
                permitPair.PreCardID = request.Precard.ID;
                permitPair.IsFilled = true;
                permit.Pairs.Add(permitPair);

                EntityRepository<PermitPair> rep = new EntityRepository<PermitPair>(false);
                this.SaveChanges(permit, UIActionType.EDIT);
            }
            else
            {
                if (groupName == PrecardGroupsName.overwork)
                {
                    permit.IsPairly = false;
                    if (request.TimeDuration == -1000)
                    {
                        permit.IsPairly = true;
                    }
                }
                else if (request.Precard.IsHourly)
                {
                    permit.IsPairly = true;
                }
                else if (request.Precard.IsDaily)
                {
                    permit.IsPairly = false;
                }

                permit.FromDate = request.FromDate;
                permit.ToDate = request.ToDate;
                permit.Pairs = new List<PermitPair>() { permitPair };
                permit.Person = request.Person;

                permitPair.Permit = permit;
                permitPair.RequestID = request.ID;
                permitPair.From = request.FromTime;
                permitPair.To = request.ToTime;
                permitPair.Value = request.TimeDuration;
                permitPair.PreCardID = request.Precard.ID;
                permitPair.IsFilled = true;

                this.SaveChanges(permit, UIActionType.ADD);
            }
        }

        public IList<Permit> GetExistingPermit(decimal personId, decimal precardId, DateTime fromDate, DateTime toDate) 
        {
            try 
            {
                IList<Permit> list = permitRep.GetExistingPermit(personId, precardId, fromDate, toDate);
                return list;
            }
            catch (Exception ex) 
            {
                LogException(ex, "BPermit", "GetExistingPermit");
                throw ex;
            }
        }
		public Permit GetExistingPermit(decimal requestId)
		{
			try
			{
				Permit permit = permitRep.GetExistingPermit(requestId);
				return permit;
			}
			catch (Exception ex)
			{
				LogException(ex, "BPermit", "GetExistingPermit");
				throw ex;
			}
		}
        public IList<Permit> GetExistingPermit(IList<decimal> personIds, IList<decimal> precardIds, DateTime date, SentryPermitsOrderBy orderby, int pageIndex, int pageSize)
        {
            try
            {
                IList<Permit> list = permitRep.GetExistingPermit(personIds, precardIds, date, orderby, pageIndex, pageSize);
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPermit", "GetExistingPermit");
                throw ex;
            }
        }

        public int GetExistingPermitCount(IList<decimal> personIds, IList<decimal> precardIds, DateTime date)
        {
            try
            {
                int count = permitRep.GetExistingPermitCount(personIds, precardIds, date);
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPermit", "GetExistingPermitCount");
                throw ex;
            }
        }
		public int GetExistingPermitCount(decimal requestId)
		{
			try
			{
				int count = permitRep.GetExistingPermitCount(requestId);
				return count;
			}
			catch (Exception ex)
			{
				LogException(ex, "BPermit", "GetExistingPermit");
				throw ex;
			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="personId">شخصی که مجوز متعلق به او میباشد</param>
        public void DeleteByRequestId(decimal requestId,decimal personId,DateTime permitDate)
        {
            try
            {
                permitRep.DeleteByRequestId(requestId);
                this.UpdateCFP(personId, permitDate);
            }
            catch (Exception ex) 
            {
                LogException(ex, "BPermit", "DeleteByRequestId");
                throw ex;
            }
        }

        #region BaseBusiness Implementation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void InsertValidate(Permit obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void UpdateValidate(Permit obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void DeleteValidate(Permit obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void Insert(Permit obj)
        {
            try
            {
                permitRep.WithoutTransactSave(obj);
            }
            catch (Exception ex)
            {
                LogException(ex, "GTS.Clock.Business-Nhibernate Action");
                throw ex;
            }
        }

        protected override void Update(Permit obj)
        {
            try
            {
                permitRep.WithoutTransactUpdate(obj);
            }
            catch (Exception ex)
            {
                LogException(ex, "GTS.Clock.Business-Nhibernate Action");
                throw ex;
            }
        }

        protected override void UIValidate(Permit obj, UIActionType action)
        {
            UIValidator.DoValidate(obj);
        }

        /// <summary>
        /// در حالات ایجاد و بروزرسانی و حذف، نشانه در صورت بزرگتر بودن به تاریخ اعمال مجوز انتقال می یابد.
        ///در صورت بسته بودن ، نشانه تغییر نمی یابد
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        protected override void UpdateCFP(Permit obj, UIActionType action)
        {
            try
            {
                base.UpdateCFP(obj.Person.ID, obj.FromDate,true);
                LogUserAction(String.Format("Update CFP , Permit ID={0}, Date:{1}", obj.ID, Utility.ToString(obj.FromDate)));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }

        }
        public  void UpdateCFPByPermitList(IList<Permit> PermitList)
        {
            try
            {

                Dictionary<decimal, DateTime> uivalidationGroupIdDic = new Dictionary<decimal, DateTime>();
                IList<CFP> cfpPersonList = new List<CFP>();
                IList<decimal> personIdList = PermitList.Select(p => p.Person.ID).Distinct().ToList<decimal>();
                IList<decimal> permitIdList = PermitList.Select(p => p.ID).Distinct().ToList<decimal>();
                if (PermitList.Count > 0)
                    cfpPersonList = base.GetCFPPersons(personIdList);
                IList<decimal> UiValidationGroupIdList = uivalidationGroupingRepository.GetUivalidationIdList(personIdList);
                IList<decimal> cfpPersonIdInsertList = new List<decimal>();
                IList<decimal> cfpPermitIdInsertList = new List<decimal>();
                foreach (decimal uiValidateionGrpId in UiValidationGroupIdList)
                {

                    if (!uivalidationGroupIdDic.ContainsKey(uiValidateionGrpId))
                    {
                        DateTime calculationLockDate = base.UIValidator.GetCalculationLockDateByGroup(uiValidateionGrpId,true);
                        uivalidationGroupIdDic.Add(uiValidateionGrpId, calculationLockDate);
                    }

                }
                 base.UpdateCfpByPermitList(permitIdList, uivalidationGroupIdDic);
                 cfpPersonIdInsertList = personIdList.Where(p => cfpPersonList !=null && !cfpPersonList.Select(c => c.PrsId).ToList().Contains(p)).Select(p => p).Distinct().ToList<decimal>();
                 cfpPermitIdInsertList = PermitList.Where(p => cfpPersonIdInsertList.Contains(p.Person.ID)).Select(p => p.ID).ToList();
                if (cfpPersonIdInsertList.Count > 0)
                    base.InsertCfpByPermitList(cfpPermitIdInsertList, uivalidationGroupIdDic);
            
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }

        }
        public bool CheckPermitPairExistanceByRequest(decimal requestID)
        {
            bool existance = false;
            int permitPairCount = 0;
            permitPairCount = NHibernateSessionManager.Instance.GetSession().QueryOver<PermitPair>()
                                                                            .Where(x => x.RequestID == requestID)
                                                                            .RowCount();
            if (permitPairCount > 0)
                existance = true;
            return existance;
        }

        #endregion
    }
}
