using Microsoft.Data.SqlClient;
using System.Globalization;

namespace Model
{
    public class Ugovor : IEntity
    {
        #region Fields
        public int BrojUgovora { get; set; }
        public DateTime DatumOd { get; set; }
        public DateTime DatumDo { get; set; }
        public Korisnik Korisnik { get; set; }
        public Paket Paket { get; set; }
        public List<Pretplata> Pretplate { get; set; }
        private CultureInfo _ci = CultureInfo.InvariantCulture;
        #endregion

        public Ugovor()
        {
            Korisnik = new Korisnik();
            Paket = new Paket();
            Pretplate = new List<Pretplata>();

            if (DatumOd == DateTime.Parse("01.01.0001", _ci)) DatumOd = DateTime.Parse("01.01.1753", _ci);
            if (DatumDo == DateTime.Parse("01.01.0001", _ci)) DatumDo = DateTime.Parse("01.01.1753", _ci);
        }

        #region Query requirements
        private Dictionary<string, object> _set;
        public string TableName => "Ugovor";

        public string TableAlias => "ug";

        public string InsertValues => "@DatumOd" +
                                    ", @DatumDo" +
                                    ", @SifraKorisnika" +
                                    ", @SifraPaketa";

        public string JoinTable => "JOIN Korisnik k ON ug.SifraKorisnika = k.Sifra " +
                                "JOIN Paket p ON ug.SifraPaketa = p.Sifra";

        public string Where => "BrojUgovora = @BrojUgovora";

        public Dictionary<string, object> Set
        {
            get
            {
                _set = new Dictionary<string, object>
                {
                    { "@BrojUgovora", BrojUgovora },
                    { "@DatumOd", DatumOd },
                    { "@DatumDo", DatumDo },
                    { "@SifraKorisnika", Korisnik.Sifra },
                    { "@JmbgKorisnika", Korisnik.Jmbg },
                    { "@ImeKorisnika", Korisnik.Ime },
                    { "@PrezimeKorisnika", Korisnik.Prezime },
                    { "@BrojTelefonaKorisnika", Korisnik.BrojTelefona },
                    { "@BrojLicneKarteKorisnika", Korisnik.BrojLicneKarte },
                    { "@SifraPaketa", Paket.Sifra },
                    { "@NazivPaketa", Paket.Naziv }
                };

                return _set;
            }
        }

        public object SelectValues => "ug.BrojUgovora" +
                                    ", ug.DatumOd" +
                                    ", ug.DatumDo" +
                                    ", k.Sifra AS SifraKorisnika" +
                                    ", k.Jmbg AS JmbgKorisnika" +
                                    ", k.Ime AS ImeKorisnika" +
                                    ", k.Prezime AS PrezimeKorisnika" +
                                    ", k.BrojTelefona AS BrojTelefonaKorisnika" +
                                    ", k.BrojLicneKarte AS BrojLicneKarteKorisnika" +
                                    ", p.Sifra AS SifraPaketa" +
                                    ", p.Naziv AS NazivPaketa";

        public List<IEntity> GetEntities(SqlDataReader reader)
        {
            List<IEntity> result = new List<IEntity>();
            while (reader.Read())
            {
                result.Add(new Ugovor()
                {
                    BrojUgovora = (int)reader["BrojUgovora"],
                    DatumOd = (DateTime)reader["DatumOd"],
                    DatumDo = (DateTime)reader["DatumDo"],
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
                });
            }
            return result;
        }

        public string GetSet()
        {
            return "SET DatumOd = @DatumOd" +
                ", DatumDo = @DatumDo" +
                ", SifraKorisnika = @SifraKorisnika" +
                ", SifraPaketa = @SifraPaketa";
        }

        public string GetWhere()
        {
            string where = BrojUgovora == 0 ? "" : " AND ug.BrojUgovora = @BrojUgovora";
            where += DatumOd == DateTime.Parse("01.01.1753", _ci) ? "" : " AND ug.DatumOd >= @DatumOd";
            where += DatumDo == DateTime.Parse("01.01.1753", _ci) ? "" : " AND ug.DatumDo <= @DatumDo";
            where += Korisnik is null || Korisnik.Sifra == 0 
                ? "" : " AND k.Sifra = @SifraKorisnika";
            where += Korisnik is null || string.IsNullOrWhiteSpace(Korisnik.Jmbg) 
                ? "" : " AND k.Jmbg = @JmbgKorisnika";
            where += Korisnik is null || string.IsNullOrWhiteSpace(Korisnik.Ime) 
                ? "" : " AND k.Ime = @ImeKorisnika";
            where += Korisnik is null || string.IsNullOrWhiteSpace(Korisnik.Prezime) 
                ? "" : " AND k.Prezime = @PrezimeKorisnika";
            where += Korisnik is null || string.IsNullOrWhiteSpace(Korisnik.BrojTelefona) 
                ? "" : " AND k.BrojTelefona = @BrojTelefonaKorisnika";
            where += Korisnik is null || string.IsNullOrWhiteSpace(Korisnik.BrojLicneKarte) 
                ? "" : " AND k.BrojLicneKarte = @BrojLicneKarteKorisnika";
            where += Paket is null || Paket.Sifra == 0 
                ? "" : " AND p.Sifra = @SifraPaketa";
            where += Paket is null || string.IsNullOrWhiteSpace(Paket.Naziv) 
                ? "" : " AND p.Naziv = @NazivPaketa";

            return "WHERE 1=1 " + where;
        }
        #endregion
    }
}
