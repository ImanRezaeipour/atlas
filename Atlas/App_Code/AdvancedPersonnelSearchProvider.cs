using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GTS.Clock.Business;
using GTS.Clock.Business.Proxy;
using System.Web.Script.Serialization;
using GTS.Clock.Infrastructure;
using System.Globalization;

/// <summary>
/// Summary description for AdvancedPersonnelSearchProvider
/// </summary>
public class AdvancedPersonnelSearchProvider
{
    public PersonAdvanceSearchProxy CreateAdvancedPersonnelSearchProxy(string StrObjPersonnelAdvancedSearch)
	{
        PersonAdvanceSearchProxy personAdvanceSearchProxy = new PersonAdvanceSearchProxy();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        if (StrObjPersonnelAdvancedSearch != string.Empty)
        {
            Dictionary<string, object> ParamDic = (Dictionary<string, object>)JsSerializer.DeserializeObject(StrObjPersonnelAdvancedSearch);
            if (ParamDic.ContainsKey("IsDeleted") && ParamDic["IsDeleted"].ToString() == "true")
            {
                personAdvanceSearchProxy.PersonIsDeleted = bool.Parse(ParamDic["IsDeleted"].ToString());
            }
           // else
           // {
                if (ParamDic.ContainsKey("Active") && ParamDic["Active"].ToString() == string.Empty)
                    personAdvanceSearchProxy.PersonActivateState = null;
                else
                    personAdvanceSearchProxy.PersonActivateState = bool.Parse(ParamDic["Active"].ToString());
           // }           
            if (ParamDic.ContainsKey("FirstName") && ParamDic["FirstName"].ToString() != string.Empty)
                personAdvanceSearchProxy.FirstName = ParamDic["FirstName"].ToString();
            if (ParamDic.ContainsKey("LastName") && ParamDic["LastName"].ToString() != string.Empty)
                personAdvanceSearchProxy.LastName = ParamDic["LastName"].ToString();
            if (ParamDic.ContainsKey("NationalCode") && ParamDic["NationalCode"].ToString() != string.Empty)
                personAdvanceSearchProxy.MelliCode = ParamDic["NationalCode"].ToString();
            if (ParamDic.ContainsKey("PersonnelNumber") && ParamDic["PersonnelNumber"].ToString() != string.Empty)
                personAdvanceSearchProxy.PersonCode = ParamDic["PersonnelNumber"].ToString();
            if (ParamDic.ContainsKey("Sex") && int.Parse(ParamDic["Sex"].ToString(), CultureInfo.InvariantCulture) != -1)
                personAdvanceSearchProxy.Sex = (PersonSex)Enum.ToObject(typeof(PersonSex), int.Parse(ParamDic["Sex"].ToString(), CultureInfo.InvariantCulture));
            if (ParamDic.ContainsKey("FatherName") && ParamDic["FatherName"].ToString() != string.Empty)
                personAdvanceSearchProxy.FatherName = ParamDic["FatherName"].ToString();
            if (ParamDic.ContainsKey("MarriageState") && int.Parse(ParamDic["MarriageState"].ToString(), CultureInfo.InvariantCulture) != -1)
                personAdvanceSearchProxy.MaritalStatus = (MaritalStatus)Enum.ToObject(typeof(MaritalStatus), int.Parse(ParamDic["MarriageState"].ToString(), CultureInfo.InvariantCulture));
            if (ParamDic.ContainsKey("MilitaryState") && int.Parse(ParamDic["MilitaryState"].ToString(), CultureInfo.InvariantCulture) != -1)
                personAdvanceSearchProxy.Military = (MilitaryStatus)Enum.ToObject(typeof(MilitaryStatus), int.Parse(ParamDic["MilitaryState"].ToString(), CultureInfo.InvariantCulture));
            if (ParamDic.ContainsKey("Education") && ParamDic["Education"].ToString() != string.Empty)
                personAdvanceSearchProxy.Education = ParamDic["Education"].ToString();
            if (ParamDic.ContainsKey("BirthLocation") && ParamDic["BirthLocation"].ToString() != string.Empty)
                personAdvanceSearchProxy.BirthPlace = ParamDic["BirthLocation"].ToString();
            if (ParamDic.ContainsKey("CardNumber") && ParamDic["CardNumber"].ToString() != string.Empty)
                personAdvanceSearchProxy.CartNumber = ParamDic["CardNumber"].ToString();
            if (ParamDic.ContainsKey("EmployNumber") && ParamDic["EmployNumber"].ToString() != string.Empty)
                personAdvanceSearchProxy.EmployeeNumber = ParamDic["EmployNumber"].ToString();
            if (ParamDic.ContainsKey("DepartmentID") && ParamDic["DepartmentID"].ToString() != string.Empty)
                personAdvanceSearchProxy.DepartmentListId = CreateIdListFromSerializationStringId(ParamDic["DepartmentID"].ToString());
            if (ParamDic.ContainsKey("IsContainsSubDepartment"))
                personAdvanceSearchProxy.IncludeSubDepartments = bool.Parse(ParamDic["IsContainsSubDepartment"].ToString());
            if (ParamDic.ContainsKey("OrganizationPostID") && decimal.Parse(ParamDic["OrganizationPostID"].ToString(), CultureInfo.InvariantCulture) != 0)
                personAdvanceSearchProxy.OrganizationUnitId = decimal.Parse(ParamDic["OrganizationPostID"].ToString(), CultureInfo.InvariantCulture);
			if (ParamDic.ContainsKey("EmployTypeID") && ParamDic["EmployTypeID"].ToString() != string.Empty)
			personAdvanceSearchProxy.EmploymentTypeListId = CreateIdListFromSerializationStringId(ParamDic["EmployTypeID"].ToString());
            if (ParamDic.ContainsKey("EmployFromDate") && ParamDic["EmployFromDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.FromEmploymentDate = ParamDic["EmployFromDate"].ToString();
            if (ParamDic.ContainsKey("EmployToDate") && ParamDic["EmployToDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.ToEmploymentDate = ParamDic["EmployToDate"].ToString();
			if (ParamDic.ContainsKey("ControlStationID") && ParamDic["ControlStationID"].ToString() != string.Empty)
				personAdvanceSearchProxy.ControlStationListId = CreateIdListFromSerializationStringId(ParamDic["ControlStationID"].ToString());
            if (ParamDic.ContainsKey("FromBirthDate") && ParamDic["FromBirthDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.FromBirthDate = ParamDic["FromBirthDate"].ToString();
            if (ParamDic.ContainsKey("ToBirthDate") && ParamDic["ToBirthDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.ToBirthDate = ParamDic["ToBirthDate"].ToString();
            if (ParamDic.ContainsKey("WorkGroupID") && decimal.Parse(ParamDic["WorkGroupID"].ToString(), CultureInfo.InvariantCulture) != 0)
                personAdvanceSearchProxy.WorkGroupId = decimal.Parse(ParamDic["WorkGroupID"].ToString(), CultureInfo.InvariantCulture);
            if (ParamDic.ContainsKey("WorkGroupFromDate") && ParamDic["WorkGroupFromDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.WorkGroupFromDate = ParamDic["WorkGroupFromDate"].ToString();
            if (ParamDic.ContainsKey("RuleGroupID") && decimal.Parse(ParamDic["RuleGroupID"].ToString(), CultureInfo.InvariantCulture) != 0)
                personAdvanceSearchProxy.RuleGroupId = decimal.Parse(ParamDic["RuleGroupID"].ToString(), CultureInfo.InvariantCulture);
            if (ParamDic.ContainsKey("RuleGroupFromDate") && ParamDic["RuleGroupFromDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.RuleGroupFromDate = ParamDic["RuleGroupFromDate"].ToString();
            if (ParamDic.ContainsKey("RuleGroupToDate") && ParamDic["RuleGroupToDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.RuleGroupToDate = ParamDic["RuleGroupToDate"].ToString();
            if (ParamDic.ContainsKey("CalculationRangeID") && decimal.Parse(ParamDic["CalculationRangeID"].ToString(), CultureInfo.InvariantCulture) != 0)
                personAdvanceSearchProxy.CalculationDateRangeId = decimal.Parse(ParamDic["CalculationRangeID"].ToString(), CultureInfo.InvariantCulture);
            if (ParamDic.ContainsKey("CalculationRangeFromDate") && ParamDic["CalculationRangeFromDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.CalculationFromDate = ParamDic["CalculationRangeFromDate"].ToString();
            if (ParamDic.ContainsKey("GradeID") && decimal.Parse(ParamDic["GradeID"].ToString(), CultureInfo.InvariantCulture) != 0)
                personAdvanceSearchProxy.GradeId = decimal.Parse(ParamDic["GradeID"].ToString(), CultureInfo.InvariantCulture);
            if (ParamDic.ContainsKey("CostCenterID") && decimal.Parse(ParamDic["CostCenterID"].ToString(), CultureInfo.InvariantCulture) != 0)
                personAdvanceSearchProxy.CostCenterId = decimal.Parse(ParamDic["CostCenterID"].ToString(), CultureInfo.InvariantCulture);
            if (ParamDic.ContainsKey("IntegratedSearchTerm") && ParamDic["IntegratedSearchTerm"].ToString() != string.Empty)
                personAdvanceSearchProxy.IntegratedSearchTerm = ParamDic["IntegratedSearchTerm"].ToString();
            if (ParamDic.ContainsKey("UIValidationGroupID") && ParamDic["UIValidationGroupID"].ToString() != string.Empty)
                personAdvanceSearchProxy.UIValidationGroupListId = CreateIdListFromSerializationStringId(ParamDic["UIValidationGroupID"].ToString());
            if (ParamDic.ContainsKey("ContractID") && decimal.Parse(ParamDic["ContractID"].ToString(), CultureInfo.InvariantCulture) != 0)
                personAdvanceSearchProxy.ContractId = decimal.Parse(ParamDic["ContractID"].ToString(), CultureInfo.InvariantCulture);
            if (ParamDic.ContainsKey("ContractFromDate") && ParamDic["ContractFromDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.ContractFromDate = ParamDic["ContractFromDate"].ToString();
            if (ParamDic.ContainsKey("ContractToDate") && ParamDic["ContractToDate"].ToString() != string.Empty)
                personAdvanceSearchProxy.ContractToDate = ParamDic["ContractToDate"].ToString();

        }
        return personAdvanceSearchProxy;
	}


    public List<decimal> CreateIdListFromSerializationStringId(string serializationStringIDs)
    {
        List<decimal> idList = new List<decimal>();
        List<string> idStringList = serializationStringIDs.Replace("#", "").Split(',').ToList<string>();
        foreach (var item in idStringList)
        {
            decimal id=0;
            if (Decimal.TryParse(item, out id))
            {
                idList.Add(id);
            }
        }
        return idList;
    }


}