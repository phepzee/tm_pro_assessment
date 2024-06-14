using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ThandoMazibuko.Data;
using ThandoMazibuko.Models;
using ThandoMazibuko.Repository.Interface;
using ThandoMazibuko.Repository.Services;

namespace ThandoMazibuko
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddDbContext<CustomerFeedbackDbContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("CustomerFeedbackContext")));

			// Register Repository
			builder.Services.AddScoped<ICustomerFeedbackRepository, CustomerFeedbackRepository>();
			builder.Services.AddScoped<IEmailRepository, EmailRepository>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IAuthRepository, AuthRepository>();
			
			// Adding Authentication  
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})

						// Adding Jwt Bearer  
						.AddJwtBearer(options =>
						{
							options.SaveToken = true;
							options.RequireHttpsMetadata = false;
							options.TokenValidationParameters = new TokenValidationParameters()
							{
								ValidateIssuer = true,
								ValidateAudience = true,
								ValidAudience = builder.Configuration["JWT:ValidAudience"],
								ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
								ClockSkew = TimeSpan.Zero,
								IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
							};
						});

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
