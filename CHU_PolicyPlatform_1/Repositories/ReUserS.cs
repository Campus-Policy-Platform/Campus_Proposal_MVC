using CHU_PolicyPlatform_1.Data;
using CHU_PolicyPlatform_1.Models;
using System.Collections.Generic;
using System.Linq;

namespace CHU_PolicyPlatform_1.Repositories
{
    public class ReUserS
    {
        private readonly ProposeContext _context;

        public ReUserS(ProposeContext context)
        {
            _context = context;
        }

        public List<ToRepond> GetAllToR()
        {
            List<ToRepond> ToR = _context.ToReponds.ToList();
            return ToR;
        }


    }
}
