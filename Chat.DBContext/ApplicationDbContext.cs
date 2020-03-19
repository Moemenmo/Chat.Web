using Chat.Models;
using Chat.Models.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DBContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<Connections> Connections { get; set; }
        public virtual DbSet<Theard> Theard { get; set; }
        

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
