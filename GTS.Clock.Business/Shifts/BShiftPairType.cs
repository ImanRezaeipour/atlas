using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.Shifts
{
    public class BShiftPairType : BaseBusiness<ShiftPairType>
    {
        const string ExceptionSrc = "GTS.Clock.Business.Shifts.BShiftPairType";
        NHibernate.ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        private EntityRepository<ShiftPairType> shiftPairTypeRepository
            = new EntityRepository<ShiftPairType>(false);             

        /// <summary>
        /// اعتبارسنجی
        /// نام نباید خالی باشد
        /// نام نوبت کاری تکراری نباشد
        /// کد تعریف شده نباید تکراری باشد
        /// </summary>
        /// <param name="shiftPairType"></param>
        protected override void InsertValidate(ShiftPairType shiftPairType)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(shiftPairType.Title))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ShiftPairTypeNameIsEmpty, "نام نباید خالی باشد", ExceptionSrc));
            }
            if (Utility.IsEmpty(shiftPairType.CustomCode))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ShiftPairTypeCustomCodeIsEmpty, "کد نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                if (shiftPairTypeRepository.Find (x=>x.CustomCode .Equals( shiftPairType.CustomCode)).Count() > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ShiftPairTypeCustomCodeRepeated, "کد نباید تکراری باشد", ExceptionSrc));
                }
            }

           

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبارسنجی
        /// نام نباید خالی باشد
        /// نام نوبت کاری تکراری نباشد
        /// کد تعریف شده باید تکراری باشد
        /// </summary>
        /// <param name="shiftPairType"></param>
        protected override void UpdateValidate(ShiftPairType shiftPairType)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(shiftPairType.Title))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ShiftPairTypeNameIsEmpty, "نام نباید خالی باشد", ExceptionSrc));
            }
            if (Utility.IsEmpty(shiftPairType.CustomCode))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ShiftPairTypeCustomCodeIsEmpty, "کد نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                if (shiftPairTypeRepository.Find(x => x.CustomCode.Equals(shiftPairType.CustomCode)&&x.ID!=shiftPairType.ID).Count() > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ShiftPairTypeCustomCodeRepeated, "کد نباید تکراری باشد", ExceptionSrc));
                }
            }
            if (shiftPairType.Active == false)
            {
                EntityRepository<ShiftPair> entityRep = new EntityRepository<ShiftPair>(false);
                int count = entityRep.Find(x => x.ShiftPairType.ID == shiftPairType.ID).Count();
                if(count > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ShiftPairTypeUsedByShiftPair, "این نوع شیفت به بازه شیفت انتساب داده شده است", ExceptionSrc));
                }
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        public override IList<ShiftPairType> GetAll()
        {
            try
            {
                IList<ShiftPairType> list = this.NHSession.QueryOver<ShiftPairType>()
                                                .Where(x => x.Active)
                                                .List<ShiftPairType>();
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionSrc);
                throw ex;
            }
        }
        protected override void GetReadyBeforeSave(ShiftPairType obj, UIActionType action)
        {
            if (action == UIActionType.EDIT)
            {
                if (obj.ShiftPairList == null)
                {
                    EntityRepository<ShiftPair> entityRep = new EntityRepository<ShiftPair>(false);
                    IList<ShiftPair> shiftPairList = entityRep.Find(x => x.ShiftPairType.ID == obj.ID).ToList();
                    obj.ShiftPairList = shiftPairList;
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void DeleteValidate(ShiftPairType shiftPairType)
        {
            EntityRepository<ShiftPair> entityRep = new EntityRepository<ShiftPair>(false);
            int count = entityRep.Find(x => x.ShiftPairType.ID == shiftPairType.ID).Count();

            if (count > 0)
            {
                NHibernateSessionManager.Instance.RollbackTransactionOn();
                UIValidationExceptions exception = new UIValidationExceptions();
                exception.Add(ExceptionResourceKeys.ShiftPairTypeUsedByShiftPair, "این نوع شیفت به بازه شیفت انتساب داده شده است", ExceptionSrc);
                throw exception;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckShiftPairTypesLoadAccess()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertShiftPairType(ShiftPairType shiftPairType, UIActionType UAT)
        {
            return base.SaveChanges(shiftPairType, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateShiftPairType(ShiftPairType shiftPairType, UIActionType UAT)
        {
            return base.SaveChanges(shiftPairType, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteShiftPairType(ShiftPairType shiftPairType, UIActionType UAT)
        {
            return base.SaveChanges(shiftPairType, UAT);
        }

    }
}
