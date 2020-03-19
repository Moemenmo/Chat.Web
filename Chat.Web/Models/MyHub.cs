using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Chat.Core;
using Chat.DBContext;
using Chat.Models.Enums;
using Chat.Models.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;

namespace Chat.Web.Models
{
    public class MyHub : Hub
    {
        readonly UnitOfWork Ctx = new UnitOfWork(new ApplicationDbContext());
        readonly ApplicationDbContext ctx = new ApplicationDbContext();
        public void Send(String ReciverID, string msg)
        {
            var userManager = Ctx.ApplicationUserManager;
            var theardManager = Ctx.TheardManager;
            var historyManager = Ctx.HistoryManager;

            var reciver = ctx.Users.Find(ReciverID);
            var sender = ctx.Users.Find(Context.User.Identity.GetUserId());
            var reciverlList=reciver.ConIDs.Select(e=>e.ConnectionId).ToList();
            var msgStatues = (reciverlList.Count != 0);
            reciverlList.AddRange(sender.ConIDs.Select(e=>e.ConnectionId));
            var theard = sender.Theard.FirstOrDefault(e => e.Users.Contains(reciver));
            if (theard==null)
            {
                theard = new Theard();
                //theardManager.Add(theard);
                theard.Users.Add(sender);
                theard.Users.Add(reciver);
                sender.Theard.Add(theard);
                reciver.Theard.Add(theard);
                //theardManager.Update(theard);
                theard.Users.OrderBy(e => e.UserName);
                ctx.Theard.Add(theard);
                ctx.SaveChanges();

            }
            var history = new History
            {
                Reciver = reciver,
                Sender = sender,
                Message = msg,
                Statues = Statues.Sent,
                DateTime = DateTime.Now
                    
                };
            if (msgStatues)
            {
                history.Statues = Statues.Delivered;
            }
                //historyManager.Add(history);
                theard.History.Add(history);
                ctx.History.Add(history);
                ctx.SaveChanges();
                ctx.Theard.Attach(theard);
                ctx.Entry(theard).State = EntityState.Modified;
                ctx.SaveChanges();
            var orderedusers = theard.Users.OrderBy(e => e.UserName);
            var id = "";
            foreach (var item in orderedusers)
            {
                id += item.Id;
            }
            Clients.Clients(reciverlList).newMessage(sender.Id.ToString(), msg,id);

        }
        //public void JoinGroup(String name, string gname)
        //{
        //    Groups.Add(Context.ConnectionId, gname);
        //    Clients.Group(gname, Context.ConnectionId).newMessage(name, "Joined " + gname);
        //}
        //public void SendGroup(string name, string gname, string message)
        //{
        //    Clients.Group(gname).newMessage(name, message);
        //}
        public override Task OnConnected()
        {
            var userId = Context.User.Identity.GetUserId();
            var user = ctx.Users.FirstOrDefault(e => e.Id == userId);
            var conID = new Connections
            {
                ConnectionId = Context.ConnectionId
            };
            user.ConIDs.Add(conID);
            ctx.Users.Attach(user);
            ctx.Entry(user).State = EntityState.Modified;
            ctx.SaveChanges();
            return base.OnConnected();

        }
        public override Task OnDisconnected(bool stopCalled)
        {
            var userId = Context.User.Identity.GetUserId();
            var user = ctx.Users.FirstOrDefault(e => e.Id == userId);
            user.ConIDs.Clear();
            ctx.Users.Attach(user);
            ctx.Entry(user).State = EntityState.Modified;
            ctx.SaveChanges();
            return base.OnDisconnected(stopCalled);

        }
        public override Task OnReconnected()
        {
            var userId = Context.User.Identity.GetUserId();
            var user = ctx.Users.FirstOrDefault(e => e.Id == userId);
            user.ConIDs.Clear();
            ctx.Users.Attach(user);
            ctx.Entry(user).State = EntityState.Modified;
            ctx.SaveChanges();
            var conID = new Connections
            {
                ConnectionId = Context.ConnectionId
            };
            user.ConIDs.Add(conID);
            ctx.Users.Attach(user);
            ctx.Entry(user).State = EntityState.Modified;
            ctx.SaveChanges();
            return base.OnReconnected();
        }
    }
}