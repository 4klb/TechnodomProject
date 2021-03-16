using System;
using System.Collections.Generic;
using System.Text;
using TechnodomProject.Services;

namespace TechnodomProject.UI
{
    public class Webpage
    {
        public void DrawPageItems(List<string> items, int currentPage, int itemsCount)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(2, 12);

            Console.SetCursorPosition(2, 0);
            Console.WriteLine("Товары");

            foreach (var item in items)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"  Наименование товара: {item}");
            }

            Console.SetCursorPosition(1, 13);
            Console.WriteLine($" Текущая страница {currentPage}");

            Console.SetCursorPosition(32, 16);
            Console.WriteLine($" Всего {itemsCount} продуктов");

            Console.SetCursorPosition(1, 1);
        }

        public void Menu()
        {
            var keyboardService = new KeyboardService();
            var goodsData = new ItemDataAccess();
            Console.SetCursorPosition(50, 0);
            Console.WriteLine("Стартовая страница");
            Console.SetCursorPosition(1, 1);
            Console.WriteLine("Популярные товары");
            //
            keyboardService.KeyboardListen();
            Console.Clear();
        }

        public void DrawPurchase(string productChoice) //Tab
        {
            Console.Clear();
            Console.SetCursorPosition(50, 0);
            Console.WriteLine("Страница покупки товара");
            Console.SetCursorPosition(2, 4);
            Console.WriteLine($"Ваш выбор {productChoice}");
            Console.WriteLine();
            Console.WriteLine("Чтобы вернуться к выбору страниц нажмите LeftArrow/RightArrow(<-/->)");
            Console.SetCursorPosition(2, 10);
            Console.WriteLine("Нажмите любую клавишу чтобы перейти к Qiwi-касса");
            Console.ReadKey();
            //оплата, процесс уменьшения товара со склада 
        }
    }
}
