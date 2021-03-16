using System;

namespace TechnodomProject.Models
{
    public class Item : Entity
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public DateTime Publicitydate { get; set; }
        public int Raiting { get; set; }

        public Guid CategoryId { get; set; }
        public Guid ManufacturerId { get; set; }
        public Guid PurchaseId { get; set; }
        
    }
}