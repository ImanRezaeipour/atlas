using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure
{

    public struct LeaveInfo
    {
        public int Day;
        public int Minute;
    }


    public enum LanguagesName
    {
        Unknown = 0, Parsi = 1, English = 2
    }

    public enum SessionWorkSpace
    {
        WEB, WinService
    }

    /// <summary>
    /// جهت استخراج مفاهیم ساعتی
    /// </summary>
    public enum ConceptsKeys
    {
        /// <summary>
        /// اضافه کاری مجاز
        /// </summary>
        gridFields_AllowableOverTime,
        /// <summary>
        /// ماموریت شبانه روزی
        /// </summary>
        gridFields_HostelryMission,
        /// <summary>
        /// مرخصی ساعتی استحقاقی
        /// </summary>
        gridFields_HourlyMeritoriouslyLeave,
        /// <summary>
        /// ماموریت ساعتی
        /// </summary>
        gridFields_HourlyMission,
        /// <summary>
        /// کارکرد خالص ساعتی
        /// </summary>
        gridFields_HourlyPureOperation,
        /// <summary>
        /// ساعتی مرخصی استعلاجی
        /// </summary>
        gridFields_HourlySickLeave,
        /// <summary>
        ///  غیبت غیر مجاز ساعتی
        /// </summary>
        gridFields_HourlyUnallowableAbsence,
        /// <summary>
        /// مرخصی ساعتی بدون حقوق
        /// </summary>
        gridFields_HourlyWithoutPayLeave,
        /// <summary>
        /// مرخصی ساعتی با حقوق
        /// </summary>
        gridFields_HourlyWithPayLeave,
        /// <summary>
        ///  مدت زمان حضور
        /// </summary>
        gridFields_PresenceDuration,
        /// <summary>
        /// اضافه کاری غیر مجاز
        /// </summary>
        gridFields_UnallowableOverTime,
        /// <summary>
        /// غیبت ساعتی تبدیل شده به غیبت روزانه
        /// </summary>
        gridFields_HourlyUnallowableAbsenceToDaily
    }

    public enum PrecardGroupsName
    {
        leave = 1, duty = 2, overwork = 3, traffic = 4, leaveestelajy = 5, imperative = 6, terminate = 7, None = 0
    }

    /// <summary>
    /// تایید شده , ردشده,تحت بررسی
    /// </summary>
    public enum RequestState
    {
        Confirmed = 1, Unconfirmed = 2, UnderReview = 3, Deleted = 4, Terminated = 5, UnKnown = 0
    }

    /// <summary>
    /// روزانه, ساعتی,اضافه کار
    /// </summary>
    public enum OfficeRequestType
    {
        None = 0, NotStarted = 1, InProcess = 2, Completed = 3
    }

    public enum RequestType
    {
        None, Hourly, Daily, Monthly, OverWork, Imperative, Terminate
    }

    public enum RequestSource { Undermanagment, Substitute }

    public enum UserSearchKeys
    {
        PersonCode = 1, Name = 2, Username = 3, RoleName = 4, NotSpecified = 5
    }

    public enum HashStandards
    {
        None, MD5, SHA1, SHA256, SHA384, SHA512
    }

    public enum MembershipProviders
    {
        ADMembershipProvider, GTSMembershipProvider
    }

    public enum MarriageStatus
    {
        Married, Single
    };

    #region Person Advance Search Paramerters

    public enum PersonSex
    {
        Male = 0, Female = 1
    }

    public enum MaritalStatus
    {
        Mojarad = 1, Motahel = 2, Motaleghe = 3
    }

    public enum MilitaryStatus
    {
        GheireMashmool = 1, AmadeBeKhedmat = 2, HeineKhedmat = 3, DarayeKartePayanKhedmat = 4,
        MoafiatTahsili = 5, MoafiatTakafol = 6, MoafiatPezeshki = 7, Sayer = 8
    }

    /// <summary>
    /// پارمترهای جستجوی پیشرفته
    /// جهت ارسال به انباره داده
    /// </summary>
    public class PersonSearchProxy
    {
        public PersonSearchProxy()
        {
            Sex = null;
            Military = null;
            Education = null;
            MaritalStatus = null;
            DepartmentId = null;
            WorkGroupId = null;
            WorkGroupFromDate = null;
            RuleGroupId = null;
            RuleGroupFromDate = null;
            RuleGroupToDate = null;
            CalculationDateRangeId = null;
            CalculationFromDate = null;
            ControlStationId = null;
            EmploymentType = null;
            PersonId = null;
            SearchInCategory = PersonCategory.Public;
            GradeId = null;
            CostCenterId = null;
            DepartmentListId = null;
            EmploymentTypeListId = null;
            ControlStationListId = null;
            IntegratedSearchTerm = null;
            UIValidationGroupListId = null;
        }

        /// <summary>
        /// اگر مقداردهی شود تنها همین شخص جستجو میشود
        /// </summary>
        public decimal? PersonId { get; set; }

        /// <summary>
        /// جنسیت
        /// </summary>
        public PersonSex? Sex { get; set; }

        /// <summary>
        /// نظام وظیفه
        /// </summary>
        public MilitaryStatus? Military { get; set; }

        /// <summary>
        /// تحصیلات
        /// </summary>
        public String Education { get; set; }

        /// <summary>
        /// وضعیت تاهل
        /// </summary>
        public MaritalStatus? MaritalStatus { get; set; }

        /// <summary>
        /// کد بخش
        /// </summary>
        public decimal? DepartmentId { get; set; }
        public List<decimal> DepartmentListId { get; set; }
        public List<decimal> EmploymentTypeListId { get; set; }

        public List<decimal> UIValidationGroupListId { get; set; }
        public List<decimal> ControlStationListId { get; set; }
        /// <summary>
        /// شامل زیر بخش ها هم بشود
        /// </summary>
        public bool IncludeSubDepartments { get; set; }

        /// <summary>
        /// کد گروه کاری
        /// </summary>
        public decimal? WorkGroupId { get; set; }

        /// <summary>
        /// تاریخ شروع انتساب گروه کاری
        /// </summary>
        public String WorkGroupFromDate { get; set; }

        /// <summary>
        /// کد گروه قوانین
        /// </summary>
        public decimal? RuleGroupId { get; set; }

        /// <summary>
        /// تاریخ شروع انتساب گروه قوانین
        /// </summary>
        public String RuleGroupFromDate { get; set; }

        /// <summary>
        /// تاریخ انتهای انتساب گروه قوانین
        /// </summary>
        public String RuleGroupToDate { get; set; }
        public decimal? ContractId { get; set; }

        /// <summary>
        /// تاریخ شروع انتساب گروه قوانین
        /// </summary>
        public String ContractFromDate { get; set; }

        /// <summary>
        /// تاریخ انتهای انتساب گروه قوانین
        /// </summary>
        public String ContractToDate { get; set; }
        /// <summary>
        /// کد دوره محاسبات
        /// </summary>
        public decimal? CalculationDateRangeId { get; set; }

        /// <summary>
        /// تاریخ شروع دوره محاسبات
        /// </summary>
        public String CalculationFromDate { get; set; }

        /// <summary>
        /// ایستگاه کنترل
        /// </summary>
        public decimal? ControlStationId { get; set; }
        public decimal? GradeId { get; set; }
        public decimal? CostCenterId { get; set; }
        /// <summary>
        /// نوع استخدام
        /// </summary>
        public decimal? EmploymentType
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal OrganizationUnitId { get; set; }

        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public String FromBirthDate { get; set; }

        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public String ToBirthDate { get; set; }

        /// <summary>
        /// تاریخ استخدام
        /// </summary>
        public String FromEmploymentDate { get; set; }

        /// <summary>
        /// تاریخ استخدام
        /// </summary>
        public String ToEmploymentDate { get; set; }

        /// <summary>
        /// شماره استخدامی
        /// </summary>
        public String EmployeeNumber { get; set; }

        /// <summary>
        /// محل تولد
        /// </summary>
        public String BirthPlace { get; set; }

        /// <summary>
        /// شماره کارت
        /// </summary>
        public String CartNumber { get; set; }

        /// <summary>
        /// نام
        /// </summary>
        public String FirstName { get; set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public String LastName { get; set; }

        /// <summary>
        /// نام پدر
        /// </summary>
        public String FatherName { get; set; }

        /// <summary>
        /// شماره پرسنلی
        /// </summary>
        public String PersonCode { get; set; }

        /// <summary>
        /// کد ملی
        /// </summary>
        public String MelliCode { get; set; }

        public bool? PersonActivateState { get; set; }

        public PersonCategory SearchInCategory { get; set; }

        public IList<decimal> PersonIdList { get; set; }

        public string IntegratedSearchTerm { get; set; }
        public bool? PersonIsDeleted { get; set; }
        public decimal UiValidationGroupID { get; set; }
    }

    /// <summary>
    /// پارمترهای جستجوی پیشرفته
    /// جهت ارسال به انباره داده
    /// </summary>
    public class PersonCLSearchProxy
    {
        public PersonCLSearchProxy()
        {
            Sex = null;
            Military = null;
            Education = null;
            MaritalStatus = null;
            DepartmentId = null;
            DepartmentPositionId = null;
            ControlStationId = null;
            EmploymentType = null;
            PersonId = null;
            SearchInCategory = PersonCategory.Public;
        }

        /// <summary>
        /// اگر مقداردهی شود تنها همین شخص جستجو میشود
        /// </summary>
        public decimal? PersonId { get; set; }

        /// <summary>
        /// جنسیت
        /// </summary>
        public PersonSex? Sex { get; set; }

        /// <summary>
        /// نظام وظیفه
        /// </summary>
        public MilitaryStatus? Military { get; set; }

        /// <summary>
        /// تحصیلات
        /// </summary>
        public String Education { get; set; }

        /// <summary>
        /// وضعیت تاهل
        /// </summary>
        public MaritalStatus? MaritalStatus { get; set; }

        /// <summary>
        /// کد بخش
        /// </summary>
        public decimal? DepartmentId { get; set; }

        public decimal? DepartmentPositionId { get; set; }

        /// <summary>
        /// شامل زیر بخش ها هم بشود
        /// </summary>
        public bool IncludeSubDepartments { get; set; }

        /// <summary>
        /// ایستگاه کنترل
        /// </summary>
        public decimal? ControlStationId { get; set; }

        /// <summary>
        /// نوع استخدام
        /// </summary>
        public decimal? EmploymentType
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal OrganizationUnitId { get; set; }

        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public String FromBirthDate { get; set; }

        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public String ToBirthDate { get; set; }

        /// <summary>
        /// تاریخ استخدام
        /// </summary>
        public String FromEmploymentDate { get; set; }

        /// <summary>
        /// تاریخ استخدام
        /// </summary>
        public String ToEmploymentDate { get; set; }

        /// <summary>
        /// شماره استخدامی
        /// </summary>
        public String EmployeeNumber { get; set; }

        /// <summary>
        /// محل تولد
        /// </summary>
        public String BirthPlace { get; set; }

        /// <summary>
        /// شماره کارت
        /// </summary>
        public String CartNumber { get; set; }

        /// <summary>
        /// نام
        /// </summary>
        public String FirstName { get; set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public String LastName { get; set; }

        /// <summary>
        /// نام پدر
        /// </summary>
        public String FatherName { get; set; }

        /// <summary>
        /// شماره پرسنلی
        /// </summary>
        public String PersonCode { get; set; }

        /// <summary>
        /// کد ملی
        /// </summary>
        public String MelliCode { get; set; }

        public bool? PersonActivateState { get; set; }

        public PersonCategory SearchInCategory { get; set; }

        public IList<decimal> PersonIdList { get; set; }
    }

    /// <summary>
    /// جهت ارسال ایمیل 
    /// </summary>
    public class InfoServiceProxy
    {
        public decimal PersonId { get; set; }

        public PersonSex Sex { get; set; }

        public string PersonName { get; set; }
        public string PersonCode { get; set; }

        public string EmailAddress { get; set; }

        public string SmsNumber { get; set; }

        public Boolean SendByDay { get; set; }

        public TimeSpan RepeatePeriod { get; set; }

    }

    #endregion

    /// <summary>
    /// public:Athorized Person By Department Access
    /// Admin:
    /// Operator:
    /// Manager:
    /// Operator_Manager_UnderManagment:
    /// Sentry_UnderManagment:
    /// </summary>
    public enum PersonCategory
    {
        Public = 1, Manager_UnderManagment = 2, Operator_UnderManagment = 3, Manager = 4, Sentry_UnderManagment = 5, Substitute_UnderManagment = 6, SubstitudeManager = 7
    }

    public enum ReportParametersActionId
    {
        PersonDateRange,
        ToDate_Implicit_StartOfYear_EndOfYear
    }

    public enum KartablSummaryItems
    {
        MainRecievedRequestCount, SubstituteRecievedRequestCount,
        ConfirmedRequestCount, NotConfirmedRequestCount,
        InFlowRequestCount, PrivateMessageCount, UnderReviewRequestSubstituteRequestsCount, UnKnown
    }

    public enum KartablOrderBy
    {
        RequestDate, PersonCode, PersonName, RegisteredBy, RequestSubject, None
    }

    public enum SentryPermitsOrderBy
    {
        PersonCode, PersonName, PermitSubject
    }

    public enum CalanderTypeItems
    {
        Rasmi = 1, GheireRasmi = 2,
        NoRooz = 3
    }

    public enum RequestSubmiter { USER, OPERATOR }

    public enum BudgetType { Usual = 1, PerMonth = 2 }

    public enum LeaveIncDecAction { Increase, Decrease }

    public enum DataAccessParts
    {
        Department, OrganizationUnit, Shift, WorkGroup, Precard,
        ControlStation, Doctor, Manager, RuleGroup, Flow, Report, Corporation, EmploymentType, Role, CostCenter
    }

    public enum ClienteleDataAccessParts
    {
        Department, OrganizationUnit, OfficeType,
        ControlStation, Manager, Flow, Report, Corporation
    }


    public enum DataAccessLevelsType { Source, Target }

    public enum ScndCnpCustomeCategoryCode
    {
        Work = 1,
        Leave = 2,
        Mission = 3,
        Absence = 4,
        OverTime = 5,
    }

    /// <summary>
    /// زمان اجرای مفهوم
    /// </summary>
    public enum ScndCnpCalcSituationType
    {
        /// <summary>
        /// هر روز اجرا شود
        /// </summary>
        EveryDay = 0,
        /// <summary>
        /// ابتدای دوره اجرا شود
        /// </summary>
        BeginOfPeriode = 1,
        /// <summary>
        /// انتهای دوره اجرا شود
        /// </summary>
        EndOfPeriode = 2
    }
    /// <summary>
    /// نوع مفهوم
    /// </summary>
    public enum ScndCnpPairableType
    {
        /// <summary>
        /// PairableSecondaryConcept
        /// </summary>
        PSC = 0,
        /// <summary>
        /// NonPairableSecondaryConcept
        /// </summary>
        NPSC = 1
    }

    public enum ScndCnpPeriodicType
    {
        NoPeriodic = 0,
        /// <summary>
        /// مفهوم دوره ای که از جمع مفاهیم روزانه بدست می آید
        /// </summary>
        Periodic = 1,
        /// <summary>
    }

    /// <summary>
    /// مقدار مفهوم در دیتابیس ذخیره شود یا خیر
    /// </summary>
    public enum ScndCnpPersistSituationType
    {
        /// <summary>
        /// اگر مقدار مفهوم بزرگتر از صفر بود ذخیره نشود 
        /// </summary>
        Persistable = 1,
        /// <summary>
        /// در هر صورت ذخیره نشود
        /// </summary>
        NotPersist = 2,
        /// <summary>
        /// در هر صورت ذخیره کن
        /// </summary>
        AlwaysPersist = 3
    }

    public enum NotificationsServices
    {
        EMAILRequestStatus = 1, EMAILTrafficItems = 2, EMAILKartabl = 3,
        SmsRequestStatus = 4, SmsTrafficItems = 5, SmsKartabl = 6
    }

    public enum PersonReservedFieldsType
    {
        TextValue = 1, ComboValue = 2
    }

    public enum PersonReservedFieldComboItems
    {
        R16, R17, R18, R19, R20
    }

    public enum ConceptReservedFields
    {
        ReserveField1, ReserveField2, ReserveField3, ReserveField4, ReserveField5,
        ReserveField6, ReserveField7, ReserveField8, ReserveField9, ReserveField10
    }

    public enum RuleParametersValidationType
    {
        RuleParametersNoRegulation,
        RuleParametersDateRangesNoCover
    }

    public enum TrafficTransferType
    {
        Backward,
        Forward
    }

    public enum TrafficTransferMode
    {
        Normal,
        RecordBase,
        IdentifierBase
    }

    public enum ServiceAuthorizeType
    {
        Legal,
        Illegal
    }

    public enum ManagerCreator
    {
        Personnel,
        OrganizationPost,
        None
    }

    public enum SystemReportType
    {
        SystemBusinessReport,
        SystemEngineReport,
        SystemWindowsServiceReport,
        SystemUserActionReport,
        SystemEngineDebugReport,
        SystemDataCollectorReport
    }

    public enum DataAccessLevelOperationType
    {
        Single,
        Group
    }

    public class SystemReportTypeFilterConditions
    {
        public string SearchTerm { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Concept { get; set; }
        public string RuleCode { get; set; }
    }

    public enum AttachmentType
    {
        Request,
        Personnel,
        Authorization,
        ReferredPerson,
        Office,
        DeliveryItem
    }

    public enum ImperativeRequestLoadState
    {
        Normal,
        Applied,
        NotApplied
    }

    public class Range
    {
        public Range()
        {

        }
        //public Range(DateTime from,DateTime to) 
        //{
        //    From = from;
        //    To = to;
        //}
        public Range(DateTime from, DateTime to, decimal aditionalField)
        {
            From = from;
            To = to;
            AditionalField = aditionalField;
        }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal AditionalField { get; set; }
    }

    public enum ArchiveExistsConditions
    {
        NotExists = 0, SomeExists = 1, AllExists = 2
    }

    public enum ConceptDataType
    {
        Int = 0, Hour = 1
    }

    public enum RoleCustomCodeType
    {
        SystemTechnicalAdmin = 1, SystemAdmin = 2, Manager = 3, Substitute = 4, Operator = 5, User = 6
    }

    public enum SubSystemIdentifier
    {
        TimeAtendance = 1, Clientele = 2
    }


    /// <summary>
    /// حالت درخواست آفیش روز
    /// </summary>
    public enum RequestOffishStateInDay
    {
        /// <summary>
        /// همه
        /// </summary>
        All,
        /// <summary>
        /// بسته
        /// </summary>
        Closed,
        /// <summary>
        /// در فرآیند
        /// </summary>
        InProcces,
    }

    /// <summary>
    /// بمنظور کلاس بندی رکوردهای استخراج شده از 
    /// دیتابیس بمنظور بررسی سطح دسترسی کاربر استفاده میشود
    /// </summary>
    public class UserAuthorizationProxy
    {
        public string Username { get; set; }
        public string Description { get; set; }
        public string Method { get; set; }
        public bool Allow { get; set; }

        public override string ToString()
        {
            return String.Format("{0} , {1}", Method, Allow);
        }
    }
    public enum DesignedReportTypeEnum
    {
        Monthly = 0, Daily = 1, Person = 2
    }
    public enum DesignedReportParameterType
    {
        DateRange, FromToDate, None
    }
    public enum ClientelePersonTrafficCaller
    {
        Contractor,
        Office
    }

    public enum StringGeneratorExceptionType
    {
        ClientAttachments,
        ReportCondition,
        ConceptRuleManagement,
        Shifts
    }



    public enum DeliveryItemLoadState
    {
        None,
        Returned,
        NotReturned
    }

    public enum PersonControlStationExistanceState
    {
        Exist,
        NotExist,
        Undefined
    }

    public enum OperationBatchSize
    {
        OperationBatchSizeKey
    }
    public enum OrganizeTempState
    {
        SessionStart,
        SessionEnd
    }

    public enum AppFolders
    {
        PersonnelImages,
        RequestsAttachments,
        ReportFiles,
        RuleGenerator
    }
    //public enum UIValidationRuleType
    //{
    //    Traffic = 1,
    //    Leave = 2,
    //    Duty = 3,
    //    OverTime = 4,
    //    Calculation = 5,
    //    Public = 6
    //}

    public enum ConditionOperators
    {
        Equal,
        NotEqual,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,
        Between
    }

    public enum GroupingReport
    {
        Part = 0,
        Employ = 1
    }

    public enum ManagerFlowConditionValueType
    {
        Minute,
        Day,
    }
    public enum CustomValidationDataType
    {
        Int = 1, Decimal = 2, String = 3, Bool = 4,
    }

    public enum DesignedReportPersonInfoKeyColumn
    {
        CardNumber = 0, Department = 1, Employment = 2, EmploymentNumber = 3, EmploymentDate = 4, EndEmploymentDate = 5, Sex = 6,
        FirstName = 7, Grade = 8, PersonActive = 9, NationalCode = 10, IDNumber = 11, LastName = 12, WorkGroup = 13, Rule = 14, UiValidation = 15,
        ControlStation = 16, LeaveRemainCurentMonthDay = 17, LeaveRemainCurentMonthHour = 18, LeaveRemainCurentYearDay = 19, LeaveRemainCurentYearHour = 20, Shift = 21,
        prsTA_R1 = 22, prsTA_R2 = 23, prsTA_R3 = 24, prsTA_R4 = 25, prsTA_R5 = 26, prsTA_R6 = 27, prsTA_R7 = 28, prsTA_R8 = 29, prsTA_R9 = 30, prsTA_R10 = 31, prsTA_R11 = 32,
        prsTA_R12 = 33, prsTA_R13 = 34, prsTA_R14 = 35, prsTA_R15 = 36, prsTA_R16 = 37,
        prsTA_R17 = 38, prsTA_R18 = 39, prsTA_R19 = 40, prsTA_R20 = 41
    }

    public enum DesignedReportTrafficKeyColumn
    {
        AllTraffic = 0, FirstTraffic = 1, LastTraffic = 2
    }

    public enum DesignedReportColumnType
    {
        Concept, Traffic, PersonInfo, None, PersonParam
    }
    public enum GridSettingCaller
    {
        Kartable = 1, SpecialKartable = 2, Survey = 3, RequestSubstituteKartable = 4
    }
    public enum KartablGridSetting
    {
        FlowLevels, RequestType, RequestSource, Select, Row, BarCode, Applicant, RequestTitle, TheFromDate, TheToDate, TheFromTime, TheToTime, TheDuration, RegistrationDate, OperatorUser, DepartmentName, Description, AttachmentFile, RequestHistory
    }
    public enum SpecialKartablGridSetting
    {
        FlowStatus, FlowLevels, RequestType, RequestSource, Select, Row, BarCode, Applicant, RequestTitle, TheFromDate, TheToDate, TheFromTime, TheToTime, TheDuration, RegistrationDate, OperatorUser, DepartmentName, Description, AttachmentFile, RequestHistory
    }
    public enum ServeyKartablGridSetting
    {
        FlowStatus, FlowLevels, RequestType, RequestSource, Row, BarCode, Applicant, RequestTitle, TheFromDate, TheToDate, TheFromTime, TheToTime, TheDuration, RegistrationDate, OperatorUser, DepartmentName, Description, AttachmentFile, RequestHistory
    }
    public enum RequestSubstituteKartableGridSetting
    {
        FlowStatus, FlowLevels, RequestType, Select, Row, BarCode, Applicant, RequestTitle, TheFromDate, TheToDate, TheFromTime, TheToTime, TheDuration, RegistrationDate, OperatorUser, Description, DepartmentName
    }
    public enum WorkFlowDetailSearch
    {
        WorkFlow = 1,
        Manager = 2,
        Operator = 3,
        Substitute = 4,
        Personnel = 5
    }
    public enum UIValidationRuleGroupType
    {
        Hourly = 1,
        Daily = 2,
        Monthly = 3,
        Etc = 4
    }
    public enum RuleGeneratorRuleScope
    {
        Daily = 0,
        Monthly = 2,
    }
    public enum UIValidationRuleType
    {
        RulePrecardParameter = 1,
        RulePrecard = 2,
        RuleParameter = 3,
        Rule = 4
    }
    public enum UIValidationExistance
    {
        NotExist = 0,
        Exist = 1,
    }
    public enum UIValidationIsWarning
    {
        Nothing = 0,
        R22 = 22
    }

    public enum UIValidationCustomCode
    {
        R1 = 1,
        R3 = 3,
        R24 = 24,
        R25 = 25,
        R30 = 30,
        R26 = 26,
        R200 = 200,
        R201 = 201,
        R36 = 36,
    }
    public enum GridRoleSettings
    {
        DayName, TheDate, FirstEntrance, FirstExit, SecondEntrance, SecondExit, ThirdEntrance, ThirdExit, FourthEntrance, FourthExit, FifthEntrance, FifthExit, LastExit, NecessaryOperation, PresenceDuration, HourlyPureOperation,
        DailyPureOperation, ImpureOperation, AllowableOverTime, UnallowableOverTime, HourlyAllowableAbsence, HourlyUnallowableAbsence, DailyAbsence, HourlyMission, DailyMission, HostelryMission, HourlySickLeave, DailySickLeave,
        HourlyMeritoriouslyLeave, DailyMeritoriouslyLeave, HourlyWithoutPayLeave, DailyWithoutPayLeave, HourlyWithPayLeave, DailyWithPayLeave, Shift, DayState, ReserveField1, ReserveField2, ReserveField3, ReserveField4, ReserveField5, ReserveField6,
        ReserveField7, ReserveField8, ReserveField9, ReserveField10,
    }
    public enum UIVlidationRulesTagExist
    {      
        R33 = 33,
    }

    public enum CollectServiceOperationType
    {
        Traffic = 0,
        Request = 1,
        Permit = 2
    }

    /// <summary>
    /// طریقه ثبت درخواست انبوه
    /// </summary>
    public enum CollectiveRequestRegistType
    {
        Business = 0,
        Service = 1

    }
    public enum RequestPersonelCountState
    {
        Single,
        Collective
    }
    public enum LoadSttate
    {
        Normal,
        Search,
        AdvancedSearch
    }
    public enum RequesttCaller
    {
        NormalUser,
        Operator,
        OperatorPermit
    }
    public enum ActionType
    {
        ADD = 1, EDIT = 2, DELETE = 3
    }

   public enum ReportOutPutType
   {
       Report,
       Excel
   }
   public enum SpecificDays
    {
        Saturday,
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        WorkingDays,
        NonWorkingDay,
        UnofficialHollydays,
        OfficialHollydays
    }
    public enum preposition
    {
        And,
        Or,
        Before
    }
    public enum RuleParameterType
    {
        MissionParameterOne = 4001,
        MissionParameterZero = 4003,
        Work = 2017,
        Absence = 5009,
        LeaveParameter3012 = 3012,
        LeaveParemeter3017 = 3017,
        Miscellaneous = 1018
    }
    public enum ViewState
    {
        YearMonth,
        Date
    }
    public enum Caller
    {
        Kartable ,
        SpecialKartable
    }
    //DNN Note----------------------------------------------------------------------

    /// <summary>
    /// نوع گره چارت سازمانی
    /// </summary>
    public enum DepartmentType
    {
        /// <summary>
        /// هیچکدام
        /// </summary>
        None = 0,
        /// <summary>
        /// معاونت
        /// </summary>
        Assistance = 1,
        /// <summary>
        /// مدیریت
        /// </summary>
        Management = 2,
        /// <summary>
        /// واحد
        /// </summary>
        Unit = 3,
        /// <summary>
        /// سازمان/ارگان
        /// </summary>
        Organization = 4
    }

    /// <summary>
    /// نوع اضافه کار تشویقی
    /// </summary>
    public enum OverTimePersuasiveType
    {
        /// <summary>
        /// اضافه کار تشویقی
        /// </summary>
        OverTimeWork = 1,
        /// <summary>
        /// تعطیل کاری تشویقی
        /// </summary>
        HolidayWork = 2,
        /// <summary>
        /// شب کاری تشویقی
        /// </summary>
        NightWork = 3
    }



    /// <summary>
    /// نوع کارتابل جهت زمان بندی تایید
    /// </summary>
    public enum ApprovalScheduleType
    {
        None = 0,
        /// <summary>
        /// تایید کارکرد پرسنل
        /// </summary>
        Personal = 1,
        /// <summary>
        /// تایید کارتابل مدیریان
        /// </summary>
        Manager = 2,
        /// <summary>
        /// تایید کارتابل معاونین
        /// </summary>
        Assistance = 3,
        /// <summary>
        /// تایید کارتابل اداری
        /// </summary>
        Administrative = 4

    }

    //End of DNN Note----------------------------------------------------------------------

}