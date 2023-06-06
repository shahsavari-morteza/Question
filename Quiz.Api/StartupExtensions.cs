﻿using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Quiz.Api.Middleware;
using Quiz.Api.Services;
using Quiz.Application;
using Quiz.Application.Contracts;
using Quiz.Identity;
using Quiz.Persistence;


namespace Quiz.Api
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            AddSwagger(builder.Services);
            builder.Services.AddAplicationServices();
            builder.Services.AddPersistanceServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddControllers();




            builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>(); 


            builder.Services.AddCors(options => {
                options.AddPolicy("Open", builder =>
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                });

            return builder.Build();
        }
        public static WebApplication ConfigurePipline(this WebApplication app)
        {
       if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quiz Management API");
                    });
            }
            app.UseHttpsRedirection();
            //app.UseRouting();
            app.UseAuthentication();
            app.UseCustomExceptionHandler();
            app.UseCors("Open");
            app.UseAuthorization();
            app.MapControllers(); 
            return app;

        }
    
        private static void AddSwagger(IServiceCollection services) 
        {
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Quiz",
                });
               // c.OperationFilter<FileResultContextTypeOperationFilter>();
            });
        }
        public static async Task ResetDataBaseAsync(this WebApplication app)
        {
            using var scope=app.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetService<QuizDbContext>();
                if(context != null)
                {
                    await context.Database.EnsureDeletedAsync();
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {

               //add logging here later on
            }
        }
    }
}
