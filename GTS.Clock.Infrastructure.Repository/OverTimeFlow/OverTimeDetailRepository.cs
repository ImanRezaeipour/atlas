using GTS.Clock.Infrastructure.RepositoryFramework;
using GTS.Clock.Model.OverTimeFlow;
using NHibernate;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure.Repository.OverTimeFlow
{
    public class OverTimeDetailRepository : RepositoryBase<GTS.Clock.Model.OverTimeFlow.OverTimeDetail>
    {
       

        public override string TableName
        {
            get { return "TA_OverTimeDetail"; }
        }

        public OverTimeDetailRepository(bool disconectly)
            : base(disconectly)
        { }


        public IList<OverTimeDetail> GetDetailByPersonDepartmentparentPath(DateTime Date, string parentPath)
        {
            IQuery Query = null;
            IList<OverTimeDetail> list = null;

            //            string SQLCommand = @"
            //                                DECLARE @Date datetime
            //                                DECLARE @parentPath varchar(50)
            //                                SET @Date=:thedate
            //                                set @parentPath=:parentPath
            //
            //                                select OTD.[OverTimeDtl_ID] AS ID
            //             
            //                                  ,OTD.[OverTimeDtl_MaxOverTime] AS MaxOverTime
            //                                  ,OTD.[OverTimeDtl_MaxHoliday] AS MaxHoliday
            //                                  ,OTD.[OverTimeDtl_MaxNightly] AS MaxNightly
            //                                FROM TA_OverTimeDetail AS OTD
            //                                inner JOIN	TA_OverTime AS OT ON OT.OverTime_ID=OTD.OverTimeDtl_OverTimeID
            //                                inner JOIN	TA_Department AS Dept ON Dept.dep_ID=OTD.OverTimeDtl_DepartmentId
            //                                where OT.OverTime_DateTime=@Date
            //                                AND @parentPath like '%'+convert(varchar(50),OTD.OverTimeDtl_DepartmentId)+'%'
            //                                AND Dept.dep_DepartmentType=1";

            string SQLCommand = @"
                                DECLARE @Date datetime
                                DECLARE @parentPath varchar(50)
                                SET @Date=:thedate
                                set @parentPath=:parentPath

                                select OTD.[OverTimeDtl_ID] AS ID
             
                                  ,OTD.[OverTimeDtl_MaxOverTime] AS MaxOverTime
                                  ,OTD.[OverTimeDtl_MaxHoliday] AS MaxHoliday
                                  ,OTD.[OverTimeDtl_MaxNightly] AS MaxNightly
                                FROM TA_OverTimeDetail AS OTD
                                inner JOIN	TA_OverTime AS OT ON OT.OverTime_ID=OTD.OverTimeDtl_OverTimeID
                                inner JOIN	TA_Department AS Dept ON Dept.dep_ID=OTD.OverTimeDtl_DepartmentId
                                where OT.OverTime_DateTime=@Date
                                AND @parentPath like '%'+convert(varchar(50),Dept.dep_ParentPath)+'%'
                                AND Dept.dep_DepartmentType=1";

            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetParameter("parentPath", parentPath)
                   .SetParameter("thedate", Date);

            Query = Query.SetResultTransformer(Transformers.AliasToBean(typeof(OverTimeDetail)));

            list = Query.List<OverTimeDetail>();

            return list;

        }

        public IList<OverTimeDetail> GetDetailByPersonDepartment(DateTime Date, string parentPath)
        {
            IQuery Query = null;
            IList<OverTimeDetail> list = null;

            string SQLCommand = @"
                                DECLARE @Date datetime
                                DECLARE @parentPath varchar(50)
                                SET @Date=:thedate
                                set @parentPath=:parentPath

                                select OTD.[OverTimeDtl_ID] AS ID
                                  ,OTD.[OverTimeDtl_MaxOverTime] AS MaxOverTime
                                  ,OTD.[OverTimeDtl_MaxHoliday] AS MaxHoliday
                                  ,OTD.[OverTimeDtl_MaxNightly] AS MaxNightly
                                FROM TA_OverTimeDetail AS OTD
                                inner JOIN	TA_OverTime AS OT ON OT.OverTime_ID=OTD.OverTimeDtl_OverTimeID
                                inner JOIN	TA_Department AS Dept ON Dept.dep_ID=OTD.OverTimeDtl_DepartmentId
                                where OT.OverTime_DateTime=@Date
                                AND @parentPath like '%,'+convert(varchar(50),Dept.dep_ID)+',%'
                                AND Dept.dep_DepartmentType=1";

            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetParameter("parentPath", parentPath)
                   .SetParameter("thedate", Date);

            Query = Query.SetResultTransformer(Transformers.AliasToBean(typeof(OverTimeDetail)));

            list = Query.List<OverTimeDetail>();

            return list;

        }

        /// <summary>
        /// لیست همه بودجه معاونت های مربوط به یک سازمان را بر می گرداند
        /// </summary>
        /// <param name="Date">تاریخ</param>
        /// <param name="parentPath">مسیر بخش سازمان</param>
        /// <returns></returns>
        public IList<OverTimeDetail> GetDetailsByOrganizationPath(DateTime Date, decimal OrganizationId)
        {
            IQuery Query = null;
            IList<OverTimeDetail> list = null;

            string SQLCommand = @"
                                DECLARE @Date datetime
                                DECLARE @parentPath varchar(50)
                                SET @Date=:thedate
                                set @parentPath=:parentPath

                                select OTD.[OverTimeDtl_ID] AS ID
                                  ,OTD.[OverTimeDtl_MaxOverTime] AS MaxOverTime
                                  ,OTD.[OverTimeDtl_MaxHoliday] AS MaxHoliday
                                  ,OTD.[OverTimeDtl_MaxNightly] AS MaxNightly
                                FROM TA_OverTimeDetail AS OTD
                                inner JOIN	TA_OverTime AS OT ON OT.OverTime_ID=OTD.OverTimeDtl_OverTimeID
                                inner JOIN	TA_Department AS Dept ON Dept.dep_ID=OTD.OverTimeDtl_DepartmentId
                                where OT.OverTime_DateTime=@Date
                                AND Dept.dep_ParentPath like '%'+@parentPath +'%'
                                AND Dept.dep_DepartmentType=1";

            Query = base.NHibernateSession.CreateSQLQuery(SQLCommand)
                .SetParameter("parentPath", OrganizationId.ToString())
                   .SetParameter("thedate", Date);

            Query = Query.SetResultTransformer(Transformers.AliasToBean(typeof(OverTimeDetail)));

            list = Query.List<OverTimeDetail>();

            return list;

        }

        
    }
}
