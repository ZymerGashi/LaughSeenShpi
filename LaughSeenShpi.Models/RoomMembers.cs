using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LaughSeenShpi.Models
{
    public class RoomMembers
    {
        [Key]

        public int MemberID { get; set; }

        public string MemberName { get; set; }

        [ForeignKey("Room")] 
        public int MemberRoomId { get; set; }

        public Room Room { get; set; }

    }
}
