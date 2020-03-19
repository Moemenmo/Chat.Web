using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Models.Models
{
    public class Connections
    {
        public Connections()
        {
            Id = Guid.NewGuid();

        }
        [Key]
        public Guid Id { get; set; }
        public string ConnectionId { get; set; }
    }
}
