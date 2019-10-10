using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire
{
    public class Results_Class
    {
        public int Id_Answer { get; set; }
        public string Result1 { get; set; }
        public string Result2 { get; set; }
        public string Result3 { get; set; }
        public string Result4 { get; set; }
        public string Result5 { get; set; }

        public Results_Class(int id_Answer,string result1, string result2, string result3)
        {
            Id_Answer = id_Answer; Result1 = result1; Result2 = result2; Result3 = result3;
        }
    }

    
}
