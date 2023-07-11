using CHU_PolicyPlatform_1.Models;
using System.Collections.Generic;

namespace CHU_PolicyPlatform_1.DTO
{
    public class AddDTO
    {
        public List<User> Users { get; set; }
        public List<Gerent> Gerents { get; set; }

    }
}
