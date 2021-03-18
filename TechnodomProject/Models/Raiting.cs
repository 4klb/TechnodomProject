using System;
using System.Collections.Generic;
using System.Text;

namespace TechnodomProject.Models
{
    public class Raiting : Entity
    {
        public Guid ItemId { get; set; }
        public int ItemsRaiting { get; set; }
    }
}
