using Services.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Services.DataAccess
{
    // Время обработки Request запроса не больше wait + 80мс 
    // Если больше, тогда TimeOut
    // Если C вернулась успешной, тогда вызываем D
    public class SearchDataAccess : ISearchDataAccess
    {
        private List<ExtractSearchModel> searchModels = new List<ExtractSearchModel>();

        public void SetExtractSearches(int wait, int randomMin, int randomMax)
        {
            searchModels = new List<ExtractSearchModel>();

            try
            {
                var extractSearchA = new ExtractSearchA();
                var extractSearchB = new ExtractSearchB();
                var extractSearchC = new ExtractSearchC();
                var extractSearchD = new ExtractSearchD();

                var firstTask = Task.Factory.StartNew<Status>(() =>
                {
                    return extractSearchA.Request(wait, randomMin, randomMax);
                });

                var secondTask = Task.Factory.StartNew(() =>
                {
                    return extractSearchB.Request(wait, randomMin, randomMax);
                });

                var thirdTask = Task.Factory.StartNew(() =>
                {
                    var statusC = extractSearchC.Request(wait, randomMin, randomMax);
                    if (statusC == Status.OK)
                    {
                        var statusD = extractSearchD.Request(wait, randomMin, randomMax);
                        return (statusC: statusC, statusD: statusD);
                    }
                    return (statusC: statusC, statusD: Status.TimeOut);


                });
                Task.WaitAll(firstTask, secondTask, thirdTask);
                searchModels.Add(new ExtractSearchModel(extractSearchA, firstTask.Result));
                searchModels.Add(new ExtractSearchModel(extractSearchB, secondTask.Result));
                searchModels.Add(new ExtractSearchModel(extractSearchC, thirdTask.Result.statusC));
                if (thirdTask.Result.statusC == Status.OK)
                    searchModels.Add(new ExtractSearchModel(extractSearchD, thirdTask.Result.statusD));
            }
            catch (Exception ex)
            {
                throw new Exception($"Запросы отправлены неверно {nameof(ex)}");
            }
        }

        public IEnumerable<ExtractSearchModel> GetExtractSearches()
        {
            return searchModels;
        }
    }
}
