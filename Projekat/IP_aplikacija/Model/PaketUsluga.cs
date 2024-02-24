using Microsoft.Data.SqlClient;

namespace Model
{
    public class PaketUsluga : IEntity
    {
        #region Fields
        public Usluga Usluga { get; set; }
        public Paket Paket { get; set; }
        public decimal Popust { get; set; }
        #endregion

        public PaketUsluga()
        {
            Usluga = new Usluga();
            Paket = new Paket();
        }

        #region Query requirements
        private Dictionary<string, object> _set;
        public string TableName => "PaketUsluga";

        public string TableAlias => "pu";

        public string InsertValues => "@SifraUsluge" +
                                    ", @SifraPaketa" +
                                    ", @Popust";

        public string JoinTable => "JOIN Usluga u ON pu.SifraUsluge = u.Sifra " +
                                "JOIN Paket p ON pu.SifraPaketa = p.Sifra";

        public string Where => "SifraUsluge = @SifraUsluge AND SifraPaketa = @SifraPaketa";

        public Dictionary<string, object> Set
        {
            get
            {
                _set = new Dictionary<string, object>
                {
                    { "@SifraUsluge", Usluga?.Sifra },
                    { "@NazivUsluge", Usluga?.Naziv },
                    { "@SifraPaketa", Paket?.Sifra },
                    { "@NazivPaketa", Paket?.Naziv },
                    { "@Popust", Popust }
                };

                return _set;
            }
        }

        public object SelectValues => "u.Sifra AS SifraUsluge" +
                                    ", u.Naziv AS NazivUsluge" +
                                    ", p.Sifra AS SifraPaketa" +
                                    ", p.Naziv AS NazivPaketa" +
                                    ", pu.Popust";

        public List<IEntity> GetEntities(SqlDataReader reader)
        {
            List<IEntity> result = new List<IEntity>();
            while (reader.Read())
            {
                result.Add(new PaketUsluga()
                {
                    Usluga = new Usluga()
                    {
                        Sifra = (int)reader["SifraUsluge"],
                        Naziv = (string)reader["NazivUsluge"]
                    },
                    Paket = new Paket() 
                    {
                        Sifra = (int)reader["SifraPaketa"],
                        Naziv = (string)reader["NazivPaketa"]
                    },
                    Popust = (decimal)reader["Popust"]
                });
            }
            return result;
        }

        public string GetSet()
        {
            return "SET Popust = @Popust";
        }

        public string GetWhere()
        {
            string where = Usluga is null || Usluga.Sifra == 0 ? "" : " AND u.Sifra = @SifraUsluge";
            where += Usluga is null || string.IsNullOrWhiteSpace(Usluga.Naziv) ? "" : " AND u.Naziv = @NazivUsluge";
            where += Paket is null || Paket.Sifra == 0 ? "" : " AND p.Sifra = @SifraPaketa";
            where += Paket is null || string.IsNullOrWhiteSpace(Paket.Naziv) ? "" : " AND p.Naziv = @NazivPaketa";
            where += Popust == 0 ? "" : " AND pu.Popust = @Popust";

            return "WHERE 1=1 " + where;
        }
        #endregion
    }
}
