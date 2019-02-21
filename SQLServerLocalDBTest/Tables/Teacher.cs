using FluentNHibernate.Mapping;
using System.Collections.Generic;

namespace SQLServerLocalDBTest
{
    public class Teacher
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }

    public class TeacherMap : ClassMap<Teacher>
    {
        public TeacherMap()
        {
            Id(u => u.Id);
            Map(u => u.Name).Nullable();
            Table("facultydata..teacher");
        }
    }
}
