using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Model
{
    public class SetTeamInvestigationModel
    {       
        public string? txtNomorID { get; set; }
        public int? intUserID { get; set; }
        public List<AduanModel>? listAduan { get; set; }

        public List<ListTeamInvestigationModel>? listTeamInvestigation { get; set; } = new List<ListTeamInvestigationModel>();
    }
}
