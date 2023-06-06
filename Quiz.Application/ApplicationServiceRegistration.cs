﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace Quiz.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services) 
        { 
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
