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

            foreach (var item in db.Profilok)
            {
                Console.WriteLine(item);
            }
        }
    }
}