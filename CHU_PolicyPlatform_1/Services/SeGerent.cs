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
        public SeGerent(ReGerent reGerent)
        {
            _reGerent = reGerent;
        }

        public List<Proposal> SeG()
        {
            var rege = _reGerent.GetAllData();
            var sepass = rege.Where(item => item.Pass == true);

            return sepass.ToList();
        }

    }
}
