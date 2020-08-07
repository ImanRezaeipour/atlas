using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model;
using NHibernate;
using NHibernate.Transform;

namespace GTS.Clock.Infrastructure.Repository
{
    public class CFPRepository : RepositoryBase<CFP>
    {
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        TempRepository tempRepository = new TempRepository(false);
        public CFPRepository(bool disconnectly) 
            :base(disconnectly)
        {

        }
        public CFPRepository()
            : base(false)
        {

        }

        public override string TableName
        {
            get { return "TA_Calculation_Flag_Persons"; }
        }

        public CFP GetByPersonID(decimal personID) 
        {
            string HQLCommand = @"select cfp from CFP as cfp
                                  where cfp.PrsId=:personId";
            IList<CFP> list = base.NHibernateSession.CreateQuery(HQLCommand)
                            .SetParameter("personId", personID)
                            .List<CFP>();
            return list.FirstOrDefault();
               
        }
        public IList<CFP> GetByPersonIDList(IList<decimal> personIDList)
        {
            IList<CFP> cfpList = null; 
            string operationGUID = string.Empty;
            string SqlCommand = string.Empty;
            if (personIDList.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
            {
                SqlCommand = @"select cfp.* 
                                from TA_Calculation_Flag_Persons cfp
                                where CFP_PrsId in (:PersonId)";
                cfpList = base.NHibernateSession.CreateSQLQuery(SqlCommand)
                         .AddEntity(typeof(CFP))
                         .SetParameterList("PersonId", personIDList.ToArray())
                         .List<CFP>();
            }
            else
            {
                operationGUID = this.tempRepository.InsertTempList(personIDList);
                SqlCommand = @"select cfp.*  
                               from TA_Calculation_Flag_Persons cfp
                               Inner Join Ta_Temp 
                               on temp_ObjectID = CFP_PrsId
                               where temp_OperationGUID = :OperationGuid";
                cfpList = base.NHibernateSession.CreateSQLQuery(SqlCommand)
                        .AddEntity(typeof(CFP))
                        .SetParameter("OperationGuid", operationGUID)
                        .List<CFP>();
                this.tempRepository.DeleteTempList(operationGUID);
            }
            return cfpList;

        }
        public void InsertAndUpdateCfpByPersons(Dictionary<decimal, DateTime> uivalidationGroupIdDic, DateTime cfpDate)
        {
            string operationGUID = string.Empty;
            string SqlCommand = string.Empty;
            foreach (decimal item in uivalidationGroupIdDic.Keys)
            {

                SqlCommand = @" update cfp set cfp.CFP_Date = case  when cfp.CFP_Date <= :newCfpDate  or :LockDate > cfp.CFP_Date then cfp.CFP_Date when cfp.CFP_Date>= :LockDate and :LockDate >= :newCfpDate  then dateadd(day,1,:LockDate) when :newCfpDate > :LockDate and :newCfpDate < cfp.CFP_Date then :newCfpDate  end, cfp.CFP_CalculationIsValid = 0
                                from TA_Calculation_Flag_Persons cfp
                                inner join ta_person
                                on prs_id = cfp.CFP_PrsId and prs_Active = 1 and prs_IsDeleted = 0
                                inner join TA_PersonTASpec
                                on cfp.CFP_PrsId = TA_PersonTASpec.prsTA_ID
                                where TA_PersonTASpec.prsTA_UIValidationGroupID= :UiValidationGroupID ";
                base.NHibernateSession.CreateSQLQuery(SqlCommand)
                    .SetParameter("UiValidationGroupID", item)
                    .SetParameter("newCfpDate", cfpDate)
                    .SetParameter("LockDate", uivalidationGroupIdDic[item])
                    .ExecuteUpdate();


                SqlCommand = @"insert into TA_Calculation_Flag_Persons (CFP_PrsId,CFP_Date,CFP_MidNightCalculate,CFP_CalculationIsValid)
                                select personID,newCfpDate,cfpMidNightCalculate = 0, cfpCalculationIsValid = 0 from 
								(select Prs_ID as personID ,IIF ( :newCfpDate >= :LockDate, :newCfpDate, dateadd(day,1,:LockDate)) as newCfpDate   
								from TA_Person 
								inner join TA_PersonTASpec
                                on TA_Person.Prs_ID = TA_PersonTASpec.prsTA_ID
                                where  TA_PersonTASpec.prsTA_UIValidationGroupID= :UiValidationGroupID 
							    and TA_Person.Prs_Active=1 and TA_Person.prs_IsDeleted=0
							    and TA_Person.Prs_ID not in (select CFP_PrsId from TA_Calculation_Flag_Persons)) prs";
                base.NHibernateSession.CreateSQLQuery(SqlCommand)
                    .SetParameter("UiValidationGroupID", item)
                    .SetParameter("newCfpDate", cfpDate)
                    .SetParameter("LockDate", uivalidationGroupIdDic[item])
                    .ExecuteUpdate();     
            }
        }
        public void UpdateCfpByWrokGroup(decimal workGroupId, Dictionary<decimal,DateTime> uivalidationGroupIdDic)
        {
            string operationGUID = string.Empty;
            string SqlCommand = string.Empty;
            foreach (decimal item in uivalidationGroupIdDic.Keys)
            {

                SqlCommand = @" update cfp set cfp.CFP_Date = case  when cfp.CFP_Date <= newCfpDate  or :LockDate > cfp.CFP_Date then cfp.CFP_Date when cfp.CFP_Date>= :LockDate and :LockDate >= newCfpDate  then dateadd(day,1,:LockDate) when newCfpDate > :LockDate and newCfpDate < cfp.CFP_Date then newCfpDate  end, cfp.CFP_CalculationIsValid = 0
                               from TA_Calculation_Flag_Persons cfp
                               inner join (select AsgWorkGroup_PersonId,min(AsgWorkGroup_FromDate) newCfpDate from TA_AssignWorkGroup
                               where AsgWorkGroup_WorkGroupId = :WorkGroupID
                               group by AsgWorkGroup_PersonId) as assign
			                   on cfp.CFP_PrsId = assign.AsgWorkGroup_PersonId
                               inner join TA_PersonTASpec
                               on cfp.CFP_PrsId = TA_PersonTASpec.prsTA_ID
                               where  TA_PersonTASpec.prsTA_UIValidationGroupID= :UiValidationGroupID ";
                  
                base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                   .SetParameter("WorkGroupID", workGroupId)
                                   .SetParameter("UiValidationGroupID", item)
                                   .SetParameter("LockDate", uivalidationGroupIdDic[item])
                                   .ExecuteUpdate();
                
            }
            

        }
        public void InsertCfpByWrokGroup(IList<decimal> personIDList, decimal workGroupId, Dictionary<decimal, DateTime> uivalidationGroupIdDic)
        {
            string operationGUID = string.Empty;
            string SqlCommand = string.Empty;
            foreach (decimal item in uivalidationGroupIdDic.Keys)
            {
                if (personIDList.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {
                    SqlCommand = @" insert into TA_Calculation_Flag_Persons (CFP_PrsId,CFP_Date,CFP_MidNightCalculate,CFP_CalculationIsValid)
                                select personID,newCfpDate,cfpMidNightCalculate = 0, cfpCalculationIsValid = 0 from 
								(select AsgWorkGroup_PersonId as personID,  IIF ( min(AsgWorkGroup_FromDate) >= :LockDate, min(AsgWorkGroup_FromDate), dateadd(day,1,:LockDate) ) as newCfpDate 
								 from TA_AssignWorkGroup
                                 where AsgWorkGroup_WorkGroupId = :WorkGroupID
                                 group by AsgWorkGroup_PersonId) as assign
								 inner join TA_PersonTASpec
                                 on assign.personID = TA_PersonTASpec.prsTA_ID
                                 where assign.personID in (:PersonID) and  TA_PersonTASpec.prsTA_UIValidationGroupID = :UiValidationGroupID";
                    base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                   .SetParameter("WorkGroupID", workGroupId)
                                   .SetParameter("UiValidationGroupID", item)
                                   .SetParameter("LockDate", uivalidationGroupIdDic[item])
                                   .SetParameterList("PersonID", personIDList.ToArray())
                                   .ExecuteUpdate();
                }
                else
                {
                    operationGUID = this.tempRepository.InsertTempList(personIDList);
                    SqlCommand = @" insert into TA_Calculation_Flag_Persons (CFP_PrsId,CFP_Date,CFP_MidNightCalculate,CFP_CalculationIsValid)
                                    select personID,newCfpDate,cfpMidNightCalculate = 0, cfpCalculationIsValid = 0 from 
								    (select AsgWorkGroup_PersonId as personID,  IIF ( min(AsgWorkGroup_FromDate) >= :LockDate, min(AsgWorkGroup_FromDate), dateadd(day,1,:LockDate) ) as newCfpDate   
								    from TA_AssignWorkGroup
                                    where AsgWorkGroup_WorkGroupId = :WorkGroupID
                                    group by AsgWorkGroup_PersonId) as assign
								    inner join TA_PersonTASpec
                                    on assign.personID = TA_PersonTASpec.prsTA_ID
                                    inner join ta_temp 
                                    on ta_temp.temp_ObjectID = TA_PersonTASpec.prsTA_ID
                                    and  TA_PersonTASpec.prsTA_UIValidationGroupID = :UiValidationGroupID 
                                    and temp_OperationGUID = :OperationGuid";
                    base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                   .SetParameter("WorkGroupID", workGroupId)
                                   .SetParameter("UiValidationGroupID", item)
                                   .SetParameter("OperationGuid", operationGUID)
                                   .SetParameter("LockDate", uivalidationGroupIdDic[item])
                                   .ExecuteUpdate();
                    this.tempRepository.DeleteTempList(operationGUID);
                }
            }


        }
        public void UpdateCfpByRuleCategory(decimal ruleCateID, Dictionary<decimal, DateTime> uivalidationGroupIdDic)
        {
            string operationGUID = string.Empty;
            string SqlCommand = string.Empty;
            foreach (decimal item in uivalidationGroupIdDic.Keys)
            {

                SqlCommand = @" update cfp set cfp.CFP_Date = case  when cfp.CFP_Date <= newCfpDate  or :LockDate > cfp.CFP_Date then cfp.CFP_Date when cfp.CFP_Date>= :LockDate and :LockDate >= newCfpDate  then dateadd(day,1,:LockDate) when newCfpDate > :LockDate and newCfpDate < cfp.CFP_Date then newCfpDate  end, cfp.CFP_CalculationIsValid = 0
                               from TA_Calculation_Flag_Persons cfp
                               inner join (select PrsRulCatAsg_PersonId,min(PrsRulCatAsg_FromDate) newCfpDate from TA_PersonRuleCategoryAssignment
                               where PrsRulCatAsg_RuleCategoryId = :RuleCateID
                               group by PrsRulCatAsg_PersonId) as assign
			                   on cfp.CFP_PrsId = assign.PrsRulCatAsg_PersonId
                               inner join TA_PersonTASpec
                               on cfp.CFP_PrsId = TA_PersonTASpec.prsTA_ID
                               where  TA_PersonTASpec.prsTA_UIValidationGroupID= :UiValidationGroupID ";

                base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                   .SetParameter("RuleCateID", ruleCateID)
                                   .SetParameter("UiValidationGroupID", item)
                                   .SetParameter("LockDate", uivalidationGroupIdDic[item])
                                   .ExecuteUpdate();

            }


        }

        public void InsertCfpByRuleCategory(IList<decimal> personIDList, decimal ruleCategoryId, Dictionary<decimal, DateTime> uivalidationGroupIdDic)
        {
            string operationGUID = string.Empty;
            string SqlCommand = string.Empty;
            foreach (decimal item in uivalidationGroupIdDic.Keys)
            {
                if (personIDList.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {
                    SqlCommand = @" insert into TA_Calculation_Flag_Persons (CFP_PrsId,CFP_Date,CFP_MidNightCalculate,CFP_CalculationIsValid)
                                select personID,newCfpDate,cfpMidNightCalculate = 0, cfpCalculationIsValid = 0 from 
								(select PrsRulCatAsg_PersonId as personID,  IIF ( min(PrsRulCatAsg_FromDate) >= :LockDate, min(PrsRulCatAsg_FromDate), dateadd(day,1,:LockDate) ) as newCfpDate 
								 from TA_PersonRuleCategoryAssignment
                                 where PrsRulCatAsg_RuleCategoryId = :RuleCategoryId
                                 group by PrsRulCatAsg_PersonId) as assign
								 inner join TA_PersonTASpec
                                 on assign.personID = TA_PersonTASpec.prsTA_ID
                                 where assign.personID in (:PersonID) and  TA_PersonTASpec.prsTA_UIValidationGroupID = :UiValidationGroupID";
                    base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                   .SetParameter("RuleCategoryId", ruleCategoryId)
                                   .SetParameter("UiValidationGroupID", item)
                                   .SetParameter("LockDate", uivalidationGroupIdDic[item])
                                   .SetParameterList("PersonID", personIDList.ToArray())
                                   .ExecuteUpdate();
                }
                else
                {
                    operationGUID = this.tempRepository.InsertTempList(personIDList);
                    SqlCommand = @" insert into TA_Calculation_Flag_Persons (CFP_PrsId,CFP_Date,CFP_MidNightCalculate,CFP_CalculationIsValid)
                                    select personID,newCfpDate,cfpMidNightCalculate = 0, cfpCalculationIsValid = 0 from 
								    (select PrsRulCatAsg_PersonId as personID,  IIF ( min(PrsRulCatAsg_FromDate) >= :LockDate, min(PrsRulCatAsg_FromDate), dateadd(day,1,:LockDate) ) as newCfpDate   
								    from TA_PersonRuleCategoryAssignment
                                    where PrsRulCatAsg_RuleCategoryId = :RuleCategoryId
                                    group by PrsRulCatAsg_PersonId) as assign
								    inner join TA_PersonTASpec
                                    on assign.personID = TA_PersonTASpec.prsTA_ID
                                    inner join ta_temp 
                                    on ta_temp.temp_ObjectID = TA_PersonTASpec.prsTA_ID
                                    and  TA_PersonTASpec.prsTA_UIValidationGroupID = :UiValidationGroupID 
                                    and temp_OperationGUID = :OperationGuid";
                    base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                   .SetParameter("RuleCategoryId", ruleCategoryId)
                                   .SetParameter("UiValidationGroupID", item)
                                   .SetParameter("OperationGuid", operationGUID)
                                   .SetParameter("LockDate", uivalidationGroupIdDic[item])
                                   .ExecuteUpdate();
                    this.tempRepository.DeleteTempList(operationGUID);
                }
            }


        }

        public void UpdateCfpByPermitList(IList<decimal> permitIdList, Dictionary<decimal, DateTime> uivalidationGroupIdDic)
        {
            string operationGUID = string.Empty;
            string SqlCommand = string.Empty;
            foreach (decimal item in uivalidationGroupIdDic.Keys)
            {
                if (permitIdList.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {
                    SqlCommand = @" update cfp set cfp.CFP_Date = case  when cfp.CFP_Date <= newCfpDate  or :LockDate > cfp.CFP_Date then cfp.CFP_Date when cfp.CFP_Date>= :LockDate and :LockDate >= newCfpDate  then dateadd(day,1,:LockDate) when newCfpDate > :LockDate and newCfpDate < cfp.CFP_Date then newCfpDate  end, cfp.CFP_CalculationIsValid = 0
                               from TA_Calculation_Flag_Persons cfp
                               inner join (select Permit_PersonId,min(Permit_FromDate) as newCfpDate from TA_Permit where Permit_ID in (:PermitIdList)
                                           group by Permit_PersonId) as permit
			                   on cfp.CFP_PrsId = permit.Permit_PersonId
                               inner join TA_PersonTASpec
                               on cfp.CFP_PrsId = TA_PersonTASpec.prsTA_ID
                               where  TA_PersonTASpec.prsTA_UIValidationGroupID= :UiValidationGroupID ";

                    base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                       .SetParameterList("PermitIdList", permitIdList.ToArray())
                                       .SetParameter("UiValidationGroupID", item)
                                       .SetParameter("LockDate", uivalidationGroupIdDic[item])
                                       .ExecuteUpdate();
                }
                else
                {
                    operationGUID = this.tempRepository.InsertTempList(permitIdList);
                    SqlCommand = @" update cfp set cfp.CFP_Date = case  when cfp.CFP_Date <= newCfpDate  or :LockDate > cfp.CFP_Date then cfp.CFP_Date when cfp.CFP_Date>= :LockDate and :LockDate >= newCfpDate  then dateadd(day,1,:LockDate) when newCfpDate > :LockDate and newCfpDate < cfp.CFP_Date then newCfpDate  end, cfp.CFP_CalculationIsValid = 0
                               from TA_Calculation_Flag_Persons cfp
                               inner join (select Permit_PersonId,min(Permit_FromDate) as newCfpDate from TA_Permit 
                                           inner join ta_temp on Permit_ID = temp_ObjectID  where temp_OperationGUID =:OperationGuid
                                           group by Permit_PersonId) as permit
			                   on cfp.CFP_PrsId = permit.Permit_PersonId
                               inner join TA_PersonTASpec
                               on cfp.CFP_PrsId = TA_PersonTASpec.prsTA_ID
                               where  TA_PersonTASpec.prsTA_UIValidationGroupID= :UiValidationGroupID ";

                    base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                       .SetParameter("UiValidationGroupID", item)
                                       .SetParameter("LockDate", uivalidationGroupIdDic[item])
                                       .SetParameter("OperationGuid", operationGUID)
                                       .ExecuteUpdate();
                    this.tempRepository.DeleteTempList(operationGUID);
                }

            }


        }
        public void InsertCfpByPermitList(IList<decimal> permitIdList, Dictionary<decimal, DateTime> uivalidationGroupIdDic)
        {
            string operationGUID = string.Empty;
            string SqlCommand = string.Empty;
            foreach (decimal item in uivalidationGroupIdDic.Keys)
            {
                if (permitIdList.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {
                    SqlCommand = @" insert into TA_Calculation_Flag_Persons (CFP_PrsId,CFP_Date,CFP_MidNightCalculate,CFP_CalculationIsValid)
                                select personID,newCfpDate,cfpMidNightCalculate = 0, cfpCalculationIsValid = 0 from 
								(select Permit_PersonId as personID,  IIF ( min(Permit_FromDate) >= :LockDate, min(Permit_FromDate), dateadd(day,1,:LockDate) ) as newCfpDate 
								 from TA_Permit where Permit_ID in (:PermitIdList)
                                 group by Permit_PersonId) as permit
								 inner join TA_PersonTASpec
                                 on permit.personID = TA_PersonTASpec.prsTA_ID
                                 where  TA_PersonTASpec.prsTA_UIValidationGroupID = :UiValidationGroupID";
                    base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                   .SetParameter("UiValidationGroupID", item)
                                   .SetParameter("LockDate", uivalidationGroupIdDic[item])
                                   .SetParameterList("PermitIdList", permitIdList.ToArray())
                                   .ExecuteUpdate();
                }
                else
                {
                    operationGUID = this.tempRepository.InsertTempList(permitIdList);
                    SqlCommand = @" insert into TA_Calculation_Flag_Persons (CFP_PrsId,CFP_Date,CFP_MidNightCalculate,CFP_CalculationIsValid)
                                    select personID,newCfpDate,cfpMidNightCalculate = 0, cfpCalculationIsValid = 0 from 
								    (select Permit_PersonId as personID,  IIF ( min(Permit_FromDate) >= :LockDate, min(Permit_FromDate), dateadd(day,1,:LockDate)) as newCfpDate 
								     from TA_Permit inner join ta_temp on Permit_ID = temp_ObjectID
                                     where temp_OperationGUID =:OperationGuid
                                     group by Permit_PersonId) as permit
								    inner join TA_PersonTASpec
                                    on permit.personID = TA_PersonTASpec.prsTA_ID
                                    and  TA_PersonTASpec.prsTA_UIValidationGroupID = :UiValidationGroupID ";
                    base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                   .SetParameter("UiValidationGroupID", item)
                                   .SetParameter("OperationGuid", operationGUID)
                                   .SetParameter("LockDate", uivalidationGroupIdDic[item])
                                   .ExecuteUpdate();
                    this.tempRepository.DeleteTempList(operationGUID);
                }
            }


        }
        public void UpdateCfpByPersonList(IList<decimal> personIdList, DateTime newCfpDate, Dictionary<decimal, DateTime> uivalidationGroupIdDic)
        {
            string operationGUID = string.Empty;
            string SqlCommand = string.Empty;
            foreach (decimal item in uivalidationGroupIdDic.Keys)
            {
                if (personIdList.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {
                    SqlCommand = @" update cfp set cfp.CFP_Date = case  when cfp.CFP_Date <= :newCfpDate  or :LockDate > cfp.CFP_Date then cfp.CFP_Date when cfp.CFP_Date>= :LockDate and :LockDate >= :newCfpDate  then dateadd(day,1,:LockDate) when :newCfpDate > :LockDate and :newCfpDate < cfp.CFP_Date then :newCfpDate  end, cfp.CFP_CalculationIsValid = 0
                               from TA_Calculation_Flag_Persons cfp
                               inner join TA_PersonTASpec
                               on cfp.CFP_PrsId = TA_PersonTASpec.prsTA_ID
                               where cfp.CFP_PrsId in (:PersonIdList) and TA_PersonTASpec.prsTA_UIValidationGroupID= :UiValidationGroupID ";

                    base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                       .SetParameterList("PersonIdList", personIdList.ToArray())
                                       .SetParameter("UiValidationGroupID", item)
                                       .SetParameter("LockDate", uivalidationGroupIdDic[item])
                                       .SetParameter("newCfpDate", newCfpDate)
                                       .ExecuteUpdate();
                }
                else
                {
                    operationGUID = this.tempRepository.InsertTempList(personIdList);
                    SqlCommand = @" update cfp set cfp.CFP_Date = case  when cfp.CFP_Date <= :newCfpDate  or :LockDate > cfp.CFP_Date then cfp.CFP_Date when cfp.CFP_Date>= :LockDate and :LockDate >= :newCfpDate  then dateadd(day,1,:LockDate) when :newCfpDate > :LockDate and :newCfpDate < cfp.CFP_Date then :newCfpDate  end, cfp.CFP_CalculationIsValid = 0
                               from TA_Calculation_Flag_Persons cfp
                               inner join TA_PersonTASpec
                               on cfp.CFP_PrsId = TA_PersonTASpec.prsTA_ID
                               inner join ta_temp
                               on cfp.CFP_PrsId = temp_ObjectID
                               where temp_OperationGUID =:OperationGuid and  TA_PersonTASpec.prsTA_UIValidationGroupID= :UiValidationGroupID ";

                    base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                       .SetParameter("UiValidationGroupID", item)
                                       .SetParameter("LockDate", uivalidationGroupIdDic[item])
                                       .SetParameter("OperationGuid", operationGUID)
                                       .SetParameter("newCfpDate", newCfpDate)
                                       .ExecuteUpdate();
                    this.tempRepository.DeleteTempList(operationGUID);
                }

            }


        }
        public void InsertCfpByPersonList(IList<decimal> personIdList, DateTime newCfpDate, Dictionary<decimal, DateTime> uivalidationGroupIdDic)
        {
            string operationGUID = string.Empty;
            string SqlCommand = string.Empty;
            foreach (decimal item in uivalidationGroupIdDic.Keys)
            {
                if (personIdList.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {
                    SqlCommand = @" insert into TA_Calculation_Flag_Persons (CFP_PrsId,CFP_Date,CFP_MidNightCalculate,CFP_CalculationIsValid)
                                select personID,newCfpDate,cfpMidNightCalculate = 0, cfpCalculationIsValid = 0 from 
								(select prsTA_ID as personID , IIF ( :NewCfpDate >= :LockDate, :NewCfpDate, dateadd(day,1,:LockDate)) as newCfpDate from  TA_PersonTASpec
                                 where prsTA_ID in (:PersonIdList) and TA_PersonTASpec.prsTA_UIValidationGroupID = :UiValidationGroupID) person";
                    base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                   .SetParameterList("PersonIdList", personIdList.ToArray())
                                   .SetParameter("UiValidationGroupID", item)
                                   .SetParameter("LockDate", uivalidationGroupIdDic[item])
                                   .SetParameter("NewCfpDate", newCfpDate)
                                   .ExecuteUpdate();
                }
                else
                {
                    operationGUID = this.tempRepository.InsertTempList(personIdList);
                    SqlCommand = @" insert into TA_Calculation_Flag_Persons (CFP_PrsId,CFP_Date,CFP_MidNightCalculate,CFP_CalculationIsValid)
                                    select personID,newCfpDate,cfpMidNightCalculate = 0, cfpCalculationIsValid = 0 from 
								    (select prsTA_ID as personID ,IIF ( :NewCfpDate >= :LockDate, :NewCfpDate, dateadd(day,1,:LockDate)) as newCfpDate from  TA_PersonTASpec
                                     inner join ta_temp
                                     on prsTA_ID = temp_ObjectID
                                     where temp_OperationGUID =:OperationGuid and  TA_PersonTASpec.prsTA_UIValidationGroupID = :UiValidationGroupID) pesron ";
                    base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                   .SetParameter("UiValidationGroupID", item)
                                   .SetParameter("OperationGuid", operationGUID)
                                   .SetParameter("LockDate", uivalidationGroupIdDic[item])
                                   .SetParameter("NewCfpDate", newCfpDate)
                                   .ExecuteUpdate();
                    this.tempRepository.DeleteTempList(operationGUID);
                }
            }


        }
        public void UpdateCfpByDateRangeGroup(decimal dateRangeGroupID, Dictionary<decimal, DateTime> uivalidationGroupIdDic)
        {
            string operationGUID = string.Empty;
            string SqlCommand = string.Empty;
            foreach (decimal item in uivalidationGroupIdDic.Keys)
            {

                SqlCommand = @" update cfp set cfp.CFP_Date = case  when cfp.CFP_Date <= newCfpDate  or :LockDate > cfp.CFP_Date then cfp.CFP_Date when cfp.CFP_Date>= :LockDate and :LockDate >= newCfpDate  then dateadd(day,1,:LockDate) when newCfpDate > :LockDate and newCfpDate < cfp.CFP_Date then newCfpDate  end , cfp.CFP_CalculationIsValid = 0
                               from TA_Calculation_Flag_Persons cfp
                               inner join (select PrsRangeAsg_PersonId,min(PrsRangeAsg_FromDate) newCfpDate from TA_PersonRangeAssignment
                               where PrsRangeAsg_CalcRangeGrpId = :DateRangeGroupID
                               group by PrsRangeAsg_PersonId) as assign
			                   on cfp.CFP_PrsId = assign.PrsRangeAsg_PersonId
                               inner join TA_PersonTASpec
                               on cfp.CFP_PrsId = TA_PersonTASpec.prsTA_ID
                               where  TA_PersonTASpec.prsTA_UIValidationGroupID= :UiValidationGroupID ";

                base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                   .SetParameter("DateRangeGroupID", dateRangeGroupID)
                                   .SetParameter("UiValidationGroupID", item)
                                   .SetParameter("LockDate", uivalidationGroupIdDic[item])
                                   .ExecuteUpdate();

            }


        }
        public void InsertCfpByDateRangeGroup(IList<decimal> personIDList, decimal dateRangeGroupID, Dictionary<decimal, DateTime> uivalidationGroupIdDic)
        {
            string operationGUID = string.Empty;
            string SqlCommand = string.Empty;
            foreach (decimal item in uivalidationGroupIdDic.Keys)
            {
                if (personIDList.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {
                    SqlCommand = @" insert into TA_Calculation_Flag_Persons (CFP_PrsId,CFP_Date,CFP_MidNightCalculate,CFP_CalculationIsValid)
                                select personID,newCfpDate,cfpMidNightCalculate = 0, cfpCalculationIsValid = 0 from 
								(select PrsRangeAsg_PersonId as personID,  IIF ( min(PrsRangeAsg_FromDate) >= :LockDate, min(PrsRangeAsg_FromDate), dateadd(day,1,:LockDate) ) as newCfpDate 
								 from TA_PersonRangeAssignment
                                 where PrsRangeAsg_CalcRangeGrpId = :DateRangeGroupID
                                 group by PrsRangeAsg_PersonId) as assign
								 inner join TA_PersonTASpec
                                 on assign.personID = TA_PersonTASpec.prsTA_ID
                                 where assign.personID in (:PersonID) and  TA_PersonTASpec.prsTA_UIValidationGroupID = :UiValidationGroupID";
                    base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                   .SetParameter("DateRangeGroupID", dateRangeGroupID)
                                   .SetParameter("UiValidationGroupID", item)
                                   .SetParameter("LockDate", uivalidationGroupIdDic[item])
                                   .SetParameterList("PersonID", personIDList.ToArray())
                                   .ExecuteUpdate();
                }
                else
                {
                    operationGUID = this.tempRepository.InsertTempList(personIDList);
                    SqlCommand = @" insert into TA_Calculation_Flag_Persons (CFP_PrsId,CFP_Date,CFP_MidNightCalculate,CFP_CalculationIsValid)
                                    select personID,newCfpDate,cfpMidNightCalculate = 0, cfpCalculationIsValid = 0 from 
								    (select PrsRangeAsg_PersonId as personID,  IIF ( min(PrsRangeAsg_FromDate) >= :LockDate, min(PrsRangeAsg_FromDate), dateadd(day,1,:LockDate) ) as newCfpDate   
								    from TA_PersonRangeAssignment
                                    where PrsRangeAsg_CalcRangeGrpId = :DateRangeGroupID
                                    group by PrsRangeAsg_PersonId) as assign
								    inner join TA_PersonTASpec
                                    on assign.personID = TA_PersonTASpec.prsTA_ID
                                    inner join ta_temp 
                                    on ta_temp.temp_ObjectID = TA_PersonTASpec.prsTA_ID
                                    and  TA_PersonTASpec.prsTA_UIValidationGroupID = :UiValidationGroupID 
                                    and temp_OperationGUID = :OperationGuid";
                    base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                   .SetParameter("DateRangeGroupID", dateRangeGroupID)
                                   .SetParameter("UiValidationGroupID", item)
                                   .SetParameter("OperationGuid", operationGUID)
                                   .SetParameter("LockDate", uivalidationGroupIdDic[item])
                                   .ExecuteUpdate();
                    this.tempRepository.DeleteTempList(operationGUID);
                }
            }


        }
        public void InsertCFP(decimal personId, DateTime date) 
        {
            string SqlCommand = @"insert into TA_Calculation_Flag_Persons (CFP_CalculationIsValid,CFP_Date,CFP_MidNightCalculate,CFP_PrsId)
                                  values(0,:date,0,:personId)";
            base.NHibernateSession.CreateSQLQuery(SqlCommand)
                           .SetParameter("personId", personId)
                           .SetParameter("date", date)
                           .ExecuteUpdate();

        }

        public IList<CFP> GetCFPListByRuleCategory(decimal ruleCatId)
        {

            string SQLCommand = @"select * from TA_Calculation_Flag_Persons where CFP_PrsId in 
                                 (select PrsRulCatAsg_PersonId from TA_PersonRuleCategoryAssignment where PrsRulCatAsg_RuleCategoryId=:RuleCatId)";
            IList<CFP> list = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                //.SetResultTransformer(Transformers.AliasToBean(typeof(CFP)))
               .AddEntity(typeof(CFP))
               .SetParameter("RuleCatId", ruleCatId)
               .List<CFP>();

            return list;
        }

        public void UpdateCfpByPersonList(IList<decimal> personIdList, DateTime newCfpDate)
        {
            string operationGUID = string.Empty;
            string SqlCommand = string.Empty;
            if (personIdList.Count > 0)
            {
                if (personIdList.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {
                    SqlCommand = @" update cfp set cfp.CFP_Date = case  when  cfp.CFP_Date <= :NewCfpDate  then cfp.CFP_Date when   :NewCfpDate <= cfp.CFP_Date then :NewCfpDate  end , cfp.CFP_CalculationIsValid = 0
                               from TA_Calculation_Flag_Persons cfp
                               where cfp.CFP_PrsId in (:PersonIdList) ";

                    base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                       .SetParameterList("PersonIdList", personIdList.ToArray())
                                       .SetParameter("NewCfpDate", newCfpDate)
                                       .ExecuteUpdate();
                }
                else
                {
                    operationGUID = this.tempRepository.InsertTempList(personIdList);
                    SqlCommand = @" update cfp set cfp.CFP_Date = case   when  cfp.CFP_Date <= :NewCfpDate  then cfp.CFP_Date when  :NewCfpDate <= cfp.CFP_Date then :NewCfpDate  end , cfp.CFP_CalculationIsValid = 0
                               from TA_Calculation_Flag_Persons cfp
                               inner join ta_temp
                               on cfp.CFP_PrsId = temp_ObjectID
                               where temp_OperationGUID =:OperationGuid";

                    base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                       .SetParameter("OperationGuid", operationGUID)
                                       .SetParameter("NewCfpDate", newCfpDate)
                                       .ExecuteUpdate();
                    this.tempRepository.DeleteTempList(operationGUID);
                }
            }

            


        }
        public void InsertCfpByPersonList(IList<decimal> personIdList, DateTime newCfpDate)
        {
            string operationGUID = string.Empty;
            string SqlCommand = string.Empty;
            if (personIdList.Count > 0)
            {
                if (personIdList.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {
                    SqlCommand = @" insert into TA_Calculation_Flag_Persons (CFP_PrsId,CFP_Date,CFP_MidNightCalculate,CFP_CalculationIsValid)
                                select personID,newCfpDate,cfpMidNightCalculate = 0, cfpCalculationIsValid = 0 from 
								(select prs_ID as personID , :NewCfpDate  as newCfpDate from  TA_Person
                                 where prs_ID in (:PersonIdList)) person";
                    base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                   .SetParameterList("PersonIdList", personIdList.ToArray())
                                   .SetParameter("NewCfpDate", newCfpDate)
                                   .ExecuteUpdate();
                }
                else
                {
                    operationGUID = this.tempRepository.InsertTempList(personIdList);
                    SqlCommand = @" insert into TA_Calculation_Flag_Persons (CFP_PrsId,CFP_Date,CFP_MidNightCalculate,CFP_CalculationIsValid)
                                    select personID,newCfpDate,cfpMidNightCalculate = 0, cfpCalculationIsValid = 0 from 
								    (select prs_ID as personID , :NewCfpDate  as newCfpDate from  TA_Person
                                     inner join ta_temp
                                     on prs_ID = temp_ObjectID
                                     where temp_OperationGUID =:OperationGuid ) pesron ";
                    base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                   .SetParameter("OperationGuid", operationGUID)
                                   .SetParameter("NewCfpDate", newCfpDate)
                                   .ExecuteUpdate();
                    this.tempRepository.DeleteTempList(operationGUID);
                }
            }
            }


        

    }
}
