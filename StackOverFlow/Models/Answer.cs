﻿namespace StackOverFlow.Models
{
    public class Answer
    {
    public int AnswerId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Foreign key for User
    public string UserId { get; set; }
    // Foreign key for Question
    public int QuestionId { get; set; }
    public Question Question { get; set; }
    }
}
