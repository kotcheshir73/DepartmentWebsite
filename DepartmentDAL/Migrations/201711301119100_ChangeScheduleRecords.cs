namespace DepartmentDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeScheduleRecords : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConsultationRecords", "DisciplineId", c => c.Long());
            AddColumn("dbo.ExaminationRecords", "DisciplineId", c => c.Long());
            AddColumn("dbo.OffsetRecords", "DisciplineId", c => c.Long());
            AddColumn("dbo.SemesterRecords", "DisciplineId", c => c.Long());
            CreateIndex("dbo.ConsultationRecords", "DisciplineId");
            CreateIndex("dbo.ExaminationRecords", "DisciplineId");
            CreateIndex("dbo.OffsetRecords", "DisciplineId");
            CreateIndex("dbo.SemesterRecords", "DisciplineId");
            AddForeignKey("dbo.ConsultationRecords", "DisciplineId", "dbo.Disciplines", "Id");
            AddForeignKey("dbo.ExaminationRecords", "DisciplineId", "dbo.Disciplines", "Id");
            AddForeignKey("dbo.OffsetRecords", "DisciplineId", "dbo.Disciplines", "Id");
            AddForeignKey("dbo.SemesterRecords", "DisciplineId", "dbo.Disciplines", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SemesterRecords", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.OffsetRecords", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.ExaminationRecords", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.ConsultationRecords", "DisciplineId", "dbo.Disciplines");
            DropIndex("dbo.SemesterRecords", new[] { "DisciplineId" });
            DropIndex("dbo.OffsetRecords", new[] { "DisciplineId" });
            DropIndex("dbo.ExaminationRecords", new[] { "DisciplineId" });
            DropIndex("dbo.ConsultationRecords", new[] { "DisciplineId" });
            DropColumn("dbo.SemesterRecords", "DisciplineId");
            DropColumn("dbo.OffsetRecords", "DisciplineId");
            DropColumn("dbo.ExaminationRecords", "DisciplineId");
            DropColumn("dbo.ConsultationRecords", "DisciplineId");
        }
    }
}
