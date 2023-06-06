using Quiz.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Application.Models.Authentication.DTO.Users
{
    public class AddNewUserResponse:BaseResponse
    {
        public AddNewUserResponse():base()
        {
            
        }
        public int UserId { get; set; }
    }
}
