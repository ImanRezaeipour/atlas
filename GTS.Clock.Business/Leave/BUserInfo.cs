using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Rules;
using GTS.Clock.Model.Contracts;

namespace GTS.Clock.Business.Leave
{


    /// <summary>
    /// اطلاعات سهمیه و مانده مرخصی پرسنل
    /// created at: 2012-01-16 9:39:02 AM
    /// by        : $GTSDeveloper$
    /// write your name here
    /// </summary>
    public class BUserInfo:MarshalByRefObject
    {
        public delegate void UserInfoMessage(decimal personId, IList<string> result,string toDate);
        private UserInfoMessage infoProviders;

        /// <summary>
        /// سازنده کلاس
        /// </summary>
        public BUserInfo()
        {
            infoProviders += new UserInfoMessage(this.GetRemainLeaveTransferFromYearAgoToCurrentYear);
            infoProviders += new UserInfoMessage(this.GetRemainMonthLeave);
            infoProviders += new UserInfoMessage(this.GetRemainYearLeave);
            infoProviders += new UserInfoMessage(this.GetUsedYearLeave);
            infoProviders += new UserInfoMessage(this.GetRemainLeaveLoss);

        }

        /// <summary>
        /// اطلاعات پرسنل را بر می گرداند
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <returns>لیست اطلاعات</returns>
        public IList<string> GetUserInfo(decimal personId,string toDate)
        {
            try
            {
                IList<string> list = new List<string>();

                infoProviders.Invoke(personId, list,toDate);

                return list;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BUserInfo", "GetUserInfo");
                throw ex;
            }
        }

        /// <summary>
        /// مانده مرخصی در ماه را برای یک پرسنل بر می گرداند
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="result">خروجی</param>
        private void GetRemainMonthLeave(decimal personId, IList<string> result,string toDate)
        {
            try
            {
                string remain = "";
                int day, minutes, hour, min;//year = 0, month = 0,
                bool negative = false;
                PersonRepository prsRep = new PersonRepository();
                ILeaveInfo leaveInfo = new BRemainLeave();

                //if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                //{

                //    year = Utility.ToPersianDateTime(DateTime.Now).Year;
                //    month = Utility.ToPersianDateTime(DateTime.Now).Month;
                //}
                //else
                //{
                //    year = DateTime.Now.Year;
                //    month = DateTime.Now.Month;
                //}
                int year = int.Parse(toDate.Substring(0, 4));
                int month = int.Parse(toDate.Substring(5, 2));
                int toDay = int.Parse(toDate.Substring(8, 2));
                leaveInfo.GetRemainLeaveToEndOfMonth(personId, year, month,toDay, out day, out minutes);
                hour = (minutes / 60);
                min = minutes % 60;

                string dayValue = day >= 0 ? day.ToString() : String.Format("-({0})", Math.Abs(day));
                string hourValue = hour >= 0 ? hour.ToString() : String.Format("-({0})", Math.Abs(hour));
                string minValue = min >= 0 ? min.ToString() : String.Format("-({0})", Math.Abs(min));
               
                if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                {
                    remain = String.Format("مانده مرخصی تا انتهای ماه انتخابی {0} روز و {1} ساعت و {2} دقیقه", dayValue, hourValue, minValue);
                }
                else if (BLanguage.CurrentLocalLanguage == LanguagesName.English)
                {
                    remain = String.Format("Remains off until the end of selected Month, {0} days and {1} hours and {2} minutes", dayValue, hourValue, minValue);
                }
                result.Add(remain);
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BUserInfo", "GetRemainMonthLeave");
                throw ex;
            }
        }

