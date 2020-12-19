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




        /*
         * 
         * [HttpGet]
         * 
         * public IEnumerable<Messages> GetMessages(int RoomId)
         {

             var messages = _db.Room.Where(s => s.ID == RoomId).Join(_db.RoomMembers, r => r.ID, rm => rm.Room,(r,rm)=>new RoomMembers() { ID=rm.ID, Name=rm.Name,Room=rm.Room }).ToList();

             return messages;

         }
        */






    }
}
