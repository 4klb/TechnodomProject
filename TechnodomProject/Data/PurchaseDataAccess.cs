using System.Data.Common;
using TechnodomProject.Models;

namespace TechnodomProject.Data
{
    public class PurchaseDataAccess : DbDataAccess<Purchase>
    {
        /// <summary>
        /// метод добавления данных в БД
        /// </summary>
        /// <param name="purchase"></param>
        public override void Insert(Purchase purchase)
        {
            string insertSqlScript = "insert into Purchase values (@Id,@Sum, @Date,@UserId)";

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
                        idParameter.Value = purchase.Id;
                        idParameter.ParameterName = "Id";
                        command.Parameters.Add(idParameter);

                        var sumParameter = factory.CreateParameter();
                        sumParameter.DbType = System.Data.DbType.Int32;
                        sumParameter.Value = purchase.Sum;
                        sumParameter.ParameterName = "Sum";
                        command.Parameters.Add(sumParameter);

                        var dateParameter = factory.CreateParameter();
                        dateParameter.DbType = System.Data.DbType.DateTime;
                        dateParameter.Value = purchase.Date;
                        dateParameter.ParameterName = "Date";
                        command.Parameters.Add(dateParameter);

                        var userIdParameter = factory.CreateParameter();
                        userIdParameter.DbType = System.Data.DbType.Guid;
                        userIdParameter.Value = purchase.UserId;
                        userIdParameter.ParameterName = "UserId";
                        command.Parameters.Add(userIdParameter);

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


        /// <summary>
        /// метод который уменьшаем количество купленного товара в БД
        /// </summary>
        /// <param name="goods"></param>
        
        public void UpdateGoodsAmount(Goods goods ) 
        {
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = "update AmountGoods set Amount = Amount-1 where goodsId = @Id";

                var idParameter = factory.CreateParameter();
                idParameter.DbType = System.Data.DbType.Guid;
                idParameter.Value = goods.Id;
                idParameter.ParameterName = "Id";
                command.Parameters.Add(idParameter);

                command.ExecuteNonQuery();
            }
        }
    }
}
