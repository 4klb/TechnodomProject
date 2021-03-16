using System;

namespace TechnodomProject.Models
{
    public class Comment : Entity
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Guid ItemId { get; set; }
        public Guid UserId { get; set; }
    }
}
