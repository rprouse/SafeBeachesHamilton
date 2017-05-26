using System;
using System.Collections.Generic;
using System.Text;

namespace ParseWeb
{
    public class Reading
    {
        public Beach Beach { get; set; }

        public OpenStatus Open { get; set; }

        public DateTime? DateTested { get; set; }

        public DateTime DateAdded { get; set; }

        public int Temperature { get; set; }

        public string Message { get; set; }
    }
}
