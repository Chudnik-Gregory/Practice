using Services.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.DataAccess
{
    public interface ISearchDataAccess
    {
        Task SetExtractSearches(int wait = 50, int randomMin = 50, int randomMax = 5000);
        Task<IEnumerable<ExtractSearchModel>> GetExtractSearches();
    }
}