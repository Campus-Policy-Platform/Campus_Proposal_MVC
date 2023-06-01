using System;
using System.Collections.Generic;

#nullable disable

namespace CHU_PolicyPlatform_1.Models
{
    public partial class Gerent
    {
        public Gerent()
        {
            ToReponds = new HashSet<ToRepond>();
        }

        public string GerentId { get; set; }
        public string Gpassword { get; set; }

        public virtual ICollection<ToRepond> ToReponds { get; set; }
    }
}
