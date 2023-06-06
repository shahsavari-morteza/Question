using Quiz.Application.Features.Answers.Queries.GetAnswerQueryList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Application.Models.Authentication.DTO.Users
{
    public class UserListPageDto
    {
        public int RecordCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public ICollection<UserListDto>? UserListDto { get; set; }
    }
}
