using System;
using System.Collections.Generic;

#nullable disable

namespace CHU_PolicyPlatform_1.Models
{
    public partial class Proposal
    {
        public Proposal()
        {
            ToReponds = new HashSet<ToRepond>();
            Votes = new HashSet<Vote>();
        }

        public string ProposalId { get; set; }
        public string Title { get; set; }
        public string Pcontent { get; set; }
        public string GainsInfluential { get; set; }
        public DateTime Pdate { get; set; }
        public bool Underways { get; set; }
        public bool? Pass { get; set; }
        public string UserId { get; set; }
        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ToRepond> ToReponds { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
