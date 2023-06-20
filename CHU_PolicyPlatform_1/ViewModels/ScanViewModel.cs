using CHU_PolicyPlatform_1.Models;
using System.Collections.Generic;

namespace CHU_PolicyPlatform_1.ViewModels
{
    public class ScanViewModel
    {
        public List<Proposal> Proposals {  get; set; }
        public List<Vote> Votes { get; set; }
        public Proposal proposal { get; set; }
    }
}
