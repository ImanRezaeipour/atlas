using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model;
using System.Reflection;
using GTS.Clock.Business.Security;
using NHibernate;
using GTS.Clock.Business.Temp;
using GTS.Clock.Infrastructure;
using NHibernate.Criterion;

namespace GTS.Clock.Business.BaseInformation
{
    /// <summary>
    /// ایستگاه کنترل
    /// </summary>
    public class BControlStation : BaseBusiness<ControlStation>
    {
        IDataAccess accessPort = new BUser();
        public BControlStation() { }
        const string ExceptionSrc = "GTS.Clock.Business.BaseInformation.BControlStation";
        private EntityRepository<ControlStation> staionRepository = new EntityRepository<ControlStation>(false);
		private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
		private BTemp bTemp = new BTemp();
		int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);

        /// <summary>
        /// لیست ایستگاه های کنترل را بر می گرداند
        /// </summary>
        /// <returns>لیست ایستگاه کنترل</returns>
        public override IList<ControlStation> GetAll()
        {
			
			IList<decimal> accessableIDs = accessPort.GetAccessibleControlStations();
			
			
			IList<ControlStation> list = new List<ControlStation>();
			if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
			{
				list = staionRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ControlStation().ID), accessableIDs.ToArray(), CriteriaOperation.IN));
			}
			else
			{
				ControlStation controlStationAlias = null;
				GTS.Clock.Model.Temp.Temp tempAlias = null;
				string operationGUID = this.bTemp.InsertTempList(accessableIDs);
				list = NHSession.QueryOver(() => controlStationAlias)
												  .JoinAlias(() => controlStationAlias.TempList, () => tempAlias)
												  .Where(() => tempAlias.OperationGUID == operationGUID)

												  .List<ControlStation>();
				this.bTemp.DeleteTempList(operationGUID);
			}

			
			return list;
        }
        public IList<ControlStation> GetAll(string SearchItem)
        {
            ControlStation ControlStationAlias = null;
            IList<ControlStation> ControlStationList = NHSession.QueryOver<ControlStation>(() => ControlStationAlias)
                                                                 .Where(() => ControlStationAlias.Name.IsInsensitiveLike(SearchItem, MatchMode.Anywhere) ||
                                                                              ControlStationAlias.CustomCode.IsInsensitiveLike(SearchItem , MatchMode.Anywhere)
                                                                       )
                                                                 .List<ControlStation>();
            return ControlStationList;
        }
        /// <summary>
        /// «اعتبارسنجی
        /// «نام نباید خالی باشد
        /// «نام  تکراری نباشد
        /// کد تعریف شده نباید تکراری باشد
        /// </summary>
        /// <param name="station"></param>
        protected override void InsertValidate(ControlStation station)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(station.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.StationNameRequierd, "درج - نام نباید خالی باشد", ExceptionSrc));
            }
            else if (staionRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => station.Name), station.Name)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.StationNameRepeated, "درج - نام نباید تکراری باشد", ExceptionSrc));
            }

            if (!Utility.IsEmpty(station.CustomCode))
            {
                if (staionRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => station.CustomCode), station.CustomCode)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.StationCustomCodeRepeated, "درج - کد ایستگاه نباید تکراری باشد", ExceptionSrc));
                }
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// «اعتبارسنجی
        /// «نام نباید خالی باشد
        /// «نام نوع استخدام تکراری نباشد
        /// کد تعریف شده نباید تکراری باشد
        /// </summary>
        /// <param name="station"></param>
        protected override void UpdateValidate(ControlStation station)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(station.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.StationNameRequierd, "نام نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                if (staionRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => station.Name), station.Name),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => station.ID), station.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.StationNameRepeated, "نام نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (!Utility.IsEmpty(station.CustomCode))
            {
                if (staionRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => station.CustomCode), station.CustomCode),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => station.ID), station.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.StationCustomCodeRepeated, "بروزرسانی - کد ایستگاه نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="station"></param>
        protected override void DeleteValidate(ControlStation station)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            PersonRepository rep = new PersonRepository(false);

            if (NHibernateSessionManager.Instance.GetSession().QueryOver<GTS.Clock.Model.BaseInformation.Clock>().JoinQueryOver(clock => clock.Station).Where(ctrlStation => ctrlStation.ID == station.ID).RowCount() > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.StationUsedByMachine, "بدلیل استفاده در دستگاه نباید حذف شود", ExceptionSrc));
            }

            if (rep.CheckIsControlStationInUseByPerson(station))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.StationUsedByPerson, "بدلیل استفاده در پرسنل نباید حذف شود", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعمال دسترسی به کاربر جاری بعد از اعمال تغییرات در دیتابیس
        /// </summary>
        /// <param name="obj">ایستگاه کنترل</param>
        /// <param name="action">نوع عملیات</param>
        protected override void OnSaveChangesSuccess(ControlStation obj, UIActionType action)
        {
            if (action == UIActionType.ADD)
            {
                new BDataAccess().InsertDataAccess(Infrastructure.DataAccessLevelOperationType.Single, Infrastructure.DataAccessParts.ControlStation, obj.ID, BUser.CurrentUser.ID, null, "");
            }
        }

        /// <summary>
        /// کنترل دسترسی ایستگاه کنترل
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckControlStationsLoadAccess()
        { 
        }

        /// <summary>
        /// عملیات درج ایستگاه کنترل
        /// </summary>
        /// <param name="controlStation">ایستگاه کنترل</param>
        /// <param name="UAT">توع عملیات</param>
        /// <returns>کلید ایستگاه کنترل</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertControlStation(ControlStation controlStation, UIActionType UAT)
        {
            return base.SaveChanges(controlStation, UAT);
        }

        /// <summary>
        /// عملیات ویرایش ایستگاه کنترل 
        /// </summary>
        /// <param name="controlStation">ایستگاه کنترل</param>
        /// <param name="UAT">توع عملیات</param>
        /// <returns>کلید ایستگاه کنترل</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateControlStation(ControlStation controlStation, UIActionType UAT)
        {
            return base.SaveChanges(controlStation, UAT);
        }

        /// <summary>
        /// عملیات حذف ایستگاه کنترل  
        /// </summary>
        /// <param name="controlStation">ایستگاه کنترل</param>
        /// <param name="UAT">توع عملیات</param>
        /// <returns>کلید ایستگاه کنترل</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteControlStation(ControlStation controlStation, UIActionType UAT)
        {
            return base.SaveChanges(controlStation, UAT);
        }

    }
}