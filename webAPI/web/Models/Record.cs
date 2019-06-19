using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web
{
    public class Record
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
        public virtual RecordType Type
        {
            get;
            set;
        }

        public virtual Module Module
        {
            get;
            set;
        }
    }

    public enum RecordType {
        rec_string,
        rec_int,
        rec_datetime           
    }

    public class RecordCollection : ICollection<Record>
    {
        // The inner collection to store objects.
        private List<Record> innerCol;

        public RecordCollection()
        {
            innerCol = new List<Record>();
        }


        public Record this[int index]
        {
            get { return (Record)innerCol[index]; }
            set { innerCol[index] = value; }
        }
        public void Add(Record item)
        {

            if (!Contains(item))
            {
                innerCol.Add(item);
            }

        }
        public IEnumerator<Record> GetEnumerator()
        {
            return (IEnumerator<Record>)innerCol;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)innerCol;
        }

        public bool Contains(Record item, EqualityComparer<Record> comp)
        {
            bool found = false;

            foreach (Record bx in innerCol)
            {
                if (comp.Equals(bx, item))
                {
                    found = true;
                }
            }

            return found;
        }
        public bool Contains(Record item)
        {
            bool found = false;

            foreach (Record bx in innerCol)
            {
                if (bx.Equals(item))
                {
                    found = true;
                }
            }

            return found;
        }

        public int Count
        {
            get
            {
                return innerCol.Count;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }


        public void Clear()
        {
            innerCol.Clear();
        }

        public void CopyTo(Record[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("The array cannot be null.");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("The starting array index cannot be negative.");
            if (Count > array.Length - arrayIndex + 1)
                throw new ArgumentException("The destination array has fewer elements than the collection.");

            for (int i = 0; i < innerCol.Count; i++)
            {
                array[i + arrayIndex] = innerCol[i];
            }
        }

        public bool Remove(Record item)
        {
            bool result = false;

            // Iterate the inner collection to 
            // find the box to be removed.
            for (int i = 0; i < innerCol.Count; i++)
            {

                Record curBook = (Record)innerCol[i];

                if (curBook.Name == item.Name)
                {
                    innerCol.RemoveAt(i);
                    result = true;
                    break;
                }
            }
            return result;
        }
    }

}
