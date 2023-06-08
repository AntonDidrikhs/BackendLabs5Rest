using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [Route("api/v1/quizzes")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizUserService _service;
        public QuizController(IQuizUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<QuizDto> FindById(int id) 
        {
            var result = QuizDto.of(_service.FindQuizById(id));
            if(result == null)
            { return NotFound(); }
            else { return Ok(result); }
        }
        [HttpGet]
        public IEnumerable<QuizDto> FindAll()
        {
            return _service.FindAllQuizzes().Select(QuizDto.of).AsEnumerable();
        }
        [HttpPost]
        [Route("{quizId}/items/{itemId}")]
        public void SaveAnswer([FromBody] QuizItemAnswerDto dto, int quizId, int itemId)
        {
            _service.SaveUserAnswerForQuiz(quizId, dto.UserId, itemId, dto.Answer);
        }
        [HttpGet]
        public ResultsDto GetResult(int userId, int quizId)
        {
            return new ResultsDto { 
                UserId = userId, 
                QuizId = quizId,
                Score = _service.CountCorrectAnswersForQuizFilledByUser(quizId, userId)
            };
        }

    }
}
