﻿using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace SQLServerLocalDBTest
{
  public class NHibernateHelper
  {
    private static ISessionFactory _sessionFactory;
    private static ISessionFactory SessionFactory
    {
      get
      {
        if (_sessionFactory == null)
        {
          _sessionFactory = Fluently.Configure()
          .Database(MsSqlConfiguration.MsSql2005
          .ConnectionString("Server=localhost;Integrated Security=True"))
          .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
          .ExposeConfiguration(config =>
          {
            SchemaExport schemaExport = new SchemaExport(config);
          })
          .BuildSessionFactory();
        }
        return _sessionFactory;
      }
    }
    public static ISession OpenSession()
    {
      return SessionFactory.OpenSession();
    }
  }
}
