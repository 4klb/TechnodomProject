using TechnodomProject.Services;

namespace TechnodomProject.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            InitConfiguration();
            var menu = new MenuUI();
            menu.Start();
        }

        private static void InitConfiguration()
        {
            ConfigurationService.Init();
        }
    }
}
