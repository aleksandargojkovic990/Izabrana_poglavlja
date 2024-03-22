using Model.DTO;
using Model;
using System.Globalization;

namespace IP_aplikacija.Helpers
{
    public class ConvertDTO
    {
        #region Convert DTO to original
        public static Ugovor ConvertUgovorDTOToUgovor(UgovorDTO ugovorDTO)
        {
            List<Pretplata> pretplate = new List<Pretplata>();
            Ugovor ugovor = new Ugovor
            {
                BrojUgovora = ugovorDTO.BrojUgovora ?? 0,
                DatumOd = ugovorDTO.DatumOd ?? DateTime.Parse("01.01.1753", CultureInfo.InvariantCulture),
                DatumDo = ugovorDTO.DatumDo ?? DateTime.Parse("01.01.1753", CultureInfo.InvariantCulture),
                Korisnik = ConvertKorisnikDTOToKorisnik(ugovorDTO.Korisnik),
                Paket = ConvertPaketDTOToPaket(ugovorDTO.Paket),
                Pretplate = pretplate
            };

            foreach (PretplataDTO pretplata in ugovorDTO.Pretplate)
            {
                pretplate.Add(new Pretplata
                {
                    Ugovor = ugovor,
                    Usluga = ConvertUslugaDTOToUsluga(pretplata.Usluga),
                    JeAktivna = pretplata.JeAktivna
                });
            }

            return ugovor;
        }

        public static Paket ConvertPaketDTOToPaket(PaketDTO paketDTO)
        {
            List<PaketUsluga> paketUsluge = new List<PaketUsluga>();

            Paket paket = new Paket
            {
                Sifra = paketDTO.Sifra ?? 0,
                Naziv = paketDTO.Naziv,
                PaketUsluge = paketUsluge
            };

            foreach (PaketUslugaDTO pu in paketDTO.PaketUsluge)
            {
                List<Cenovnik> cenovnici = new List<Cenovnik>();
                Usluga u = ConvertUslugaDTOToUsluga(pu.Usluga);

                paketUsluge.Add(new PaketUsluga
                {
                    Usluga = u,
                    Paket = paket,
                    Popust = pu.Popust
                });
            }

            return paket;
        }

        public static Cenovnik ConvertCenovnikDTOToCenovnik(CenovnikDTO cenovnikDTO)
        {
            Usluga usluga = new Usluga
            {
                Sifra = cenovnikDTO.Usluga.Sifra == null ? 0 : (int)cenovnikDTO.Usluga.Sifra,
                Naziv = cenovnikDTO.Usluga.Naziv
            };

            Cenovnik cenovnik = new Cenovnik
            {
                Sifra = cenovnikDTO.Sifra == null ? 0 : (int)cenovnikDTO.Sifra,
                Usluga = usluga,
                DatumOd = cenovnikDTO.DatumOd ?? DateTime.Parse("01.01.1753", CultureInfo.InvariantCulture),
                DatumDo = cenovnikDTO.DatumDo,
                Cena = cenovnikDTO.Cena ?? 0
            };

            return cenovnik;
        }

        public static Korisnik ConvertKorisnikDTOToKorisnik(KorisnikDTO korisnikDTO)
        {
            Korisnik korisnik = new Korisnik
            {
                Sifra = korisnikDTO.Sifra ?? 0,
                Jmbg = korisnikDTO.Jmbg,
                Ime = korisnikDTO.Ime,
                Prezime = korisnikDTO.Prezime,
                BrojTelefona = korisnikDTO.BrojTelefona,
                BrojLicneKarte = korisnikDTO.BrojLicneKarte
            };

            return korisnik;
        }

        public static Usluga ConvertUslugaDTOToUsluga(UslugaDTO uslugaDTO)
        {
            List<Cenovnik> cenovnici = new List<Cenovnik>();
            Usluga usluga = new Usluga
            {
                Sifra = uslugaDTO.Sifra ?? 0,
                Naziv = uslugaDTO.Naziv,
                Cenovnici = cenovnici
            };

            foreach (CenovnikDTO cenovnik in uslugaDTO.Cenovnici)
            {
                cenovnici.Add(new Cenovnik
                {
                    Sifra = cenovnik.Sifra ?? 0,
                    Usluga = usluga,
                    DatumOd = cenovnik.DatumOd ?? DateTime.Parse("01.01.1753", CultureInfo.InvariantCulture),
                    DatumDo = cenovnik.DatumDo,
                    Cena = cenovnik.Cena ?? 0,
                    Action = cenovnik.Action
                });
            }

            return usluga;
        }
        #endregion

        #region Convert original to DTO
        public static List<UgovorDTO> ConvertUgovoriToUgovoriDTO(List<Ugovor> ugovori)
        {
            List<UgovorDTO> ugovoriDTO = new List<UgovorDTO>();
            foreach (Ugovor u in ugovori)
            {
                List<PretplataDTO> pretplate = new List<PretplataDTO>();
                foreach (Pretplata p in u.Pretplate)
                {
                    pretplate.Add(new PretplataDTO
                    {
                        Ugovor = null,
                        Usluga = new UslugaDTO
                        {
                            Sifra = p.Usluga.Sifra,
                            Naziv = p.Usluga.Naziv,
                            isChecked = true
                        },
                        JeAktivna = p.JeAktivna
                    });
                }

                UgovorDTO ugovorDTO = new UgovorDTO
                {
                    BrojUgovora = u.BrojUgovora,
                    DatumOd = u.DatumOd,
                    DatumDo = u.DatumDo,
                    Paket = new PaketDTO
                    {
                        Sifra = u.Paket.Sifra,
                        Naziv = u.Paket.Naziv
                    },
                    Korisnik = new KorisnikDTO
                    {
                        Sifra = u.Korisnik.Sifra,
                        Ime = u.Korisnik.Ime,
                        Prezime = u.Korisnik.Prezime,
                        Jmbg = u.Korisnik.Jmbg,
                        BrojLicneKarte = u.Korisnik.BrojLicneKarte,
                        BrojTelefona = u.Korisnik.BrojTelefona
                    },
                    Pretplate = pretplate
                };

                ugovoriDTO.Add(ugovorDTO);
            }

            return ugovoriDTO;
        }

