using System;

namespace TechnodomProject.Models
{
    public class Basket : Entity
    {
        public Guid PurchaseId { get; set; }
        public Guid GoodsId { get; set; }
    }
}
