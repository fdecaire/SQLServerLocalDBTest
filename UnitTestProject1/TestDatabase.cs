using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public static class TestDatabase
    {
        public static void Create(string databaseName, string instanceConnection)
        {
            string databaseDirectory = Directory.GetCurrentDirectory();

            SqlConnection db;
            db = new SqlConnection("server=" + instanceConnection + ";" +
                                   "Trusted_Connection=yes;" +
                                   "database=master; " +
                                   "Integrated Security=true; " +
                                   "connection timeout=30");

            db.Open();

            SqlCommand myCommand = new SqlCommand(@"CREATE DATABASE [" + databaseName + @"]
              CONTAINMENT = NONE
              ON  PRIMARY 
              ( NAME = N'" + databaseName + @"', FILENAME = N'" + databaseDirectory + @"\" + databaseName +
                                                      @".mdf' , SIZE = 3072KB , FILEGROWTH = 1024KB )
              LOG ON 
              ( NAME = N'" + databaseName + @"_log', FILENAME = N'" + databaseDirectory + @"\" + databaseName +
                                                      @"_log.ldf' , SIZE = 1024KB , FILEGROWTH = 10%)
              ", db);

            myCommand.ExecuteNonQuery();
            db.Close();
        }

        public static void Remove(string databaseName, string instanceConnection)
        {
            SqlConnection db;
            db = new SqlConnection("server=" + instanceConnection + ";" +
                                   "Trusted_Connection=yes;" +
                                   "database=master; " +
                                   "Integrated Security=true; " +
                                   "connection timeout=30");

            db.Open();

            SqlCommand myCommand = new SqlCommand("DROP DATABASE [" + databaseName + "]", db);
            myCommand.ExecuteNonQuery();
            db.Close();
        }
    }
}
