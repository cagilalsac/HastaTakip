using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace MVC.Controllers
{
    public class DbController : Controller
    {
        //Db db = new Db(); // DbContext tipindeki gibi objeler new'lenmez, sınıfa (controller) enjekte edilir

        private readonly Db _db;

        public DbController(Db db)
        {
            _db = db;
        }

        // ~/Db/Seed
        public IActionResult Seed()
        {
            #region Verilerin Silinmesi
            _db.DoktorHastalar.RemoveRange(_db.DoktorHastalar.ToList());
            _db.Hastalar.RemoveRange(_db.Hastalar.ToList());
            _db.Doktorlar.RemoveRange(_db.Doktorlar.ToList());
            _db.Klinikler.RemoveRange(_db.Klinikler.ToList());

            var branslar = _db.Branslar.ToList();

            // 1. yöntem:
            //foreach (var brans in branslar)
            //{
            //    _db.Branslar.Remove(brans);
            //}
            // 2. yöntem:
            _db.Branslar.RemoveRange(branslar);

            _db.Kullanicilar.RemoveRange(_db.Kullanicilar.ToList());

            if (_db.Roller.Count() > 0)
            {
				_db.Roller.RemoveRange(_db.Roller.ToList());
				_db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('Roller', RESEED, 0)");
            }
            #endregion

            #region Verilerin Eklenmesi
            _db.Branslar.Add(new Brans()
            {
                Adi = "Cerrahi",
                Guid = Guid.NewGuid().ToString()
            });
            _db.Branslar.Add(new Brans()
            {
                Adi = "Psikiyatri",
                Guid = Guid.NewGuid().ToString()
            });

            _db.Hastalar.Add(new Hasta()
            {
                Adi = "Çağıl",
                Boyu = 1.75m,
                Cinsiyeti = Cinsiyetler.Erkek,
                DogumTarihi = DateTime.Parse("01.05.1980", new CultureInfo("tr-TR")),
                Guid = Guid.NewGuid().ToString(),
                Kilosu = 55,
                KimlikNo = "12345678901",
                Soyadi = "Alsaç"
            });
            _db.Hastalar.Add(new Hasta()
            {
                Adi = "Leo",
                Boyu = 0.4M,
                Cinsiyeti = Cinsiyetler.Erkek,
                DogumTarihi = new DateTime(2014, 5, 29),
                Guid = Guid.NewGuid().ToString(),
                Kilosu = 10,
                Soyadi = "Alsaç"
            });
            _db.Hastalar.Add(new Hasta()
            {
                Adi = "Luna",
                Boyu = 0.5M,
                Cinsiyeti = Cinsiyetler.Kadın,
                DogumTarihi = DateTime.Parse("09/20/2022", new CultureInfo("en-US")),
                Guid = Guid.NewGuid().ToString(),
                Kilosu = 11,
                Soyadi = "Alsaç"
            });

            _db.SaveChanges(); // unit of work

            _db.Klinikler.Add(new Klinik()
            {
                Aciklamasi = "Ankara Çankaya'da özel bir poliklinik.",
                Adi = "Özel Çankaya Polikliniği",
                Guid = Guid.NewGuid().ToString(),
                Doktorlar = new List<Doktor>()
                {
                    new Doktor()
                    {
                        Guid = Guid.NewGuid().ToString(),
                        Adi = "Ali",
                        Soyadi = "Vefa",
                        //UzmanMi = false, // default false

                        // 1. yöntem:
                        //Brans = _db.Branslar.SingleOrDefault(brans => brans.Adi == "Cerrahi"),
                        // 2. yöntem:
                        BransId = _db.Branslar.SingleOrDefault(brans => brans.Adi == "Cerrahi").Id,

                        DoktorHastalar = new List<DoktorHasta>()
                        {
                            new DoktorHasta()
                            {
                                HastaId = _db.Hastalar.SingleOrDefault(hasta => hasta.Adi == "Çağıl" && hasta.Soyadi == "Alsaç").Id
                            },
                            new DoktorHasta()
                            {
                                HastaId = _db.Hastalar.SingleOrDefault(hasta => hasta.Adi == "Leo" && hasta.Soyadi == "Alsaç").Id
                            }
                        }
                    },
                    new Doktor()
                    {
                        Guid = Guid.NewGuid().ToString(),
                        Adi = "Ferman",
                        Soyadi = "Eryiğit",
                        UzmanMi = true,
                        BransId = _db.Branslar.SingleOrDefault(brans => brans.Adi == "Cerrahi").Id,
                        DoktorHastalar = new List<DoktorHasta>()
                        {
                            new DoktorHasta()
                            {
                                HastaId = _db.Hastalar.SingleOrDefault(hasta => hasta.Adi == "Luna" && hasta.Soyadi == "Alsaç").Id
                            },
                            new DoktorHasta()
                            {
                                HastaId = _db.Hastalar.SingleOrDefault(hasta => hasta.Adi == "Leo" && hasta.Soyadi == "Alsaç").Id
                            }
                        }
                    },
                    new Doktor()
                    {
                        Guid = Guid.NewGuid().ToString(),
                        Adi = "Gülseren",
                        Soyadi = "Budayıcıoğlu",
                        UzmanMi = true,
                        BransId = _db.Branslar.SingleOrDefault(brans => brans.Adi == "Psikiyatri").Id
                    }
                }
            });

            _db.Roller.Add(new Rol()
            {
                Adi = "admin",
                Guid = Guid.NewGuid().ToString(),
                Kullanicilar = new List<Kullanici>()
                {
                    new Kullanici()
                    {
                        AktifMi = true,
                        Guid = Guid.NewGuid().ToString(),
                        KullaniciAdi = "cagil",
                        Sifre = "cagil"
                    }
                }
            });
			_db.Roller.Add(new Rol()
			{
				Adi = "kullanıcı",
				Guid = Guid.NewGuid().ToString(),
				Kullanicilar = new List<Kullanici>()
				{
					new Kullanici()
					{
						AktifMi = true,
						Guid = Guid.NewGuid().ToString(),
						KullaniciAdi = "leo",
						Sifre = "leo"
					}
				}
			});

			_db.SaveChanges();
            #endregion

            return Content("<label style=\"color:red;\">Database seed successful.</label>", "text/html");
        }
    }
}
