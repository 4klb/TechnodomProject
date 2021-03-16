using System;
using System.Collections.Generic;
using System.Text;

namespace TechnodomProject.UI
{
    public class Webpage
    {
        public List<int> ListId { get; set; }

        public List<int> DrawPageGoods(int key)
        {
            var goodsData = new GoodsDataAccess();

            var keyboard = new Keyboard
            {
                PagesArray = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 } //забирать общее кол во с бд
            };

            ListId = new List<int>();

            for (int i = 0; i < keyboard.PagesArray.Length; i++)
            {
                if (key == keyboard.PagesArray[i])
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(2, 12);
                    Console.WriteLine($"Текущая страница - {keyboard.PagesArray[i]} из {keyboard.PagesArray.Length}");

                    var products = goodsData.SelectGoods(key);

                    Console.SetCursorPosition(2, 0);
                    Console.WriteLine("Товары");


                    foreach (var product in products)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"  Наименование товара: {product.Name}");
                    }

                    TopCursorPosition();

                    foreach (var id in products)
                    {
                        var value = id.Id;
                        ListId.Add(value);
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            return ListId;
        }

        public void Menu()
        {
            KeyboardService keyboardService = new KeyboardService();
            var goodsData = new GoodsDataAccess();
            Console.SetCursorPosition(50, 0);
            Console.WriteLine("Стартовая страница");
            Console.SetCursorPosition(1, 1);
            Console.WriteLine("Популярные товары");
            Page();
            keyboardService.KeyboardListen();
            Console.Clear();
        }

        public void Page()
        {
            //Console.Clear();
            Console.SetCursorPosition(3, 18);
            Console.WriteLine("Выберите страницу 1...9"); // определить общее количество стр с бд.
            Console.SetCursorPosition(0, 20);
        }

        public void TopCursorPosition()
        {
            Console.SetCursorPosition(0, 1);
        }

        public ICollection<Comment> DrowComments() //Shift
        {
            return null;
        }

        public void DrowBuy(int productChoice) //Tab
        {
            Console.Clear();
            Console.SetCursorPosition(50, 0);
            Console.WriteLine("Страница покупки товара");
            Console.SetCursorPosition(2, 4);
            Console.WriteLine($"Ваш выбор {productChoice}");
            Console.WriteLine("Чтобы вернуться к выбору страниц нажмите Escape");
            Console.SetCursorPosition(2, 6);
            Console.WriteLine("Нажмите любую клавишу чтобы перейти к Qiwi-касса");
            Console.ReadKey();
            //оплата, процесс уменьшения товара со склада 
        }
    }
}
