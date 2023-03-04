using Services.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.DataAccess
{
    public interface ISearchDataAccess
    {
        Task SetExtractSearches(int wait, int randomMin, int randomMax);
        Task<IEnumerable<ExtractSearchModel>> GetExtractSearches();
    }
}