using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Application.Models.Authentication.DTO.Users
{
    public class UserListDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
       // public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public int AccessFailedCount { get; set; }
    }
}
