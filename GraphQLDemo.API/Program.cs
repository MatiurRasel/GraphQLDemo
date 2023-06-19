using GraphQLDemo.API.DataLoaders;
using GraphQLDemo.API.Schema.Course;
using GraphQLDemo.API.Schema.Subscriptions;
using GraphQLDemo.API.Services;
using GraphQLDemo.API.Services.Courses;
using GraphQLDemo.API.Services.Instructors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<CourseQuery>()
    .AddMutationType<CourseMutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions()
    .AddFiltering().AddSorting().AddProjections();

var connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddPooledDbContextFactory<SchoolDbContext>(options =>
    options.UseSqlite(connectionString).LogTo(Console.WriteLine));

builder.Services.AddScoped<CoursesRepository>();
builder.Services.AddScoped<InstructorsRepository>();
builder.Services.AddScoped<InstructorDataLoader>();

var app = builder.Build();
app.UseRouting();
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
