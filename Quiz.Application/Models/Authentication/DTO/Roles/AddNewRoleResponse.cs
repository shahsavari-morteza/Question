using Quiz.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Application.Models.Authentication.DTO.Roles
{
    public class AddNewRoleResponse:BaseResponse
    {
        public AddNewRoleResponse():base()
        {
            
        }
        public  int  Id { get; set; }
    }
}
