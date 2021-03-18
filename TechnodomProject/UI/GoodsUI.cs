using System;
using System.Collections.Generic;
using System.Linq;
using TechnodomProject.Data;
using TechnodomProject.Enum;
using TechnodomProject.Models;
using TechnodomProject.Services;

namespace TechnodomProject.UI
{
    public class GoodsUI
    {
        public void PrintGoods(List<Goods> goods)
        {
            int i = 1;
            foreach(var good in goods)
            {
                Console.WriteLine($"{i++}. {good.Category.Name} {good.Manufacturer.Name} {good.Name}");
                Console.WriteLine($"\tНаименование: {good.Name}\n\tЦена: {good.Price} kzt\n\tДата публикации: {good.Publicitydate}");
                Console.WriteLine($"\tПроизводитель: {good.Manufacturer.Name} {good.Manufacturer.Country}\n");
            }
        }


        public void ShowGoods(Goods goods, int index, bool variant) // выводит выбранные товар
        {
            Console.Clear();
            Console.WriteLine($"\n{index}.{goods.Category.Name} {goods.Manufacturer.Name} {goods.Name}");
            Console.WriteLine($"\tНаименование: {goods.Name}\n\tЦена: {goods.Price} kzt\n\tДата публикации: {goods.Publicitydate}\n\tКатегория: {goods.Category.Name}");
            Console.WriteLine($"\tПроизводитель: {goods.Manufacturer.Name}\n\tСтрана производства: {goods.Manufacturer.Country}\n");

            if(variant == true)
            {
                Console.WriteLine("\tДля добавления в корзине нажмите Tab");
            }
        }



        public void ShowPopularGoods(User user)
        {
            List<Goods> products = new List<Goods>();
            int count = 4;

            using (var itemsDataAccess = new ItemsDataAccess())
            {
                products = itemsDataAccess.SelectPopularGoodsId(count).ToList();
                foreach(var product in products)
                {
                    product.Category = itemsDataAccess.GetCategory(product);
                    product.Manufacturer = itemsDataAccess.GetManufacturer(product);
                }
            }

            do
            {
                Console.Clear();
                Console.WriteLine("\t\tПолулярные товары:\n");
                PrintGoods(products);

                Console.WriteLine("Для просмотра всех товаров введите 0");
                Console.Write("Введите номер товара который хотите посмотреть -> ");
                int inputkey = int.Parse(Console.ReadLine());
                if(inputkey <0 || inputkey > count)
                {
                    inputkey = 1;
                }else if (inputkey == 0)
                {
                    Start(user);
                }
                ShowGoods(products[inputkey - 1], inputkey,false);
                Console.ReadKey();
            } while (true);
        }



        public void Start(User user)
        {
            var productsForPay = new List<Goods>();

            int countOfGoods = 0;
            using(var itemsDataAccess = new ItemsDataAccess())
            {
                countOfGoods = itemsDataAccess.GetCountOfGoods();
            }

            var products = new List<Goods>();
            int offsetCount = 0;
            int fetchCount = 4;

            var purchase = new Purchase();
            var basket = new Basket();
            var qiwiService = new QiwiService();

            do
            {
                Console.Clear();
                using (var itemsDataAccess = new ItemsDataAccess())
                {
                    products = itemsDataAccess.SelectGoods(offsetCount, fetchCount).ToList();

                    foreach (var product in products)
                    {
                        product.Category = itemsDataAccess.GetCategory(product);
                        product.Manufacturer = itemsDataAccess.GetManufacturer(product);
                    }
                }
                Console.WriteLine("Для перемещения по странице нажмите на стрелочки <-назад | вперед->\t Для просмотра корзины нажмите на B");
                Console.WriteLine("\t\tOnlineShop\n");
                PrintGoods(products);
                Console.Write("Введите номер товара который хотите посмотреть -> ");

                var input = Console.ReadKey(true).Key;

                switch (input)
                {
                    case ConsoleKey.NumPad1:                        
                        ShowGoods(products[0], 1,true);
                        var keyInput = Console.ReadKey(true).Key;
                        switch (keyInput)
                        {
                            case ConsoleKey.Tab: //добавить в корзину
                                productsForPay.Add(products[0]);
                                break;
                        }
                        break;
                    case ConsoleKey.NumPad2:
                        ShowGoods(products[1], 2,true);
                        keyInput = Console.ReadKey(true).Key;
                        switch (keyInput)
                        {
                            case ConsoleKey.Tab: //добавить в корзину
                                productsForPay.Add(products[0]);
                                break;
                        }
                        break;
                    case ConsoleKey.NumPad3:
                        ShowGoods(products[2], 3,true);
                        keyInput = Console.ReadKey(true).Key;
                        switch (keyInput)
                        {
                            case ConsoleKey.Tab: //добавить в корзину
                                productsForPay.Add(products[0]);
                                break;
                        }
                        Console.ReadKey();
                        break;
                    case ConsoleKey.NumPad4:
                        ShowGoods(products[3], 4,true);
                        keyInput = Console.ReadKey(true).Key;
                        switch (keyInput)
                        {
                            case ConsoleKey.Tab: //добавить в корзину
                                productsForPay.Add(products[0]);
                                break;
                        }
                        Console.ReadKey();
                        break;
                    case ConsoleKey.RightArrow:
                        if (offsetCount < countOfGoods-4)
                        {
                            offsetCount+=4;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (offsetCount>4)
                        {
                            offsetCount-=4;
                        }
                        break;
                    case ConsoleKey.B: //ПОСМОТРЕТЬ ТОВАРЫ В КОРЗИНЕ
                        Console.Clear();
                        PrintGoods(productsForPay);
                        Console.WriteLine("Для покупки нажмите Q");
                        keyInput = Console.ReadKey(true).Key;
                        if (keyInput == ConsoleKey.Q)
                        {
                            purchase.products = productsForPay;
                            purchase.Date = DateTime.Now;
                            purchase.UserId = user.Id;
                            if(qiwiService.Pay(user, purchase) == Status.PAID.ToString())
                            {
                                Console.WriteLine("Оплата завершена!");
                                using(var purchaceDataAccess = new PurchaseDataAccess())
                                {
                                    purchaceDataAccess.Insert(purchase);
                                    using(var basketDataAccess = new BasketDataAccess())
                                    {
                                        foreach(var good in productsForPay)
                                        {
                                            basket.GoodsId = good.Id;
                                            basket.PurchaseId = purchase.Id;
                                            basketDataAccess.Insert(basket);
                                        }
                                    }
                                    foreach(var payProduct in productsForPay)
                                    {
                                        purchaceDataAccess.UpdateGoodsAmount(payProduct);
                                        productsForPay.Remove(payProduct);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Произошла ошибка повторите позже");
                                Console.ReadKey();
                            }
                        }
                        break;
                }
            } while (true);
         }   
    }
}
