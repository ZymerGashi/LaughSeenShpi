using LaughSeenShpi.DataAccess.Data;
using LaughSeenShpi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaughSeenShpi.Controllers
{
    [Route("api/[controller]")]
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




        
         
         [HttpGet]
         
          public IEnumerable<Messages> GetMessages(int RoomId)
        {

            var messages = _db.Room.Where(s => s.ID == RoomId)
                .Join(_db.RoomMembers, r => r.ID, rm => rm.MemberRoomId, (r, rm) => new RoomMembers() { MemberID = rm.MemberID, MemberName = rm.MemberName, MemberRoomId = rm.MemberRoomId })
                .Join(_db.Messages, rm=>rm.MemberRoomId,m=>m.RoomMemberId,(rm,m)=> new Messages() {ID=m.ID,Content=m.Content,RoomMemberId=m.RoomMemberId,SendTime=m.SendTime,SeenTime=m.SeenTime})
                .ToList();

            return messages;

        }

        public IActionResult AddMessages(Messages message )
        {

            message.SeenTime = DateTime.Now;
            message.SendTime = DateTime.Now; //this needs to be changed in order to have the logic when the message is seen



            _db.Messages. Add(message);

            _db.SaveChanges();

            return new ObjectResult(new { status = "success", data = message });
        }





    }
}
