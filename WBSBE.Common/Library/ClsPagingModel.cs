using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Library
{
    public class ClsPagingModelRequest
    {
        public int intPage { get; set; }
        public int intLength { get; set; }
        public string txtSearch { get; set; }
        public string txtSortBy { get; set; }
        public bool bitAscending { get; set; }
        public string queryPaging
        {
            get
            {
                return " and noIndex >= " + ((this.intLength * (this.intPage - 1)) + 1).ToString() + " and noIndex <= " + (this.intPage * this.intLength).ToString() + "";
            }
        }
        public string queryOrder
        {
            get
            {
                var order = this.bitAscending ? "ASC" : "DESC";
                return " Order by " + this.txtSortBy + " " + order;
            }
        }

        public int intStart
        {
            get
            {
                return this.intLength * (this.intPage - 1);
            }
        }

    }

    public class ClsPagingModelResponse
    {
        public object Data { get; set; }
        public int intStart { get; set; }
        public decimal intCount { get; set; }
        public int intRow { get; set; }
        public int intLength { get; set; }
        public string txtSearch { get; set; }
        public string txtSortBy { get; set; }
        public bool bitAscending { get; set; }
    }
}
