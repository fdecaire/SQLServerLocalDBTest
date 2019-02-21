using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace SQLServerLocalDBTest
{
  class Program
  {
    //http://www.mssqltips.com/sqlservertip/2694/getting-started-with-sql-server-2012-express-localdb/
    //http://blogs.msdn.com/b/jerrynixon/archive/2012/02/26/sql-express-v-localdb-v-sql-compact-edition.aspx

    static void Main(string[] args)
    {
      using (ISession session = NHibernateHelper.OpenSession())
      {
        var query = (from t in session.Query<Teacher>()
                     join c in session.Query<SchoolClass>() on t.Id equals c.TeacherId
                     join s in session.Query<Student>() on c.Id equals s.classid
                     select new
                       {
                         TeacherName = t.Name,
                         ClassName = c.Name,
                         StudentName = s.Name
                       }).ToList();


      }
    }
  }
}
