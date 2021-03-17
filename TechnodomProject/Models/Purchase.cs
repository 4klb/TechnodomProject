using System;
using System.Collections.Generic;

namespace TechnodomProject.Models
{
    public class Purchase : Entity
    {
        public ICollection<Goods> products;
        public decimal Sum { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
    }
}
