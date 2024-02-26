using MyProject.Data;
using MyProject.Interfaces;
using MyProject.Models;
using System.Data.SqlTypes;

namespace MyProject.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly ISqlDataAccess _db;
        public TestRepository(ISqlDataAccess db)
        {
            _db = db;
        }
        public DateTime ToSafeDbDateDBnull(DateTime dt)
        {
            try
            {
                if (dt >= SqlDateTime.MinValue)
                {
                    return dt;
                }
                else
                {
                    return new DateTime(1900, 1, 1);
                }
            }
            catch (Exception)
            {

                return new DateTime(1900, 1, 1);
            }

        }
        public string sqltexto(string texto)
        {
            return texto.Replace("'", "''");
        }



        public Task<List<TestModel>> GetTest(string campos, string filtros, string ordem)
        {
            string sql = "";
            if (filtros == "")
            {
                sql = "select " + campos + " from TestTable"
                    + " order by " + ordem;
            }
            else
            {
                sql = "select " + campos + " from TestTable"
                    + " where " + filtros + " order by " + ordem;
            }

            return _db.LoadData<TestModel, dynamic>(sql, new { });
        }

        public Task ApagaTest(string test)
        {
            string sql = "delete from TestTable"
                       + " where Id='" + sqltexto(test.ToString()) + "'";

            return _db.SaveData(sql, "");
        }

        public Task InsertTest(TestModel test)
        {
            string sql = @"insert into TestTable (Name, Email)
                          values (@Name, @Email)";

            return _db.SaveData(sql, test);
        }

        public Task UpdateTest(TestModel test)
        {
            string sql = @"update TestTable set Name=@Name, Email=@Email
                          where id=@Id";

            return _db.SaveData(sql, test);
        }
    }
}
