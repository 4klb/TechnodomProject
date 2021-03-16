using System;
using System.Collections.Generic;
using System.Text;
using TechnodomProject.UI;

namespace TechnodomProject.Data
{
    public class ItemDataAccess
    {
        public void Raiting()
        {

        }

        public List<string> SelectAllItems(int currentPage, int cursorPosition)
        {
            var webpage = new Webpage();

            var perPage = 10;

            List<string> items;

            List<string> price;

            using (var context = new ShopContext())
            {
                //if (currentPage < 0)
                //{
                //    items = context.Item.Select(item => item.Name)
                //   .Skip(0)
                //   .Take(perPage)
                //   .ToList();
                //}
                //else
                //{
                //}
                int itemsCount = context.Item.Count();

                items = context.Item.Select(item => item.Name)
                   .Skip((currentPage - 1) * perPage)
                   .Take(perPage)
                   .ToList();

                price = context.Item.Select(item => item.Price.ToString())
                            .Skip((currentPage - 1) * perPage)
                            .Take(perPage)
                            .ToList();

                webpage.DrawPageItems(items, currentPage, itemsCount);
            }
            return items;
        }

        public string SelectItemChoice(List<string> list, int cursorPosition)
        {
            Console.Clear();
            string result = string.Empty;
            for (int i = 0; i <= list.Count; i++)
            {
                if (i == cursorPosition)
                {
                    Console.WriteLine($"Товар {list[i - 1]}");
                    result = list[i - 1];
                }
            }
            Console.WriteLine("Купить (нажмите Tab)");
            return result;
        }
    }
}
