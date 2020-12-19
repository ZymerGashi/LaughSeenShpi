using LaughSeenShpi.DataAccess.Data;
using LaughSeenShpi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return View();
        }



        [HttpPost]
        public IActionResult Post(RoomMembers roomMember)
        {
            dbSet.Add(roomMember);

            _db.SaveChanges();

            //return View("Room/Index",room);

            return RedirectToAction("Index", "Room", roomMember);

        }



    }
}
