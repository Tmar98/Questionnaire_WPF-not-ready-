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

        public Results_Class()
        {

        }
        public Results_Class(int id_Answer,string result1, string result2, string result3)
        {
            Id_Answer = id_Answer; Result1 = result1; Result2 = result2; Result3 = result3;
        }
    }

    public class rt :Results_Class
    {
        public DateTime Date { get; set; }
        rt(DateTime date, int id_Answer, string result1, string result2, string result3)
        {
           Date=date; Id_Answer = id_Answer; Result1 = result1; Result2 = result2; Result3 = result3;
        }

        public rt(Results_Class results_Class, DateTime date)
        {
             this.Id_Answer = results_Class.Id_Answer; Date = date; this.Result1 = results_Class.Result1; this.Result2 = results_Class.Result2; this.Result3 = results_Class.Result3;
        }
    }


}
