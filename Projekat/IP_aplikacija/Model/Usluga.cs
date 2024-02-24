using Microsoft.Data.SqlClient;

namespace Model
{
    public class Usluga : IEntity
    {
        #region Fields
        public int Sifra { get; set; }
        public string Naziv { get; set; }
        public List<Cenovnik> Cenovnici { get; set; }
        #endregion

        public Usluga()
        {
            Cenovnici = new List<Cenovnik>();
        }

        #region Query requirements
        private Dictionary<string, object> _set;
        public string TableName => "Usluga";

        public string TableAlias => "u";

        public string InsertValues => "@Naziv";

        public string JoinTable => "";

        public string Where => "Sifra = @Sifra";

        public Dictionary<string, object> Set
        {
            get
            {
                _set = new Dictionary<string, object>
                {
                    { "@Sifra", Sifra },
                    { "@Naziv", Naziv }
                };

                return _set;
            }
        }

        public object SelectValues => "u.Sifra" +
                                    ", u.Naziv";

        public List<IEntity> GetEntities(SqlDataReader reader)
        {
            List<IEntity> result = new List<IEntity>();
            while (reader.Read())
            {
                result.Add(new Usluga()
                {
                    Sifra = (int)reader["Sifra"],
                    Naziv = (string)reader["Naziv"]
                });
            }
            return result;
        }

        public string GetSet()
        {
            return "SET Naziv = @Naziv";
        }

        public string GetWhere()
        {

            if (Naziv == "core")
            {
                return "WHERE u.Naziv IN ('KTV', 'NET', 'FIX')";
            }
            else if (Naziv == "additional")
            {
                return "WHERE u.Naziv NOT IN ('KTV', 'NET', 'FIX')";
            }
            else
            {
                string where = Sifra == 0 ? "" : " AND u.Sifra = @Sifra";
                where += string.IsNullOrWhiteSpace(Naziv) ? "" : " AND u.Naziv LIKE @Naziv";

                return "WHERE 1=1 " + where;
            }
        }
        #endregion
    }
}