        public static List<PretplataDTO> ConvertPretplateToPretplateDTO(List<Pretplata> pretplate)
        {
            List<PretplataDTO> pretplateDTO = new List<PretplataDTO>();
            foreach (Pretplata p in pretplate)
            {
                pretplateDTO.Add(new PretplataDTO
                {
                    Usluga = new UslugaDTO
                    {
                        Sifra = p.Usluga.Sifra,
                        Naziv = p.Usluga.Naziv
                    },
                    Ugovor = new UgovorDTO
                    {
                        BrojUgovora = p.Ugovor.BrojUgovora,
                        DatumOd = p.Ugovor.DatumOd,
                        DatumDo = p.Ugovor.DatumDo,
                        Korisnik = new KorisnikDTO
                        {
                            BrojLicneKarte = p.Ugovor.Korisnik.BrojLicneKarte,
                            Ime = p.Ugovor.Korisnik.Ime,
                            Prezime = p.Ugovor.Korisnik.Prezime,
                            BrojTelefona = p.Ugovor.Korisnik.BrojTelefona,
                            Jmbg = p.Ugovor.Korisnik.Jmbg,
                            Sifra = p.Ugovor.Korisnik.Sifra
                        },
                        Paket = new PaketDTO
                        {
                            Sifra = p.Ugovor.Paket.Sifra,
                            Naziv = p.Ugovor.Paket.Naziv
                        },
                        Pretplate = pretplateDTO
                    },
                    JeAktivna = p.JeAktivna
                });
            }

            return pretplateDTO;
        }

        public static List<CenovnikDTO> ConvertCenovniciToCenovniciDTO(List<Cenovnik> cenovnici)
        {
            List<CenovnikDTO> cenovniciDTO = new List<CenovnikDTO>();
            foreach (Cenovnik c in cenovnici)
            {
                cenovniciDTO.Add(new CenovnikDTO
                {
                    Sifra = c.Sifra,
                    Usluga = new UslugaDTO
                    {
                        Sifra = c.Usluga.Sifra,
                        Naziv = c.Usluga.Naziv,
                        Cenovnici = null,
                        isChecked = true
                    },
                    DatumOd = c.DatumOd,
                    DatumDo = c.DatumDo,
                    Cena = c.Cena
                });
            }

            return cenovniciDTO;
        }

        public static List<KorisnikDTO> ConvertKorisniciToKorisniciDTO(List<Korisnik> korisnici)
        {
            List<KorisnikDTO> korisniciDTO = new List<KorisnikDTO>();
            foreach (Korisnik k in korisnici)
            {
                korisniciDTO.Add(new KorisnikDTO
                {
                    BrojLicneKarte = k.BrojLicneKarte,
                    Ime = k.Ime,
                    Prezime = k.Prezime,
                    BrojTelefona = k.BrojTelefona,
                    Jmbg = k.Jmbg,
                    Sifra = k.Sifra
                });
            }

            return korisniciDTO;
        }

        public static List<PaketDTO> ConvertPaketiToPaketiDTO(List<Paket> paketi)
        {
            List<PaketDTO> paketiDTO = new List<PaketDTO>();
            foreach (Paket p in paketi)
            {
                PaketDTO pdto = new PaketDTO
                {
                    Sifra = p.Sifra,
                    Naziv = p.Naziv,
                };

                paketiDTO.Add(pdto);

                foreach (PaketUsluga pu in p.PaketUsluge)
                {
                    UslugaDTO udto = new UslugaDTO
                    {
                        Sifra = pu.Usluga.Sifra,
                        Naziv = pu.Usluga.Naziv,
                        isChecked = true
                    };

                    pdto.PaketUsluge.Add(new PaketUslugaDTO
                    {
                        Popust = pu.Popust,
                        Usluga = udto,
                        Paket = null
                    });

                    foreach (Cenovnik c in pu.Usluga.Cenovnici)
                    {
                        udto.Cenovnici.Add(new CenovnikDTO
                        {
                            Sifra = c.Sifra,
                            DatumOd = c.DatumOd,
                            DatumDo = c.DatumDo,
                            Cena = c.Cena,
                            Usluga = null
                        });
                    }
                }
            }

            return paketiDTO;
        }

        public static List<UslugaDTO> ConvertUslugeToUslugeDTO(List<Usluga> usluge)
        {
            List<UslugaDTO> uslugeDTO = new List<UslugaDTO>();
            foreach (Usluga u in usluge)
            {
                List<CenovnikDTO> cenovnici = new List<CenovnikDTO>();
                UslugaDTO us = new UslugaDTO
                {
                    Sifra = u.Sifra,
                    Naziv = u.Naziv,
                    Cenovnici = cenovnici,
                    isChecked = true
                };

                uslugeDTO.Add(us);

                foreach (Cenovnik c in u.Cenovnici)
                {
                    cenovnici.Add(new CenovnikDTO
                    {
                        Sifra = c.Sifra,
                        Usluga = null,
                        DatumOd = c.DatumOd,
                        DatumDo = c.DatumDo,
                        Cena = c.Cena
                    });
                }
            }

            return uslugeDTO;
        }
        #endregion
    }
}
