using Chat.Core;
using Chat.DBContext;
using Chat.Models;
using Chat.Models.Enums;
using Chat.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chat.Web.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        ApplicationDbContext ctx = new ApplicationDbContext(); 
        public UnitOfWork UnitOfWork
        {
            get
            {
                return HttpContext.GetOwinContext().Get<UnitOfWork>();
            }
        }
        // GET: Chat
        public ActionResult Index()
        {
            var cVM = new ChatViewModel();
            var userM = UnitOfWork.ApplicationUserManager;
            var users = userM.Users.ToList();
            var user = userM.FindById(User.Identity.GetUserId());
            users.Remove(user);
            cVM.Contacts.AddRange(users);
            cVM.User = user;

            return View(cVM);
        }
        [HttpGet]
        public ActionResult ShowTheard(string userid)
        {
            MesseagesViewModel MVM = new MesseagesViewModel();
            var userM = UnitOfWork.ApplicationUserManager;
            var historyM = UnitOfWork.HistoryManager;
            var user = userM.FindById(User.Identity.GetUserId());
            var reciver = userM.FindById(userid);
            var theard = user.Theard.Where(e => e.Users.Contains(reciver)).ToList();
            foreach (var item in theard[0].History)
            {
                if (item.ReciverId==user.Id)
                {
                    item.Statues = Statues.Seen;
                    ctx.History.Attach(item);
                    ctx.Entry(item).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
                
            }
            var users = new List<ApplicationUser>
            {
                user,
                reciver
               
            };
            Console.WriteLine("lamiaaaaa");
          var orderedusers= users.OrderBy(e => e.UserName);
            foreach (var item in orderedusers)
            {
                MVM.Id += item.Id;
            }
            MVM.UserId = user.Id;
            MVM.ContactId = reciver.Id;
            MVM.History = theard[0].History.OrderBy(d => Convert.ToDateTime(d.DateTime)).ToList();
            return PartialView("_ChatBody",MVM);
        }
        
    }
}