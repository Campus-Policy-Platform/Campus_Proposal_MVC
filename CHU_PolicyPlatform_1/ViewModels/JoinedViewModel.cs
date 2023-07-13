using CHU_PolicyPlatform_1.Models;
using System.Collections.Generic;

namespace CHU_PolicyPlatform_1.ViewModels
{
    public class JoinedViewModel
    {
        public GerentSeeVM GeseVM { set; get; }
        public List<Proposal> Propsunde { get; set; }
        public List<Proposal> Propspass { get; set; }
        public GerentSeeVM VgeseVM { set; get; }
        public List<Proposal> Voteunde { get; set; }
        public List<Proposal> Votepass { get; set; }
        public List<Category> Categories { get; set; }
    }
}
