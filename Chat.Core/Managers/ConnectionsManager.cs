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
    public class ConnectionsManager : Repository<History, ApplicationDbContext>
    {
        static ConnectionsManager manager;

        private ConnectionsManager(ApplicationDbContext context) : base(context)
        {
        }

        public static ConnectionsManager GetInstance(ApplicationDbContext context)
        {
            if (manager == null)
            {
                manager = new ConnectionsManager(context);
            }
            return manager;
        }
    }
}
