﻿using System;
using System.Collections.Generic;

namespace GasStationData.Models
{
    public partial class User
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
    }
}
