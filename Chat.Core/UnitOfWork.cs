using Chat.Core.Managers;
using Chat.DBContext;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Core
{
    public class UnitOfWork : IDisposable
    {
        ApplicationDbContext context;
        

        public UnitOfWork(IOwinContext owinContext)
        {
            context = owinContext.Get<ApplicationDbContext>();
            ApplicationUserManager = owinContext.Get<ApplicationUserManager>();
            ApplicationRoleManager = owinContext.Get<ApplicationRoleManager>();
            ApplicationSignInManager = owinContext.Get<ApplicationSignInManager>();
        }

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            this.context = applicationDbContext;
        }

        public static UnitOfWork Create(IdentityFactoryOptions<UnitOfWork> options, IOwinContext owinContext)
        {
            return new UnitOfWork(owinContext);
        }


        
        public HistoryManager HistoryManager
        {
            get
            {
                return HistoryManager
                    .GetInstance(context);
            }
        }
        public TheardManager TheardManager
        {
            get
            {
                return TheardManager
                    .GetInstance(context);
            }
        }
        public ConnectionsManager ConnectionsManager
        {
            get
            {
                return ConnectionsManager.GetInstance(context);
            }
        }
        public ApplicationUserManager ApplicationUserManager { get; }

        public ApplicationRoleManager ApplicationRoleManager { get; }

        public ApplicationSignInManager ApplicationSignInManager { get; }

        public void Dispose()
        {

        }
    }
}
