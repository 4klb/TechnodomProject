using System;
using System.Collections.Generic;
using System.Text;
using TechnodomProject.Data;
using TechnodomProject.Enum;
using TechnodomProject.Models;
using TechnodomProject.Services;

namespace TechnodomProject.UI
{
    public class Webpage
    {
        public List<Goods> ListId { get; set; }

        public List<Goods> DrawPageGoods(int key)
        {
            var goodsData = new ItemsDataAccess();

            var keyboard = new Keyboard
            {
                PagesArray = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 } //забирать общее кол во с бд
            };

            ListId = new List<Goods>();

            for (int i = 0; i < keyboard.PagesArray.Length; i++)
            {
                if (key == keyboard.PagesArray[i])
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(2, 12);
                    Console.WriteLine($"Текущая страница - {keyboard.PagesArray[i]} из {keyboard.PagesArray.Length}");

                    var products = goodsData.SelectItems(key);

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
                        Guid value = id.Id;
                        //ListId.Add(value);//?
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
            var keyboardService = new KeyboardService();
            var goodsData = new ItemsDataAccess();
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

        public ICollection<Comment> DrawComments() //Shift
        {
            return null;
        }

 


        public void DrawBasket(Basket basket)
        { 
            Console.Clear();

            foreach(var value in basket.products)
            {
                Console.WriteLine(value.Name);
                Console.WriteLine(value.Price);
                Console.WriteLine(value.Publicitydate);
            }
            
            Console.SetCursorPosition(50, 0);
            Console.WriteLine("Корзина");
            Console.SetCursorPosition(2, 4);
            Console.WriteLine("Чтобы вернуться к выбору страниц нажмите Escape");
            Console.WriteLine("Купить (нажмите Ctrl)");
            Console.SetCursorPosition(2, 8);
            Console.WriteLine("Нажмите любую клавишу чтобы перейти к Qiwi-касса");
            Console.ReadKey();
        }



        public void PurchaseItem(Basket purchase)
        {
            Console.SetCursorPosition(50, 0);
            Console.WriteLine("Страница покупки товара");

        }

        public void DeleteItem(Basket purchase)
        {
            Console.SetCursorPosition(50, 0);
            Console.WriteLine("Страница покупки товара");

        }









        public void MakePurchase(Basket basket) // совершить покупку F1
        {
            var user = new User();
            using (var userDataAccess = new UserDataAccess())
            {
                user = userDataAccess.SelectByPhone(user.Phone); // получаю поьзователя по телефону
            }

            var purchase = new Purchase(); //создается покупка

            purchase.products = basket.products; // добавляем продукты
            foreach (var product in purchase.products)
            {
                purchase.Sum += product.Price; // добавляем цену
            }
            purchase.Date = DateTime.Now; // добавляем время совершения покупки
            purchase.UserId = user.Id; // добавляем пользователи который совершил покупку

            var qiwiService = new QiwiService();
            var result = qiwiService.Pay(user, purchase); // платим за покупку
            
            using(var purchaseDataAccess = new PurchaseDataAccess())
            {
                if (result == Status.PAID.ToString()) //если покупка была успешной удаляем купленные продукты
                {
                    purchaseDataAccess.Insert(purchase);
                    foreach(var goods in purchase.products)
                    {
                        purchaseDataAccess.UpdateGoodsAmount(goods); // уменьшаем количество проданных продуктов
                    }
                    Console.WriteLine("Покупка успешна завершена");
                    Console.WriteLine("Хотите ли вы оставить отзыв? Введите Y - если да, N - если нет");
                    var inputAnswer = Console.ReadLine();
                    switch (inputAnswer.ToLower())
                    {
                        case "y":
                            foreach(var goods in purchase.products)
                            {
                                LeaveComment(goods, user);
                            }
                            break;
                        case "n":
                            break;
                        default:
                            break;
                    }
                }
                else 
                {
                    Console.WriteLine("Произошла ошибка");
                }
            }
        }


        public void LeaveComment(Goods goods, User user)
        {
            Console.WriteLine($"Введите комментарий для {goods.Category.Name} {goods.Name}");
            var comment = new Comment();
            comment.Text = Console.ReadLine();
            comment.Date = DateTime.Now;
            comment.UserId = user.Id;
            comment.GoodId = goods.Id;
            Console.WriteLine($"Введите рейтинг данного товара от 0 до 10");

        }

    }
}
