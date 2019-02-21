using FluentNHibernate.Mapping;
using System.Collections.Generic;

namespace SQLServerLocalDBTest
{
    public class Student
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int classid {get;set;}
    }

    public class StudentMap : ClassMap<Student>
    {
        public StudentMap()
        {
            Id(u => u.Id);
            Map(u => u.Name).Nullable();
            Map(u => u.classid).Not.Nullable();
            Table("studentdata..student");
        }
    }
}
