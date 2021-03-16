using System;
using System.Collections.Generic;

namespace TechnodomProject.Models
{
    public class Purchase : Entity
    {
        public ICollection<Item> Products { get; set; }
        public decimal Sum { get; set; }
        public DateTime Date { get; set; }

    }
}
