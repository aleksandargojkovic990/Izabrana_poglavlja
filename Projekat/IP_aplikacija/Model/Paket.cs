using Microsoft.Data.SqlClient;

namespace Model
{
    public class Paket : IEntity
    {
        #region Fields
        public int Sifra { get; set; }
        public string Naziv { get; set; }
        public List<PaketUsluga> PaketUsluge { get; set; }
        #endregion

        public Paket()
        {
            PaketUsluge = new List<PaketUsluga>();
        }

        #region Query requirements
        private Dictionary<string, object> _set;
        public string TableName => "Paket";

        public string TableAlias => "p";

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

        public object SelectValues => "p.Sifra" +
                                    ", p.Naziv";

        public List<IEntity> GetEntities(SqlDataReader reader)
        {
            List<IEntity> result = new List<IEntity>();
            while (reader.Read())
            {
                result.Add(new Paket()
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
            string where = Sifra == 0 ? "" : " AND p.Sifra = @Sifra";
            where += string.IsNullOrWhiteSpace(Naziv) ? "" : " AND p.Naziv LIKE @Naziv";

            return "WHERE 1=1 " + where;
        }
        #endregion
    }
}
