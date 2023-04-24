using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wpf_GenyiIdiot.Storage;
using Wpf_GenyiIdiot.Model;

namespace Wpf_GenyiIdiot.Service
{
    public class ResultService
    {
        static List<Result> resultList = new();
        static readonly string pathToResults = @"Resources\results.json";
        
        public static List<Result> GetResults()
        {
            resultList = ResultStorage.GetListOfResults();
            return resultList;
        }

        public static void SaveResult(Result result)
        {
           ResultStorage.AddResult(result);
        }

        public static void ClearResults()
        {
            resultList.Clear();
            ResultStorage.ClearResults();
        }

        public static void RemoveResult(Result result)
        {
            ResultStorage.RemoveChoosenResult(result);
            resultList.Clear();
            resultList = GetResults();
        }
    }
}
