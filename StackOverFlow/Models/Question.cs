using System.ComponentModel.DataAnnotations;

namespace StackOverFlow.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key for User
        public string UserId { get; set; }

        // Navigation Properties
        public ICollection<Answer> Answers { get; set; }

    }
}
