using AutoMapper;
using MediatR;
using Quiz.Application.Contracts;
using Quiz.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Application.Features.Quizs.Commands.CreateQuiz
{
    public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, CreateQuizCommandResponse>
    {
        private readonly IAsyncRepository<Quiz.Domain.Entities.Quiz> repo;
        private readonly ILoggedInUserService loggedInUserService;
        private readonly IMapper mapper;
        public CreateQuizCommandHandler(IAsyncRepository<Quiz.Domain.Entities.Quiz> repo,IMapper mapper, ILoggedInUserService loggedInUserService)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.loggedInUserService = loggedInUserService;
        }
        public async Task<CreateQuizCommandResponse> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
            var createQuizCommandResponse=new CreateQuizCommandResponse();
            var Validate = new CreateQuizCommandValidator();
            var ValidateResult=await Validate.ValidateAsync(request);
            if (ValidateResult.Errors.Count > 0)
            {
                createQuizCommandResponse.Success = false;
                createQuizCommandResponse.ValidationErrors=new List<string> ();
                foreach(var error in ValidateResult.Errors)
                {
                    createQuizCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            if (createQuizCommandResponse.Success)
            {
                var quiz = mapper.Map<Quiz.Domain.Entities.Quiz>(request);
                quiz.Creator = loggedInUserService.UserId;
                quiz.QuizUrl = $"{RandomString(10)}{quiz.QuizName}{RandomString(10)}";
                quiz=await repo.AddAsync(quiz);
                createQuizCommandResponse.QuizID=quiz.QuizID;

            }
            return createQuizCommandResponse;
        }

        private Random random = new Random();
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
