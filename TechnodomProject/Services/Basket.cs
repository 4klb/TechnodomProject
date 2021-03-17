using System;
using System.Collections.Generic;
using System.Text;
using TechnodomProject.Models;

namespace TechnodomProject.Services
{
    public class Basket
    {
        public List<Item> products = new List<Item>();
        public void Add(Item productChoice) 
        {
            products = new List<Item>();
            products.Add(productChoice);
        }
        public void Delete(Item product) 
        {
            products.Remove(product);
        }
    }
}
