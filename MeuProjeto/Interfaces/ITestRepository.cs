using MyProject.Models;

namespace MyProject.Interfaces
{
    public interface ITestRepository
    {
        DateTime ToSafeDbDateDBnull(DateTime dt);
        string sqltexto(string texto);


        Task ApagaTest(string test);
        Task<List<TestModel>> GetTest(string campos, string filtros, string ordem);
        Task InsertTest(TestModel test);
        Task UpdateTest(TestModel test);
    }
}
