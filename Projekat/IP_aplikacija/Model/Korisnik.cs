using Microsoft.Data.SqlClient;

namespace Model
{
    public class Korisnik : IEntity
    {
        #region Fields
        public int Sifra { get; set; }
        public string Jmbg { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string BrojTelefona { get; set; }
        public string BrojLicneKarte { get; set; }
        #endregion

        #region Query requirements
        private Dictionary<string, object> _set;
        public string TableName => "Korisnik";

        public string TableAlias => "k";

        public string InsertValues => "@Jmbg" +
                                    ", @Ime" +
                                    ", @Prezime" +
                                    ", @BrojTelefona" +
                                    ", @BrojLicneKarte";

        public string JoinTable => "";

        public string Where => "Sifra = @Sifra";

        public Dictionary<string, object> Set
        {
            get
            {
                _set = new Dictionary<string, object>
                {
                    { "@Sifra", Sifra },
                    { "@Jmbg", Jmbg},
                    { "@Ime", Ime },
                    { "@Prezime", Prezime },
                    { "@BrojTelefona", BrojTelefona },
                    { "@BrojLicneKarte", BrojLicneKarte }
                };

                return _set;
            }
        }

        public object SelectValues => "k.Sifra" +
                                    ", k.Jmbg" +
                                    ", k.Ime" +
                                    ", k.Prezime" +
                                    ", k.BrojTelefona" +
                                    ", k.BrojLicneKarte";

        public List<IEntity> GetEntities(SqlDataReader reader)
        {
            List<IEntity> result = new List<IEntity>();
            while (reader.Read())
            {
                result.Add(new Korisnik()
                {
                    Sifra = (int)reader["Sifra"],
                    Jmbg = (string)reader["Jmbg"],
                    Ime = (string)reader["Ime"],
                    Prezime = (string)reader["Prezime"],
                    BrojTelefona = (string)reader["BrojTelefona"],
                    BrojLicneKarte = (string)reader["BrojLicneKarte"]
                });
            }
            return result;
        }

        public string GetSet()
        {
            return "SET Jmbg = @Jmbg" +
                ", Ime = @Ime" +
                ", Prezime = @Prezime" +
                ", BrojTelefona = @BrojTelefona" +
                ", BrojLicneKarte = @BrojLicneKarte";
        }

        public string GetWhere()
        {
            string where = Sifra == 0 ? "" : " AND k.Sifra = @Sifra";
            where += string.IsNullOrWhiteSpace(Jmbg) ? "" : " AND k.Jmbg LIKE @Jmbg";
            where += string.IsNullOrWhiteSpace(Ime) ? "" : " AND k.Ime LIKE @Ime";
            where += string.IsNullOrWhiteSpace(Prezime) ? "" : " AND k.Prezime LIKE @Prezime";
            where += string.IsNullOrWhiteSpace(BrojTelefona) ? "" : " AND k.BrojTelefona LIKE @BrojTelefona";
            where += string.IsNullOrWhiteSpace(BrojLicneKarte) ? "" : " AND k.BrojLicneKarte LIKE @BrojLicneKarte";

            return "WHERE 1=1 " + where;
        }
        #endregion
    }
}
