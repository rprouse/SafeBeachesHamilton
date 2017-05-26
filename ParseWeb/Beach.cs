using System;
using System.Collections.Generic;
using System.Text;

namespace ParseWeb
{
    public class Beach
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public static Beach[] Beaches = new[]
        {
            new Beach { Id = 1, Name = "Bayfront Park Beach" },
            new Beach { Id = 2, Name = "Beach Boulevard" },
            new Beach { Id = 3, Name = "Binbrook Conservation Area Beach" },
            new Beach { Id = 4, Name = "Christie Conservation Area Beach'" },
            new Beach { Id = 5, Name = "Confederation Park Beach" },
            new Beach { Id = 6, Name = "Pier 4 Park Beach" },
            new Beach { Id = 7, Name = "Valens Conservation Area Beach" },
            new Beach { Id = 8, Name = "Van Wagner's Beach" }
        };
    }
}
