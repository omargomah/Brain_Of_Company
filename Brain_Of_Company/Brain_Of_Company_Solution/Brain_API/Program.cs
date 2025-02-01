
using Microsoft.EntityFrameworkCore;
using System;
using Interfaces;
using DAL;
using Brain_DAL.Data;

namespace Brain_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var con = builder.Configuration.GetConnectionString("con");
            builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(con));
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddTransient<SoftDeleteOfEmployeeMiddleWare>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseMiddleware<SoftDeleteOfEmployeeMiddleWare>();
            
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
