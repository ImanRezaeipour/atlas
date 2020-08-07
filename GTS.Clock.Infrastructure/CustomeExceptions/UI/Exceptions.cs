using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure.Exceptions.UI
{
    public enum ExceptionResourceKeys
    {
        #region TA

        #region General
        ItemNotExist,

        #endregion

        #region Department
        DepartmentRepeatedName, DepParentIDRequierd, DepUsedByPersons,
        DepNameRequierd, DepParentNotExists, DepartCustomCodeRepeated,
        DepartmentRootDeleteIllegal, ChildsDepUsedByPersons, DutyPlaceRootDeleteIllegal,
        #endregion

        #region Shift
        ShiftColorRepeated, ShiftNameRepeated, ShiftUsedInWorkGroup,
        ShiftPairNull, ShiftPairFromToEmpty, ShiftPairHasIntersect,
        ShiftNameRequierd, ShiftColorRequierd, ShiftFromAndToAreEquals,
        ShiftFromGreaterThanTo, ShiftItemNotExists, ShiftPairItemNotExists,
        ShiftTypeRequierd, ShiftCustomCodeRepeated, ShiftPairTypeIsEmpty, ShiftShortsKeyRepeated,
        #endregion

        #region NobatKari
        NobatKariNameEmpty, NobatKariRepeated, NobatKariUsedByShift,
        NobatKariCustomCodeRepeated,
        #endregion

        #region Workgroup
        WorkGroupNameRequierd, WorkGroupNameRepeated,
        WorkGroupCustomCodeRepeated, WorkGroupUsedByPerson,
        #endregion

        #region organizationUnit
        OrganizationUnitUsedByPerson, OrganizationUnitNameRepeated, OrganizationUnitParentIDRequierd,
        OrganizationUnitNameRequierd, OrganizationUnitParentNotExists, OrganizationUnitCustomCodeRepeated,
        OrganizationUnitRootDeleteIllegal, ChildOrganizationUnitUsedByPerson, OrganizationUnitUsedInFlowAsManager,
        #endregion

        #region Employment
        EmploymentTypeNameRequierd, EmploymentTypeNameRepeated, EmploymentTypeCustomCodeRepeated,
        EmploymentTypeUsedByPerson,
        #endregion

        #region RuleCategory
        RuleCategoryNameRequierd, RuleCategoryNameRepeated,
        RuleCategroyInsertedRuleIsEmpty, RuleCategoryRootDeleteIllegal,
        RuleCategoryUsedByPerson,

        #endregion

        #region RuleParameter
        AssignParameterDateHasIntersect, AssignParameterDateIsInvalid,
        AssignParameterFromDateGreaterThanToDate, AssignParameterRuleIDInvalid,
        AssignParameterFromDateAndToDateAreEquals, AssignParameterHasNotSpace,
        #endregion

        #region RuleViewer
        RuleCategoryIdIsInValid,
        #endregion

        #region CalculationDateRange
        DateRangesCountNotEqualToTwelve, DateRangesGroupNameRequierd,
        DateRangesGroupNameRepeated, DateRangesMustHaveConcept,
        DateRangesUsedByPerson, DateRangesCopyIdIsNotValid,
        PersonnelCalculationDateRangeIsNotValuedForSelectedYear,
        PersonCalculationRequied,
        #endregion

        #region ControlStation
        StationNameRequierd, StationNameRepeated, StationCustomCodeRepeated,
        StationUsedByPerson, StationUsedByMachine,
        #endregion

        #region Person
        PersonNameRequied, PersonLastNameRequierd,
        PersonSexRequierd, PersonMarriedRequierd,
        PersonCodeRequierd, PersonDepartmentRequierd,
        PersonWorkGroupRequierd, PersonRuleGroupRequierd,
        PersonEmploymenttypeRequierd, PersonDateRangeRequierd,
        PersonBarcodeRequierd, PersonMeliCodeRepeated, PersonnelMeliCodeRequierd, PersonnelMeliCodeLength, PersonnelMeliCodeExpression, PersonnelMeliCodeExpressionCorrectFormat,
        PersonBarcodeRepeated, PersonCartNumberRepeated, PersonCardNumberIsTooLong,
        PersonEmployeeNumRepeated, PersonMeliCodeInValid,
        PersonBarcodeInValid, PersonCartNumInValid,
        PersonShenasnameCodeInValid, PersonEmploymentFromDateRequired, PersonEmploymentFromDateGreaterThanToDate,
        PersonUIValidationRequierd, PersonBirthDateIsNotValid, PersonStartEmployeeDateIsNotValid,
        PersonEndEmployeeDateIsNotValid, PersonCardNumIsNumberOrIsCaracter, PersonUsedInFlowAsManager, PersonDeactiveNotAllowedBecauseUsedInFlowAsManager, PersonOrganizationUnitChangeNotAllowedBecauseUsedInFlowAsManager, CurrentUserDeleteNotAllowed, CurrentUserdoseNotAllowedDeactiveItsPersonnel, CurrentUserdoseNotAllowedDeleteItsPersonnel, AssignedRootAsOrganizationPostNotValid,
        PersonCostCenterRequired,
        #endregion

        #region PersonReservedFields
        PrsRsvFldLableIsEmpty, PrsRsvFldComboValueRepeated, PrsRsvFldComboValueUsedByPerson,
        PrsRsvFldComboValueIsEmpty,
        #endregion

        #region AssignWorkGroup
        AssignWorkGroupPersonIdNotExsits, AssignWorkGroupIdNotExsits,
        AssignWorkGroupSmallerThanStandardValue,
        #endregion
        #region AssignEmploymentType
        AssignEmploymentTypePersonIdNotExsits, AssignEmploymentTypeIdNotExsits,
        AssignEmploymentTypeSmallerThanStandardValue, AssignEmploymentTypeFromDateGreaterToDate, AssignEmploymentTypeDateHasConfilict,
        #endregion
        #region AssignRule
        AssignRulePersonIdNotExsits, AssignRuleIdNotExsits,
        AssignRuleDateFormatProblem, AssignRuleDateSmallerThanStandardValue,
        AssignRuleFromDateGreaterThanToDate, AssignRuleDateHasConfilict,
        #endregion

        #region PersonContractAssignment
        PersonContractAssignmentPersonIdNotExsits, PersonContractAssignmentContractIdNotExsits,
        PersonContractAssignmentDateSmallerThanStandardValue,
        PersonContractAssignmentFromDateGreaterThanToDate, PersonContractAssignmentDateHasConfilict, PersonMustHaveOneContractAssignment,
        personContractAssignmentFromDateMustNotLessThanEmploymentDate, PersonMustHaveOneRuleAssignment, PeronMustHaveOneAssignDateRange, PersonMustHaveOneAssignWorkGroup,
        #endregion

        #region AssignRange
        AssignRangePersonIdNotExsits, AssignRangeGroupIdNotExsits,
        AssignRangeSmallerThanStandardValue, AssignRangeFirstMustBeFromStartYear,
        AssignRangeDateIsRepeated, AssignWorkGroupIsRepeated,
        #endregion

        #region WorkGroupCalendar
        WorkGroupCalendarDublicateDate, WorkGroupCalendarPriodIsEmpty, WorkGroupCalendarPriodDateIsNotValid, FromdayElderToDay,
        #endregion

        #region Manager
        ManagerOwnerNotSpecified, ManagerUsedByFlow, PersonnelAssignedToOrganizationPostIsNotActiveOrDeleted, SelectedPersonnelIsDeActive, UnderManagementPersonnels, AssignedOrganizationPostRootAsManagerNotValid,
        #endregion

        #region Flow
        FlowNameRepeated, FlowNameRequierd, FlowAccessGroupRequierd, FlowAccessGroupRepeated,
        FlowMustHaveOneManagerFlow, FlowPersonOrOrganizationMustSpecified, FlowGroupNameRequired, FlowGroupNameRepeated, FlowDepartmentRootNotAccessSystemTechnical,
        FlowUnderManagementPersonnelRequired,
        #endregion

        #region
        ContractNameRequired, ContractCodeIsRepeated, ContractContractorRequired, ContractDefaultIsRequired, ContractDefaultCanNotDeleted, ContractIsAssignedToPerson, ContractTitleIsRepeated,
        #endregion
        #region Pishcart
        PrecardNameRequierd, PrecardNameRepeated, PrecardGroupRequierd,
        PrecardInvalidStatus, PrecardCodeRequierd, PrecardCodeRepeated,
        PrecardUsedBySubestitute, PrecardUsedByBasicTraffic, PrecardIsLock,
        PrecardNotSpec,
        #endregion

        #region Ilness
        IllnessNameRequierd, IllnessNameRepeated,
        #endregion
        #region
        GradeNameRequierd, GradeNameRepeated, GradeUsedByPersons,
        #endregion
        #region Doctor
        DoctorLastNameRequierd, DoctorNezampezeshkiRepeated, DoctorNazampezeshkiRequired,
        #endregion

        #region Precard Access Group
        AccessGroupNameRequierd, AccessGroupNameRepeated,
        AccessGroupUsedByFlow,
        #endregion

        #region MonthlyOperation
        MonthlyOpCurentUserIsNotValid, MonthlyOpIDMustSpecified,
        #endregion

        #region Request
        RequestUsedByFlow, RequestRepeated, RequestImperativeRepeated, RequestDateShouldNotEmpty,
        RequestFromDateGreaterThanToDate, RequestFromTimeGreaterThanToTime,
        RequestTimeShouldNotEmpty, RequestFromToDateNotEqual, RequestPrecardIsEmpty,
        RequestTimeIsNotValid, RequestPersonRequierd, RequestMonthIsEmpty, RequestYearIsEmpty,
        RequestIsNotAllowed, EditRequestNotAllowed, RequestRequired, RequestFromDateIsNotEqualToDate, RequestFlowNotStarted, DateNotValid,
        #endregion

        #region Role
        RoleNameRequierd, RoleNameReplication, RoleCodeReplication, RoleUSedByUser,
        RoleParentNotSpecified, RoleRootDeleteIllegal, RoleSystemNotAllowedUpdate, RoleSystemNotAllowedDelete,
        #endregion

        #region User
        UsernameReplication, UserPasswordIsNull, UserConfirmPasswordNotEqual, UsernameNotProvided,
        UserPersonIsNotSpecified, UserRoleIsNotSpecified, UserPasswordIsNotCurrent, UserNameNotAbleEdit, UserPasswordIsNotCorrect,
        UserPasswordMustContainsWordOrNumber, UserPasswordMustNotContainsNumber, UserPasswordMustContainsOnlyWord, UserPasswordMustNotContainsWord, UserPasswordMustContainsOnlyNumber, UserPasswordMustNotContainsWordOrNumber,
        UserPasswordMinCharacterLength, UserPasswordMaxCharacterLength,
        #endregion

        #region Report
        ReportRepeatedName, ReportParentIDRequierd, ReportRootDeleteIllegal,
        ReportNameRequierd, ReportParentNotExists, ReportFileNotSpecified,
        ReportCanNotBeParent, ReportParameterActionIdIsEmpty, ReportParametersIsEmpty,
        ReportParameterPersonIsEmpty, NoColumnSelectedForReport, ReportNotSpec, DesignedReportColumnIsRepeated, DesignedReportTypeIsNotSelected, DesignedReportColumnIsRequired, DesignedReportColumnIsMax,
        DesignedReportTrafficTypeIsConflicted, ReportParameterFromDateNotGreaterToDate, DesignedReportTypeNotCorrectColumns, DesignedReportPersonTypeNotCorrectColumns, ReportColumnInUseConditions, MonthlyReportHavNotTrafficColumns, PersonNotExist, KeyColumnNameConceptIsRepeated, InsertReportParentIDRequierd, ReportGroupDeleteIllegal,

        #endregion

        #region ReportFile
        ReportFileRepeatedName, ReportFileParentIDRequierd, ReportFileNameRequierd,

        #endregion

        #region Calendar
        CalendarNameRequierd, CalendarNameRepeated, CalendarCustomCodeRequierd,
        CalendarCustomCodeRepeated, CalendarTypeUsedInHolidayTemplates, CalendarTypeAssignWorkGroup,
        #endregion

        #region DutyPalce
        DutyPlaceNameRequierd, DutyPlaceNameRepeated, DutyPlaceCustomCodeRepeated,
        DutyPlaceUsedByRequest, DutyPlaceParentRequest,
        #endregion

        #region Clock
        ClockNameRequierd, ClockNameRepeated, ClockCustomCodeRepeated,
        ClockTypeRequierd, ClockControStationRequierd,
        #endregion

        #region Substitute
        SubstituteManagerRequiered, SubstitutePersonRequiered, SubstituteDateRequired, SubstituteFromDateGreaterThanToDate,
        SubstituteIsNotSpecified, SubstituteUpdateFlowAndSubstituteIdRequeiered, SubstitutePersonMustNotEqualtoManager,
        #endregion

        #region Operator
        OperatorPersonIsRequierd, OperatorFlowIsRequierd, OperatorRepeated,
        #endregion

        #region RemainLeave
        RemainLeaveExists, RemainLeavePersonNotSelected, RemainTransferFromToYearDiffrenceMoreThanOne, RemainTransferFromYearIsNotExists,
        #endregion

        #region Help
        HelpIdNotSpecified,
        #endregion

        #region Public Message
        PublicMessageContentRequierd, PublicMessageSubjecttRequierd,
        #endregion

        #region UIValidationGroup
        ValidationGroupNameIsEmpty, ValidationGroupNameIsRepeated, ValidationGroupRulesIsEmpty, UIValidationGroupUsedByPerson, ValidationGroupIdIsEmpty, ValidationGroupIdIsRepeated, ValidationPrecardParamIsNotValue, ValidationRuleParamIsNotValue, ValidationRuleNotValid, ValidationGroupCodeIsRepeated, ValidationGroupCodeIsEmpty,
        #endregion

        #region UIValidationValidator
        lockCalculationFromMonthNotValid, LockCalculationFromCurrentMonthNotValid, LockCalculationtMonthNotValid,
        LockCalculationDayNotValid, WeekDayNotValid, PrecardCountNotValid, PrecardValueNotValid, Discontinuous, DayCount, GivingBirthLeaveRespite,
        #endregion

        #region BasicTraffic
        TrafficDateRequierd, TrafficTimeRequierd, TrafficPrecardRequierd, TrafficIsRepeated,
        TrafficPersonRequierd,
        #endregion

        #region ExceptionShift
        ExceptionShiftPersonIdRequierd, ExceptionShiftShiftIdRequierd, ExceptionShiftWorkGroupIdRequierd, ShiftWithThisCustomCodeNotExists, ExceptionShiftDateInvalid, SelectedDateIsSimilar,

        #endregion

        #region Validation
        UIValidation_R1_LockCalculationFromCurrentMonth, UIValidation_R2_LockCalculationFromCalculationMonth, UIValidation_R3_LockCalculationFromDate,
        UIValidation_R4_MaxTarfficRequest, UIValidation_R5_HourlyTrafficRequest, UIValidation_R6_RequestMaxAvvalVaght, UIValidation_R7_MaxValueOfRequest, UIValidation_R8_OverlyHourlyPrecard,
        UIValidation_R9_OverlyDailyPrecard, UIValidation_R10_SaveRequestwithTimeRange, UIValidation_R11_RequestMinLength, UIValidation_R12_RequestDescriptionRequierd, UIValidation_R13_IllenssRequest,
        UIValidation_R14_DoctorRequest, UIValidation_R15_DutyPlaceRequest, UIValidation_R16_DailyTrafficRequest, UIValidation_R17_MaxAmountHourlyRequest, UIValidation_R18_MaxDailyRequestInYear,
        UIValidation_R19_MaxDailyRequestCountInMonth, UIValidation_R20_MaxHourlyRequestCountInDay, UIValidation_R21_OperatorRequestMaxCount, UIValidation_R22_RequestRemainLeaveToEndMonth,
        UIValidation_R23_SaveDailyRequestwithTimeRange, UIValidation_R24_DailyRequestInWeekdays, UIValidation_R25_HourlyRequestInWeekdays, UIValidation_R26_SumOfPrecads, UIValidation_R28_MaxAmountHourlyRequestForMonthlyPrecard,
        UIValidation_R29_RequestMaxArrivalExit, UIValidation_R27_ArchiveCalculationKaheshi, UIValidation_R32_AmountInDay, UIValidation_R31_OverlapDailyHourlyPrecard, UIValidation_R200_RequestLeaveHourlyNotAllowedWithOtherPrecardsInSticking,
        UIValidation_R200_RequestLeaveHourlyStickingInOtherPrecards, UIValidation_R201_RequestLeaveDailyNotAllowedWithOtherPrecardsInSticking, UIValidation_R33_SubstituteRequest, UIValidation_R201_RequestLeaveDailyStickingInOtherPrecards,
        UIValidation_R36_GivingBirthLeave, UIValidation_R34_WithOutPayLeaveInRemainLeave, UIValidation_R35_InWayLeave, UIValidation_R37_EstelajiRequeredFileAttachement, UIValidation_R38_RequestMaxLength,
        UIValidation_R202_RequestRegisterSchedule, UIValidation_R203_RequestConfirmedSchedule, UIValidation_R204_RequestIncompleteTraffic,UIValidation_R205_RequestInHoliday,
        //UIValidation_R7TrafficRequestMaxCount, UIValidation_R8TrafficRequestDayTimeFinished,
        // UIValidation_R5_LockCalculationFromBeforeMonth,
        //UIValidation_R6_LockCalculationFromDate,
        //UIValidation_R9_Before, UIValidation_R9_After, UIValidation_R10_Before, UIValidation_R10_After,
        //UIValidation_R11_Before, UIValidation_R11_After, UIValidation_R12_Before, UIValidation_R12_After,
        //UIValidation_R13_Before, UIValidation_R13_After, UIValidation_R14_Before, UIValidation_R14_After,
        //UIValidation_R15_Before, UIValidation_R15_After, UIValidation_R16RequestMaxCount,
        //UIValidation_R17RequestMaxCount, UIValidation_R18RequestMaxCount, UIValidation_R19RequestMaxCount,
        //UIValidation_R20RequestMaxCount, UIValidation_R21RequestMaxCount, UIValidation_R22RequestMaxHrour,
        //UIValidation_R23RequestMaxCount, UIValidation_R26RequestMaxAvvalVaght,
        //UIValidation_R28RequestMaxValue, UIValidation_R30DutyPlace, UIValidation_R31Doctor, UIValidation_R32Illenss,
        //UIValidation_R33LeaveRemain, UIValidation_R37OperatorRequestMaxCount, UIValidation_R38RequestMaxCount, UIValidation_R39_ArchiveCalculationKaheshi, UIValidation_R40_RequestRemainLeaveToEndMonth,
        //UIValidation_R41RequestMaxHrour, UIValidation_R42RequestMaxCount, UIValidation_R43RequestMaxAmount, UIValidation_R44RequestMaxValue, UIValidation_R45OverlyHourlyPrecard, UIValidation_R46OverlyDailyPrecard, UIValidation_R48LockCalculationAfterspesificDays,
        //UIValidation_R50MarriageChildren, UIValidation_R49Marriage, UIValidation_OverWorkInFriday, UIValidation_R52DeadDegree2, UIValidation_R51DeadDegree1, UIValidation_R57HalfDayLeavecount,
        //UIValidation_R56MaxHortatoryAmountInMonth, UIValidation_R55MaxHourlyLeaveHortatoryRequest, UIValidation_R54SumOfEstelajiANDhardIllness, UIValidation_R47RegularRequestMaxCountInDay,
        //UIValidation_R36RequestMinLength, UIValidation_R35RequestMaxCountInDay, UIValidation_R34RequestDescriptionRequierd, UIValidation_R29DasturyOverworkRequest,
        //UIValidation_R27MaxCountInYearRequest, UIValidation_R25MaxWithoutPayLeaveInMonthRequest, UIValidation_R24AllRequestOffset, UIValidation_R58MissionDayOfsetAfter,UIValidation_R58MissionDayOfsetBefore,       
        #endregion

        #region USerSetting
        UserSet_EmailTimeIsNotValid, UserSet_EmailTimeLessThanMin, UserSet_SMSTimeIsNotValid, UserSet_SMSTimeLessThanMin, DashboardIsDuplicated,
        #endregion

        #region Corporation
        CorporationNameRequierd, CorporationNameRepeated, CorporationCodeRequierd, CorporationCodeRepeated,
        #endregion

        #region BSecondaryConceptUserDefined

        BSecondaryConceptRequierd,
        BSecondaryConceptCustomeCategoryParentExpressionRequierd,

        BSecondaryConceptNameRequierd, BSecondaryConceptNameRepeated,
        BSecondaryConceptCodeRequierd, BSecondaryConceptCodeRepeated,
        BSecondaryConceptColorRequierd, BSecondaryConceptColorRepeated,

        BSecondaryConceptTypeRequierd,
        BSecondaryConceptPeriodicTypeRequierd,
        BSecondaryConceptCalcSituationTypeRequierd,
        BSecondaryConceptPersistSituationTypeRequierd,
        BSecondaryConceptCustomeCategoryCodeRequierd,


        #endregion

        #region BSecondaryConceptUserDefined

        BRuleNameRequierd, BRuleNameRepeated,
        BRuleCodeRequierd, BRuleCodeRepeated,

        #endregion

        #region Archive Concepts

        ArchiveDataTypeIsNotValid,

        #endregion

        #region Person Param Value

        ParamFieldIsEmpty, ParamPersonIsEmpty, ParamFromDateGreaterThanToDate, ParamFieldNameIsEmpty, ParamFieldKeyIsEmpty, ParamFieldKeyRepeated, ParamFieldKeyIsUsed, ParamFromDateToDateNotEmpty, paramNotSelected,

        #endregion

        #region ShiftPairType
        ShiftPairTypeNameIsEmpty, ShiftPairTypeUsedByShiftPair,
        ShiftPairTypeCustomCodeIsEmpty, ShiftPairTypeCustomCodeRepeated,
        #endregion

        #region SpecialKartable
        ManagerIsInvalid, ManagerFlowIsInvalid, NoAccessibleDepartmentIsAvailable, NoAccessiblePrecardIsAvailable,
        #endregion

        #region DataAccessLevels
        DepartmentAccessDenied, OrganizationUnitAccessDenied, ShiftAccessDenied, WorkGroupAccessDenied, PrecardAccessDenied,
        ControlStationAccessDenied, DoctorAccessDenied, ManagerAccessDenied, RuleGroupAccessDenied, FlowAccessDenied, ReportAccessDenied, CorporationAccessDenied, EmploymentTypeAccessDenied, RoleAccessDenied, CostCenterAccessDenied,
        #endregion

        #region OperatorPermit
        CurrentUserIsNotManager,
        CurrentUserIsNotOperator,
        ManagersCountInOperatorFlowIsGreaterThanOne,
        #endregion

        #region Calculations
        CalculationStartDateIsGreaterThanCalculationEndDate, CalculationPersonNotSelected,
        #endregion

        #region OverTime

        DateRequierd, MaxNightlyRequierd, MaxHolidayRequierd, MaxOverTimeRequierd, DepartmentRequierd, DateTimeRequierd, MaxOverTimeOverFlow, MaxNightlyOverFlow, MaxHolidayOverFlow,
        DaysInMonthValidate,

        #endregion

        #region PersonApprovalAttendance

        ApprovalDuplicate, ApprovalExpire, ApprovalMonthNotCompeleted, CostCenterRequired,

        #endregion

        #region ApprovalAttendanceSchedule

        ApprovalScheduleDateFromRequired, ApprovalScheduleDateToRequired, ApprovalScheduleTypeRequired, ApprovalScheduleDateValidRequired, ApprovalScheduleDateRangeOrderRequired, ApprovalScheduleDateRangeOrderValidate, ApprovalScheduleCostCenterRequired,
        #endregion

        #region CostCenter
        CostCenterNameRequierd, CostCenterNameRepeated, CostCenterCodeRepeated, CostCenterUsedByPersons,
        #endregion

        #endregion

        #region Clientele

        #region DepartmentPosition
        DepartmentPositionUnitNameRequired, DepartmentPositionLocationRequired, DepartmentPositionNameIsDuplicated,
        #endregion

        #region Contractor
        ContractorNameIsEmpty, ContractorNameRepeated, ContractorMeetingPersonIsEmpty,
        ContractorFromDateIsGreaterThanContractorToDate, ContractorDepartmentIsEmpty, ContractorCodeRepeated, ContractorEconomicCodeRepeated, ContractorUseInContract, AtLeastOneContractorMustBeSelectForDefault,
        #endregion

        #region ContractorEmp
        ContractorEmpNameIsEmpty, ContractorEmpNationalCodeRepeated,
        #endregion

        #region
        EquipmentNameIsEmpty,
        #endregion


        #region Car
        CarNameIsEmpty, CarCodeIsEmpty, CarCodeRepeated,
        #endregion

        #region Equipment
        EquipNameIsEmpty, EquipNameRepeated,
        #endregion

        #region EquipmentBlackList
        EquipBLNameIsEmpty, EquipBLNameRepeated, EquipBLCustomCodeRepeated, EquipBLFromDateIsEmpty, EquipBLToDateIsEmpty, EquipBLFromDateIsGreaterThanToDate,
        #endregion

        #region ClientelePerson
        CLPrsNameIsEmpty, CLPrsNationalCodeIsEmpty, CLPrsNationalCodeRepeated, CLPrsSexIsEmpty,
        #endregion

        #region Office
        OfficeDepartmentIsEmpty, OfficeMeetingPersonIsEmpty, OfficeReferredPersonNameIsEmpty, OfficeReferredPersonNationalCodeIsEmpty, OfficeRequestOfficeTypeIsEmpty,
        OfficeReferredPersonSexIsEmpty, OfficeActiveDirectoryUserNameIsEmpty, OfficeFromDateIsEmpty, OfficeToDateIsEmpty,
        OfficeMeetingPersonIsEqualtoOfficeSubstituteMeetingPerson, OfficeFromDateIsGreaterThanOfficeToDate, OfficeTypeNotSpec,
        #endregion

        #region OfficeType
        OfficeTypeNameIsEmpty,
        OfficeTypeNameIsDuplicated,
        OfficeTypeCustomCodeIsEmpty,
        OfficeTypeCustomCodeIsDuplicated,
        #endregion

        #region PersonBlackList
        CPBL_PersonIsEmpty, CPBL_FromDateIsEmpty, CPBL_ToDateIsEmpty, CPBL_FromDateIsGreaterThanToDate, CPBL_IsDuplicated,
        #endregion

        #region BPersonTraffic
        CPTA_PersonIsEmpty,
        CPTA_OffishRequestAndContractorIsEmpty,
        CPTA_FromDateIsEmpty,
        CPTA_FromDateGreateThanToDate,
        CPTA_FromTimeGreateThanToTime,
        CPTA_InvalidDateTimeRange,
        CPTA_InvalidDateRange,
        CPTA_InvalidTimeRange,

        CPTA_DuplicatedTraffic,

        CPTA_ErrorOnDeleting,



        #endregion


        #region BDeliveryItem

        DeliveryItem_TitleEmpty,
        DeliveryItem_OffishOrContractorIsEmpty,
        DeliveryItem_DeliveryItemIdListIsEmpty,
        DeliveryItem_ClientelePersonListIsEmpty,
        DeliveryItem_ClientelePersonListHasMoreThanOneItem,


        #endregion

        BExpressionRequiedScriptBeginFa,

        #region BRuleTempParameter

        BRuleTempParameterNameRequied,
        BRuleTempParameterRuleRequied,
        BRuleTempNameRepeated,
        BRuleTempShouldBeUserDefined,

        #endregion

        #endregion



    };
    //
    public enum UIExceptionTypes
    {
        Fatal, Reload, ShowMessage
    };

    public enum UIFatalExceptionIdentifiers
    {
        NONE = 1000,
        IllegalServiceAccess = 1001,
        DepartmentRootMoreThanOne = 1002,
        OrganizationUnitRootMoreThanOne = 1003,
        RuleCategoryRootMoreThanOne = 1004,
        RuleTypeNodesMoreThanSix = 1005,
        RuleCategoryInsertedRulesIsNULL = 1006,
        PersonDetailNotExistsInDatabase = 1007,
        UpdatePersonImageHasError = 1008,
        ShiftColorIsNotUnique = 1009,
        ManagerOrganizationUnitProblem = 1010,
        UnderManagmentDepartmentNull = 1011,
        UserSettingsLanguageOrUserNotExists = 1012,
        PersonDateRangeIsNotDefiend = 1013,
        ExpectedPrecardDoesNotExists = 1014,
        UsualPrecardIsNotExistsInDatabase = 1015,
        RoleRootMoreThanOne = 1016,
        ResourceControlsWithRepeatedId = 1017,
        ResourceRootMoreThanOne = 1018,
        ReportRootMoreThanOne = 1019,
        ReportParameterParsingIsNotMatch = 1020,
        DutyPlaceRootMoreThanOne = 1021,
        OperatorOrganizationUnitProblem = 1022,
        LeaveBudgetRecordsCountInDatabaseIsNotValid = 1023,
        LeaveLCRDoesNotExists = 1024,
        HelpFormKeyDoesNotExists = 1025,
        HelpRootIsMorThanOne = 1026,
        UserSettingsSkinOrUserNotExists = 1027,
        ReportParameterParsingSplitSign = 1028,
        UIValidationParameterCount = 1029,
        UIValidationParameterNotfound = 1030,
        CurrentUserIsNotValid = 1031,
        PersonReservedFiledsCount = 1032,
        UIValidationR3MustBeActive = 1033,
        PrecardGroupIsNull = 1034,
        OfficeTypeGroupIsNull = 1035,
        UserSettingsUserGridSchemaSttingNotExists = 1036,
        UIValidationParameterValueIncorrect = 1037,
    };
}
