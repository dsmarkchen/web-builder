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
                var r1 = new Record { Name = "Committed to Health and Safety", Content = "Our safety performance has demonstrated that operational success and safety can be achieved collaboratively. ",  Type = RecordType.rec_string };
                var r2 = new Record { Name = "Focused on Environmentally Safe Solutions", Content = "We demonstrate our commitment to being environmentally-conscious through: tada", Type = RecordType.rec_string };

                run11.AddRecord(r1);
                run11.AddRecord(r2);

                var run21 = new web.Module { Id = 0, Name = "Team", };
                var r21 = new Record { Name = "BH", Content = "phd ", Type = RecordType.rec_string };
                var r22 = new Record { Name = "SP", Content = "this is content", Type = RecordType.rec_string };
                run21.AddRecord(r21);
                run21.AddRecord(r22);

                using (var transaction = session.BeginTransaction())
                {
                    session.Save(run11);

                    session.Save(run21);

                    transaction.Commit();
                }

                var foos = session.CreateCriteria(typeof(web.Module)).List < web.Module>();

            }

        }
    }

    
    
}
