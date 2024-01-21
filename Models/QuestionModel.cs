using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace integral_api.Models
{
    public class QuestionModel
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public List<AnswerModel> Answers { get; set; }
    }
}
