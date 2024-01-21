using System.ComponentModel.DataAnnotations;

namespace integral_api.Models
{
    public class AnswerModel
    {   
        [Key]
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

    }
}
