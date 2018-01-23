using System;
using System.Collections.Generic;
using System.Linq;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SomeUI
{
    internal class Program
    {
        private static SamuraiContext _context = new SamuraiContext();

        private static void Main(string[] args)
        {
            _context.Database.EnsureCreated();
            //InsertNewPkFkGraph();
            //InsertNewPkFkGraphMultipleChildren();
            //InsertNewOneToOneGraph();
            //AddChildToExistingObjectWhileTracked();
            //AddOneToOneToExistingObjectWhileTracked();
            //AddBattles();
            AddManyToManyWithObjects();
            AddManyToManyWithObjects();
        }

        private static void InsertNewPkFkGraph()
        {
            var samurai = new Samurai
            {
                Name = "Kambei Shimada",
                Quotes = new List<Quote>
                {
                    new Quote { Text = "I've come to save you" }
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void InsertNewPkFkGraphMultipleChildren()
        {
            var samurai = new Samurai
            {
                Name = "Kyuzo",
                Quotes = new List<Quote>
                {
                    new Quote { Text = "Watch out for my sharp sword!" },
                    new Quote { Text = "I told you to watch out for the sharp sword!" }
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void InsertNewOneToOneGraph()
        {
            var samurai = new Samurai { Name = "Shichiroji" };
            samurai.SecretIdentity = new SecretIdentity { RealName = "Julie" };
            _context.Add(samurai);
            _context.SaveChanges();
        }

        private static void AddChildToExistingObjectWhileTracked()
        {
            var samurai = _context.Samurais.First();
            samurai.Quotes.Add(new Quote { Text = "I bet you're happy that I've saved you!" });
            _context.SaveChanges();
        }

        private static void AddOneToOneToExistingObjectWhileTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.SecretIdentity == null);
            samurai.SecretIdentity = new SecretIdentity { RealName = "Sampson" };
            _context.SaveChanges();
        }

        private static void AddBattles()
        {
            _context.Battles.AddRange(
                new Battle { Name = "Battle of Shiroyama", StartDate = new DateTime(1877, 9, 24), EndDate = new DateTime(1877, 9, 24) },
                new Battle { Name = "Siege of Osaka", StartDate = new DateTime(1614, 1, 1), EndDate = new DateTime(1615, 12, 31) },
                new Battle { Name = "Boshin War", StartDate = new DateTime(1868, 1, 1), EndDate = new DateTime(1869, 1, 1) }
            );
            _context.SaveChanges();
        }

        private static void AddManyToManyWithFks()
        {
            _context = new SamuraiContext();
            var sb = new SamuraiBattle { SamuraiId = 1, BattleId = 1 };
            _context.SamuraiBattles.Add(sb);
            _context.SaveChanges();
        }

        private static void AddManyToManyWithObjects()
        {
            _context = new SamuraiContext();
            var samurai = _context.Samurais.FirstOrDefault();
            var battle = _context.Battles.FirstOrDefault();
            _context.SamuraiBattles.Add(
             new SamuraiBattle { Samurai = samurai, Battle = battle });
            _context.SaveChanges();
        }
    }
}