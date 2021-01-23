using LaughSeenShpi.DataAccess.Data;
using LaughSeenShpi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PusherServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaughSeenShpi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReadWriteMessagesController : ControllerBase
    {

        private readonly ApplicationDbContext _db;
        private readonly DbSet<Messages> dbSet;

        public ReadWriteMessagesController(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<Messages>();
        }




        
         
         [HttpGet("{RoomId}")]
         
          public IEnumerable<Messages> GetMessages(int RoomId)
        {

            var messages = _db.Room.Where(s => s.ID == RoomId)
            .Join(_db.RoomMembers, r => r.ID, rm => rm.MemberRoomId, (r, rm) => rm)
            .Join(_db.Messages, rm => rm.MemberID, m => m.RoomMemberId, (rm, m) => m).Include(rm=>rm.RoomMembers).ThenInclude(rm=>rm.Room)
            .ToList().OrderBy(m=>m.SendTime);

            return messages;

        }

        public async Task<IActionResult> AddMessages(Messages message)
        {

            if (message.Content!="")
            {

                message.SeenTime = DateTime.Now;
                message.SendTime = DateTime.Now; //this needs to be changed in order to have the logic when the message is seen





                _db.Messages.Add(message);

                _db.SaveChanges();

                var messagesPlusSender = _db.Messages.Where(m => m.ID == message.ID).Include(m => m.RoomMembers).Where(m => m.RoomMembers.MemberID == message.RoomMemberId).FirstOrDefault();

                var room = _db.RoomMembers.Where(s => s.MemberID == message.RoomMemberId)
      .Join(_db.Room, rm => rm.MemberRoomId, r => r.ID, (rm, r) => r).FirstOrDefault();

                MessagePlusRoom messagePlusRoom = new MessagePlusRoom { room = room, messages = messagesPlusSender };



                var options = new PusherOptions
                {
                    Cluster = "eu",
                    Encrypted = true
                };
                var pusher = new Pusher(
                    "1126876",
                    "07088996639e59625df1",
                    "588dfb9942c061b04ff6",
                    options
                );

                var result = await pusher.TriggerAsync(
                     room.ID.ToString(),
                     "new_message",
                     new { messagePlusRoom },
                     new TriggerOptions() { SocketId = message.socketId });


                return new ObjectResult(new { status = "success", data = message });
            }
            return new ObjectResult(new { status = "erroo"});

        }


        public async  void UpdateRoom(RoomMembers roomMember)
        {
            var roomFromDb = _db.Room.Where(r=>r.ID== roomMember.Room.ID).FirstOrDefault();

            roomFromDb.CurrentTime = roomMember.Room.CurrentTime;
            roomFromDb.PlayTheMovie = roomMember.Room.PlayTheMovie;

            _db.SaveChanges();

            var options = new PusherOptions
            {
                Cluster = "eu",
                Encrypted = true
            };
            var pusher = new Pusher(
                "1126876",
                "07088996639e59625df1",
                "588dfb9942c061b04ff6",
                options
            );

            var result = await pusher.TriggerAsync(
                 roomMember.Room.ID.ToString(),
                 "video_parameters_changed",
                 new { roomMember },
                 new TriggerOptions() {  });

        }






    }


    public class MessagePlusRoom
    {

        public Room room { get; set; }

        public Messages messages { get; set; }

    }

}
