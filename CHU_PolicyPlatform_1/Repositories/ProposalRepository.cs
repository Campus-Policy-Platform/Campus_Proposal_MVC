using CHU_PolicyPlatform_1.Data;
using CHU_PolicyPlatform_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHU_PolicyPlatform_1.Repositories
{
    public class ProposalRepository
    {
        private readonly ProposeContext _context;
        private readonly List<Proposal> _proposal;
        public ProposalRepository(ProposeContext context)
        {
            _context = context;
            List<Proposal> proposals = _context.Proposals.ToList();
            _proposal = proposals;
        }

        public async Task<List<Proposal>> readAllProposal()
        {
            foreach (var proposal in _proposal)
            {
                //判斷提案是否結束
                if((DateTime.Now-proposal.Pdate).Days >= 20 && proposal.Underways)
                {
                    proposal.Underways = false;

                    //判斷提案是否通過
                    if (_context.Votes.Count() >= 20)
                    {
                        //定義投票同意數和不同意數
                        int agree = 0, disagree = 0;
                        foreach (var vote in _context.Votes)
                        {
                            if(vote.ProposalId == proposal.ProposalId)
                            {
                                if (vote.Crucial)
                                {
                                    agree++;
                                }
                                else
                                {
                                    disagree++;
                                }
                            }
                        }
                        //判斷同意數是否大於不同意數
                        if (agree > disagree)
                        {
                            proposal.Pass = true;
                        }
                        else
                        {
                            proposal.Pass = false;
                        }
                    }
                    else
                    {
                        proposal.Pass = false;
                    }
                    
                    //修正資料
                    _context.Proposals.Update(proposal);
                    await _context.SaveChangesAsync();
                }                
            }

            return _proposal;
        }
    }
}
