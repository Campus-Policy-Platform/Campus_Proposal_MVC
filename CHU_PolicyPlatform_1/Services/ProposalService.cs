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
        private ProposalRepository _proposalRepo;
        public ProposalService(ProposalRepository proposalRepo) 
        { 
            _proposalRepo = proposalRepo;
        }

        public async Task<List<Proposal>> readProposal()
        {
            return await _proposalRepo.readAllProposal();
        }
    }
}
