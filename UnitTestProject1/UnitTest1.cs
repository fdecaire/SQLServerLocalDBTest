using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using System.Data.SqlClient;
using NHibernate.Linq;
using SQLServerLocalDBTest;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Initialize()
        {
            // make sure any previous instances are shut down
            ProcessStartInfo startInfo = new ProcessStartInfo
              {
                  WindowStyle = ProcessWindowStyle.Hidden,
                  FileName = "cmd.exe",
                  Arguments = "/c sqllocaldb stop \"testinstance\""
              };

            Process process = new Process { StartInfo = startInfo };
            process.Start();
            process.WaitForExit();

            // delete any previous instance
            startInfo.Arguments = "/c sqllocaldb delete \"testinstance\"";
            process.Start();
            process.WaitForExit();

            // create a new localdb sql server instance
            startInfo = new ProcessStartInfo
              {
                  WindowStyle = ProcessWindowStyle.Hidden,
                  FileName = "cmd.exe",
                  Arguments = "/c sqllocaldb create \"testinstance\" -s"
              };

            process = new Process { StartInfo = startInfo };
            process.Start();
            process.WaitForExit();

            TestDatabase.Create("facultydata", "(localdb)\\testinstance");
            TestDatabase.Create("studentdata", "(localdb)\\testinstance");
        }

        [TestCleanup]
        public void Cleanup()
        {
            // remove the databases
            TestDatabase.Remove("facultydata", "(localdb)\\testinstance");
            TestDatabase.Remove("studentdata", "(localdb)\\testinstance");

            // shut down the instance
            ProcessStartInfo startInfo = new ProcessStartInfo
              {
                  WindowStyle = ProcessWindowStyle.Hidden,
                  FileName = "cmd.exe",
                  Arguments = "/c sqllocaldb stop \"testinstance\""
              };

            Process process = new Process { StartInfo = startInfo };
            process.Start();
            process.WaitForExit();

            // delete the instance
            startInfo.Arguments = "/c sqllocaldb delete \"testinstance\"";
            process.Start();
            process.WaitForExit();
        }

        [TestMethod]
        public void TestMethod1()
        {
            using (ISession db = TestSessionFactory.OpenSession())
            {
                Teacher teacher = new Teacher
                  {
                      Name = "test teacher"
                  };

                db.Save(teacher);

                SchoolClass schoolClass = new SchoolClass
                  {
                      Name = "MTH 101",
                      TeacherId = 1
                  };
                db.Save(schoolClass);

                Student student = new Student
                  {
                      Name = "Joe",
                      classid = 1
                  };
                db.Save(student);

                var query = (from t in db.Query<Teacher>()
                             join c in db.Query<SchoolClass>() on t.Id equals c.TeacherId
                             join s in db.Query<Student>() on c.Id equals s.classid
                             select new
                             {
                                 TeacherName = t.Name,
                                 ClassName = c.Name,
                                 StudentName = s.Name
                             }).ToList();


                Assert.AreEqual(1, query.Count);
            }
        }

        [TestMethod]
        public void AnotherTest()
        {
            using (ISession db = TestSessionFactory.OpenSession())
            {
                var query = (from t in db.Query<Teacher>() select t).ToList();

                Assert.AreEqual(0, query.Count);
            }
        }

        [TestMethod]
        public void ThirdTest()
        {
            using (ISession db = TestSessionFactory.OpenSession())
            {
                Teacher teacher = new Teacher
                {
                    Name = "test teacher"
                };

                db.Save(teacher);

                teacher = new Teacher
                {
                    Name = "test teacher2"
                };

                db.Save(teacher);

                var query = (from t in db.Query<Teacher>() select t).ToList();

                Assert.AreEqual(2, query.Count);
            }
        }

    }
}
