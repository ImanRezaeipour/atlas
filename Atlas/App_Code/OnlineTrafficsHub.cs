using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using OnlineTrafficsServiceReference;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Microsoft.AspNet.SignalR.Hubs;
using System.ServiceModel;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business;
using GTS.Clock.Model;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.Security;
using System.Globalization;
using System.IO;
using System.Web.Configuration;

[HubName("OnlineTrafficsHub")]
public class OnlineTrafficsHub : Hub, IOnlineTrafficsServiceCallback
{
    public JavaScriptSerializer JsSerializer
    {
        get
        {
            return new JavaScriptSerializer();
        }
    }

    public override Task OnConnected()
    {
        decimal userID = 0;
        BUser userBuisiness = new BUser();
        Context.RequestCookies.Add(Context.ConnectionId, new Cookie(Context.ConnectionId, Context.ConnectionId));
        if (HttpContext.Current.User.Identity.IsAuthenticated)
            userID = userBuisiness.GetByUsername(HttpContext.Current.User.Identity.Name).ID;
        else
            userID = 0;
        Context.RequestCookies.Add("UserID", new Cookie("UserID", userID.ToString()));
        InstanceContext instanceContext = new InstanceContext(this);
        OnlineTrafficsServiceClient clientProxy = new OnlineTrafficsServiceClient(instanceContext);
        clientProxy.InitializeClient();
        return base.OnConnected();
        
    }

    public override Task OnDisconnected(bool stopCalled)
    {
        return base.OnDisconnected(stopCalled);
    }

    public override Task OnReconnected()
    {
        return base.OnReconnected();
    }

    public void RecieveTrafficProxy(TrafficProxy trafficProxy)
    {
        //BPerson bPerson = new BPerson();
        //PersonControlStationExistanceState pcses = bPerson.CheckPersonnelExistanceInControlStation(trafficProxy.PersonID);
        //if(pcses == PersonControlStationExistanceState.Exits || pcses == PersonControlStationExistanceState.Undefined)
        //   this.SendTraffic(this.JsSerializer.Serialize(trafficProxy));
        //................................
        BPerson bPerson = new BPerson();
        decimal userID = decimal.Parse(Context.RequestCookies["UserID"].Value, CultureInfo.InstalledUICulture);
        Person person = bPerson.GetByID(trafficProxy.PersonID);
        PersonControlStationExistanceState pcses = bPerson.CheckPersonnelExistanceInControlStation(userID, person);
        if (pcses == PersonControlStationExistanceState.Exits || pcses == PersonControlStationExistanceState.Undefined)
        {
            if (person.PersonDetail != null)
            {
                trafficProxy.PersonImage = this.GetPersonImage(person.PersonDetail.Image);
            }
            switch (BLanguage.CurrentSystemLanguage)
            {
                case LanguagesName.Parsi:
                    if (trafficProxy.Date != null)
                    {
                        trafficProxy.TheDate = Utility.ToPersianDate(trafficProxy.Date);
                    }
                    break;
                case LanguagesName.English:
                    if (trafficProxy.Date != null)
                    {
                        trafficProxy.TheDate = Utility.ToString(trafficProxy.Date);
                    }
                    break;
            }
            trafficProxy.TheTime = Utility.IntTimeToRealTime(trafficProxy.Time);
            this.SendTraffic(this.JsSerializer.Serialize(trafficProxy));
        }
    }
    private string GetPersonImage(string ImageName)
    {
        byte[] buffer = null;
        string myFileExtention = string.Empty;
        string imageSource = string.Empty;
        string attachmentKey = AppFolders.PersonnelImages.ToString();
        string imagePath = AppDomain.CurrentDomain.BaseDirectory + attachmentKey + "\\" + ImageName; 
        if (File.Exists(imagePath))
        {
            buffer = File.ReadAllBytes(imagePath);
            myFileExtention = Path.GetExtension(imagePath);
            myFileExtention = myFileExtention.Replace(".", string.Empty);
        }

        else
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\ClientAttachments\\user.png"))
                buffer = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "ClientAttachments\\user.png");
            myFileExtention = Path.GetExtension(AppDomain.CurrentDomain.BaseDirectory + "ClientAttachments\\user.png");
            myFileExtention = myFileExtention.Replace(".", string.Empty);
        }
        imageSource = string.Format("data:image/{0};base64,", myFileExtention) + Convert.ToBase64String(buffer);
        return imageSource;
    }

    [HubMethodName("SendTraffic")]
    public void SendTraffic(string message)
    {
        IHubContext context = GlobalHost.ConnectionManager.GetHubContext<OnlineTrafficsHub>();
        context.Clients.Client(Context.ConnectionId).RecieveTraffic(message);
    }

}
