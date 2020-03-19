using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Models.Models
{
    public class Theard
    {
        public Theard()
        {
            Id = Guid.NewGuid();
            Users = new HashSet<ApplicationUser>();
            History = new HashSet<History>();
            
        }
        [Key]
        public Guid Id { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<History> History { get; set; }
    }
}
