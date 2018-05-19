namespace Caelum.Fn23.Curso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Validacoes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Titulo", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Posts", "Resumo", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Resumo", c => c.String());
            AlterColumn("dbo.Posts", "Titulo", c => c.String());
        }
    }
}
