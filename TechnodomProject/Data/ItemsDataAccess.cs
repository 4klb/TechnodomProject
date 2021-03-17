using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using TechnodomProject.Models;

namespace TechnodomProject.Data
{
    public class ItemsDataAccess : DbDataAccess<Goods>
    {
        public override void Insert(Goods entity) {}

        public void SetRaiting(Guid id, int mark)
        {
            string insertMark = $"INSERT INTO Raiting VALUES({id},{mark})"; 

            using (var transaction = connection.BeginTransaction())
            {
                using (var command = factory.CreateCommand())
                {
                    command.CommandText = insertMark;
                    command.Connection = connection;
                    try
                    {
                        command.Transaction = transaction;

                        var idParameter = factory.CreateParameter();
                        idParameter.DbType = System.Data.DbType.Guid;
                        idParameter.Value = id;
                        idParameter.ParameterName = "Id";

                        command.Parameters.Add(idParameter);

                        var markParameter = factory.CreateParameter();
                        idParameter.DbType = System.Data.DbType.String;
                        idParameter.Value = mark;
                        idParameter.ParameterName = "Raiting";

                        command.Parameters.Add(idParameter);

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
        
        public ICollection<Goods> SelectRaiting()
        {
            string selectSqlScript = $"SELECT gs.Name FROM Raiting ra join Goods gs on ra.GoodsId = gs.Id order by gs.Name";

            using (var command = factory.CreateCommand())
            {
                command.CommandText = selectSqlScript;
                command.Connection = connection;

                var dataReader = command.ExecuteReader();

                var products = new List<Goods>();

                while (dataReader.Read())
                {
                    products.Add(new Goods
                    {
                        Id = Guid.Parse(dataReader["Id"].ToString()),
                        Name = dataReader["Name"].ToString(),
                        Price = int.Parse(dataReader["Price"].ToString()),
                        Publicitydate = DateTime.Parse(dataReader["Publicitydate"].ToString()),
                        CategoryId = Guid.Parse(dataReader["CategoryId"].ToString()),
                        ManufacturerId = Guid.Parse(dataReader["ManufacturerId"].ToString())
                    });
                }

                dataReader.Close();

                return products;
            }

        }
        public ICollection<Goods> SelectItems(int data)
        {
            string selectSqlScript = string.Empty;

            if (data == 1) // change
            {
                selectSqlScript = $"SELECT * FROM GOODS ORDER BY Id OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY";
            }
            else if (data >= 2 && data <= 9)
            {
                selectSqlScript = $"SELECT * FROM GOODS ORDER BY Id OFFSET {data}0 - 10 Rows FETCH NEXT 10 ROWS ONLY";
            }

            using (var command = factory.CreateCommand())
            {
                command.CommandText = selectSqlScript;
                command.Connection = connection;

                var dataReader = command.ExecuteReader();

                var products = new List<Goods>();

                while (dataReader.Read())
                {
                    products.Add(new Goods
                    {
                        Id = Guid.Parse(dataReader["Id"].ToString()),
                        Name = dataReader["Name"].ToString(),
                        Price = int.Parse(dataReader["Price"].ToString()),
                        Publicitydate = DateTime.Parse(dataReader["Publicitydate"].ToString()),
                        CategoryId = Guid.Parse(dataReader["CategoryId"].ToString()),
                        ManufacturerId = Guid.Parse(dataReader["ManufacturerId"].ToString())
                    }) ;
                }

                dataReader.Close();

                return products;
            }
        }

        public ICollection<Goods> SelectItem(string name)
        {
            string selectSqlScript = $"SELECT * FROM Items WHERE NAME = {name}";

            using (var command = factory.CreateCommand())
            {
                command.CommandText = selectSqlScript;
                command.Connection = connection;

                var products = new List<Goods>();

                var dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    products.Add(new Goods
                    {
                        Id = Guid.Parse(dataReader["Id"].ToString()),
                        Name = dataReader["Name"].ToString(),
                        Price = int.Parse(dataReader["Price"].ToString()),
                        Publicitydate = DateTime.Parse(dataReader["Publicitydate"].ToString()),

                        CategoryId = Guid.Parse(dataReader["CategoryId"].ToString()),
                        ManufacturerId = Guid.Parse(dataReader["ManufacturerId"].ToString())
                    });
                }

                dataReader.Close();

                return products;
            }
        }

        public void DeleteItem(Guid productId)
        {
            var deleteItem = "DELETE Products WHERE Id = @id";

            using(var command = factory.CreateCommand())
            {
                command.CommandText = deleteItem;
                command.Connection = connection;

                var idParameter = factory.CreateParameter();
                idParameter.DbType = System.Data.DbType.Guid;
                idParameter.Value = productId;
                idParameter.ParameterName = "Id";
                command.Parameters.Add(idParameter);

                command.ExecuteNonQuery();
            }
        }
    }
}
