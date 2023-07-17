using System.Collections.Generic;

namespace CHU_PolicyPlatform_1.ViewModels
{
    public class CategoryOptionVM
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public int MinDay { get; set; }
        public int MaxDay { get; set; }
        public int CategoryReview {get; set; }
        public IEnumerable<CategoryOptionVM> Options { get; set; }
    }
}
