using CHU_PolicyPlatform_1.Models;
using System.Collections.Generic;

namespace CHU_PolicyPlatform_1.ViewModels
{
    public class GerentSeeVM
    {
        public List<propM> pieVMs { get; set; }
        public List<cateM> categories { get; set; }
    }
    public class propM
    {
        public string ProposalId { get; set; }
        public string Title { get; set; }
        public string CategoryId { get; set; }
    }
    public class cateM
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
