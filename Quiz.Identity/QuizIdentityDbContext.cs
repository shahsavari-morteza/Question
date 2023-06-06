using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quiz.Identity.Models;


namespace Quiz.Identity
{
    public class QuizIdentityDbContext:IdentityDbContext<ApplicationUser,ApplicationRole,int>
    {
        public QuizIdentityDbContext(DbContextOptions<QuizIdentityDbContext>options):base(options)
        {
            
        }
    }
}
