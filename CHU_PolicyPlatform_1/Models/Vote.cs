using System;
using System.Collections.Generic;

#nullable disable

namespace CHU_PolicyPlatform_1.Models
{
    public partial class Vote
    {
        public string UserId { get; set; }
        public string ProposalId { get; set; }
        public string Vcontent { get; set; }
        public bool Crucial { get; set; }
        public DateTime Vdate { get; set; }

        public virtual Proposal Proposal { get; set; }
        public virtual User User { get; set; }
    }
}
