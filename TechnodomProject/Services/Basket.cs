using System;
using System.Collections.Generic;
using System.Text;
using TechnodomProject.Models;

namespace TechnodomProject.Services
{
    public class Basket
    {
        public List<Goods> products = new List<Goods>();

        public void Add(Goods productChoice) 
        {
            products = new List<Goods>();
            products.Add(productChoice);
        }
        public void Delete(Goods product) 
        {
            products.Remove(product);
        }
    }
}
