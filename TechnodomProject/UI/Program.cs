using System;
using TechnodomProject.Services;

namespace TechnodomProject.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            InitConfiguration();

            var menuUi = new MenuUI();
            menuUi.Start();


        }

        private static void InitConfiguration()
        {
            ConfigurationService.Init();
        }
    }
}
