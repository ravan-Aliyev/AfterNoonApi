﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AfterNoonV2.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string NameSurname { get; set; }
        public string? RefreshToken { get; set; }

        public DateTime? EndDate { get; set; }

        public ICollection<Basket> Baskets { get; set; }
    }
}
