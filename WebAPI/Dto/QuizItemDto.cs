using ApplicationCore.Models;

namespace WebAPI.Dto
{
    public class QuizItemDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public List<string> Options { get; set; }

        public static QuizItemDto of(QuizItem quiz)
        {
            if(quiz is null)
            {
                return null;
            }
            List<string> options = new List<string>();
            options.AddRange(quiz.IncorrectAnswers);
            options.Add(quiz.CorrectAnswer);
            return new QuizItemDto()
            {
                Id = quiz.Id,
                Question = quiz.Question,
                Options = options
            };
        }
    }
}
