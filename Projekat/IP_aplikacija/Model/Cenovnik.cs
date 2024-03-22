using Microsoft.Data.SqlClient;
using System.Globalization;

namespace Model
{
    public class Cenovnik : IEntity
    {
        #region Fields
        public int Sifra { get; set; }
        public Usluga Usluga { get; set; }
        public DateTime DatumOd { get; set; }
        public DateTime? DatumDo { get; set; }
        public decimal Cena { get; set; }
        public Action? Action { get; set; }
        private CultureInfo _ci = CultureInfo.InvariantCulture;
        #endregion

        public Cenovnik()
        {
            Usluga = new Usluga();

            if (DatumOd == DateTime.Parse("01.01.0001", _ci)) DatumOd = DateTime.Parse("01.01.1753", _ci);
            if (DatumDo == DateTime.Parse("01.01.0001", _ci)) DatumDo = DateTime.Parse("01.01.1753", _ci);
        }

        #region Query requirements
        private Dictionary<string, object> _set;
        public string TableName => "Cenovnik";

        public string TableAlias => "c";

        public string InsertValues => "@SifraUsluge" +
                                    ", @DatumOd" +
                                    ", @DatumDo" +
                                    ", @Cena";

        public string JoinTable => "JOIN Usluga u ON c.SifraUsluge = u.Sifra";
        public string Where => "Sifra = @Sifra AND SifraUsluge = @SifraUsluge";

        public Dictionary<string, object> Set
        {
            get
            {
                _set = new Dictionary<string, object>
                {
                    { "@Sifra", Sifra },
                    { "@SifraUsluge", Usluga.Sifra },
                    { "@NazivUsluge", Usluga.Naziv },
                    { "@DatumOd", DatumOd },
                    { "@DatumDo", DatumDo is null ? DBNull.Value : DatumDo },
                    { "@Cena", Cena }
                };

                return _set;
            }
        }

        public object SelectValues => "c.Sifra" +
                                    ", u.Sifra AS SifraUsluge" +
                                    ", u.Naziv AS NazivUsluge" +
                                    ", c.DatumOd" +
                                    ", c.DatumDo" +
                                    ", c.Cena";

        public List<IEntity> GetEntities(SqlDataReader reader)
        {
            List<IEntity> result = new List<IEntity>();
            while (reader.Read())
            {
                result.Add(new Cenovnik()
                {
                    Sifra = (int)reader["Sifra"],
                    Usluga = new Usluga()
                    { 
                        Sifra = (int)reader["SifraUsluge"] ,
                        Naziv = (string)reader["NazivUsluge"]
                    },
                    DatumOd = (DateTime)reader["DatumOd"],
                    DatumDo = reader["DatumDo"] is DBNull ? null : (DateTime)reader["DatumDo"],
                    Cena = (decimal)reader["Cena"]
                });
            }
            return result;
        }

        public string GetSet()
        {
            return "SET DatumOd = @DatumOd" +
                ", DatumDo = @DatumDo" +
                ", Cena = @Cena";
        }

        public string GetWhere()
        {
            string where = Sifra == 0 ? "" : " AND c.Sifra = @Sifra";
            where += Usluga is null || Usluga.Sifra == 0 ? "" : " AND u.Sifra = @SifraUsluge"; 
            where += Usluga is null || string.IsNullOrWhiteSpace(Usluga.Naziv) ? "" : " AND u.Naziv LIKE @NazivUsluge";
            where += DatumOd == DateTime.Parse("01.01.1753", _ci) ? "" : " AND c.DatumOd >= @DatumOd";
            where += DatumDo is null ? "" : " AND (ISNULL(c.DatumDo, '18000101') <= @DatumDo)";
            where += Cena == 0 ? "" : " AND c.Cena = @Cena";

            return "WHERE 1=1 " + where + " ORDER BY u.Sifra, CASE WHEN c.DatumDo IS NULL THEN 0 ELSE 1 END, DatumDo DESC";
        }
        #endregion
    }
}
