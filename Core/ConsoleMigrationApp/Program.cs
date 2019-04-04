using System;
using Microsoft.EntityFrameworkCore;

namespace ConsoleMigrationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DatabaseContext.DatabaseContext())
            {
                db.Database.Migrate();
            }
            Console.WriteLine("Hello World!");
        }
    }
}


//Migration: Add-Migration InitialCreate -StartupProject ConsoleMigrationApp