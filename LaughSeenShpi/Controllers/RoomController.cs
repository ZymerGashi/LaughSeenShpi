using LaughSeenShpi.Models;
using LaughSeenShpi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaughSeenShpi.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult Index(RoomMembers roomMembers)
        {
            //ViewBag.messages = JsonConvert.DeserializeObject<IEnumerable<Messages>>( TempData["messages"].ToString());
            ViewBag.Room= JsonConvert.DeserializeObject<Room>(TempData["Room"].ToString());

            return View(roomMembers);
        }
    }
}
