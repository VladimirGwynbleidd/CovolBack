using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace SYE.Covol.PI
{
    public class SqlHelperConnection
    {
        public ILogger Logger { get; }

        public SqlHelperConnection(ILogger logger = null)
        {
            Logger = logger;
        }
        private static string Password_string { get => Environment.GetEnvironmentVariable("COVOL_PASSWORD_SQL").ToString(); }

        private static SecureString Password_secureString
        {
            get
            {
                SecureString passwordSql = new SecureString();
                Password_string.ToCharArray().ToList().ForEach(p => passwordSql.AppendChar(p));
                passwordSql.MakeReadOnly();

                return passwordSql;
            }
        }

        private static SqlCredential credential_sql
        {
            get
            {
                return new SqlCredential(Environment.GetEnvironmentVariable("COVOL_USER_SQL"), Password_secureString);
            }
        }

        private static string connection_string
        {
            get
            {
                return $"Server={Environment.GetEnvironmentVariable("COVOL_DATASOURCE_SQL")};" +
                    $"Database={Environment.GetEnvironmentVariable("COVOL_DB_SQL")};" +
                    $"Connection Lifetime=7200;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;";
            }
        }

        public async Task<SqlConnection> connectionAsync()
        {
            SqlConnection conn = new SqlConnection(connection_string, credential_sql);
            Logger?.LogInformation("Abriendo conexión de base de datos en modo asincrono");
            await conn.OpenAsync();
            return conn;
        }

        public SqlConnection connection()
        {
            SqlConnection conn = new SqlConnection(connection_string, credential_sql);
            Logger?.LogInformation("Abriendo conexión de base de datos en modo sincrono");
            conn.Open();
            return conn;
        }
    }
}
