using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.ViewModel
{
    public class SearchVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string JoiningDate { get; set; }
        public string FullAddress { get; set; }
        public DateTime JoiningStartDate { get; set; }
        public DateTime JoiningEndDate { get; set; }
        public string Country { get; set; }
        public string Image { get; set; }
        public string? TotalRecord { get; set; }
        public string? sort { get; set; }
        public int pageNumber { get; set; }
        public string Sortcolumn { get; set; }


    }

}
