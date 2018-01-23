using Microsoft.EntityFrameworkCore.ChangeTracking;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SomeUI
{
    internal static class DisconnectedMethods
    {
        private static void DisplayState(List<EntityEntry> es, string method)
        {
            Console.WriteLine(method);
            es.ForEach(e => Console.WriteLine(
                $"{e.Entity.GetType().Name} : {e.State.ToString()}"));
            Console.WriteLine();
        }

        public static void AddGraphAllNew()
        {
            var samuraiGraph = new Samurai { Name = "Julie" };
            samuraiGraph.Quotes.Add(new Quote { Text = "This is new" });
            using (var context = new SamuraiContext())
            {
                context.Samurais.Add(samuraiGraph);
                var es = context.ChangeTracker.Entries().ToList();
                DisplayState(es, "AddGraphAllNew");
            }
        }

        public static void AddGraphWithKeyValues()
        {
            var samuraiGraph = new Samurai { Name = "Julie", Id = 1 };
            samuraiGraph.Quotes.Add(new Quote { Text = "This is not new", Id = 1 });
            using (var context = new SamuraiContext())
            {
                context.Samurais.Add(samuraiGraph);
                var es = context.ChangeTracker.Entries().ToList();
                DisplayState(es, "AddGraphWithKeyValues");
            }
        }
        public static void AttachGraphAllNew()
        {
            var samuraiGraph = new Samurai { Name = "Julie" };
            samuraiGraph.Quotes.Add(new Quote { Text = "This is new" });
            using (var context = new SamuraiContext())
            {
                context.Samurais.Attach(samuraiGraph);
                var es = context.ChangeTracker.Entries().ToList();
                DisplayState(es, "AttachGraphAllNew");
            }
        }

        public static void AttachGraphWithKeyValues()
        {
            var samuraiGraph = new Samurai { Name = "Julie", Id = 1 };
            samuraiGraph.Quotes.Add(new Quote { Text = "This is not new", Id = 1 });
            using (var context = new SamuraiContext())
            {
                context.Samurais.Attach(samuraiGraph);
                var es = context.ChangeTracker.Entries().ToList();
                DisplayState(es, "AttachGraphWithKeyValues");
            }
        }

        public static void UpdateGraphAllNew()
        {
            var samuraiGraph = new Samurai { Name = "Julie" };
            samuraiGraph.Quotes.Add(new Quote { Text = "This is new" });
            using (var context = new SamuraiContext())
            {
                context.Samurais.Update(samuraiGraph);
                var es = context.ChangeTracker.Entries().ToList();
                DisplayState(es, "UpdateGraphAllNew");
            }
        }

        public static void UpdateGraphWithKeyValues()
        {
            var samuraiGraph = new Samurai { Name = "Julie", Id = 1 };
            samuraiGraph.Quotes.Add(new Quote { Text = "This is not new", Id = 1 });
            using (var context = new SamuraiContext())
            {
                context.Samurais.Update(samuraiGraph);
                var es = context.ChangeTracker.Entries().ToList();
                DisplayState(es, "UpdateGraphWithKeyValues");
            }
        }
        public static void DeleteGraphAllNew()
        {
            var samuraiGraph = new Samurai { Name = "Julie" };
            samuraiGraph.Quotes.Add(new Quote { Text = "This is new" });
            using (var context = new SamuraiContext())
            {
                context.Samurais.Remove(samuraiGraph);
                var es = context.ChangeTracker.Entries().ToList();
                DisplayState(es, "DeleteGraphAllNew");
            }
        }

        public static void DeleteGraphWithKeyValues()
        {
            var samuraiGraph = new Samurai { Name = "Julie", Id = 1 };
            samuraiGraph.Quotes.Add(new Quote { Text = "This is not new", Id = 1 });
            using (var context = new SamuraiContext())
            {
                context.Samurais.Remove(samuraiGraph);
                var es = context.ChangeTracker.Entries().ToList();
                DisplayState(es, "DeleteGraphWithKeyValues");
            }
        }

    }
}
