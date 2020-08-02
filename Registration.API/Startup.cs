using GraphiQl;
using GraphQL;
using GraphQL.Conventions;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Registration.API.GraphQL_Mutations;
using Registration.API.GraphQL_Queries;
using Registration.API.GraphQL_Schema;
using Registration.Entities.Models;
using Registration.Entities.Seeding;
using Registration.Repository.Contracts;
using Registration.Repository.Repositories;
using Registration.Service.Contracts;
using Registration.Service.Services;

namespace Registration.API
{
    public class Startup
    {
        private IHostingEnvironment _hostingEnvironment;
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("RegistrationDatabase");

            services.AddSingleton(provider => new GraphQLEngine()
                .WithFieldResolutionStrategy(FieldResolutionStrategy.Normal)
                .BuildSchema(typeof(SchemaDefinition<RegistrationQuery, RegistrationMutation>)));
            services.AddDbContext<RegistrationsDBContext>(builder => builder.UseSqlServer(connectionString));
            services.AddScoped<IDependencyResolver>(dependencyResolver => new FuncDependencyResolver(dependencyResolver.GetRequiredService));
            services.AddScoped<RegistrationSchema>();

            services.AddGraphQL(options => { options.ExposeExceptions = _hostingEnvironment.IsDevelopment(); })
                    .AddGraphTypes(ServiceLifetime.Scoped).AddUserContextBuilder(httpContext => httpContext.User)
                    .AddDataLoader();

            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ISemesterService, SemesterService>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ISemesterRepository, SemesterRepository>();
            services.AddScoped<IGenericRepository<Student>, GenericRepository<Student>>();
            services.AddScoped<IGenericRepository<Address>, GenericRepository<Address>>();
            services.AddScoped<IGenericRepository<Subject>, GenericRepository<Subject>>();
            services.AddScoped<IGenericRepository<Course>, GenericRepository<Course>>();
            services.AddScoped<IGenericRepository<Semester>, GenericRepository<Semester>>();

            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, RegistrationsDBContext registrationsDBContext)
        {
            app.UseCors(builder =>
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseWebSockets();
            app.UseGraphiQl("/graphiql");
            app.UseGraphQL<RegistrationSchema>("/graphql");
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions { Path = "/ui/playground" });
            app.UseHttpsRedirection();
            registrationsDBContext.Seed();
            app.UseMvc();
        }
    }
}
