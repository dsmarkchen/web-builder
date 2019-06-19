using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web
{
    public class Module
    {
        public virtual int Id
        {
            get;
            set;
        }
        public virtual string Name
        {
            get;
            set;
        }
        public virtual ICollection<Record> Records
        {
            get;
            set;
        }


        public Module()
        {
            Records = new RecordCollection();
        }

        public virtual void AddRecord(Record rec)
        {
            rec.Module = this;
            Records.Add(rec);
        }


    }

    public class ModuleDto
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual int RecordsCount { get; set; }

        public List<RecordDto> Records { get; set; }

        public ModuleDto()
        {
            Records = new List<RecordDto>() { };
            RecordsCount = 0;
        }
    }
}
