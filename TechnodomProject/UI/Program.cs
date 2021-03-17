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
            var webpage = new Webpage();

            //тут регистрация 
            //webpage.StartPage();

            //var itemsDataAccess = new ItemsDataAccess();
            //var items = itemsDataAccess.SelectRaiting();
            //foreach (var item in items)
            //{
            //    Console.WriteLine(item);
            //}


        }

        private static void InitConfiguration()
        {
            ConfigurationService.Init();
        }
    }
}
