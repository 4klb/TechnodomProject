using System;
using System.Collections.Generic;
using System.Text;

namespace TechnodomProject.Data
{
    public class ItemsDataAccess : DbDataAccess<Item>
    {
        public override void Insert(Item entity) { } //!!

        public void Raiting()
        {

        }
        public ICollection<Item> SelectItems(int data)
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

                var products = new List<Item>();

                while (dataReader.Read())
                {
                    products.Add(new Item
                    {
                        Id = int.Parse(dataReader["Id"].ToString()),
                        Name = dataReader["Name"].ToString(),
                        Price = dataReader["Price"].ToString(),
                        Category = dataReader["Category"].ToString(),
                        Country = dataReader["Country"].ToString(),
                        Raiting = int.Parse(dataReader["Raiting"].ToString()),
                        Comments = new List<Comment>()
                        //{
                        //    Id = int.Parse(dataReader["Id"].ToString()),
                        //    Text = dataReader["Text"].ToString(),
                        //}
                    });
                }

                dataReader.Close();

                return products;
            }
        }

        public ICollection<Item> SelectItem(int Id)
        {
            string selectSqlScript = $"SELECT * FROM Items WHERE Id = {Id}";

            using (var command = factory.CreateCommand())
            {
                command.CommandText = selectSqlScript;
                command.Connection = connection;

                var products = new List<Item>();

                var dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    products.Add(new Item
                    {
                        Id = int.Parse(dataReader["Id"].ToString()),
                        Name = dataReader["Name"].ToString(),
                        Price = dataReader["Price"].ToString(),
                        Category = dataReader["Category"].ToString(),
                        Country = dataReader["Country"].ToString(),
                        Raiting = int.Parse(dataReader["Raiting"].ToString()),
                        Comments = new List<Comment>()
                    });
                }

                dataReader.Close();

                return products;
            }
        }
    }
}
