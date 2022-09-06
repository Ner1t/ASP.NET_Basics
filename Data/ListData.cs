using Entities;
using System;
using System.Collections.Generic;

namespace Data
{
    public class ListData
    {
        public List<User> Users = new List<User>
        {
           new User { Id= 1, FirstName =  "Roman",  LastName =  "Alliance", BirthDate =  new DateTime( 1993,05,27),  },
           new User { Id = 2, FirstName = "Kirill",   LastName =  "Horde",  BirthDate =  new DateTime( 2021,10,28) },
           new User {Id = 3, FirstName = "Maria",   LastName =  "Horde",  BirthDate =  new DateTime( 1998,01,17) }
        };

        public List<Reward> Rewards = new List<Reward>
        {
           new Reward {  Id= 1, Title = " Going Down? ",  Description =  "Fall 65 yards without dying." },
           new Reward { Id= 2, Title = " Friend or Fowl? ", Description = "Slay 15 turkeys in 3 minutes." },
           new Reward {  Id=3,Title = " Ready, Set, Goat! ", Description ="Using the Billy Goat Blaster or the Billy Goat Blaster DX, blast 12 Billy Goats in under 1 minute." },
            new Reward { Id= 4, Title =  " Turkey Lurkey ", Description ="Blast those dirty, sneaking Rogues with your Turkey Shooter." }
        };
    }
}
