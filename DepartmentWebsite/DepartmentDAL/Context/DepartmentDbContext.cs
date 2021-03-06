﻿using DepartmentDAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace DepartmentDAL.Context
{
    [Table("DepartmentDatabase")]
    public class DepartmentDbContext : DbContext
    {
        public DepartmentDbContext()
        {//настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            Database.SetInitializer(new CreateDatabaseIfNotExists<DepartmentDbContext>());
        }

        public virtual DbSet<Access> Access { set; get; }
        public virtual DbSet<Classroom> Classrooms { set; get; }
        public virtual DbSet<EducationDirection> EducationDirections { set; get; }
        public virtual DbSet<Lecturer> Lecturers { set; get; }
        public virtual DbSet<Message> Messages { set; get; }
        public virtual DbSet<SemesterRecord> SemesterRecords { set; get; }
        public virtual DbSet<Role> Roles { set; get; }
        public virtual DbSet<Student> Students { set; get; }
        public virtual DbSet<StudentGroup> StudentGroups { set; get; }
        public virtual DbSet<User> Users { set; get; }
    }
}
