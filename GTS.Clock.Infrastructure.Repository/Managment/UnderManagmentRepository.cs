using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using System.Linq;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model;


namespace GTS.Clock.Infrastructure.Repository
{
    public class UnderManagmentRepository : RepositoryBase<UnderManagment>, IUnderManagmentRepository
    {
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        public override string TableName
        {
            get { return "TA_UnderManagment"; }
        }

        public UnderManagmentRepository(bool disconnectly)
            : base(disconnectly)
        { }

        #region IUnderManagmentRepository Members

        public IList<Department> GetAssignDepartments()
        {
            string HQLCommand = @"from Department where ID in
                        (select Department from UnderManagment)";
            IList<Department> list = base.NHibernateSession.CreateQuery(HQLCommand).List<Department>();
            return list;
        }

        public void DeleteUnderManagments(decimal flowId)
        {
            string HQLCommand = @"Delete from UnderManagment where Flow = :flow";
            base.NHibernateSession.CreateQuery(HQLCommand)
                .SetParameter("flow", new Flow() { ID = flowId })
                .ExecuteUpdate();
        }
        public void InsertUnderManagmentPersons(decimal flowId)
        {
            string SQLCommand = @"INSERT INTO TA_UnderManagementsPersons( underManagementPersons_FlowID , underManagementPersons_PersonID)                                                                                                       
                                  SELECT flow_ID, Prs_Id
	                              FROM (SELECT flow_ID FROM TA_Flow     
		                                WHERE flow_Deleted = 0 and flow_ID =:flowId
			                           ) Flow			
                                  CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (Flow.flow_ID) as UndermanagmentsPersons ";
            base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                  .SetParameter("flowId", flowId)
                                  .ExecuteUpdate();

        }
        public void InsertUnderManagmentPersons(decimal personId, IList<decimal> flowIds)
        {
            string SQLCommand = string.Empty;
            if (flowIds.Count < operationBatchSizeValue && operationBatchSizeValue < 2100)
            {
                SQLCommand = @"INSERT INTO TA_UnderManagementsPersons( underManagementPersons_FlowID , underManagementPersons_PersonID)                                                                                                       
                                  SELECT flow_ID ,:personId
	                              FROM (SELECT flow_ID FROM TA_Flow     
		                                WHERE flow_Deleted = 0 and flow_ID in (:flowIds)
			                           ) Flow			
                                 ";
                base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                      .SetParameterList("flowIds", flowIds.ToArray())
                                      .SetParameter("personId", personId)
                                      .ExecuteUpdate();
            }
            else
            {
                TempRepository temp = new TempRepository(false);
                string operationGUID = temp.InsertTempList(flowIds);
                SQLCommand = @"INSERT INTO TA_UnderManagementsPersons( underManagementPersons_FlowID , underManagementPersons_PersonID)                                                                                                       
                                  SELECT flow_ID ,:personId
	                              FROM (SELECT flow_ID FROM TA_Flow
                                        INNER JOIN TA_Temp temp
                                        ON    flow_ID = temp.temp_ObjectID
		                                WHERE flow_Deleted = 0 and temp.temp_OperationGUID =:operationGUID
			                           ) Flow			
                                   ";
                base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                     .SetParameter("operationGUID", operationGUID)
                                     .SetParameter("personId", personId)
                                     .ExecuteUpdate();
                temp.DeleteTempList(operationGUID);
            }
        }
        public void InsertUnderManagmentPersons(IList<decimal> personIds, IList<decimal> flowIds)
        {
            string SQLCommand = string.Empty;
            if (personIds.Count < operationBatchSizeValue && operationBatchSizeValue < 2100)
            {
                foreach (decimal flowid in flowIds)
                {
                    SQLCommand = "insert into TA_UnderManagementsPersons (underManagementPersons_FlowID , underManagementPersons_PersonID) " +
                                  "select " + flowid.ToString() + " , Prs_ID from TA_Person where Prs_ID in (:personIds) and " +
                                                                                             "Prs_ID not in(select underMng_PersonID " +
                                                                                                           "from TA_UnderManagment " +
                                                                                                           "where underMng_PersonID in (:personIds) and underMng_Contains = 1 and underMng_FlowID = " + flowid.ToString() + "" +
                                                                                                          ")";
                    base.NHibernateSession.CreateSQLQuery(SQLCommand)
                        .SetParameterList("personIds", personIds.ToArray())
                        .ExecuteUpdate();
                }

            }
            else
            {
                foreach (decimal flowid in flowIds)
                {
                    TempRepository temp = new TempRepository(false);
                    string operationGUID = temp.InsertTempList(personIds);
                    SQLCommand = "insert into TA_UnderManagementsPersons (underManagementPersons_FlowID , underManagementPersons_PersonID) " +
                                          "select " + flowid.ToString() + " , Prs_ID from TA_Person " +
                                                          "inner join ta_temp temp" +
                                                          " on Prs_ID = temp.temp_ObjectID " +
                                                           " where  temp.temp_OperationGUID =:operationGUID  and " +
                                                                  " Prs_ID not in(select underMng_PersonID " +
                                                                                " from TA_UnderManagment " +
                                                                                " inner join ta_temp temp " +
                                                                                " on  underMng_PersonID = temp.temp_objectID " +
                                                                                " where temp.temp_OperationGUID =:operationGUID and underMng_Contains = 1 and underMng_FlowID = " + flowid.ToString() + "" +
                                                                               " )";

                    base.NHibernateSession.CreateSQLQuery(SQLCommand)
                        .SetParameter("operationGUID", operationGUID)
                        .ExecuteUpdate();
                    temp.DeleteTempList(operationGUID);
                }
            }
        }
        public void DeleteUnderManagmentPersonsByPerson(decimal personId)
        {
            string SQLCommand = @"Delete From TA_UnderManagementsPersons where underManagementPersons_PersonID =:PersonId";
            base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetParameter("PersonId", personId)
                .ExecuteUpdate();
        }
        public void DeleteUnderManagmentPersons(decimal flowId)
        {
            string SQLCommand = @"Delete From TA_UnderManagementsPersons where underManagementPersons_FlowID =:flowId";
            base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetParameter("flowId", flowId)
                .ExecuteUpdate();
        }
        public void DeleteUnderManagmentPersons(decimal personId, IList<decimal> flowIds)
        {
            string SQLCommand = string.Empty;
            if (flowIds.Count < operationBatchSizeValue && operationBatchSizeValue < 2100)
            {
                SQLCommand = @"DELETE FROM TA_UnderManagementsPersons WHERE underManagementPersons_FlowID IN (:FlowIds) and underManagementPersons_PersonID =:personId";
                base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                      .SetParameterList("FlowIds", flowIds.ToArray())
                                      .SetParameter("personId", personId)
                                      .ExecuteUpdate();
            }
            else
            {
                TempRepository tempRep = new TempRepository(false);
                string operationGUID = tempRep.InsertTempList(flowIds);
                SQLCommand = @"Delete under FROM TA_UnderManagementsPersons under
                               INNER JOIN TA_Temp temp
                               ON    underManagementPersons_FlowID = temp.temp_ObjectID
                               WHERE temp_OperationGUID =:operationGUID  and underManagementPersons_PersonID =:personId
                                ";
                base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                      .SetParameter("operationGUID", operationGUID)
                                      .SetParameter("personId", personId)
                                      .ExecuteUpdate();
                tempRep.DeleteTempList(operationGUID);
            }
        }

