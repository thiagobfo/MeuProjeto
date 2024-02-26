using Dapper;
using System.Data;

namespace MyProject.Data
{
    public interface ISqlDataAccess
    {
        string ConnectionStringName { get; set; }

        Task<List<T>> LoadData<T, U>(string sql, U parameters);
        Task SaveData<T>(string sql, T parameters);
        DataTable Sql(string sql);
        Task SqlGrava(string sql);
        Task SqlGravaDados(string sql, string bdados);
    }
}
