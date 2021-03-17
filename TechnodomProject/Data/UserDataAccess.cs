using System;
using System.Data.Common;
using TechnodomProject.Models;

namespace TechnodomProject.Data
{
    public class UserDataAccess : DbDataAccess<User>
    {
        public override void Insert(User user)
        {
            string insertSqlScript = "insert into Users values (@Id,@FullName, @Phone,@Email)";

            using (var transaction = connection.BeginTransaction())
            {
                using (var command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = insertSqlScript;

                    try
                    {
                        command.Transaction = transaction;

                        var idParameter = factory.CreateParameter();
                        idParameter.DbType = System.Data.DbType.Guid;
                        idParameter.Value = user.Id;
                        idParameter.ParameterName = "Id";
                        command.Parameters.Add(idParameter);

                        var fullNameParameter = factory.CreateParameter();
                        fullNameParameter.DbType = System.Data.DbType.String;
                        fullNameParameter.Value = user.FullName;
                        fullNameParameter.ParameterName = "FullName";
                        command.Parameters.Add(fullNameParameter);

                        var phoneParameter = factory.CreateParameter();
                        phoneParameter.DbType = System.Data.DbType.String;
                        phoneParameter.Value = user.Phone;
                        phoneParameter.ParameterName = "Phone";
                        command.Parameters.Add(phoneParameter);

                        var emailParameter = factory.CreateParameter();
                        emailParameter.DbType = System.Data.DbType.String;
                        emailParameter.Value = user.Email;
                        emailParameter.ParameterName = "Email";
                        command.Parameters.Add(emailParameter);

                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (DbException)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        public bool IsPhoneExists(string phone)
        {
            string selectSqlScript = "SELECT Phone FROM Users";

            using (var command = factory.CreateCommand())
            {
                command.CommandText = selectSqlScript;
                command.Connection = connection;

                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        if (phone == dataReader["Phone"].ToString())
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

        }

        public User SelectByPhone(string phone)
        {
            var user = new User();
            string selectSqlScript = $"SELECT * FROM Users Where Phone = '{phone}'";

            using (var command = factory.CreateCommand())
            {
                command.CommandText = selectSqlScript;
                command.Connection = connection;

                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        user = new User
                        {
                            Id = Guid.Parse(dataReader["Id"].ToString()),
                            Phone = dataReader["Phone"].ToString(),
                            FullName = dataReader["FullName"].ToString()

                        };
                    }
                }

                return user;
            }
        }
    }
}
