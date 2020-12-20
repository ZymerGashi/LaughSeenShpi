using System;
using System.Collections.Generic;
using System.Text;

namespace LaughSeenShpi.Models.ViewModels
{
    public class MemberMessagesViewModel
    {
        public RoomMembers roommembers { get; set; }

        public IEnumerable<Messages> messages { get; set; }




    }
}
