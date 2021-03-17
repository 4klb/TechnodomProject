using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using TechnodomProject.Models;

namespace TechnodomProject.Data
{
    public class PurchaseDataAccess : DbDataAccess<Purchase>
    {
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

        public void UpdateGoodsInPurchases(Goods goods )
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
