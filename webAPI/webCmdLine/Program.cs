using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using web;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using webCmdLine;

namespace webCmdLine
{
    class Program
    {
        static void Main(string[] args)
        {
            

           

            {
                ISession session = xSessionManager.GetCurrentSession();
                xSessionManager.ExportSchema();
                DateTime dt = DateTime.Now;
                var run11 = new web.Module { Id = 0, Name = "HSE", };
                var r1 = new Record { Name = "Title", Type = RecordType.rec_string };
                var r2 = new Record { Name = "Content", Type = RecordType.rec_string };

                run11.AddRecord(r1);
                run11.AddRecord(r2);


                using (var transaction = session.BeginTransaction())
                {
                    session.Save(run11);


                    transaction.Commit();
                }

                var foos = session.CreateCriteria(typeof(web.Module)).List < web.Module>();

            }

        }
    }

    
    
}