        public void DeleteUnderManagmentPersonsWithOrganicInfo(decimal flowId, IList<decimal> PersonIds)
        {
            string SQLCommand = string.Empty;
            if (PersonIds.Count < operationBatchSizeValue && operationBatchSizeValue < 2100)
            {
                SQLCommand = @"DELETE FROM TA_UnderManagementsPersons WHERE underManagementPersons_PersonID IN (:PersonIds) and underManagementPersons_FlowID =:flowId";
                base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                      .SetParameterList("PersonIds", PersonIds.ToArray())
                                      .SetParameter("flowId", flowId)
                                      .ExecuteUpdate();
            }
            else
            {
                TempRepository tempRep = new TempRepository(false);
                string operationGUID = tempRep.InsertTempList(PersonIds);
                SQLCommand = @"Delete under FROM TA_UnderManagementsPersons under
                               INNER JOIN TA_Temp temp
                               ON    underManagementPersons_PersonID = temp.temp_ObjectID
                               WHERE temp_OperationGUID =:operationGUID  and underManagementPersons_FlowID =:flowId
                                ";
                base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                      .SetParameter("operationGUID", operationGUID)
                                      .SetParameter("flowId", flowId)
                                      .ExecuteUpdate();
                tempRep.DeleteTempList(operationGUID);
            }
        }
        public void UpdateUnderManagementPersons(decimal flowId)
        {
            string SQLCommanddDelete = @"Delete From TA_UnderManagementsPersons where underManagementPersons_FlowID =:flowId";
            base.NHibernateSession.CreateSQLQuery(SQLCommanddDelete)
                .SetParameter("flowId", flowId)
                .ExecuteUpdate();
            string SQLCommandInsert = @"INSERT INTO TA_UnderManagementsPersons( underManagementPersons_FlowID , underManagementPersons_PersonID)                                                                                                       
                                        SELECT flow_ID, Prs_Id
	                                    FROM (SELECT flow_ID FROM TA_Flow     
		                                      WHERE flow_Deleted = 0 and flow_ID =:flowId
			                                 ) Flow			
                                        CROSS APPLY [dbo].[TA_GetUnderManagmentPersons] (Flow.flow_ID) as UndermanagmentsPersons ";
            base.NHibernateSession.CreateSQLQuery(SQLCommandInsert)
                                  .SetParameter("flowId", flowId)
                                  .ExecuteUpdate();
        }
        public void UpdateUnderManagementPersons(decimal personId, decimal newDepartmentId, decimal oldDepartmentId, ActionType action)
        {
            string SQLCommandNewFlow = string.Empty;
            string SQLCommandOldFlow = string.Empty;
            string SQLCommandPersonFlows = string.Empty;

            IList<decimal> NewFlowList = this.GetDepartmentFlows(newDepartmentId);
            IList<decimal> PersonelFlowList = this.GetPersonelparticularFlows(personId);
            //ویرایش پرسنل
            if (action == ActionType.EDIT)
            {
                if (oldDepartmentId != 0)
                {
                    IList<decimal> OldFlowList = this.GetDepartmentFlows(oldDepartmentId);
                    if (OldFlowList.Count != 0)
                    {
                        if (PersonelFlowList.Count != 0)
                            OldFlowList = OldFlowList.Where(x => !PersonelFlowList.ToList<decimal>().Contains(x)).ToList<decimal>();
                        if (OldFlowList.Count != 0)
                            this.DeleteUnderManagmentPersons(personId, OldFlowList);
                    }
                    if (NewFlowList.Count != 0)
                    {
                        if (PersonelFlowList.Count != 0)
                            NewFlowList = NewFlowList.Where(x => !PersonelFlowList.ToList<decimal>().Contains(x)).ToList<decimal>();
                        if (NewFlowList.Count != 0)
                        this.InsertUnderManagmentPersons(personId, NewFlowList);
                    }

                }
                //پرسنل جدید و بازیابی پرسنل
                else
                {
                    if (NewFlowList.Count != 0 && PersonelFlowList.Count != 0)
                    {
                        ((List<decimal>)NewFlowList).AddRange(PersonelFlowList);
                        NewFlowList = NewFlowList.Distinct().ToList();
                        this.InsertUnderManagmentPersons(personId, NewFlowList);
                    }
                    if (NewFlowList.Count != 0 && PersonelFlowList.Count == 0)
                    {
                        this.InsertUnderManagmentPersons(personId, NewFlowList);
                    }
                    if (NewFlowList.Count == 0 && PersonelFlowList.Count != 0)
                    {
                        this.InsertUnderManagmentPersons(personId, PersonelFlowList);
                    }

                }
            }
            //حذف پرسنل
            if (action == ActionType.DELETE)
            {
                if (NewFlowList.Count != 0 || PersonelFlowList.Count != 0)
                {
                    this.DeleteUnderManagmentPersonsByPerson(personId);
                }
            }

        }

