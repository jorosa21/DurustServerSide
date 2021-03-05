using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using APIGateway.Helper;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.Configuration;


namespace APIGateway
{

    //public class Startup
    //{
    //    public Startup(IWebHostEnvironment env)
    //    {
    //        var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
    //        builder.SetBasePath(env.ContentRootPath)
    //               .AddJsonFile("appsettings.json")
    //               //add configuration.json
    //               .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    //               .AddEnvironmentVariables();

    //        Configuration = builder.Build();
    //    }

    //    public IConfigurationRoot Configuration { get; }

    //    public void ConfigureServices(IServiceCollection services)
    //    {
    //        var audienceConfig = Configuration.GetSection("Audience");

    //        var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"]));
    //        var tokenValidationParameters = new TokenValidationParameters
    //        {
    //            ValidateIssuerSigningKey = true,
    //            IssuerSigningKey = signingKey,
    //            ValidateIssuer = true,
    //            ValidIssuer = audienceConfig["Iss"],
    //            ValidateAudience = true,
    //            ValidAudience = audienceConfig["Aud"],
    //            ValidateLifetime = true,
    //            ClockSkew = TimeSpan.Zero,
    //            RequireExpirationTime = true,
    //        };

    //        services.AddAuthentication(o =>
    //        {
    //            o.DefaultAuthenticateScheme = "TestKey";
    //        })
    //        .AddJwtBearer("TestKey", x =>
    //        {
    //            x.RequireHttpsMetadata = false;
    //            x.TokenValidationParameters = tokenValidationParameters;
    //        });

    //        services.AddOcelot(Configuration);
    //    }

    //    public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    //    {
    //        app.UseRouting();
    //        app.UseAuthorization();
    //        app.UseAuthentication();
    //        await app.UseOcelot();
    //    }
    //}


    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940




        public void ConfigureServices(IServiceCollection services)
        {
            //IOptions<AppSetting> appSettings;
            //_appSettings = ;

            //services.Configure<AppSetting>(Configuration.GetSection("AppSettings"));

            var secret = "Sample keyword which is used for verification";
            var key = Encoding.ASCII.GetBytes(secret);


            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services
                .AddOcelot();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            await app.UseOcelot();

        }
    }
}
