using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Model.Report
{
    public partial class SystemReportTypesDataContext : System.Data.Linq.DataContext, IEntity
    {

        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        partial void InsertSystemBusinessReport(SystemBusinessReport instance);
        partial void UpdateSystemBusinessReport(SystemBusinessReport instance);
        partial void DeleteSystemBusinessReport(SystemBusinessReport instance);
        partial void InsertSystemEngineReport(SystemEngineReport instance);
        partial void UpdateSystemEngineReport(SystemEngineReport instance);
        partial void DeleteSystemEngineReport(SystemEngineReport instance);
        partial void InsertSystemUserActionReport(SystemUserActionReport instance);
        partial void UpdateSystemUserActionReport(SystemUserActionReport instance);
        partial void DeleteSystemUserActionReport(SystemUserActionReport instance);
        partial void InsertSystemWindowsServiceReport(SystemWindowsServiceReport instance);
        partial void UpdateSystemWindowsServiceReport(SystemWindowsServiceReport instance);
        partial void DeleteSystemWindowsServiceReport(SystemWindowsServiceReport instance);
        partial void InsertSystemEngineDebugReport(SystemEngineDebugReport instance);
        partial void UpdateSystemEngineDebugReport(SystemEngineDebugReport instance);
        partial void DeleteSystemEngineDebugReport(SystemEngineDebugReport instance);
        partial void InsertSystemDataCollectorReport(SystemDataCollectorReport instance);
        partial void UpdateSystemDataCollectorReport(SystemDataCollectorReport instance);
        partial void DeleteSystemDataCollectorReport(SystemDataCollectorReport instance);
        #endregion

        //public SystemReportTypesDataContext() :
        //    base(ConfigurationManager.ConnectionStrings["log4netConnectionStr"].ConnectionString, mappingSource)
        //{
        //    OnCreated();
        //}

        public SystemReportTypesDataContext() :
            base(ConfigurationHelper.GetLogDBConnectionString(), mappingSource)
        {
            OnCreated();
        }


        public SystemReportTypesDataContext(string connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public SystemReportTypesDataContext(System.Data.IDbConnection connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public SystemReportTypesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public SystemReportTypesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public System.Data.Linq.Table<SystemBusinessReport> SystemBusinessReports
        {
            get
            {
                return this.GetTable<SystemBusinessReport>();
            }
        }

        public System.Data.Linq.Table<SystemEngineReport> SystemEngineReports
        {
            get
            {
                return this.GetTable<SystemEngineReport>();
            }
        }

        public System.Data.Linq.Table<SystemUserActionReport> SystemUserActionReports
        {
            get
            {
                return this.GetTable<SystemUserActionReport>();
            }
        }

        public System.Data.Linq.Table<SystemWindowsServiceReport> SystemWindowsServiceReports
        {
            get
            {
                return this.GetTable<SystemWindowsServiceReport>();
            }
        }

        public System.Data.Linq.Table<SystemEngineDebugReport> SystemEngineDebugReports
        {
            get
            {
                return this.GetTable<SystemEngineDebugReport>();
            }
        }
        public System.Data.Linq.Table<SystemDataCollectorReport> SystemDataCollectorReports
        {
            get
            {
                return this.GetTable<SystemDataCollectorReport>();
            }
        }


        public decimal ID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TA_Businesslog")]
    public partial class SystemBusinessReport : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private decimal id;
        private System.DateTime date;
        private string uiDate;
        private string uiTime;
        private string username;
        private string ipAddress;
        private string className;
        private string methodName;
        private string message;
        private string level;
        private string exception;
        private string exceptionSource;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIDChanging(decimal value);
        partial void OnIDChanged();
        partial void OnDateChanging(System.DateTime value);
        partial void OnDateChanged();
        partial void OnUsernameChanging(string value);
        partial void OnUsernameChanged();
        partial void OnClientIPAddressChanging(string value);
        partial void OnClientIPAddressChanged();
        partial void OnClassNameChanging(string value);
        partial void OnClassNameChanged();
        partial void OnMethodNameChanging(string value);
        partial void OnMethodNameChanged();
        partial void OnMessageChanging(string value);
        partial void OnMessageChanged();
        partial void OnLevelChanging(string value);
        partial void OnLevelChanged();
        partial void OnLoggerChanging(string value);
        partial void OnLoggerChanged();
        partial void OnExceptionChanging(string value);
        partial void OnExceptionChanged();
        partial void OnExceptionSourceChanging(string value);
        partial void OnExceptionSourceChanged();
        #endregion

        public SystemBusinessReport()
        {
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "ID", Storage = "id", AutoSync = AutoSync.OnInsert, DbType = "Decimal(18,0) NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]

        public decimal ID
        {
            get
            {
                return this.id;
            }
            set
            {
                if ((this.id != value))
                {
                    this.OnIDChanging(value);
                    this.SendPropertyChanging();
                    this.id = value;
                    this.SendPropertyChanged("ID");
                    this.OnIDChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "Date", Storage = "date", DbType = "DateTime NOT NULL")]
        public System.DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                if ((this.date != value))
                {
                    this.OnDateChanging(value);
                    this.SendPropertyChanging();
                    this.date = value;
                    this.SendPropertyChanged("Date");
                    this.OnDateChanged();
                }
            }
        }

        public string UIDate
        {
            get
            {
                return this.uiDate;
            }
            set
            {
                this.uiDate = value;
            }
        }
        public string UITime
        {
            get
            {
                return this.uiTime;
            }
            set
            {
                this.uiTime = value;
            }
        }
        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "Username", Storage = "username", DbType = "NVarChar(100)")]
        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                if ((this.username != value))
                {
                    this.OnUsernameChanging(value);
                    this.SendPropertyChanging();
                    this.username = value;
                    this.SendPropertyChanged("Username");
                    this.OnUsernameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "ClientIPAddress", Storage = "ipAddress", DbType = "NVarChar(100)")]
        public string IPAddress
        {
            get
            {
                return this.ipAddress;
            }
            set
            {
                if ((this.ipAddress != value))
                {
                    this.OnClientIPAddressChanging(value);
                    this.SendPropertyChanging();
                    this.ipAddress = value;
                    this.SendPropertyChanged("IPAddress");
                    this.OnClientIPAddressChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "ClassName", Storage = "className", DbType = "NVarChar(100)")]
        public string ClassName
        {
            get
            {
                return this.className;
            }
            set
            {
                if ((this.className != value))
                {
                    this.OnClassNameChanging(value);
                    this.SendPropertyChanging();
                    this.className = value;
                    this.SendPropertyChanged("ClassName");
                    this.OnClassNameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "MethodName", Storage = "methodName", DbType = "NVarChar(100)")]
        public string MethodName
        {
            get
            {
                return this.methodName;
            }
            set
            {
                if ((this.methodName != value))
                {
                    this.OnMethodNameChanging(value);
                    this.SendPropertyChanging();
                    this.methodName = value;
                    this.SendPropertyChanged("MethodName");
                    this.OnMethodNameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "Message", Storage = "message", DbType = "NText NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                if ((this.message != value))
                {
                    this.OnMessageChanging(value);
                    this.SendPropertyChanging();
                    this.message = value;
                    this.SendPropertyChanged("Message");
                    this.OnMessageChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Level]", Storage = "level", DbType = "VarChar(50)")]
        public string Level
        {
            get
            {
                return this.level;
            }
            set
            {
                if ((this.level != value))
                {
                    this.OnLevelChanging(value);
                    this.SendPropertyChanging();
                    this.level = value;
                    this.SendPropertyChanged("Level");
                    this.OnLevelChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "Exception", Storage = "exception", DbType = "VarChar(MAX)")]
        public string Exception
        {
            get
            {
                return this.exception;
            }
            set
            {
                if ((this.exception != value))
                {
                    this.OnExceptionChanging(value);
                    this.SendPropertyChanging();
                    this.exception = value;
                    this.SendPropertyChanged("Exception");
                    this.OnExceptionChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "ExceptionSource", Storage = "exceptionSource", DbType = "VarChar(200)")]
        public string ExceptionSource
        {
            get
            {
                return this.exceptionSource;
            }
            set
            {
                if ((this.exceptionSource != value))
                {
                    this.OnExceptionSourceChanging(value);
                    this.SendPropertyChanging();
                    this.exceptionSource = value;
                    this.SendPropertyChanged("ExceptionSource");
                    this.OnExceptionSourceChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TA_EngineLog")]
    public partial class SystemEngineReport : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int id;
        private string personBarcode;
        private System.DateTime date;
        private string uiDate;
        private string uiTime;
        private string level;
        private string message;
        private string exception;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnPersonBarcodeChanging(string value);
        partial void OnPersonBarcodeChanged();
        partial void OnDateChanging(System.DateTime value);
        partial void OnDateChanged();
        partial void OnThreadChanging(string value);
        partial void OnThreadChanged();
        partial void OnLevelChanging(string value);
        partial void OnLevelChanged();
        partial void OnLoggerChanging(string value);
        partial void OnLoggerChanged();
        partial void OnMessageChanging(string value);
        partial void OnMessageChanged();
        partial void OnExceptionChanging(string value);
        partial void OnExceptionChanged();
        #endregion

        public SystemEngineReport()
        {
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "Id", Storage = "id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                if ((this.id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this.id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "PersonBarcode", Storage = "personBarcode", DbType = "VarChar(50)")]
        public string PersonBarcode
        {
            get
            {
                return this.personBarcode;
            }
            set
            {
                if ((this.personBarcode != value))
                {
                    this.OnPersonBarcodeChanging(value);
                    this.SendPropertyChanging();
                    this.personBarcode = value;
                    this.SendPropertyChanged("PersonBarcode");
                    this.OnPersonBarcodeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "Date", Storage = "date", DbType = "DateTime NOT NULL")]
        public System.DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                if ((this.date != value))
                {
                    this.OnDateChanging(value);
                    this.SendPropertyChanging();
                    this.date = value;
                    this.SendPropertyChanged("Date");
                    this.OnDateChanged();
                }
            }
        }

        public string UIDate
        {
            get
            {
                return this.uiDate;
            }
            set
            {
                this.uiDate = value;
            }
        }
        public string UITime
        {
            get
            {
                return this.uiTime;
            }
            set
            {
                this.uiTime = value;
            }
        }
        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Level]", Storage = "level", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Level
        {
            get
            {
                return this.level;
            }
            set
            {
                if ((this.Level != value))
                {
                    this.OnLevelChanging(value);
                    this.SendPropertyChanging();
                    this.level = value;
                    this.SendPropertyChanged("Level");
                    this.OnLevelChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "Message", Storage = "message", DbType = "VarChar(4000) NOT NULL", CanBeNull = false)]
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                if ((this.message != value))
                {
                    this.OnMessageChanging(value);
                    this.SendPropertyChanging();
                    this.message = value;
                    this.SendPropertyChanged("Message");
                    this.OnMessageChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "Exception", Storage = "exception", DbType = "VarChar(2000)")]
        public string Exception
        {
            get
            {
                return this.exception;
            }
            set
            {
                if ((this.exception != value))
                {
                    this.OnExceptionChanging(value);
                    this.SendPropertyChanging();
                    this.exception = value;
                    this.SendPropertyChanged("Exception");
                    this.OnExceptionChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

     [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TA_EngineDebugLog")]
    public partial class SystemEngineDebugReport : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int id;
        private string personBarcode;
        private System.Nullable<System.DateTime> date;
        private string uiDate;
        private string uiTime;
        private string shamsiDate;
        private string ruleIden;
        private string cnpname;
        private string cnpIden;
        private string beforeValue;
        private string afterValue;
        private string message;
        private int ruleOrder;
        private System.Nullable<System.DateTime> miladiDate;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnPersonBarcodeChanging(string value);
        partial void OnPersonBarcodeChanged();
        partial void OnDateChanging(System.Nullable<System.DateTime> value);
        partial void OnDateChanged();
        partial void OnShamsiDateChanging(string value);
        partial void OnShamsiDateChanged();
        partial void OnRuleIdenChanging(string value);
        partial void OnRuleIdenChanged();
        partial void OnCnpIdenChanging(string value);
        partial void OnCnpIdenChanged();
        partial void OncnpNameChanging(string value);
        partial void OncnpNameChanged();
        partial void OnBeforeValueChanging(string value);
        partial void OnBeforeValueChanged();
        partial void OnAfterValueChanging(string value);
        partial void OnAfterValueChanged();
        partial void OnMessageChanging(string value);
        partial void OnMessageChanged();
        partial void OnRuleOrderChanging(int value);
        partial void OnRuleOrderChanged();
        partial void OnMiladiDateChanging(System.Nullable<System.DateTime> value);
        partial void OnMiladiDateChanged();

        #endregion

        public SystemEngineDebugReport()
        {
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "Id", Storage = "id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                if ((this.id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this.id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "PersonBarcode", Storage = "personBarcode", DbType = "nVarChar(50)")]
        public string PersonBarcode
        {
            get
            {
                return this.personBarcode;
            }
            set
            {
                if ((this.personBarcode != value))
                {
                    this.OnPersonBarcodeChanging(value);
                    this.SendPropertyChanging();
                    this.personBarcode = value;
                    this.SendPropertyChanged("PersonBarcode");
                    this.OnPersonBarcodeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "Date", Storage = "date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Date
        {
            get
            {
                return this.date;
            }
            set
            {
                if ((this.date != value))
                {
                    this.OnDateChanging(value);
                    this.SendPropertyChanging();
                    this.date = value;
                    this.SendPropertyChanged("Date");
                    this.OnDateChanged();
                }
            }
        }

        public string UIDate
        {
            get
            {
                return this.uiDate;
            }
            set
            {
                this.uiDate = value;
            }
        }
        public string UITime
        {
            get
            {
                return this.uiTime;
            }
            set
            {
                this.uiTime = value;
            }
        }
        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "ShamsiDate", Storage = "shamsiDate", DbType = "nVarChar(50)")]
        public string ShamsiDate
        {
            get
            {
                return this.shamsiDate;
            }
            set
            {
                if ((this.shamsiDate != value))
                {
                    this.OnShamsiDateChanging(value);
                    this.SendPropertyChanging();
                    this.shamsiDate = value;
                    this.SendPropertyChanged("ShamsiDate");
                    this.OnShamsiDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "RuleIden", Storage = "ruleIden", DbType = "nVarChar(50)")]
        public string RuleIden
        {
            get
            {
                return this.ruleIden;
            }
            set
            {
                if ((this.ruleIden != value))
                {
                    this.OnRuleIdenChanging(value);
                    this.SendPropertyChanging();
                    this.ruleIden = value;
                    this.SendPropertyChanged("RuleIden");
                    this.OnRuleIdenChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "cnpName", Storage = "cnpname", DbType = "nVarChar(max)")]
        public string cnpName
        {
            get
            {
                return this.cnpname;
            }
            set
            {
                if ((this.cnpname != value))
                {
                    this.OncnpNameChanging(value);
                    this.SendPropertyChanging();
                    this.cnpname = value;
                    this.SendPropertyChanged("cnpName");
                    this.OncnpNameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "CnpIden", Storage = "cnpIden", DbType = "nVarChar(50)")]
        public string CnpIden
        {
            get
            {
                return this.cnpIden;
            }
            set
            {
                if ((this.cnpIden != value))
                {
                    this.OnCnpIdenChanging(value);
                    this.SendPropertyChanging();
                    this.cnpIden = value;
                    this.SendPropertyChanged("CnpIden");
                    this.OnCnpIdenChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "AfterValue", Storage = "afterValue", DbType = "nVarChar(max)")]
        public string AfterValue
        {
            get
            {
                return this.afterValue;
            }
            set
            {
                if ((this.afterValue != value))
                {
                    this.OnAfterValueChanging(value);
                    this.SendPropertyChanging();
                    this.afterValue = value;
                    this.SendPropertyChanged("AfterValue");
                    this.OnAfterValueChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "BeforeValue", Storage = "beforeValue", DbType = "nVarChar(max)")]
        public string BeforeValue
        {
            get
            {
                return this.beforeValue;
            }
            set
            {
                if ((this.beforeValue != value))
                {
                    this.OnBeforeValueChanging(value);
                    this.SendPropertyChanging();
                    this.beforeValue = value;
                    this.SendPropertyChanged("beforeValue");
                    this.OnBeforeValueChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "Message", Storage = "message", DbType = "nVarChar(max)")]
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                if ((this.message != value))
                {
                    this.OnMessageChanging(value);
                    this.SendPropertyChanging();
                    this.message = value;
                    this.SendPropertyChanged("Message");
                    this.OnMessageChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "RuleOrder", Storage = "ruleOrder", DbType = "Int")]
        public int RuleOrder
        {
            get
            {
                return this.ruleOrder;
            }
            set
            {
                if ((this.ruleOrder != value))
                {
                    this.OnRuleOrderChanging(value);
                    this.SendPropertyChanging();
                    this.ruleOrder = value;
                    this.SendPropertyChanged("RuleOrder");
                    this.OnRuleOrderChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "MiladiDate", Storage = "miladiDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> MiladiDate
        {
            get
            {
                return this.miladiDate;
            }
            set
            {
                if ((this.miladiDate != value))
                {
                    this.OnMiladiDateChanging(value);
                    this.SendPropertyChanging();
                    this.miladiDate = value;
                    this.SendPropertyChanged("MiladiDate");
                    this.OnMiladiDateChanged();
                }
            }
        }


       

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TA_UserActionLog")]
    public partial class SystemUserActionReport : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private decimal id;
        private System.Nullable<System.DateTime> date;
        private string uiDate;
        private string uiTime;
        private string username;
        private string ipAddress;
        private string pageId;
        private string className;
        private string methodName;
        private string action;
        private string objectInformation;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIDChanging(decimal value);
        partial void OnIDChanged();
        partial void OnDateChanging(System.Nullable<System.DateTime> value);
        partial void OnDateChanged();
        partial void OnUsernameChanging(string value);
        partial void OnUsernameChanged();
        partial void OnClientIPAddressChanging(string value);
        partial void OnClientIPAddressChanged();
        partial void OnPageIdChanging(string value);
        partial void OnPageIdChanged();
        partial void OnClassNameChanging(string value);
        partial void OnClassNameChanged();
        partial void OnMethodNameChanging(string value);
        partial void OnMethodNameChanged();
        partial void OnActionChanging(string value);
        partial void OnActionChanged();
        partial void OnObjectInfoChanging(string value);
        partial void OnObjectInfoChanged();
        #endregion

        public SystemUserActionReport()
        {
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "ID", Storage = "id", AutoSync = AutoSync.OnInsert, DbType = "Decimal(18,0) NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]

        public decimal ID
        {
            get
            {
                return this.id;
            }
            set
            {
                if ((this.id != value))
                {
                    this.OnIDChanging(value);
                    this.SendPropertyChanging();
                    this.id = value;
                    this.SendPropertyChanged("ID");
                    this.OnIDChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "Date", Storage = "date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Date
        {
            get
            {
                return this.date;
            }
            set
            {
                if ((this.date != value))
                {
                    this.OnDateChanging(value);
                    this.SendPropertyChanging();
                    this.date = value;
                    this.SendPropertyChanged("Date");
                    this.OnDateChanged();
                }
            }
        }

        public string UIDate
        {
            get
            {
                return this.uiDate;
            }
            set
            {
                this.uiDate = value;
            }
        }
        public string UITime
        {
            get
            {
                return this.uiTime;
            }
            set
            {
                this.uiTime = value;
            }
        }
        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "Username", Storage = "username", DbType = "NVarChar(100)")]
        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                if ((this.username != value))
                {
                    this.OnUsernameChanging(value);
                    this.SendPropertyChanging();
                    this.username = value;
                    this.SendPropertyChanged("Username");
                    this.OnUsernameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "ClientIPAddress", Storage = "ipAddress", DbType = "NVarChar(100)")]
        public string IPAddress
        {
            get
            {
                return this.ipAddress;
            }
            set
            {
                if ((this.ipAddress != value))
                {
                    this.OnClientIPAddressChanging(value);
                    this.SendPropertyChanging();
                    this.ipAddress = value;
                    this.SendPropertyChanged("IPAddress");
                    this.OnClientIPAddressChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "PageId", Storage = "pageId", DbType = "NVarChar(100)")]
        public string PageID
        {
            get
            {
                return this.pageId;
            }
            set
            {
                if ((this.pageId != value))
                {
                    this.OnPageIdChanging(value);
                    this.SendPropertyChanging();
                    this.pageId = value;
                    this.SendPropertyChanged("PageID");
                    this.OnPageIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "ClassName", Storage = "className", DbType = "NVarChar(100)")]
        public string ClassName
        {
            get
            {
                return this.className;
            }
            set
            {
                if ((this.className != value))
                {
                    this.OnClassNameChanging(value);
                    this.SendPropertyChanging();
                    this.className = value;
                    this.SendPropertyChanged("ClassName");
                    this.OnClassNameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "MethodName", Storage = "methodName", DbType = "NVarChar(100)")]
        public string MethodName
        {
            get
            {
                return this.methodName;
            }
            set
            {
                if ((this.methodName != value))
                {
                    this.OnMethodNameChanging(value);
                    this.SendPropertyChanging();
                    this.methodName = value;
                    this.SendPropertyChanged("MethodName");
                    this.OnMethodNameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "Action", Storage = "action", DbType = "NVarChar(100)")]
        public string Action
        {
            get
            {
                return this.action;
            }
            set
            {
                if ((this.action != value))
                {
                    this.OnActionChanging(value);
                    this.SendPropertyChanging();
                    this.action = value;
                    this.SendPropertyChanged("Action");
                    this.OnActionChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "ObjectInfo", Storage = "objectInformation", DbType = "NVarChar(MAX)")]
        public string ObjectInformation
        {
            get
            {
                return this.objectInformation;
            }
            set
            {
                if ((this.objectInformation != value))
                {
                    this.OnObjectInfoChanging(value);
                    this.SendPropertyChanging();
                    this.objectInformation = value;
                    this.SendPropertyChanged("ObjectInformation");
                    this.OnObjectInfoChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TA_WinSvcLog")]
    public partial class SystemWindowsServiceReport : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private decimal id;
        private System.DateTime date;
        private string uiDate;
        private string uiTime;
        private string level;
        private string messag;
        private string exception;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnWinSvcLog_IDChanging(decimal value);
        partial void OnWinSvcLog_IDChanged();
        partial void OnWinSvcLog_DateChanging(System.DateTime value);
        partial void OnWinSvcLog_DateChanged();
        partial void OnWinSvcLog_ThreadChanging(string value);
        partial void OnWinSvcLog_ThreadChanged();
        partial void OnWinSvcLog_LevelChanging(string value);
        partial void OnWinSvcLog_LevelChanged();
        partial void OnWinSvcLog_LoggerChanging(string value);
        partial void OnWinSvcLog_LoggerChanged();
        partial void OnWinSvcLog_MessageChanging(string value);
        partial void OnWinSvcLog_MessageChanged();
        partial void OnWinSvcLog_ExceptionChanging(string value);
        partial void OnWinSvcLog_ExceptionChanged();
        #endregion

        public SystemWindowsServiceReport()
        {
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "WinSvcLog_ID", Storage = "id", AutoSync = AutoSync.OnInsert, DbType = "Decimal(18,0) NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]

        public decimal ID
        {
            get
            {
                return this.id;
            }
            set
            {
                if ((this.id != value))
                {
                    this.OnWinSvcLog_IDChanging(value);
                    this.SendPropertyChanging();
                    this.id = value;
                    this.SendPropertyChanged("ID");
                    this.OnWinSvcLog_IDChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "WinSvcLog_Date", Storage = "date", DbType = "DateTime NOT NULL")]
        public System.DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                if ((this.date != value))
                {
                    this.OnWinSvcLog_DateChanging(value);
                    this.SendPropertyChanging();
                    this.date = value;
                    this.SendPropertyChanged("Date");
                    this.OnWinSvcLog_DateChanged();
                }
            }
        }

        public string UIDate
        {
            get
            {
                return this.uiDate;
            }
            set
            {
                this.uiDate = value;
            }
        }
        public string UITime
        {
            get
            {
                return this.uiTime;
            }
            set
            {
                this.uiTime = value;
            }
        }
        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "WinSvcLog_Level", Storage = "level", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Level
        {
            get
            {
                return this.level;
            }
            set
            {
                if ((this.level != value))
                {
                    this.OnWinSvcLog_LevelChanging(value);
                    this.SendPropertyChanging();
                    this.level = value;
                    this.SendPropertyChanged("Level");
                    this.OnWinSvcLog_LevelChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "WinSvcLog_Message", Storage = "messag",  DbType = "VarChar(4000) NOT NULL", CanBeNull = false)]
        public string Message
        {
            get
            {
                return this.messag;
            }
            set
            {
                if ((this.messag != value))
                {
                    this.OnWinSvcLog_MessageChanging(value);
                    this.SendPropertyChanging();
                    this.messag = value;
                    this.SendPropertyChanged("Message");
                    this.OnWinSvcLog_MessageChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "WinSvcLog_Exception", Storage = "exception", DbType = "VarChar(2000)")]
        public string Exception
        {
            get
            {
                return this.exception;
            }
            set
            {
                if ((this.exception != value))
                {
                    this.OnWinSvcLog_ExceptionChanging(value);
                    this.SendPropertyChanging();
                    this.exception = value;
                    this.SendPropertyChanged("Exception");
                    this.OnWinSvcLog_ExceptionChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TA_DataCollectorLog")]
    public partial class SystemDataCollectorReport : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private decimal id;
        private string personBarcode;
        private System.Nullable<System.DateTime> trafficDateTime;
        private string trafficDate;
        private string trafficTime;
        private System.Nullable<System.DateTime> recieveDateTime;
        private string  recieveDate;
        private string  recieveTime;
        private string deviceID;
        private string status;
        private string message;
        

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(decimal value);
        partial void OnIdChanged();
        partial void OnPersonBarcodeChanging(string value);
        partial void OnPersonBarcodeChanged();
        partial void OnTrafficDateTimeChanging(System.Nullable<System.DateTime> value);
        partial void OnTrafficDateTimeChanged();
        partial void OnRecieveDateTimeChanging(System.Nullable<System.DateTime> value);
        partial void OnRecieveDateTimeChanged();
        partial void OnDeviceIDChanging(string value);
        partial void OnDeviceIDChanged();
        partial void OnStatusChanging(string value);
        partial void OnStatusChanged();
        partial void OnMessageChanging(string value);
        partial void OnMessageChanged();
       

        #endregion

        public SystemDataCollectorReport()
        {
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "Id", Storage = "id", AutoSync = AutoSync.OnInsert, DbType = "Decimal(18,0) NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public decimal Id
        {
            get
            {
                return this.id;
            }
            set
            {
                if ((this.id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this.id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "PersonBarcode", Storage = "personBarcode", DbType = "VarChar(50) NOT NULL")]
        public string PersonBarcode
        {
            get
            {
                return this.personBarcode;
            }
            set
            {
                if ((this.personBarcode != value))
                {
                    this.OnPersonBarcodeChanging(value);
                    this.SendPropertyChanging();
                    this.personBarcode = value;
                    this.SendPropertyChanged("PersonBarcode");
                    this.OnPersonBarcodeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "TrafficDateTime", Storage = "trafficDateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> TrafficDateTime
        {
            get
            {
                return this.trafficDateTime;
            }
            set
            {
                if ((this.trafficDateTime != value))
                {
                    this.OnTrafficDateTimeChanging(value);
                    this.SendPropertyChanging();
                    this.trafficDateTime = value;
                    this.SendPropertyChanged("TrafficDateTime");
                    this.OnTrafficDateTimeChanged();
                }
            }
        }

        public string TrafficDate
        {
            get
            {
                return this.trafficDate;
            }
            set
            {
                this.trafficDate = value;
            }
        }

        public string TrafficTime
        {
            get
            {
                return this.trafficTime;
            }
            set
            {
                this.trafficTime = value;
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "RecieveDateTime", Storage = "recieveDateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> RecieveDateTime
        {
            get
            {
                return this.recieveDateTime;
            }
            set
            {
                if ((this.recieveDateTime != value))
                {
                    this.OnRecieveDateTimeChanging(value);
                    this.SendPropertyChanging();
                    this.recieveDateTime = value;
                    this.SendPropertyChanged("RecieveDateTime");
                    this.OnRecieveDateTimeChanged();
                }
            }
        }

        public string RecieveDate
        {
            get
            {
                return this.recieveDate;
            }
            set
            {
                this.recieveDate = value;
            }
        }

        public string RecieveTime
        {
            get
            {
                return this.recieveTime;
            }
            set
            {
                this.recieveTime = value;
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "DeviceID", Storage = "deviceID", DbType = "nVarchar(50) Not Null")]
        public string DeviceID
        {
            get
            {
                return this.deviceID;
            }
            set
            {
                if ((this.deviceID != value))
                {
                    this.OnDeviceIDChanging(value);
                    this.SendPropertyChanging();
                    this.deviceID = value;
                    this.SendPropertyChanged("DeviceID");
                    this.OnDeviceIDChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "Status", Storage = "status", DbType = "nVarChar(50)")]
        public string Status
        {
            get
            {
                return this.status;
            }
            set
            {
                if ((this.status != value))
                {
                    this.OnStatusChanging(value);
                    this.SendPropertyChanging();
                    this.status = value;
                    this.SendPropertyChanged("Status");
                    this.OnStatusChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "Message", Storage = "message", DbType = "nText")]
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                if ((this.message != value))
                {
                    this.OnMessageChanging(value);
                    this.SendPropertyChanging();
                    this.Message = value;
                    this.SendPropertyChanged("Message");
                    this.OnMessageChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

