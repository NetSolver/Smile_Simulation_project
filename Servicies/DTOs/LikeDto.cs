﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class LikeDto
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }

}
