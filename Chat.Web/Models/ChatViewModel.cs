using Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.Web.Models
{
    public class ChatViewModel
    {
        public ChatViewModel()
        {
            Contacts = new List<ApplicationUser>();
            MesseagesViewModel = new MesseagesViewModel();
        }
        public ApplicationUser  User{ get; set; }
        public List<ApplicationUser> Contacts{ get; set; }
        public MesseagesViewModel MesseagesViewModel { get; set; }
    }
}