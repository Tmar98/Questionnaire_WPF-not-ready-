using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Questionnaire
{
    public class School
    {
         int Id { get; set; }
         string School_Number { get; set; }


        public School(int id, string school_number)
        {
            Id = id; School_Number = school_number;
        }
    }


    public class Schools : List<School>
    {
        public Schools(SqlDataReader reader)
        {
            while (reader.Read())
            {
                School school = new School(Int32.Parse(reader["ID"].ToString()), reader["School_Number"].ToString());
                this.Add(school);
            }
            reader.Close();
        }
    }
}
