using Chat.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Models.Models
{
    public class History
    {
        public History()
        {
            Id = Guid.NewGuid();

        }
        [Key]
        public Guid Id { get; set; }
        public string SenderId { get; set; }
        public virtual ApplicationUser Sender { get; set; }
        public virtual ApplicationUser Reciver { get; set; }
        public virtual Theard Theard { get; set; }
        public string ReciverId { get; set; }
        public string TheardId { get; set; }
        public Statues Statues { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }
}
