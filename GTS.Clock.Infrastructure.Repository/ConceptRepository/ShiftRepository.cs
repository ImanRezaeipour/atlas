using System;
using System.Linq;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Infrastructure.RepositoryFramework;
using NHibernate.Criterion;
using System.Collections.Generic;
using GTS.Clock.Model;

namespace GTS.Clock.Infrastructure.Repository
{
    public class ShiftRepository : RepositoryBase<Shift>, IShiftRepository 
    {
        public override string TableName
        {
            get { return "TA_Shift"; }
        }       

        public ShiftRepository() 
        {

        }

        public ShiftRepository(bool Disconnectedly)
            : base(Disconnectedly)
        {

        }

        /// <summary>
        /// آیا این شیفت به روزی اختصاص داده شده است یا نه
        /// </summary>
        /// <param name="shiftId"></param>
        /// <returns></returns>
        public bool HasWorkGroupDetail(decimal shiftId) 
        {
            return base.NHibernateSession.CreateSQLQuery("select * from TA_WorkGroupDetail where WorkGroupDtl_ShiftID= :shiftId")
                  .SetParameter("shiftId", shiftId).List().Count > 0;
        }

        public decimal? GetShiftIdByPersonId(decimal personId, DateTime date) 
        {
            String sqlCommand = @"select WorkGroupDtl_ShiftId from TA_WorkGroupDetail 
                                 where WorkGroupDtl_WorkGroupId in (select top(1)AsgWorkGroup_WorkGroupId from TA_AssignWorkGroup where TA_AssignWorkGroup.AsgWorkGroup_PersonId=:personId AND AsgWorkGroup_FromDate <=:date ORDER BY AsgWorkGroup_FromDate DESC)
                                 AND WorkGroupDtl_Date=:date";

            IList<decimal> result= NHibernateSession.CreateSQLQuery(sqlCommand)
                                                    .SetParameter("personId", personId)
                                                    .SetParameter("date", date.Date)
                                                    .List<decimal>();
            return result.FirstOrDefault();
        }

        public decimal? GetShiftIdByWorkGroupId(decimal workGroupId, DateTime date)
        {
            String sqlCommand = @"select WorkGroupDtl_ShiftId from TA_WorkGroupDetail 
                                 where WorkGroupDtl_WorkGroupId =:workGroupId
                                 AND WorkGroupDtl_Date=:date";

            IList<decimal> result = NHibernateSession.CreateSQLQuery(sqlCommand)
                .SetParameter("workGroupId", workGroupId)
                .SetParameter("date", date.Date)
                .List<decimal>();
            return result.FirstOrDefault();
        }

        public DateTime? GetFirstShiftUsedInWorkGroup(decimal shiftId, decimal workGroupId) 
        {
            String sqlCommand = @"select WorkGroupDtl_Date from TA_WorkGroupDetail 
                                 where WorkGroupDtl_WorkGroupId =:workGroupId
                                 AND WorkGroupDtl_ShiftId=:shiftId";

            IList<DateTime> result = NHibernateSession.CreateSQLQuery(sqlCommand)
                .SetParameter("workGroupId", workGroupId)
                .SetParameter("shiftId", shiftId)
                .List<DateTime>();
            return result.FirstOrDefault();
        }

        public Shift GetShiftByPerson(decimal personId, DateTime date)
        {
            decimal? shiftId = this.GetShiftIdByPersonId(personId, date);
            if (shiftId != null)
            {
                Shift sh = base.GetById((decimal)shiftId, false);
                return sh;
            }
            return null;
        }
        public IList<Shift> GetAllShift(string SearchTerm)
        {
            string SqlCommand = @"select * from TA_Shift where Shift_Name like :SearchTerm  or Shift_CustomCode like :SearchTerm";
            IList<Shift> ShiftList = base.NHibernateSession.CreateSQLQuery(SqlCommand)
                                                           .AddEntity(typeof(Shift))
                                                           .SetParameter("SearchTerm", String.Format("%{0}%", SearchTerm))
                                                           .List<Shift>();
            return ShiftList;
        }
        public int CountByShiftName(string name)
        {
            throw new NotImplementedException();
        }

        public int CountByShiftColor(string name)
        {
            throw new NotImplementedException();
        }

        public int CountByShiftCustomCode(string name)
        {
            throw new NotImplementedException();
        }

      
    }
}
