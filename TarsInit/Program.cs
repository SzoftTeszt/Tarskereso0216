using Microsoft.EntityFrameworkCore;
using System;
using TarsInit.Data;
using TarsInit.Model;


namespace MyApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            string conn = "Server=localhost; User ID=szoft; Password=alma; Database=tarskereso";
            var serverVersion = new MariaDbServerVersion(ServerVersion.AutoDetect(conn));

            var options = new DbContextOptionsBuilder<TarskeresoContext>()
                .UseMySql(conn, serverVersion)
                .Options;

            using var db = new TarskeresoContext(options);

            if (!db.Erdeklodesek.Any())
            {

                var sorok = File.ReadAllLines("erdeklodesek.txt").Skip(1); //LINQ
                foreach (var sor in sorok)
                {
                    db.Erdeklodesek.Add(new Erdeklodes(sor));
                }
                db.SaveChanges();
            }

            if (!db.Profilok.Any())
            {

                var sorok = File.ReadAllLines("profilok.txt").Skip(1); //LINQ
                foreach (var sor in sorok)
                {
                    db.Profilok.Add(new Profil(sor));
                }
                db.SaveChanges();
            }

            if (!db.Profilerdeklodesek.Any())
            {

                var sorok = File.ReadAllLines("profilerdeklodes.txt").Skip(1); //LINQ
                foreach (var sor in sorok)
                {
                    db.Profilerdeklodesek.Add(new Profilerdeklodes(sor));
                }
                db.SaveChanges();



            }
            //select* from `profilok`;
            foreach (var item in db.Profilok)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            Console.WriteLine("2. feladat");
            var q2 = db.Profilok.Where(x => x.Varos.Equals("Budapest"));
            foreach (var item in q2) {
                Console.WriteLine(item.Nev+" "+item.Varos+","+item.Cel);
            }
            //Console.WriteLine();
            //Console.WriteLine("2. feladat");
            //var q2 = db.Profilok.Where(x=>x.Varos.Equals("Budapest")).Select(p=> new {p.Nev, p.Varos, p.Cel });
            //foreach (var item in q2) {
            //    Console.WriteLine(item);
            //}

            Console.WriteLine();
            Console.WriteLine("4. feladat");

            var q4 = db.Profilok.Where(z=>z.Eletkor>=25 && z.Eletkor<=35 && z.Cel.Equals("komoly kapcsolat"));
            foreach (var item in q4)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            Console.WriteLine("5. feladat");
            //foreach (var item in db.Profilok.OrderBy(x=>x.Nev))
            //{
            //    Console.WriteLine(item);
            //}

            db.Profilok.OrderBy(x => x.Nev).ToList().ForEach(x=>Console.WriteLine(x));

            Console.WriteLine();
            Console.WriteLine("6. feladat");
            var q6 = db.Profilok
                .OrderByDescending(x => x.MagassagCm)
                .Take(10);
            foreach (var item in q6)
            {
                Console.WriteLine(item+" - "+item.MagassagCm+" cm");
            }

            Console.WriteLine();
            Console.WriteLine("7. feladat");
            var q71 = db.Profilok.Select(x => x.Varos);
            var q72 = db.Profilok.Select(x => x.Varos).Distinct();
            Console.WriteLine("Vásrosok ismétlődéssel: "+q71.Count());
            Console.WriteLine("Városok száma: "+q72.Count());


            //Console.WriteLine("Emberek és érdeklődésük");
            //Console.WriteLine();

            //var profils = db.Profilok.AsNoTracking().Include(p => p.ProfilErdeklodes).ThenInclude(p => p.Erdeklodes).ToList();


            //var qk= profils.OrderByDescending(x => x.EvesBevetelHuf).Take(1).ToList();
            //foreach (var item in qk)
            //{
            //    Console.WriteLine(item);
            //    Console.WriteLine("A gazdag csaj érdeklődései: ");
            //    foreach (var pe in item.ProfilErdeklodes)
            //    {
            //        Console.WriteLine(pe.Erdeklodes+"+"+pe.ErdeklodesId);
            //    }
            //}

            Console.WriteLine();
            Console.WriteLine("8. feladat");

            var q8 = db.Profilerdeklodesek.Select(x => x.Erdeklodes).Distinct();

            foreach (var e in q8)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine();
            Console.WriteLine("9. feladat");

            var q9= db.Profilok.GroupBy(x => x.Varos)
                .Select(x=> new {Varos=x.Key, ProfilDb= x.Count() })
                .OrderBy(x=>x.Varos);

            foreach (var e in q9)
            {
                Console.WriteLine(e);
            }
        }
    }
}