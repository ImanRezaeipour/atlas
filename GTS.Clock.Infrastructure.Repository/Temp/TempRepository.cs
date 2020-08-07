using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.RepositoryFramework;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure.Repository
{
    public class TempRepository : RepositoryBase<GTS.Clock.Model.Temp.Temp>
    {

        public override string TableName
        {
            get { return "TA_Temp"; }
        }

        public TempRepository(bool Disconnectedly)
            : base(Disconnectedly)
        {

        }

        public string InsertTempList(IList<decimal> objectIDList)
        {
            string operationGUID = string.Empty;
            DataTable tempTable = new DataTable();
            try
            {
                using (NHibernateSessionManager.Instance.BeginTransactionOn())
                {
                    tempTable = new DataTable();
                    tempTable.Columns.Add("ObjectID", typeof(decimal));
                    tempTable.Columns.Add("OperationGUID", typeof(string));
                    tempTable.Columns.Add("CreationDate", typeof(string));

                    operationGUID = Guid.NewGuid().ToString();
                    DateTime creationDate = DateTime.Now;
                    foreach (decimal objectID in objectIDList)
                    {
                        DataRow drTemp = tempTable.NewRow();
                        drTemp["ObjectID"] = objectID;
                        drTemp["OperationGUID"] = operationGUID;
                        drTemp["CreationDate"] = creationDate;
                        tempTable.Rows.Add(drTemp);
                    }

                    SqlBulkCopy TempBulkInsert = new System.Data.SqlClient.SqlBulkCopy((SqlConnection)NHibernateSessionManager.Instance.GetSession().Connection, SqlBulkCopyOptions.Default, (SqlTransaction)NHibernateSessionManager.Instance.GetTransaction().GetDbTransaction);
                    TempBulkInsert.DestinationTableName = "dbo.TA_Temp";
                    TempBulkInsert.ColumnMappings.Add("ObjectID", "temp_ObjectID");
                    TempBulkInsert.ColumnMappings.Add("OperationGUID", "temp_OperationGUID");
                    TempBulkInsert.ColumnMappings.Add("CreationDate", "temp_CreationDate");
                    TempBulkInsert.WriteToServer(tempTable);

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }

                return operationGUID;
            }
            catch (Exception)
            {
                NHibernateSessionManager.Instance.RollbackTransactionOn();
                try
                {
                    SqlBulkCopy TempBulkInsert = new System.Data.SqlClient.SqlBulkCopy(NHibernateSessionManager.Instance.SessionFactoryPropsDic["connection.connection_string"], SqlBulkCopyOptions.Default);
                    TempBulkInsert.DestinationTableName = "dbo.TA_Temp";
                    TempBulkInsert.ColumnMappings.Add("ObjectID", "temp_ObjectID");
                    TempBulkInsert.ColumnMappings.Add("OperationGUID", "temp_OperationGUID");
                    TempBulkInsert.ColumnMappings.Add("CreationDate", "temp_CreationDate");
                    TempBulkInsert.WriteToServer(tempTable);
                }
                catch (Exception exe)
                {
                    throw exe;
                }
                return operationGUID;
            }
        }

        public void DeleteTempList(string operationGUID)
        {
            try
            {
                if (operationGUID != string.Empty)
                {
                    using (NHibernateSessionManager.Instance.BeginTransactionOn())
                    {
                        ISession NHSession = NHibernateSessionManager.Instance.GetSession();
                        NHSession.CreateSQLQuery("Delete From TA_Temp Where temp_OperationGUID = :operationGUID OR temp_CreationDate <= DATEADD(Hour, -2, GETDATE())")
                                 .SetParameter("operationGUID", operationGUID)
                                 .ExecuteUpdate();
                        NHibernateSessionManager.Instance.CommitTransactionOn();
                    }
                }
            }
            catch (Exception ex)
            {
                NHibernateSessionManager.Instance.RollbackTransactionOn();
                throw ex;
            }
        }
        public void OrganizeTemp()
        {
            try
            {
                string sqlCommand = string.Empty;
                ISession NHSession = NHibernateSessionManager.Instance.GetSession();

                sqlCommand = "TRUNCATE TABLE TA_Temp";
                NHSession.CreateSQLQuery(sqlCommand)
                         .ExecuteUpdate();

                sqlCommand = "DBCC CHECKIDENT('TA_Temp', RESEED)";
                NHSession.CreateSQLQuery(sqlCommand)
                         .ExecuteUpdate();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ClearChartTempImagesDirectory()
        {
            string ChartTempImagesDirectory = AppDomain.CurrentDomain.BaseDirectory + @"TempImages";
            if (Directory.Exists(ChartTempImagesDirectory))
            {
                string[] ChartTempImagesFiles = Directory.GetFiles(ChartTempImagesDirectory);
                foreach (string chartTempImagesFile in ChartTempImagesFiles)
                {
                    File.Delete(chartTempImagesFile);
                }
            }   
        }        
    }
}
