using Microsoft.Data.SqlClient;

namespace Model
{
    public class Pretplata : IEntity
    {
        #region Fields
        public Usluga Usluga { get; set; }
        public Ugovor Ugovor { get; set; }
        public bool JeAktivna { get; set; }
        #endregion

        public Pretplata()
        {
            Usluga = new Usluga();
            Ugovor = new Ugovor();
        }

        #region Query requirements
        private Dictionary<string, object> _set;
        public string TableName => "Pretplata";

        public string TableAlias => "pr";

        public string InsertValues => "@SifraUsluge" +
                                    ", @BrojUgovora" +
                                    ", @JeAktivna";

        public string JoinTable => "JOIN Usluga u ON pr.SifraUsluge = u.Sifra " +
                                "JOIN Ugovor ug ON pr.BrojUgovora = ug.BrojUgovora "+
                                "JOIN Korisnik k ON ug.SifraKorisnika = k.Sifra " +
                                "JOIN Paket p ON ug.SifraPaketa = p.Sifra";

        public string Where => "SifraUsluge = @SifraUsluge AND BrojUgovora = @BrojUgovora";

        public Dictionary<string, object> Set
        {
            get
            {
                _set = new Dictionary<string, object>
                {
                    { "@SifraUsluge", Usluga.Sifra },
                    { "@NazivUsluge", Usluga.Naziv },
                    { "@BrojUgovora", Ugovor.BrojUgovora },
                    { "@DatumOdUgovora", Ugovor.DatumOd },
                    { "@DatumDoUgovora", Ugovor.DatumDo },
                    { "@SifraKorisnika", Ugovor.Korisnik.Sifra },
                    { "@JmbgKorisnika", Ugovor.Korisnik.Jmbg },
                    { "@ImeKorisnika", Ugovor.Korisnik.Ime },
                    { "@PrezimeKorisnika", Ugovor.Korisnik.Prezime },
                    { "@BrojTelefonaKorisnika", Ugovor.Korisnik.BrojTelefona },
                    { "@BrojLicneKarteKorisnika", Ugovor.Korisnik.BrojLicneKarte },
                    { "@SifraPaketa", Ugovor.Paket.Sifra },
                    { "@NazivPaketa", Ugovor.Paket.Naziv },
                    { "@JeAktivna", JeAktivna }
                };

                return _set;
            }
        }

        public object SelectValues => "u.Sifra AS SifraUsluge" +
                                    ", u.Naziv AS NazivUsluge" +
                                    ", ug.BrojUgovora AS BrojUgovora" +
                                    ", ug.DatumOd AS DatumOdUgovora" +
                                    ", ug.DatumDo AS DatumDoUgovora" +
                                    ", k.Sifra AS SifraKorisnika" +
                                    ", k.Jmbg AS JmbgKorisnika" +
                                    ", k.Ime AS ImeKorisnika" +
                                    ", k.Prezime AS PrezimeKorisnika" +
                                    ", k.BrojTelefona AS BrojTelefonaKorisnika" +
                                    ", k.BrojLicneKarte AS BrojLicneKarteKorisnika" +
                                    ", p.Sifra AS SifraPaketa" +
                                    ", p.Naziv AS NazivPaketa" +
                                    ", pr.JeAktivna";

        public List<IEntity> GetEntities(SqlDataReader reader)
        {
            List<IEntity> result = new List<IEntity>();
            while (reader.Read())
            {
                result.Add(new Pretplata()
                {
                    Usluga = new Usluga()
                    {
                        Sifra = (int)reader["SifraUsluge"],
                        Naziv = (string)reader["NazivUsluge"]
                    },
                    Ugovor = new Ugovor()
                    {
                        BrojUgovora = (int)reader["BrojUgovora"],
                        DatumOd = (DateTime)reader["DatumOdUgovora"],
                        DatumDo = (DateTime)reader["DatumDoUgovora"],
                        Korisnik = new Korisnik()
                        {
                            Sifra = (int)reader["SifraKorisnika"],
                            Jmbg = (string)reader["JmbgKorisnika"],
                            Ime = (string)reader["ImeKorisnika"],
                            Prezime = (string)reader["PrezimeKorisnika"],
                            BrojTelefona = (string)reader["BrojTelefonaKorisnika"],
                            BrojLicneKarte = (string)reader["BrojLicneKarteKorisnika"]
                        },
                        Paket = new Paket()
                        {
                            Sifra = (int)reader["SifraPaketa"],
                            Naziv = (string)reader["NazivPaketa"]
                        }
                    },
                    JeAktivna = (bool)reader["JeAktivna"]
                });
            }
            return result;
        }

        public string GetSet()
        {
            return "SET JeAktivna = @JeAktivna";
        }

        public string GetWhere()
        {
            string where = Usluga is null || Usluga.Sifra == 0 ? "" : " AND u.Sifra = @SifraUsluge";
            where += Ugovor is null || Ugovor.BrojUgovora == 0 ? "" : " AND ug.BrojUgovora = @BrojUgovora";
            //where += Usluga is null || string.IsNullOrWhiteSpace(Usluga.Naziv) ? "" : " AND u.Naziv = @NazivUsluge";
            //where += Ugovor is null || Ugovor.BrojUgovora == 0 ? "" : " AND ug.BrojUgovora = @BrojUgovora";
            //where += Ugovor is null || Ugovor.DatumOd == DateTime.Parse("01.01.1753", CultureInfo.InvariantCulture)
            //    ? "" : " AND ug.DatumOd >= @DatumOdUgovora";
            //where += Ugovor is null || Ugovor.DatumDo == DateTime.Parse("01.01.1753", CultureInfo.InvariantCulture)
            //    ? "" : " AND ug.DatumDo <= @DatumDoUgovora";
            //where += Ugovor is null || Ugovor.Korisnik is null || Ugovor.Korisnik.Sifra == 0
            //    ? "" : " AND k.Sifra = @SifraKorisnika";
            //where += Ugovor is null || Ugovor.Korisnik is null || string.IsNullOrWhiteSpace(Ugovor.Korisnik.Jmbg)
            //    ? "" : " AND k.Jmbg = @JmbgKorisnika";
            //where += Ugovor is null || Ugovor.Korisnik is null || string.IsNullOrWhiteSpace(Ugovor.Korisnik.Ime)
            //    ? "" : " AND k.Ime = @ImeKorisnika";
            //where += Ugovor is null || Ugovor.Korisnik is null || string.IsNullOrWhiteSpace(Ugovor.Korisnik.Prezime)
            //    ? "" : " AND k.Prezime = @PrezimeKorisnika";
            //where += Ugovor is null || Ugovor.Korisnik is null || string.IsNullOrWhiteSpace(Ugovor.Korisnik.BrojTelefona)
            //    ? "" : " AND k.BrojTelefona = @BrojTelefonaKorisnika";
            //where += Ugovor is null || Ugovor.Korisnik is null || string.IsNullOrWhiteSpace(Ugovor.Korisnik.BrojLicneKarte)
            //    ? "" : " AND k.BrojLicneKarte = @BrojLicneKarteKorisnika";
            //where += Ugovor is null || Ugovor.Paket is null || Ugovor.Paket.Sifra == 0
            //    ? "" : " AND p.Sifra = @SifraPaketa";
            //where += Ugovor is null || Ugovor.Paket is null || string.IsNullOrWhiteSpace(Ugovor.Paket.Naziv)
            //    ? "" : " AND p.Naziv = @NazivPaketa";

            return "WHERE 1=1 " + where;
        }
        #endregion
    }
}
