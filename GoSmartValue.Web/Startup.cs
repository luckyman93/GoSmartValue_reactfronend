using Autofac;
using Autofac.Extensions.DependencyInjection;
using AV.Common.DTOs;
using AV.Common.Entities;
using AV.Contracts;
using AV.Contracts.ConfigurationDtos;
using AV.Contracts.Models;
using AV.Infrastructure.Services.PaymentGateways.Cellulant;
using AV.Persistence.EntityFramework;
using FluentValidation.AspNetCore;
using GoSmartValue.Web.AppStartConfigs;
using GoSmartValue.Web.Filters;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using AV.Contracts.Models.Accounts.Subscriptions.Command;
using User = AV.Common.Entities.User;

namespace GoSmartValue.Web
{
    public class Startup
    {
        private readonly string _connectionString;
        public static IDictionary EnvironmentVariables { get; set; } = Environment.GetEnvironmentVariables();
        public ILifetimeScope AutofacContainer { get; private set; }

        public IConfiguration Configuration { get; }
        public static string Hostname => EnvironmentVariables["Hostname"].ToString();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _connectionString = GetMysqlUnixSocketConnection().ConnectionString;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add functionality to inject IOptions<T>
            services.AddOptions();

            services.AddHttpClient();
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()); })
                .AddXmlDataContractSerializerFormatters();

            services.AddLogging();

            services.AddSingleton<Microsoft.AspNetCore.Mvc.ViewFeatures.IHtmlGenerator,
                CustomHtmlGenerator>();

            services.AddApplicationInsightsTelemetry();

            services.AddDbContext<ValuationsContext>(options =>
                options.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString)
                ));

            services.AddIdentity<User, Role>(options => { options.SignIn.RequireConfirmedEmail = true; })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ValuationsContext>();

            services.AddMvc()
                .AddFluentValidation(opt => { opt.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()); });

            services.AddAntiforgery();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                // Password settings.
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 4;

                // Lockout settings.
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = false;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.CustomSchemaIds(x => x.FullName);
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Gosmartvalue API",
                    Description = "Automating the Real Estate industry",
                    TermsOfService = new Uri("https://gosmartvalue.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Miselo Kangwa",
                        Email = "miselo.dev@gmail.com",
                        Url = new Uri("https://gosmartvalue.com"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under commercial license",
                        Url = new Uri("https://gosmartvalue.com/api-license"),
                    }
                });

                //// To Enable authorization using Swagger (JWT)    
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}

                    }
                });

                c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Name = "ApiKey",
                    Description = "ApiKey header using the Api scheme. Example: \"Apikey: {token}\"",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" }
                        },
                        new string[] { "readAccess", "writeAccess" }
                    }
                });

            });

            services.AddAutoMapper(typeof(AutoValuationProfile));

            services.AddCors(options =>
            {
                options.AddPolicy(Environments.Development,
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .Build();
                    });
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder
                            .WithOrigins("https://*.gosmartvalue.com", "gosmartvalue.com",
                                "www.gosmartvalue.com", "https://staginggosmartvalueapi-bxxyuu2dva-ew.a.run.app/api/baskets/success",
                                "https://gosmartvalue.com/api/baskets/success")
                            .AllowCredentials()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .Build();
                    });
            });

            services.ConfigureAuthentication();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.AccessPolicies.Admin, policy =>
                    policy.RequireAssertion(context => context.User.IsInRole("admin")));
                options.AddPolicy(Constants.AccessPolicies.ManagerAccess, policy =>
                    policy.RequireAssertion(context => context.User.IsInRole("manager")
                                                       || context.User.IsInRole("admin")));
                options.AddPolicy(Constants.AccessPolicies.InternalStaff, policy =>
                    policy.RequireAssertion(context => context.User.IsInRole("manager")
                                                       || context.User.IsInRole("admin")
                                                       || context.User.IsInRole("analyst")));

                options.AddPolicy(Constants.AccessPolicies.All, policy =>
                    policy.RequireAssertion(context => context.User.Identity.IsAuthenticated));

                options.AddPolicy(Constants.AccessPolicies.ValuerAccess, policy =>
                    policy.RequireAssertion(context => context.User.IsInRole("admin")
                                                       || context.User.IsInRole("manager")
                                                       || context.User
                                                           .IsInRole("analyst") //staff working for gosmartvalue
                                                       || context.User.IsInRole("valuer")
                    ));
                options.AddPolicy(Constants.AccessPolicies.SalesAgentAccess, policy =>
                    policy.RequireAssertion(context => context.User.IsInRole("admin")
                                                       || context.User.IsInRole("manager")
                                                       || context.User.IsInRole("analyst")
                                                       || context.User.IsInRole("salesagent")
                    ));
                options.AddPolicy(Constants.AccessPolicies.Corporate, policy =>
                    policy.RequireAssertion(context => context.User.IsInRole("admin")
                                                       || context.User.IsInRole("manager")
                                                       || context.User.IsInRole("analyst")
                                                       || context.User.IsInRole("corporate")
                    ));
            });

            services.AddHealthChecks();

            services.Configure<SmtpConfiguration>(options =>
                {
                    options.UserName = EnvironmentVariables["SmtpSettings_UserName"]?.ToString();
                    options.Password = EnvironmentVariables["SmtpSettings_Password"]?.ToString();
                    options.SmtpMailServer = EnvironmentVariables["SmtpSettings_SmtpMailServer"]?.ToString();
                    options.FromEmailAddress = EnvironmentVariables["SmtpSettings_FromEmailAddress"]?.ToString();
                    options.Port = Convert.ToInt32(EnvironmentVariables["SmtpSettings_Port"]);
                    options.UseTLS = Convert.ToBoolean(EnvironmentVariables["SmtpSettings_UseTLS"]);
                    options.AlternativePort = Convert.ToInt32(EnvironmentVariables["SmtpSettings_AlternativePort"]);
                }
            );


            // Add our Config object so it can be injected
            services.Configure<ConfigurationDpo>(opt =>
            {
                opt.CreateTokenUrl = EnvironmentVariables["PaymentGateways_Dpo_CreateTokenUrl"].ToString();
                opt.PaymentPageUrl = EnvironmentVariables["PaymentGateways_Dpo_PaymentPageUrl"].ToString();
                opt.CompanyToken = EnvironmentVariables["PaymentGateways_Dpo_CompanyToken"].ToString();
            });

            services.Configure<ConfigurationUiDto>(opt =>
            {
                opt.API_URL = EnvironmentVariables["API_URL"].ToString();
                opt.HOST = EnvironmentVariables["HOST"].ToString();
                opt.API_KEY = EnvironmentVariables["API_KEY"].ToString();
                opt.FIRE_API_KEY = EnvironmentVariables["FIRE_API_KEY"].ToString();
                opt.FIRE_AUTH_DOMAIN = EnvironmentVariables["FIRE_AUTH_DOMAIN"].ToString();
                opt.FIRE_PROJECT_ID = EnvironmentVariables["FIRE_PROJECT_ID"].ToString();
                opt.FIRE_STORAGE_BUCKETID = EnvironmentVariables["FIRE_STORAGE_BUCKETID"].ToString();
                opt.FIRE_MESSAGING_SENDER_ID = EnvironmentVariables["FIRE_MESSAGING_SENDER_ID"].ToString();
                opt.FIRE_APP_ID = EnvironmentVariables["FIRE_APP_ID"].ToString();
                opt.FIRE_MEASUREMENT_ID = EnvironmentVariables["FIRE_MEASUREMENT_ID"].ToString();
            });

            services.Configure<ConfigurationCellulantDto>(opt =>
            {
                opt.ServiceCode = EnvironmentVariables["Cellulant_ServiceCode"].ToString();
                opt.IvKey = EnvironmentVariables["Cellulant_IvKey"].ToString();
                opt.ClientCode = EnvironmentVariables["Cellulant_ClientCode"].ToString();
                opt.SecretKey = EnvironmentVariables["Cellulant_SecretKey"].ToString();
                opt.AccessKey = EnvironmentVariables["Cellulant_AccessKey"].ToString();
                opt.CurrencyCode = EnvironmentVariables["Cellulant_CurrencyCode"].ToString();
                opt.ServiceShortName = EnvironmentVariables["Cellulant_ServiceShortName"].ToString();
                opt.CallBackUrl = EnvironmentVariables["Cellulant_CallBackUrl"].ToString();
                opt.FailRedirectUrl = EnvironmentVariables["Cellulant_FailRedirectUrl"].ToString();
                opt.SuccessRedirectUrl = EnvironmentVariables["Cellulant_SuccessRedirectUrl"].ToString();
                opt.PendingRedirectUrl = EnvironmentVariables["Cellulant_PendingRedirectUrl"].ToString();
            });

            services.Configure<CellulantEncryptionService>(opt =>
            {
                opt.IvKey = EnvironmentVariables["Cellulant_IvKey"]?.ToString() ?? "";
                opt.SecretKey = EnvironmentVariables["Cellulant_SecretKey"]?.ToString() ?? "";
            });

            services.AddMediatR(typeof(CreatePackageCommand));

            services.AddSingleton<SymmetricSecurityKey>(provider =>
            {
                //Security Key
                var securityKey = EnvironmentVariables["AuthKey"].ToString();
                return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            });

            services.AddSpaStaticFiles(configuration => configuration.RootPath = $"ClientApp/dist/vantageproperties");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ValuationsContext context)
        {
            if (env.IsProduction())
            {
                app.UseCors();
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            else
            {
                //app.UseCors(Environments.Development);
                app.UseDeveloperExceptionPage();
            }
            
            //allow angular UI to make calls to local API
            if (env.IsDevelopment())
            {
                app.UseCors(Environments.Development);
            }
            // If, for some reason, you need a reference to the built container, you
            // can use the convenience extension method GetAutofacRoot.
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            //app.UseHealthChecks("/health");
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            //app.UseRewriter(new RewriteOptions()
            //    .AddRedirectToWww()
            //    .AddRedirectToHttps());
            //    .AddRedirect("index.html", "/"));

            app.UseRouting();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gosmartvalue v2");
                c.DocExpansion(DocExpansion.None);
            });

            app.UseAuthentication();

            app.UseAuthorization();
            //app.UseCookiePolicy();

            app.UseStatusCodePages(async context =>
            {
                if (context.HttpContext.Request.Path.StartsWithSegments("/api"))
                {
                    if (!context.HttpContext.Response.ContentLength.HasValue || context.HttpContext.Response.ContentLength == 0)
                    {
                        // You can change ContentType as json serialize
                        context.HttpContext.Response.ContentType = "application/json";
                        await context.HttpContext.Response.WriteAsync($"Status Code: {context.HttpContext.Response.StatusCode}");
                    }
                }
                else
                {
                    // You can ignore redirect
                    context.HttpContext.Response.Redirect($"/error?code={context.HttpContext.Response.StatusCode}");
                }
            });

            app.UseEndpoints(endPoints =>
            {
                endPoints.MapControllers();
                endPoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endPoints.MapRazorPages();
            });
            
            if (context.Database.IsRelational())
            {
                context.Database.Migrate();
            }

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";
            });
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.
            //builder.RegisterModule(new MyApplicationModule());
            builder.BuildApplicationContainer(Configuration);
        }

        private DbConnection GetMysqlUnixSocketConnection()
        {
            // [START cloud_sql_mysql_dotnet_ado_connection_socket]
            // Equivalent connection string:
            // "Server=<dbSocketDir>/<INSTANCE_CONNECTION_NAME>;Uid=<DB_USER>;Pwd=<DB_PASS>;Database=<DB_NAME>;Protocol=unix"
            var connectionString = new MySqlConnectionStringBuilder()
            {
                // The Cloud SQL proxy provides encryption between the proxy and instance.
                SslMode = MySqlSslMode.None,
                // Remember - storing secrets in plaintext is potentially unsafe. Consider using
                // something like https://cloud.google.com/secret-manager/docs/overview to help keep
                // secrets secret.
                Server = Environment.GetEnvironmentVariable("DB_Host"),
                UserID = Environment.GetEnvironmentVariable("DB_User"),   // e.g. 'my-db-user
                Password = Environment.GetEnvironmentVariable("DB_Password"), // e.g. 'my-db-password'
                Database = Environment.GetEnvironmentVariable("DB_Database"), // e.g. 'my-database'
                Port = Convert.ToUInt32(Environment.GetEnvironmentVariable("DB_Port")),
                ConnectionProtocol = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
                    MySqlConnectionProtocol.Tcp : MySqlConnectionProtocol.Unix
            };
            connectionString.Pooling = true;
            connectionString.AllowPublicKeyRetrieval = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            // Specify additional properties here.
            // [START_EXCLUDE]
            SetDbConfigOptions(connectionString);
            // [END_EXCLUDE]
            DbConnection connection =
                new MySqlConnection(connectionString.ConnectionString);
            // [END cloud_sql_mysql_dotnet_ado_connection_socket]
            return connection;
        }

        private void SetDbConfigOptions(MySqlConnectionStringBuilder connectionString)
        {
            // [START cloud_sql_mysql_dotnet_ado_limit]
            // MaximumPoolSize sets maximum number of connections allowed in the pool.
            connectionString.MaximumPoolSize = 20;
            // MinimumPoolSize sets the minimum number of connections in the pool.
            connectionString.MinimumPoolSize = 0;
            // [END cloud_sql_mysql_dotnet_ado_limit]
            // [START cloud_sql_mysql_dotnet_ado_timeout]
            // ConnectionTimeout sets the time to wait (in seconds) while
            // trying to establish a connection before terminating the attempt.
            connectionString.ConnectionTimeout = 60;
            // [END cloud_sql_mysql_dotnet_ado_timeout]
            // [START cloud_sql_mysql_dotnet_ado_lifetime]
            // ConnectionLifeTime sets the lifetime of a pooled connection
            // (in seconds) that a connection lives before it is destroyed
            // and recreated. Connections that are returned to the pool are
            // destroyed if it's been more than the number of seconds
            // specified by ConnectionLifeTime since the connection was
            // created. The default value is zero (0) which means the
            // connection always returns to pool.
            connectionString.ConnectionLifeTime = 1800; // 30 minutes
            // [END cloud_sql_mysql_dotnet_ado_lifetime]
        }
    }

    public enum AppEnvironment
    {
        Development,
        Production
    }
}
