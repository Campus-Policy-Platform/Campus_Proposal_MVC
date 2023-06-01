using System;
using System.Collections.Generic;

#nullable disable

namespace CHU_PolicyPlatform_1.Models
{
    public partial class ToRepond
    {
        public string ProposalId { get; set; }
        public string GerentId { get; set; }
        public string Rcontent { get; set; }

        public virtual Gerent Gerent { get; set; }
        public virtual Proposal Proposal { get; set; }
    }
}
