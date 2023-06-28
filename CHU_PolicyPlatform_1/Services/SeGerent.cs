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

        public List<Proposal> SeG()//目前無使用到
        {
            var rege = _reGerent.GetAllData();
            var sepass = rege.Where(item => item.Pass == true);

            return sepass.ToList();
        }
        public List<Proposal> SeUserfail()
        {
            var rege = _reGerent.GetAllData();
            var sepass = rege.Where(item => item.Pass == false);

            return sepass.ToList();
        }

        public List<Proposal> SeUser()//以seg為基礎做篩選
        {

            var rege = _reGerent.GetAllData();
            var reus = _reUserS.GetAllToR();
            var sepass = rege.Where(item => item.Pass == true && !reus.Any(reuItem => reuItem.ProposalId == item.ProposalId))
                     .Distinct()
                     .ToList();

            return sepass.ToList();
        }

    }
}
