/*
 * MIT License
 * 
 * Copyright (c) 2019-2020 Stanislaw Schlosser <http://github.com/0x2aff>
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using ADNCWebpackBoilerplate.Contexts;
using ADNCWebpackBoilerplate.Entities.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace ADNCWebpackBoilerplate
{
    /// <summary>
    ///     Defines the request-handling pipeline and configures services.
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     Instantiates the Startup class.
        /// </summary>
        public Startup(IHostEnvironment environment)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("Config.json")
                .AddEnvironmentVariables()
                .Build();
        }

        /// <summary>
        ///     Current instance configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///     Defines the services used by the web application.
        /// </summary>
        /// <param name="services">Collection of service descriptors.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region Configuration

            services.Configure<Config>(options =>
            {
                options.Host = Configuration.GetSection("DatabaseHost").Value;
                options.Port = int.Parse(Configuration.GetSection("DatabasePort").Value);
                options.Username = Configuration.GetSection("DatabaseUsername").Value;
                options.Password = Configuration.GetSection("DatabasePassword").Value;
                options.Database = Configuration.GetSection("DatabaseName").Value;
            });

            #endregion

            #region Database Configuration

            services.AddDbContextPool<DatabaseContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseMySql(
                    $"Server={Configuration.GetSection("DatabaseHost").Value};" +
                    $"Port={Configuration.GetSection("DatabasePort").Value};" +
                    $"Database={Configuration.GetSection("DatabaseName").Value};" +
                    $"User={Configuration.GetSection("DatabaseUsername").Value};" +
                    $"Password={Configuration.GetSection("DatabasePassword").Value};",
                    sqlOptions =>
                    {
                        sqlOptions.ServerVersion(new Version(10, 5, 5), ServerType.MariaDb);
                        sqlOptions.MigrationsHistoryTable("MigrationHistory");
                        sqlOptions.EnableRetryOnFailure(5);
                    });
            });

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            #endregion

            #region IdentityConfiguration

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._-";
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "ADNCWebpackBoilerplateCookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(24);
                options.SlidingExpiration = true;

                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.AccessDeniedPath = "/error/403";

                options.ReturnUrlParameter = "returnUrl";
            });

            #endregion

            #region Web Configuration

            services.AddRazorPages(options => { options.RootDirectory = "/Client"; })
                .AddMvcOptions(options =>
                    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "required field"))
                .AddRazorRuntimeCompilation();

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = false;
            });

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });

            #endregion
        }

        /// <summary>
        ///     Defines the middleware for the request pipeline.
        /// </summary>
        /// <param name="app">Defines the application's request pipeline.</param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler("/error/500");
            app.UseHsts();

            app.UseStatusCodePages("text/plain", "Status code: {0}");
            app.UseStatusCodePagesWithRedirects("~/error/{0}");

            app.UseHttpsRedirection();

            app.UseResponseCompression();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseDbMigration().Wait();

            app.UseEndpoints(endpoints => endpoints.MapRazorPages());
        }
    }
}