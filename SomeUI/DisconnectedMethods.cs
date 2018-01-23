using Microsoft.EntityFrameworkCore;
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

        public static void AddGraphViaEntryAllNew()
        {
            var samuraiGraph = new Samurai { Name = "Julie" };
            samuraiGraph.Quotes.Add(new Quote { Text = "This is new" });
            using (var context = new SamuraiContext())
            {
                context.Entry(samuraiGraph).State = EntityState.Added;
                var es = context.ChangeTracker.Entries().ToList();
                DisplayState(es, "AddGraphViaEntryAllNew");
            }
        }

        public static void AddGraphViaEntryWithKeyValues()
        {
            var samuraiGraph = new Samurai { Name = "Julie", Id = 1 };
            samuraiGraph.Quotes.Add(new Quote { Text = "This is not new", Id = 1 });
            using (var context = new SamuraiContext())
            {
                context.Entry(samuraiGraph).State = EntityState.Added;
                var es = context.ChangeTracker.Entries().ToList();
                DisplayState(es, "AddGraphViaEntryWithKeyValues");
            }
        }

        public static void AttachGraphViaEntryAllNew()
        {
            var samuraiGraph = new Samurai { Name = "Julie" };
            samuraiGraph.Quotes.Add(new Quote { Text = "This is new" });
            using (var context = new SamuraiContext())
            {
                context.Entry(samuraiGraph).State = EntityState.Detached;
                var es = context.ChangeTracker.Entries().ToList();
                DisplayState(es, "AttachGraphViaEntryAllNew");
            }
        }

        public static void AttachGraphViaEntryWithKeyValues()
        {
            var samuraiGraph = new Samurai { Name = "Julie", Id = 1 };
            samuraiGraph.Quotes.Add(new Quote { Text = "This is not new", Id = 1 });
            using (var context = new SamuraiContext())
            {
                context.Entry(samuraiGraph).State = EntityState.Detached;
                var es = context.ChangeTracker.Entries().ToList();
                DisplayState(es, "AttachGraphViaEntryWithKeyValues");
            }
        }

        public static void UpdateGraphViaEntryAllNew()
        {
            var samuraiGraph = new Samurai { Name = "Julie" };
            samuraiGraph.Quotes.Add(new Quote { Text = "This is new" });
            using (var context = new SamuraiContext())
            {
                context.Entry(samuraiGraph).State = EntityState.Modified;
                var es = context.ChangeTracker.Entries().ToList();
                DisplayState(es, "UpdateGraphViaEntryAllNew");
            }
        }

        public static void UpdateGraphViaEntryWithKeyValues()
        {
            var samuraiGraph = new Samurai { Name = "Julie", Id = 1 };
            samuraiGraph.Quotes.Add(new Quote { Text = "This is not new", Id = 1 });
            using (var context = new SamuraiContext())
            {
                context.Entry(samuraiGraph).State = EntityState.Modified;
                var es = context.ChangeTracker.Entries().ToList();
                DisplayState(es, "UpdateGraphViaEntryWithKeyValues");
            }
        }
        public static void DeleteGraphViaEntryAllNew()
        {
            var samuraiGraph = new Samurai { Name = "Julie" };
            samuraiGraph.Quotes.Add(new Quote { Text = "This is new" });
            using (var context = new SamuraiContext())
            {
                context.Entry(samuraiGraph).State = EntityState.Deleted;
                var es = context.ChangeTracker.Entries().ToList();
                DisplayState(es, "DeleteGraphViaEntryAllNew");
            }
        }

        public static void DeleteGraphViaEntryWithKeyValues()
        {
            var samuraiGraph = new Samurai { Name = "Julie", Id = 1 };
            samuraiGraph.Quotes.Add(new Quote { Text = "This is not new", Id = 1 });
            using (var context = new SamuraiContext())
            {
                context.Entry(samuraiGraph).State = EntityState.Deleted;
                var es = context.ChangeTracker.Entries().ToList();
                DisplayState(es, "DeleteGraphViaEntryWithKeyValues");
            }
        }

        public static void ChangeStateUsingEntry()
        {
            var samurai = new Samurai { Name = "She Who Changes State", Id = 1 };
            using (var context = new SamuraiContext())
            {
                context.Entry(samurai).State = EntityState.Modified;
                Console.WriteLine("Change State Using Entry");
                DisplayState(context.ChangeTracker.Entries().ToList(), "Initial State");

                context.Entry(samurai).State = EntityState.Added;
                DisplayState(context.ChangeTracker.Entries().ToList(), "New State");
                context.SaveChanges();
            }
        }
        public static void AddGraphViaTrackGraphAllNew()
        {
            var samuraiGraph = new Samurai { Name = "Julie" };
            samuraiGraph.Quotes.Add(new Quote { Text = "This is new" });
            using (var context = new SamuraiContext())
            {
                context.ChangeTracker.TrackGraph(samuraiGraph, e => e.Entry.State = EntityState.Added);
                var es = context.ChangeTracker.Entries().ToList();
                DisplayState(es, "AddGraphViaEntryAllNew");
            }
        }

        public static void AddGraphViaTrackGraphWithKeyValues()
        {
            var samuraiGraph = new Samurai { Name = "Julie", Id = 1 };
            samuraiGraph.Quotes.Add(new Quote { Text = "This is not new", Id = 1 });
            using (var context = new SamuraiContext())
            {
                context.ChangeTracker.TrackGraph(samuraiGraph, e => e.Entry.State = EntityState.Added);
                var es = context.ChangeTracker.Entries().ToList();
                DisplayState(es, "AddGraphViaEntryWithKeyValues");
            }
        }


    }
}
