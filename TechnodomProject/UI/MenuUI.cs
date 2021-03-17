using System;

namespace TechnodomProject.UI
{
    public class MenuUI
    {
        public string Print(string[] items)
        {
            string text = ("\n\n\n\t\t-----------Меню--------------\n");
            for (int i = 0; i < items.Length; i++)
            {
                text += ($"\t\t\t{i + 1}. {items[i]}\n");
            }

            text += ("\n\t -->");
            return text;
        }

        public void Start()
        {
            string[] items = { "Войти", "Зарегистрироваться" };

            string choice;
            do
            {
                Console.Clear();
                Console.WriteLine(Print(items));
                choice = Console.ReadLine();
                var userSignInUp = new UserSignInUp();
                switch (choice)
                {
                    case "1":
                        {
                            userSignInUp.Registration();
                        }
                        break;
                    case "2":
                        {
                            userSignInUp.Registration();
                        }
                        break;

                }
                Console.ReadKey();
            } while (choice != "3");
        }
    }
}
