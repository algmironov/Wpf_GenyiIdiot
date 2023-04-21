using System.Threading;
using System.Windows;

namespace Wpf_GenyiIdiot.Storage
{
    public class DataDealer
    {
        public static bool SaveData(string filename, string data)
        {
            try
            {
                StreamWriter writer = new StreamWriter(filename);
                writer.Write(data);
                writer.Flush();
                writer.Close();
                return true;
            }
            catch 
            {
                return false; 
            }
        }

        public static string GetDataFromJson(string filename)
        {
            try
            {
                var readedFile = File.OpenText(filename);
                var text = readedFile.ReadToEnd();
                readedFile.Close();
                return text;
            }
            catch (IOException)
            {
                return string.Empty;
            }
        }
    }
}
