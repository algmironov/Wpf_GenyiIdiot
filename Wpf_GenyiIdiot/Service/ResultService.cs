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
            
            var contents = DataDealer.GetDataFromJson(pathToResults);
            resultList = JsonSerializer.Deserialize<List<Result>>(contents);
            return resultList;
        }

        public static void SaveResult(Result result)
        {
           ResultStorage.AddResult(result);
        }
    }
}
