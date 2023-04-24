using Wpf_GenyiIdiot.Model;
using Wpf_GenyiIdiot.Service;

namespace Wpf_GenyiIdiot.Storage
{
    public class ResultStorage
    {
        static readonly string filename = "results.json";
        static readonly string pathToResults = @$"Resources\{filename}";


        public static bool AddResult(Result result)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            List<Result> resultList = GetListOfResults();
            resultList.Add(result);
            var data = JsonSerializer.Serialize(resultList, options);
            return DataDealer.SaveData(pathToResults, data);
        }

        public static List<Result> GetListOfResults()
        {
            var resultsString = DataDealer.GetDataFromJson(pathToResults);
            if (string.IsNullOrEmpty(resultsString))
            {
                return new List<Result>();
            }
            else
            {
                var results = JsonSerializer.Deserialize<List<Result>>(resultsString);
                return results;
            }

        }

        public static void ClearResults()
        {
            File.WriteAllText(pathToResults, string.Empty);
        }

        public static bool RemoveChoosenResult(Result result)
        {
            var results = GetListOfResults().Where(x =>!x.Equals(result)).ToList();
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize<List<Result>>(results, options);
            return DataDealer.SaveData(pathToResults, jsonString);
        }

    }
}
