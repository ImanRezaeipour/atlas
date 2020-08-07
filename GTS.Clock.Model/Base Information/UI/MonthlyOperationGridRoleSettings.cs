using GTS.Clock.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.UI
{
    public class MonthlyOperationGridRoleSettings : IEntity
    {

        public virtual Decimal ID { get; set; }
        public virtual Role Role { get; set; }
        public virtual Boolean DayName { get; set; }
        public virtual Boolean TheDate { get; set; }
        public virtual Boolean FirstEntrance { get; set; }
        public virtual Boolean FirstExit { get; set; }
        public virtual Boolean SecondEntrance { get; set; }
        public virtual Boolean SecondExit { get; set; }
        public virtual Boolean ThirdEntrance { get; set; }
        public virtual Boolean ThirdExit { get; set; }
        public virtual Boolean FourthEntrance { get; set; }
        public virtual Boolean FourthExit { get; set; }
        public virtual Boolean FifthEntrance { get; set; }
        public virtual Boolean FifthExit { get; set; }
        public virtual Boolean LastExit { get; set; }
        public virtual Boolean NecessaryOperation { get; set; }
        public virtual Boolean PresenceDuration { get; set; }
        public virtual Boolean HourlyPureOperation { get; set; }
        public virtual Boolean DailyPureOperation { get; set; }
        public virtual Boolean ImpureOperation { get; set; }
        public virtual Boolean AllowableOverTime { get; set; }
        public virtual Boolean UnallowableOverTime { get; set; }
        public virtual Boolean HourlyAllowableAbsence { get; set; }
        public virtual Boolean HourlyUnallowableAbsence { get; set; }
        public virtual Boolean DailyAbsence { get; set; }
        public virtual Boolean HourlyMission { get; set; }
        public virtual Boolean DailyMission { get; set; }
        public virtual Boolean HostelryMission { get; set; }
        public virtual Boolean HourlySickLeave { get; set; }
        public virtual Boolean DailySickLeave { get; set; }
        public virtual Boolean HourlyMeritoriouslyLeave { get; set; }
        public virtual Boolean DailyMeritoriouslyLeave { get; set; }
        public virtual Boolean HourlyWithoutPayLeave { get; set; }
        public virtual Boolean DailyWithoutPayLeave { get; set; }
        public virtual Boolean HourlyWithPayLeave { get; set; }
        public virtual Boolean DailyWithPayLeave { get; set; }
        public virtual Boolean Shift { get; set; }
        public virtual Boolean DayState { get; set; }
        public virtual Boolean ReserveField1 { get; set; }
        public virtual Boolean ReserveField2 { get; set; }
        public virtual Boolean ReserveField3 { get; set; }
        public virtual Boolean ReserveField4 { get; set; }
        public virtual Boolean ReserveField5 { get; set; }
        public virtual Boolean ReserveField6 { get; set; }
        public virtual Boolean ReserveField7 { get; set; }
        public virtual Boolean ReserveField8 { get; set; }
        public virtual Boolean ReserveField9 { get; set; }
        public virtual Boolean ReserveField10 { get; set; }
    }
}
