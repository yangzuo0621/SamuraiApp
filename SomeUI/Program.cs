using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            //AddManyToManyWithFks();
            //AddManyToManyWithObjects();

            //EagerLoadWithInclude();
            //EagerLoadManyToManyAkaChildrenGrandchildren();
            //EagerLoadWithMultipleBranches();
            //EagerLoadWithFromSql();
            //AnonymousTypeViaProjection();
            //AnonymousViaProjectionWithRelated();
            //RelatedObjectsFixup();
            //EagerLoadViaProjectionNotQuite();
            //FilteredEagerLoadViaProjectionNope();

            //ExplicitLoad();
            //ExplicitLoadWithChildFilter();
            //UsingRelatedDataForFiltersAndMore();

            //DisconnectedMethods.AddGraphAllNew();
            //DisconnectedMethods.AddGraphWithKeyValues();
            //DisconnectedMethods.AttachGraphAllNew();
            //DisconnectedMethods.AttachGraphWithKeyValues();
            //DisconnectedMethods.UpdateGraphAllNew();
            //DisconnectedMethods.UpdateGraphWithKeyValues();
            //DisconnectedMethods.DeleteGraphAllNew();
            //DisconnectedMethods.DeleteGraphWithKeyValues();
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

        private static void EagerLoadWithInclude()
        {
            _context = new SamuraiContext();
            var samuraiWithQuotes = _context.Samurais.Include(s => s.Quotes).ToList();
        }

        private static void EagerLoadManyToManyAkaChildrenGrandchildren()
        {
            _context = new SamuraiContext();
            var samuraiWithBattles = _context.Samurais.Include(s => s.SamuraiBattles)
                .ThenInclude(sb => sb.Battle)
                .ToList();
        }

        private static void EagerLoadWithMultipleBranches()
        {
            _context = new SamuraiContext();
            var samurais = _context.Samurais
                .Include(s => s.SecretIdentity)
                .Include(s => s.Quotes)
                .ToList();
        }

        private static void EagerLoadWithFromSql()
        {
            var samurais = _context.Samurais.FromSql("select * from samurais")
                .Include(s => s.Quotes)
                .ToList();
        }

        private static void AnonymousTypeViaProjection()
        {
            _context = new SamuraiContext();
            var quotes = _context.Quotes
                .Select(q => new { q.Id, q.Text })
                .ToList();
        }

        private static void AnonymousViaProjectionWithRelated()
        {
            _context = new SamuraiContext();
            var samurais = _context.Samurais
                .Select(s => new
                {
                    s.Id,
                    s.SecretIdentity.RealName,
                    QuoteCount = s.Quotes.Count
                })
                .ToList();
        }

        private static void RelatedObjectsFixup()
        {
            _context = new SamuraiContext();
            var sumarai = _context.Samurais.Find(1);
            var quotes = _context.Quotes.Where(q => q.SamuraiId == 1).ToList();
        }

        private static void EagerLoadViaProjectionNotQuite()
        {
            _context = new SamuraiContext();
            var samurais = _context.Samurais
                .Select(s => new { Samurai = s, Quotes = s.Quotes })
                .ToList();
        }

        private static void FilteredEagerLoadViaProjectionNope()
        {
            _context = new SamuraiContext();
            var sumarais = _context.Samurais
                .Select(s => new
                {
                    Samurai = s,
                    Quotes = s.Quotes.Where(q => q.Text.Contains("happy")).ToList()
                })
                .ToList();
        }

        private static void ExplicitLoad()
        {
            _context = new SamuraiContext();
            var samurai = _context.Samurais.FirstOrDefault();
            _context.Entry(samurai).Collection(s => s.Quotes).Load();
            _context.Entry(samurai).Reference(s => s.SecretIdentity).Load();
        }

        private static void ExplicitLoadWithChildFilter()
        {
            _context = new SamuraiContext();
            var samurai = _context.Samurais.FirstOrDefault();
            _context.Entry(samurai)
                .Collection(s => s.Quotes)
                .Query()
                .Where(q => q.Text.Contains("happy"))
                .Load();
        }

        private static void UsingRelatedDataForFiltersAndMore()
        {
            _context = new SamuraiContext();
            var samurais = _context.Samurais
                .Where(s => s.Quotes.Any(q => q.Text.Contains("happy")))
                .ToList();
        }
    }
}