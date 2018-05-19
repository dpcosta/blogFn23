namespace Caelum.Fn23.Curso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Autor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "AutorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Posts", "AutorId");
            AddForeignKey("dbo.Posts", "AutorId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "AutorId", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "AutorId" });
            DropColumn("dbo.Posts", "AutorId");
        }
    }
}
