using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web;

namespace web
{
    public class RecordMap : ClassMap<Record>
    {
        public RecordMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Content);

            References(x => x.Module);

        }


    }

    public class ModuleMap : ClassMap<Module>
    {
        public ModuleMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);

            HasMany(x => x.Records)
               .Inverse()
               .Cascade.AllDeleteOrphan();
        }
    }

}
