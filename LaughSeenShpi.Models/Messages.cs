using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LaughSeenShpi.Models
{
    public class Messages
    {
        [Key]
        public int ID { get; set; }

        public string Content { get; set; }

        [ForeignKey("RoomMembers")]
        public int RoomMemberId { get; set; }

        public RoomMembers RoomMembers { get; set; }


        public DateTime SendTime{ get; set; }

        public DateTime SeenTime { get; set; }


    }
}
