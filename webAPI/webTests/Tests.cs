using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using web;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace fooTests
{
    [TestFixture]
    public class SimpleTests
    {
        [Test]
        public void test1()
        {
            int i = 0;
            Assert.That(i, Is.EqualTo(0));
        }
    }
    [TestFixture]
    public class RecordTests
    {
        InMemorySqLiteSessionFactory sqliteSessionFactory;

        [SetUp]
        public void init()
        {
            sqliteSessionFactory = new InMemorySqLiteSessionFactory();
        }
        [TearDown]
        public void free()
        {
            sqliteSessionFactory.Dispose();
        }

        [Test]
        public void test1()
        {            
            Record foo = new Record() { Name = "foo"};

            using (ISession session = sqliteSessionFactory.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Assert.That(foo.Id == 0);

                    session.SaveOrUpdate(foo);

                    transaction.Commit();

                    Assert.That(foo.Id > 0);
                }

            }
        }

        
    }

    public class InMemorySqLiteSessionFactory : IDisposable
    {
        private Configuration _configuration;
        private ISessionFactory _sessionFactory;

        public ISession Session { get; set; }

        public InMemorySqLiteSessionFactory()
        {
            _sessionFactory = CreateSessionFactory();
            Session = _sessionFactory.OpenSession();
            ExportSchema();
        }
        public ISession reopen()
        {
            return _sessionFactory.OpenSession();
        }
        private void ExportSchema()
        {
            var export = new SchemaExport(_configuration);

            using (var file = new FileStream(@"c:\temp\create.objects.sql", FileMode.Create, FileAccess.Write))
            {
                using (var sw = new StreamWriter(file))
                {
                    export.Execute(true, true, false, Session.Connection, sw);
                    sw.Close();
                }
            }
        }

        private ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                     .Database(SQLiteConfiguration.Standard.InMemory().ShowSql())
                     .Mappings(m =>
                     {
                         m.FluentMappings.Conventions.Setup(c => c.Add(AutoImport.Never()));
                         m.FluentMappings.Conventions.AddAssembly(Assembly.GetExecutingAssembly());
                         m.HbmMappings.AddFromAssembly(Assembly.GetExecutingAssembly());

                         var assembly = Assembly.Load("web");
                         m.FluentMappings.Conventions.AddAssembly(assembly);
                         m.FluentMappings.AddFromAssembly(assembly);
                         m.HbmMappings.AddFromAssembly(assembly);

                     })
                     .ExposeConfiguration(cfg => _configuration = cfg)
                     .BuildSessionFactory();
        }

        public void Dispose()
        {
            Session.Dispose();
            _sessionFactory.Close();
        }
    }
}
