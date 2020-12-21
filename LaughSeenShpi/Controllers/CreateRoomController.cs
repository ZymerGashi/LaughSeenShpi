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
    public class CreateRoomController : Controller
    {

        private readonly ApplicationDbContext _db;
        private DbSet<Room> dbSet;
        public CreateRoomController(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = db.Set<Room>();
        }




        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Post(Room room)
        {

            Room existingRoom;

            existingRoom = _db.Room.Where(s => s.Name == room.Name).FirstOrDefault();



            if (existingRoom == null)
            {


                dbSet.Add(room);

                _db.SaveChanges();
            }
            //return View("Room/Index",room);



            return RedirectToAction("Index", "PickTheUserName", existingRoom==null ?  room:existingRoom);
        
        }





     
    }
}
