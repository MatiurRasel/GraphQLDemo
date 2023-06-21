using AppAny.HotChocolate.FluentValidation;
using FirebaseAdmin;
using FirebaseAdminAuthentication.DependencyInjection.Extensions;
using FirebaseAdminAuthentication.DependencyInjection.Models;
using FluentValidation.AspNetCore;
using GraphQLDemo.API.DataLoaders;
using GraphQLDemo.API.Schema.Course;
using GraphQLDemo.API.Schema.Instructor;
using GraphQLDemo.API.Schema.Subscriptions;
using GraphQLDemo.API.Services;
using GraphQLDemo.API.Services.Courses;
using GraphQLDemo.API.Services.Instructors;
using GraphQLDemo.API.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidation();
builder.Services.AddTransient<CourseTypeInputValidator>();

builder.Services.AddGraphQLServer()
    .AddQueryType<CourseQuery>()
    .AddMutationType<CourseMutation>()
    .AddSubscriptionType<Subscription>()
    .AddType<CourseType>().AddType<InstructorType>()
    .AddTypeExtension<CourseQuery>()
    .AddInMemorySubscriptions()
    .AddFiltering().AddSorting().AddProjections()
    .AddAuthorization()
    .AddFluentValidation( o=>
    {
        o.UseDefaultErrorMapper();
    });

builder.Services.AddSingleton(FirebaseApp.Create());
builder.Services.AddFirebaseAuthentication();
builder.Services.AddAuthorization(o =>
                    o.AddPolicy("IsAdmin",
                        p => p.RequireClaim(FirebaseUserClaimType.EMAIL, "matiurrasel1002@gmail.com")));

var connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddPooledDbContextFactory<SchoolDbContext>(options =>
    options.UseSqlite(connectionString).LogTo(Console.WriteLine));

builder.Services.AddScoped<CoursesRepository>();
builder.Services.AddScoped<InstructorsRepository>();
builder.Services.AddScoped<InstructorDataLoader>();
builder.Services.AddScoped<UserDataLoader>();

var app = builder.Build();
app.UseRouting();
app.UseAuthentication();
app.UseWebSockets();
//app.MapGet("/", () => "Hello World!");
app.MapGraphQL();
using (IServiceScope scope = app.Services.CreateScope())
{
    var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<SchoolDbContext>>();
    using (SchoolDbContext context = contextFactory.CreateDbContext())
    {
        context.Database.Migrate();
    }
}
//app.GetRequiredService<SchoolDbContext>();
app.Run();


//Add Extension For Visual Studio For SQLITE ..

//GUID Generator https://www.guidgenerator.com/online-guid-generator.aspx
