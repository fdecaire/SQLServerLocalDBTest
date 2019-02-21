using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using SQLServerLocalDBTest;

namespace UnitTestProject1
{
    public class TestSessionFactory
    {
        private static ISessionFactory SessionFactory
        {
            get
            {
                return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2005
                .ConnectionString("Server=(localdb)\\testinstance;Integrated Security=True"))
                .Mappings(m => m.FluentMappings.Add<SchoolClassMap>())
                .Mappings(m => m.FluentMappings.Add<TeacherMap>())
                .Mappings(m => m.FluentMappings.Add<StudentMap>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                .BuildSessionFactory();
            }
        }
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
