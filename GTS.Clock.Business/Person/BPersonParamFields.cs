using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using System.Reflection;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.Rules;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Business.Rules
{
    public class BPersonParamFields : BaseBusiness<PersonParamField>
    {
        const string ExceptionSrc = "GTS.Clock.Business.Rules.BPersonParamFields";
        private EntityRepository<PersonParamField> paramFieldRepository = new EntityRepository<PersonParamField>(false);

        public override IList<PersonParamField> GetAll()
        {
            try
            {
                IList<PersonParamField> list = paramFieldRepository.Find(x => x.Active && x.SubSystemId == SubSystemIdentifier.TimeAtendance).ToList();
                foreach (PersonParamField field in list)
                {
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        field.Title = field.FnTitle;
                    }
                    else
                    {
                        field.Title = field.EnTitle;
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        protected override void GetReadyBeforeSave(PersonParamField obj, UIActionType action)
        {
            if (action != UIActionType.DELETE)
            {
                obj.SubSystemId = SubSystemIdentifier.TimeAtendance;
                switch (BLanguage.CurrentSystemLanguage)
                {
                    case LanguagesName.Parsi:
                        obj.FnTitle = obj.Title;
                        break;
                    case LanguagesName.English:
                        obj.EnTitle = obj.Title;
                        break;
                    default:
                        break;
                }
            }
        }

        protected override void InsertValidate(PersonParamField field)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(field.Key))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ParamFieldKeyIsEmpty, " کلید خالی است", ExceptionSrc));
            }
            if (Utility.IsEmpty(field.Title))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ParamFieldNameIsEmpty, " نام خالی است", ExceptionSrc));
            }
            if (exception.Count == 0)
            {
                if (paramFieldRepository.Find(x => x.Key == field.Key && x.Active).Count() > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ParamFieldKeyRepeated, " کلید تکراری است", ExceptionSrc));
                }
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void UpdateValidate(PersonParamField field)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(field.Key))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ParamFieldKeyIsEmpty, " کلید خالی است", ExceptionSrc));
            }
            if (Utility.IsEmpty(field.Title))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ParamFieldNameIsEmpty, " نام خالی است", ExceptionSrc));
            }
            if (exception.Count == 0)
            {
                if (paramFieldRepository.Find(x => x.ID != field.ID && x.Key == field.Key && x.Active).Count() > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ParamFieldKeyRepeated, " کلید تکراری است", ExceptionSrc));
                }
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void DeleteValidate(PersonParamField obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            PersonParamValue personParamValueAlias=null;
            IList<PersonParamValue> personParamValueList = NHibernateSessionManager.Instance.GetSession()
                                                          .QueryOver<PersonParamValue>(() => personParamValueAlias)
                                                          .Where(() => personParamValueAlias.ParamField.ID == obj.ID)
                                                          .List<PersonParamValue>();
            
            if(personParamValueList.Count>0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ParamFieldKeyIsUsed, " پارامتر برای پرسنل دیگر استفاده شده است", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override bool Delete(PersonParamField field)
        {
            field.Active = false;
            paramFieldRepository.Update(field);
            return true;
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckPersonParamFieldsLoadAccess_onPersonnelInsert()
        { 
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckPersonParamFieldsLoadAccess_onPersonnelUpdate()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPersonParamField_onPersonnelInsert(PersonParamField field, UIActionType UAT)
        {
            return this.SaveChanges(field, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPersonParamField_onPersonnelUpdate(PersonParamField field, UIActionType UAT)
        {
            return this.SaveChanges(field, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdatePersonParamField_onPersonnelInsert(PersonParamField field, UIActionType UAT)
        {
            return this.SaveChanges(field, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdatePersonParamField_onPersonnelUpdate(PersonParamField field, UIActionType UAT)
        {
            return this.SaveChanges(field, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeletePersonParamField_onPersonnelInsert(PersonParamField field, UIActionType UAT)
        {
            return this.SaveChanges(field, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeletePersonParamField_onPersonnelUpdate(PersonParamField field, UIActionType UAT)
        {
            return this.SaveChanges(field, UAT);
        }


    }
}