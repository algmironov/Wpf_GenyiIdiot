using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GenyiIdiotMauiApp.DataBase;
using GenyiIdiotMauiApp.Model;
using GenyiIdiotMauiApp.View;

namespace GenyiIdiotMauiApp.Service
{
    public class ResultService
    {
        static List<Result> resultList = new();
        static readonly string pathToResults = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "results.json");
        public async Task<List<Result>> GetResults()
        {
            StreamReader reader = new(pathToResults, Encoding.UTF8);
            var contents = await reader.ReadToEndAsync();
            resultList = JsonSerializer.Deserialize<List<Result>>(contents);
            return resultList;
        }

        public void SaveResult(Result result)
        {
           ResultStorage.AddResult(result);
        }
    }
}
