using CHU_PolicyPlatform_1.Data;
using CHU_PolicyPlatform_1.Models;
using CHU_PolicyPlatform_1.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHU_PolicyPlatform_1.Services
{
    public class ProposalService
    {
        private ProposeContext _context;
        private ProposalRepository _proposalRepo;
        public ProposalService(ProposalRepository proposalRepo, ProposeContext context) 
        { 
            _proposalRepo = proposalRepo;
            _context = context;
        }

        public async Task<List<Proposal>> readProposal()
        {
            var props = await _proposalRepo.readAllProposal();
            List<Proposal> propListOrderby = (from prop in props
                                              orderby prop.Pdate descending
                                              select prop).ToList();
            return propListOrderby;
        }

        public List<Category> findAllCate()
        {
            var cateList = _context.Categories.ToList();
            return cateList;
        }
    }
}
