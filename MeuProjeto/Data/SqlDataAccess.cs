using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace MyProject.Data
{
    public class SqlDataAccess : ISqlDataAccess
    {
        public string ConnectionStringName { get; set; } = "DefaultConnection";
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        //public SqlDataAccess()
        //{
        //}


        public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
        {
            string? connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql, parameters);
                return data.ToList();
            }
        }

        public async Task SaveData<T>(string sql, T parameters)
        {
            string? connectionString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.ExecuteAsync(sql, parameters);
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
            }
        }

        public DataTable Sql(string sql)
        {
            string? connectionString = _config.GetConnectionString(ConnectionStringName);
            
            SqlConnection conn = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable datatable = new DataTable();
            adapter.Fill(datatable);
            return datatable;
        }

        public async Task SqlGrava(string sql)
        {
            string? connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql);
            }
        }

        public async Task SqlGravaDados(string sql, string bdados)
        {
            string? connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql);
            }
        }
    }
}
