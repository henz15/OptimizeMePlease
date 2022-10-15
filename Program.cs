using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.IO;
using BenchmarkDotNet.Running;

namespace OptimizeMePlease
{

    /// <summary>
    /// Steps: 
    /// 
    /// 1. Create a database with name "OptimizeMePlease"
    /// 2. Run application Debug/Release mode for the first time. IWillPopulateData method will get the script and populate
    /// created db.
    /// 3. Comment or delete IWillPopulateData() call from Main method. 
    /// 4. Go to BenchmarkService.cs class
    /// 5. Start coding within GetAuthors_Optimized method
    /// GOOD LUCK! :D 
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            //Debugging 
            //BenchmarkService benchmarkService = new BenchmarkService();
            //benchmarkService.GetAuthors_Optimized();

            //Comment me after first execution, please.
            //IWillPopulateData();

            BenchmarkRunner.Run<BenchmarkService>();
        }

        public static void IWillPopulateData()
        {
            //create OptimizeMePlease db
            RunRawSQL("master", "createdb");
            //run query in OptimizeMePlease db
            RunRawSQL("OptimizeMePlease", "script");
        }

        private static void RunRawSQL(string databaseName, string fileName)
        {
            var sqlConnectionString =
                $"Data Source=localhost;Initial Catalog={databaseName};User ID=sa;Password=Admin2013+";

            var workingDirectory = Environment.CurrentDirectory;
            var directoryInfo = Directory.GetParent(workingDirectory).Parent.Parent;
            if (directoryInfo != null)
            {
                var path = Path.Combine(directoryInfo.FullName, $"{fileName}.sql");
                var script = File.ReadAllText(path);

                var conn = new SqlConnection(sqlConnectionString);
                var server = new Server(new ServerConnection(conn));

                server.ConnectionContext.ExecuteNonQuery(script);
            }
        }
    }
}
