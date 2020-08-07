using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Model;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.UIValidation;
using GTS.Clock.Model.Contracts;

namespace GTS.Clock.Business
{
    public interface ISearchPerson
    {
        int GetPersonCount();
        int GetPersonCountByMonthlyExceptionShift(int year, int month , string searchvalue);

        int GetPersonInAdvanceSearchCount(PersonAdvanceSearchProxy proxy);
        int GetPersonInAdvanceSearchCount(PersonAdvanceSearchProxy proxy, PersonCategory searchInCategory);
        int GetPersonInAdvanceSearchCountByOperator(PersonAdvanceSearchProxy proxy);
        int GetPersonInQuickSearchCount(string searchValue);
        int GetPersonInQuickSearchCountByMonthlyExceptionShift(string searchValue, IList<DateTime> MonthDateList);
        int GetPersonInQuickSearchCount(string searchValue, PersonCategory searchInCategory);
        int GetPersonInQuickSearchCountBySubstitute(string searchValue, PersonCategory searchInCategory, decimal personId);
        int GetPersonInQuickSearchCountByMonthlyExceptionShift(string searchValue, PersonCategory searchInCategory, IList<DateTime> MonthDatesList);
        int GetPersonInQuickIntegratedSearchCount(string searchValue, string integratedSearchValue, PersonCategory searchInCategory);
        int GetPersonInQuickSearchCountByOperator(string searchValue);

        IList<Person> GetAllPerson(int pageIndex, int pageSize);

        IList<Person> GetPersonInAdvanceSearch(Business.Proxy.PersonAdvanceSearchProxy proxy);        
        IList<Person> GetPersonInAdvanceSearch(Business.Proxy.PersonAdvanceSearchProxy proxy, int pageIndex, int pageSize);
        IList<Person> GetPersonInAdvanceSearch(Business.Proxy.PersonAdvanceSearchProxy proxy, int pageIndex, int pageSize, PersonCategory searchInCategory);
        IList<Person> GetPersonInAdvanceSearchByOperator(Business.Proxy.PersonAdvanceSearchProxy proxy, int pageIndex, int pageSize, out int count);
        IList<Person> GetPersonInAdvanceSearchApplyCulture(Business.Proxy.PersonAdvanceSearchProxy proxy, int pageIndex, int pageSize);

        IList<Person> QuickSearch(string searchValue, PersonCategory searchInCategory);
        IList<Person> QuickSearchByPage(int pageIndex, int pageSize, string searchValue);
        IList<Person> QuickSearchByPage(int pageIndex, int pageSize, string searchValue, PersonCategory searchInCategory);
        IList<Person> QuickSearchByPageBySubstitute(int pageIndex, int pageSize, string searchValue, PersonCategory searchInCategory , int personId);
        IList<Person> QuickIntegratedSearchByPage(int pageIndex, int pageSize, string searchValue, string integratedSerachValue, PersonCategory searchInCategory);
        IList<Person> QuickSearchByPageByOperator(int pageIndex, int pageSize, string searchValue, out int count);
        IList<Person> QuickSearchByPageApplyCulture(int pageIndex, int pageSize, string searchValue);
        IList<Person> QuickSearchByPageApplyCultureMonthlyExceptionShift(int pageIndex, int pageSize, string searchValue, IList<DateTime> MonthDatesList);
        IList<Person> QuickSearchByPageByMonthlyExceptionShift(int pageIndex, int pageSize, string searchValue, PersonCategory searchInCategory, IList<DateTime> MonthDatesList);
        IList<Person> QuickSearchMethodBaseByPage(int pageIndex, int pageSize, string searchValue, PersonCategory searchInCategory);
       
        IList<Person> GetPersonByPersonIdList(IList<decimal> personIdList);
        Person RetrievelDeletedPersonnel(decimal personId, string barCode, string userPassword);

        #region AdvanceSearch Items
        Department GetDepartmentRoot();
        IList<Department> GetAllDepartments();
        IList<Department> GetDepartmentChild(decimal parentId,IList<Department> allDepartmens);
        IList<Department> GetDepartmentChild(decimal parentId);
        OrganizationUnit GetOrganizationRoot();
        IList<OrganizationUnit> GetOrganizationChild(decimal parentId);
        IList<ControlStation> GetAllControlStation();
        IList<WorkGroup> GetAllWorkGroup();
        IList<RuleCategory> GetAllRuleGroup();
        IList<CalculationRangeGroup> GetAllDateRanges();
        IList<EmploymentType> GetAllEmploymentTypes();
        IList<EmploymentType> GetAllEmploymentTypesWithoutAccessible();
        IList<UIValidationGroup> GetAllUIValidationGroup();
        IList<Grade> GetAllGrade();
        IList<Contract> GetAllContract();
        IList<CostCenter> GetAllCostCenter();
        #endregion
    }
}
