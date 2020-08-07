using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;

namespace GTS.Clock.Model.Concepts
{
    public enum SortOrder
    {
        asc, desc
    }

    public class TrafficProxy : BasicTraffic
    {
        private BasicTraffic baseicTraffic = null;
        private PermitPair permitPair = null;

        public TrafficProxy() { }
        public TrafficProxy(BasicTraffic traffic) 
        {
            this.baseicTraffic = traffic;
            this.Time = traffic.Time;
            this.Date = traffic.Date;
            this.ID = traffic.ID;
            this.Precard = traffic.Precard;
        }
        public TrafficProxy(DateTime date, int time, PermitPair permit) 
        {
            this.permitPair = permit;
            this.Time = time;
            this.Date = date;
            this.ID = permit.ID;
            this.Precard = permitPair.Precard;

        }

        public TrafficProxy(DateTime date, int time,Precards precard, PermitPair permit)
        {
            this.permitPair = permit;
            this.Time = time;
            this.Date = date;
            this.ID = permit.ID;
            this.Precard = this.GetPrecardId(precard);

        }
       
        public bool IsTraffic
        {
            get
            {
                if (baseicTraffic != null) return true;
                return false;
            }
        }
       
        public bool IsPermit
        {
            get
            {
                if (permitPair != null) return true;
                return false;
            }
        }

        public bool Used
        {
            get
            {
                if (IsTraffic) return baseicTraffic.Used;
                else if (IsPermit) return permitPair.IsApplyedOnTraffic;
                else return false;
            }
            set
            {
                if (IsTraffic)
                    baseicTraffic.Used = value;
                else if (IsPermit)
                    permitPair.IsApplyedOnTraffic = value;
            }
        }

        public override string ToString()
        {
            return String.Format("{0} ,{1} , {2}", this.IsTraffic ? "Traffic" : "Permit", Utility.ToPersianDate(Date), Utility.IntTimeToRealTime(Time));
        }


        private Precard GetPrecardId(Precards precards)
        {
            switch (precards)
            {
                case Precards.Usual:
                    return new Precard() { ID = 8832 };
                case Precards.Enter:
                    return new Precard() { ID = 4 };
                case Precards.Exit:
                    return new Precard() { ID = 5 };
                default:
                    return new Precard() { ID = 8832 };

            }
        }
    }
    public class BasicTrafficController
    {
        /// <summary>
        /// صعودی یا نزولی
        /// </summary>
       
        #region variable
        const int NotPaired = -1000;   
        private int curentIndex = 0;
        //private List<BasicTraffic> basicTrafficList = new List<BasicTraffic>();
        private List<TrafficProxy> trafficProxyList = new List<TrafficProxy>();

