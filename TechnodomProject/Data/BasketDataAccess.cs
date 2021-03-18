
using System.Data.Common;
using TechnodomProject.Models;

namespace TechnodomProject.Data
{
    public class BasketDataAccess : DbDataAccess<Basket>
    {
        public override void Insert(Basket basket)
        {
            string insertSqlScript = "insert into Basket values (@Id,@PurchaseId, @GoodsId)";

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
                        idParameter.Value = basket.Id;
                        idParameter.ParameterName = "Id";
                        command.Parameters.Add(idParameter);

                        var PurchaseIdParameter = factory.CreateParameter();
                        PurchaseIdParameter.DbType = System.Data.DbType.Guid;
                        PurchaseIdParameter.Value = basket.PurchaseId;
                        PurchaseIdParameter.ParameterName = "PurchaseId";
                        command.Parameters.Add(PurchaseIdParameter);

                        var GoodsIdParameter = factory.CreateParameter();
                        GoodsIdParameter.DbType = System.Data.DbType.Guid;
                        GoodsIdParameter.Value = basket.GoodsId;
                        GoodsIdParameter.ParameterName = "GoodsId";
                        command.Parameters.Add(GoodsIdParameter);

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
    }
}
