using CHU_PolicyPlatform_1.Models;
using System.Collections.Generic;

namespace CHU_PolicyPlatform_1.ViewModels
{
    public class ReviewtotalViewModel
    {
        public Proposal proposal { get; set; }

        public ToRepond toRepond { get; set; }

        public List<Vote> votes { get; set;}
    }
}
