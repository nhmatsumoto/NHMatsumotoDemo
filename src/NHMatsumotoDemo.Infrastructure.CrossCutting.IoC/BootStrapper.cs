using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NHMatsumotoDemo.Domain.Interfaces;
using NHMatsumotoDemo.Infrastructure.Database.Context;
using NHMatsumotoDemo.Infrastructure.Database.Repository;
using NHMatsumotoDemo.Services;
using NHMatsumotoDemo.Services.Auth;
using NHMatsumotoDemo.Services.Translators;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System.Text;

namespace NHMatsumotoDemo.Infrastructure.CrossCutting.IoC
{
    public static class BootStrapper
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Services
            services.AddTransient<ITokenService, TokenService>();
         
            services.AddTransient<IEnderecoServices, EnderecoServices>();
            services.AddTransient<IAuthService, AuthService>();

            //Repository
            //Generic configuration
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
           
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            services.AddTransient<IEnderecoRepository, EnderecoRepository>();

            //Unit Of Work
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //configuration.GetConnectionString("MariaDbConnectionString")
            services
                .AddDbContextPool<MariaDbContext>(options => options
                .UseMySql(configuration.GetConnectionString("MariaDbConnectionString"), ServerVersion.Create(new Version(10, 11, 2), ServerType.MariaDb)));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new NHMatsumotoDemoProfile());
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);


            services.AddEndpointsApiExplorer();
            services.AddCors();
            services.AddControllers();

            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("JwtPrivateToken"));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        }
    }
}