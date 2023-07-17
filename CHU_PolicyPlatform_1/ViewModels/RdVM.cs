using CHU_PolicyPlatform_1.Models;
using System;
using System.Collections.Generic;

namespace CHU_PolicyPlatform_1.ViewModels
{
    public class RdVM
    {
        public int CategoryMinDay { get; set; }
        public int CategoryMaxDay { get; set; }
        public int CategoryGerentReview { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Category> Categories { get; set; }

    }
}
