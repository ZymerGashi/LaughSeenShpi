using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LaughSeenShpi.Models
{
    public class Messages
    {
        [Key]
        public int ID { get; set; }

        public string Content { get; set; }

        public RoomMembers RoomMember { get; set; }

        public DateTime SendTime{ get; set; }

        public DateTime SeenTime { get; set; }


    }
}
