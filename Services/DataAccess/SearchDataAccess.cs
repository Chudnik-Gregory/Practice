using Services.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.DataAccess
{
    public class SearchDataAccess : ISearchDataAccess
    {
        private List<ExtractSearchModel> searchModels = new List<ExtractSearchModel>();

        public async Task SetExtractSearches(int wait, int randomMin, int randomMax)
        {
            searchModels = new List<ExtractSearchModel>();

            try
            {
                var extractSearchA = new ExtractSearchA();
                var extractSearchB = new ExtractSearchB();
                var extractSearchC = new ExtractSearchC();
                var extractSearchD = new ExtractSearchD();

                var firstTask = Task.Run(async () =>
                {
                    return await extractSearchA.Request(wait, randomMin, randomMax);
                });

                var secondTask = Task.Run(async () =>
                {
                    return await extractSearchB.Request(wait, randomMin, randomMax);
                });

                var thirdTask = Task.Factory.StartNew(async () =>
                {
                    var statusC = await extractSearchC.Request(wait, randomMin, randomMax);
                    if (statusC == Status.OK)
                    {
                        var statusD = await extractSearchD.Request(wait, randomMin, randomMax);
                        return (statusC: statusC, statusD: statusD);
                    }
                    return (statusC: statusC, statusD: Status.TimeOut);
                });

                await Task.WhenAll(firstTask, secondTask, thirdTask);
                searchModels.Add(new ExtractSearchModel(extractSearchA, firstTask.Result));
                searchModels.Add(new ExtractSearchModel(extractSearchB, secondTask.Result));
                searchModels.Add(new ExtractSearchModel(extractSearchC, thirdTask.Result.Result.statusC));
                if (thirdTask.Result.Result.statusC == Status.OK)
                    searchModels.Add(new ExtractSearchModel(extractSearchD, thirdTask.Result.Result.statusD));
            }
            catch (Exception ex)
            {
                throw new Exception($"Запросы отправлены неверно {nameof(ex)}");
            }
        }

        public Task<IEnumerable<ExtractSearchModel>> GetExtractSearches()
        {
            return Task.FromResult(searchModels.AsEnumerable());
        }
    }
}
