using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Model;
using System.Security.Principal;

namespace DatabaseBroker
{
    public class Broker
    {
        private SqlConnection connection;
        private SqlTransaction transaction;

        public Broker(IConfiguration configuration)
        {
            connection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        #region Connection
        public void OpenConnection()
        {
            connection.Open();
        }

        public void CloseConnection()
        {
            connection.Close();
        }
        #endregion

        #region Transaction
        public void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
        }

        public void Commit()
        {
            transaction?.Commit();
        }

        public void Rollback()
        {
            transaction?.Rollback();
        }
        #endregion

        #region Queries
        public async Task<List<IEntity>> Get(IEntity e)
        {
            List<IEntity> result;
            SqlCommand command = new SqlCommand("", connection, transaction);

            command.CommandText = $"SELECT {e.SelectValues} "
                                    + $"FROM {e.TableName} {e.TableAlias} "
                                    + $"{e.JoinTable} "
                                    + e.GetWhere();

            foreach (var item in e.Set)
                command.Parameters.AddWithValue(item.Key, item.Value ?? DBNull.Value);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            result = e.GetEntities(reader);
            reader.Close();
            return result;
        }

        public async Task<int> GetNewId(IEntity e)
        {
            SqlCommand command = new SqlCommand("", connection, transaction);

            command.CommandText = $"SELECT CAST(IDENT_CURRENT('{e.TableName}') AS INT) ";

            object result = await command.ExecuteScalarAsync();
            if (result is DBNull)
            {
                return 1;
            }
            else
            {
                return (int)result + 1;
            }
        }

        public async Task Save(IEntity e)
        {
            SqlCommand command = new SqlCommand("", connection, transaction);

            command.CommandText = $"INSERT INTO {e.TableName} " +
                                $"VALUES ({e.InsertValues})";

            foreach (var item in e.Set)
                command.Parameters.AddWithValue(item.Key, item.Value ?? DBNull.Value);

            if (await command.ExecuteNonQueryAsync() == 0)
            {
                throw new Exception("Greška sa bazom!");
            }
        }

        public async Task Update(IEntity e)
        {
            SqlCommand command = new SqlCommand("", connection, transaction);

            command.CommandText = $"UPDATE {e.TableName} "
                                   + e.GetSet()
                                   + $" WHERE {e.Where}";

            foreach (var item in e.Set)
                command.Parameters.AddWithValue(item.Key, item.Value ?? DBNull.Value);

            if (await command.ExecuteNonQueryAsync() == 0)
            {
                throw new Exception("Greška sa bazom!");
            }
        }

        public async Task Delete(IEntity e)
        {
            SqlCommand command = new SqlCommand("", connection, transaction);

            command.CommandText = $"DELETE FROM {e.TableName} "
                                + $"WHERE {e.Where}";

            foreach (var item in e.Set)
                command.Parameters.AddWithValue(item.Key, item.Value ?? DBNull.Value);

            if (await command.ExecuteNonQueryAsync() == 0)
            {
                throw new Exception("Greška sa bazom!");
            }
        }
        #endregion
    }
}
