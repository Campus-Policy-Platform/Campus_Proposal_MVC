using CHU_PolicyPlatform_1.Models;
using CHU_PolicyPlatform_1.Repositories;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace CHU_PolicyPlatform_1.Services
{
    public class SeUserS
    {
        private ReGerent _reGerent;
        private ReUserS _reUserS;

        public SeUserS(ReGerent reGerent, ReUserS reUserS)
        {
            _reGerent = reGerent;
            _reUserS = reUserS;
        }

        public List<Proposal> SeU()
        {
            var reg = _reGerent.GetAllData();
            var reu = _reUserS.GetAllToR();
            var toppass = new List<Proposal>();
            reu.ForEach(reuItem =>
            {
                toppass.AddRange(reg.Where(item => item.Pass == true && item.ProposalId == reuItem.ProposalId).ToList());
            });

            return toppass;
        }
    }
}
