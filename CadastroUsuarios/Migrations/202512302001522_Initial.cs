namespace CadastroUsuarios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsuarioModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ativo = c.Boolean(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Sobrenome = c.String(nullable: false, maxLength: 100),
                        NomeSocial = c.String(maxLength: 100),
                        DataNascimento = c.DateTime(nullable: false),
                        Senha = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UsuarioModels");
        }
    }
}
