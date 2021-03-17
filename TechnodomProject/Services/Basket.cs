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

        public void Add(Goods productChoice) 
        {
            var webpage = new Webpage();
            products = new List<Goods>();
            products.Add(productChoice);
            webpage.DrawAddToBasket();
        }
        public void Delete(Goods product) 
        {
            products.Remove(product);
        }
    }
}
