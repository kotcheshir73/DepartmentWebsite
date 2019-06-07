using System;
using Microsoft.EntityFrameworkCore;

namespace ConsoleMigrationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DatabaseContext.DepartmentDatabaseContext())
            {
                db.Database.Migrate();
            }
            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}


//Migration: Add-Migration InitialCreate -StartupProject ConsoleMigrationApp