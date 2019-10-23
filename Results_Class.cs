using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire
{
    public class Results_Class
    {
        public int Id_Result { get; set; }
        public  int Id_Answer { get; set; }
        public string Result1 { get; set; }
        public string Result2 { get; set; }
        public string Result3 { get; set; }
        public string Result4 { get; set; }
        public string Result5 { get; set; }
        

        public Results_Class()
        {

        }
        public Results_Class(int id, int id_Answer,string result1, string result2, string result3)
        {
            Id_Result = id; Id_Answer = id_Answer; Result1 = result1; Result2 = result2; Result3 = result3; ;
        }
        public Results_Class( int id_Answer, string result1, string result2, string result3)
        {
            Id_Answer = id_Answer; Result1 = result1; Result2 = result2; Result3 = result3; ;
        }
        public Results_Class(int id, int id_Answer, string result1, string result2, string result3, string result4,string result5)
        {
            Id_Result = id; Id_Answer = id_Answer; Result1 = result1; Result2 = result2; Result3 = result3; Result4 = result4;Result5 = result5; 
        }
    }

    public class Named_Results :Results_Class
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string School_Number { get; set; }
        public string Klass_Name { get; set; }
        public DateTime Date { get; set; }
        public int TestNumber { get; set; }

        public Named_Results(int id, string fio, string school_Number, string klass_Name,DateTime date,int testNumber, Results_Class results_Class)
        {
            Id = id; FIO = fio; School_Number = school_Number; Klass_Name = klass_Name; this.Id_Answer = results_Class.Id_Answer; Date = date; TestNumber = testNumber; this.Result1 = results_Class.Result1; this.Result2 = results_Class.Result2; this.Result3 = results_Class.Result3; this.Result4 = results_Class.Result4;this.Result5 = results_Class.Result5;
        }
    }

    
}
