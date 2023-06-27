using CHU_PolicyPlatform_1.Data;
using CHU_PolicyPlatform_1.Models;
using CHU_PolicyPlatform_1.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
namespace CHU_PolicyPlatform_1.Services
{
    public class SeGerent
    {
        private ReGerent _reGerent;
        private ReUserS _reUserS;
        public SeGerent(ReGerent reGerent, ReUserS reUserS)
        {
            _reGerent = reGerent;
            _reUserS = reUserS;
        }

        public List<Proposal> SeG()
        {
            var rege = _reGerent.GetAllData();
            var sepass = rege.Where(item => item.Pass == true);

            return sepass.ToList();
        }

        public List<Proposal> SeUser()
        {

            var rege = _reGerent.GetAllData();
            var reus = _reUserS.GetAllToR();
            var sepass = new List<Proposal>();
            reus.ForEach(reuItem =>
            {
                sepass.AddRange(rege.Where(item => item.Pass == true && item.ProposalId != reuItem.ProposalId).ToList());
            });
            return sepass.ToList();
        }
    }
}
