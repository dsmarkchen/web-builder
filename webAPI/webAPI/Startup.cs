using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NHibernate.Tool.hbm2ddl;

namespace webAPI
{
    public class Startup
    {
        private NHibernate.Cfg.Configuration _cfg;
        //private NHibernate.ISession _session;
        private bool exported = false;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private void ExportSchema(NHibernate.Cfg.Configuration cfg, NHibernate.ISession session)
        {
            var export = new SchemaExport(cfg);

            using (var file = new FileStream(@"c:\temp\create.objects.sql", FileMode.Create, FileAccess.Write))
            {
                using (var sw = new StreamWriter(file))
                {
                    export.Execute(true, true, false, session.Connection, sw);
                    sw.Close();
                }
            }
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
                string _db_filename = @"c:\tempsq\web-builder.sq3";

                services.AddSingleton<FluentConfiguration>((provider) => {

                var cfg2 = Fluently.Configure();                
                cfg2.Database(SQLiteConfiguration.Standard.UsingFile(_db_filename));
                cfg2.Mappings(m =>
                {
                    m.FluentMappings.Conventions.Setup(c => c.Add(AutoImport.Never()));
                    m.FluentMappings.Conventions.AddAssembly(Assembly.GetExecutingAssembly());
                    m.HbmMappings.AddFromAssembly(Assembly.GetExecutingAssembly());

                    var assembly = Assembly.Load("web");
                    m.FluentMappings.Conventions.AddAssembly(assembly);
                    m.FluentMappings.AddFromAssembly(assembly);
                    m.HbmMappings.AddFromAssembly(assembly);

                });
                cfg2.ExposeConfiguration(cfg =>
                {
                    cfg.SetProperty("current_session_context_class", "thread_static");
                    _cfg = cfg;
                });

                
                return cfg2;
            });

            services.AddSingleton<NHibernate.ISessionFactory>((provider) => {
                var cfg = provider.GetService<FluentConfiguration>();
                var fact = cfg.BuildSessionFactory();

               
                return fact;
            });

            services.AddScoped<NHibernate.ISession>((provider) =>
            {
                var cfg = provider.GetService<FluentConfiguration>();
                NHibernate.ISessionFactory factory = provider.GetService<NHibernate.ISessionFactory>();
                return factory.OpenSession();
            });

            services.AddScoped<NHibernate.Cfg.Configuration>((provider) =>
            {
                return _cfg;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // angularjs client project start with port 8000
            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:8000", "http://192.168.1.75:8000")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            app.UseMvc();
        }
    }

   




}
