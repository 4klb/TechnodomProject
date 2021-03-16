using System;
using TechnodomProject.Services;

namespace TechnodomProject.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            InitConfiguration();

            Webpage webpage = new Webpage();
            webpage.Menu();
        }

        private static void InitConfiguration()
        {
            ConfigurationService.Init();
        }
    }
}