        #endregion        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trafficList">یک لیست از ترددها که میخواهیم آنرا پردازش کنیم</param>
        public BasicTrafficController(List<BasicTraffic> trafficList, IList<Permit> permitList,bool checkEnterExitOnRequest)
        {
            IList<Permit> localPermitList = permitList.Where(x => x.Pairs.Where(y => !y.IsApplyedOnTraffic && y.PreCardID == 8832).Count() > 0)
                            .OrderBy(x => x.FromDate).ToList();
            if (trafficList != null)
            {
               IList<BasicTraffic> localTrafficList = trafficList.Where(x => x.Active).OrderBy(x => x.Date).ThenBy(x => x.Time).ToList();
               for (int i = 0; i < localTrafficList.Count(); i++)
                {
                    if (localTrafficList[i].Used == false)
                    {
                        for (int j = i; j < localTrafficList.Count; j++)
                        {
                            trafficProxyList.Add(new TrafficProxy(localTrafficList[j]));
                        }
                        break;
                    }
                }
            }
            
            if (localPermitList != null)
            {
                foreach (Permit permit in localPermitList)
                {
                    foreach (PermitPair permitPair in permit.Pairs)
                    {
                        ///در حالتی که ورود و خروج در یک درخواست باشند جهت کار با آیدی یکسان در ترافیک مپر مجبوریم ورود خروج لحاظ کنیم
                        if (checkEnterExitOnRequest || (permitPair.From > 0 && permitPair.To > 0))
                        {
                            if (permitPair.From != NotPaired && !permitPair.IsApplyedOnTraffic && trafficList.Where(x => x.Time == permitPair.From && x.Date == permit.FromDate && x.Active).Count() == 0)
                            {
                                TrafficProxy proxyFrom = new TrafficProxy(permit.FromDate, permitPair.From, Precards.Enter, permitPair);
                                trafficProxyList.Add(proxyFrom);
                            }

                            if (permitPair.To != NotPaired && !permitPair.IsApplyedOnTraffic && trafficList.Where(x => x.Time == permitPair.To && x.Date == permit.FromDate && x.Active).Count() == 0)
                            {
                                TrafficProxy proxyTo = new TrafficProxy(permit.FromDate, permitPair.To, Precards.Exit, permitPair);
                                trafficProxyList.Add(proxyTo);
                            }
                        }
                        else
                        {
                            if (permitPair.From != NotPaired && !permitPair.IsApplyedOnTraffic && trafficList.Where(x => x.Active && x.Time == permitPair.From && x.Date == permit.FromDate).Count() == 0)
                            {
                                TrafficProxy proxyFrom = new TrafficProxy(permit.FromDate, permitPair.From, Precards.Usual, permitPair);
                                trafficProxyList.Add(proxyFrom);
                            }

                            if (permitPair.To != NotPaired && !permitPair.IsApplyedOnTraffic && trafficList.Where(x => x.Active && x.Time == permitPair.To && x.Date == permit.FromDate).Count() == 0)
                            {
                                TrafficProxy proxyTo = new TrafficProxy(permit.FromDate, permitPair.To, Precards.Usual, permitPair);
                                trafficProxyList.Add(proxyTo);
                            }
                        }
                    }
                }
            }
           

            /*if (trafficList != null)
            {
                basicTrafficList = trafficList.Where(x => x.Active).ToList();
                for (int i = 0; i < basicTrafficList.Count; i++)
                {
                    if (basicTrafficList[i].Used == false)
                    {
                        curentIndex = i;
                        break;
                    }
                }
            }
            else
            {
                basicTrafficList = new List<BasicTraffic>();
            }*/
            if (trafficProxyList != null)
            {
                trafficProxyList = trafficProxyList.OrderBy(x => x.Date).ThenBy(x => x.Time).ToList();
                curentIndex = 0;
            }
        }


