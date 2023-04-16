using System.Windows;

namespace Wpf_GenyiIdiot.Storage
{
    public class DataDealer
    {

        public static async Task<bool> SaveData(string filename, string data)
        {
            await File.WriteAllTextAsync(filename, data);
            
            return true;
        }

        public static string GetDataFromJson(string filename)
        {
            try
            {
                var data = File.ReadAllText(filename, Encoding.UTF8);
                return data;
            }
            catch (IOException)
            {
                return string.Empty;
            }
        }

        //public static async void PreLoadData() 
        //{
        //    var questions = "questions.json";
        //    var results = "results.json";
        //    var diagnoses = "diagnoses.json";

        //    var pathToQuestions = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), questions);
        //    var pathToResults = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), results);
        //    var pathToDiagnoses = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), diagnoses);


        //    using var qStream = Application.Current;
        //        //FileSystem.OpenAppPackageFileAsync(questions).Result;
        //    using var qReader = new StreamReader(qStream);
        //    var originalQuestions = await qReader.ReadToEndAsync();
        //    if (!File.Exists(pathToQuestions))
        //    {
        //        File.WriteAllText(pathToQuestions, originalQuestions);
        //    }

        //    using var rStream = FileSystem.OpenAppPackageFileAsync(results).Result;
        //    using var rReader = new StreamReader(rStream);
        //    var originalResults = await rReader.ReadToEndAsync();
        //    if (!File.Exists(pathToDiagnoses))
        //    {
        //        File.WriteAllText(pathToResults, originalResults);
        //    }

        //    using var dStream = FileSystem.OpenAppPackageFileAsync(diagnoses).Result;
        //    using var dReader = new StreamReader(dStream);
        //    var originalDiagnoses = await dReader.ReadToEndAsync();
        //    if (!File.Exists(pathToDiagnoses))
        //    {
        //        File.WriteAllText(pathToDiagnoses, originalDiagnoses);
        //    }



        //}

    }
}
