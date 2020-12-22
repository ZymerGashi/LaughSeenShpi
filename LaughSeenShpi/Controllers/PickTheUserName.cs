using LaughSeenShpi.DataAccess.Data;
using LaughSeenShpi.Models;
using LaughSeenShpi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaughSeenShpi.Controllers
{
    public class PickTheUserName : Controller
    {
        private readonly ApplicationDbContext _db;
        private DbSet<RoomMembers> dbSet;
        public PickTheUserName(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = db.Set<RoomMembers>();
        }




        public IActionResult Index(Room room)
        {
            ViewBag.Room = room;
            TempData["Room"] = JsonConvert.SerializeObject(room);

            return View();
        }



        [HttpPost]
        public IActionResult Post(RoomMembers roomMember)
        {
            dbSet.Add(roomMember);

            _db.SaveChanges();

            RoomMembers roomMembersWithRoomData = _db.RoomMembers.Where(rm => rm.MemberID == roomMember.MemberID).Include(rm => rm.Room).FirstOrDefault();
            // ViewBag.messages = messages;
            //return View("Room/Index",room);

            return RedirectToAction("Index", "Room", roomMembersWithRoomData);
        }



        public IEnumerable<Messages> GetMessages(int RoomId)
        {

            var messages = _db.Room.Where(s => s.ID == RoomId)
                .Join(_db.RoomMembers, r => r.ID, rm => rm.MemberRoomId, (r, rm) => rm)
                .Join(_db.Messages, rm => rm.MemberID, m => m.RoomMemberId, (rm, m) => m).Include(m=>m.RoomMembers)
                .ToList();

            return messages;
        }

    }

}