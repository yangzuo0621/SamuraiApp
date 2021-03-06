﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SomeUI
{
    internal class Module4Methods
    {
        private static SamuraiContext _context = new SamuraiContext();

        public static void RunAll()
        {
            //InsertSamurai();
            //InsertMultipleSamurais();
            //SimpleSamuraiQuery();
            //MoreQueries();
            //RetrieveAndUpdateSamurai();
            //RetrieveAndUpdateMultipleSamurais();
            //RawSqlCommand();
            //RawSqlQuery();
            RawSqlCommandWithOutput();
        }

        private static void RawSqlCommand()
        {
            var affected = _context.Database.ExecuteSqlCommand(
                "update samurais set Name=REPLACE(Name, 'San', 'Nan')");
            Console.WriteLine($"Affected rows {affected}");
        }

        private static void RawSqlQuery()
        {
            //var samurais = _context.Samurais.FromSql("Select * from Samurais")
            //                .OrderByDescending(s => s.Name)
            //                .Where(s => s.Name.Contains("San"))
            //                .ToList();
            var namePart = "San";
            var samurais = _context.Samurais
                .FromSql("EXEC FilterSamuraiByNamePart {0}", namePart)
                .OrderByDescending(s => s.Name).ToList();

            samurais.ForEach(s => Console.WriteLine(s.Name));
            Console.WriteLine();
        }

        private static void RawSqlCommandWithOutput()
        {
            var procResult = new SqlParameter
            {
                ParameterName = "@procResult",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Output,
                Size = 50
            };
            _context.Database.ExecuteSqlCommand(
                "EXEC FindLongestName @procResult OUT", procResult);
            Console.WriteLine($"Longest name: {procResult.Value}");
        }

        private static void QueryWithNonSql()
        {
            var samurais = _context.Samurais
                .Select(s => new { newName = ReverseString(s.Name) })
                .ToList();
            samurais.ForEach(s => Console.WriteLine(s.newName));
            Console.WriteLine();
        }

        private static string ReverseString(string value)
        {
            var stringChar = value.AsEnumerable();
            return string.Concat(stringChar.Reverse());
        }

        private static void DeleteWhileTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Kambei Shimada");
            _context.Samurais.Remove(samurai);
            // alternates:
            // _context.Remove(samurai);
            // _context.Entry(samurai).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            // _context.Samurais.Remove(_context.Samurais.Find(1));
            _context.SaveChanges();
        }

        private static void DeleteMany()
        {
            var samurais = _context.Samurais.Where(s => s.Name.Contains("o"));
            _context.Samurais.RemoveRange(samurais);
            // alternate
            //_context.RemoveRange(samurais);
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateMultipleSamurais()
        {
            var samurais = _context.Samurais.ToList();
            samurais.ForEach(s => s.Name += "San");
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();
        }

        private static void MoreQueries()
        {
            var samurais = _context.Samurais.Where(s => s.Name == "Sampson").ToList();
        }

        private static void SimpleSamuraiQuery()
        {
            using (var context = new SamuraiContext())
            {
                var samurais = context.Samurais.ToList();
            }
        }

        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "Julie" };
            var samuraiSammy = new Samurai { Name = "Sampson" };
            _context.Samurais.AddRange(new List<Samurai> { samurai, samuraiSammy });
            _context.SaveChanges();
        }

        private static void InsertSamurai()
        {
            var samurai = new Samurai { Name = "Julie" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
    }
}