using System;
using System.Collections.Generic;

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
         
        // public ICollection<Comment> Comments { get; set; } - это кажется вообще не нужно
    }
}