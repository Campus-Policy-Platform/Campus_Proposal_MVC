using CHU_PolicyPlatform_1.Models;
using System.Collections.Generic;

namespace CHU_PolicyPlatform_1.ViewModels
{
    public class JoinedViewModel
    {
        public List<Proposal> MyProps { get; set; }
        public List<Proposal> VotedProps { get; set;}
        public List<Category> Categories { get; set; }
    }
}
