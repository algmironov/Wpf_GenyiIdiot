using Wpf_GenyiIdiot.Model;

namespace Wpf_GenyiIdiot.Storage
{
    public class DiagnosisStorage
    {
        public static HashSet<Diagnosis> diagnoses = new ();
        static readonly string filename = "diagnoses.json";
        static readonly string pathToDiagnoses = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), filename);


        public static void AddDiagnosis(Diagnosis diagnosis)
        {
            diagnoses.Add(diagnosis);
        }

        public static string GetDiagnosisByResult(int correctAnswersCount, int questionsCount)
        {
            LoadAllDiagnoses();
            int correctAnswersPercentage = (correctAnswersCount * 100) / questionsCount ;
            foreach (var diagnosis in diagnoses)
            {
                if (correctAnswersPercentage > diagnosis.Min && correctAnswersPercentage < diagnosis.Max)
                {
                    return diagnosis.UserDiagnosis;
                }
            }
            return string.Empty;
        }

        private static void LoadAllDiagnoses()
        {
            var allDiagnoses = DataDealer.GetDataFromJson(pathToDiagnoses);
            List<Diagnosis> diagnosesList = JsonSerializer.Deserialize<List<Diagnosis>>(allDiagnoses);
            diagnoses = diagnosesList.ToHashSet();
        }
    }
}
