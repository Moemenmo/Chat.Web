using Chat.Models;
using Chat.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.Web.Models
{
    public class MesseagesViewModel
    {
        public MesseagesViewModel()
        {
            History = new List<History>();
            Id = "";
        }
        public string Id{ get; set; }
        public List<History> History{ get; set; }
        public String UserId{ get; set; }
        public String ContactId { get; set; }
    }
}