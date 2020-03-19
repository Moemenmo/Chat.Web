using Chat.DBContext;
using Chat.Models.Models;
using Chat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Core.Managers
{
    public class TheardManager : Repository<Theard, ApplicationDbContext>
    {
        static TheardManager manager;

        private TheardManager(ApplicationDbContext context) : base(context)
        {
        }

        public static TheardManager GetInstance(ApplicationDbContext context)
        {
            if (manager == null)
            {
                manager = new TheardManager(context);
            }
            return manager;
        }
    }
}
