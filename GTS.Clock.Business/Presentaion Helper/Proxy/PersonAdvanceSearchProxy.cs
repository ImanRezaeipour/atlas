using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Business.Proxy
{
    /// <summary>
    /// جهت استفاده در واسط کاربر
    /// همه پروکسی هایی که واسط کاربر با آنها کار میکند در این فضای کاری هستند
    /// </summary>
    public class PersonAdvanceSearchProxy : PersonSearchProxy, ICloneable
    {
        public object Clone()
        {
            PersonAdvanceSearchProxy proxy = new PersonAdvanceSearchProxy();
            proxy.BirthPlace = this.BirthPlace;
            proxy.CalculationDateRangeId = this.CalculationDateRangeId;
            proxy.CalculationFromDate = this.CalculationFromDate;
            proxy.CartNumber = this.CartNumber;
            proxy.ControlStationId = this.ControlStationId;
            proxy.ControlStationListId = this.ControlStationListId;
            proxy.DepartmentId = this.DepartmentId;
            proxy.DepartmentListId = this.DepartmentListId;
            proxy.Education = this.Education;
            proxy.EmployeeNumber = this.EmployeeNumber;
            proxy.EmploymentType = this.EmploymentType;
            proxy.EmploymentTypeListId = this.EmploymentTypeListId;
            proxy.FatherName = this.FatherName;
            proxy.FirstName = this.FirstName;
            proxy.FromBirthDate = this.FromBirthDate;
            proxy.FromEmploymentDate = this.FromEmploymentDate;
            proxy.GradeId = this.GradeId;
            proxy.CostCenterId = this.CostCenterId;
            proxy.IncludeSubDepartments = this.IncludeSubDepartments;
            proxy.IntegratedSearchTerm = this.IntegratedSearchTerm;
            proxy.LastName = this.LastName;
            proxy.MaritalStatus = this.MaritalStatus;
            proxy.MelliCode = this.MelliCode;
            proxy.Military = this.Military;
            proxy.OrganizationUnitId = this.OrganizationUnitId;
            proxy.PersonActivateState = this.PersonActivateState;
            proxy.PersonCode = this.PersonCode;
            proxy.PersonId = this.PersonId;
            proxy.PersonIdList = this.PersonIdList;
            proxy.PersonIsDeleted = this.PersonIsDeleted;
            proxy.RuleGroupFromDate = this.RuleGroupFromDate;
            proxy.RuleGroupId = this.RuleGroupId;
            proxy.RuleGroupToDate = this.RuleGroupToDate;
            proxy.SearchInCategory = this.SearchInCategory;
            proxy.Sex = this.Sex;
            proxy.ToBirthDate = this.ToBirthDate;
            proxy.ToEmploymentDate = this.ToEmploymentDate;
            proxy.UiValidationGroupID = this.UiValidationGroupID;
            proxy.UIValidationGroupListId = this.UIValidationGroupListId;
            proxy.WorkGroupFromDate = this.WorkGroupFromDate;
            proxy.WorkGroupId = this.WorkGroupId;
            proxy.ContractFromDate = this.ContractFromDate;
            proxy.ContractId = this.ContractId;
            proxy.ContractToDate = this.ContractToDate;
            return proxy;
        }
    }
}
