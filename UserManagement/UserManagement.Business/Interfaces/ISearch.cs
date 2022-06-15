using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.ViewModel;

namespace UserManagement.Business.Interfaces
{
    public interface ISearch
    {
        public IQueryable<UserVM> GetSearch(SearchVM query);
    }
}
