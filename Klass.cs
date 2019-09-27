using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace Questionnaire
{
    public class Klass
    {
        public int Id;
        public int Id_School;
        public string Klass_Name;

        public Klass(int id, int id_School, string klass_Name)
        {
            Id = id; Id_School = id_School; Klass_Name = klass_Name;
        }
    }

    public class Klasses : List<Klass>
    {
        public Klasses(SqlDataReader reader)
        {
            while(reader.Read())
            {
                Klass klass = new Klass(Int32.Parse(reader["ID"].ToString()), Int32.Parse(reader["Id_School"].ToString()), reader["Class_Name"].ToString());
                this.Add(klass);
            }
        }
    }
}
