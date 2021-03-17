using System;
using System.Collections.Generic;
using TechnodomProject.Data;
using TechnodomProject.Models;
using TechnodomProject.UI;

namespace TechnodomProject.Services
{
    public class KeyboardService
    {
        public ConsoleKeyInfo CatchKey { get; set; }

        public int Key { get; set; }
        public Guid ProductChoice { get; set; }

        public List<Guid> List { get; set; }

        public List<Guid> BasketList { get; set; }
        public int KeyboardListen()
        {
            string status = string.Empty;

            int key;
            var webpage = new Webpage();

            do
            {
                CatchKey = Console.ReadKey();

                Move(CatchKey.Key);

                status = CatchKey.KeyChar.ToString();

                int.TryParse(status, out key);

                Key = key;


            } while (CatchKey.Key != ConsoleKey.Backspace);

            return Key;
        }

        public void Move(ConsoleKey keyDirection)
        {
            var goodsData = new ItemsDataAccess();
            var webpage = new Webpage();
            var keyboardService = new KeyboardService();
            var basket = new Basket();

            var cursorPosition = Console.CursorTop;

            switch (keyDirection)
            {
                case ConsoleKey.UpArrow:
                    if (Console.CursorTop > 0)
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop - 1);
                    }
                    else if (Console.CursorTop == 0)
                    {
                        Console.SetCursorPosition(0, 0);
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (Console.CursorTop < Console.BufferHeight)
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop + 1);
                    }
                    break;
                case ConsoleKey.Spacebar:
                    List = webpage.DrawPageGoods(Key);
                    break;
                case ConsoleKey.Enter:
                    Console.Clear();
                    for (int i = 0; i <= List.Count; i++)
                    {
                        if (cursorPosition == i)
                        {
                            var products = goodsData.SelectItem(List[i - 1]); // Id

                            ProductChoice = List[i - 1];

                            foreach (var product in products)
                            {
                                product.Category = goodsData.GetCategory(product);
                                product.Manufacturer = goodsData.GetManufacturer(product);
                                Console.WriteLine($" Наименование товара: {product.Name}");
                                Console.WriteLine($" Цена: {product.Price}");
                                Console.WriteLine($" Дата публикации: {product.Publicitydate}");
                                Console.WriteLine($" Категория: {product.Category.Name}");
                                Console.WriteLine($" Производитель: {product.Manufacturer.Name}");
                                Console.WriteLine($" Страна производитель: {product.Manufacturer.Country}");
                                Console.WriteLine($" Рейтинг: {product.Raiting}");
                                Console.WriteLine($" \n В корзину (нажмите Tab): ");
                                webpage.Page();
                            }
                        }
                    }
                    break;
                case ConsoleKey.Tab:           //добавление в корзину                  
                    BasketList = basket.Add(ProductChoice);
                    break;
                case ConsoleKey.Escape:        //вызов набора страниц
                    webpage.Page();
                    break;
                case ConsoleKey.RightArrow:
                    Console.SetCursorPosition(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    Console.SetCursorPosition(0, 1);
                    break;
                case ConsoleKey.B:
                    webpage.DrawBasket(BasketList); //вызов корзины
                    break;
                case ConsoleKey.Q:
                    webpage.MakePurchase(basket); //покупка
                    break;
                case ConsoleKey.Delete:
                    //basket.Delete(goodsData.SelectById(ProductChoice)); //удаление
                    break;
                default:
                    break;
            }
        }
    }
}
