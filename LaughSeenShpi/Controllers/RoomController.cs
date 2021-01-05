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
    public class RoomController : Controller
    {
        private readonly ApplicationDbContext _db;


        public RoomController(ApplicationDbContext db)
        {
            _db = db;

        }



        public IActionResult Index(RoomMembers roomMembersWithRoomData)
        {
            //ViewBag.messages = JsonConvert.DeserializeObject<IEnumerable<Messages>>( TempData["messages"].ToString());
           //ViewBag.Room= JsonConvert.DeserializeObject<Room>(TempData["Room"].ToString());

            RoomMembers roomMembersAndRoom = _db.RoomMembers.Where(rm => rm.MemberID == roomMembersWithRoomData.MemberID).Include(rm => rm.Room).FirstOrDefault();

            //need to delete this and uncomment the first above line
            //ViewBag.Room = new Room();


            return View(roomMembersAndRoom);
        }
    }
}
