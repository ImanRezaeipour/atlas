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
using GTS.Clock.Infrastructure;
using GTS.Clock.Model;
using GTS.Clock.Business.Proxy;


namespace GTS.Clock.Business.Rules
{
    public class BPersonParams : BaseBusiness<PersonParamValue>
    {
        IDataAccess accessPort = new BUser();
        const int dayMinutes = 1440;
        const string ExceptionSrc = "GTS.Clock.Business.Shifts.Business.Shift";
        private EntityRepository<PersonParamValue> paramValueRepository = new EntityRepository<PersonParamValue>(false);
        EntityRepository<PersonParamField> paramFieldRepository = new EntityRepository<PersonParamField>(false);

        public IList<PersonParamValue> GetAll(decimal personId, decimal paramFieldId)
        {
            try
            {
                IList<PersonParamValue> list = paramValueRepository.Find(x => x.Person.ID == personId && x.ParamField.ID == paramFieldId).ToList();
                foreach (PersonParamValue value in list)
                {
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        value.TheFromDate = Utility.ToPersianDate(value.FromDate);
                        value.TheToDate = Utility.ToPersianDate(value.ToDate);
                    }
                    else
                    {
                        value.TheFromDate = Utility.ToString(value.FromDate);
                        value.TheToDate = Utility.ToString(value.ToDate);
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
        public IList<PersonParamValue> GetAll(decimal paramFieldId,string searchTerm)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInQuickSearchCount(searchTerm);
                IList<Person> personList = searchTool.QuickSearchByPage(0, count, searchTerm);
                List<decimal> personIDList=personList.Select(r=>r.ID).ToList<decimal>();
                IList<PersonParamValue> list = paramValueRepository.Find(x => personIDList.Contains(x.Person.ID)  && x.ParamField.ID == paramFieldId).ToList();
                List<PersonParamValue> listParams = new List<PersonParamValue>();

                foreach (PersonParamValue item in list)
                {
                    bool result = true;
                    foreach (decimal personID in personIDList)
                    {
                        if (list.Count(p => p.Person.ID == personID && p.FromDate==item.FromDate && p.ToDate==item.ToDate && p.Value==item.Value)==0)
                            result = false;
                    }
                    if (result == true)
                    {
                        if (listParams.Count(c => c.FromDate == item.FromDate && c.ToDate == item.ToDate && c.Value == item.Value)==0)
                            listParams.Add(item);
                    }
                }
               
                foreach (PersonParamValue value in listParams)
                {
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        value.TheFromDate = Utility.ToPersianDate(value.FromDate);
                        value.TheToDate = Utility.ToPersianDate(value.ToDate);
                    }
                    else
                    {
                        value.TheFromDate = Utility.ToString(value.FromDate);
                        value.TheToDate = Utility.ToString(value.ToDate);
                    }
                }
                return listParams;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }
        public IList<PersonParamValue> GetAll(decimal paramFieldId, PersonAdvanceSearchProxy proxy)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
                IList<Person> personList = searchTool.GetPersonInAdvanceSearch(proxy, 0, count);
                List<decimal> personIDList = personList.Select(r => r.ID).ToList<decimal>();
                IList<PersonParamValue> list = paramValueRepository.Find(x => personIDList.Contains(x.Person.ID) && x.ParamField.ID == paramFieldId).ToList();
                List<PersonParamValue> listParams = new List<PersonParamValue>();

                foreach (PersonParamValue item in list)
                {
                    bool result = true;
                    foreach (decimal personID in personIDList)
                    {
                        if (list.Count(p => p.Person.ID == personID && p.FromDate == item.FromDate && p.ToDate == item.ToDate && p.Value == item.Value) == 0)
                            result = false;
                    }
                    if (result == true)
                    {
                        if (listParams.Count(c => c.FromDate == item.FromDate && c.ToDate == item.ToDate && c.Value == item.Value) == 0)
                            listParams.Add(item);
                    }
                }

                foreach (PersonParamValue value in listParams)
                {
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        value.TheFromDate = Utility.ToPersianDate(value.FromDate);
                        value.TheToDate = Utility.ToPersianDate(value.ToDate);
                    }
                    else
                    {
                        value.TheFromDate = Utility.ToString(value.FromDate);
                        value.TheToDate = Utility.ToString(value.ToDate);
                    }
                }
                return listParams;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }
        protected override void InsertValidate(PersonParamValue val)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (val.ParamField == null || val.ParamField.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ParamFieldIsEmpty, " فیلد نباید خالی باشد", ExceptionSrc));
            }
            if (val.Person == null || val.Person.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ParamPersonIsEmpty, " شخص نباید خالی باشد", ExceptionSrc));
            }
            if(val.FromDate==new DateTime() || val.ToDate==new DateTime())
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ParamFromDateToDateNotEmpty, " پارامترهای تاریخ نباید خالی باشد", ExceptionSrc));
            }
            if (val.FromDate > val.ToDate)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ParamFromDateGreaterThanToDate, " تاریخ ابتدا از انتها بزرگتر است", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void UpdateValidate(PersonParamValue val)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            if (val.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.paramNotSelected, " پارامتری جهت ویرایش انتخاب نشده است", ExceptionSrc));
            }
            else
            {
                if (val.ParamField == null || val.ParamField.ID == 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ParamFieldIsEmpty, " فیلد نباید خالی باشد", ExceptionSrc));
                }
                if (val.Person == null || val.Person.ID == 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ParamPersonIsEmpty, " شخص نباید خالی باشد", ExceptionSrc));
                }
                if (val.FromDate == new DateTime() || val.ToDate == new DateTime())
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ParamFromDateToDateNotEmpty, " پارامترهای تاریخ نباید خالی باشد", ExceptionSrc));
                }
                if (val.FromDate > val.ToDate)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ParamFromDateGreaterThanToDate, " تاریخ ابتدا از انتها بزرگتر است", ExceptionSrc));
                }
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void DeleteValidate(PersonParamValue obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void GetReadyBeforeSave(PersonParamValue value, UIActionType action)
        {
            if (!Utility.IsEmpty(value.TheToDate) && !Utility.IsEmpty(value.TheToDate))
            {
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    value.FromDate = Utility.ToMildiDate(value.TheFromDate);
                    value.ToDate = Utility.ToMildiDate(value.TheToDate);
                }
                else
                {
                    value.FromDate = Utility.ToMildiDateTime(value.TheFromDate);
                    value.ToDate = Utility.ToMildiDateTime(value.TheToDate);
                }
            }
            if (Utility.IsEmpty(value.Value))
            {
                value.Value = "0";
            }
        }

        protected override void UpdateCFP(PersonParamValue value, UIActionType action)
        {
            try
            {

                decimal personId = value.Person.ID;
                DateTime newCfpDate = value.FromDate;
                //CFP cfp = base.GetCFP(personId);
                //if (cfp.ID == 0 || cfp.Date > newCfpDate)
                //{
                //    DateTime calculationLockDate = base.UIValidator.GetCalculationLockDate(personId);

                //    //بسته بودن محاسبات 
                //    if (calculationLockDate > Utility.GTSMinStandardDateTime && calculationLockDate > newCfpDate)
                //    {
                //        newCfpDate = calculationLockDate.AddDays(1);
                //    }

                    base.UpdateCFP(personId, newCfpDate);

               // }
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPersonParamValue_onPersonnelInsert(PersonParamValue filedValue, UIActionType UAT)
        {
            return this.SaveChanges(filedValue, UAT);
        }

         [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckInsertPersonParamValue_onPersonnelUpdate()
        {

        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPersonParamValue_onPersonnelUpdate(PersonParamValue filedValue, UIActionType UAT)
        {
            return this.SaveChanges(filedValue, UAT);
        }

        public decimal InsertPersonsParamValue_onPersonnelUpdate(PersonParamValue filedValue, UIActionType UAT, PersonAdvanceSearchProxy proxy)
        {
            ISearchPerson searchTool = new BPerson();
            int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count);
            PersonParamValue personParamObj = new PersonParamValue();
            foreach (Person prs in list)
            {
                personParamObj = new PersonParamValue();
                personParamObj.ID = filedValue.ID;
                personParamObj.FromDate = filedValue.FromDate;
                personParamObj.ParamField = filedValue.ParamField;
                personParamObj.Person = prs;
                personParamObj.TheFromDate = filedValue.TheFromDate;
                personParamObj.TheToDate = filedValue.TheToDate;
                personParamObj.ToDate = filedValue.ToDate;
                personParamObj.Value = filedValue.Value;

                this.SaveChanges(personParamObj, UAT);
            }
            return list.Count;

        }
        
        public decimal InsertPersonsParamValue_onPersonnelUpdate(PersonParamValue filedValue, UIActionType UAT, string searchTerm)
        {
            ISearchPerson searchTool = new BPerson();
            int count = searchTool.GetPersonInQuickSearchCount(searchTerm);
            IList<Person> list = searchTool.QuickSearchByPage(0, count, searchTerm);
            PersonParamValue personParamObj = new PersonParamValue();
            foreach (Person prs in list)
            {
                personParamObj = new PersonParamValue();
                personParamObj.ID = filedValue.ID;
                personParamObj.FromDate = filedValue.FromDate;
                personParamObj.ParamField = filedValue.ParamField;
                personParamObj.Person = prs;
                personParamObj.TheFromDate = filedValue.TheFromDate;
                personParamObj.TheToDate = filedValue.TheToDate;
                personParamObj.ToDate = filedValue.ToDate;
                personParamObj.Value = filedValue.Value;
               
                this.SaveChanges(personParamObj, UAT);
            }

            return list.Count;
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdatePersonParamValue_onPersonnelInsert(PersonParamValue filedValue, UIActionType UAT)
        {
            return this.SaveChanges(filedValue, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckUpdatePersonsParamValue_onPersonnelUpdate()
        {

        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdatePersonParamValue_onPersonnelUpdate(PersonParamValue filedValue, UIActionType UAT)
        {
            return this.SaveChanges(filedValue, UAT);
        }
        public decimal UpdatePersonsParamValue_onPersonnelUpdate(PersonParamValue filedValue, UIActionType UAT, PersonAdvanceSearchProxy proxy)
        {
            ISearchPerson searchTool = new BPerson();
            int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count);
            PersonParamValue personParamOld = GetAll().SingleOrDefault(p => p.ID == filedValue.ID);

            string value = personParamOld.Value;
            DateTime fromDate = personParamOld.FromDate;
            DateTime toDate = personParamOld.ToDate;

            foreach (Person prs in list)
            {
                PersonParamValue personParamObj = new PersonParamValue();
                personParamObj = GetAll(prs.ID, filedValue.ParamField.ID).SingleOrDefault(f => f.FromDate == fromDate && f.ToDate == toDate && f.Value == value);
                personParamObj.FromDate = filedValue.FromDate;
                personParamObj.ToDate = filedValue.ToDate;
                personParamObj.Value = filedValue.Value;

                this.SaveChanges(personParamObj, UAT);
            }
            return list.Count;

        }

        public decimal UpdatePersonsParamValue_onPersonnelUpdate(PersonParamValue filedValue, UIActionType UAT, string searchTerm)
        {
            ISearchPerson searchTool = new BPerson();
            int count = searchTool.GetPersonInQuickSearchCount(searchTerm);
            IList<Person> list = searchTool.QuickSearchByPage(0, count, searchTerm);
            PersonParamValue personParamOld = GetAll().SingleOrDefault(p => p.ID == filedValue.ID);
            
            string value = personParamOld.Value;
            DateTime fromDate = personParamOld.FromDate;
            DateTime toDate = personParamOld.ToDate;

            foreach (Person prs in list)
            {
                PersonParamValue personParamObj = new PersonParamValue();
                personParamObj = GetAll(prs.ID, filedValue.ParamField.ID).SingleOrDefault(f=>f.FromDate==fromDate && f.ToDate==toDate && f.Value==value);
                personParamObj.FromDate = filedValue.FromDate;
                personParamObj.ToDate = filedValue.ToDate;
                personParamObj.Value = filedValue.Value;

                this.SaveChanges(personParamObj, UAT);
            }

            return list.Count;
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeletePersonParamValue_onPersonnelInsert(PersonParamValue filedValue, UIActionType UAT)
        {
            return this.SaveChanges(filedValue, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeletePersonParamValue_onPersonnelUpdate(PersonParamValue filedValue, UIActionType UAT)
        {
            return this.SaveChanges(filedValue, UAT);
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckDeletePersonsParamValue_onPersonnelUpdate()
        {

        }
        
        public decimal DeletePersonsParamValue_onPersonnelUpdate(PersonParamValue filedValue, UIActionType UAT,string searchTerm)
        {
            ISearchPerson searchTool = new BPerson();
            int count = searchTool.GetPersonInQuickSearchCount(searchTerm);
            IList<Person> list = searchTool.QuickSearchByPage(0, count, searchTerm);
            PersonParamValue personParamOld = GetAll().SingleOrDefault(p => p.ID == filedValue.ID);

            string value = personParamOld.Value;
            DateTime fromDate = personParamOld.FromDate;
            DateTime toDate = personParamOld.ToDate;

            foreach (Person prs in list)
            {
                PersonParamValue personParamObj = new PersonParamValue();
                personParamObj = GetAll().SingleOrDefault(f => f.Person.ID==prs.ID &&  f.FromDate == fromDate && f.ToDate == toDate && f.Value == value);
                

                this.SaveChanges(personParamObj, UAT);
            }

            return list.Count;
        }
        public decimal DeletePersonsParamValue_onPersonnelUpdate(PersonParamValue filedValue, UIActionType UAT, PersonAdvanceSearchProxy proxy)
        {
            ISearchPerson searchTool = new BPerson();
            int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count);
            PersonParamValue personParamOld = GetAll().SingleOrDefault(p => p.ID == filedValue.ID);

            string value = personParamOld.Value;
            DateTime fromDate = personParamOld.FromDate;
            DateTime toDate = personParamOld.ToDate;

            foreach (Person prs in list)
            {
                PersonParamValue personParamObj = new PersonParamValue();
                personParamObj = GetAll().SingleOrDefault(f => f.Person.ID == prs.ID && f.FromDate == fromDate && f.ToDate == toDate && f.Value == value);


                this.SaveChanges(personParamObj, UAT);
            }

            return list.Count;
        }
        

    }
}