﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LaughSeenShpi.Models
{
    public class Room
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

    }
}
