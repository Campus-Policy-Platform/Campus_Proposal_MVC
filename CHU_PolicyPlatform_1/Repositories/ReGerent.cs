using CHU_PolicyPlatform_1.Data;
using CHU_PolicyPlatform_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHU_PolicyPlatform_1.Repositories
{
    public class ReGerent
    {
        private readonly ProposeContext _context;

        public ReGerent(ProposeContext context)
        {
            _context = context;
        }

        public List<Proposal> GetAllData()
        {
            List<Proposal> value = _context.Proposals.ToList();
            return value;
        }
    }
}