        /// <summary>
        /// مانده مرخصی در سال را برای یک پرسنل بر می گرداند
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="result">خروجی</param>
        private void GetRemainYearLeave(decimal personId, IList<string> result,string toDate)
        {
            try
            {
                string remain = "";
                int day, minutes, hour, min;//,year = 0, month = 0;
                ILeaveInfo leaveInfo = new BRemainLeave();
                PersonRepository prsRep = new PersonRepository();
                Person prs = prsRep.GetById(personId, false);

                //if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                //{
                //    year = Utility.ToPersianDateTime(DateTime.Now).Year;
                //    month = Utility.ToPersianDateTime(DateTime.Now).Month;
                //}
                //else
                //{
                //    year = DateTime.Now.Year;
                //    month = DateTime.Now.Month;
                //}
                int year = int.Parse(toDate.Substring(0, 4));
                int month = int.Parse(toDate.Substring(5, 2));
                int toDay = int.Parse(toDate.Substring(8, 2));
                leaveInfo.GetRemainLeaveToEndOfYear(personId, year, month,toDay, out day, out minutes);
                hour = (minutes / 60);
                min = minutes % 60;
                
                string dayValue = day >= 0 ? day.ToString() : String.Format("-({0})", Math.Abs(day));
                string hourValue = hour >= 0 ? hour.ToString() : String.Format("-({0})", Math.Abs(hour));
                string minValue = min >= 0 ? min.ToString() : String.Format("-({0})", Math.Abs(min));

                if (prs.PersonTASpec.IsLeaveYearDependonContract)
                {
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        remain = String.Format("مانده مرخصی تا انتهای سال قراردادی {0} روز و {1} ساعت و {2} دقیقه", dayValue, hourValue, minValue);
                    }
                    else if (BLanguage.CurrentLocalLanguage == LanguagesName.English)
                    {
                        remain = String.Format("Remains off until the end of this Leave Year, {0} days and {1} hours and {2} minutes", dayValue, hourValue, minValue);
                    }
                }
                else
                {
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        remain = String.Format("مانده مرخصی تا انتهای سال جاری {0} روز و {1} ساعت و {2} دقیقه", dayValue, hourValue, minValue);
                    }
                    else if (BLanguage.CurrentLocalLanguage == LanguagesName.English)
                    {
                        remain = String.Format("Remains off until the end of this  Year, {0} days and {1} hours and {2} minutes", dayValue, hourValue, minValue);
                    }
                }
                
