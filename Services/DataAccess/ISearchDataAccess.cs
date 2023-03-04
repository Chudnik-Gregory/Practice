using Services.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.DataAccess
{
    public interface ISearchDataAccess
    {
        void SetExtractSearches(int wait = 50, int randomMin = 50, int randomMax = 5000);
        IEnumerable<ExtractSearchModel> GetExtractSearches();
    }
}