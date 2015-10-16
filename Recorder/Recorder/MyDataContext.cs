using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recorder
{
    [Database(Name="")]
    public class MyDataContext:DataContext
    {
        public MyDataContext(string connectionString)
            : base(connectionString)
        { 
        
        }

        Table<Record> MyRecord { get; set; }
    }
}
