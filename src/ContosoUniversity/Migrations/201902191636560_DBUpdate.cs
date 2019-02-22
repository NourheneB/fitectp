namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.File",
                c => new
                    {
                        FileID = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                        Path = c.String(),
                        PersonID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileID)
                .ForeignKey("dbo.Person", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.Lesson",
                c => new
                    {
                        LessonID = c.Int(nullable: false, identity: true),
                        InstructorID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                        StartHour = c.DateTime(nullable: false),
                        EndHour = c.DateTime(nullable: false),
                        DateStart = c.DateTime(nullable: false),
                        Day = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LessonID)
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.InstructorID, cascadeDelete: true)
                .Index(t => t.InstructorID)
                .Index(t => t.CourseID);
            
            AddColumn("dbo.Person", "Login", c => c.String(nullable: false));
            AddColumn("dbo.Person", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lesson", "InstructorID", "dbo.Person");
            DropForeignKey("dbo.Lesson", "CourseID", "dbo.Course");
            DropForeignKey("dbo.File", "PersonID", "dbo.Person");
            DropIndex("dbo.Lesson", new[] { "CourseID" });
            DropIndex("dbo.Lesson", new[] { "InstructorID" });
            DropIndex("dbo.File", new[] { "PersonID" });
            DropColumn("dbo.Person", "Password");
            DropColumn("dbo.Person", "Login");
            DropTable("dbo.Lesson");
            DropTable("dbo.File");
        }
    }
}