        /// <summary>
        /// آیا آیتم بعدی وجود دارد
        /// </summary>
        public bool HasNextItem 
        {
            get 
            {
                if (curentIndex < trafficProxyList.Count - 1) 
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// آیا دوتا آیتم جلوتر وجود دارد
        /// </summary>
        public bool HasAfterNextItem
        {
            get
            {
                if (curentIndex < trafficProxyList.Count - 2)
                {
                    return true;
                }
                return false;
            }
        }
       

        /// <summary>
        /// آیتم فعلی
        /// </summary>
        public TrafficProxy CurrentBasicItem
        {
            get
            {
                if (trafficProxyList.Count > 0)
                {
                    return trafficProxyList[curentIndex];
                }
                else 
                {
                    throw new Exception("The List Is Empty: GTS.Clock.Model.Concepts.CurrentBasicItem");
                }
            }
        }

        /// <summary>
        /// آیتم قبلی
        /// </summary>
        public TrafficProxy BeforeItem 
        {
            get 
            {
                if (curentIndex > 0) 
                {
                    return trafficProxyList[curentIndex - 1];
                }
                return new TrafficProxy();
            }
        }

        /// <summary>
        /// آیتم بعدی
        /// </summary>
        public TrafficProxy NextItem
        {
            get
            {
                if (curentIndex < trafficProxyList.Count - 1)
                {

                    return trafficProxyList[curentIndex + 1];
                }
                return new TrafficProxy();
            }
        }

        /// <summary>
        /// دوتا آیتم جلوتر
        /// </summary>
        public TrafficProxy AfterNextItem
        {
            get
            {
                if (curentIndex < trafficProxyList.Count - 2)
                {
                    return trafficProxyList[curentIndex + 2];
                }
                return new TrafficProxy();
            }
        }        

        /// <summary>
        /// آیا همه آیتم ها استفاده شده اند
        /// </summary>
        public bool Finished
        {
            get
            {
                if (trafficProxyList.Count == 0) 
                {
                    return true;
                }
                if (curentIndex == trafficProxyList.Count - 1 && CurrentBasicItem.Used)
                {
                    return true;
                }
                else if (CurrentBasicItem.Used)
                {
                    for (int i = curentIndex; i < trafficProxyList.Count; i++)
                    {
                        if (trafficProxyList[i].Used == false)
                        {
                            return false;
                        }
                    }
                }
                if (CurrentBasicItem.Used)//از حلقه بالا عبور کرده است
                {
                    return true;
                }
               
                return false;
            }
        }

        /// <summary>
        /// هم آیتم جاری را بررسی مسکند آیتم بعدی
        /// </summary>
        public bool CanFindPair 
        {
            get 
            {
                return (!Finished) && HasNextItem;
            }
        }

        /// <summary>
        /// آیا آیتم جاری در بازه زمانی شیفت ورودی میگنجد
        /// </summary>
        /// <param name="shift">شیفت مقایسه</param>
        public bool IsTrafficInShift(AssignedWGDShift shift)
        {
            foreach (ShiftPair sp in shift.Pairs) 
            {
                if (trafficProxyList[curentIndex].Time <= sp.To && trafficProxyList[curentIndex].Time >= sp.From) 
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// آیا تردد ورودی در بازه زمانی شیفت ورودی میگنجد
        /// </summary>
        /// <param name="shift">شیفت مقایسه</param>
        /// <param name="basicTraffic">تردد مقایسه</param>
        /// <returns></returns>
        public bool IsTrafficInShift(AssignedWGDShift shift, BasicTraffic basicTraffic)
        {
            foreach (ShiftPair sp in shift.Pairs)
            {
                if (basicTraffic.Time <= sp.To && basicTraffic.Time >= sp.From)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// آیتم بعدی که استفاده نشده است را آیتم جاری قرار بده
        /// </summary>
        public void MoveCurrentToNextUnusedItem() 
        {
            for (int i = curentIndex ; i < trafficProxyList.Count; i++)
            {
                if (!trafficProxyList[i].Used) 
                {
                    curentIndex = i;
                    break;
                }
            }
        }


        /// <summary>
        /// مرتب سازی ترددهای پایه بر اساس زمان تردد
        /// </summary>
        /// <param name="order"></param>
        public void SortByTime(SortOrder order) 
        {
            if (order == SortOrder.asc)
            {
                trafficProxyList = trafficProxyList.OrderBy(x => x.Date).ThenBy(x => x.Time).ToList();
                 
            }
            else 
            {
                trafficProxyList = trafficProxyList.OrderByDescending(x => x.Date).ThenBy(x => x.Time).ToList();
                
            }
        }


        /// <summary>
        /// یک تردد پردازش نشده به لیست اضافه میکند
        /// </summary>
        /// <param name="_basicTraffic">این تردد باید با دقت در اضافه شود تا ترتیب لیست از بین نرود</param>
        public void InsertTrafficAfterCurentItem(BasicTraffic _basicTraffic1, BasicTraffic _basicTraffic2) 
        {
            trafficProxyList.Insert(curentIndex + 1, new TrafficProxy(_basicTraffic1));
            trafficProxyList.Insert(curentIndex + 2, new TrafficProxy(_basicTraffic2));
        }

        /// <summary>
        /// یک تردد پردازش نشده به لیست اضافه میکند
        /// </summary>
        /// <param name="_basicTraffic">این تردد باید با دقت در اضافه شود تا ترتیب لیست از بین نرود</param>
        public void InsertTrafficBeforeCurentItem(BasicTraffic _basicTraffic)
        {
            trafficProxyList.Insert(curentIndex, new TrafficProxy(_basicTraffic));
        }

        private void CheckDublicate() 
        {

        }

        /// <summary>
        /// تنظیم دوباره اولین آیتم
        /// </summary>
        public void Reset() 
        {
            for (int i = 0; i < trafficProxyList.Count; i++)
            {
                if (trafficProxyList[i].Used == false)
                {
                    curentIndex = i;
                    break;
                }
            }
        }


        
    }
}
