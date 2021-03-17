using System;
using System.Collections.Generic;
using TechnodomProject.Data;
using TechnodomProject.Models;
using TechnodomProject.Services;

namespace TechnodomProject.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            InitConfiguration();

            var goods = new List<Goods>{
                new Goods
                {
                    Id = new Guid("61F8ACEE-DF79-434B-8E4C-6D586838F6BE")
                }
            };

            using (var purchaseDataAccess = new PurchaseDataAccess())
            {
            
                purchaseDataAccess.UpdateGoodsInPurchases(goods[0]);
            }

            //var menuUi = new MenuUI();
            //menuUi.Start();


        }

        private static void InitConfiguration()
        {
            ConfigurationService.Init();
        }
    }
}
