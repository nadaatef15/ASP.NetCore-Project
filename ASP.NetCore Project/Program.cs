
using ASP.NetCore_Project.Extension;
using BusinessLogic.Managers.AccountManager;
using BusinessLogic.Managers.Clients;
using BusinessLogic.Managers.Identity;
using BusinessLogic.Services.GeneralServices;
using BusinessLogic.Services.User;
using CloudinaryDotNet;
using DataAccess.DBContext;
using DataAccess.Entity;
using DataAccess.Repo;
using Microsoft.AspNetCore.Identity;

namespace ASP.NetCore_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //Cloudinary
            var cloudinaryConfig = new CloudinaryDotNet.Account(
            builder.Configuration["Cloudinary:CloudName"],
            builder.Configuration["Cloudinary:ApiKey"],
            builder.Configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinary = new Cloudinary(cloudinaryConfig);
            builder.Services.AddSingleton(cloudinary);

            builder.Services.AddIdentity<UserEntity, IdentityRole>().AddEntityFrameworkStores<ProjectDBContext>();
            builder.Services.DBContextService(builder.Configuration);
            builder.Services.AuthenticationService(builder.Configuration);
            builder.Services.SwaggerConfiguration();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("myPolicy", policy =>
                {
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                    policy.AllowAnyHeader();

                });
            });


            RegisterServices(builder.Services);

        
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("myPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IRoleManager, RoleManager>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IClientsManager, ClientsManager>();
            services.AddScoped<IClientRepo, ClientRepo>();
        }
    }
}
