using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPr18Generalskiy
{
    public partial class StudentsEntities : DbContext
    {
        public static StudentsEntities context;
        public static StudentsEntities GetContext()
        {
            if (context == null)
                context = new StudentsEntities();
            return context;
        }

    }
}
