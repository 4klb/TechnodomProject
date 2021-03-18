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

        public Category GetCategory(Goods goods)
        {
            string selectSqlScript = $"SELECT * FROM Category where Id='{goods.CategoryId}'";

            var category = new Category();

            using (var command = factory.CreateCommand())
            {
                command.CommandText = selectSqlScript;
                command.Connection = connection;

                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        category = new Category
                        {
                            Id = Guid.Parse(dataReader["Id"].ToString()),
                            Name = dataReader["Name"].ToString()

                        };
                    }
                }

                return category;
            }
        }


        public Manufacturer GetManufacturer(Goods goods)
        {
            string selectSqlScript = $"SELECT * FROM Manufacturer where Id='{goods.ManufacturerId}'";

            var manufacturer = new Manufacturer();

            using (var command = factory.CreateCommand())
            {
                command.CommandText = selectSqlScript;
                command.Connection = connection;

                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        manufacturer = new Manufacturer
                        {
                            Id = Guid.Parse(dataReader["Id"].ToString()),
                            Name = dataReader["Name"].ToString(),
                            Country = dataReader["Country"].ToString(),
                        };
                    }
                }

                return manufacturer;
            }
        }


        public int GetRaiting(Goods goods)
        {
            string selectSqlScript = $"select avg(r.Raiting) as raiting from goods g join raiting r on g.Id = r.GoodsId where r.GoodsId = '{goods.Id}' group by r.GoodsId ";

            int raiting = 0;

            using (var command = factory.CreateCommand())
            {
                command.CommandText = selectSqlScript;
                command.Connection = connection;

                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        raiting = int.Parse(dataReader["raiting"].ToString());
                    }
                }

                return raiting;
            }
        }



        public ICollection<Goods> SelectPopularGoodsId(int count)
        {
            string selectSqlScript = $"Select * from goods where id in (select top {count} r.GoodsId from Raiting r join goods g on g.Id = r.GoodsId group by r.GoodsId order by avg(r.raiting) desc)";

            var goods = new List<Goods>();

            using (var command = factory.CreateCommand())
            {
                command.CommandText = selectSqlScript;
                command.Connection = connection;

                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        goods.Add(new Goods
                        {
                            Id = Guid.Parse(dataReader["Id"].ToString()),
                            Name = dataReader["Name"].ToString(),
                            Price = int.Parse(dataReader["Price"].ToString()),
                            Publicitydate = DateTime.Parse(dataReader["Publicitydate"].ToString()),

                            CategoryId = Guid.Parse(dataReader["CategoryId"].ToString()),
                            ManufacturerId = Guid.Parse(dataReader["ManufacturerId"].ToString())
                        });
                    }
                }

                return goods;
            }
        }



        public ICollection<Goods> SelectGoods(int offsetCount, int fetchCount)
        {
            string selectSqlScript = $"Select * from goods order by Name OFFSET {offsetCount} rows FETCH NEXT {fetchCount} ROWS ONLY";

            var goods = new List<Goods>();

            using (var command = factory.CreateCommand())
            {
                command.CommandText = selectSqlScript;
                command.Connection = connection;

                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        goods.Add(new Goods
                        {
                            Id = Guid.Parse(dataReader["Id"].ToString()),
                            Name = dataReader["Name"].ToString(),
                            Price = int.Parse(dataReader["Price"].ToString()),
                            Publicitydate = DateTime.Parse(dataReader["Publicitydate"].ToString()),

                            CategoryId = Guid.Parse(dataReader["CategoryId"].ToString()),
                            ManufacturerId = Guid.Parse(dataReader["ManufacturerId"].ToString())
                        });
                    }
                }

                return goods;
            }
        }

        public int GetCountOfGoods()
        {
            string selectSqlScript = "Select count(id) as count from goods";

            int countOfGoods =0;

            using (var command = factory.CreateCommand())
            {
                command.CommandText = selectSqlScript;
                command.Connection = connection;

                using (var dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        countOfGoods = int.Parse(dataReader["count"].ToString());
                    }

                }
                return countOfGoods;
            }
        }
    }
}
