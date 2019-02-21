using FluentNHibernate.Mapping;
using System.Collections.Generic;

namespace SQLServerLocalDBTest
{
    public class SchoolClass
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int TeacherId { get; set; }
    }

    public class SchoolClassMap : ClassMap<SchoolClass>
    {
      public SchoolClassMap()
        {
            Id(u => u.Id);
            Map(u => u.Name).Nullable();
            Map(u => u.TeacherId).Not.Nullable();
            Table("facultydata..class");
        }
    }
}
