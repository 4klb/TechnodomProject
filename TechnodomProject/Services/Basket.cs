using System;
using System.Collections.Generic;
using System.Text;
using TechnodomProject.Models;
using TechnodomProject.UI;

namespace TechnodomProject.Services
{
    public class Basket
    {
        public List<Goods> products = new List<Goods>();

        public List<string> Add(Goods productChoice) 
        {
            var webpage = new Webpage();
            products = new List<Goods>();
            var list = new List<string>();
            products.Add(productChoice);
            webpage.DrawAddToBasket();
            foreach(var i in products)
            {
                list = i;
            }
            return list;
        }
        public void Delete(Goods product) 
        {
            products.Remove(product);
        }
    }
}
