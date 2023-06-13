using GraphQLDemo.API.Schema.Course;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGraphQLServer()
    .AddQueryType<CourseQuery>()
    .AddMutationType<CourseMutation>();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.MapGraphQL();
app.Run();
