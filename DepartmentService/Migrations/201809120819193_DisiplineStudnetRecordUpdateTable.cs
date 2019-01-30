namespace DepartmentService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisiplineStudnetRecordUpdateTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DisciplineStudentRecords", "Semester", c => c.Int(nullable: false));
            AlterColumn("dbo.DisciplineStudentRecords", "Variant", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DisciplineStudentRecords", "Variant", c => c.Int(nullable: false));
            DropColumn("dbo.DisciplineStudentRecords", "Semester");
        }
    }
}
