using CadastroUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CadastroUsuarios.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=DefaultConnection")
        {
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }

    }
}