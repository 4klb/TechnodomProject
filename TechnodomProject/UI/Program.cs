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

            //регистрация 
            webpage.StartPage();
        }

        private static void InitConfiguration()
        {
            ConfigurationService.Init();
        }
    }
}
