using System;
using System.Linq;
using GTS.Clock.Infrastructure.RepositoryFramework;
using NHibernate.Criterion;
using System.Collections.Generic;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.NHibernateFramework;

namespace GTS.Clock.Infrastructure.Repository
{
    public class PermitRepository : RepositoryBase<Permit>
    {
        TempRepository tempRepository = new TempRepository(false);
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);

        public override string TableName
        {
            get { return "TA_Permit"; }
        }

        public PermitRepository()
        {

        }

        public PermitRepository(bool Disconnectedly)
            : base(Disconnectedly)
        {

        }


        /// <summary>
        /// مجوز را برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="precardId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IList<Permit> GetExistingPermit(decimal personId, decimal precardId, DateTime fromDate, DateTime toDate)
        {
            string HQLCommand = @"select prm from Permit prm
                                Inner Join prm.Pairs as pairs
                                where prm.FromDate =:fromDate
                                AND prm.ToDate=:toDate AND prm.Person.ID=:personId
                                AND pairs.PreCardID =:precardId
                                ";
            IList<Permit> list = base.NHibernateSession.CreateQuery(HQLCommand)
               .SetParameter("fromDate", fromDate)
               .SetParameter("toDate", toDate)
               .SetParameter("personId", personId)
               .SetParameter("precardId", precardId)
               .List<Permit>();
            return list;
        }
		public Permit GetExistingPermit(decimal requestID)
		{
			string HQLCommand = @"select prm from Permit prm
                                Inner Join prm.Pairs as pairs
                                where 
                                 pairs.RequestID =:requestId
                                ";
			Permit permit = base.NHibernateSession.CreateQuery(HQLCommand)
			   
			   .SetParameter("requestId", requestID)
			   .List<Permit>().FirstOrDefault();
			return permit;
		}
        public void DeletePermitWithoutPair()
        {
            string SQLCommand = @"DELETE from TA_Permit 
                                where Permit_ID not in 
                                (select PermitPair_PermitId from TA_PermitPair)";
            base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .ExecuteUpdate();
        }

