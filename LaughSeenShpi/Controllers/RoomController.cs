using LaughSeenShpi.Models;
using Microsoft.AspNetCore.Mvc;
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
            return View(roomMembers);
        }
    }
}
