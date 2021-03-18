using System;
using TechnodomProject.Models;

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
                Console.Write(Print(items));
                var user = new User();
                choice = Console.ReadLine();
                var userSignInUp = new UserSignInUp();
                bool result;
                var goodsUi = new GoodsUI();
                switch (choice)
                {
                    case "1":
                        {
                            result = userSignInUp.Registration();
                            user = userSignInUp.User;
                            if (result == true)
                            {
                                goodsUi.ShowPopularGoods(user);
                            }
                        }
                        break;
                    case "2":
                        {
                            result = userSignInUp.Registration();
                            user = userSignInUp.User;
                            if (result == true)
                            {
                                goodsUi.ShowPopularGoods(user);
                            }
                        }
                        break;
                }
                Console.ReadKey();
            } while (choice != "3");
        }
    }
}
