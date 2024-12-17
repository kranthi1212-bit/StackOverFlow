using System.ComponentModel.DataAnnotations;

namespace StackOverFlow.Models
{
    public class Answer
    {
    public int AnswerId { get; set; }
    [Required]
    public string AnsContent { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Foreign key for User
    public string UserId { get; set; }
    // Foreign key for Question
    public int QuestionId { get; set; }
    public Question Question { get; set; }
    }
}
