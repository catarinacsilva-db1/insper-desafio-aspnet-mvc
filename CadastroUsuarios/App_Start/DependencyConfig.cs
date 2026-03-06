using Autofac;
using Autofac.Integration.Mvc;
using CadastroUsuarios.Controllers.Utils;
using CadastroUsuarios.Data;
using CadastroUsuarios.Repositories;
using CadastroUsuarios.Service;
using CadastroUsuarios.Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CadastroUsuarios.App_Start
{
    public class DependencyConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AppDbContext>().InstancePerRequest();

            builder.RegisterType<UsuarioRepository>().As<IUsuarioRepository>().InstancePerRequest();
            builder.RegisterType<UsuarioService>().As<IUsuarioService>().InstancePerRequest();

            builder.RegisterType<Validators>().InstancePerRequest();
            builder.RegisterType<FiltroQueries>().InstancePerRequest();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}