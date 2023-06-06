using MediatR;
using Framework;
using Microsoft.AspNetCore.Mvc;
using Quiz.Api.Models;
using Quiz.Application.Features.Questions.Commands.CreateQuestion;
using Quiz.Application.Features.Questions.Commands.DeleteQuestion;
using Quiz.Application.Features.Questions.Commands.UpdateQuestion;
using Quiz.Application.Features.Questions.Queries.GetQuestionQueryList;
using Quiz.Application.Features.Questions.Queries.GetQuestionQueryListPaging;
using Quiz.Application.Features.Quizs.Queries.GetQuizDetail;
using Quiz.Application.Response;
using Quiz.Application.Exceptions;

namespace Quiz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment env;
        public QuestionController(IMediator mediator, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.mediator = mediator;
            this.env = env;
        }
        [HttpGet("all",Name ="GetallQuestion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<QuestionListVm>> GetAllQusetion()
        {
            var dtos = await mediator.Send(new GetQuestionQueryList());
            return Ok(dtos);
        }
        [HttpGet("{id}", Name = "GetQuestionById")]
        public async Task<ActionResult<QuestionListVm>> GetQuestionById(int id)
        {
            var question = new GetQuizDetailQuery() { Id = id };
            return Ok(await mediator.Send(question));
        }

        [HttpGet("{PageIndex}-{PageSize}", Name = "GetQuestionListPaging")]
        public async Task<ActionResult<QuestionPageListVm>> GetQuestionListPaging(int PageIndex, int PageSize)
        {
            var Questionlist = new GetQuestionQueryListPaging() { PageIndex = PageIndex, PageSize = PageSize };
            return Ok(await mediator.Send(Questionlist));
        }


        [HttpPost(Name = "AddNewQuestion")]
         public async Task<ActionResult<CreateQuestionCommandResponse>> AddNewQuestion([FromForm] CreateQuestionCommandVm createQuestionCommand)
        {
            CreateQuestionCommand q = new CreateQuestionCommand
            {
                QuizID = createQuestionCommand.QuizID,
                question = createQuestionCommand.question,
                Option1 = createQuestionCommand.Option1,
                Option2 = createQuestionCommand.Option2,
                Option3 = createQuestionCommand.Option3,
                Option4 = createQuestionCommand.Option4,
                Answer = createQuestionCommand.Answer,
                Score = createQuestionCommand.Score,
                Picture = ""
            };
            if (createQuestionCommand.Picture != null)
            {
                var fn = System.IO.Path.GetFileName(createQuestionCommand.Picture.FileName);
                if (!fn.IsValidFileName())
                {
                    throw new BadRequestException("نام فایل صحیح نمی باشد");
                }

                if (createQuestionCommand.Picture.Length < 5000 || createQuestionCommand.Picture.Length > 480000)
                {
                    throw new BadRequestException ("حجم فایل صحیح نمیباشد");

                    
                }
                if (!fn.ToLower().EndsWith(".jpg") && !fn.ToLower().EndsWith(".png"))
                {
                    throw new BadRequestException("پسوند فایل صحیح نمیباشد");
                    
                }


                fn = fn.ToUniqueFileName();
                var path = $"{env.WebRootPath}/ProductImages/{fn}";
                var dbName = $"~/ProductImages/{fn}";

               
                FileStream fs = new FileStream(path, FileMode.Create);
                createQuestionCommand.Picture.CopyTo(fs);
                q.Picture = dbName;
            }
           
            var response = await mediator.Send(q);
            return Ok(response);
        }
        [HttpPut(Name = "UpdateQuestion")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BaseResponse>> UpdateQuestion([FromForm] UpdateQuestionCommandVm UpdateQuestionCommand)
        {
            UpdateQuestionCommand q = new UpdateQuestionCommand
            {
                QuestionID = UpdateQuestionCommand.QuestionID,
                QuizID = UpdateQuestionCommand.QuizID,
                question = UpdateQuestionCommand.question,
                Option1 = UpdateQuestionCommand.Option1,
                Option2 = UpdateQuestionCommand.Option2,
                Option3 = UpdateQuestionCommand.Option3,
                Option4 = UpdateQuestionCommand.Option4,
                Answer = UpdateQuestionCommand.Answer,
                Score = UpdateQuestionCommand.Score,
                Picture = ""
            };
            if (UpdateQuestionCommand.Picture != null)
            {
                var fn = System.IO.Path.GetFileName(UpdateQuestionCommand.Picture.FileName);
                if (!fn.IsValidFileName())
                {
                    throw new BadRequestException("نام فایل صحیح نمی باشد");
                }

                if (UpdateQuestionCommand.Picture.Length < 5000 || UpdateQuestionCommand.Picture.Length > 480000)
                {
                    throw new BadRequestException("حجم فایل صحیح نمیباشد");


                }
                if (!fn.ToLower().EndsWith(".jpg") && !fn.ToLower().EndsWith(".png"))
                {
                    throw new BadRequestException("پسوند فایل صحیح نمیباشد");

                }


                fn = fn.ToUniqueFileName();
                var path = $"{env.WebRootPath}/ProductImages/{fn}";
                var dbName = $"~/ProductImages/{fn}";


                FileStream fs = new FileStream(path, FileMode.Create);
                UpdateQuestionCommand.Picture.CopyTo(fs);
                q.Picture = dbName;
            }

            return Ok( mediator.Send(q));
        }

        [HttpDelete("{id}", Name = "DeleteQuestion")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<BaseResponse>> DeleteQuestion(int id)
        {
            var question = new DeleteQuestioncommand { QuestionID = id };
            return Ok(await mediator.Send(question));
        }
    }
}
