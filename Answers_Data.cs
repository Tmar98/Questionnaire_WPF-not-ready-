using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire
{
    public class Answers_Data
    {
        public int Id { get; set; }
        public int Id_Children { get; set; }
        public int Test_Number { get; set; }
        public DateTime Date { get; set; }
        public Boolean Test_Result_Id { get; set; }
        public int Question1 { get; set; }
        public int Question2 { get; set; }
        public int Question3 { get; set; }
        public int Question4 { get; set; }
        public int Question5 { get; set; }
        public int Question6 { get; set; }
        public int Question7 { get; set; }
        public int Question8 { get; set; }
        public int Question9 { get; set; }
        public int Question10 { get; set; }
        public int Question11 { get; set; }
        public int Question12 { get; set; }
        public int Question13 { get; set; }
        public int Question14 { get; set; }
        public int Question15 { get; set; }
        public int Question16 { get; set; }
        public int Question17 { get; set; }
        public int Question18 { get; set; }
        public int Question19 { get; set; }
        public int Question20 { get; set; }
        public int Question21 { get; set; }
        public int Question22 { get; set; }
        public int Question23 { get; set; }
        public int Question24 { get; set; }
        public int Question25 { get; set; }
        public int Question26 { get; set; }
        public int Question27 { get; set; }
        public int Question28 { get; set; }
        public int Question29 { get; set; }
        public int Question30 { get; set; }
        public int Question31 { get; set; }
        public int Question32 { get; set; }
        public int Question33 { get; set; }
        public int Question34 { get; set; }
        public int Question35 { get; set; }
        public int Question36 { get; set; }
        public int Question37 { get; set; }
        public int Question38 { get; set; }
        public int Question39 { get; set; }
        public int Question40 { get; set; }

        public Answers_Data()
        {

        }

        public Answers_Data(int id,int id_Children,int test_Number,DateTime date,int question1, int question2, int question3, int question4, int question5, int question6, int question7, int question8, int question9, int question10, int question11, int question12, int question13, int question14, int question15)
        {
            Id = id;Id_Children = id_Children;Test_Number = test_Number;Date = date;Question1 = question1; Question2 = question2;Question3 = question3;Question4 = question4;Question5 = question5;Question6 = question6;Question7 = question7;Question8 = question8;Question9 = question9;Question10 = question10;Question11 = question11;Question12 = question12;Question13 = question13;Question14 = question14;Question15 = question15;
        }

        public Answers_Data(int id,int id_Children,int test_Number,DateTime date,int question1, int question2, int question3, int question4, int question5, int question6, int question7, int question8, int question9, int question10, int question11, int question12, int question13, int question14, int question15, int question16, int question17, int question18, int question19, int question20)
        {
            Id = id;Id_Children = id_Children;Test_Number = test_Number;Date = date;Question1 = question1; Question2 = question2;Question3 = question3;Question4 = question4;Question5 = question5;Question6 = question6;Question7 = question7;Question8 = question8;Question9 = question9;Question10 = question10;Question11 = question11;Question12 = question12;Question13 = question13;Question14 = question14;Question15 = question15; Question16 = question16; Question17 = question17; Question18 = question18; Question19 = question19; Question20 = question20;
        }
    }
}
