using System;
using System.Collections.Generic;

#nullable disable

namespace CHU_PolicyPlatform_1.Models
{
    public partial class Category
    {
        public Category()
        {
            Proposals = new HashSet<Proposal>();
        }

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int CategoryMinDay { get; set; }
        public int CategoryMaxDay { get; set; }
        public int CategoryGerentReview { get; set; }

        public virtual ICollection<Proposal> Proposals { get; set; }
    }
}
