using System;
using System.Collections.Generic;
using System.Text;
using TechnodomProject.Data;
using TechnodomProject.Models;
using TechnodomProject.UI;

namespace TechnodomProject.Services
{
    public class Basket
    {
        public ICollection<Goods> products;

        public List<Guid> Add(Guid productChoice)  //корзина каждый раз записывает новые значения и SelectRaiting() не работает !
        {
            var dataAccess = new ItemsDataAccess();
            var webpage = new Webpage();
            
            var list = new List<Guid>();

            webpage.DrawAddToBasket();

            var result = dataAccess.SelectById(productChoice);

            foreach(var value in result)
            {
                list.Add(value.Id);
            }

            return list;
        }
        public void Delete(Goods product) 
        {
            products.Remove(product);
        }
    }
}
