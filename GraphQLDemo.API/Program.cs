using GraphQLDemo.API.Schema.Course;
using GraphQLDemo.API.Schema.Subscriptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<CourseQuery>()
    .AddMutationType<CourseMutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions();


var app = builder.Build();
app.UseRouting();
app.UseWebSockets();
//app.MapGet("/", () => "Hello World!");
app.MapGraphQL();
app.Run();