        public IList<decimal> GetPersonelparticularFlows(decimal PersonId)
        {
            string SQLCommandPersonFlows = @"select underMng_FlowID from TA_UnderManagment
                                                  where underMng_PersonID IS NOT NULL  AND 
                                                  underMng_PersonID =:personId  AND 
                                                  underMng_Contains = 1";
            IList<decimal> PersonelFlowList = base.NHibernateSession.CreateSQLQuery(SQLCommandPersonFlows)
                                                                    .SetParameter("personId", PersonId)
                                                                    .List<decimal>();
            return PersonelFlowList;
        }
        public IList<UnderManagment> GetAllPersonelparticularFlows(IList<decimal> FlowIds)
        {
            string SQLCommand = @"select * from TA_UnderManagment where underMng_PersonID is not null and underMng_FlowId in (:FlowIds) and underMng_Contains = 1";
            IList<UnderManagment> underMngList = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                                                       .AddEntity(typeof(UnderManagment))
                                                                       .SetParameterList("FlowIds", FlowIds.ToArray())
                                                                       .List<UnderManagment>();
            return underMngList;
        }

        public IList<decimal> GetDepartmentFlows(decimal DepartmentId)
        {
            string SQLCommand = @"SELECT * FROM [dbo].[TA_GetDepartmentFlows] (:DepartmentId)";
            IList<decimal> DepartmentFlowsList = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                                                        .SetParameter("DepartmentId", DepartmentId)
                                                                        .List<decimal>();
            return DepartmentFlowsList;
        }

        public IList<decimal> GetFlowDirectPersonles(decimal FlowId)
        {
            string SQLCommand = @"select underMng_PersonID from TA_UnderManagment where underMng_PersonID is not null and underMng_FlowID =:FlowId and underMng_Contains = 1";
            IList<decimal> PersonIds = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                                                             .SetParameter("FlowId", FlowId)
                                                             .List<decimal>();
            return PersonIds;
        }


        #endregion

    }
}
