using System;
using System.Collections.Generic;

#nullable disable

namespace CHU_PolicyPlatform_1.Models
{
    public partial class User
    {
        public User()
        {
            Proposals = new HashSet<Proposal>();
            Votes = new HashSet<Vote>();
        }

        public string UserId { get; set; }
        public string Upassword { get; set; }

        public virtual ICollection<Proposal> Proposals { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