        public int GetExistingPermitCount(IList<decimal> personIds, IList<decimal> precardIds, DateTime date)
        {
            if (personIds == null || personIds.Count == 0 || precardIds == null || precardIds.Count == 0)
            {
                return 0;
            }

            string personOperationGUID = string.Empty;
            string precardOperationGUID = string.Empty;
            personOperationGUID = this.tempRepository.InsertTempList(personIds);
            precardOperationGUID = this.tempRepository.InsertTempList(precardIds);
            string HQLCommand = @"select count(pairs) as pairsCount from Permit prm
                                  Inner Join prm.Person person
                                  Inner Join person.TempList personTemp
                                  Inner Join prm.Pairs as pairs
                                  Inner Join pairs.Precard precard
                                  Inner Join precard.TempList precardTemp
                                  where prm.FromDate =:fromDate
                                  AND personTemp.OperationGUID = :personOperationGUID
                                  AND precardTemp.OperationGUID = :precardOperationGUID";
            object count = base.NHibernateSession.CreateQuery(HQLCommand)
               .SetParameter("fromDate", date)
               .SetParameterList("personOperationGUID", personOperationGUID)
               .SetParameterList("precardOperationGUID", precardOperationGUID)
               .List<object>().First();
            this.tempRepository.DeleteTempList(personOperationGUID);
            this.tempRepository.DeleteTempList(precardOperationGUID);
            return Utility.Utility.ToInteger(count.ToString());
        }
		public int GetExistingPermitCount(decimal requestId)
		{
			if (requestId == 0)
			{
				return 0;
			}


			string HQLCommand = @"select count(pairs) as permitPairsCount from PermitPair as pairs
                                  where pairs.RequestID =:requestID"
								  ;
			object count = base.NHibernateSession.CreateQuery(HQLCommand)
			   .SetParameter("requestID", requestId)
			
			   .List<object>().First();
			return Utility.Utility.ToInteger(count.ToString());
		}
        public IList<Permit> GetExistingPermit(IList<decimal> personIds, IList<decimal> precardIds, DateTime date, SentryPermitsOrderBy orderby, int pageIndex, int pageSize)
        {
            if (personIds == null || personIds.Count == 0 || precardIds == null || precardIds.Count == 0)
            {
                return new List<Permit>();
            }

            IList<Permit> list = null;
            string HQLCommand = string.Empty;
            string HQLrderby = "";
            switch (orderby)
            {
                case SentryPermitsOrderBy.PersonCode:
                    HQLrderby = " order by prm.Person.BarCode";
                    break;
                case SentryPermitsOrderBy.PersonName:
                    HQLrderby = " order by (prm.Person.FirstName + ' ' + prm.Person.LastName)";
                    break;
                case SentryPermitsOrderBy.PermitSubject:
                    HQLrderby = " order by pairs.PreCardID";
                    break;
            }

            if (personIds.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
            {
                HQLCommand = @"select prm from Permit prm    
                                  join prm.Pairs as pairs                           
                                  where prm.FromDate =:fromDate
                                  AND prm.Person.ID in (:personIds)
                                " + HQLrderby;
                list = base.NHibernateSession.CreateQuery(HQLCommand)
                                             .SetParameter("fromDate", date)
                                             .SetParameterList("personIds", base.CheckListParameter(personIds))
                                             .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                                             .List<Permit>();
            }
            else
            {
                string operationGUID = this.tempRepository.InsertTempList(personIds);
                HQLCommand = @"select prm from Permit prm    
                                  Inner join prm.Pairs pairs
                                  Inner join prm.Person person
                                  Inner join person.TempList temp                          
                                  where prm.FromDate =:fromDate
                                  AND temp.OperationGUID = :operationGUID)
                                " + HQLrderby;
                list = base.NHibernateSession.CreateQuery(HQLCommand)
                                             .SetParameter("fromDate", date)
                                             .SetParameterList("operationGUID", operationGUID)
                                             .SetFirstResult(pageIndex * pageSize).SetMaxResults(pageSize)
                                             .List<Permit>();
                this.tempRepository.DeleteTempList(operationGUID);
            }
            var permits = from o in list
                          group o by o.ID;
            return list;
        }

        public void DeleteByRequestId(decimal requestID)
        {
            String SQLCommand = @"DELETE FROM  TA_PermitPair WHERE PermitPair_RequestId=:requestId";

            base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetParameter("requestId", requestID)
                .ExecuteUpdate();
        }