                result.Add(remain);
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        /// مرخصی استفاده شده در سال را برای یک پرسنل بر می گرداند
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="result">خروجی</param> 
        private void GetUsedYearLeave(decimal personId, IList<string> result,string toDate)
        {
            try
            {
                string usedLeave = "";
                int day, minutes, hour, min;//, year = 0, month = 0;
                ILeaveInfo leaveInfo = new BRemainLeave();
                PersonRepository prsRep = new PersonRepository();
                Person prs = prsRep.GetById(personId, false);

                //if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                //{
                //    year = Utility.ToPersianDateTime(DateTime.Now).Year;
                //    month = Utility.ToPersianDateTime(DateTime.Now).Month;
                //}
                //else
                //{
                //    year = DateTime.Now.Year;
                //    month = DateTime.Now.Month;
                //}
                int year = int.Parse(toDate.Substring(0, 4));
                int month = int.Parse(toDate.Substring(5, 2));
                int toDay = int.Parse(toDate.Substring(8, 2));
                leaveInfo.GetUsedLeaveToEndOfYear(personId, year,month, toDay, out day, out minutes);
                hour = (minutes / 60);
                min = minutes % 60;

                string dayValue = day >= 0 ? day.ToString() : String.Format("-({0})", Math.Abs(day));
                string hourValue = hour >= 0 ? hour.ToString() : String.Format("-({0})", Math.Abs(hour));
                string minValue = min >= 0 ? min.ToString() : String.Format("-({0})", Math.Abs(min));
                if (prs.PersonTASpec.IsLeaveYearDependonContract)
                {
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        usedLeave = String.Format("مرخصی مصرف شده از ابتدای سال قراردادی {0} روز و {1} ساعت و {2} دقیقه", dayValue, hourValue, minValue);
                    }
                    else if (BLanguage.CurrentLocalLanguage == LanguagesName.English)
                    {
                        usedLeave = String.Format("Used Leave From Begenning of current leave year is , {0} days and {1} hours and {2} minutes", dayValue, hourValue, minValue);
                    }
                }
                else
                {
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        usedLeave = String.Format("مرخصی مصرف شده از ابتدای سال  {0} روز و {1} ساعت و {2} دقیقه", dayValue, hourValue, minValue);
                    }
                    else if (BLanguage.CurrentLocalLanguage == LanguagesName.English)
                    {
                        usedLeave = String.Format("Used Leave From Begenning of current  year is , {0} days and {1} hours and {2} minutes", dayValue, hourValue, minValue);
                    }
                }
                result.Add(usedLeave);
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// ابطال مانده مرخصی تا آخر سال را بر می گرداند
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="result">خروجی</param>
        private void GetRemainLeaveLoss(decimal personId, IList<string> result,string toDate)
        {
            try
            {
                string leaveLossStr = string.Empty;
                BRemainLeave leaveInfo = new BRemainLeave();
                ILeaveInfo leaveInfo2 = new BRemainLeave();
                PersonRepository prsRep = new PersonRepository();
                Person prs = prsRep.GetById(personId, false);
                int day, minutes;//, year = 0, month = 0;
                //if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                //{
                //    year = Utility.ToPersianDateTime(DateTime.Now).Year;
                //    month = Utility.ToPersianDateTime(DateTime.Now).Month;
                //}
                //else
                //{
                //    year = DateTime.Now.Year;
                //    month = DateTime.Now.Month;
                //}
                int year = int.Parse(toDate.Substring(0, 4));
                int month = int.Parse(toDate.Substring(5, 2));
                int toDay = int.Parse(toDate.Substring(8, 2));
                IList<RemainLeaveProxy> remainLeaveCurrentYearProxy = leaveInfo.GetRemainLeave(personId, year, year);
                object paramRuleRemainLeaveLossValue = this.GetRuleParameter(DateTime.Now, prs, 3009, "first");
                leaveInfo2.GetRemainLeaveToEndOfYear(personId, year, month,toDay, out day, out minutes);
                int leaveLossInYear = 0;
                if (paramRuleRemainLeaveLossValue != null && Utility.ToInteger(paramRuleRemainLeaveLossValue) != 0)
                {
                    leaveLossInYear = Convert.ToInt32(paramRuleRemainLeaveLossValue);
                }
                
                object paramRuleLeaveMinuteInDayObj = GetRuleParameter(DateTime.Now, prs, 3017, "First");
                int leaveMinuteInDay = 0;
                if (paramRuleLeaveMinuteInDayObj != null && Utility.ToInteger(paramRuleLeaveMinuteInDayObj) != 0)
                {
                    leaveMinuteInDay = Convert.ToInt32(paramRuleLeaveMinuteInDayObj);
                }
                else
                {
                    //7.30 ساعت به طور پیش فرض گرفته شده
                    leaveMinuteInDay = (int)((7 * 60) + 30);
                }

                int leaveRemainLeaveCurrentYear = 0;
                if (remainLeaveCurrentYearProxy.Count != 0)
                    leaveRemainLeaveCurrentYear = (Utility.ToInteger(remainLeaveCurrentYearProxy.First().ConfirmedDay) * leaveMinuteInDay) + (Utility.RealTimeToIntTime(remainLeaveCurrentYearProxy.First().ConfirmedHour) == -1000 ? 0 : Utility.RealTimeToIntTime(remainLeaveCurrentYearProxy.First().ConfirmedHour));

                int leaveLossInt = ((day * leaveMinuteInDay) + minutes) - leaveRemainLeaveCurrentYear - (Utility.ToInteger(paramRuleRemainLeaveLossValue) * leaveMinuteInDay);
                string dayLossStr = string.Empty;
                string hourLossStr = string.Empty;
                string minuteLossStr = string.Empty;
                if (leaveLossInt > 0)
                {
                    dayLossStr = (leaveLossInt / leaveMinuteInDay).ToString();
                    int remainMinuteLoss = (leaveLossInt % leaveMinuteInDay);
                     hourLossStr = (remainMinuteLoss / 60).ToString();
                     minuteLossStr = (remainMinuteLoss % 60).ToString();
                }
                else
                {
                     dayLossStr = "0";
                     hourLossStr = "0";
                     minuteLossStr = "0";
                }
                if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                {
                    leaveLossStr = string.Format("مقدار مرخصی غیر قابل انتقال به سال بعد {0} روز و {1} ساعت و {2} دقیقه .",dayLossStr,hourLossStr,minuteLossStr);
                }
                else if (BLanguage.CurrentLocalLanguage == LanguagesName.English)
                {
                    leaveLossStr = string.Format("Amount Leave Not Transfer To next Year {0} Day and {1} Hours And {2} Minute ", dayLossStr, hourLossStr, minuteLossStr);
                }
                result.Add(leaveLossStr);
            }
            catch (Exception ex)
            {
                
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// تعداد مانده مرخصی سال قبل که به سال جاری منتقل شده است را برمی گرداند
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="result">خروجی</param>
        private void GetRemainLeaveTransferFromYearAgoToCurrentYear(decimal personId, IList<string> result,string toDate)
        {
            try
            {
                BRemainLeave leaveInfo = new BRemainLeave();
                int hour=0,min=0,day=0;//, year = 0, month = 0;
                //if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                //{
                //    year = Utility.ToPersianDateTime(DateTime.Now).Year;
                //    month = Utility.ToPersianDateTime(DateTime.Now).Month;
                //}
                //else
                //{
                //    year = DateTime.Now.Year;
                //    month = DateTime.Now.Month;
                //}
                int year = int.Parse(toDate.Substring(0, 4));
                int month = int.Parse(toDate.Substring(5, 2));
                int toDay = int.Parse(toDate.Substring(8, 2));
                IList<RemainLeaveProxy> remainLeaveCurrentYearProxy = leaveInfo.GetRemainLeave(personId, year, year);
                if (remainLeaveCurrentYearProxy.Count != 0){
                    hour =( Utility.RealTimeToIntTime(remainLeaveCurrentYearProxy.First().ConfirmedHour) == -1000 ? 0 : Utility.RealTimeToIntTime(remainLeaveCurrentYearProxy.First().ConfirmedHour))  / 60;
                    min =(Utility.RealTimeToIntTime(remainLeaveCurrentYearProxy.First().ConfirmedHour) == -1000 ? 0 : Utility.RealTimeToIntTime(remainLeaveCurrentYearProxy.First().ConfirmedHour)) % 60;
                    day = Utility.ToInteger( remainLeaveCurrentYearProxy.First().ConfirmedDay);
                }
                
                string remainLeaveFromYearAgo = string.Empty;
                if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                {
                    remainLeaveFromYearAgo = string.Format("مانده مرخصی انتقال یافته از سال قبل {0} روز و {1} ساعت و {2} دقیقه  .",day.ToString() , hour.ToString(), min.ToString());
                }
                else if (BLanguage.CurrentLocalLanguage == LanguagesName.English)
                {
                    remainLeaveFromYearAgo = string.Format("Remain Leave Transfer From Year Ago {0} Day and {1} Hour and {2} Minute ", day. ToString(), hour.ToString(), min.ToString());
                }
                result.Add(remainLeaveFromYearAgo);
            }
            catch (Exception ex)
            {

                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// مقدار پارامتر قانون پرسنل را برمی گرداند
        /// </summary>
        /// <param name="currentDate">تاریخ جاری</param>
        /// <param name="person">پرسنل</param>
        /// <param name="ruleIdentifier">کد قانون</param>
        /// <param name="parameterName">نام پارامتر</param>
        /// <returns>مقدار پارامتر</returns>
        private object GetRuleParameter(DateTime currentDate, Person person, int ruleIdentifier, string parameterName)
        {
            try
            {



                if (person.AssignedRuleList != null)
                {
                    AssignedRule ar = person.AssignedRuleList.Where(x => x.FromDate <= currentDate && x.ToDate >= currentDate && x.IdentifierCode == ruleIdentifier).FirstOrDefault();
                    if (ar != null)
                    {
                        EntityRepository<AssignRuleParameter> paramRep = new EntityRepository<AssignRuleParameter>();
                        IList<AssignRuleParameter> paramList = paramRep.Find(x => x.Rule.ID == ar.RuleId).ToList();

                        AssignRuleParameter asp = paramList.Where(x => x.FromDate <= currentDate && x.ToDate >= currentDate).FirstOrDefault();
                        if (asp != null)
                        {
                            RuleParameter parameter = asp.RuleParameterList.Where(x => x.Name.ToLower().Equals(parameterName.ToLower())).FirstOrDefault();

                            if (parameter != null)
                                return parameter.Value;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                 BaseBusiness<Entity>.LogException(ex, "BUserInfo", "GetRuleParameter");
                throw ex;
            }
        }
        
        /// <summary>
        /// بررسی دسترسی اطلاعات تکمیلی پرسنل در نمای شبکه ای گزارش کارکرد ماهانه
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckUserInformationLoadAccess_onPersonnelLoadStateInGridSchema()
        { 
        }

        /// <summary>
        /// بررسی دسترسی اطلاعات تکمیلی پرسنل در نمای گرافیکی گزارش کارکرد ماهانه
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckUserInformationLoadAccess_onPersonnelLoadStateInGanttChartSchema()
        { 
        }

        /// <summary>
        /// بررسی دسترسی اطلاعات تکمیلی پرسنل در نمای شبکه ای گزارش مدیریتی کارکرد ماهیانه 
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckUserInformationLoadAccess_onManagerLoadStateInGridSchema()
        { 
        }

        /// <summary>
        /// بررسی دسترسی اطلاعات تکمیلی پرسنل در نمای گرافیکی گزارش مدیریتی کارکرد ماهیانه
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckUserInformationLoadAccess_onManagerLoadStateInGanttChartSchema()
        { 
        }


    }
}
