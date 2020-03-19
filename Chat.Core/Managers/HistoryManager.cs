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
    public class HistoryManager : Repository<History, ApplicationDbContext>
    {
        static HistoryManager manager;

        private HistoryManager (ApplicationDbContext context) : base(context)
        {
        }

        public static HistoryManager GetInstance(ApplicationDbContext context)
        {
            if (manager == null)
            {
                manager = new HistoryManager(context);
            }
            return manager;
        }
        public List<History> GetByTheardId(string id)
        {
            return manager.GetAll().Where(e=>e.TheardId==id).ToList();
        }

    }
}