        public void InvalidateTrafficCalculation(decimal personId, DateTime date)
        {
            try
            {
                string SQLCommand = "";

//                SQLCommand = @"update TA_BaseTraffic
//                           set BasicTraffic_Used = 0
//                           where BasicTraffic_PersonId = :personId
//                           and (BasicTraffic_Date >= :fromDate 
//                                OR BasicTraffic_ID in (select isnull(ProceedTrafficPair_BasicTrafficIdFrom,0) from TA_ProceedTraffic join TA_ProceedTrafficPair on ProceedTraffic_ID=ProceedTrafficPair_ProceedTrafficId and ProceedTraffic_PersonId=:personId and ProceedTraffic_FromDate>=:fromDate
//                                                       union
//                                                       select isnull(ProceedTrafficPair_BasicTrafficIdTo,0) from TA_ProceedTraffic join TA_ProceedTrafficPair on ProceedTraffic_ID=ProceedTrafficPair_ProceedTrafficId and ProceedTraffic_PersonId=:personId and ProceedTraffic_FromDate>=:fromDate
//                                                      )
//                            )";

                SQLCommand = @"update TA_BaseTraffic
                           set BasicTraffic_Used = 0
                           where BasicTraffic_PersonId = :personId
                           and (BasicTraffic_Date >= :fromDate 
                                OR exists (select isnull(ProceedTrafficPair_BasicTrafficIdFrom,0) from TA_ProceedTraffic join TA_ProceedTrafficPair on ProceedTraffic_ID=ProceedTrafficPair_ProceedTrafficId and ProceedTraffic_PersonId=:personId and ProceedTraffic_FromDate>=:fromDate and ProceedTrafficPair_BasicTrafficIdFrom = BasicTraffic_ID
                                           union
                                           select isnull(ProceedTrafficPair_BasicTrafficIdTo,0) from TA_ProceedTraffic join TA_ProceedTrafficPair on ProceedTraffic_ID=ProceedTrafficPair_ProceedTrafficId and ProceedTraffic_PersonId=:personId and ProceedTraffic_FromDate>=:fromDate and ProceedTrafficPair_BasicTrafficIdTo = BasicTraffic_ID
                                          )
                            )";


                base.NHibernateSession.CreateSQLQuery(SQLCommand)
                    .SetParameter("personId", personId)
                    .SetParameter("fromDate", date.Date)
                    .ExecuteUpdate();

//                SQLCommand = @"update prmPair
//                                SET PermitPair_IsApplyedOnTraffic = 0
//                                FROM TA_Permit join TA_PermitPair prmPair on Permit_ID=PermitPair_PermitId and Permit_PersonId = :personId 
//                                and (Permit_FromDate >= :fromDate 
//                                        OR permitPair_ID in (select isnull(ProceedTrafficPair_PermitIdFrom,0) from TA_ProceedTraffic join TA_ProceedTrafficPair on ProceedTraffic_ID=ProceedTrafficPair_ProceedTrafficId and ProceedTraffic_PersonId=:personId and ProceedTraffic_FromDate>=:fromDate
//                                                             union
//                                                             select isnull(ProceedTrafficPair_PermitIdTo,0) from TA_ProceedTraffic join TA_ProceedTrafficPair on ProceedTraffic_ID=ProceedTrafficPair_ProceedTrafficId and ProceedTraffic_PersonId=:personId and ProceedTraffic_FromDate>=:fromDate
//                                                            )
//                                 )";

                SQLCommand = @"update prmPair
                                SET PermitPair_IsApplyedOnTraffic = 0
                                FROM TA_Permit join TA_PermitPair prmPair on Permit_ID=PermitPair_PermitId and Permit_PersonId = :personId 
                                and (Permit_FromDate >= :fromDate 
                                        OR  exists (select isnull(ProceedTrafficPair_PermitIdFrom,0) from TA_ProceedTraffic join TA_ProceedTrafficPair on ProceedTraffic_ID=ProceedTrafficPair_ProceedTrafficId and ProceedTraffic_PersonId=:personId and ProceedTraffic_FromDate>=:fromDate and ProceedTrafficPair_PermitIdFrom = permitPair_ID
                                                    union
                                                    select isnull(ProceedTrafficPair_PermitIdTo,0) from TA_ProceedTraffic join TA_ProceedTrafficPair on ProceedTraffic_ID=ProceedTrafficPair_ProceedTrafficId and ProceedTraffic_PersonId=:personId and ProceedTraffic_FromDate>=:fromDate and ProceedTrafficPair_PermitIdTo = permitPair_ID
                                                   )
                                 )";

                SQLCommand = @"update TA_PermitPair
                           set PermitPair_IsApplyedOnTraffic = 0
                           where Permitpair_PermitId in (select Permit_ID from TA_Permit
						   where Permit_PersonId = :personId
						   and Permit_FromDate >= :fromDate)";
                base.NHibernateSession.CreateSQLQuery(SQLCommand)
                    .SetParameter("personId", personId)
                    .SetParameter("fromDate", date.Date)
                    .ExecuteUpdate();

//                SQLCommand = @"delete from TA_ProceedTraffic
//                                  where ProceedTraffic_PersonId = :personId
//                                  and ProceedTraffic_FromDate >= :fromDate";

//                base.NHibernateSession.CreateSQLQuery(SQLCommand)
//                    .SetParameter("personId", personId)
//                    .SetParameter("fromDate", date.Date)
//                    .ExecuteUpdate();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteProceedTraffic(decimal personId, DateTime date)
        {
            string SQLCommand = @"delete from TA_ProceedTraffic
                                         where ProceedTraffic_PersonId = :personId
                                         and ProceedTraffic_FromDate >= :fromDate";

            base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetParameter("personId", personId)
                .SetParameter("fromDate", date.Date)
                .ExecuteUpdate(); 
        }
   
    }
}
