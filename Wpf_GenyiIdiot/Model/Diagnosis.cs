using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_GenyiIdiot.Model
{
    [Serializable]
    public class Diagnosis
    {
        public string UserDiagnosis { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public Diagnosis(string userDiagnosis, int min, int max)
        {
            UserDiagnosis = userDiagnosis;
            Min = min;
            Max = max;
        }
    }
}
