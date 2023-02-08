using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Model
{
    public class SortModel
    {
        public DateTime? dateStart{ get; set; }

        public DateTime? dateEnd { get; set; }

        public string status { get; set; }
    }
}
